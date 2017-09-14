using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace CustomRestServer.Models
{
    [DataContract]
    public class FileModel
    {
        [DataMember]
        public string FolderPath { get; set; }
        [DataMember]
        public string FileName { get; set; }
        [DataMember]
        public int Version { get; set; }
        [DataMember]
        public string FullPath { get; set; }

        public FileModel() { }

        public FileModel(string path)
        {
            FullPath = path;
            if (path.Contains(@"\"))
            {
                int idx = path.LastIndexOf(@"\");
                FolderPath = path.Substring(0, idx + 1);
                FileName = path.Substring(idx + 1);
            }
            else
            {
                FileName = path;
            }
            int version = 1;
            if (int.TryParse(FileName.Substring(0, 3), out version) && "-".Equals(FileName.Substring(3, 1)))
            {
                FileName = FileName.Substring(4);
            }
            Version = version;
        }

        public string GetTrueFileName()
        {
            string version = Version.ToString();
            while (version.Length < 3)
            {
                version = "0" + version;
            }
            return version + "-" + FileName;
        }

        public override string ToString()
        {
            return "File: " + FileName + ", version: " + Version.ToString() + " from folder: " + FolderPath;
        }
    }
}