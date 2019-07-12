using FileApi.Model;
using FileApi.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace FileApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FileController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly FileTypeOptions _fileTypeOptions;
        public FileController(IHostingEnvironment hostingEnvironment, IOptions<FileTypeOptions> fileTypeOptions)
        {
            _hostingEnvironment = hostingEnvironment;
            _fileTypeOptions = fileTypeOptions.Value;

        }

        [HttpPost]
        public async Task<AjaxResponse> UploadFile(IFormCollection form )
        {
            try
            {
                var schoolId = form["schoolId"].ToString()==string.Empty?"0000" : form["schoolId"].ToString();
                var year = DateTime.Now.ToString("yyyy");
                var month = DateTime.Now.ToString("MM");
                var day = DateTime.Now.ToString("dd");
                string fileTypePath = "/Other";
                var basicPath = $"/{year}/{month}/{day}/";
                List<string> lstShowPaths = new List<string>();
                foreach (var file in form.Files)
                {
                    if (file.Length == 0)
                    {
                        continue;
                    }
                    var guid = Guid.NewGuid().ToString();
                    var fileExtension = Path.GetExtension(file.FileName).ToLower();
                    if (_fileTypeOptions.Photo.Contains(fileExtension))
                    {
                        fileTypePath = "/Photo";
                    }
                    else if (_fileTypeOptions.Video.Contains(fileExtension))
                    {
                        fileTypePath = "/Video";
                    }
                    else if (_fileTypeOptions.Word.Contains(fileExtension))
                    {
                        fileTypePath = "/Word";
                    }
                    var showPath = schoolId + fileTypePath + basicPath + Path.GetFileNameWithoutExtension(file.FileName)+"$!$" + guid + fileExtension;
                    var mkdirPath = _hostingEnvironment.ContentRootPath + "/Great/" + schoolId + fileTypePath + basicPath;
                    var absoluteSavepath = _hostingEnvironment.ContentRootPath + "/Great/" + showPath;
                    lstShowPaths.Add(showPath);
                    if (!Directory.Exists(mkdirPath))
                    {
                        Directory.CreateDirectory(mkdirPath);
                    }
                    using (var stream = new FileStream(absoluteSavepath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
                return new AjaxResponse { Code = 1, Message = lstShowPaths };
            }
            catch (Exception e)
            {
                LogUtility.Exception(e);
                return new AjaxResponse { Code = 0, Message = "上传失败" };
            }
        }

    }
}