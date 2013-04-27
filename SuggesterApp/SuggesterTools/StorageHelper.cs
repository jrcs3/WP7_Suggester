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
using System.IO;
using System.IO.IsolatedStorage;
using System.Xml.Serialization;

namespace SuggesterTools
{
    public class StorageHelper<T>
    {
        private string _fileName;

        public string FileName
        {
            get { return _fileName; }
        }
        public List<T> GetList()
        {
            List<T> jogs = new List<T>();
            TextReader reader = null;
            try
            {
                if (!string.IsNullOrWhiteSpace(_fileName))
                {
                    IsolatedStorageFile isoStorage = IsolatedStorageFile.GetUserStoreForApplication();
                    IsolatedStorageFileStream file = isoStorage.OpenFile(_fileName, FileMode.OpenOrCreate);
                    if (file.Length > 0)
                    {
                        reader = new StreamReader(file);

                        string content;
                        //using (StreamReader reader = new StreamReader(fs, Encoding.Unicode))
                        {
                            content = reader.ReadToEnd();
                        }

                        XmlSerializer xs = new XmlSerializer(typeof(List<T>));
                        jogs.AddRange((List<T>)xs.Deserialize(reader));
                        reader.Close();
                    }
                    file.Close();
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Dispose();
                }
            }
            return jogs;
        }
        public void SaveList(List<T> jogs)
        {
            TextWriter writer = null;
            try
            {
                if (!string.IsNullOrWhiteSpace(_fileName))
                {
                    IsolatedStorageFile isoStorage = IsolatedStorageFile.GetUserStoreForApplication();
                    //isoStorage.CreateFile(_fileName);
                    IsolatedStorageFileStream file = isoStorage.OpenFile(_fileName, FileMode.Create);
                    writer = new StreamWriter(file);

                    XmlSerializer xs = new XmlSerializer(typeof(List<T>));
                    xs.Serialize(writer, jogs);
                    writer.Close();
                    file.Close();
                }
            }
            finally
            {
                if (writer != null)
                    writer.Dispose();
            }
        }
        public StorageHelper(string fileName)
        {
            _fileName = fileName;
        }


    }
}
