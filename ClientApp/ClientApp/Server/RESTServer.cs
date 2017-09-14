using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientApp.Models;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using static System.Net.WebRequestMethods;
using System.Collections.Specialized;
using Newtonsoft.Json;
using System.IO;

namespace ClientApp.Server
{
    class RESTServer : IServerConnector
    {
        private HttpTool tool;

        public RESTServer()
        {
            tool = new HttpTool("http://localhost:55997", "http://localhost:56001");
        }

        private List<Employee> GetEmployeesTeamMembers(JArray ids)
        {
            List<Employee> teamMembers = new List<Employee>();
            foreach (int i in ids)
            {
                JObject response = tool.GetResponseJObject("Employees/" + i.ToString());
                if (response != null)
                    teamMembers.Add(JsonToEmployee(response));
            }
            return teamMembers;
        }

        private Employee JsonToEmployee(JObject response)
        {
            Employee employee = null;
            if (response != null)
            {
                int iD = (int)response["Id"];
                string firstName = (string)response["FirstName"];
                string tussenvoegsel = (string)response["Tussenvoegsel"];
                string lastName = (string)response["LastName"];
                employee = new Employee(iD, firstName, tussenvoegsel, lastName);
            }
            return employee;
        }

        public Employee GetEmployeeWithTeammembers(int id)
        {
            Employee foundEmployee = null;
            JObject response = tool.GetResponseJObject("Employees/" + id.ToString());
            if (response != null)
            {
                foundEmployee = JsonToEmployee(response);
                JArray teamMemberIds = (JArray)response["TeamMembers"];
                foundEmployee.TeamMembers = GetEmployeesTeamMembers(teamMemberIds);
                tool.SubscribeTo("Employee:" + foundEmployee.Id.ToString());
            }
            return foundEmployee;
        }

        public void UpdateEmployee(Employee employeeToUpdate)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(employeeToUpdate), Encoding.UTF8, "application/Json");
            tool.PostJsonObject("Employees/", content);
        }

        public bool SignIn(string userName, string password)
        {
            return tool.SignIn(userName, password);
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
