<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="EmployeeManagement.Registration" %>

<link href='https://fonts.googleapis.com/css?family=Open+Sans:400,300,300italic,400italic,600' rel='stylesheet' type='text/css'>
<link href="//netdna.bootstrapcdn.com/font-awesome/3.1.1/css/font-awesome.css" rel="stylesheet">
<html>
    
    
<head>

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title><%--Admin Panel | Log in--%></title>
    <!-- Tell the browser to be responsive to screen width -->
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
    <!-- Bootstrap 3.3.6 -->
    <link rel="stylesheet" href="../../bootstrap/css/bootstrap.min.css">
    <!-- Font Awesome -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.5.0/css/font-awesome.min.css">
    <!-- Ionicons -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/ionicons/2.0.1/css/ionicons.min.css">
    <!-- Theme style -->
    <link rel="stylesheet" href="../../dist/css/AdminLTE.min.css">
    <!-- iCheck -->
    <link rel="stylesheet" href="../../plugins/iCheck/square/blue.css">
    </head>
<body>
<div class="testbox">
  <h1>Registration</h1>
    
    <div class="box box-default">
            <div class="box-body">
                <div class="row">
                    <form runat="server">
                    <div class="col-md-12">

                        <div class="col-md-2">
                            <asp:Label ID="lblempname" runat="server" Text="User Name"></asp:Label><asp:Label ID="Label1" runat="server" Text="*" ForeColor="Red"></asp:Label>
                        </div>
                        <div class="col-md-3">
                          <%--  <asp:TextBox ID="txtname" runat="server" class="form-control" AutoPostBack="false"></asp:TextBox>--%>
                           
                            <asp:textbox runat="server"  ID="txtname"  class="form-control"></asp:textbox>
                        </div><span style="color: red; margin-left: 15px" id="val_msg_wrap">
                                        <asp:Literal ID="ltrErr" runat="server"></asp:Literal>
                                    </span>
                    </div>
                      <br />
                    <br />
                    <div class="col-md-12">

                        <div class="col-md-2">
                            <asp:Label ID="Label2" runat="server" Text="Password"></asp:Label><asp:Label ID="Label3" runat="server" Text="*" ForeColor="Red"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtpwd" runat="server" TextMode="Password" class="form-control" AutoPostBack="false"></asp:TextBox>

                        </div><span style="color: red; margin-left: 15px" id="val_msg_wrap">
                                        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                                    </span>
                    </div>
                      <br />
                    <br />
                    <div class="col-md-12">

                        <div class="col-md-2">
                            <asp:Label ID="Label7" runat="server"  Text="Confirm Password"></asp:Label><asp:Label ID="Label8" runat="server" Text="*" ForeColor="Red"></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtconfpwd" runat="server" TextMode="Password" class="form-control" AutoPostBack="false"></asp:TextBox>

                        </div><span style="color: red; margin-left: 15px" id="val_msg_wrap">
                                        <asp:Literal ID="Literal2" runat="server"></asp:Literal>
                                    </span>
                    </div>
                      <br />
                    <br />
                    <div class="col-md-12">
                        <div class="col-md-2">
                            <asp:Label ID="Label9" runat="server" Text="Email"><%--</asp:Label><asp:Label ID="Label5" runat="server" Text="*" ForeColor="Red">--%></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtemail" runat="server" class="form-control" on AutoPostBack="false"></asp:TextBox>

                        </div>
                      <br />
                    <br />
                    <div class="col-md-12">
                        <div class="col-md-2">
                            <asp:Label ID="Label5" runat="server" Text="ContactNo"><%--</asp:Label><asp:Label ID="Label5" runat="server" Text="*" 
ForeColor="Red">--%></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtcnno" onkeypress="return isNumberKey(event)"  runat="server" class="form-control" AutoPostBack="false"></asp:TextBox>

                        </div>
                    </div>
                      <br />
                    <br />
                   
                    <%--<div class="col-md-12">
                        <div class="col-md-2">
                            <asp:Label ID="Label4" runat="server" Text="ContactNo">--%><%--</asp:Label><asp:Label ID="Label5" runat="server" Text="*" ForeColor="Red">--%</asp:Label>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtcn" runat="server" class="form-control" AutoPostBack="false"></asp:TextBox>

                        </div>
                    </div>
                     <%-- <br />
                    <br />--%>
                      <div class="col-md-12">

                                    <div class="col-md-2">
                                        <asp:Label ID="Label6" class="control-label" runat="server" Text="Gender"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:RadioButton ID="rdbmale" runat="server" Text="&nbsp;Male" Checked="true" GroupName="gender"  TabIndex="8"/>
                                        &nbsp;&nbsp;&nbsp;
                                <asp:RadioButton ID="rdbFemale" runat="server" Text="&nbsp;Female" GroupName="gender"  TabIndex="9"/>
                          </div>          </div>
                     <div class="row">
                    <div class="col-md-12 text-left" style="padding: 30px">
                        <asp:Button ID="btnSave" CssClass="btn btn-primary"
                            runat="server" Text="Save" Style="width: 150px" OnClientClick="return validateRegistrationDetails()" OnClick="btnSave_Click"  />
                     
                    </div>
                </div>
                        </form>
                </div>
            </div></div>
    <div class="col-xs-12">
                        <asp:Label ID="lblmsg" ForeColor="Red" runat="server" Font-Size="14px" Visible="false"
                            Text=""></asp:Label>
                        <div id="val_msg_wrapl" class="ehide" style="display: none">
                            <div class="alert alert-danger" id="val_msg">
                            </div>
                        </div>
                    </div>
</div>
</body>
  <!-- jQuery 2.2.3 -->
    <script src="../../plugins/jQuery/jquery-2.2.3.min.js"></script>
    <!-- Bootstrap 3.3.6 -->
    <script src="../../bootstrap/js/bootstrap.min.js"></script>
    <!-- iCheck -->
<script type="text/javascript">
function validateEmail(emailField){
        var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;

        if (reg.test(emailField.value) == false) 
        {
            alert('Invalid Email Address');
            return false;
        }

        return true;
    
}


    </script>

    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                if (charCode == 45) {
                    return true;
                }
                else {
                    return false;
                }

            return true;
        }

    </script>
<script type="text/javascript">
    function validateRegistrationDetails() {
        if (document.getElementById("<%=txtname.ClientID%>").value == "") {
                alert("User Name can not be blank");
                document.getElementById("<%=txtname.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtpwd.ClientID%>").value == "") {
                alert("Password  can not be blank");
                document.getElementById("<%=txtpwd.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtconfpwd.ClientID%>").value == "") {
                alert("Confirm Password can not be blank");
                document.getElementById("<%=txtconfpwd.ClientID%>").focus();
                return false;
            }
        var pass1 = document.getElementById("<%=txtpwd.ClientID %>").value;
        var pass2 = document.getElementById("<%=txtconfpwd.ClientID %>").value;
        if (pass1 != pass2) {
            document.getElementById("<%=lblmsg.ClientID %>").innerHTML = "Passwords Don't Match";
            return false;
        }
        else {
            document.getElementById("<%=lblmsg.ClientID %>").innerHTML = "";
            //empty string means no validation error
        }

            return true;
        }
    </script>


</html>