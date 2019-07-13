using MZcms.CommonModel;
using MZcms.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MZcms.Web.Controllers
{
    public class PublicOperationController : Controller
    {

        /// <summary>
        /// 上传文件的扩展名集合
        /// </summary>
        string[] AllowFileExtensions = new string[] { ".rar", ".zip" };

        /// <summary>
        /// 上传图片文件扩展名集合
        /// </summary>
        string[] AllowImageExtensions = new string[] { ".png", ".jpg", ".jpeg", ".gif", ".bmp" };

        /// <summary>
        /// 检查图片文件扩展名
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        private bool CheckImageFileType(string filename)
        {
            var fileExtension = Path.GetExtension(filename).ToLower();
            return AllowImageExtensions.Select(x => x.ToLower()).Contains(fileExtension);
        }

        /// <summary>
        /// 检查上传文件的扩展名
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        private bool CheckFileType(string filename)
        {
            var fileExtension = Path.GetExtension(filename).ToLower();
            return AllowFileExtensions.Select(x => x.ToLower()).Contains(fileExtension);
        }

        [HttpPost]
        public ActionResult UploadPic()
        {

            string path = "";
            string filename = "";

            List<string> files = new List<string>();
            if (Request.Files.Count == 0) return Content("NoFile", "text/html");
            else
            {
                for (var i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];
                    if (null == file || file.ContentLength <= 0) return Content("格式不正确！", "text/html");

                    Random ra = new Random();
                    filename = DateTime.Now.ToString("yyyyMMddHHmmssffffff") + i
                        + Path.GetExtension(file.FileName);
                    if (!CheckImageFileType(filename))
                    {
                        return Content("上传的图片格式不正确", "text/html");
                    }

                    var fname = "/Temp/" + filename;
                    var ioname = Core.MZcmsIO.GetImagePath(fname);
                    files.Add(ioname);
                    try
                    {
                        System.Drawing.Bitmap bitImg = new System.Drawing.Bitmap(100, 100);
                        bitImg = (System.Drawing.Bitmap)System.Drawing.Image.FromStream(file.InputStream);

                        var orientation = 0;
                        if (!string.IsNullOrEmpty(Request.Form["hidFileMaxSize"]))
                        {
                            int.TryParse(Request.Form["hidFileMaxSize"], out orientation);
                        }
                        switch (orientation)
                        {
                            case 3: bitImg.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone); break;
                            case 6: bitImg.RotateFlip(System.Drawing.RotateFlipType.Rotate90FlipNone); break;
                            case 8: bitImg.RotateFlip(System.Drawing.RotateFlipType.Rotate270FlipNone); break;
                            default: break;
                        }
                        path = AppDomain.CurrentDomain.BaseDirectory + "/Temp/";
                        bitImg.Save(Path.Combine(path, filename));
                    }
                    catch (Exception ex)
                    {
                        Log.Error("上传文件错误", ex);
                    }
                }
            }
            return Content(string.Join(",", files), "text/html");
        }

        [HttpPost]
        public ActionResult UploadFile()
        {
            string strResult = "NoFile";
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];
                if (file.ContentLength == 0)
                {
                    strResult = "文件长度为0,格式异常。";
                }
                else
                {
                    Random ra = new Random();
                    string filename = DateTime.Now.ToString("yyyyMMddHHmmssfff") + ra.Next(1000, 9999)
                         //+ file.FileName.Substring(file.FileName.LastIndexOf("\\") + 1);
                         + Path.GetExtension(file.FileName);
                    if (!CheckFileType(filename))
                    {
                        return Content("上传的文件格式不正确", "text/html");
                    }
                    string DirUrl = Server.MapPath("~/Temp/");
                    if (!System.IO.Directory.Exists(DirUrl))      //检测文件夹是否存在，不存在则创建
                    {
                        System.IO.Directory.CreateDirectory(DirUrl);
                    }
                    string strfile = filename;
                    try
                    {
                        var opcount = Core.Cache.Get<int>(CacheKeyCollection.UserImportOpCount);
                        if (opcount == 0)
                        {
                            Core.Cache.Insert(CacheKeyCollection.UserImportOpCount, 1);
                        }
                        else
                        {
                            Core.Cache.Insert(CacheKeyCollection.UserImportOpCount, opcount + 1);
                        }
                        file.SaveAs(Path.Combine(DirUrl, filename));
                    }
                    catch (Exception e)
                    {
                        var opcount = Core.Cache.Get<int>(CacheKeyCollection.UserImportOpCount);
                        if (opcount != 0)
                        {
                            Core.Cache.Insert(CacheKeyCollection.UserImportOpCount, opcount - 1);
                        }
                        Core.Log.Error("商品导入上传文件异常：" + e.Message);
                        strfile = "Error";
                    }
                    strResult = strfile;
                }
            }
            return Content(strResult, "text/html");
        }

    }
}