using System;
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

namespace SuggesterTools
{
    public class ListHelper
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
            return list;
        }

        //static public string AssemblyDirectory
        //{
        //    get
        //    {
        //        string codeBase = Assembly.GetExecutingAssembly().CodeBase;
        //        UriBuilder uri = new UriBuilder(codeBase);
        //        string path = Uri.UnescapeDataString(uri.Path);
        //        return Path.GetDirectoryName(path);
        //    }
        //}

        static public string getAssemblyFirstName(Assembly a)
        {
            string[] nameParts = a.FullName.Split(new char[] { ',' });
            return nameParts[0];
        }

    }
}
