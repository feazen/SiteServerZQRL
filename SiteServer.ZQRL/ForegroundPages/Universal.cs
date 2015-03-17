using System;
using System.Web;
using System.IO;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections;
using System.Text.RegularExpressions;
using System.Data.SqlTypes;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;



namespace SiteServer.ZQRL
{
	/// <summary>
	/// Universal 的摘要说明。
	/// </summary>
	public class Universal {
        private static object lockHelper = new object();
		public Universal() {
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		/// <summary>
		/// 得到SQL数据库最小时间
		/// </summary>
		public static DateTime MinSqlDateTime {
			get {
				return DateTime.Parse(System.Data.SqlTypes.SqlDateTime.MinValue.ToString() );
			}
		}

		/// <summary>
		/// HTML解码
		/// </summary>
		/// <param name="enHtml"></param>
		/// <returns></returns>
		public static string HtmlDecode(string enHtml) {
			return HttpContext.Current.Server.HtmlDecode(enHtml);
		} 

		/// <summary>
		/// HTML编码
		/// </summary>
		/// <param name="html"></param>
		/// <returns></returns>
		public static string HtmlEncode(string html) {       
			return HttpContext.Current.Server.HtmlEncode(html);
		}

		/// <summary>
		/// 弹出提示信息
		/// </summary>
		/// <param name="message"></param>
		public static void AlertMessage(string message) {
			if( message.Trim() == string.Empty) {
				return;
			}

			string script = "<script language='javascript'>alert('{0}')</script>";

			HttpContext.Current.Response.Write(string.Format(script,Universal.HtmlEncode(message)));
		}

		/// <summary>
		/// 弹出提示信息并转向某处
		/// </summary>
		/// <param name="message"></param>
		public static void AlertMessage(string message, string strURL) {
			if( message.Trim() == string.Empty || strURL.Trim() == string.Empty ) {
				return;
			}

			string script = string.Format("<script>alert('{0}');window.location.href='{1}';</script>",message,strURL);

			HttpContext.Current.Response.Write(script);
		}

		// 更改文件上传名
		public static string GetUpLoadFileName(string orgfileName) {
			if(orgfileName != null &&  orgfileName != string.Empty) {
				return ( DateTime.Now.Year.ToString()+DateTime.Now.Month.ToString()+"/"+DateTime.Now.Year.ToString()+DateTime.Now.Month.ToString()+DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() +DateTime.Now.Minute.ToString() +DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + new Random().Next(10000) + Path.GetExtension(orgfileName)); 
			}
			else {
				return string.Empty;
			}
		}

		/// <summary>
		/// 删除文件
		/// </summary>
		/// <param name="filePath">图片绝对路径</param>
		/// <returns></returns>
		public static bool DelUpLoadFile( string filePath) {			
			bool result = true;

			if( File.Exists( filePath ) ) {
				try {
					File.Delete(filePath);
				}
				catch {
					result =  false;
				}
			}
			
			return result;
		}

        /// <summary>
        /// 将字符串截断，被截断的字符串后面带...符号
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static string MaxLengthText(string inputString, int maxLength)
        {
            Regex regEx = new Regex("^[\u4e00-\u9fa5]{0,}$"); // 汉字

            if (inputString.Length > maxLength)
            {
                int cnt = 0;
                bool isFirst = true;

                for (int i = 0; i < maxLength; i++)
                {
                    if (!regEx.IsMatch(inputString[i].ToString()))
                    {
                        cnt++;
                        if (inputString.Length > maxLength + cnt)
                        {
                            if (regEx.IsMatch(inputString[maxLength + cnt].ToString()))
                            {
                                if (isFirst)
                                {
                                    cnt--;
                                    isFirst = false;
                                    continue;
                                }
                                isFirst = true;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                if (inputString.Length > maxLength + cnt)
                {
                    inputString = inputString.Substring(0, maxLength + cnt) + "..";
                }
            }
            return inputString;
        }

		public static string ApplicationPath {
			get {
				string path = HttpContext.Current.Request.ApplicationPath;
				if (path == "/") {
					return String.Empty;
				}
				else {
					return path;
				}
			}
		}

		public static string ConvertNullToEmpty(string temp) {
			if (temp == null) {
				return "";
			}
			else {
				return temp;
			}
		}

		/// <summary>
		/// 将字符串格式化为HTML格式
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static string ToHtml(String str) {

			String html = str;
			if (str == null || str.Length == 0) {
				return "";
			}
			html = html.Replace("<", "&lt;");
			html = html.Replace(">", "&gt;");
			html = html.Replace("\r\n", "<br>");
			html = html.Replace("\n", "<br>");
			//html = html.Replace(" ", "&nbsp;");

			return html;
		}

		/// <summary>
		/// 将字符串数组联合成字符串，以seperate隔开
		/// </summary>
		/// <param name="arr"></param>
		/// <param name="seperate"></param>
		/// <returns></returns>
		public static string UnionAll(string[] arr, string seperate) {
			StringBuilder sb = new StringBuilder();
			for (int i=0; i<arr.Length; i++) {
				sb.Append(arr[i]);
				sb.Append(seperate);
			}

			return sb.ToString().Substring(0,sb.ToString().Length - seperate.Length);
		}

        /// <summary>
        /// 高质量缩略图
        /// </summary>
        /// <param name="bigFilePath"></param>
        /// <param name="breviaryFilePath"></param>
        /// <param name="thumbWidth"></param>
        /// <param name="thumbHeigth"></param>
        /// <returns></returns>
        public static bool CreateThumbnailImage(string bigFilePath, string breviaryFilePath, int thumbWidth, int thumbHeigth)
        { 

            Image myBitmap = Image.FromFile(bigFilePath);
            try
            {
                if (myBitmap.Width > thumbWidth || myBitmap.Height > thumbHeigth)
                {
                    if ((float)myBitmap.Width / myBitmap.Height > (float)thumbWidth / thumbHeigth)
                    {
                        thumbHeigth = thumbWidth * myBitmap.Height / myBitmap.Width;
                    }
                    else
                    {
                        thumbWidth = thumbHeigth * myBitmap.Width / myBitmap.Height;
                    }
                }
                else
                {
                    thumbHeigth = myBitmap.Height;
                    thumbWidth = myBitmap.Width;
                }

                //新建一个bmp图片
                System.Drawing.Image myThumbnail = new System.Drawing.Bitmap(thumbWidth, thumbHeigth);
                //新建一个画板
                System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(myThumbnail);
                //设置高质量插值法
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                //设置高质量,低速度呈现平滑程度
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                //清空一下画布
                g.Clear(Color.Transparent);
                //在指定位置画图
                g.DrawImage(myBitmap, new System.Drawing.Rectangle(0, 0, myThumbnail.Width, myThumbnail.Height),
                new System.Drawing.Rectangle(0, 0, myBitmap.Width, myBitmap.Height),
                System.Drawing.GraphicsUnit.Pixel);
                //保存高清晰度的缩略图
                EncoderParameters parms = new EncoderParameters(1);
                EncoderParameter parm = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, ((long)90));
                parms.Param[0] = parm;
                ImageCodecInfo icf = GetImageCodecInfo(ImageFormat.Jpeg);
                myThumbnail.Save(breviaryFilePath, icf, parms);
                parms.Dispose();
                //myThumbnail.Save(breviaryFilePath);
                g.Dispose();

            }
            catch (Exception ex)
            {
                Universal.ExceptionLog("Universal.CreateThumbnailImage", ex.ToString());
                return false;
            }
            finally
            {
                myBitmap.Dispose();
            }
            return true;
        }

        /// <summary>
        /// 获取图像编码解码器的所有相关信息
        /// </summary>
        /// <param name="mimeType">包含编码解码器的多用途网际邮件扩充协议 (MIME) 类型的字符串</param>
        /// <returns>返回图像编码解码器的所有相关信息</returns>
        public static ImageCodecInfo GetCodecInfo(string mimeType)
        {
            ImageCodecInfo[] CodecInfo = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo ici in CodecInfo)
            {
                if (ici.MimeType == mimeType) return ici;
            }
            return null;
        }

        public static ImageCodecInfo GetImageCodecInfo(ImageFormat format)
        {
            ImageCodecInfo[] encoders = ImageCodecInfo.GetImageEncoders();

            foreach (ImageCodecInfo icf in encoders)
            {
                if (icf.FormatID == format.Guid)
                {
                    return icf;
                }
            }

            return null;
        }

		public static bool CreateThumbnailImage_old(string bigFilePath, string breviaryFilePath, int thumbWidth, int thumbHeigth) {
			// 在此处放置用户代码以初始化页面
			Bitmap myBitmap = new Bitmap(bigFilePath);
			try {
				Image.GetThumbnailImageAbort myCallback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
				Image myThumbnail = null;
				
				if (myBitmap.Width <= thumbWidth && myBitmap.Height <= thumbHeigth) {
					if (GetImagetFormatByFileName(breviaryFilePath) == null) {
						myBitmap.Save(breviaryFilePath);
					}
					else {
						myBitmap.Save(breviaryFilePath,GetImagetFormatByFileName(breviaryFilePath));
					}
				}
				else {
					if ((float)myBitmap.Width / myBitmap.Height > (float)thumbWidth/thumbHeigth) {
						thumbHeigth = thumbWidth * myBitmap.Height / myBitmap.Width;
					}
					else {
						thumbWidth = thumbHeigth * myBitmap.Width / myBitmap.Height;
					}
					myThumbnail = myBitmap.GetThumbnailImage(thumbWidth, thumbHeigth, myCallback, IntPtr.Zero);
					if (GetImagetFormatByFileName(breviaryFilePath) == null) {
						myThumbnail.Save(breviaryFilePath);
					}
					else {
						myThumbnail.Save(breviaryFilePath,GetImagetFormatByFileName(breviaryFilePath));
					}
				}
				if (myThumbnail != null) {
					myThumbnail.Dispose(); 
				}
			}
			catch (Exception ex) {
				Universal.ExceptionLog("Universal.CreateThumbnailImage",ex.ToString());
				return false;
			} finally {
				myBitmap.Dispose();
			}
			return true;
		}

		public static bool ThumbnailCallback() {
			return false;
		}

		public static System.Drawing.Imaging.ImageFormat GetImagetFormatByFileName(string FileName) {
			string suffix = FileName.Substring(FileName.LastIndexOf(".")+1).ToLower();
			if (suffix.Equals("bmp")) {
				return System.Drawing.Imaging.ImageFormat.Bmp;
			}
			if (suffix.Equals("emf")) {
				return System.Drawing.Imaging.ImageFormat.Emf;
			}
			if (suffix.Equals("exif")) {
				return System.Drawing.Imaging.ImageFormat.Exif;
			}
			if (suffix.Equals("gif")) {
				return System.Drawing.Imaging.ImageFormat.Gif;
			}
			if (suffix.Equals("icon")) {
				return System.Drawing.Imaging.ImageFormat.Icon;
			}
			if (suffix.Equals("jpg") || suffix.Equals("jpeg")) {
				return System.Drawing.Imaging.ImageFormat.Jpeg;
			}
			if (suffix.Equals("png")) {
				return System.Drawing.Imaging.ImageFormat.Png;
			}
			if (suffix.Equals("tiff")) {
				return System.Drawing.Imaging.ImageFormat.Tiff;
			}
			if (suffix.Equals("wmf")) {
				return System.Drawing.Imaging.ImageFormat.Wmf;
			}
			return null;
		}
        /// <summary>
        /// 发送电子邮件
        /// </summary>
        /// <param name="SendEmail"></param>
        /// <param name="Title"></param>
        /// <param name="Content"></param>
        /// <returns></returns>
        public static void SendMail(string fromName, string SendEmail, string Title, string Content)
        {

            MailMessage mailMsg = new MailMessage();
            mailMsg.From = new MailAddress(System.Configuration.ConfigurationManager.AppSettings["SendUserName"], fromName);
            string[] tos = SendEmail.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string t in tos)
            {
                mailMsg.To.Add(t);
            }
            mailMsg.Sender = mailMsg.From;
            mailMsg.Subject = Title;
            mailMsg.IsBodyHtml = true;
            mailMsg.BodyEncoding = Encoding.UTF8;
            mailMsg.Body = Content;
            mailMsg.Priority = MailPriority.Normal;
            // Smtp configuration
            SmtpClient client = new SmtpClient();
            client.Credentials = new NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["SendUserName"], System.Configuration.ConfigurationManager.AppSettings["SendPassword"]);
            if (string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["MailPort"]))
            {
                client.Port = 25;
            }
            else
            {
                client.Port = int.Parse(System.Configuration.ConfigurationManager.AppSettings["MailPort"]);
            }
            client.Host = System.Configuration.ConfigurationManager.AppSettings["MailServer"];
            if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["EnableSSL"]) && System.Configuration.ConfigurationManager.AppSettings["EnableSSL"].ToLower() == "true")
            {
                client.EnableSsl = true;
            }
            else
            {
                client.EnableSsl = false;
            }

            try
            {
                client.Send(mailMsg);
            }
            catch (SmtpException se)
            {
                throw se;
            }
        }
        /// <summary>
        /// 发送电子邮件
        /// </summary>
        /// <param name="SendEmail"></param>
        /// <param name="Title"></param>
        /// <param name="Content"></param>
        /// <returns></returns>
        public static void SendMail(string fromName, string SendEmail, string Title, string Content, string fromEmailName, string fromNamePwd, string MailPort, string MailServer, string EnabledSSL)
        {

            MailMessage mailMsg = new MailMessage();
            mailMsg.From = new MailAddress(fromEmailName, fromName);
            string[] tos = SendEmail.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string t in tos)
            {
                mailMsg.To.Add(t);
            }
            mailMsg.Sender = mailMsg.From;
            mailMsg.Subject = Title;
            mailMsg.IsBodyHtml = true;
            mailMsg.BodyEncoding = Encoding.UTF8;
            mailMsg.Body = Content;
            mailMsg.Priority = MailPriority.Normal;
            // Smtp configuration
            SmtpClient client = new SmtpClient();
            client.Credentials = new NetworkCredential(fromEmailName, fromNamePwd);
            if (string.IsNullOrEmpty(MailPort))
            {
                client.Port = 25;
            }
            else
            {
                client.Port = int.Parse(MailPort);
            }
            client.Host = MailServer;
            if (!string.IsNullOrEmpty(EnabledSSL) && EnabledSSL.ToLower() == "true")
            {
                client.EnableSsl = true;
            }
            else
            {
                client.EnableSsl = false;
            }

            try
            {
                client.Send(mailMsg);
            }
            catch (SmtpException se)
            {
                throw se;
            }
        }

        public static void SendMailWithAttach(string fromName, string SendEmail, string Title, string Content, string[] Attach, string fromEmailName, string fromNamePwd, string MailPort, string MailServer, string EnabledSSL)
        {
            MailMessage mailMsg = new MailMessage();
            mailMsg.From = new MailAddress(fromEmailName, fromName);
            string[] tos = SendEmail.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string t in tos)
            {
                mailMsg.To.Add(t);
            }
            mailMsg.Sender = mailMsg.From;
            mailMsg.Subject = Title;
            mailMsg.IsBodyHtml = true;
            mailMsg.BodyEncoding = Encoding.UTF8;
            mailMsg.Body = Content;
            mailMsg.Priority = MailPriority.Normal;
            // Smtp configuration
            SmtpClient client = new SmtpClient();
            client.Credentials = new NetworkCredential(fromEmailName, fromNamePwd);
            if (string.IsNullOrEmpty(MailPort))
            {
                client.Port = 25;
            }
            else
            {
                client.Port = int.Parse(MailPort);
            }
            client.Host = MailServer;
            if (!string.IsNullOrEmpty(EnabledSSL) && EnabledSSL.ToLower() == "true")
            {
                client.EnableSsl = true;
            }
            else
            {
                client.EnableSsl = false;
            }

            // add Attach
            foreach (string file in Attach)
            {
                Attachment data = new Attachment(file, MediaTypeNames.Application.Octet);
                mailMsg.Attachments.Add(data);
            }

            try
            {
                client.Send(mailMsg);
            }
            catch (SmtpException se)
            {
                throw se;
            }
        }
		/// <summary>
		/// 异常日志
		/// </summary>
		/// <param name="ErrorMessage"></param>
        public static void ExceptionLog(string location, string ErrorMessage)
        {

            lock (lockHelper)
            {
                System.IO.FileStream fileStr = System.IO.File.Open(GetWebLogicPath() + "/logFile/" + "log" + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + ".txt", System.IO.FileMode.Append);
                string str = "Exception time：" + DateTime.Now.ToString();
                str += "， Location：" + location + "\r\n";
                str += "info：" + ErrorMessage + "\r\n";
                byte[] info = new System.Text.UTF8Encoding(true).GetBytes(str + "\r\n");
                fileStr.Write(info, 0, info.Length);
                fileStr.Close();
            }
        }

        public static void ExceptionLog(string location, string errorMessage,string fileName) {
            lock (lockHelper)
            {
                System.IO.FileStream fileStr = System.IO.File.Open(GetWebLogicPath() + "/logFile/" + "log" + DateTime.Now.Year + DateTime.Now.Month + ".txt", System.IO.FileMode.Append);
                string str = "Exception time：" + DateTime.Now.ToString();
                str += "， Location：" + location + "\r\n";
                str += "info：" + errorMessage + "\r\n";
                byte[] info = new System.Text.UTF8Encoding(true).GetBytes(str + "\r\n");
                fileStr.Write(info, 0, info.Length);
                fileStr.Close();
            }
        }

		/// <summary>
		/// 将图片名转换成缩略图的文件名（文件名加上_small）
		/// </summary>
		/// <param name="PictureUrl"></param>
		/// <returns></returns>
		public static string PictureUrlToBreviaryUrl(string PictureUrl) {
			if (PictureUrl == null || PictureUrl.IndexOf(".") == -1) {
				throw new Exception("The file name is invalid!");
			}
			else {
				return PictureUrl.Insert(PictureUrl.LastIndexOf("."),"_small");
			}
		}

        /// <summary>
        /// 将图片名转换成缩略图的文件名（文件名加上_）
        /// </summary>
        /// <param name="PictureUrl"></param>
        /// <returns></returns>
        public static string PictureUrlToBreviaryUrl(string PictureUrl,string log)
        {
            if (PictureUrl == null || PictureUrl.IndexOf(".") == -1)
            {
                throw new Exception("The file name is invalid!");
            }
            else
            {
                return PictureUrl.Insert(PictureUrl.LastIndexOf("."), "_" + log);
            }
        }

		public static string GetHtmlSourceCode(string url) {

			System.Net.WebRequest request = System.Net.WebRequest.Create(url);

			try {
				//请求服务
				System.Net.WebResponse response = request.GetResponse();
				//返回信息
				Stream resStream = response.GetResponseStream(); 
				StreamReader sr = null;
				sr = new StreamReader(resStream, System.Text.Encoding.Default);
				string outString = sr.ReadToEnd();
				resStream.Close(); 
				sr.Close();	
				return outString;
			}
			catch(Exception ex) {
				throw new Exception(string.Format("页面地址“{0}”无法访问！",ex.Message));
			}
		}

        public static string GetHtmlSourceCode(string url, string encoding)
        {

            System.Net.WebRequest request = System.Net.WebRequest.Create(url);

            try
            {
                //请求服务
                System.Net.WebResponse response = request.GetResponse();
                //返回信息
                Stream resStream = response.GetResponseStream();
                StreamReader sr = null;
                sr = new StreamReader(resStream, System.Text.Encoding.GetEncoding(encoding));
                string outString = sr.ReadToEnd();
                resStream.Close();
                sr.Close();
                return outString;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("页面地址“{0}”无法访问！", ex.Message));
            }
        }

		public static string GetHtmlSourceCodeByPost(string url,HttpRequest request,string ViewState) {
			string realUrl = "", param = "__VIEWSTATE="+ViewState; //EVENTTARGET
			if (url.IndexOf("?") == -1) {
				realUrl = url;
			}
			else {
				realUrl = url.Substring(0,url.IndexOf("?"));
				param += url.Replace(realUrl+"?","&");
			}
			System.Net.HttpWebRequest req = System.Net.WebRequest.Create(realUrl) as System.Net.HttpWebRequest;
			System.Net.CookieContainer cookieContainer = new System.Net.CookieContainer();
			req.CookieContainer = cookieContainer;
			req.AllowAutoRedirect = true;
			req.Method = "POST";
			req.ContentType="application/x-www-form-urlencoded";
			//req.ContentType="multipart/form-data; boundary=-----------------------------7d73e1f90622";

			foreach (object o in request.Form.Keys) {
				if (o.ToString() != "__VIEWSTATE" && o.ToString() != "hdViewState") {
					param += "&"+o.ToString()+"="+request.Form[o.ToString()];
				}
			}
			
			byte[] SomeBytes = null;
			SomeBytes = Encoding.UTF8.GetBytes(GetUrlEncodeParam(param));
			req.ContentLength = SomeBytes.Length;
			Stream newStream = req.GetRequestStream();
			newStream.Write(SomeBytes, 0, SomeBytes.Length);
			newStream.Close();
			System.Net.WebClient webClient = new System.Net.WebClient();

			try {
				//请求服务
				System.Net.WebResponse response = req.GetResponse();
				//返回信息
				Stream resStream = response.GetResponseStream(); 
				StreamReader sr = null;
				sr = new StreamReader(resStream, System.Text.Encoding.UTF8);
				string outString = sr.ReadToEnd();
				resStream.Close();
				sr.Close();	
				return outString;
			}
			catch(Exception ex) {
				throw new Exception(string.Format("页面地址“{0}”无法访问！",ex.Message));
			}
		}

		public static string GetUrlEncodeParam(string param) {
			StringBuilder UrlEncoded = new StringBuilder();
			Char[] reserved = {'?', '=', '&'};
			if (param != null) {
				int i=0, j;
				while(i<param.Length) {
					j=param.IndexOfAny(reserved, i);
					if (j==-1) {
						UrlEncoded.Append(HttpUtility.UrlEncode(param.Substring(i, param.Length-i),System.Text.Encoding.Default));
						break;
					}
					UrlEncoded.Append(HttpUtility.UrlEncode(param.Substring(i, j-i),System.Text.Encoding.UTF8));
					UrlEncoded.Append(param.Substring(j,1));
					i = j+1;
				}
				return UrlEncoded.ToString();

			}
			else {
				return "";
			}
		}

		/// <summary>
		/// 得到网站的根目录（物理路径）
		/// </summary>
		/// <returns></returns>
		public static string getCurrentPhyUrl() {   
			string[] arrRequestFileName = (string[])System.Web.HttpContext.Current.Request.PhysicalPath.ToString().Trim().Split('\\');   
			int RequestFileNameLen = arrRequestFileName[arrRequestFileName.GetUpperBound(0)].ToString().Length;   
			int intStart = System.Web.HttpContext.Current.Request.PhysicalPath.ToString().Trim().Length-RequestFileNameLen;   
			string RequestPhyURL = System.Web.HttpContext.Current.Request.PhysicalPath.ToString().Remove(intStart,RequestFileNameLen);   
			return RequestPhyURL;
		}
         

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="myfile"></param>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static string UploadFile(System.Web.HttpPostedFile myfile, string filepath)
        {
            if (myfile.FileName.Length < 3)
            {
                return "";
            }

            string oldFilename = Universal.GetPicName(myfile);
            string stufixName = oldFilename.Substring(oldFilename.LastIndexOf(".") + 1).ToLower();
            if (stufixName == "asp" || stufixName == "php" || stufixName == "aspx" || stufixName == "exe")
            {
                System.Web.HttpContext.Current.Response.Write("文件上传失败，此文件格式不能上传！");
                return "";
            }
            string newFilename = Universal.GetUpLoadFileName(oldFilename);
            if (!System.IO.Directory.Exists(filepath + newFilename.Substring(0, newFilename.IndexOf("/"))))
            {
                DirectoryInfo di = new DirectoryInfo(filepath + newFilename.Substring(0, newFilename.IndexOf("/")));
                if (!di.Exists)
                    di.Create(); 
            }
            myfile.SaveAs(filepath + newFilename);
            myfile.InputStream.Close();

            return newFilename;
        }

		/// <summary>
		/// 上传文件
		/// </summary>
		/// <param name="myfile"></param>
		/// <param name="filepath"></param>
		/// <returns></returns>
		public static string UploadFile(System.Web.UI.HtmlControls.HtmlInputFile myfile,string filepath) {
			if(myfile.PostedFile.FileName.Length<3) {
				return "";
			}

			string oldFilename = Universal.GetPicName(myfile);
            string stufixName = oldFilename.Substring(oldFilename.LastIndexOf(".")+1).ToLower();
            if(stufixName == "asp" || stufixName == "php" || stufixName == "aspx" || stufixName == "exe") {
                System.Web.HttpContext.Current.Response.Write("文件上传失败，此文件格式不能上传！");
                return "";
            }
			string newFilename = Universal.GetUpLoadFileName(oldFilename);
			if (!System.IO.Directory.Exists(filepath+newFilename.Substring(0,newFilename.IndexOf("/")))) {
                DirectoryInfo di = new DirectoryInfo(filepath + newFilename.Substring(0, newFilename.IndexOf("/")));
                if (!di.Exists)
                    di.Create(); 
			}
			myfile.PostedFile.SaveAs( filepath + newFilename);
			myfile.PostedFile.InputStream.Close();

			return newFilename;
		}

        public static string UploadFile(System.Web.UI.WebControls.FileUpload myfile, string filepath) {
            if(myfile.PostedFile.FileName.Length < 3) {
                return "";
            }

            string oldFilename = myfile.FileName;
            string stufixName = oldFilename.Substring(oldFilename.LastIndexOf(".") + 1).ToLower();
            if(stufixName == "asp" || stufixName == "php" || stufixName == "aspx" || stufixName == "exe") {
                System.Web.HttpContext.Current.Response.Write("文件上传失败，此文件格式不能上传！");
                return "";
            }
            string newFilename = Universal.GetUpLoadFileName(oldFilename);
            if(!System.IO.Directory.Exists(filepath + newFilename.Substring(0, newFilename.IndexOf("/")))) { 
                DirectoryInfo di = new DirectoryInfo(filepath + newFilename.Substring(0, newFilename.IndexOf("/")));
                if (!di.Exists)
                    di.Create();
            }
            myfile.PostedFile.SaveAs(filepath + newFilename);
            myfile.PostedFile.InputStream.Close();

            return newFilename;
        }

		public static string GetPicName(System.Web.UI.HtmlControls.HtmlInputFile myfile) {
			return myfile.Value.Substring( myfile.Value.LastIndexOf( "\\" ) + 1 );
		}

        public static string GetPicName(System.Web.HttpPostedFile myfile)
        {
            return myfile.FileName.Substring(myfile.FileName.LastIndexOf("\\") + 1);
        }

        /// <summary>
        /// 对URL追加参数
        /// </summary>
        /// <param name="url"></param>
        /// <param name="paramName"></param>
        /// <param name="paramValue"></param>
        /// <returns></returns>
        public static string LocationAddParam(string url, string paramName, string paramValue)
        {
            if (url == "" || url.Length == 0)
            {
                url = System.Web.HttpContext.Current.Request.RawUrl;
            }
            if (url.EndsWith("#"))
            {
                url = url.Substring(0, url.Length - 1);
            }
            int pos = url.IndexOf("&" + paramName + "=");
            if (pos == -1)
            {
                pos = url.IndexOf("?" + paramName + "=");
            }
            if (pos != -1)
            {
                if (url.IndexOf("&", pos + 1) == -1)
                {
                    url = url.Substring(0, url.IndexOf("=", pos + 1) + 1) + paramValue;
                }
                else
                {
                    string tempStr = url.Substring(url.IndexOf("&", pos + 1));
                    url = url.Substring(0, url.IndexOf("=", pos + 1) + 1) + paramValue + tempStr;
                }
            }
            else
            {
                if (url.IndexOf("?") != -1)
                {
                    url = url + "&" + paramName + "=" + paramValue;
                }
                else
                {
                    url = url + "?" + paramName + "=" + paramValue;
                }
            }
            return url;
        }

        public static string LocationDelParam(string url, string paramName)
        {
            if (string.IsNullOrEmpty(url))
            {
                url = HttpContext.Current.Request.Url.ToString();
            }
            if (url.EndsWith("#"))
            {
                url = url.Substring(0, url.Length - 1);
            }
            int pos = url.IndexOf("&" + paramName + "=");
            if (pos == -1)
            {
                pos = url.IndexOf("?" + paramName + "=");
            }
            if (pos != -1)
            {
                if (url.IndexOf("&", pos + 1) == -1)
                {
                    url = url.Substring(0, pos);
                }
                else
                {
                    string tempStr = url.Substring(url.IndexOf("&", pos + 1) + 1);
                    url = url.Substring(0, pos + 1) + tempStr;
                }
            }
            return url;
        }



        public static string CheckRequestString(string text)
        {
            string retStr = "";
            if (text != null && text != String.Empty && text != "")
            {
                retStr = text;
                retStr = retStr.Replace("'", "''");//sql关键字
                retStr = retStr.Replace("create", "create　");
                retStr = retStr.Replace("sp_", "sp_　");
                retStr = retStr.Replace("xp_", "xp_　");
                retStr = retStr.Replace("0x", "0x　");
                retStr = retStr.Replace("union", "union　");
                retStr = retStr.Replace("update", "update　");
                retStr = retStr.Replace("cmd", "cmd　");
                retStr = retStr.Replace("delete", "delete　");
                retStr = retStr.Replace("backup", "backup　");
                retStr = retStr.Replace("restore", "restore　");
                retStr = retStr.Replace("insert", "insert　");
                retStr = retStr.Replace("exec", "exec　");
                retStr = retStr.Replace("truncate", "truncate　");
                retStr = retStr.Replace("chr(0)", "");
                retStr = retStr.Replace("char", "char　");
                retStr = retStr.Replace("drop table", "drop　table");
                retStr = retStr.Replace("count", "count　");
                retStr = retStr.Replace("declare", "declare　");
                retStr = retStr.Replace("net user", "net　user　");
                retStr = retStr.Replace("net localgroup administrators", "net　localgroup　administrators　");
                retStr = retStr.Replace("xp_cmdshell", "xp_cmdshell　");
                retStr = retStr.Replace("/add", "/add　");
                retStr = retStr.Replace("net user TsInternetUsers", "net　user　TsInternetUsers　");
                retStr = retStr.Replace("net localGroup Administrators", "net　localGroup　Administrators　");
                retStr = retStr.Replace("secedit /export /CFG", "secedit　/export　/CFG　");
                retStr = retStr.Replace("echo sedenynetworklogonright =>>", "echo　sedenynetworklogonright　=>>　");
                retStr = retStr.Replace("secedit /configure /db", "secedit　/configure　/db　");
                retStr = retStr.Replace("<", "&#x3C;");
                retStr = retStr.Replace("<", "&#x3C;");//跨站脚本
                retStr = retStr.Replace(">", "&#x3E;");
                retStr = retStr.Replace("jscript:", "jscript：");
                retStr = retStr.Replace("javascript:", "javascript：");
                retStr = retStr.Replace("vbscript:", "vbscript：");
                retStr = retStr.Replace("&", "&#x26;");
                retStr = retStr.Replace("<iframe", "[iframe"); 

            }
            return retStr;
        }

        /// <summary>
        /// 处理用户提交的请求
        /// </summary>
        public static bool IsBadRequest()
        {
            bool result = false;
            try
            {
                string getKeys = "";

                if (System.Web.HttpContext.Current.Request.QueryString != null)
                {

                    for (int i = 0; i < System.Web.HttpContext.Current.Request.QueryString.Count; i++)
                    {
                        getKeys = System.Web.HttpContext.Current.Request.QueryString.Keys[i];
                        string badStr = ProcessSqlStr(System.Web.HttpContext.Current.Request.QueryString[getKeys]);
                        if (badStr.Length > 0)
                        {
                            result = true;
                            break;

                        }
                    }
                }
                if (System.Web.HttpContext.Current.Request.Form != null)
                {
                    for (int i = 0; i < System.Web.HttpContext.Current.Request.Form.Count; i++)
                    {
                        getKeys = System.Web.HttpContext.Current.Request.Form.Keys[i];
                        string badStr = ProcessSqlStr(System.Web.HttpContext.Current.Request.Form[getKeys]);
                        if (badStr.Length > 0)
                        {
                            if (getKeys != "__EVENTTARGET" && getKeys != "__EVENTARGUMENT" && getKeys != "__VIEWSTATE")
                            {
                                result = true;
                                break;
                            }

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                // 错误处理: 处理用户提交信息!
                throw new Exception(string.Format("检查传递参数错误！", ex.Message));
            }
            return result;
        }

        /// <summary>
        /// 分析用户请求是否正常
        /// </summary>
        /// <param name="Str">传入用户提交数据</param>
        /// <returns>返回是否含有SQL注入式攻击代码</returns>
        private static string ProcessSqlStr(string Str)
        {
            string ReturnValue = "";
            try
            {
                if (Str != "")
                {
                    string SqlStr = "and|add|exec|insert|create|select|delete|form|script|drop|update|count|mid|master|set|truncate|backup|char|declare|use|model|restore|echo|sedenynetworklogonright|localgroup|administrators|xp_|sp_|cmdshell|union|secedit|export|configure|'|%20|%27";
                    string[] anySqlStr = SqlStr.Split('|');
                    foreach (string ss in anySqlStr)
                    {
                        if (Str.ToLower().IndexOf(ss) >= 0)
                        {
                            ReturnValue += ss + ",";
                        }
                    }
                }
            }
            catch
            {
            }
            if (ReturnValue.Length > 0)
            {
                ReturnValue = ReturnValue.Substring(0, ReturnValue.Length - 1);
            }
            return ReturnValue;
        }

        /// <summary>
        /// 拷贝文件夹
        /// </summary>
        /// <param name="fromDir">源文件夹</param>
        /// <param name="toDir">目标文件夹</param>
        public static void CopyDirectory(string fromDir, string toDir)
        {
            if (!Directory.Exists(fromDir))
            {
                throw new Exception("目录不存在！");
            }
            Directory.CreateDirectory(toDir);
            string[] files = Directory.GetFiles(fromDir);
            for (int i = 0; i < files.Length; i++)
            {
                string currentFile = files[i].Substring(files[i].LastIndexOf("\\") + 1);
                File.Copy(fromDir + "/" + currentFile, toDir + "/" + currentFile, true);
            }
            string[] dirs = Directory.GetDirectories(fromDir);
            for (int i = 0; i < dirs.Length; i++)
            {
                string currentDir = dirs[i].Substring(dirs[i].LastIndexOf("\\") + 1);
                CopyDirectory(fromDir + "/" + currentDir, toDir + "/" + currentDir);
            }
        }


        #region 加密解密用
        public static byte[] HexToBytes(string hexText)
        {
            int size = (int)hexText.Length / 2;
            byte[] bytes = new byte[size];
            for (int i = 0; i < size; i++)
            {
                bytes[i] = byte.Parse(hexText.Substring(i * 2, 2), System.Globalization.NumberStyles.HexNumber);
            }
            return bytes;
        }

        public static bool TryParseHex(string hexText, out byte[] bytes)
        {
            bytes = null;
            if (string.IsNullOrEmpty(hexText))
                return false;
            if (hexText.Length % 2 != 0)
                return false;
            int size = (int)hexText.Length / 2;
            byte[] tmpBytes = new byte[size];
            for (int i = 0; i < size; i++)
            {
                if (!byte.TryParse(hexText.Substring(i * 2, 2), System.Globalization.NumberStyles.HexNumber,
                    System.Globalization.NumberFormatInfo.InvariantInfo, out tmpBytes[i]))
                    return false;
            }
            bytes = tmpBytes;
            return true;
        }

        public static string BytesToHex(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder(bytes.Length * 2);
            foreach (byte b in bytes)
            {
                sb.AppendFormat(b.ToString("x2"));
            }
            return sb.ToString();
        }
        #endregion


        /// <summary>
        /// 将string转换成int
        /// </summary>
        /// <param name="o"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int GetInt(string num, int defaultValue)
        {
            int.TryParse(num, out defaultValue);
            return defaultValue;
        }

        /// <summary>
        /// 将string转datetime
        /// </summary>
        /// <param name="time"></param>
        /// <param name="defaultDate"></param>
        /// <returns></returns>
        public static DateTime GetSqlDateTime(string time, DateTime defaultDate)
        {
            DateTime.TryParse(time, out defaultDate);
            if (defaultDate < SqlDateTime.MinValue.Value || defaultDate > SqlDateTime.MaxValue.Value)
            {
                defaultDate = SqlDateTime.MinValue.Value;
            }
            return defaultDate;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Content"></param>
        /// <param name="Num"></param>
        /// <returns></returns>
        public static string GetIntercepitonStr(string Content, int Num)
        {
            StringBuilder strContext = new StringBuilder();

            if (Content.Length <= Num)
            {
                strContext.Append(Content);
            }
            else
            {
                strContext.Append(Content.Substring(0, Num));
                //strContext.Append("...");
            }
            return strContext.ToString();
        }

        /// <summary>
        /// 高质量缩略图
        /// </summary>
        /// <param name="bigFilePath"></param>
        /// <param name="breviaryFilePath"></param>
        /// <param name="thumbWidth"></param>
        /// <param name="thumbHeigth"></param>
        /// <returns></returns>
        public static bool CreateClipPic(string bigFilePath, string ClipFilePath, int ClipPicWidth, int ClipPicHeight, int x1, int y1, int NewWidth, int NewHeight)
        {

            Image myBitmap = Image.FromFile(bigFilePath);
            try
            {


                //新建一个bmp图片
                System.Drawing.Image myThumbnail = new System.Drawing.Bitmap(NewWidth, NewHeight);
                //新建一个画板
                System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(myThumbnail);
                //设置高质量插值法
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                //设置高质量,低速度呈现平滑程度
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                //清空一下画布
                g.Clear(Color.Transparent);
                //在指定位置画图
                g.DrawImage(myBitmap, new System.Drawing.Rectangle(0, 0, myThumbnail.Width, myThumbnail.Height),
                new System.Drawing.Rectangle(x1, y1, ClipPicWidth, ClipPicHeight),
                System.Drawing.GraphicsUnit.Pixel);
                //保存高清晰度的缩略图
                EncoderParameters parms = new EncoderParameters(1);
                EncoderParameter parm = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, ((long)100));
                parms.Param[0] = parm;
                ImageCodecInfo icf = GetImageCodecInfo(ImageFormat.Jpeg);
                myThumbnail.Save(ClipFilePath, icf, parms);
                parms.Dispose();
                myThumbnail.Save(ClipFilePath);
                g.Dispose();

            }
            catch
            {
                return false;
            }
            finally
            {
                myBitmap.Dispose();
            }
            return true;
        }

        /// <summary>
        /// 获取网站物理根目录（最后不带/）
        /// </summary>
        /// <returns></returns>
        public static string GetWebLogicPath()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            if (path.EndsWith("\\"))
            {
                return path.Substring(0, path.Length - 1).Replace("\\", "/");
            }
            else
            {
                return path.Replace("\\", "/");
            }
        }

        private static Regex regHtml = new Regex(
            @"(?<script><script[^>]*?>.*?</script>)|(?<style><style>.*?</style>)|(?<comment><!--.*?-->)" +
            @"|(?<html><\/?\w+((\s+(\w|\w[\w-]*\w)(\s*=\s*(?:"".*?""|'.*?'|[^'"">\s]+))?)+\s*|\s*)\/?>)" + // HTML标记
            @"|(?<quot>&(quot|#34);)" + // 符号: "
            @"|(?<amp>&(amp|#38);)" + // 符号: &
            @"|(?<lt>&(lt|#60);)" + // 符号: <
            @"|(?<gt>&(gt|#62);)" + // 符号: >
            @"|(?<iexcl>&(iexcl|#161);)" + // 符号: (char)161
            @"|(?<cent>&(cent|#162);)" + // 符号: (char)162
            @"|(?<pound>&(pound|#163);)" + // 符号: (char)163
            @"|(?<copy>&(copy|#169);)" + // 符号: (char)169
            @"|(?<others>&(\d+);)" + // 符号: 其他
            @"|(?<space> | )" +// 空格
            @"|(?<control>[\r\n\s])", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);

        private static Regex regBlank = new Regex(@"\s+", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);


        /// <summary>
        /// 转换HTML为纯文本
        /// </summary>
        /// <param name="html">HTML字符串</param>
        /// <returns></returns>
        public static string Html2Text(string html)
        {
            string txt = regHtml.Replace(html.Replace("&nbsp;", " "), new MatchEvaluator(Html2Text_Match));
            txt = regBlank.Replace(txt, " ");
            return txt; // 替换多个连续空格
        }


        private static string Html2Text_Match(Match m)
        {
            if (m.Groups["quot"].Value != string.Empty)
                return "\"";
            else if (m.Groups["amp"].Value != string.Empty)
                return "&";
            else if (m.Groups["lt"].Value != string.Empty)
                return "<";
            else if (m.Groups["gt"].Value != string.Empty)
                return ">";
            else if (m.Groups["iexcl"].Value != string.Empty)
                return "\xa1";
            else if (m.Groups["cent"].Value != string.Empty)
                return "\xa2";
            else if (m.Groups["pound"].Value != string.Empty)
                return "\xa3";
            else if (m.Groups["copy"].Value != string.Empty)
                return "(c)";
            else if (m.Groups["space"].Value != string.Empty)
                return "\u0020";
            else if (m.Groups["control"].Value != string.Empty)
                return "\u0020";
            else
                return string.Empty;
        }
	}

}
