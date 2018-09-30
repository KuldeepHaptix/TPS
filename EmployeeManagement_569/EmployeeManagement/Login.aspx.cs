using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmployeeManagement
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Request.Cookies.AllKeys.Contains("LoginCookies") && Request.Cookies["LoginCookies"] != null)
                    {
                        Response.Redirect("~/Search_Employee.aspx", false);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("Login 27: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }
        MySQLDB objmysqldb = new MySQLDB();
        protected void Login_Click(object sender, EventArgs e)
        {

            objmysqldb.ConnectToDatabase();
            lblmsg.Text = "";
            try
            {
                string username = tb_UserName.Value.ToLower().ToString().Trim();
                string pwd = tb_password.Value.ToString().Trim();


                if (username != "" && pwd != "")
                {
                    DataTable dt = new DataTable();
                    dt = objmysqldb.GetData("select User_id,User_Name,User_Password,User_Type from user_account where User_Name='" + username + "' and User_Password='" + pwd + "' and IsDelete=0");
                    //Response.Write("select User_id,User_Name,User_Password,User_Type from user_account where User_Name='" + username + "' and User_Password='" + pwd + "' and IsDelete=0");
                    if (dt.Rows.Count > 0)
                    {
                      
                        if (pwd.ToString().Equals(dt.Rows[0]["User_Password"].ToString()))
                        {
                            HttpCookie cookie = new HttpCookie("LoginCookies");
                            cookie["UserId"] = dt.Rows[0]["User_id"].ToString();
                            cookie["User_Name"] = dt.Rows[0]["User_Name"].ToString();
                            cookie["User_Type"] = dt.Rows[0]["User_Type"].ToString();
                            //if (!dt.Rows[0]["module_id"].ToString().Equals(""))
                            //{
                            //    cookie["modules"] = dt.Rows[0]["module_id"].ToString();
                            //}
                            cookie.Expires.Add(new TimeSpan(0, 1, 0));
                            Response.Cookies.Add(cookie);
                            if (cookie != null)
                            {
                                int usertid = 0;
                                int.TryParse(dt.Rows[0]["User_id"].ToString(), out usertid);
                                //string moduleid = dt.Rows[0]["module_id"].ToString();
                                //string[] modules = moduleid.Split(',');
                                //if(usertid>2)
                                //{
                                //    foreach (string mid in modules)
                                //    {
                                //        if (mid.Equals("1"))
                                //        {
                                //            Response.Redirect("~/Search_Employee.aspx", false);
                                //            break;
                                //        }
                                //        else
                                //        {
                                //            Response.Redirect("~/ManageDepartment.aspx", false);
                                //        }
                                //    }
                                //}
                                //else
                                //{
                                //    Response.Redirect("~/Search_Employee.aspx", false);
                                //}                              
                                Response.Redirect("~/Search_Employee.aspx", false);
                            }
                        }
                        else
                        {
                            lblmsg.Text = "Wrong Login Details.";
                            lblmsg.Visible = true;
                        }
                    }
                    else
                    {
                        lblmsg.Text = "Wrong Login Details.";
                        lblmsg.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("Login 78: exception:" + ex.Message + "::::::::" + ex.StackTrace);
                Logger.WriteCriticalLog("Login 78: exception:" + ex.Message + "::::::::" + ex.StackTrace);
                lblmsg.Text = "Please Try Again.";
                lblmsg.Visible = true;
            }
            finally
            {
                objmysqldb.CloseSQlConnection();
                objmysqldb.disposeConnectionObj();
            }
        }
    }
}