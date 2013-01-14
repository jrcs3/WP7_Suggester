using System;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.Xml.Linq;
using System.Text;

namespace SuggesterTools
{
    public static class ListHelper
    {

        public static List<Suggestion> ReadResourceFile(string assemblyname, string fileName)
        {
            var rVal = new List<Suggestion>();
            int lineNumber = 1;
            foreach (string line in GetListFromAssemblyResources(assemblyname, fileName))
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    rVal.Add(new Suggestion(lineNumber++, line));
                }
            }
            return rVal;
        }

        public static List<string> GetListFromAssemblyResources(string assemblyname, string fileName)
        {
            Assembly a = Assembly.Load(assemblyname);
            var list = new List<string>();
            using (Stream stream = a.GetManifestResourceStream(getAssemblyFirstName(a) + "." + fileName))
            {
                if (stream != null)
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        while (reader.Peek() >= 0)
                        {
                            string line = reader.ReadLine();
                            if (!string.IsNullOrWhiteSpace(line))
                            {
                                list.Add(line);
                            }
                        }
                    }
                }
            }
            return list;
        }

        public static SuggesterAppConfig GetSuggesterAppConfig(string assemblyname)
        {
            Assembly a = Assembly.Load(assemblyname);
            SuggesterAppConfig list = new SuggesterAppConfig();
            using (Stream stream = a.GetManifestResourceStream(getAssemblyFirstName(a) + "." + "Config.xml"))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string contents = reader.ReadToEnd();
                    XDocument doc = XDocument.Parse(contents);
                    list.LoadFromXml(doc);
                }
            }
            return list;
        }

        static public string getAssemblyFirstName(Assembly a)
        {
            string[] nameParts = a.FullName.Split(new char[] { ',' });
            return nameParts[0];
        }


        public static string[] ConvertSuggestionListToStringArray(List<Suggestion> list)
        {
            return list.Select(x => x.Text).ToArray();
        }

        public static Stream ConvertSuggestionListToStreamy(List<Suggestion> list)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(string.Join(Environment.NewLine,  ConvertSuggestionListToStringArray(list)));
            MemoryStream stream = new MemoryStream(byteArray);
            return stream;
        }
    }
}
