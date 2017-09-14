using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientApp.Models;

namespace ClientApp.Server
{
    class SocketServer : IServerConnector
    {
        private SocketTool tool;

        public SocketServer()
        {
            tool = new SocketTool();
        }

        public bool SignIn(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public Employee GetEmployeeWithTeammembers(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateEmployee(Employee employeeToUpdate)
        {
            throw new NotImplementedException();
        }

        public List<FileModel> FetchFiles()
        {
            throw new NotImplementedException();
        }

        public void DownloadFile(FileModel selectedFile)
        {
            throw new NotImplementedException();
        }

        public void UpdateFile(Stream selectedFile, FileModel toUpdate)
        {
            throw new NotImplementedException();
        }

        public void UploadFile(Stream selectedFile, FileModel toUpload)
        {
            throw new NotImplementedException();
        }

        public List<ProjectModel> ExecuteComplex1()
        {
            throw new NotImplementedException();
        }

        public List<EmployeeProjectHourModel> ExecuteComplex2()
        {
            throw new NotImplementedException();
        }
    }
}
