﻿using System;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http.Headers;

namespace PitneyBowes.Developer.ShippingApi
{
    public static class DocumentsMethods
    {
        public static async Task WriteToStream(IDocument document, Stream stream, Func<Stream,int, Stream> nextPageAction = null, ISession session = null )
        {
            if (document.ContentType == ContentType.BASE64)
            { 
                int pageCount = 0;
                foreach (var page in document.Pages)
                {
                    try
                    {
                        pageCount++;
                        if (nextPageAction != null)
                        {
                            stream = nextPageAction(stream, pageCount);
                        }
                        await WriteBase64Page(page, stream, session);
                    }
                    catch
                    {
                        if (stream!= null ) stream.Dispose();
                        throw;
                    }
                }
            }
            else
            {
                await WriteURL(document.Contents, stream, document.FileFormat, session);
            }
        }

        public static async Task WriteBase64Page(string page, Stream stream, ISession session )
        {
            var buffer = Convert.FromBase64String(page);
            await stream.WriteAsync(buffer, 0, buffer.Length);
        }
        public static async Task WriteURL(string page, Stream stream, FileFormat format, ISession session)
        {
            var uri = new Uri(page);
            var client = Globals.Client(uri.GetComponents(UriComponents.SchemeAndServer,UriFormat.Unescaped));
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Clear();
            switch(format)
            {
                case FileFormat.PDF:
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/pdf"));
                    break;
                case FileFormat.PNG:
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("image/png"));
                    break;
                case FileFormat.ZPL2:
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
                    break;
                default:
                    break;
            }
            client.DefaultRequestHeaders.Add("user-agent", "Ps API Client Proxy");
            var httpResponseMessage = await client.GetAsync(page);
            await httpResponseMessage.Content.CopyToAsync(stream);
        }

    }
}
