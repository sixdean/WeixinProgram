﻿using System;
using Weixin.Model.Common;
using Weixin.Model.Enum;


namespace Weixin.Model.Request
{

    /// <summary>
    /// 接收小视频消息
    /// </summary>
    [System.Xml.Serialization.XmlRoot(ElementName = "xml")]
    public class RequestShortVideo : BaseMessage
    {
        public RequestShortVideo()
        {
            this.MsgType = RequestMsgType.shortvideo.ToString().ToLower();
        }
        public RequestShortVideo(BaseMessage info)
            : this()
        {
            this.ToUserName = info.ToUserName;
            this.FromUserName = info.FromUserName;
        }

        /// <summary>
        /// 视频消息媒体id，可以调用多媒体文件下载接口拉取数据。
        /// </summary>
        public string MediaId { get; set; }

        /// <summary>
        /// 视频消息缩略图的媒体id，可以调用多媒体文件下载接口拉取数据。
        /// </summary>
        public string ThumbMediaId { get; set; }

        /// <summary>
        /// 消息id，64位整型 
        /// </summary>
        public Int64 MsgId { get; set; }
    }
}