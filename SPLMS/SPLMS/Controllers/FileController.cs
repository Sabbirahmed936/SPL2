using AuthenticationAPI.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using AuthenticationAPI.Entities;
using AuthenticationAPI.Infrastructure;

namespace AuthenticationAPI.Controllers
{
    [RoutePrefix("api/file")]
    public class FileController : ApiController
    {
        ApplicationDbContext db = new ApplicationDbContext();
        [Route("uploadfile")]
        [HttpPost]
        public async Task<HttpResponseMessage> UploadFile(HttpRequestMessage request)
        {
            if (!request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var data = await Request.Content.ParseMultipartAsync();
            
            if (data.Files.ContainsKey("document"))
            {
                long milliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                byte[] file = data.Files["document"].File;
                var fileName = data.Files["document"].Filename;
                string path = HttpContext.Current.Server.MapPath("~/UploadedFiles/" + milliseconds + "-" + fileName);
                File.WriteAllBytes(path, file);

            }

            if (data.Fields.ContainsKey("description"))
            {
                var description = data.Fields["description"].Value;
            }

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("FileUploaded Successfully")
            };
        }



        [Route("files")]
        public IHttpActionResult GetFiles()
        {
            string path = HttpContext.Current.Server.MapPath("~/UploadedFiles");
            string[] fileArray = Directory.GetFiles(path);
            string[][] newKeys = fileArray.Select(x => new string[] { x }).ToArray();

            string json = JsonConvert.SerializeObject(newKeys);

            return Ok(json);
        }



        [Route("directories")]
        public IHttpActionResult GetDirectories()
        {
            string path = HttpContext.Current.Server.MapPath("~/UploadedFiles");
            string[] directoryArray = Directory.GetDirectories(path);
            string[][] newKeys = directoryArray.Select(x => new string[] { x }).ToArray();
            string json = JsonConvert.SerializeObject(newKeys);
            return Ok(json);
        }


        [Route("directory/{folderName}")]
        public IHttpActionResult GetDirectorySize(string folderName)
        {
            string path= HttpContext.Current.Server.MapPath("~/UploadedFiles/"+folderName);
            DirectoryInfo di = new DirectoryInfo(path);
            long directorySize=di.EnumerateFiles("*", SearchOption.AllDirectories).Sum(fi => fi.Length);
            return Ok(directorySize);
        }


    }
}
