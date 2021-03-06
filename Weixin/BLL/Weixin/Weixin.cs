﻿using System;
using System.IO;
using System.Net;
using System.Text;
using Weixin.DAL;

namespace Weixin.BLL.Weixin
{
    public class Weixin
    {

        /// <summary>
        /// 显示天气信息
        /// </summary>
        /// <param name="city"></param>
        /// <param name="cityid"></param>
        /// <param name="cityip"></param>
        /// <returns></returns>
        public string ShowWeather(string city, string cityid = null, string cityip = null)
        {
            var result = "";
            try
            {
                var weatherResult = WeatherApi.Weather.GetWeatherResult(city);
                if (weatherResult != null && weatherResult.HeWeatherList != null &&
                    weatherResult.HeWeatherList.Count > 0 && weatherResult.HeWeatherList[0].status == "ok")
                {
                    var weather = weatherResult.HeWeatherList[0];
                    var sb = new StringBuilder();
                    sb.Append("地点:" + weather.basic.city + Environment.NewLine);
                    sb.Append("当前时间:" + weather.basic.update.loc + Environment.NewLine);
                    sb.Append("天气状况:" + weather.now.cond.txt + Environment.NewLine);
                    sb.Append("当前温度:" + weather.now.tmp + Environment.NewLine);
                    sb.Append("当前风向:" + weather.now.wind.dir + Environment.NewLine);
                    sb.Append("当前风力:" + weather.now.wind.sc + Environment.NewLine);
                    if (weather.aqi != null)
                    {

                        sb.Append("空气质量:" + weather.aqi.city.qlty + Environment.NewLine);
                        sb.Append("PM2.5值:" + weather.aqi.city.pm25 + Environment.NewLine);
                    }
                    if (weather.suggestion != null)
                    {

                        sb.Append("舒适度:" + weather.suggestion.comf.brf + Environment.NewLine);
                        sb.Append(weather.suggestion.comf.txt + Environment.NewLine);
                    }
                    result = sb.ToString();

                }
                else
                {
                    if (weatherResult != null)
                        if (weatherResult.HeWeatherList != null)
                            result = "Sorry,获取天气失败:" + weatherResult.HeWeatherList[0].status + Environment.NewLine + "请重试!";
                }
            }
            catch (Exception e)
            {

                result = e.Message;
            }
            return result;
        }


        ///// <summary>
        ///// 订阅或者显示公司信息
        ///// </summary>
        ///// <param name="info"></param>
        ///// <returns></returns>
        //private string ShowCompanyInfo(BaseMessage info)
        //{
        //    string result = "";
        //    //使用在微信平台上的图文信息(单图文信息)
        //    ResponseNews response = new ResponseNews(info);
        //    ArticleEntity entity = new ArticleEntity();
        //    entity.Title = "广州爱奇迪软件科技有限公司";
        //    entity.Description = "欢迎关注广州爱奇迪软件--专业的单位信息化软件和软件开发框架提供商，我们立志于为客户提供最好的软件及服务。\r\n";
        //    entity.Description +=
        //        "我们是一家极富创新性的软件科技公司，从事研究、开发并销售最可靠的、安全易用的技术产品及优质专业的服务，帮助全球客户和合作伙伴取得成功。\r\n......（此处省略1000字，哈哈）";
        //    entity.PicUrl = "http://www.iqidi.com/WeixinImage/company.png";
        //    entity.Url = "http://www.iqidi.com";

        //    response.Articles.Add(entity);
        //    result = response.ToXml();

        //    return result;
        //}

        ///// <summary>
        ///// 获取每次操作微信API的Token访问令牌
        ///// </summary>
        ///// <param name="appid">应用ID</param>
        ///// <param name="secret">开发者凭据</param>
        ///// <returns></returns>
        //public string GetAccessToken(string appid, string secret)
        //{
        //    //正常情况下access_token有效期为7200秒,这里使用缓存设置短于这个时间即可
        //    string access_token = MemoryCacheHelper.GetCacheItem<string>("access_token", delegate()
        //    {
        //        string grant_type = "client_credential";
        //        var url = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type={0}&appid={1}&secret={2}",
        //            grant_type, appid, secret);

        //        HttpHelper helper = new HttpHelper();
        //        string result = helper.GetHtml(url);
        //        string regex = "\"access_token\":\"(?<token>.*?)\"";
        //        string token = CRegex.GetText(result, regex, "token");
        //        return token;
        //    },
        //        new TimeSpan(0, 0, 7000) //7000秒过期
        //        );

        //    return access_token;
        //}


        /// <summary>
        ///更新微信菜单
        /// </summary>
        /// <returns></returns>
        //public ActionResult UpdateWeixinMenu()
        //{
        //    string token = base.GetAccessToken();
        //    MenuListJson menuJson = GetWeixinMenu();

        //    IMenuApi menuApi = new MenuApi();
        //    CommonResult result = menuApi.CreateMenu(token, menuJson);
        //    return ToJsonContent(result);
        //}

        /// <summary>
        /// 生成微信菜单的Json数据
        /// </summary>
        /// <returns></returns>
        //private MenuListJson GetWeixinMenu()
        //{
        //    MenuListJson menuJson = new MenuListJson();

        //    List<MenuNodeInfo> menuList = BLLFactory<Menu>.Instance.GetTree();
        //    foreach (MenuNodeInfo info in menuList)
        //    {
        //        ButtonType type = (info.Type == "click") ? ButtonType.click : ButtonType.view;
        //        string value = (type == ButtonType.click) ? info.Key : info.Url;

        //        MenuJson weiInfo = new MenuJson(info.Name, type, value);
        //        AddSubMenuButton(weiInfo, info.Children);

        //        menuJson.button.Add(weiInfo);
        //    }
        //    return menuJson;
        //}

        //private void AddSubMenuButton(MenuJson menu, List<MenuNodeInfo> menuList)
        //{
        //    if (menuList.Count > 0)
        //    {
        //        menu.sub_button = new List<MenuJson>();
        //    }
        //    foreach (MenuNodeInfo info in menuList)
        //    {
        //        ButtonType type = (info.Type == "click") ? ButtonType.click : ButtonType.view;
        //        string value = (type == ButtonType.click) ? info.Key : info.Url;

        //        MenuJson weiInfo = new MenuJson(info.Name, type, value);
        //        menu.sub_button.Add(weiInfo);

        //        AddSubMenuButton(weiInfo, info.Children);
        //    }
        //}


        ///// <summary>
        ///// 同步服务器的分组信息
        ///// </summary>
        ///// <returns></returns>
        ////public ActionResult SyncGroup()
        ////{
        ////    string accessToken = GetAccessToken();
        ////    CommonResult result = BLLFactory<Group>.Instance.SyncGroup(accessToken);
        ////    return ToJsonContent(result);
        ////}


        ///// <summary>
        ///// 同步服务器的分组信息
        ///// </summary>
        ///// <returns></returns>
        //public CommonResult SyncGroup(string accessToken)
        //{
        //    CommonResult result = new CommonResult();

        //    try
        //    {
        //        IUserApi api = new UserApi();

        //        using (DbTransaction trans = baseDal.CreateTransaction())
        //        {
        //            //先把本地标志groupId = -1未上传的记录上传到服务器,然后进行本地更新
        //            string condition = string.Format("GroupID = '-1' ");
        //            List<GroupInfo> unSubmitList = base.Find(condition);
        //            foreach (GroupInfo info in unSubmitList)
        //            {
        //                GroupJson groupJson = api.CreateGroup(accessToken, info.Name);
        //                if (groupJson != null)
        //                {
        //                    info.GroupID = groupJson.id;
        //                    baseDal.Update(info, info.ID, trans);
        //                }
        //            }

        //            //把标志为修改状态的记录，在服务器上修改
        //            condition = string.Format("GroupID >=0 and Modified =1 ");
        //            List<GroupInfo> unModifyList = base.Find(condition);
        //            foreach (GroupInfo info in unModifyList)
        //            {
        //                CommonResult modifyed = api.UpdateGroupName(accessToken, info.GroupID, info.Name);
        //                if (modifyed != null && modifyed.Success)
        //                {
        //                    info.Modified = 0; //重置标志
        //                    baseDal.Update(info, info.ID, trans);
        //                }
        //            }

        //            //删除具有删除标志的分组
        //            //condition = string.Format("GroupID >=100 and Deleted=1 ");
        //            //List<GroupInfo> unDeletedList = base.Find(condition);
        //            //foreach (GroupInfo info in unDeletedList)
        //            //{
        //            //    CommonResult deleted = api.DeleteGroup(accessToken, info.GroupID, info.Name);
        //            //    if (deleted != null && deleted.Success)
        //            //    {
        //            //        baseDal.Delete(info.ID, trans);
        //            //    }
        //            //}

        //            List<GroupJson> list = api.GetGroupList(accessToken);
        //            foreach (GroupJson info in list)
        //            {
        //                UpdateGroup(info, trans);
        //            }

        //            try
        //            {
        //                trans.Commit();
        //                result.Success = true;
        //            }
        //            catch
        //            {
        //                trans.Rollback();
        //                throw;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.ErrorMessage = ex.Message;
        //    }

        //    return result;
        //}

        ///// <summary>
        ///// 对语音请求信息进行处理
        ///// </summary>
        ///// <param name="info">语音请求信息实体</param>
        ///// <returns></returns>
        ////public string HandleVoice(Entity.RequestVoice info)
        ////{
        ////    string xml = "";
        ////    // 开通语音识别功能，用户每次发送语音给公众号时，
        ////    // 微信会在推送的语音消息XML数据包中，增加一个Recongnition字段。
        ////    if (!string.IsNullOrEmpty(info.Recognition))
        ////    {
        ////        TextDispatch dispatch = new TextDispatch();
        ////        xml = dispatch.HandleVoiceText(info, info.Recognition);
        ////    }
        ////    else
        ////    {
        ////        xml = "";
        ////    }

        ////    return xml;
        ////}

        ///// <summary>
        ///// 如果用户用语音读出菜单的内容，那么我们应该先根据菜单对应的事件触发，最后再交给普通事件处理
        ///// </summary>
        ///// <param name="info"></param>
        ///// <returns></returns>
        //public string HandleVoiceText(BaseMessage info, string voiceText)
        //{
        //    string xml = "";
        //    MenuInfo menuInfo = BLLFactory<Menu>.Instance.FindByName(voiceText);
        //    if (menuInfo != null)
        //    {
        //        #region 如果找到菜单对象的处理

        //        if (menuInfo.Type == "click")
        //        {
        //            //模拟单击事件
        //            RequestEventClick eventInfo = new RequestEventClick();
        //            eventInfo.CreateTime = info.CreateTime;
        //            eventInfo.EventKey = menuInfo.Key;
        //            eventInfo.FromUserName = info.FromUserName;
        //            eventInfo.ToUserName = info.ToUserName;

        //            xml = base.DealEvent(eventInfo, eventInfo.EventKey);
        //        }
        //        else
        //        {
        //            //由于无法自动切换到连接，
        //            //转换为连接文本供用户进入
        //            string content = string.Format("请单击链接进入<a href=\"{0}\">{1}</a> ", menuInfo.Url, menuInfo.Name);

        //            ResponseText textInfo = new ResponseText(info);
        //            textInfo.Content = content;

        //            xml = textInfo.ToXml();
        //        }

        //        #endregion
        //    }
        //    else
        //    {
        //        //交给事件机制处理
        //        if (string.IsNullOrEmpty(xml))
        //        {
        //            xml = HandleText(info, voiceText);
        //        }
        //    }

        //    //最后如果没有处理到，那么提示用户的语音内容
        //    if (string.IsNullOrEmpty(xml))
        //    {
        //        ResponseText textInfo = new ResponseText(info);
        //        textInfo.Content = string.Format("非常抱歉，您输入的语音内容没有找到对应的处理方式。您的语音内容为：{0}", voiceText);
        //        xml = textInfo.ToXml();
        //    }

        //    return xml;
        //}

        public string GetPage(string posturl, string postData)
        {
            Encoding encoding = Encoding.UTF8;
            byte[] data = encoding.GetBytes(postData);
            // 准备请求...
            try
            {
                // 设置参数
                var request = WebRequest.Create(posturl) as HttpWebRequest;
                CookieContainer cookieContainer = new CookieContainer();
                request.CookieContainer = cookieContainer;
                request.AllowAutoRedirect = true;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                var outstream = request.GetRequestStream();
                outstream.Write(data, 0, data.Length);
                outstream.Close();
                //发送请求并获取相应回应数据
                var response = request.GetResponse() as HttpWebResponse;
                //直到request.GetResponse()程序才开始向目标网页发送Post请求
                var instream = response.GetResponseStream();
                var sr = new StreamReader(instream, encoding);
                //返回结果网页（html）代码
                string content = sr.ReadToEnd();
                string err = string.Empty;
                //Response.Write(content);
                return content;
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                return string.Empty;
            }
        }

        //public string GetPage(string posturl)
        //{
        //    Stream instream = null;
        //    StreamReader sr = null;
        //    HttpWebResponse response = null;
        //    HttpWebRequest request = null;
        //    Encoding encoding = Encoding.UTF8;
        //    // 准备请求...
        //    try
        //    {
        //        // 设置参数
        //        request = WebRequest.Create(posturl) as HttpWebRequest;
        //        CookieContainer cookieContainer = new CookieContainer();
        //        request.CookieContainer = cookieContainer;
        //        request.AllowAutoRedirect = true;
        //        request.Method = "GET";
        //        request.ContentType = "application/x-www-form-urlencoded";
        //        //发送请求并获取相应回应数据
        //        response = request.GetResponse() as HttpWebResponse;
        //        //直到request.GetResponse()程序才开始向目标网页发送Post请求
        //        instream = response.GetResponseStream();
        //        sr = new StreamReader(instream, encoding);
        //        //返回结果网页（html）代码
        //        string content = sr.ReadToEnd();
        //        string err = string.Empty;
        //        Response.Write(content);
        //        return content;
        //    }
        //    catch (Exception ex)
        //    {
        //        string err = ex.Message;
        //        return string.Empty;
        //    }
        //}


        //        using System.IO;      日志功能



        //NorthwindDataContext ctx = new NorthwindDataContext("server=xxx;database=Northwind;uid=xxx;pwd=xxx");

        //StreamWriter sw = new StreamWriter(Server.MapPath("log.txt"), true); // Append

        //ctx.Log = sw;

        //GridView1.DataSource = from c in ctx.Customers where c.CustomerID.StartsWith("A") select new { 顾客ID = c.CustomerID, 顾客名 = c.Name, 城市 = c.City };

        //GridView1.DataBind();

        //sw.Close();
    }
}