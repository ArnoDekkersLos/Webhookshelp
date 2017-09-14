using ClientApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.Server
{
    interface IServerConnector
    {
        Employee GetEmployeeWithTeammembers(int id);
        bool SignIn(string userName, string password);
        List<FileModel> FetchFiles();
        void DownloadFile(FileModel selectedFile);
        void UpdateFile(Stream selectedFile, FileModel toUpdate);
        void UploadFile(Stream selectedFile, FileModel toUpload);
        void UpdateEmployee(Employee employeeToUpdate);
        List<ProjectModel> ExecuteComplex1();
        List<EmployeeProjectHourModel> ExecuteComplex2();
    }
}
