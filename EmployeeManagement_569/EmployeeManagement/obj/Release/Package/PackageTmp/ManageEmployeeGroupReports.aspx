﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ManageEmployeeGroupReports.aspx.cs" Inherits="EmployeeManagement.ManageEmployeeGroupReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <h1>Manage Employee Report Group
                <small>
                    <asp:Label runat="server" ID="lblDateTime"></asp:Label></small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="../EmployeeGroupForReports.aspx"><i></i>Employee Report's Group List</a></li>
            <li class="active">Manage Employee Report Group</li>
        </ol>
    </section>

    <section class="content">

        <div class="box box-primary">
            <div class="box-header">
                <span style="font-size: large">Manage Report Group</span>

                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">

                            <div class="col-md-2">
                                <asp:Label ID="Label1" class="control-label" runat="server" Text="Report Group Name"></asp:Label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtgroup" runat="server" class="form-control" AutoPostBack="false"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <asp:CheckBox ID="chkleft" runat="server" Text="&nbsp;&nbsp;Include Left" OnCheckedChanged="chkleft_CheckedChanged" AutoPostBack="true"/>
                            </div>
                              <div class="col-md-2">
                                <asp:CheckBox ID="chkIsDefault" runat="server" Text="&nbsp;&nbsp;Default Group" />
                            </div>
                            <asp:HiddenField ID="report_grp" runat="server" />
                            <div class="col-md-3" style="width: auto"></div>
                            <span style="color: red; margin-left: 15px" id="val_msg_wrap">
                                <asp:Literal ID="ltrErr" runat="server"></asp:Literal>
                            </span>


                        </div>


                        <div class="col-md-12">
                            <div class="col-md-2">
                                <br />
                                <asp:Label ID="lblgrpname" class="control-label" runat="server" Text="Employee List"></asp:Label>
                            </div>
                             <asp:Button ID="Button2" runat="server" class="btn btn-primary"  Text="Save" Width="100px"  OnClientClick=" return validate()" OnClick="Button2_Click" />
                            <div class="col-md-5">
                                <br />
                                <br />
                                <asp:GridView ID="GridView1" CssClass="table table-bordered table-hover dataTable" runat="server" HeaderStyle-BackColor="#ede8e8" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Show" ControlStyle-Width="20px">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />

                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%-- <asp:CheckBox ID="cbSelect" CssClass="chkbox Group1" data-value="1" runat="server"></asp:CheckBox>--%>
                                                <asp:CheckBox ID="cbSelect" runat="server" onclick="Check_Click(this)" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="emp_id" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblemp_id" runat="server" Visible="false" Text='<%# Eval("empid") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Employee Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblempName" runat="server" Text='<%# Eval("FullName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                                <br />

                                <asp:Button ID="Button1" runat="server" class="btn btn-primary" Text="Save" Width="100px" OnClick="BtnSave_Click" OnClientClick=" return validateSaveDetails()" />
                            </div>


                        </div>
                    </div>
                </div>



            </div>
        </div>

        <script type="text/javascript">
            function validateSaveDetails() {
                if (document.getElementById("<%=txtgroup.ClientID%>").value == "") {
                    alert("Report Group name can not be blank");
                    document.getElementById("<%=txtgroup.ClientID%>").focus();
                    return false;
                }

                return true;
            }
        </script>

        <script type="text/javascript">
            function callfootercss() {
                var options = $('[id*=ltrErr] + .btn-group ul li.active');
                if (options.length == 0) {
                    $("#val_msg_wrap").show();
                    setTimeout(function () {
                        $("#val_msg_wrap").hide();
                    }, 3000);
                    return false;
                }
            }
            callfootercss();
        </script>

        <script type="text/javascript">
            function Check_Click(objRef) {
                //Get the Row based on checkbox
                var row = objRef.parentNode.parentNode.parentNode.parentNode;
                //Get the reference of GridView
                var GridView = row.parentNode;

                //Get all input elements in Gridview
                var inputList = GridView.getElementsByTagName("input");

                for (var i = 0; i < inputList.length; i++) {
                    //The First element is the Header Checkbox
                    var headerCheckBox = inputList[0];

                    //Based on all or none checkboxes
                    //are checked check/uncheck Header Checkbox
                    var checked = true;
                    if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                        if (!inputList[i].checked) {
                            checked = false;
                            break;
                        }
                    }
                }
                headerCheckBox.checked = checked;

            }
        </script>
        <script type="text/javascript">
            function checkAll(objRef) {
                var GridView = objRef.parentNode.parentNode.parentNode.parentNode.parentNode;
                var inputList = GridView.getElementsByTagName("input");
                for (var i = 0; i < inputList.length; i++) {
                    //Get the Cell To find out ColumnIndex
                    var row = inputList[i].parentNode.parentNode;
                    if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                        if (objRef.checked) {
                            inputList[i].checked = true;
                        }
                        else {
                            inputList[i].checked = false;
                        }
                    }
                }
            }
        </script>


        <script type="text/javascript">
            $(document).ready(function () {
                $('#ContentPlaceHolder1_GridView1').DataTable({
                    "fixedHeader": true,
                    "paging": false,
                    "lengthChange": true,
                    "searching": true,
                    "ordering": true,
                    "info": true,
                    "autoWidth": false
                });
            });
        </script>
    </section>
</asp:Content>
