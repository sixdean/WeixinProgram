﻿using System;
using Weixin.Model.Common;
using Weixin.Model.Enum;


namespace Weixin.Model.Request
{
    /// <summary>
    /// 接收语音消息
    /// </summary>
    [System.Xml.Serialization.XmlRoot(ElementName = "xml")]
    public class RequestVoice : BaseMessage
    {
        public RequestVoice()
        {
            this.MsgType = RequestMsgType.voice.ToString().ToLower();
        }

        public RequestVoice(BaseMessage info)
            : this()
        {
            this.ToUserName = info.ToUserName;
            this.FromUserName = info.FromUserName;
        }
        /// <summary>
        /// 语音格式，如amr，speex等
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// 语音消息媒体id，可以调用多媒体文件下载接口拉取数据。
        /// </summary>
        public string MediaId { get; set; }

        /// <summary>
        /// 消息ID
        /// </summary>
        public Int64 MsgId { get; set; }

        /// <summary>
        /// 语音识别结果，UTF8编码
        /// </summary>
        public string Recognition { get; set; }

    }
}