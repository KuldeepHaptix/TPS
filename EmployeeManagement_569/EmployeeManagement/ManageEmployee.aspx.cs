using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data.SqlClient;


namespace EmployeeManagement
{
    public partial class ManageEmployee : System.Web.UI.Page
    {
        MySQLDB objmysqldb = new MySQLDB();
        int user_id = 0;
        int Comp_id = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
              
                try
                {
                    if (Request.Cookies.AllKeys.Contains("LoginCookies") && Request.Cookies["LoginCookies"] != null)
                    {
                        int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
                        Label header = Master.FindControl("lbl_pageHeader") as Label;
                        string temp = Request.QueryString.ToString();
                        if (Request.QueryString.Count > 0)
                        {
                        
                            string emp_id = Request.QueryString["Comp"].ToString();
                            int.TryParse(emp_id.ToString(), out Comp_id);
                            if (Comp_id > 0)
                            {
                                GetComplaintDetails();
                            }
                        }
                        if (Comp_id == 0)
                        {
                            header.Text = "Add Complaint Details";

                        }
                        else
                        {
                            btnAddComailnt.Text = "Update Complaint";
                            header.Text = "Update Complaint Details";
                        }

                    }
                    else
                    {
                        Response.Redirect("~/login.aspx", false);
                    }
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/login.aspx", false);
                    Logger.WriteCriticalLog("ManageEmployee 87: exception:" + ex.Message + "::::::::" + ex.StackTrace);
                }

                if (!IsPostBack)
                {
                    bindStatus();
                    if (Comp_id > 0)
                    {
                        GetComplaintDetails();
                        //Emp_idHidden.Value = (string)ViewState["Employee"];
                        //GetEmployeeData();

                    }
                }
                //ViewState["Employee"] = Comp_id.ToString();
                
                //    else
                //    {
                //        Emp_idHidden.Value = (string)ViewState["Employee"];
                //        DateTime dtt = MySQLDB.GetIndianTime();
                //        string date = (dtt.Day.ToString().Length == 1 ? "0" + dtt.Day.ToString() : dtt.Day.ToString()) +
                //            "/" +
                //            (dtt.Month.ToString().Length == 1 ? "0" + dtt.Month.ToString() : dtt.Month.ToString())
                //            + "/" +
                //            dtt.Year;
                //        datepickerdoj.Text = date;//DateTime.Parse(MySQLDB.getIndiantime()).ToShortDateString().ToString();
                //        datepickerdob.Text = date;//DateTime.Parse(MySQLDB.getIndiantime()).ToShortDateString().ToString();
                //    }

                //    GetFamilyDetails();
                //    GetQualificationDetails();
                //    GetExperienceDetails();
                // }
            }
            catch (Exception ex)
            {
                Logger.WriteCriticalLog("ManageEmployee 87: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            }
        }


        //private void GetEmployeeData()
        //{
        //    DataTable dtEmpList = new DataTable();
        //    try
        //    {


        //        objmysqldb.ConnectToDatabase();
        //        int emp_id = 0;
        //        int.TryParse(Emp_idHidden.Value.ToString(), out emp_id);
        //        try
        //        {
        //            dtEmpList = objmysqldb.GetData("select * from employee_master where EmpId=" + emp_id + " and IsDelete=0;");
        //            if (dtEmpList.Rows[0]["Marriage_Date"].ToString().Equals(""))
        //            {

        //            }
        //            else
        //            {

        //                string[] arrdob = dtEmpList.Rows[0]["Marriage_Date"].ToString().Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

        //                if (arrdob.Length == 3)
        //                {
        //                    string date = ((arrdob[0]).ToString().Length == 1 ? "0" + (arrdob[0]).ToString() : (arrdob[0]).ToString()) + "/" + ((arrdob[1]).ToString().Length == 1 ? "0" + (arrdob[1]).ToString() : (arrdob[1]).ToString()) + "/" + ((arrdob[2]).ToString());
        //                    txtmarrigeDate.Text = date;
        //                }
        //                else
        //                {
        //                    lttE.Text = "Marriage Date is not in proper format.";
        //                    return;
        //                }

        //            }
        //            if (dtEmpList.Rows[0]["Resignation_Date"].ToString().Equals(""))
        //            {

        //            }
        //            else
        //            {
        //                string[] arrdob = dtEmpList.Rows[0]["Resignation_Date"].ToString().Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

        //                if (arrdob.Length == 3)
        //                {
        //                    string date = ((arrdob[0]).ToString().Length == 1 ? "0" + (arrdob[0]).ToString() : (arrdob[0]).ToString()) + "/" + ((arrdob[1]).ToString().Length == 1 ? "0" + (arrdob[1]).ToString() : (arrdob[1]).ToString()) + "/" + ((arrdob[2]).ToString());
        //                    txtresiDate.Text = date;
        //                }
        //                else
        //                {
        //                    lttE.Text = "Resignation Date is not in proper format.";
        //                    return;
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {

        //        }
        //        if (dtEmpList != null && dtEmpList.Rows.Count > 0)
        //        {
        //            txtlname.Text = dtEmpList.Rows[0]["EmpLastName"].ToString();
        //            txtfname.Text = dtEmpList.Rows[0]["EmpFirstName"].ToString();
        //            txtmname.Text = dtEmpList.Rows[0]["EmpMiddleName"].ToString();
        //            txtfpId.Text = dtEmpList.Rows[0]["FP_Id"].ToString();
        //            txtphone.Text = dtEmpList.Rows[0]["EmpPhone"].ToString();
        //            datepickerdob.Text = dtEmpList.Rows[0]["EmpDOB"].ToString();
        //            datepickerdoj.Text = dtEmpList.Rows[0]["EmpDOJ"].ToString();
        //            txtEmail.Text = dtEmpList.Rows[0]["EmpEmail"].ToString();

        //            string gender = dtEmpList.Rows[0]["EmpGender"].ToString();
        //            if (gender.ToLower().Equals("male"))
        //            {
        //                rdbmale.Checked = true;
        //                //rdbFemale.Checked = false;
        //            }
        //            else
        //            {
        //                rdbFemale.Checked = true;
        //                //rdbmale.Checked = false ;
        //            }
        //            txtnational.Text = dtEmpList.Rows[0]["Nationality"].ToString();
        //            txtresiAdd.Text = dtEmpList.Rows[0]["Emp_Resi_Adderss"].ToString();
        //            txtresicity.Text = dtEmpList.Rows[0]["Emp_Resi_City"].ToString();
        //            txtRstate.Text = dtEmpList.Rows[0]["Emp_Resi_State"].ToString();
        //            txtRCountry.Text = dtEmpList.Rows[0]["Emp_Resi_Country"].ToString();
        //            txtRpin.Text = dtEmpList.Rows[0]["Resi_Pincode"].ToString();
        //            txtperAdd.Text = dtEmpList.Rows[0]["Emp_Per_Adderss"].ToString();
        //            txtpercity.Text = dtEmpList.Rows[0]["Emp_per_City"].ToString();
        //            txtPState.Text = dtEmpList.Rows[0]["Emp_per_State"].ToString();
        //            txtPCountry.Text = dtEmpList.Rows[0]["Emp_per_Country"].ToString();
        //            txtPpin.Text = dtEmpList.Rows[0]["Per_Pincode"].ToString();

        //            txtblood.Text = dtEmpList.Rows[0]["Emp_Blood_Group"].ToString();
        //            //   txtmarrigeDate.Text = dtEmpList.Rows[0]["Marriage_Date"].ToString();
        //            txtheight.Text = dtEmpList.Rows[0]["Height"].ToString();
        //            txtweight.Text = dtEmpList.Rows[0]["Weight"].ToString();
        //            txtahdar.Text = dtEmpList.Rows[0]["AadharNo"].ToString();
        //            txtpf.Text = dtEmpList.Rows[0]["EmpPFNo"].ToString();
        //            txtpancard.Text = dtEmpList.Rows[0]["EmpPanNumber"].ToString();
        //            txtpayscale.Text = dtEmpList.Rows[0]["PayScale"].ToString();
        //            txtNotice.Text = dtEmpList.Rows[0]["Notice_Peroid"].ToString();
        //            //   txtresiDate.Text = dtEmpList.Rows[0]["Resignation_Date"].ToString();
        //            txtdeposite.Text = dtEmpList.Rows[0]["Deposit"].ToString();

        //            string cast = "";
        //            cast = dtEmpList.Rows[0]["EmpCaste"].ToString();
        //            //ddlcaste.SelectedIndex = ddlcaste.Items.IndexOf(ddlcaste.Items.FindByValue(cast));


        //            //string cate = "";
        //            //cate = dtEmpList.Rows[0]["EmpCategory"].ToString();
        //            //ddlcategory.SelectedIndex = ddlcategory.Items.IndexOf(ddlcategory.Items.FindByValue(cate));


        //            //string reli = "";
        //            //reli = dtEmpList.Rows[0]["EmpReligion"].ToString();
        //            //ddlreligion.SelectedIndex = ddlreligion.Items.IndexOf(ddlreligion.Items.FindByValue(reli));

        //            //string dep = "";
        //            //dep = dtEmpList.Rows[0]["EmpDeptID"].ToString();
        //            //ddldepart.SelectedIndex = ddldepart.Items.IndexOf(ddldepart.Items.FindByValue(dep));

        //            //string desi = "";
        //            //desi = dtEmpList.Rows[0]["EmpDesignId"].ToString();
        //            //ddldesignation.SelectedIndex = ddldesignation.Items.IndexOf(ddldesignation.Items.FindByValue(desi));

        //            string marStatus = dtEmpList.Rows[0]["Maritial_Status"].ToString();
        //            ddlMaritial.SelectedIndex = 0;
        //            if (marStatus.ToLower().Equals("unmarried"))
        //            {
        //                ddlMaritial.SelectedIndex = 1;
        //            }
        //            else if (marStatus.ToLower().Equals("married"))
        //            {
        //                ddlMaritial.SelectedIndex = 2;
        //            }

        //            string uploadFolderPath = "~/Emp_Signature/";
        //            string pathSign = HttpContext.Current.Server.MapPath(uploadFolderPath) + "\\" + dtEmpList.Rows[0]["Emp_signature"].ToString();
        //            //if (File.Exists(pathSign))
        //            //{
        //            //    string test = "http://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port + "//Emp_Signature";
        //            //    Image2.ImageUrl = test + "//" + dtEmpList.Rows[0]["Emp_signature"].ToString();
        //            //}
        //            //else
        //            //{
        //            //    string test = "http://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port;
        //            //    Image2.ImageUrl = test + "//sign.jpg";
        //            //}

        //            //uploadFolderPath = "~/Emp_Image/";

        //            //pathSign = HttpContext.Current.Server.MapPath(uploadFolderPath) + "\\" + dtEmpList.Rows[0]["Emp_photo"].ToString();
        //            //if (File.Exists(pathSign))
        //            ////if (!dtEmpList.Rows[0]["Emp_photo"].ToString().Equals(""))
        //            //{
        //            //    string test = "http://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port + "//Emp_Image";
        //            //    Image1.ImageUrl = test + "//" + dtEmpList.Rows[0]["Emp_photo"].ToString();

        //            //    //btnphoto.Style.Add("display", "block");
        //            //    //filePhotos.Style.Add("display", "none");
        //            //}
        //            //else
        //            //{
        //            //    string test = "http://" + HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port;
        //            //    Image1.ImageUrl = test + "//photo.jpg";
        //            //}

        //            //Resign.Text = dtEmpList.Rows[0]["Resignation_Proof"].ToString();
        //            //AppointMent.Text = dtEmpList.Rows[0]["Appointmnet_Letter"].ToString();
        //            //Offer.Text = dtEmpList.Rows[0]["Offer_Letter"].ToString();
        //            if (!dtEmpList.Rows[0]["Resignation_Proof"].ToString().Equals(""))
        //            {
        //                resignfeild.Value = dtEmpList.Rows[0]["Resignation_Proof"].ToString();
        //                Resign.Text = "View";
        //            }
        //            else
        //            {
        //                resignfeild.Value = "";
        //                Resign.Text = "";
        //            }
        //            if (!dtEmpList.Rows[0]["Appointmnet_Letter"].ToString().Equals(""))
        //            {
        //                AppointMentfield.Value = dtEmpList.Rows[0]["Appointmnet_Letter"].ToString();
        //                AppointMent.Text = "View";
        //            }
        //            else
        //            {
        //                AppointMentfield.Value = "";
        //                AppointMent.Text = "";
        //            }
        //            if (!dtEmpList.Rows[0]["Offer_Letter"].ToString().Equals(""))
        //            {
        //                offerField.Value = dtEmpList.Rows[0]["Offer_Letter"].ToString();
        //                Offer.Text = "View";
        //            }
        //            else
        //            {
        //                offerField.Value = "";
        //                Offer.Text = "";
        //            }
        //        }
        //        else
        //        {
        //            ltrper.Text = "Record does not exist";
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.WriteCriticalLog("ManageEmployee 303: exception:" + ex.Message + "::::::::" + ex.StackTrace);
        //    }

        //    finally
        //    {
        //        objmysqldb.disposeConnectionObj();
        //    }

        //}

        private void bindStatus()
        {
            String strConnString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(strConnString);
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetAllStatus";
                cmd.Connection = con;
                con.Open();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dtStatus = new DataTable();
                adp.Fill(dtStatus);
                if (dtStatus != null && dtStatus.Rows.Count > 0)
                {

                    ddlStatus.DataSource = dtStatus;
                    ddlStatus.DataTextField = "Status";
                    ddlStatus.DataValueField = "Status_id";
                    ddlStatus.DataBind();
                    ddlStatus.Items.Insert(0, new ListItem("Select Status", "-1"));


                }
            }
            catch (Exception Ex)
            {

            }
            finally
            {
                con.Close();

            }

        }
        //private void BindComboBox()
        //{
        //    try
        //    {
        //        objmysqldb.ConnectToDatabase();
        //        DataTable dtCaste = objmysqldb.GetData("select caste_id,caste_name from caste_master where IsDelete=0");

        //        ddlcaste.DataSource = dtCaste;
        //        ddlcaste.DataTextField = "caste_name";
        //        ddlcaste.DataValueField = "caste_id";
        //        ddlcaste.DataBind();
        //        ddlcaste.Items.Insert(0, new ListItem("Select Caste", "-2"));

        //        DataTable dtcategory = objmysqldb.GetData("select category_id,category_name from category_master where IsDelete=0");

        //        ddlcategory.DataSource = dtcategory;
        //        ddlcategory.DataTextField = "category_name";
        //        ddlcategory.DataValueField = "category_id";
        //        ddlcategory.DataBind();
        //        ddlcategory.Items.Insert(0, new ListItem("Select Category", "-2"));

        //        DataTable dtdepartment = objmysqldb.GetData("select department_id,Department_Name from Department_master where IsDelete=0");

        //        ddldepart.DataSource = dtdepartment;
        //        ddldepart.DataTextField = "Department_Name";
        //        ddldepart.DataValueField = "department_id";
        //        ddldepart.DataBind();
        //        ddldepart.Items.Insert(0, new ListItem("Select Department", "-2"));

        //        DataTable dtdesignation = objmysqldb.GetData("select designation_id,Designation_Name from designation_master where IsDelete=0");

        //        ddldesignation.DataSource = dtdesignation;
        //        ddldesignation.DataTextField = "Designation_Name";
        //        ddldesignation.DataValueField = "designation_id";
        //        ddldesignation.DataBind();
        //        ddldesignation.Items.Insert(0, new ListItem("Select Designation", "-2"));


        //        DataTable dtreligion = objmysqldb.GetData("select religion_id,religion_name from religion_master where IsDelete=0");

        //        ddlreligion.DataSource = dtreligion;
        //        ddlreligion.DataTextField = "religion_name";
        //        ddlreligion.DataValueField = "religion_id";
        //        ddlreligion.DataBind();
        //        ddlreligion.Items.Insert(0, new ListItem("Select Religion", "-2"));

        //        DataTable dtMaritial_Status = new DataTable();
        //        dtMaritial_Status.Columns.Add("Status_id");
        //        dtMaritial_Status.Columns.Add("Status");
        //        ddlMaritial.DataSource = dtMaritial_Status;
        //        ddlMaritial.DataTextField = "Status";
        //        ddlMaritial.DataValueField = "Status_id";
        //        ddlMaritial.DataBind();
        //        ddlMaritial.Items.Insert(0, new ListItem("Select Maritial Status", "-1"));
        //        ddlMaritial.Items.Insert(1, new ListItem("UnMarried", "1"));
        //        ddlMaritial.Items.Insert(2, new ListItem("Married", "2"));

        //        DataTable Family_Relation = new DataTable();
        //        Family_Relation.Columns.Add("id");
        //        Family_Relation.Columns.Add("Relation");
        //        ddlrelation.DataSource = Family_Relation;
        //        ddlrelation.DataTextField = "Relation";
        //        ddlrelation.DataValueField = "id";
        //        ddlrelation.DataBind();
        //        ddlrelation.Items.Insert(0, new ListItem("Select Relation", "-2"));
        //        ddlrelation.Items.Insert(1, new ListItem("Father", "1"));
        //        ddlrelation.Items.Insert(2, new ListItem("Mother", "2"));
        //        ddlrelation.Items.Insert(3, new ListItem("Husband", "3"));
        //        ddlrelation.Items.Insert(4, new ListItem("Wife", "4"));
        //        ddlrelation.Items.Insert(5, new ListItem("Sister", "5"));
        //        ddlrelation.Items.Insert(6, new ListItem("Brother", "6"));
        //        ddlrelation.Items.Insert(7, new ListItem("Son", "7"));
        //        ddlrelation.Items.Insert(8, new ListItem("Daughter", "8"));


        //        DataTable QualificationType = new DataTable();
        //        QualificationType.Columns.Add("id");
        //        QualificationType.Columns.Add("Type");
        //        ddlqualification.DataSource = QualificationType;
        //        ddlqualification.DataTextField = "Type";
        //        ddlqualification.DataValueField = "id";
        //        ddlqualification.DataBind();
        //        ddlqualification.Items.Insert(0, new ListItem("Select Qualification", "-2"));
        //        ddlqualification.Items.Insert(1, new ListItem("SSC", "1"));
        //        ddlqualification.Items.Insert(2, new ListItem("HSC", "2"));
        //        ddlqualification.Items.Insert(3, new ListItem("Diploma", "3"));
        //        ddlqualification.Items.Insert(4, new ListItem("Under Graduation", "4"));
        //        ddlqualification.Items.Insert(5, new ListItem("Graduation", "5"));
        //        ddlqualification.Items.Insert(6, new ListItem("Post Graduation", "6"));
        //        ddlqualification.Items.Insert(7, new ListItem("PhD", "7"));
        //        ddlqualification.Items.Insert(8, new ListItem("Other", "8"));

        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.WriteCriticalLog("ManageEmployee 408: exception:" + ex.Message + "::::::::" + ex.StackTrace);
        //    }

        //    finally
        //    {
        //        objmysqldb.disposeConnectionObj();
        //    }
        //}


        private void updateComapaintDetails()
        {
            String strConnString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(strConnString);

            try
            {
                if (txtname.Equals(""))
                {
                    //ltrErr.Text = "Customer name is mandatory";
                    return;
                }
                long Comp_date = 0;
                string Comp_dt = "";
                string Comp_Complete_dt = "";
                if (txtcomplaintDate.Text.Equals(""))
                {
                    //ltrErr.Text = "Complaint Date is mandatory";
                    return;
                }
                else
                {
                    string[] arrdob = txtcomplaintDate.Text.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                    if (arrdob.Length == 3)
                    {
                        Comp_dt = ((arrdob[0]).ToString().Length == 1 ? "0" + (arrdob[0]).ToString() : (arrdob[0]).ToString()) + "/" + ((arrdob[1]).ToString().Length == 1 ? "0" + (arrdob[1]).ToString() : (arrdob[1]).ToString()) + "/" + ((arrdob[2]).ToString());
                    }
                    else
                    {
                        //  ltrErr.Text = "Complaint Date is not in proper format.";
                        return;
                    }

                }
                string[] comp_date = txtcomplaintDate.Text.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                if (comp_date.Length == 3)
                {
                    Comp_Complete_dt = ((comp_date[0]).ToString().Length == 1 ? "0" + (comp_date[0]).ToString() : (comp_date[0]).ToString()) + "/" + ((comp_date[1]).ToString().Length == 1 ? "0" + (comp_date[1]).ToString() : (comp_date[1]).ToString()) + "/" + ((comp_date[2]).ToString());
                }
                if (txtresiAdd.Text.Equals(""))
                {
                    //ltrErr.Text = "Complaint Date is not in proper format.";
                    return;
                }
                if (txtdesc.Text.Equals(""))
                {
                    //ltrErr.Text = "Descuition can't be blank.";
                    return;

                }
                int CancelFlag = 0;
                int status = 0;
                int.TryParse(ddlStatus.Items[ddlStatus.SelectedIndex].Value.ToString(), out status);
                if (status < 0)
                {

                    //ltrErr.Text = "Select Valid Status.";
                    return;

                }
                if (status==3)
                {
                    CancelFlag = 1;
                
                }


                SqlCommand cmdUpdate = new SqlCommand("UpdateComplaintDetails", con);
               //cmdUpdate.CommandText = ();
                cmdUpdate.CommandType = CommandType.StoredProcedure;
                cmdUpdate.Parameters.AddWithValue("@comp_id", Comp_id);
                cmdUpdate.Parameters.AddWithValue("@custome_name", txtname.Text.ToString());
                cmdUpdate.Parameters.AddWithValue("@mobile_no", txtcontactno.Text.ToString());
                cmdUpdate.Parameters.AddWithValue("@alternate_no ", txtotherno.Text.ToString());
               // cmdUpdate.Parameters.AddWithValue("@resi_city ",);
                cmdUpdate.Parameters.AddWithValue("@pin_code ", txtpincode.Text.ToString());
                cmdUpdate.Parameters.AddWithValue("@complaint_desc ", txtdesc.Text.ToString());
                //cmdUpdate.Parameters.AddWithValue("@addeby ", user_id);
               // cmdUpdate.Parameters.AddWithValue("@IsDelete ", 0);
               // cmdUpdate.Parameters.AddWithValue("@DOC", DateTime.Now.Ticks);
                cmdUpdate.Parameters.AddWithValue("@DOM ", DateTime.Now.Ticks);
                cmdUpdate.Parameters.AddWithValue("@status_id", status);
                cmdUpdate.Parameters.AddWithValue("@resi_city", txtcity.Text.ToString());
                long comp_date1 = 0;
                long comp_date2 = 0;
                long.TryParse(comp_date.ToString(), out comp_date1);
                long.TryParse(Comp_Complete_dt.ToString(), out comp_date2);
                cmdUpdate.Parameters.AddWithValue("@comp_date", comp_date1);
                cmdUpdate.Parameters.AddWithValue("@IsCancel",CancelFlag);
                cmdUpdate.Parameters.AddWithValue("@comp_complete_date", comp_date2);
           
                //cmdUpdate.Parameters.AddWithValue(" @IsCancel", 0);
                con.Open();
                int Success = cmdUpdate.ExecuteNonQuery();
                if (Success > 0)
                {
                    // ltrErr.Text = "Complaint Updated Successfully......";
                }
                else
                {

                    //ltrErr.Text = "Complaint Updated Successfully......";

                }

            }
            catch (Exception ex)
            {


            }
            finally
            {
                con.Close();
            }

        
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    ltrErr.Text = "";
            //    string strdob = "";
            //    string strdoj = "";
            //    if (!Emp_idHidden.Value.ToString().Equals("0"))
            //    {
            //        upadteGeneralDetails();
            //        return;
            //    }
            //    #region validation
            //    if (txtlname.Text.Equals("") || txtfname.Text.Equals("") || txtlname.Text.Equals(""))
            //    {
            //        ltrErr.Text = "Employee name is mandatory";
            //        return;
            //    }
            //    if (txtphone.Text.Equals("") || txtphone.Text.ToString().Length != 10)
            //    {
            //        ltrErr.Text = "Contact No is mandatory and enter valid Number";
            //        return;
            //    }
            //    if (datepickerdob.Text.Equals(""))
            //    {
            //        ltrErr.Text = "Date oF Birth is mandatory";
            //        return;
            //    }
            //    else
            //    {
            //        string[] arrdob = datepickerdob.Text.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            //        if (arrdob.Length == 3)
            //        {
            //            strdob = ((arrdob[0]).ToString().Length == 1 ? "0" + (arrdob[0]).ToString() : (arrdob[0]).ToString()) + "/" + ((arrdob[1]).ToString().Length == 1 ? "0" + (arrdob[1]).ToString() : (arrdob[1]).ToString()) + "/" + ((arrdob[2]).ToString());
            //        }
            //        else
            //        {
            //            lttE.Text = "Date of Birth is not in proper format.";
            //            return;
            //        }
            //    }
            //    if (datepickerdoj.Text.Equals(""))
            //    {
            //        ltrErr.Text = "Date oF join is mandatory";
            //        return;
            //    }
            //    else
            //    {
            //        string[] arrdoj = datepickerdoj.Text.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            //        if (arrdoj.Length == 3)
            //        {
            //            strdoj = ((arrdoj[0]).ToString().Length == 1 ? "0" + (arrdoj[0]).ToString() : (arrdoj[0]).ToString()) + "/" + ((arrdoj[1]).ToString().Length == 1 ? "0" + (arrdoj[1]).ToString() : (arrdoj[1]).ToString()) + "/" + ((arrdoj[2]).ToString());
            //        }
            //        else
            //        {
            //            lttE.Text = "Date of Joining is not in proper format.";
            //            return;
            //        }
            //    }
            //    if (ddldepart.SelectedIndex <= 0)
            //    {
            //        ltrErr.Text = "Department is mandatory";
            //        return;
            //    }
            //    if (ddldesignation.SelectedIndex <= 0)
            //    {
            //        ltrErr.Text = "Designation is mandatory";
            //        return;
            //    }
            //    long dob = 0;
            //    long doj = 0;
            //    try
            //    {
            //        string[] arrdob = datepickerdob.Text.Split('/');
            //        string[] arrdoj = datepickerdoj.Text.Split('/');

            //        dob = new DateTime(int.Parse(arrdob[2]), int.Parse(arrdob[1]), int.Parse(arrdob[0])).Ticks;
            //        doj = new DateTime(int.Parse(arrdoj[2]), int.Parse(arrdoj[1]), int.Parse(arrdoj[0])).Ticks;
            //    }
            //    catch (Exception aa)
            //    {

            //    }
            //    if (dob == 0)
            //    {
            //        ltrErr.Text = "Please Enter valid Date of birth";
            //        return;
            //    }
            //    if (doj == 0)
            //    {
            //        ltrErr.Text = "Please Enter valid Date of join";
            //        return;
            //    }
            //    long mode = Logger.getIndiantimeDT().Ticks;
            //    if (doj > mode)
            //    {
            //        ltrErr.Text = "Please Enter valid Date of join";
            //        return;
            //    }
            //    if (dob >= mode || dob >= doj)
            //    {
            //        ltrErr.Text = "Please Enter valid Date of birth";
            //        return;
            //    }

            //    #endregion
            //    bool photochange = false;
            //    bool Signchange = false;
            //    string sign_img = "";
            //    string emp_photo = "";

            //    if (chksign.Checked)
            //    {
            //        Signchange = true;
            //    }
            //    else
            //    {
            //        if (empsign.HasFile)
            //        {
            //            string uploadFolderPath = "~/Emp_Signature/";
            //            string filePath = HttpContext.Current.Server.MapPath(uploadFolderPath);
            //            if (!Directory.Exists(filePath))
            //            {
            //                Directory.CreateDirectory(filePath);
            //            }
            //            if (empsign.FileName.ToString().ToLower().Equals("sign.jpg"))
            //            {
            //                Signchange = false;
            //            }
            //            else
            //            {
            //                string ext = System.IO.Path.GetExtension(empsign.FileName.ToString());

            //                string fileName = txtlname.Text.ToString() + "_" + txtfname.Text.ToString() + "_" + txtmname.Text.ToString() + "_" + mode + ext;
            //                sign_img = fileName;
            //                empsign.SaveAs(filePath + "\\" + fileName);
            //                string imgPath;
            //                imgPath = System.IO.Path.Combine(filePath, fileName);
            //                //System.Drawing.Image img = ResizeImage(imgPath, 250, 0, true);
            //                System.Drawing.Image img = System.Drawing.Image.FromFile(imgPath);
            //                Bitmap b = new Bitmap(img);
            //                System.Drawing.Image i = ResizeImage(b, 250, 150, true);
            //                b.Dispose();
            //                img.Dispose();
            //                empsign.SaveAs(imgPath);
            //                //empsign.SaveAs(filePath + "\\" + fileName);
            //                Image2.ImageUrl = "~/Emp_Signature/" + "/" + fileName.ToString();
            //                Signchange = true;
            //            }
            //        }
            //    }
            //    if (chkphoto.Checked)
            //    {
            //        photochange = true;
            //    }
            //    else
            //    {
            //        if (filePhotos.HasFile)
            //        {
            //            string uploadFolderPath = "~/Emp_Image/";
            //            string filePath = HttpContext.Current.Server.MapPath(uploadFolderPath);
            //            if (!Directory.Exists(filePath))
            //            {
            //                Directory.CreateDirectory(filePath);
            //            }
            //            if (filePhotos.FileName.ToString().ToLower().Equals("photo.jpg"))
            //            {
            //                photochange = false;
            //            }
            //            else
            //            {
            //                string ext = System.IO.Path.GetExtension(filePhotos.FileName.ToString());

            //                string fileName = txtlname.Text.ToString() + "_" + txtfname.Text.ToString() + "_" + txtmname.Text.ToString() + "_" + mode + ext;
            //                emp_photo = fileName;
            //                filePhotos.SaveAs(filePath + "\\" + fileName);
            //                string imgPath;
            //                imgPath = System.IO.Path.Combine(filePath, fileName);
            //                //System.Drawing.Image img = ResizeImage(imgPath, 200, 0, true);
            //                System.Drawing.Image img = System.Drawing.Image.FromFile(imgPath);
            //                Bitmap b = new Bitmap(img);
            //                //System.Drawing.Image i = ResizeImage(b, 200, 240, true);
            //                System.Drawing.Image i = ResizeImage1(b, new Size(200, 240));
            //                b.Dispose();
            //                img.Dispose();
            //                //
            //                filePhotos.SaveAs(imgPath);

            //                Image1.ImageUrl = "~/Emp_Image/" + "/" + fileName.ToString();
            //                photochange = true;
            //            }
            //        }
            //    }

            //    #region insert
            //    objmysqldb.ConnectToDatabase();
            //    objmysqldb.OpenSQlConnection();
            //    DataTable dtcontact = objmysqldb.GetData("Select * from employee_master where EmpPhone=" + txtphone.Text + "");
            //    if (dtcontact.Rows.Count > 0 && dtcontact != null)
            //    {
            //        ltrErr.Text = "Contact Number Alredy Exist.";
            //    }
            //    else
            //    {
            //        int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
            //        objmysqldb.AddCommandParameter("Firstname", txtfname.Text.ToString());
            //        objmysqldb.AddCommandParameter("Lastname", txtlname.Text.ToString());
            //        objmysqldb.AddCommandParameter("Middlename", txtmname.Text.ToString());
            //        objmysqldb.AddCommandParameter("Dob", strdob);
            //        objmysqldb.AddCommandParameter("DOJ", strdoj);
            //        objmysqldb.AddCommandParameter("contactNo", txtphone.Text.ToString());
            //        objmysqldb.AddCommandParameter("email", txtEmail.Text.ToString());
            //        if (photochange)
            //        {
            //            objmysqldb.AddCommandParameter("photo", emp_photo.ToString());
            //        }

            //        if (Signchange)
            //        {
            //            objmysqldb.AddCommandParameter("sign", sign_img.ToString());
            //        }

            //        int fingerprint = 0;
            //        int.TryParse(txtfpId.Text, out fingerprint);
            //        string gender = "Female";
            //        if (rdbmale.Checked)
            //        {
            //            gender = "Male";
            //        }
            //        int caste = 0;
            //        int.TryParse(ddlcaste.Items[ddlcaste.SelectedIndex].Value.ToString(), out caste);
            //        if (caste < 1)
            //        {
            //            caste = -1;
            //        }
            //        int cate = 0;
            //        int.TryParse(ddlcategory.Items[ddlcategory.SelectedIndex].Value.ToString(), out cate);
            //        if (cate < 1)
            //        {
            //            cate = -1;
            //        }

            //        int depart = 0;
            //        int.TryParse(ddldepart.Items[ddldepart.SelectedIndex].Value.ToString(), out depart);
            //        if (depart < 1)
            //        {
            //            depart = -1;
            //        }

            //        int desi = 0;
            //        int.TryParse(ddldesignation.Items[ddldesignation.SelectedIndex].Value.ToString(), out desi);
            //        if (desi < 1)
            //        {
            //            desi = -1;
            //        }

            //        string photo = "";
            //        string photovalue = "";
            //        if (photochange)
            //        {
            //            photo = ",Emp_photo";
            //            photovalue = ",?photo";
            //        }
            //        if (Signchange)
            //        {
            //            photo += ",Emp_signature";
            //            photovalue += ",?sign";
            //        }

            //        string query = "Insert into  employee_master (EmpFirstName,EmpLastName,EmpMiddleName,EmpDOB,EmpDOJ,EmpGender,EmpPhone,EmpCaste,EmpCategory,EmpStatusFlag,FP_Id,EmpEmail,EmpDeptID,EmpDesignId " + photo + ",modify_datetime,DOC,UserID,IsDelete,IsUpdate) values (?Firstname,?Lastname,?Middlename,?Dob,?DOJ,'" + gender + "',?contactNo," + caste + "," + cate + ",0,'" + fingerprint.ToString() + "',?email," + depart + "," + desi + " " + photovalue + "," + mode + "," + mode + "," + user_id + ",0,1) ";

            //        int res = objmysqldb.InsertUpdateDeleteData(query);
            //        if (res != 1)
            //        {
            //            ltrErr.Text = "Please Try Again.";

            //            Logger.WriteCriticalLog("ManageEmployee 695 Update error.");
            //        }
            //        else
            //        {
            //            long lastEmpid = objmysqldb.LastNo("employee_master", "EmpId", mode);
            //            ViewState["Employee"] = lastEmpid.ToString();
            //            Emp_idHidden.Value = (string)ViewState["Employee"];
            //            //Emp_idHidden.Value = lastEmpid.ToString();
            //            ltrErr.Text = "Employee Details Save Successfully";
            //            //Response.Redirect("~/ManageEmployee.aspx?Emp=" + Emp_idHidden.Value.ToString() + "", false);
            //            GetEmployeeData();
            //        }
            //    }
            //    #endregion

            //    ltrper.Text = "";
            //    lttF.Text = "";
            //    lttQ.Text = "";
            //    lttE.Text = "";
            //}
            //catch (Exception ex)
            //{
            //    Logger.WriteCriticalLog("ManageEmployee 717 exception:" + ex.Message + "::::::::" + ex.StackTrace);
            //}
            //finally
            //{
            //    objmysqldb.CloseSQlConnection();
            //    objmysqldb.disposeConnectionObj();

            //}



            //private void upadteGeneralDetails()
            //{
            //    try
            //    {
            //        ltrErr.Text = "";
            //        int emp_id = 0;
            //        string strdob = "";
            //        string strdoj = "";
            //        int.TryParse(Emp_idHidden.Value.ToString(), out emp_id);
            //        if (emp_id < 1)
            //        {
            //            return;
            //        }
            //        #region validation
            //        if (txtlname.Text.Equals("") || txtfname.Text.Equals("") || txtlname.Text.Equals(""))
            //        {
            //            ltrErr.Text = "Employee name is mandatory";
            //            return;
            //        }
            //        if (txtphone.Text.Equals("") || txtphone.Text.ToString().Length != 10)
            //        {
            //            ltrErr.Text = "Contact No is mandatory and enter valid Number";
            //            return;
            //        }
            //        if (datepickerdob.Text.Equals(""))
            //        {
            //            ltrErr.Text = "Date oF Birth is mandatory";
            //            return;
            //        }
            //        else
            //        {
            //            string[] arrdob = datepickerdob.Text.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            //            if (arrdob.Length == 3)
            //            {
            //                strdob = ((arrdob[0]).ToString().Length == 1 ? "0" + (arrdob[0]).ToString() : (arrdob[0]).ToString()) + "/" + ((arrdob[1]).ToString().Length == 1 ? "0" + (arrdob[1]).ToString() : (arrdob[1]).ToString()) + "/" + ((arrdob[2]).ToString());
            //            }
            //            else
            //            {
            //                lttE.Text = "Date of Birth is not in proper format.";
            //                return;
            //            }
            //        }
            //        if (datepickerdoj.Text.Equals(""))
            //        {
            //            ltrErr.Text = "Date oF join is mandatory";
            //            return;
            //        }
            //        else
            //        {
            //            string[] arrdoj = datepickerdoj.Text.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            //            if (arrdoj.Length == 3)
            //            {
            //                strdoj = ((arrdoj[0]).ToString().Length == 1 ? "0" + (arrdoj[0]).ToString() : (arrdoj[0]).ToString()) + "/" + ((arrdoj[1]).ToString().Length == 1 ? "0" + (arrdoj[1]).ToString() : (arrdoj[1]).ToString()) + "/" + ((arrdoj[2]).ToString());
            //            }
            //            else
            //            {
            //                lttE.Text = "Date of Joining is not in proper format.";
            //                return;
            //            }
            //        }
            //        if (ddldepart.SelectedIndex <= 0)
            //        {
            //            ltrErr.Text = "Department is mandatory";
            //            return;
            //        }
            //        if (ddldesignation.SelectedIndex <= 0)
            //        {
            //            ltrErr.Text = "Designation is mandatory";
            //            return;
            //        }
            //        long dob = 0;
            //        long doj = 0;
            //        try
            //        {
            //            string[] arrdob = datepickerdob.Text.Split('/');
            //            string[] arrdoj = datepickerdoj.Text.Split('/');

            //            dob = new DateTime(int.Parse(arrdob[2]), int.Parse(arrdob[1]), int.Parse(arrdob[0])).Ticks;
            //            doj = new DateTime(int.Parse(arrdoj[2]), int.Parse(arrdoj[1]), int.Parse(arrdoj[0])).Ticks;
            //        }
            //        catch (Exception aa)
            //        {

            //        }
            //        if (dob == 0)
            //        {
            //            ltrErr.Text = "Please Enter valid Date of birth";
            //            return;
            //        }
            //        if (doj == 0)
            //        {
            //            ltrErr.Text = "Please Enter valid Date of join";
            //            return;
            //        }
            //        long mode = Logger.getIndiantimeDT().Ticks;
            //        if (doj > mode)
            //        {
            //            ltrErr.Text = "Please Enter valid Date of join";
            //            return;
            //        }
            //        if (dob >= mode || dob >= doj)
            //        {
            //            ltrErr.Text = "Please Enter valid Date of birth";
            //            return;
            //        }

            //        #endregion
            //        bool photochange = false;
            //        bool Signchange = false;
            //        string sign_img = "";
            //        string emp_photo = "";

            //        if (chksign.Checked)
            //        {
            //            Signchange = true;
            //        }
            //        else
            //        {
            //            if (empsign.HasFile)
            //            {
            //                string uploadFolderPath = "~/Emp_Signature/";
            //                string filePath = HttpContext.Current.Server.MapPath(uploadFolderPath);
            //                if (!Directory.Exists(filePath))
            //                {
            //                    Directory.CreateDirectory(filePath);
            //                }
            //                if (empsign.FileName.ToString().ToLower().Equals("sign.jpg"))
            //                {
            //                    Signchange = false;
            //                }
            //                else
            //                {
            //                    string ext = System.IO.Path.GetExtension(empsign.FileName.ToString());

            //                    string fileName = txtlname.Text.ToString() + "_" + txtfname.Text.ToString() + "_" + txtmname.Text.ToString() + "_" + mode + ext;
            //                    sign_img = fileName;
            //                    empsign.SaveAs(filePath + "\\" + fileName);
            //                    string imgPath;
            //                    imgPath = System.IO.Path.Combine(filePath, fileName);
            //                    //System.Drawing.Image img = ResizeImage(imgPath, 250, 0, true);
            //                    System.Drawing.Image img = System.Drawing.Image.FromFile(imgPath);
            //                    Bitmap b = new Bitmap(img);
            //                    System.Drawing.Image i = ResizeImage(b, 250, 150, true);
            //                    b.Dispose();
            //                    img.Dispose();
            //                    empsign.SaveAs(imgPath);
            //                    //empsign.SaveAs(filePath + "\\" + fileName);
            //                    Image2.ImageUrl = "~/Emp_Signature/" + "/" + fileName.ToString();
            //                    Signchange = true;
            //                }
            //            }
            //        }
            //        if (chkphoto.Checked)
            //        {
            //            photochange = true;
            //        }
            //        else
            //        {
            //            if (filePhotos.HasFile)
            //            {
            //                string uploadFolderPath = "~/Emp_Image/";
            //                string filePath = HttpContext.Current.Server.MapPath(uploadFolderPath);
            //                if (!Directory.Exists(filePath))
            //                {
            //                    Directory.CreateDirectory(filePath);
            //                }
            //                if (filePhotos.FileName.ToString().ToLower().Equals("photo.jpg"))
            //                {
            //                    photochange = false;
            //                }
            //                else
            //                {
            //                    string ext = System.IO.Path.GetExtension(filePhotos.FileName.ToString());

            //                    string fileName = txtlname.Text.ToString() + "_" + txtfname.Text.ToString() + "_" + txtmname.Text.ToString() + "_" + mode + ext;
            //                    emp_photo = fileName;
            //                    filePhotos.SaveAs(filePath + "\\" + fileName);
            //                    string imgPath;
            //                    imgPath = System.IO.Path.Combine(filePath, fileName);
            //                    //System.Drawing.Image img = ResizeImage(imgPath, 200, 0, true);
            //                    System.Drawing.Image img = System.Drawing.Image.FromFile(imgPath);
            //                    Bitmap b = new Bitmap(img);
            //                    //System.Drawing.Image i = ResizeImage(b, 200, 240, true);
            //                    System.Drawing.Image i = ResizeImage1(b, new Size(200, 240));
            //                    b.Dispose();
            //                    img.Dispose();
            //                    //
            //                    filePhotos.SaveAs(imgPath);

            //                    Image1.ImageUrl = "~/Emp_Image/" + "/" + fileName.ToString();
            //                    photochange = true;
            //                }
            //            }
            //        }
            //        #region insert
            //        objmysqldb.ConnectToDatabase();
            //        DataTable dtcontact = objmysqldb.GetData("Select * from employee_master where EmpPhone=" + txtphone.Text + " and EmpId<>" + emp_id + "");
            //        if (dtcontact.Rows.Count > 0 && dtcontact != null)
            //        {
            //            ltrErr.Text = "Contact Number Alredy Exist.";
            //        }
            //        else
            //        {
            //            objmysqldb.OpenSQlConnection();
            //            int.TryParse(Request.Cookies["LoginCookies"]["UserId"].ToString(), out user_id);
            //            objmysqldb.AddCommandParameter("Firstname", txtfname.Text.ToString());
            //            objmysqldb.AddCommandParameter("Lastname", txtlname.Text.ToString());
            //            objmysqldb.AddCommandParameter("Middlename", txtmname.Text.ToString());
            //            objmysqldb.AddCommandParameter("Dob", strdob);
            //            objmysqldb.AddCommandParameter("DOJ", strdoj);
            //            objmysqldb.AddCommandParameter("contactNo", txtphone.Text.ToString());
            //            objmysqldb.AddCommandParameter("email", txtEmail.Text.ToString());
            //            if (photochange)
            //            {
            //                objmysqldb.AddCommandParameter("photo", emp_photo.ToString());
            //            }
            //            if (Signchange)
            //            {
            //                objmysqldb.AddCommandParameter("sign", sign_img.ToString());
            //            }



            //            int fingerprint = 0;
            //            int.TryParse(txtfpId.Text, out fingerprint);
            //            string gender = "Female";
            //            if (rdbmale.Checked)
            //            {
            //                gender = "Male";
            //            }
            //            int caste = 0;
            //            int.TryParse(ddlcaste.Items[ddlcaste.SelectedIndex].Value.ToString(), out caste);
            //            if (caste < 1)
            //            {
            //                caste = -1;
            //            }
            //            int cate = 0;
            //            int.TryParse(ddlcategory.Items[ddlcategory.SelectedIndex].Value.ToString(), out cate);
            //            if (cate < 1)
            //            {
            //                cate = -1;
            //            }

            //            int depart = 0;
            //            int.TryParse(ddldepart.Items[ddldepart.SelectedIndex].Value.ToString(), out depart);
            //            if (depart < 1)
            //            {
            //                depart = -1;
            //            }

            //            int desi = 0;
            //            int.TryParse(ddldesignation.Items[ddldesignation.SelectedIndex].Value.ToString(), out desi);
            //            if (desi < 1)
            //            {
            //                desi = -1;
            //            }
            //            string photo = "";
            //            if (photochange)
            //            {
            //                photo = ",Emp_photo=?photo";
            //            }
            //            if (Signchange)
            //            {
            //                photo += ",Emp_signature=?sign";
            //            }

            //            string query = "update   employee_master set EmpFirstName=?Firstname,EmpLastName=?Lastname,EmpMiddleName=?Middlename,EmpDOB=?Dob,EmpDOJ=?DOJ,EmpGender='" + gender + "',EmpPhone=?contactNo,EmpCaste=" + caste + ",EmpCategory=" + cate + ",FP_Id='" + fingerprint.ToString() + "',EmpEmail=?email,EmpDeptID=" + depart + ",EmpDesignId=" + desi + "" + photo + ",modify_datetime=" + mode + ",UserID=" + user_id + ",IsUpdate=1 where EmpId=" + emp_id + "";

            //            int res = objmysqldb.InsertUpdateDeleteData(query);
            //            if (res != 1)
            //            {
            //                ltrErr.Text = "Please Try Again.";
            //                Logger.WriteCriticalLog("ManageEmployee 999 Update error.");
            //            }
            //            else
            //            {
            //                //long lastEmpid = objmysqldb.LastNo("employee_master", "EmpId", mode);
            //                //Emp_idHidden.Value = emp_id.ToString();
            //                ViewState["Employee"] = emp_id.ToString();
            //                Emp_idHidden.Value = (string)ViewState["Employee"];
            //                ltrErr.Text = "Employee Details Save Successfully";
            //                GetEmployeeData();
            //            }
            //        }
            //        #endregion

            //        ltrper.Text = "";
            //        lttF.Text = "";
            //        lttQ.Text = "";
            //        lttE.Text = "";
            //    }
            //    catch (Exception ex)
            //    {
            //        Logger.WriteCriticalLog("ManageEmployee 1020: exception:" + ex.Message + "::::::::" + ex.StackTrace);
            //    }
            //    finally
            //    {
            //        objmysqldb.CloseSQlConnection();
            //        objmysqldb.disposeConnectionObj();

            //    }
            //}





        }

        protected void btnAddComailnt_Click(object sender, EventArgs e)
        {
            int comid=int.Parse(Comp_id.ToString());

            if (comid > 0)
            {
                if (!int.Parse(Comp_id.ToString()).Equals("0"))
                {
                    // GetComplaintDetails();
                    updateComapaintDetails();
                    return;
                }
            }
            string doc = " ";
            String strConnString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(strConnString);
            try
            {
                if (txtname.Equals(""))
                {
                    //ltrErr.Text = "Customer name is mandatory";
                    return;
                }
                long Comp_date = 0;
                string Comp_dt = "";
                string Comp_Complete_dt = "";
                if (txtcomplaintDate.Text.Equals(""))
                {
                    //ltrErr.Text = "Complaint Date is mandatory";
                    return;
                }
                else
                {
                    string[] arrdob = txtcomplaintDate.Text.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                    if (arrdob.Length == 3)
                    {
                        Comp_dt = ((arrdob[0]).ToString().Length == 1 ? "0" + (arrdob[0]).ToString() : (arrdob[0]).ToString()) + "/" + ((arrdob[1]).ToString().Length == 1 ? "0" + (arrdob[1]).ToString() : (arrdob[1]).ToString()) + "/" + ((arrdob[2]).ToString());
                    }
                    else
                    {
                        //  ltrErr.Text = "Complaint Date is not in proper format.";
                        return;
                    }

                }
                string[] comp_date = txtcomplaintDate.Text.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                if (comp_date.Length == 3)
                {
                    Comp_Complete_dt = ((comp_date[0]).ToString().Length == 1 ? "0" + (comp_date[0]).ToString() : (comp_date[0]).ToString()) + "/" + ((comp_date[1]).ToString().Length == 1 ? "0" + (comp_date[1]).ToString() : (comp_date[1]).ToString()) + "/" + ((comp_date[2]).ToString());
                }
                if (txtresiAdd.Text.Equals(""))
                {
                    //ltrErr.Text = "Complaint Date is not in proper format.";
                    return;
                }
                if (txtdesc.Text.Equals(""))
                {
                    //ltrErr.Text = "Descuition can't be blank.";
                    return;

                }
                int status = 0;
                int.TryParse(ddlStatus.Items[ddlStatus.SelectedIndex].Value.ToString(), out status);
                if (status < 0)
                {

                    //ltrErr.Text = "Select Valid Status.";
                    return;

                }


                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "getComplaintID";
                cmd.Connection = con;
                con.Open();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable dtCompNo = new DataTable();
                adp.Fill(dtCompNo);
                string Comp_no_Append = "COMP";
                string FinalComp_no = "";
                if (dtCompNo !=null && dtCompNo.Rows.Count==0)
                {

                    FinalComp_no = "COMP01";
                }

                else
                {

                    string comp_no = dtCompNo.Rows[0]["comp_id"].ToString();
                    if (comp_no.Equals(""))
                    {

                        FinalComp_no = "COMP01";
                    }
                    else
                    {
                        int CONO = int.Parse(comp_no.ToString());
                        int fcno = CONO + 1;
                        FinalComp_no = Comp_no_Append +fcno.ToString();
                        txtcompno.Text = FinalComp_no.ToString();
                    
                    }
                   
                
                }

                SqlCommand cmdInsert = new SqlCommand("sp_save_compDetails", con);
                cmdInsert.CommandType = CommandType.StoredProcedure;
                cmdInsert.Parameters.AddWithValue("@comp_number", FinalComp_no);
                cmdInsert.Parameters.AddWithValue("@custome_name", txtname.Text.ToString());
                cmdInsert.Parameters.AddWithValue("@mobile_no", txtcontactno.Text.ToString());
                cmdInsert.Parameters.AddWithValue("@alternate_no ", txtotherno.Text.ToString());
                //cmdInsert.Parameters.AddWithValue("@resi_city ",);
                cmdInsert.Parameters.AddWithValue("@pin_code ", txtpincode.Text.ToString());
                cmdInsert.Parameters.AddWithValue("@complaint_desc ", txtdesc.Text.ToString());
                cmdInsert.Parameters.AddWithValue("@addeby ", user_id);
                cmdInsert.Parameters.AddWithValue("@IsDelete ", 0);
                cmdInsert.Parameters.AddWithValue("@DOC", DateTime.Now.Ticks);
                cmdInsert.Parameters.AddWithValue("@DOM ", DateTime.Now.Ticks);
                cmdInsert.Parameters.AddWithValue("@status_id", status);
                cmdInsert.Parameters.AddWithValue("@resi_city", txtcity.Text.ToString());
                long comp_date1 = 0;
                long comp_date2 = 0;
                long.TryParse(comp_date.ToString(), out comp_date1);
                long.TryParse(Comp_Complete_dt.ToString(),out comp_date2);
                cmdInsert.Parameters.AddWithValue("@comp_date",comp_date1);
                  cmdInsert.Parameters.AddWithValue("@comp_complete_date",comp_date2);
                  cmdInsert.Parameters.AddWithValue("@IsCancel", 0);
               
                int Success = cmdInsert.ExecuteNonQuery();
                if (Success > 0)
                {
                   ltrper.Text = "Complaint Saved Successfully......";
                   try
                   { 
                       string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };  
  
                        string sRandomOTP = GenerateRandomOTP(4, saAllowedCharacters);
                       string date=DateTime.Now.ToShortDateString();
                       string con_no=FinalComp_no.ToString();
                       string msg="Your Complaint has been book On date:"+date+" and Complaint_id:"+con_no+" and Otp_is:"+sRandomOTP+"";
                       Logger.WriteCriticalLog("SMS SENT DETAILS:" + msg + "::::::::" + date);
                   }
                    catch(Exception ex)
                   {
                       Logger.WriteCriticalLog("Message Sent Error: exception:" + ex.Message + "::::::::" + ex.StackTrace);

                   }
                }
                else
                {

                    ltrper.Text = "Complaint  Not Saved Successfully......";
                
                }


            }

            catch (Exception Ex)
            {



            }
            finally
            {

                con.Close();
            }
        }
        private string GenerateRandomOTP(int iOTPLength, string[] saAllowedCharacters)
        {

            string sOTP = String.Empty;

            string sTempChars = String.Empty;

            Random rand = new Random();

            for (int i = 0; i < iOTPLength; i++)
            {

                int p = rand.Next(0, saAllowedCharacters.Length);

                sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];

                sOTP += sTempChars;

            }

            return sOTP;

        }  
        private void GetComplaintDetails()
        {
            String strConnString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(strConnString);
            try
            {
               
                SqlCommand cmd = new SqlCommand();

                if (Comp_id > 0)
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "GetAllComplaintDetailsBYComp_Id";
                    cmd.Parameters.AddWithValue("@comp_id", Comp_id);
                    cmd.Connection = con;
                    con.Open();
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    DataTable dtCompNo = new DataTable();
                    adp.Fill(dtCompNo);
                    if (dtCompNo != null && dtCompNo.Rows.Count > 0)
                    {
                        if (!dtCompNo.Rows[0]["customer_name"].ToString().Equals(""))
                        {
                            string Customer_Name = dtCompNo.Rows[0]["customer_name"].ToString();
                            if (!Customer_Name.Equals(""))
                            {

                                txtname.Text = Customer_Name;
                            }
                        }

                        if (!dtCompNo.Rows[0]["comp_number"].ToString().Equals(""))
                        {
                            string Comp_no = dtCompNo.Rows[0]["comp_number"].ToString();
                            if(!Comp_no.Equals(""))
                            {
                                txtcompno.Text = Comp_no;

                            }
                        
                        }

                        if (!dtCompNo.Rows[0]["mobile_no"].ToString().Equals(""))
                        {
                            string mobile_no = dtCompNo.Rows[0]["mobile_no"].ToString();
                            if (!mobile_no.Equals(""))
                            {
                               txtcontactno.Text = mobile_no;
                            }
                        
                        }
                        if (!dtCompNo.Rows[0]["alternate_no"].ToString().Equals(""))
                        {
                            string Alternate_no = dtCompNo.Rows[0]["alternate_no"].ToString();
                            if (!Alternate_no.Equals(""))
                            {
                                txtotherno.Text = Alternate_no;
                            }

                        }
                        if (!dtCompNo.Rows[0]["pin_code"].ToString().Equals(""))
                        {
                            string pin_code = dtCompNo.Rows[0]["pin_code"].ToString();
                            if (!pin_code.Equals(""))
                            {
                                txtpincode.Text = pin_code;
                            }

                        }
                        if (!dtCompNo.Rows[0]["complaint__desc"].ToString().Equals(""))
                        {
                            string complaint__desc = dtCompNo.Rows[0]["complaint__desc"].ToString();
                            if (!complaint__desc.Equals(""))
                            {
                                txtdesc.Text = complaint__desc;
                            }

                        }
                        if (!dtCompNo.Rows[0]["resi_add"].ToString().Equals(""))
                        {
                            string resi_add = dtCompNo.Rows[0]["resi_add"].ToString();
                            if (!resi_add.Equals(""))
                            {
                                txtresiAdd.Text = resi_add;
                            }

                        }
                        string Status_id = dtCompNo.Rows[0]["status_id"].ToString();
                        if (!Status_id.Equals("-1"))
                        {
                            ddlStatus.SelectedIndex = ddlStatus.Items.IndexOf(ddlStatus.Items.FindByValue(Status_id));
                        }
                        if (!dtCompNo.Rows[0]["comp_date"].ToString().Equals(""))
                        {
                            long Comp_Date = long.Parse(dtCompNo.Rows[0]["comp_date"].ToString());
                            string comp_Date = (String)Comp_Date.ToString();
                            string[] co_date = comp_Date.Split('/');
                            if (co_date.Length == 3)
                            {
                                string mon = co_date[0].ToString();
                                string date = co_date[1].ToString();
                                string yr = co_date[2].ToString();

                                string final_comp_date = mon + "/" + date + "/" + "/" + yr;
                                txtcompdate.Text = final_comp_date;
                            }
                        }
                        if (!dtCompNo.Rows[0]["comp_complete_date"].ToString().Equals(""))
                        {
                            long Comp_Complete_Date = long.Parse(dtCompNo.Rows[0]["comp_complete_date"].ToString());
                            string comp_Complete_Date = (String)Comp_Complete_Date.ToString();
                            string[] co_Complete_date = comp_Complete_Date.Split('/');
                            if (co_Complete_date.Length == 3)
                            {
                                string mon = co_Complete_date[0].ToString();
                                string date = co_Complete_date[1].ToString();
                                string yr = co_Complete_date[2].ToString();

                                string final_comp_date = mon + "/" + date + "/" + "/" + yr;
                                txtcompdate.Text = final_comp_date;
                            }
                        }
                        if (!dtCompNo.Rows[0]["resi_city"].ToString().Equals(""))
                        {
                            string city = dtCompNo.Rows[0]["resi_city"].ToString();
                            txtcity.Text = city;
                        }
                    }
                }
               

            }
            catch (Exception ex)
            {
                //String PageName = "ManageEmployee.Aspx";
                //Logger.WriteCriticalLog("Error GetComplaintDetails Line no 1352 "+ex.Message +"Error:="+ex.StackTrace);

            }
            finally
            {

                con.Close();
            }
        }
    }
}
