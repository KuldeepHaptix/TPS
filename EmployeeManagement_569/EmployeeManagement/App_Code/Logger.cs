using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Net.Mail;
using System.IO;

namespace EmployeeManagement
{
    public class Logger
    {

        public Logger()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public static int SendMailForgetPassword(string subject, string bmessage, string emailid)
        {
            string FromEmailAddress = "info@xpditesolutions.com";
            string ToEmailAddress = emailid;
            string UserEmailAddress = emailid;
            string EmailSubject = subject;

            string EmailMessage = bmessage;

            SmtpClient Client = new SmtpClient();
            MailMessage message = new MailMessage(FromEmailAddress, ToEmailAddress);

            try
            {
                message.Subject = EmailSubject;
                //Body can be Html or text format
                //Specify true if it  is html message
                message.IsBodyHtml = true;
                //arranging the email body in proper format.Also appending the user email inside the body
                //Due to the security reasons on goddaddy.com server..from email address can only from 
                // domains hosted on godaddy.com
                message.Priority = MailPriority.High;
                string str;
                str = EmailMessage;
                if (UserEmailAddress != "")
                {
                    str = null;
                    str = UserEmailAddress + "<br/><br/>";
                    str = str + EmailMessage;
                }
                message.Body = str;
                message.Bcc.Add("bhavin.patel@xpditesolutions.com");
                Client.Send(message);
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int SendBugMail(string exMessage, string exStack)
        {
            //string FromEmailAddress, string ToEmailAddress, string UserEmailAddress, string EmailSubject, string EmailMessage
            string FromEmailAddress = "info@xpditesolutions.com";
            string ToEmailAddress = "bugs@xpditesolutions.com";
            string UserEmailAddress = "BUG";
            string EmailSubject = "AUTOMATED BUG MESSAGE FROM CM";

            string EmailMessage = "";
            EmailMessage = "<br/><br/>";
            EmailMessage += "A new bug has been found with the following Details";
            EmailMessage += "<br/><br/>";
            EmailMessage += "ERROR MESSAGE :" + exMessage + "<br/>"; ;
            EmailMessage += "ERROR STACKTRACE :" + exStack + "<br/>"; ;

            SmtpClient Client = new SmtpClient();
            MailMessage message = new MailMessage(FromEmailAddress, ToEmailAddress);
            try
            {
                message.Subject = EmailSubject;
                //Body can be Html or text format
                //Specify true if it  is html message
                message.IsBodyHtml = true;
                //arranging the email body in proper format.Also appending the user email inside the body
                //Due to the security reasons on goddaddy.com server..from email address can only from 
                // domains hosted on godaddy.com
                message.Priority = MailPriority.High;
                string str;
                str = EmailMessage;
                if (UserEmailAddress != "")
                {
                    str = null;
                    str = UserEmailAddress + "<br/><br/>";
                    str = str + EmailMessage;
                }
                message.Body = str;
                //message.Bcc.Add("bugs@xpditesolutions.com");
                //message.Bcc.Add("yashvant@xpditesolutions.com");
                Client.Send(message);
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }


        /// <summary>
        /// for esp
        /// 30/06/2011
        /// </summary>
        /// <param name="FromMail"></param>
        /// <param name="ToMail"></param>
        /// <param name="Message"></param>
        /// <returns></returns>
        public int SendEMail(string FromMail, string ToMail, string Message)
        {
            string FromEmailAddress = FromMail;
            string ToEmailAddress = ToMail;
            string UserEmailAddress = "";
            string EmailSubject = "Greet:";
            SmtpClient Client = new SmtpClient();
            MailMessage message = new MailMessage(FromMail, ToMail);
            try
            {
                message.Subject = EmailSubject;

                message.IsBodyHtml = true;
                message.Priority = MailPriority.High;
                string str;
                str = Message;
                message.Body = str;
                Client.Send(message);
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static void CriticalBugMail(string message, string exception, string source)
        {
            try
            {

                if (HttpContext.Current.Request.Cookies["UserName"] != null)
                {
                    SendCriticalBugMail("Cricital Bugs from SIP", "IP: " + HttpContext.Current.Request.UserHostAddress + " <br/> " + "User: " + HttpContext.Current.Request.Cookies["UserName"].ToString() + " <br/> " + "Exception Message: " + message + " <br/> " + "StackTrace: " + exception + " <br/> " + "Source: " + source, "");
                }
                else
                {
                    SendCriticalBugMail("Cricital Bugs from SIP", "IP: " + HttpContext.Current.Request.UserHostAddress + " <br/> " + "User: " + "Session Null" + " <br/> " + "Exception Message: " + message + " <br/> " + "StackTrace: " + exception + " <br/> " + "Source: " + source, "");
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Critical Bug Mail
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="bmessage"></param>
        /// <param name="emailid"></param>
        /// <returns></returns>
        public static int SendCriticalBugMail(string subject, string bmessage, string emailid)
        {
            string FromEmailAddress = "nileshrai78@gmail.com";
            string ToEmailAddress = "nileshrai78@gmail.com";
            string UserEmailAddress = emailid;
            string EmailSubject = subject;

            string EmailMessage = bmessage;

            SmtpClient Client = new SmtpClient("smtp.gmail.com");
            Client.Port = 578;
            MailMessage message = new MailMessage(FromEmailAddress, ToEmailAddress);

            try
            {
                message.Subject = EmailSubject;
                //Body can be Html or text format
                //Specify true if it  is html message
                message.IsBodyHtml = true;
                //arranging the email body in proper format.Also appending the user email inside the body
                //Due to the security reasons on goddaddy.com server..from email address can only from 
                // domains hosted on godaddy.com
                message.Priority = MailPriority.High;
                string str;
                str = EmailMessage;
                message.Body = str;
                message.Bcc.Add("nileshrai78@gmail.com");
                //message.CC.Add("ankit@xpditesolutions.com");
                Client.Send(message);
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static void CriticalBugLog(string message, string exception, string source)
        {
            try
            {

                if (HttpContext.Current.Request.Cookies["UserName"] != null)
                {
                    WriteCriticalLog("IP: " + HttpContext.Current.Request.UserHostAddress + " || " + "User: " + HttpContext.Current.Request.Cookies["UserName"].ToString() + " || " + "Source: " + source + " || " + "Exception Message: " + message + " || " + "StackTrace: " + exception);
                }
                else
                {
                    WriteCriticalLog("IP: " + HttpContext.Current.Request.UserHostAddress + " || " + "User: " + "Session Null" + " || " + "Source: " + source + " || " + "Exception Message: " + message + " ||" + "StackTrace: " + exception);
                }
            }
            catch (Exception ex)
            {
            }
        }

        private static System.IO.StreamWriter wrCritical = null;
        private static string pathCritical = HttpContext.Current.Server.MapPath("~") + "\\Log\\CriticalBugs.txt";
        private static string pathActivity = HttpContext.Current.Server.MapPath("~") + "\\Log\\Activity.txt";
        // private static int intCriticalSkip = LoggerConstantMode.intCriticalSkip;//10;
        private static int intCriticalCount = 0;

        public static void WriteCriticalLog(string log)
        {
            try
            {
                EmailClass email = new EmailClass();
                string path = HttpContext.Current.Server.MapPath(".") + "//Log//CriticalLog.txt";
                if (!File.Exists(path))
                {
                    File.Create(path);
                }
                StreamWriter sw = new StreamWriter(path, true);
                sw.WriteLine("Time:" + getIndiantime() + "\n" + log + "\n\n");
               // string Schids =HttpContext.Current.Application["schId"].ToString();
              //  log = "Time:" + getIndiantime() + "\nLog:" + log + "\n SchoolID:" + Schids + "";
                email.SendCriticalBugMail("Time:" + getIndiantime() + "::::::::::::" + log + "");
                email.SendEmail("Critical Bug Complaint Management From Page:", log,System.Web.Mail.MailFormat.Text, "", "nieshrai78@gmail.com");
                SendCriticalBugMail("Employee Management database Export Critical Bug.", log, "nieshrai78@gmail.com");
                sw.Close();
                sw.Dispose();
            }
            catch (Exception err)
            {
                string errmsg = err.StackTrace.ToString();
            }
        }
        public static void WriteClientCriticalLog(string log, string username, string softwarevirsion)
        {
            try
            {
                EmailClass email = new EmailClass();
                string path = HttpContext.Current.Server.MapPath(".") + "//Log//ClientCriticalLog.txt";
                StreamWriter sw = new StreamWriter(path, true);
                sw.WriteLine("Time:" + getIndiantime() + "\nUsername:" + username + "\nSoftwareVirson:" + softwarevirsion + "\nLog:" + log + "\n\n");
                email.SendEmail("Critical Bug Employee Management Client", "Time:" + getIndiantime() + "\nUsername:" + username + "\nSoftwareVirson:" + softwarevirsion + "\nLog:" + log + "\n\n", System.Web.Mail.MailFormat.Text, "", "nileshrai78@gmail.com");
                sw.Close();
                sw.Dispose();
            }
            catch (Exception err)
            {
                string errmsg = err.StackTrace.ToString();
            }
        }

        public static void SendForgotPwdEmail(string email, string subject, string body)
        {
            try
            {
                EmailClass email1 = new EmailClass();
                email1.SendEmail(subject, body, System.Web.Mail.MailFormat.Text, "", email);
            }
            catch (Exception err)
            {
                string errmsg = err.StackTrace.ToString();
            }
        }


        public static void WriteActivityLog(string log)
        {
            try
            {
                string path = HttpContext.Current.Server.MapPath(".") + "//Log//ActivityLog.txt";
                StreamWriter sw = new StreamWriter(path, true);
                sw.WriteLine("Time:" + getIndiantime() + "\n" + log + "\n\n");
                sw.Close();
                sw.Dispose();
            }
            catch (Exception err)
            {
                string errmsg = err.StackTrace.ToString();
            }
        }

        public static string getIndiantime()
        {
            try
            {
                DateTime nonISD = DateTime.Now;

                //Change Time zone to ISD timezone
                TimeZoneInfo myTZ = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                DateTime ISDTime = TimeZoneInfo.ConvertTime(nonISD, TimeZoneInfo.Local, myTZ);
                string temp = ISDTime.ToString();
                return temp;
            }
            catch (Exception ex)
            {
                return DateTime.Now.ToString();
            }
        }

        public static DateTime getIndiantimeDT()
        {
            try
            {
                DateTime nonISD = DateTime.Now;

                TimeZoneInfo myTZ = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                DateTime ISDTime = TimeZoneInfo.ConvertTime(nonISD, TimeZoneInfo.Local, myTZ);

                return ISDTime;
            }
            catch (Exception ex)
            {
                return DateTime.Now;
            }

        }

        public static void PageActivityFunction(string pagesource, string message, string trace)
        {
            try
            {
                Logger.WritePageAction(HttpContext.Current.Request.UserHostAddress, pagesource, message, trace);
            }
            catch (Exception ex)
            {
            }
        }

        private static System.IO.StreamWriter wrActivity = null;
        // private static int intActivitySkip = LoggerConstantMode.intActivitySkip;//20;
        private static int intActivityCount = 0;

        public static void WritePageAction(string Address, string PageSource, string message, string trace)
        {
            try
            {

                if (wrActivity == null)
                {
                    wrActivity = new StreamWriter(pathActivity, true);
                }

                // wrActivity.WriteLine(Address + " || " + PageSource + " || " + message + " || " + trace + " || " + "Date Time: " + Serverdatetime.GetIndianTime());

                // if (++intActivityCount == LoggerConstantMode.intActivitySkip)
                //{
                intActivityCount = 0;
                wrActivity.Close();
                wrActivity.Dispose();
                wrActivity = null;
                // }
            }
            catch (Exception ex)
            {
            }
        }

        public static void TimeLogFunction(string title, long t1, string pagename)
        {
            try
            {
                //if (LoggerConstantMode.Logger_APP_MODE == LoggerConstantMode.DebugMode)
                // {
                long t2 = DateTime.Now.Ticks;

                if (HttpContext.Current.Request.Cookies["UserName"] != null)
                {
                    Logger.WriteDebugTimeLog(title + ": " + "MiliSeconds: " + ((t2 - t1) / 10000).ToString() + " || " + "User: " + HttpContext.Current.Request.Cookies["UserName"].ToString(), pagename, HttpContext.Current.Request.UserHostAddress);
                }
                //}
            }
            catch (Exception ex)
            {
            }
        }


        private static System.IO.StreamWriter wrTime = null;
        private static string pathTime = HttpContext.Current.Server.MapPath("~") + "\\Log\\DebugTimeLog.txt";
        //private static int intTimeSkip = LoggerConstantMode.intTimeSkip; //10;
        private static int intTimeCount = 0;

        /// <summary>
        /// Debug Time Logs
        /// </summary>
        /// <param name="log"></param>
        /// <param name="PageSource"></param>
        /// <param name="Address"></param>
        public static void WriteDebugTimeLog(string log, string PageSource, string Address)
        {
            try
            {
                if (wrTime == null)
                {
                    wrTime = new StreamWriter(pathTime, true);
                }

                // wrTime.WriteLine(log + " || " + PageSource + " || " + Address + " || " + "Date Time: " + Serverdatetime.GetIndianTime());

                // if (++intTimeCount == LoggerConstantMode.intTimeSkip)
                // {
                intTimeCount = 0;
                wrTime.Close();
                wrTime.Dispose();
                wrTime = null;
                // }
            }
            catch (Exception ex)
            {
            }
        }


        public static void WriteEntry(string log)
        {
            try
            {
                string path = HttpContext.Current.Server.MapPath("Log") + "\\log.txt";
                StreamWriter wr = new StreamWriter(path, true);
                wr.WriteLine("\r\n");
                wr.WriteLine(log);
                wr.WriteLine("\r\n");
                wr.Close();
                wr.Dispose();
            }
            catch (Exception ex)
            {
            }
        }

        public static void WriteClientCriticalLog(string log)
        {
            try
            {
                EmailClass email = new EmailClass();
                string path = HttpContext.Current.Server.MapPath(".") + "\\Log\\ClientCriticalLog.txt";
                StreamWriter sw = new StreamWriter(path, true);
                sw.WriteLine("Time:" + getIndiantime() + "\nLog:" + log + "\n\n");
                email.SendEmail("Critical Bug Complaint Management Client", "Time:" + getIndiantime() + "\nLog:" + log + "\n\n", System.Web.Mail.MailFormat.Text, "", "nileshrai78@gmail.com");
                sw.Close();
                sw.Dispose();
            }
            catch (Exception err)
            {
                string errmsg = err.StackTrace.ToString();
            }
        }

        public static void WriteLog(string log)
        {
            try
            {
                EmailClass email = new EmailClass();
                string path = HttpContext.Current.Server.MapPath(".") + "\\Log\\Log.txt";
                StreamWriter sw = new StreamWriter(path, true);
                sw.WriteLine("Time:" + getIndiantime() + "\n" + log + "\n\n");

                sw.Close();
                sw.Dispose();
            }
            catch (Exception err)
            {
                string errmsg = err.StackTrace.ToString();
            }
        }

    }
}