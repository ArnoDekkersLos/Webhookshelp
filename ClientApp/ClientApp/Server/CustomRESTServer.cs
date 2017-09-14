using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientApp.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http;

namespace ClientApp.Server
{
    class CustomRESTServer : IServerConnector
    {
        private HttpTool tool;

        public CustomRESTServer()
        {
            tool = new HttpTool("http://localhost:51907", "http://localhost:56011");
        }

        public bool SignIn(string userName, string password)
        {
            return tool.SignIn(userName, password);
        }      

        public Employee GetEmployeeWithTeammembers(int id)
        {
            JObject response = tool.GetResponseJObject("Employees/GetTeamLeaderAndTeam/" + id);
            Employee foundMatch;
            foundMatch = JsonConvert.DeserializeObject<Employee>(response.ToString());
            if(foundMatch != null)
            {
                tool.SubscribeTo("Employee:" + foundMatch.Id.ToString());
            }
            return foundMatch;
        }     

        public void UpdateEmployee(Employee employeeToUpdate)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(employeeToUpdate), Encoding.UTF8, "application/Json");
            tool.PostJsonObject("Employees/", content);
        }

        public List<FileModel> FetchFiles()
        {
            List<FileModel> foundfiles = new List<FileModel>();
            JArray response = tool.GetResponseJArray("files");
            if (response != null)
            {
                foreach (JObject x in response)
                {
                    FileModel currentFile = new FileModel();
                    currentFile = x.ToObject<FileModel>();
                    foundfiles.Add(currentFile);
                }
            }
            return foundfiles;
        }

        private async void SaveFile(HttpResponseMessage result)
        {
            try
            {
                Stream stream = null;

                stream = await result.Content.ReadAsStreamAsync()
                                                        .ConfigureAwait(false);
                bool readableStream = stream.CanRead;
                string header = result.Content.Headers.ContentDisposition.FileName;
                string path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Downloads\" + header;
                int coppy = 1;
                int indexBeforeFileExtension;
                while (System.IO.File.Exists(path))
                {
                    indexBeforeFileExtension = header.LastIndexOf(".");
                    StringBuilder aStringBuilder = new StringBuilder(header);
                    aStringBuilder.Insert(indexBeforeFileExtension, "-" + coppy.ToString());
                    path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Downloads\" + aStringBuilder.ToString();
                    coppy++;
                }
                using (Stream fileStream = System.IO.File.Create(path))
                {
                    stream.CopyTo(fileStream);
                }
            }
            catch (Exception ex)
            {
                string exception = ex.Message;
            }
        }

        public void DownloadFile(FileModel selectedFile)
        {
            HttpResponseMessage response = tool.RetrieveFile(selectedFile);
            SaveFile(response);
        }

        public void UpdateFile(Stream selectedFile, FileModel toUpdate)
        {
            tool.PutFileAsync(selectedFile, toUpdate);
        }

        public void UploadFile(Stream selectedFile, FileModel toUpload)
        {
            tool.PutFileAsync(selectedFile, toUpload);
        }

        public List<ProjectModel> ExecuteComplex1()
        {
            throw new NotSupportedException("don't go there");
        }

        public List<EmployeeProjectHourModel> ExecuteComplex2()
        {
            throw new NotSupportedException("don't go there");
        }
    }
}
