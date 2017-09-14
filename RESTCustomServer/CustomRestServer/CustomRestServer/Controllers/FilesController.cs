using Newtonsoft.Json.Linq;
using CustomRestServer.Database;
using CustomRestServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace CustomRestServer.Controllers
{
    [Authorize]
    public class FilesController : ApiController
    {
        private static readonly log4net.ILog log
       = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // GET: api/Files
        public IEnumerable<FileModel> Get()
        {
            List<FileModel> foundfilesLatestVersions = new List<FileModel>();
            Dictionary<string, FileModel> foundfiles = new Dictionary<string, FileModel>();
            //get's the directory name for the server's files folder.
            string path = AppDomain.CurrentDomain.BaseDirectory + "files";
            foreach (string s in Directory.GetFiles(path, "*.*", SearchOption.AllDirectories))
            {
                string filePath = s.Replace(path + "\\", string.Empty);
                FileModel newfile = new FileModel(filePath);
                if (foundfiles.ContainsKey(newfile.FolderPath + newfile.FileName))
                {
                    //Is the file's version more recent then the old one remove old one from list and add the new file
                    if (foundfiles[newfile.FolderPath + newfile.FileName].Version < newfile.Version)
                    {
                        foundfiles[newfile.FolderPath + newfile.FileName] = newfile;
                    }
                }
                else
                {
                    foundfiles[newfile.FolderPath + newfile.FileName] = newfile;
                }
            }
            return foundfiles.Values;
        }

        public HttpResponseMessage Post(FileModel p)
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + "files\\" + p.FullPath;
                if (File.Exists(path))
                {
                    FileStream selectedFile = File.Open(path, FileMode.Open, FileAccess.ReadWrite);
                    MemoryStream memoryStream = new MemoryStream();
                    selectedFile.CopyTo(memoryStream);
                    memoryStream.Position = 0;

                    HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);

                    result.Content = new StreamContent(memoryStream);
                    selectedFile.Close();
                    //memoryStream.Close();
                    result.Content.Headers.Add("Content-Type", "application/octet-stream");
                    result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                    {
                        FileName = p.GetTrueFileName()
                    };
                    return result;
                }
                else
                {
                    log.Warn("The user: " + User.Identity.Name + " tried to lookup a non existent file");
                    throw new Exception("file not found");
                }

            }
            catch (Exception ex)
            {
                log.Error("upon request by user: " + User.Identity.Name + " with file model: " + Newtonsoft.Json.JsonConvert.SerializeObject(p) + " an exception was thrown", ex);
                return null;
            }
        }

        // PUT: api/Files/5
        public IHttpActionResult Put()
        {
            FileModel file = null;
            try
            {
                var provider = Request.Content.ReadAsMultipartAsync().Result;

                List<HttpContent> content = provider.Contents.ToList();

                StreamContent streamContent = (StreamContent)content[0];
                Stream received = streamContent.ReadAsStreamAsync().Result;
                StreamContent stringContent = (StreamContent)content[1];
                JObject jFileIno = JObject.Parse(stringContent.ReadAsStringAsync().Result);
                file = jFileIno.ToObject<FileModel>();
                string serverpathprefix = AppDomain.CurrentDomain.BaseDirectory + @"files\";
                file.Version++;
                while (File.Exists(serverpathprefix + file.FolderPath + file.GetTrueFileName()))
                {
                    file.Version++;
                }

                if (!Directory.Exists(serverpathprefix + file.FolderPath))
                {
                    Directory.CreateDirectory(serverpathprefix + file.FolderPath);
                }

                using (Stream fileStream = File.Create(serverpathprefix + file.FolderPath + file.GetTrueFileName()))
                {
                    received.CopyTo(fileStream);
                }
                log.Info("User: " + User.Identity.Name + " Created a new file on the server on template " + Newtonsoft.Json.JsonConvert.SerializeObject(file));
                return Ok();
            }
            catch (Exception ex)
            {
                log.Error("User: " + User.Identity.Name + " failed to create a file on the server on template " + Newtonsoft.Json.JsonConvert.SerializeObject(file), ex);
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }

        }
    }
}