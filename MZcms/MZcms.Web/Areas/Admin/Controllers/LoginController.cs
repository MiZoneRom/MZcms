using MZcms.CommonModel;
using MZcms.Core.Helper;
using MZcms.IServices;
using MZcms.Web.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MZcms.Web.Areas.Admin.Controllers
{
    public class LoginController : BaseController
    {
        private IManagerService _iManagerService;

        public LoginController(IManagerService iManagerService)
        {
            _iManagerService = iManagerService;
        }

        /// <summary>
        /// 同一用户名无需验证的的尝试登录次数
        /// </summary>
        const int TIMES_WITHOUT_CHECKCODE = 3;

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Login(string username, string password, string checkCode)
        {
            string host = System.Web.HttpContext.Current.Request.Url.Host;
            try
            {
                //检查输入合法性
                CheckInput(username, password);

                //检查验证码
                CheckCheckCode(username, checkCode);

                var manager = _iManagerService.Login(username, password);
                if (manager == null)
                {
                    throw new MZcms.Core.MZcmsException("用户名和密码不匹配");
                }
                ClearErrorTimes(username);//清除输入错误记录次数

                base.SetAdminLoginCookie(manager.Id);
                System.Web.HttpResponse.RemoveOutputCacheItem("/m-wap");//更新授权要移除掉移动端首页缓存
                System.Web.HttpResponse.RemoveOutputCacheItem("/m-wap/");//更新授权要移除掉移动端首页缓存

                return Json(new Result { success = true });
            }
            catch (MZcms.Core.MZcmsException ex)
            {
                int errorTimes = SetErrorTimes(username);
                return Json(new { success = false, msg = ex.Message, errorTimes = errorTimes, minTimesWithoutCheckCode = TIMES_WITHOUT_CHECKCODE });
            }
            catch (Exception ex)
            {
                int errorTimes = SetErrorTimes(username);
                Exception innerEx = GerInnerException(ex);
                string showerrmsg = "未知错误";
                if (innerEx is MZcms.Core.MZcmsException)
                {
                    showerrmsg = innerEx.Message;
                }
                else
                {
                    Core.Log.Error("用户" + username + "登录时发生异常", ex);
                }
                return Json(new { success = false, msg = showerrmsg, errorTimes = errorTimes, minTimesWithoutCheckCode = TIMES_WITHOUT_CHECKCODE });
            }

        }

        public ActionResult Logout()
        {
            WebHelper.DeleteCookie(CookieKeysCollection.PLATFORM_MANAGER);

            return RedirectToAction("index");
        }

        public ActionResult GetCheckCode()
        {
            string code;
            var image = Core.Helper.ImageHelper.GenerateCheckCode(out code);
            Session["checkCode"] = code;
            return File(image.ToArray(), "image/png");
        }

        #region 方法

        private void CheckInput(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new MZcms.Core.MZcmsException("请填写用户名");

            if (string.IsNullOrWhiteSpace(password))
                throw new MZcms.Core.MZcmsException("请填写密码");

        }

        void CheckCheckCode(string username, string checkCode)
        {
            var errorTimes = GetErrorTimes(username);
            if (errorTimes >= TIMES_WITHOUT_CHECKCODE)
            {
                if (string.IsNullOrWhiteSpace(checkCode))
                    throw new MZcms.Core.MZcmsException("30分钟内登录错误3次以上需要提供验证码");

                string systemCheckCode = Session["checkCode"] as string;
                if (systemCheckCode.ToLower() != checkCode.ToLower())
                    throw new MZcms.Core.MZcmsException("验证码错误");

                //生成随机验证码，强制使验证码过期（一次提交必须更改验证码）
                Session["checkCode"] = Guid.NewGuid().ToString();
            }
        }

        /// <summary>
        /// 获取指定用户名在30分钟内的错误登录次数
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        int GetErrorTimes(string username)
        {
            var timesObject = Core.Cache.Get<int>(CacheKeyCollection.ManagerLoginError(username));
            //var times = timesObject == null ? 0 : int.Parse(timesObject.ToString());
            return timesObject;
        }

        void ClearErrorTimes(string username)
        {
            Core.Cache.Remove(CacheKeyCollection.ManagerLoginError(username));
        }

        /// <summary>
        /// 设置错误登录次数
        /// </summary>
        /// <param name="username"></param>
        /// <returns>返回最新的错误登录次数</returns>
        int SetErrorTimes(string username)
        {
            var times = GetErrorTimes(username) + 1;
            Core.Cache.Insert(CacheKeyCollection.ManagerLoginError(username), times, DateTime.Now.AddMinutes(30.0));//写入缓存
            return times;
        }

        #endregion

    }
}