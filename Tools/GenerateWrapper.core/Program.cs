﻿using System;
using System.Reflection;
using System.Text;
using System.IO;
using PitneyBowes.Developer.ShippingApi;

namespace GenerateWrapper.core
{
    class Program
    {
        static void Main(string[] args)
        {
#pragma warning disable CS0168 // Simplify object initialization
            IAddress address; // get shipping API assembly loaded
#pragma warning restore CS0168
            var cwd = Directory.GetCurrentDirectory();
            string path = @"..\..\src\";
            Assembly assembly = null;
            foreach( var a in Assembly.GetEntryAssembly().GetReferencedAssemblies())
            {
                if (a.Name.Equals("shippingapi"))
                {
                    assembly = Assembly.Load(new AssemblyName(a.FullName));
                    break;
                }
            }

            foreach( var filePath in Directory.EnumerateFiles(path + "Contract") )
            {
                var file = Path.GetFileName(filePath);
                if (file.StartsWith("I"))
                {
                    var baseName = file.Substring(1, file.Length - 4);
                    var ifTypeName = "PitneyBowes.Developer.ShippingApi.I" + baseName;
                    var ifType = assembly.GetType(ifTypeName);

                    if (ifType != null)
                    {
                        var modelPath = path + @"model\" + baseName + ".cs";
                        var c = ifType.GetCustomAttribute<CodeGenerationAttribute>();
                        if ((c == null || c.GenerateModel) && !File.Exists(modelPath))
                        {
                            using (var modelStream = new FileStream(modelPath, FileMode.OpenOrCreate))
                            using (var modelWriter = new StreamWriter(modelStream))
                            {

                                GenerateModel(ifType, s => modelWriter.WriteLine(s));
                            }
                        }

                        var jsonPath = path + @"method\json\Json" + baseName + ".cs";

                        if ((c == null || c.GenerateJsonWrapper) && !File.Exists(jsonPath))
                        {
                            using (var jsonStream = new FileStream(jsonPath, FileMode.OpenOrCreate))
                            using (var jsonWriter = new StreamWriter(jsonStream))
                            {
                                GenerateWrapper(ifType, s => jsonWriter.WriteLine(s));
                            }
                        }
                    }
                }
            }
        }

        private static void Usings( Action<string> writer)
        {
            writer("using System;");
            writer("using Newtonsoft.Json;");
            writer("using Newtonsoft.Json.Converters;");
            writer("using System.Collections.Generic;");
            writer("");
        }

        private static string TypeName( Type t)
        {
            var tName = new StringBuilder();
            bool nullable = false;
            if ((t.GetTypeInfo().IsValueType && (Nullable.GetUnderlyingType(t) != null)))
            {
                nullable = true;
                tName.Append(TypeName(Nullable.GetUnderlyingType(t)));
            }
            else if (t.IsGenericType)
            {
                tName.Append(t.Name.Substring(t.Name.IndexOf('`'))); // drop the `1 after the type name
                tName.Append('<');
                foreach (var gt in t.GenericTypeArguments) tName.Append(TypeName(gt));
                tName.Append('>');
                return tName.ToString();
            }
            else
            {
                switch (t.Name.ToString())
                {
                    case "String":
                        tName.Append("string");
                        break;
                    case "Int32":
                        tName.Append("int");
                        break;
                    case "Decimal":
                        tName.Append("decimal");
                        break;
                    default:
                        tName.Append(t.Name);
                        break;
                }
            }
            if (nullable) tName.Append("?");
            return tName.ToString();
        }

        static void GenerateModel(Type intf, Action<string> writer)
        {
            var jName = intf.Name.Substring(1);

            Usings(writer);
            writer("namespace PitneyBowes.Developer.ShippingApi.Model");
            writer("{");
            writer("    public class " + jName + " : " + intf.Name );
            writer("    {");
            writer("");

            foreach (var prop in intf.GetProperties())
            {
                var cName = prop.Name;
                writer("        public " + TypeName(prop.PropertyType) + " " + prop.Name + "{get; set;}");
            }
            writer("    }");
            writer("}");
        }


        static void GenerateWrapper(Type intf, Action<string> writer)
        {
            var jName = "Json" + intf.Name.Substring(1);

            Usings(writer);

            writer("namespace PitneyBowes.Developer.ShippingApi.Json");
            writer("{");
            writer("    [JsonObject(MemberSerialization.OptIn)]");
            writer("    public class " + jName + "<T> : JsonWrapper<T>, " + intf.Name + " where T : " + intf.Name + ", new()");
            writer("    {");
            writer("        public " + jName + "() : base() { }");
            writer("        public " + jName + "(T t) : base(t) { }");
            writer("");

            foreach (var prop in intf.GetProperties())
            {
                var cName = prop.Name.Substring(0, 1).ToLower() + prop.Name.Substring(1);

                writer("        [JsonProperty(\"" + cName + "\")]");
                if (prop.PropertyType.GetTypeInfo().IsEnum) writer("        [JsonConverter(typeof(StringEnumConverter))]");
                writer("        public " + TypeName(prop.PropertyType) + " " + prop.Name);
                writer("        {");
                writer("            get => Wrapped." + prop.Name + ";");
                writer("            set { Wrapped." + prop.Name + " = value; }");
                writer("        }");
            }
            writer("    }");
            writer("}");
        }
    }
}