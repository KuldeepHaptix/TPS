using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using RestSharp;

namespace EmployeeManagement
{

    public partial class Assign_Enginerr : System.Web.UI.Page
    {
        int user_id = 0;
        int comp_id = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.Cookies.AllKeys.Contains("LoginCookies") && Request.Cookies["LoginCookies"] != null)
                {
                    int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                    Label header = Master.FindControl("lbl_pageHeader") as Label;
                    header.Text = "DateWise Time Config For Emp";
                    // string date = Request.QueryString["date"].ToString();

                    //string date = DateTime.Now.ToShortDateString;
                    //string emp_id = Request.QueryString["Emp_id"].ToString();
                    string emp_id = "1";
                    if (!Page.IsPostBack)
                    {
                        ////lblDate1.Text = date;
                        getAllComplaint();
                        GetEnginerrDetails();
                        //// BindData(emp_id);
                        ////getComplaintDetails(comp_id);
                        ////bindMonth(date);
                        ////prevPage = Request.Url.ToString();
                        //getAllComplaint();
                        //GetEnginerrDetails();
                    }
                    //getAllComplaint();
                    //GetEnginerrDetails();
                }
                else
                {
                    Response.Redirect("~/login.aspx", false);
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/login.aspx", false);
            }

        }

        DataTable dtStatus = new DataTable();
        public DataTable getAllComplaint()
        {
            String strConnString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(strConnString);
            try
            {
                SqlCommand cmd = new SqlCommand();
                //cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "select [comp_id],[comp_number],[resi_add],[customer_name],[mobile_no],[alternate_no] from [dbo].[Complaint_details] where [status_id]=1 and [IsDelete]=0;";
                cmd.Connection = con;
                con.Open();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dtStatus);
                if (dtStatus != null && dtStatus.Rows.Count > 0)
                {
                    dlComplaint.DataValueField = "comp_id";
                    dlComplaint.DataTextField = "comp_number";
                    dlComplaint.DataSource = dtStatus;

                    dlComplaint.DataBind();
                    dlComplaint.Items.Insert(0, new ListItem("Select Compalint", "-1"));
                }
                else
                {
                    dlComplaint.Items.Insert(0, new ListItem("Select Compalint", "-1"));

                }

            }
            catch (Exception ex)
            {


            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            return dtStatus;
        }


        public void GetEnginerrDetails()
        {
            String strConnString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(strConnString);
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = " select [eng_id],[Name] ,[mobile_no] from [dbo].[Engineer-Details];";
                cmd.Connection = con;
                con.Open();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dtEng = new DataTable();
                adp.Fill(dtEng);
                if (dtEng != null && dtEng.Rows.Count > 0)
                {

                    GrdEmpList.DataSource = dtEng;
                    GrdEmpList.DataBind();
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
                con.Dispose();
            }

        }

        public void GetCOmplaintDetailsById(int Comp_id)
        {
            try
            {
                DataTable dtAllCOmplaint = getAllComplaint();
                if (dtAllCOmplaint != null && dtAllCOmplaint.Rows.Count > 0)
                {
                    DataRow[] drComp = dtAllCOmplaint.Select("comp_id=" + comp_id);
                    if (drComp.Length > 0)
                    {

                        DataTable dtComp = drComp.CopyToDataTable();
                        for (int j = 0; j < dtComp.Rows.Count; j++)
                        {
                            string Contact_no = dtComp.Rows[j]["mobile_no"].ToString();
                            //if (Contact_no.Equals(""))
                            //{
                                Contact_no = dtComp.Rows[j]["alternate_no"].ToString();

                                if (!Contact_no.Equals(""))
                                {
                                    txtExtremeEarly.Text = Contact_no;
                                }

                            //}
                            string name = dtComp.Rows[j]["customer_name"].ToString();
                            if (name != null && !name.Equals(""))
                            {
                                txtExtremeLate.Text = name;

                            }
                            string resi_add = dtComp.Rows[j]["resi_add"].ToString();
                            if (resi_add != null && !resi_add.Equals(""))
                            {

                                txtoutTime.Text = resi_add;
                            }
                        }


                    }


                }
            }
            catch (Exception Ex)
            {


            }


        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            String strConnString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(strConnString);
            try
            {

                string Eng_ids = "";
                string Eng_Contact_no="";
                string empname = "";
                foreach (GridViewRow gvrow in GrdEmpList.Rows)
                {

                    CheckBox chk = (CheckBox)gvrow.FindControl("chkSelect");

                    if (chk.Checked && chk != null)
                    {
                        Label emp = (Label)gvrow.FindControl("lblemp_id");
                        Label EngContactNo=(Label)gvrow.FindControl("lblengcontact");
                        Label ename = (Label)gvrow.FindControl("lblempName");
                        Eng_ids += emp.Text.ToString() + ",";
                         Eng_Contact_no=Eng_Contact_no.ToString();
                        empname = ename.Text.ToString();

                    }
                   

                }
                Eng_ids = Eng_ids.TrimEnd(',');

                string cust_Contact_no=txtExtremeEarly.Text.ToString();
                string[] arrempid = Eng_ids.Split(',');
                if (arrempid != null && !arrempid.Equals("") && arrempid.Length > 0)
                {
                    if (arrempid.Length==1)
                    {
                        int comp_id =0;
                        string Comp_no=dlComplaint.SelectedValue.ToString();
                        int.TryParse(dlComplaint.SelectedValue.ToString(),out comp_id);
                        SqlCommand cmdInsert = new SqlCommand();
                        cmdInsert.CommandType = CommandType.StoredProcedure;
                        cmdInsert.CommandText = "spSaveCompAssignDetails";
                        cmdInsert.Connection = con;
                        con.Open();
                        cmdInsert.Parameters.AddWithValue("@eng_id",Eng_ids);
                        //cmdInsert.Parameters.AddWithValue("@assigndate",);
                        cmdInsert.Parameters.AddWithValue("@eng_contact_no",Eng_Contact_no);
                        cmdInsert.Parameters.AddWithValue("@comp_no",Comp_no);
                        cmdInsert.Parameters.AddWithValue("@cust_contact_no",cust_Contact_no);
                         cmdInsert.Parameters.AddWithValue("@comp_id",comp_id);
                        cmdInsert.Parameters.AddWithValue("@DOC",DateTime.Now.Ticks.ToString());
                          cmdInsert.Parameters.AddWithValue("@DOM",DateTime.Now.Ticks.ToString());
                         cmdInsert.Parameters.AddWithValue("@IsDelete",0);
                          cmdInsert.Parameters.AddWithValue("@user_id",user_id);
                        int Success = cmdInsert.ExecuteNonQuery();
                if (Success > 0)
                {
                  ltrErr.Text = "Engineer Assigned Successfully......";
                            sendtoclient(Comp_no, cust_Contact_no, Eng_Contact_no, empname);
                        }    
                    
                    }
                    else
                    {

                        ltrErr.Text = "Select Oney One Enginerr";
                    }
                
                }
            }
            catch(Exception ex)
            {

            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }
        public void sendtoclient(string Comp_no, string cust_Contact_no, string Eng_Contact_no, string empname)
        {
            var client = new RestClient("http://api.msg91.com/api/v2/sendsms?campaign=&response=&afterminutes=&schtime=&unicode=&flash=&message=&encrypt=&authkey=&mobiles=&route=&sender=&country=91");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
             request.AddHeader("authkey", "241515A8p8I240V25bb9896e");
            request.AddParameter("application/json", "{ \"sender\": \"TPS\", \"route\": \"4\", \"country\": \"91\", \"sms\": [ { \"message\": \"Your Complaint has been book On date:"+ DateTime.Now.Ticks.ToString() + " and Complaint #:"+ Comp_no + " Assigned to"+ empname + ""+ Eng_Contact_no + "  FROM TPS\", \"to\": [ \""+ cust_Contact_no + "\" ] }, { \"message\": \"this is employee test \", \"to\": [ \""+ Eng_Contact_no + "\" ] } ] }", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Response.Write("status"+response.Content);
        }

        protected void dlComplaint_SelectedIndexChanged(object sender, EventArgs e)
        {
            int.TryParse(dlComplaint.SelectedValue.ToString(), out comp_id);
            if (comp_id > 0)
            {
                GetCOmplaintDetailsById(comp_id);
                
            }
            //getComplaintDetails(comp_id);

        }

        
    }
}