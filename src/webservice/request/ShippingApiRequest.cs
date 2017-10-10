/*
Copyright 2016 Pitney Bowes Inc.

Licensed under the MIT License(the "License"); you may not use this file except in compliance with the License.  
You may obtain a copy of the License in the README file or at
   https://opensource.org/licenses/MIT 
Unless required by applicable law or agreed to in writing, software distributed under the License is distributed 
on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.  See the License 
for the specific language governing permissions and limitations under the License.
*/

using System;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using System.Reflection;
using System.Text;
using System.Collections.Generic;

namespace PitneyBowes.Developer.ShippingApi
{
    public abstract class ShippingApiRequest : IShippingApiRequest
    {
        public abstract string ContentType {get;}
        public abstract StringBuilder Authorization { get; set; }

        /// <summary>
        /// When recording a response, add a suffix to make the  filename unique
        /// </summary>
        virtual public string RecordingSuffix => "";

        public static string RecordingFullPath(IShippingApiRequest request, string resource, ISession session)
        { 
            string dirname = session.RecordPath;
            StringBuilder uriBuilder = new StringBuilder(resource);
            AddRequestResource(request, uriBuilder);

            string fullPath = (dirname + uriBuilder.ToString().ToLower() + @"\")
                .Replace('?', Path.DirectorySeparatorChar)
                .Replace('&', Path.DirectorySeparatorChar)
                .Replace('/', Path.DirectorySeparatorChar)
                .Replace('=', '-');
            string fileName = "default";

            if (session == null) session = Globals.DefaultSession;

            foreach (var h in request.GetHeaders())
            {
                if (h.Item3.ToLower().Equals("authorization"))
                {
                    if (fileName.Equals("default"))
                    {
                        fileName = h.Item2.Substring(0, 8).ToLower();
                    }
                }
                if (h.Item1.Name.ToLower().Equals("x-pb-transactionid"))
                {
                    fileName = h.Item2.ToLower();
                }
            }
            fileName += request.RecordingSuffix;
            fileName += ".http";
            return fullPath + Path.DirectorySeparatorChar + fileName;
        }

        private static void ProcessRequestAttributes<Attribute>(object o, Action<Attribute, string, string, string> propAction) where Attribute : ShippingApiAttribute
        {
            foreach (var propertyInfo in o.GetType().GetProperties())
            { 
                foreach (object attribute in propertyInfo.GetCustomAttributes(true))
                {
                    if (attribute is Attribute)
                    {
                        string v;
                        if (propertyInfo.GetValue(o) is StringBuilder)
                        {
                            v = ((StringBuilder)propertyInfo.GetValue(o)).ToString();
                        }
                        else
                        {
                            if (propertyInfo.GetValue(o) == null)
                            {
                                v = null;
                            }
                            else
                            {
                                if (((Attribute)attribute).Format != null)
                                    v = string.Format(((Attribute)attribute).Format, propertyInfo.GetValue(o));
                                else
                                {
                                    var val = propertyInfo.GetValue(o) as string;
                                    v = val ?? propertyInfo.GetValue(o).ToString();
                                }
                            }
                        }
                        propAction((Attribute)attribute, ((Attribute)attribute).Name, v, propertyInfo.Name);
                    }
                }
            }
        }
        public static void AddRequestResource(object o, StringBuilder uri)
        {
            ProcessRequestAttributes<ShippingApiResourceAttribute>( o,
               (a, s, v, p) => {
                   if (v == null || v.Equals(String.Empty)) return;
                   uri.Append('/');
                   uri.Append(WebUtility.UrlEncode(s));
                   if (a.AddId)
                   {
                       uri.Append('/');
                       uri.Append(WebUtility.UrlEncode((string)v));
                   }
                   if (a.PathSuffix != null)
                   {
                       uri.Append(a.PathSuffix);
                   }
               }
           );
        }
        public static void AddRequestQuery(object o, StringBuilder uri)
        {

            bool hasQuery = false;

            ProcessRequestAttributes<ShippingApiQueryAttribute>( o, 
               (a, s, v, p) => {
                   if (a.OmitIfEmpty && (v == null || v.Equals(String.Empty))) return;
                   if (!hasQuery)
                   {
                       uri.Append('?');
                       hasQuery = true;
                   }
                   else
                   {
                       uri.Append('&');
                   }
                   uri.Append(WebUtility.UrlEncode(s));
                   if (v != null)
                   {
                       uri.Append('=');
                       uri.Append(WebUtility.UrlEncode(v));
                   }
               }
           );
        }

        public virtual void SerializeBody( StreamWriter writer, ISession session)
        {
            SerializeBody(this, writer, session);
        }

        public static void SerializeBody(IShippingApiRequest request, StreamWriter writer, ISession session)
        {
            switch (request.ContentType)
            {
                case "application/json":
                    var serializer = new JsonSerializer() { ContractResolver = new ShippingApiContractResolver() };
                    ((ShippingApiContractResolver)serializer.ContractResolver).Registry = session.SerializationRegistry;
                    serializer.NullValueHandling = NullValueHandling.Ignore;
                    serializer.Formatting = Formatting.Indented;
                    serializer.Serialize(writer, request);
                    writer.Flush();
                    return;

                case "application/x-www-form-urlencoded":
                    bool isFirst = true;

                    foreach (var propertyInfo in request.GetType().GetProperties())
                    {
                        foreach (object attribute in propertyInfo.GetCustomAttributes(true))
                        {
                            if (attribute is JsonPropertyAttribute)
                            {
                                if (!isFirst) { writer.WriteLine(); isFirst = false; }
                                writer.Write(((JsonPropertyAttribute)attribute).PropertyName);
                                writer.Write('=');
                                writer.Write((string)propertyInfo.GetValue(request));
                            }
                        }
                    }
                    writer.Flush();

                    return;
                default:
                    session.LogConfigError("Unrecognized request content type:" + request.ContentType);
                    throw new InvalidOperationException("Unrecognized request content type:" + request.ContentType); 
            }
        }
        public virtual string GetUri(string baseUrl)
        {
            var uri = new StringBuilder(baseUrl);
            AddRequestResource(this, uri);
            AddRequestQuery(this, uri);
            return uri.ToString();
        }
        public static IEnumerable<Tuple<ShippingApiHeaderAttribute, string, string>> GetHeaders( object o)
        {
            foreach (var propertyInfo in o.GetType().GetProperties())
            {
                foreach (object attribute in propertyInfo.GetCustomAttributes(true))
                {
                    if (attribute is ShippingApiHeaderAttribute)
                    {
                        var headerAttribute = attribute as ShippingApiHeaderAttribute;
                        string v;
                        if (propertyInfo.GetValue(o) is StringBuilder)
                        {
                            v = ((StringBuilder)propertyInfo.GetValue(o)).ToString();
                            yield return new Tuple<ShippingApiHeaderAttribute, string, string>(headerAttribute, v, propertyInfo.Name);
                        }
                        else
                        {
                            if (propertyInfo.GetValue(o) == null)
                            {
                                v = null;
                            }
                            else
                            {
                                if (((ShippingApiHeaderAttribute)attribute).Format != null)
                                    v = string.Format(headerAttribute.Format, propertyInfo.GetValue(o));
                                else
                                {
                                    var val = propertyInfo.GetValue(o) as string;
                                    v = val ?? propertyInfo.GetValue(o).ToString();
                                }
                            }
                            yield return new Tuple<ShippingApiHeaderAttribute, string, string>(headerAttribute, v, propertyInfo.Name);
                        }
                    }
                }
            }

        }

        public virtual IEnumerable<Tuple<ShippingApiHeaderAttribute, string, string>> GetHeaders()
        {
            return GetHeaders(this);
        }

        public string RecordingFullPath(string resource, ISession session)
        {
            return RecordingFullPath( this, resource, session );
        }
    }
}
