using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmployeeManagement
{
    public partial class Registration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            
        }
        public void cleardata()
        {
            txtconfpwd.Text = " ";
            txtemail.Text = "";
            txtpwd.Text = "";
            txtname.Text="";

        }
        private bool ValidateEmail(string emailId)
        {
            bool Success;
            string email = emailId;
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (match.Success)
            {
                lblmsg.Text = email + " is Valid Email Address";
                Success = true;
            }
            else
            {
                lblmsg.Text = email + " is Invalid Email Address";
                Success = false;
            }
            return Success;
        }
 
        protected void btnSave_Click(object sender, EventArgs e)
        {
            String strConnString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(strConnString);
            try
            {
                string user_nm=txtname.Text.ToString();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "select user_nm from user_master where user_nm="+user_nm+"";
                cmd.Connection = con;
                con.Open();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dtUserNm = new DataTable();
                adp.Fill(dtUserNm);
                if (dtUserNm != null && dtUserNm.Rows.Count > 0)
                {
                    lblmsg.Text = "UserName Is Already Exists:"+dtUserNm.Rows[0]["User_nm"].ToString();                   
                
                }
                string pwd = txtpwd.Text.ToString();
                string ConfPwd = txtconfpwd.Text.ToString();
                if (pwd.Equals(ConfPwd))
                {

                    lblmsg.Text = "Passwod do not match..";
                }
                else
                {
                    string gender = "";
                    if (rdbmale.Checked)
                    {
                        gender = "Male";
                    }
                    else
                    {

                        gender = "Female";
                    }
                    string mail = txtemail.Text.ToString();
                  bool yes=  ValidateEmail(mail);
                  if (yes.Equals("true"))
                  {
                      string Contact_no = txtcnno.Text.ToString();
                      SqlCommand cmdInsert = new SqlCommand();
                      cmdInsert.CommandType = CommandType.StoredProcedure;
                      cmdInsert.CommandText = "spsaveUserDetails ";
                      //cmdInsert.CommandText = CommandType.StoredProcedure();
                      cmdInsert.Connection = con;
                      con.Open();
                      cmdInsert.Parameters.AddWithValue("@username", user_nm);
                      cmdInsert.Parameters.AddWithValue("@passowrd", pwd);
                      cmdInsert.Parameters.AddWithValue("@gender", gender);
                      cmdInsert.Parameters.AddWithValue("@contact_no", Contact_no);
                      int y = cmdInsert.ExecuteNonQuery();
                      if(y > 0)
                      {

                          lblmsg.Text= "Successfully Registered....";
                      }
                      else
                      {
                          lblmsg.Text = "Error While Saving Data...";


                      }
                  }
                  else
                  { 
                  
                            
                  
                  }
                    
                }
            }
            catch (Exception ex)
            {

            }
        }

    }
}