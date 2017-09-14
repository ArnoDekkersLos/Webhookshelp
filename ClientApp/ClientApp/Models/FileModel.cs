using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.Models
{
    public class FileModel
    {
        public string FolderPath { get; set; }
        public string FileName { get; set; }
        public int Version { get; set; }
        public string FullPath { get; set; }

        public override string ToString()
        {
            return FileName;
        }
    }
}
