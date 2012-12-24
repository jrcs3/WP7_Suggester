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

namespace SuggesterTools
{
    public class SkyDriveContent
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Id { get; set; }
        public string ParentId { get; set; }
        public string Type { get; set; }
        public string Ext { get; set; }
        public int Size { get; set; }
        public int? Count { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTime { get; set; }

        public int SortPriority
        {
            get
            {
                if (IsFolder)
                {
                    return 0;
                }
                if (IsTextFile)
                {
                    return 1;
                }
                return int.MaxValue;
            }
        }

        public bool IsFolder
        {
            get { return Type == "folder"; }
        }

        public bool IsTextFile
        {
            get { return Type == "file" && Ext.ToLower() == ".txt"; }
        }

        public string StatusLine
        {
            get
            {
                if (IsFolder)
                {
                    return string.Format("{0} files  {1:d} {1:t}", Count, UpdatedTime);
                }
                return string.Format("{0} KB  {1:d} {1:t}", Math.Ceiling(Size / 1024.0), UpdatedTime);
            }
        }

        public Uri ImageUri
        {
            get
            {
                if (IsFolder)
                {
                    return (new Uri("Images/folder.png", UriKind.Relative));
                }
                if (IsTextFile)
                {
                    return (new Uri("Images/text.png", UriKind.Relative));
                }
                return (new Uri("Images/unknown.png", UriKind.Relative));
            }
        }
        public override string ToString()
        {
            return string.Format("{0} - {1}", Name, Id);
        }
    }
}
