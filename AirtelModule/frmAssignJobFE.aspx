﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmAssignJobFE.aspx.cs" Inherits="frmAssignJobFE"
    MasterPageFile="~/MasterPage2.master" %>

<asp:Content ID="conhead" runat="server" ContentPlaceHolderID="head">
    <!----------------Bootstrp js-------------------->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <!--------------------------Page Loader------------------->
    <style>
        #loader
        {
            position: fixed;
            left: 0px;
            top: 0px;
            width: 100%;
            height: 100%;
            z-index: 9999;
            background: url('img/loader.gif') 50% 50% no-repeat rgb(249,249,249);
        }
    </style>
    <script src="http://code.jquery.com/jquery-1.8.2.js"></script>
    <script type="text/javascript">
        $(window).load(function () {
            $("#loader").fadeOut(1000);
        });
    </script>
    <!---------------------End--------------------------------->
    <!---------------------------------------------------------Update panel progress---------------------->
    <script type="text/javascript">
        function showProgress() {
            var updateProgress = $get("<%= UpdateProgress.ClientID %>");
            updateProgress.style.display = "block";
        }
    </script>
    <style type="text/css">
        .overlay
        {
            position: fixed;
            height: 100%;
            width: 100%;
            top: 0;
            left: 0px;
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.2;
            -moz-opacity: 0.2;
        }
    </style>
    <!----------------------------End----------------------------------->
</asp:Content>
<asp:Content ID="contentmain" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="loader">
    </div>
    <div class="right_col" role="main">
        <div class="page-title">
            <div class="title_left">
                <h3>
                    Assign Job</h3>
            </div>
        </div>
        <div class="card">
            <asp:UpdatePanel ID="updatepanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="card-body">
                        <div class="form-horizontal">
                            <div class="form-group row">
                                <div class="clearfix pb10">
                                </div>
                                <div runat="server" id="diverror" class="col-sm-12">
                                </div>
                                <div class="clearfix pb10">
                                </div>
                                <div class="col-sm-12">
                                    <div class="label label-danger" style="height: 35px; width=30px;">
                                    </div>
                                    <div class="label label-info ml10 fs14">
                                        Today's Prescheduled Appointments.
                                        <asp:Label ID="lblcounttotal" runat="server" class="label label-primary fs14 pull-right"></asp:Label>
                                        <asp:Label ID="Label1" runat="server" class="label label-primary fs14 pull-right mr10"
                                            Text="For Sort Data please click on grid Header."></asp:Label></div>
                                </div>
                                <div class="clearfix pb5">
                                </div>
                                <div>
                                    <asp:HiddenField ID="hffename" runat="server" />
                                    <div class="col-sm-6">
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="txtsearch" runat="server" class="form-control" Placeholder="Enter Party Name or Logical Circuit ID"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:Button ID="btnsearch" runat="server" class="btn btn-success pull-right" OnClick="btnsearch_Click"
                                                Text="Search" />
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="col-sm-3">
                                            Select Audit Case (Category Wise)
                                        </div>
                                        <div class="col-sm-9">
                                            <asp:DropDownList ID="ddlprodbycat" runat="server" AutoPostBack="true" class="form-control focus"
                                                OnSelectedIndexChanged="ddlprodbycat_SelectedIndexChanged">
                                                <asp:ListItem Selected="True" Value="0">All</asp:ListItem>
                                                <asp:ListItem Value="Leased Line-Initial">Leased Line-Initial</asp:ListItem>
                                                <asp:ListItem Value="Leased Line-Periodic">Leased Line-Periodic</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="clearfix pb10">
                                    </div>
                                    <div class="table-responsive">
                                        <asp:HiddenField ID="hflogicalcircuitid" runat="server" />
                                        <asp:HiddenField ID="hfPartyName" runat="server" />
                                        <asp:HiddenField ID="hfotp" runat="server" />
                                        <asp:HiddenField ID="hflocsrid" runat="server" />
                                        <asp:HiddenField ID="hfEmailCustomer" runat="server" />
                                        <asp:HiddenField ID="hfNameCustomer" runat="server" />
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvexcel" runat="server" AllowSorting="true" AutoGenerateColumns="false"
                                                class="table text-nowrap" HeaderStyle-Wrap="false" OnRowCommand="gvexcel_RowCommand"
                                                OnRowDataBound="gvexcel_RowDataBound" OnSorting="OnSorting">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="View Detail">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lblview" runat="server" class="fa fa-eye fa-2x" CommandArgument='<%#Eval("UniqueID") %>'
                                                                CommandName="ViewDetail"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Update Status">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lblstatus" runat="server" class="fa fa-edit fa-2x" CommandArgument='<%#Eval("UniqueID") %>'
                                                                CommandName="UpdateStatus"></asp:LinkButton>
                                                            <asp:HiddenField ID="hfstatusfe" runat="server" Value='<%#Eval("StatusByFE") %>' />
                                                            <asp:HiddenField ID="hfemail" runat="server" Value='<%#Eval("[Cust Email]") %>' />
                                                            <asp:HiddenField ID="Customer" runat="server" Value='<%#Eval("[Customer Name]") %>' />
                                                            <asp:HiddenField ID="hfrevisitdate" runat="server" Value='<%#Eval("[Revisit Date Time]","{0:dd/MMM/yyyy}") %>' />
                                                            <asp:HiddenField ID="hfPreMeetingDate" runat="server" Value='<%#Eval("PreMeetingDate","{0:dd/MMM/yyyy}") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Customer Name" HeaderText="Customer Name" NullDisplayText="#"
                                                        SortExpression="Customer Name" />
                                                    <asp:TemplateField HeaderText="Party Name" SortExpression="Party Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblpartyname" runat="server" Text='<%# Eval("Party Name").ToString() == "" ? "#" : Eval("Party Name").ToString()%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Logical Circuit ID" SortExpression="Logical Circuit ID">
                                                        <ItemTemplate>
                                                            <asp:Label ID="logiclacircuitid" runat="server" Text='<%# Eval("Logical Circuit ID").ToString() == "" ? "#" : Eval("Logical Circuit ID").ToString()%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="From Site" HeaderText="Customer Address-1" NullDisplayText="#"
                                                        SortExpression="From Site" />
                                                    <asp:BoundField DataField="ProductName" HeaderText="Product Name" NullDisplayText="#"
                                                        SortExpression="ProductName" />
                                                    <asp:BoundField DataField="ProdTypeName" HeaderText="Type" NullDisplayText="#" SortExpression="ProdTypeName" />
                                                    <asp:BoundField DataField="BILLING_BANDWIDTH" HeaderText="Circuit BandWidth" NullDisplayText="#"
                                                        SortExpression="BILLING_BANDWIDTH" />
                                                    <asp:TemplateField HeaderText="Appoinment Mail Date or Email">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblappointmentmailsenddate" runat="server" Text='<%# Eval("AppointmentMailDatetime", "{0:dd/MMM/yyyy}").ToString() == "" ? "#" : Eval("AppointmentMailDatetime", "{0:dd/MMM/yyyy}").ToString()%>'></asp:Label>
                                                            <div class="clearfix">
                                                            </div>
                                                            <asp:Label ID="lblemailcustomer" runat="server" Text='<%#Eval("[Cust Email]") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Account Manager" HeaderText="Account Manager" NullDisplayText="#"
                                                        SortExpression="Account Manager" />
                                                    <asp:TemplateField HeaderText="Compliance Date" SortExpression="FEComplianceDate">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblcomp" runat="server" Text='<%# Eval("FEComplianceDate", "{0:dd/MMM/yyyy}").ToString() == "" ? "#" : Eval("FEComplianceDate", "{0:dd/MMM/yyyy}").ToString()%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                                <div id="myModal" aria-hidden="true" aria-labelledby="exampleModalLabel" class="modal fade text-left"
                                    role="dialog" tabindex="-1" data-backdrop="static" data-keyboard="false">
                                    <div class="modal-dialog" role="document">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <button aria-label="Close" class="close" data-dismiss="modal" type="button">
                                                    <span aria-hidden="true">×</span>
                                                </button>
                                            </div>
                                            <div class="modal-body">
                                                <div id="divshow1" runat="server">
                                                    <h3>
                                                        A brief detail of client information.</h3>
                                                    <asp:DataList ID="dluploadexcelrecord" runat="server" RepeatColumns="1" RepeatDirection="Horizontal"
                                                        RepeatLayout="Flow">
                                                        <ItemTemplate>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    Allocated Date</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("Allocated Date", "{0:dd/MMM/yyyy}").ToString() == "" ? "#" : Eval("Allocated Date", "{0:dd/MMM/yyyy}").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    Account Manager
                                                                </h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("Account Manager").ToString() == "" ? "#" : Eval("Account Manager").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    Account Number
                                                                </h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("Account Number").ToString() == "" ? "#" : Eval("Account Number").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    Annotation</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("Annotation").ToString() == "" ? "#" : Eval("Annotation").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    B2B Email</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("B2B_Email").ToString() == "" ? "#" : Eval("B2B_Email").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    B2B Head Email</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("B2B_Head_Email").ToString() == "" ? "#" : Eval("B2B_Head_Email").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    B2B Head Name</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("B2B_Head_Name").ToString() == "" ? "#" : Eval("B2B_Head_Name").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    Bill Company</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("Bill Company").ToString() == "" ? "#" : Eval("Bill Company").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    Billing Address</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("Billing Address").ToString() == "" ? "#" : Eval("Billing Address").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    Billing Trig Date</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("Billing Trig Date", "{0:dd/MMM/yyyy}").ToString() == "" ? "#" : Eval("Billing Trig Date", "{0:dd/MMM/yyyy}").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    BILLING BANDWIDTH</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("BILLING_BANDWIDTH").ToString() == "" ? "#" : Eval("BILLING_BANDWIDTH").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    BILLING BANDWIDTH UOM</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("BILLING_BANDWIDTH_UOM").ToString() == "" ? "#" : Eval("BILLING_BANDWIDTH_UOM").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    Billing Contact Number</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("Billing_Contact_Number").ToString() == "" ? "#" : Eval("Billing_Contact_Number").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    Billing Email Id</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("Billing_Email_Id").ToString() == "" ? "#" : Eval("Billing_Email_Id").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    Complaince Date</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("FEComplianceDate", "{0:dd/MMM/yyyy}").ToString() == "" ? "#" : Eval("FEComplianceDate", "{0:dd/MMM/yyyy}").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    Contact Phone1</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("Contact Phone1").ToString() == "" ? "#" : Eval("Contact Phone1").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    Cust Email</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("Cust Email").ToString() == "" ? "#" : Eval("Cust Email").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    Customer Name</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("Customer Name").ToString() == "" ? "#" : Eval("Customer Name").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    Customer Segment</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("Customer Segment").ToString() == "" ? "#" : Eval("Customer Segment").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    Case Eligible/Not Eligible</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("Eligible/Not Eligible").ToString() == "" ? "#" : Eval("Eligible/Not Eligible").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    FE Name</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("FE Name").ToString() == "" ? "#" : Eval("FE Name").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    FX ACCOUNT EXTERNAL ID</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("FX_ACCOUNT_EXTERNAL_ID").ToString() == "" ? "#" : Eval("FX_ACCOUNT_EXTERNAL_ID").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    Installation Address</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("Installation Address").ToString() == "" ? "#" : Eval("Installation Address").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    Record Inserted Date</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("InsertedDate", "{0:dd/MMM/yyyy}").ToString() == "" ? "#" : Eval("InsertedDate", "{0:dd/MMM/yyyy}").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    KAM Contact Number</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("KAM_Contact_Number").ToString() == "" ? "#" : Eval("KAM_Contact_Number").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    KAM Email</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("KAM_Email").ToString() == "" ? "#" : Eval("KAM_Email").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    KAM Name</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("KAM_Name").ToString() == "" ? "#" : Eval("KAM_Name").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    Logical Circuit Id</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("Logical Circuit Id").ToString() == "" ? "#" : Eval("Logical Circuit Id").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    Verify Client Mobile OTP
                                                                </h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("Mobile OTP").ToString() == "" ? "#" : Eval("Mobile OTP").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    Mobile No</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("Mobile No").ToString() == "" ? "#" : Eval("Mobile No").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    Visit Company Name</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("Party Name").ToString() == "" ? "#" : Eval("Party Name").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    PO Number</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("PO Number").ToString() == "" ? "#" : Eval("PO Number").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    Primary Address</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("Primary Address").ToString() == "" ? "#" : Eval("Primary Address").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    Project Manager</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("Project Manager").ToString() == "" ? "#" : Eval("Project Manager").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    Product Type by Excel</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("PRODUCT_TYPE").ToString() == "" ? "#" : Eval("PRODUCT_TYPE").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    Product Name by Excel</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("Product Name").ToString() == "" ? "#" : Eval("Product Name").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    Product Name as Master Entry</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("ProductName").ToString() == "" ? "#" : Eval("ProductName").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    Prod Type Name as Master Entry</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("ProdTypeName").ToString() == "" ? "#" : Eval("ProdTypeName").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    RM Contact Number</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("RM_Contact_Number").ToString() == "" ? "#" : Eval("RM_Contact_Number").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    RM Email</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("RM_Email").ToString() == "" ? "#" : Eval("RM_Email").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    RM NAME</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("RM_NAME").ToString() == "" ? "#" : Eval("RM_NAME").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    SAM Contact Number</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("SAM_Contact_Number").ToString() == "" ? "#" : Eval("SAM_Contact_Number").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    SAM Email</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("SAM_Email").ToString() == "" ? "#" : Eval("SAM_Email").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    SAM Name</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("SAM_Name").ToString() == "" ? "#" : Eval("SAM_Name").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    SAM TL</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("SAM_TL").ToString() == "" ? "#" : Eval("SAM_TL").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    SAM TL Contact Number</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("SAM_TL_Contact_Number").ToString() == "" ? "#" : Eval("SAM_TL_Contact_Number").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    SAM TL EMAIL</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("SAM_TL_EMAIL").ToString() == "" ? "#" : Eval("SAM_TL_EMAIL").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    Secondary Address</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("Secondary Address").ToString() == "" ? "#" : Eval("Secondary Address").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    Segment</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("Segment").ToString() == "" ? "#" : Eval("Segment").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    Service Name</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("Service Name").ToString() == "" ? "#" : Eval("Service Name").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    TL Name</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("TL Name").ToString() == "" ? "#" : Eval("TL Name").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    TL Contact Number</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("TL_Contact_Number").ToString() == "" ? "#" : Eval("TL_Contact_Number").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    TL Email</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("TL_Email").ToString() == "" ? "#" : Eval("TL_Email").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    Verification Type</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("Verification Type").ToString() == "" ? "#" : Eval("Verification Type").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    Vertical</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("Vertical").ToString() == "" ? "#" : Eval("Vertical").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    VH Contact Number</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("VH_Contact_Number").ToString() == "" ? "#" : Eval("VH_Contact_Number").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    VH Email</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("VH_Email").ToString() == "" ? "#" : Eval("VH_Email").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    VH Name</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("VH_Name").ToString() == "" ? "#" : Eval("VH_Name").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:DataList>
                                                </div>
                                                <asp:UpdatePanel ID="upanel" runat="server" ChildrenAsTriggers="true">
                                                    <ContentTemplate>
                                                        <div class="clearfix pb5">
                                                        </div>
                                                        <div id="divmssgpopup" runat="server">
                                                        </div>
                                                        <div class="clearfix pb5">
                                                        </div>
                                                        <div id="divupdatebyfe" runat="server">
                                                            <h3>
                                                                Update Status as per client response.</h3>
                                                            <div>
                                                                <p>
                                                                    Select any one value if your value not in a dropdown selection section then select
                                                                    None of Above</p>
                                                                <p class="text-right pull-right mr10">
                                                                    <strong>Mark with * are mandatory field</strong></p>
                                                                <div class="clearfix pb5">
                                                                </div>
                                                                <div class="form-group row">
                                                                    <label class="col-sm-3 form-control-label">
                                                                        Select Case *
                                                                    </label>
                                                                    <div class="col-sm-9">
                                                                        <asp:DropDownList ID="ddlselectcase" runat="server" AutoPostBack="true" class="form-control"
                                                                            OnSelectedIndexChanged="ddlselectcase_SelectedIndexChanged">
                                                                            <asp:ListItem Value="01">--Select Case--</asp:ListItem>
                                                                            <asp:ListItem Value="02">Customer Shifted</asp:ListItem>
                                                                            <asp:ListItem Value="03">Company Closed</asp:ListItem>
                                                                            <asp:ListItem Value="09">Address Not Found</asp:ListItem>
                                                                            <asp:ListItem Value="04">Connection Disconnected</asp:ListItem>
                                                                            <asp:ListItem Value="06">Revisit Required</asp:ListItem>
                                                                            <asp:ListItem Value="07">Entry Not Allowed</asp:ListItem>
                                                                            <asp:ListItem Value="08">Customer Denied Audit</asp:ListItem>
                                                                            <asp:ListItem Value="05">None of the Above</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="Clearfix pb5">
                                                            </div>
                                                            <div id="DivCompanyshifted" runat="server" visible="false">
                                                                <div id="divaddforcompshift" runat="server" class="form-group row">
                                                                    <label class="col-sm-3 form-control-label">
                                                                        Enter New Address *
                                                                    </label>
                                                                    <div class="col-sm-9">
                                                                        <asp:TextBox ID="txtnewcompanyaddress" runat="server" Height="70px" placeholder="New Address"
                                                                            required="" TextMode="MultiLine"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group row">
                                                                    <label class="col-sm-3 form-control-label">
                                                                        Enter Person Met Name
                                                                    </label>
                                                                    <div class="col-sm-9">
                                                                        <asp:TextBox ID="txtcompshiftpersonmetname" runat="server" placeholder="Enter Person Met Name"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group row">
                                                                    <label class="col-sm-3 form-control-label">
                                                                        Enter Person Met Designation
                                                                    </label>
                                                                    <div class="col-sm-9">
                                                                        <asp:TextBox ID="txtcompshiftpersonmetdesig" runat="server" placeholder="Enter Person Met Designation"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group row">
                                                                    <label class="col-sm-3 form-control-label">
                                                                        Enter Person Met Email
                                                                    </label>
                                                                    <div class="col-sm-9">
                                                                        <asp:TextBox ID="txtcompshiftpersonmetemail" runat="server" placeholder="Enter Person Met Email"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="Clearfix pb5">
                                                                </div>
                                                                <asp:Button ID="btnaddaddress" runat="server" class="btn btn-primary pull-right mr5"
                                                                    OnClick="btnaddaddress_Click" Text="Submit and Save" />
                                                            </div>
                                                            <div class="Clearfix pb5">
                                                            </div>
                                                            <div id="Divrevisit" runat="server" visible="false">
                                                                <div class="form-group row">
                                                                    <label class="col-sm-3 form-control-label">
                                                                        Why Revisit Is Required *
                                                                    </label>
                                                                    <div class="col-sm-9">
                                                                        <asp:DropDownList ID="ddlrevisitrequired" runat="server" AutoPostBack="true" class="form-control"
                                                                            OnSelectedIndexChanged="ddlrevisitrequired_SelectedIndexChanged">
                                                                            <asp:ListItem Value="01">--Select Revisit Reason--</asp:ListItem>
                                                                            <asp:ListItem Value="02">Customer Not Available Today</asp:ListItem>
                                                                            <asp:ListItem Value="03">Customer Busy</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div id="revisittimedate" runat="server" visible="false">
                                                                    <div class="form-group row">
                                                                        <label class="col-sm-3 form-control-label">
                                                                            On Which Date And Time Revisit Is Required *
                                                                        </label>
                                                                        <div class="col-sm-9">
                                                                            <asp:TextBox ID="txtrevisittimeanddate" runat="server" class="form-control" reuqired=""
                                                                                type="date"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group row">
                                                                        <label class="col-sm-3 form-control-label">
                                                                            On Which Time Revisit Is Required *
                                                                        </label>
                                                                        <div class="col-sm-9">
                                                                            <asp:TextBox ID="txttimerevisit" runat="server" class="form-control" placeholder="On Which Time Revisit Is Required"
                                                                                reuqired="" type="time"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group row">
                                                                        <label class="col-sm-3 form-control-label">
                                                                            Person Met Name *
                                                                        </label>
                                                                        <div class="col-sm-9">
                                                                            <asp:TextBox ID="txtrevisitpersonmetname" runat="server" class="form-control" placeholder="Person Met Name"
                                                                                reuqired=""></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group row">
                                                                        <label class="col-sm-3 form-control-label">
                                                                            Person Met Mobile No *
                                                                        </label>
                                                                        <div class="col-sm-9">
                                                                            <asp:TextBox ID="txtrevisitpersonmobileno" runat="server" class="form-control" MaxLength="10"
                                                                                Placeholder="Mobile No" reuqired=""></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="clearfix pb10">
                                                                    </div>
                                                                    <asp:Button ID="btngetreviitotp" runat="server" class="btn btn-primary pull-right mr5"
                                                                        OnClick="btngetreviitotp_Click" Text="Next" />
                                                                    <div class="clearfix pb10">
                                                                    </div>
                                                                </div>
                                                                <div id="DivOTPRevisit" runat="server" visible="false">
                                                                    <label class="col-sm-3 form-control-label">
                                                                        <h4>
                                                                            Enter OTP</h4>
                                                                    </label>
                                                                    <div class="col-sm-9">
                                                                        <asp:TextBox ID="txtotprevisitcustmer" runat="server" class="form-control" Placeholer="Enter Six Digit OTP"
                                                                            reuqired=""></asp:TextBox>
                                                                    </div>
                                                                    <div class="clearfix pb10">
                                                                    </div>
                                                                    <asp:Button ID="btnsubmitrevisitstatus" runat="server" class="btn btn-primary pull-right mr5"
                                                                        OnClick="btnsubmitrevisitstatus_Click" Text="Submit" />
                                                                    <div class="clearfix pb10">
                                                                    </div>
                                                                    <asp:LinkButton ID="lbresendotp" runat="server" class="blue pull-right mr5" OnClick="lbresendotp_Click">Resend OTP</asp:LinkButton>
                                                                </div>
                                                            </div>
                                                            <div class="Clearfix pb5">
                                                            </div>
                                                            <div id="panstatuscase1" runat="server" visible="false">
                                                                <div class="form-group row">
                                                                    <label class="col-sm-3 form-control-label">
                                                                        Did he put stamp *
                                                                    </label>
                                                                    <div class="col-sm-9">
                                                                        <asp:DropDownList ID="ddlstamp" runat="server" AutoPostBack="true" class="form-control"
                                                                            OnSelectedIndexChanged="ddlstamp_SelectedIndexChanged">
                                                                            <asp:ListItem Value="01">--Select Status--</asp:ListItem>
                                                                            <asp:ListItem Value="02">Yes</asp:ListItem>
                                                                            <asp:ListItem Value="03">No</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="Clearfix pb5">
                                                            </div>
                                                            <div id="panelstatuscase2" runat="server" visible="false">
                                                                <div class="form-group row">
                                                                    <label class="col-sm-3 form-control-label">
                                                                        Did he email from his domain regarding stamp *
                                                                    </label>
                                                                    <div class="col-sm-9">
                                                                        <asp:DropDownList ID="ddlemailregardstamp" runat="server" AutoPostBack="true" class="form-control"
                                                                            OnSelectedIndexChanged="ddlemailregardstamp_SelectedIndexChanged">
                                                                            <asp:ListItem Value="01">--Select Status--</asp:ListItem>
                                                                            <asp:ListItem Value="02">Yes</asp:ListItem>
                                                                            <asp:ListItem Value="03">No</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="Clearfix pb5">
                                                            </div>
                                                            <div id="divemailifotherempverify" runat="server" visible="false">
                                                                <div class="form-group row">
                                                                    <label class="col-sm-3 form-control-label">
                                                                        Contact Person Name
                                                                    </label>
                                                                    <div class="col-sm-9">
                                                                        <asp:TextBox ID="txtsecondpersonname" runat="server" class="form-control" Placeholder="Contact Person Name"
                                                                            required=""></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="clearfix pb10">
                                                                </div>
                                                                <div class="form-group row">
                                                                    <label class="col-sm-3 form-control-label">
                                                                        Contact Person Designation
                                                                    </label>
                                                                    <div class="col-sm-9">
                                                                        <asp:TextBox ID="txtsecondpersondesig" runat="server" class="form-control" Placeholder="Contact Person Designation"
                                                                            required=""></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="clearfix pb10">
                                                                </div>
                                                                <div class="form-group row">
                                                                    <label class="col-sm-3 form-control-label">
                                                                        Contact Person Email *
                                                                    </label>
                                                                    <div class="col-sm-9">
                                                                        <asp:TextBox ID="txtemailsecondperson" runat="server" class="form-control" Placeholder="Contact Person Email"
                                                                            required=""></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="clearfix pb10">
                                                                </div>
                                                                <div class="form-group row">
                                                                    <label class="col-sm-3 form-control-label">
                                                                        Contact Person Mobile-No *
                                                                    </label>
                                                                    <div class="col-sm-9">
                                                                        <asp:TextBox ID="txtnumbersecondperson" runat="server" class="form-control" MaxLength="10"
                                                                            Placeholder="Contact Person Mobile-No" required=""></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div id="divEntrynotallowed" runat="server" visible="false">
                                                                <div class="form-group row">
                                                                    <label class="col-sm-3 form-control-label">
                                                                        Call to SAM *
                                                                    </label>
                                                                    <div class="col-sm-9">
                                                                        <asp:DropDownList ID="ddlsamper" runat="server" class="form-control">
                                                                            <asp:ListItem Value="01">Select Call to SAM</asp:ListItem>
                                                                            <asp:ListItem Value="02">Yes</asp:ListItem>
                                                                            <asp:ListItem Value="03">No</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group row">
                                                                    <label class="col-sm-3 form-control-label">
                                                                        Call to TL *
                                                                    </label>
                                                                    <div class="col-sm-9">
                                                                        <asp:DropDownList ID="ddltlper" runat="server" class="form-control">
                                                                            <asp:ListItem Value="01">Select Call to TL</asp:ListItem>
                                                                            <asp:ListItem Value="02">Yes</asp:ListItem>
                                                                            <asp:ListItem Value="03">No</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group row">
                                                                    <label class="col-sm-3 form-control-label">
                                                                        Call to Airtel Process Owner Rcpl *
                                                                    </label>
                                                                    <div class="col-sm-9">
                                                                        <asp:DropDownList ID="ddlairper" runat="server" class="form-control">
                                                                            <asp:ListItem Value="01">Select Call to Airtel Process Owner Rcpl</asp:ListItem>
                                                                            <asp:ListItem Value="02">Yes</asp:ListItem>
                                                                            <asp:ListItem Value="03">No</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group row">
                                                                    <label class="col-sm-3 form-control-label">
                                                                        Call to Airtel Manager *
                                                                    </label>
                                                                    <div class="col-sm-9">
                                                                        <asp:DropDownList ID="ddlairmana" runat="server" class="form-control">
                                                                            <asp:ListItem Value="01">Select Call to Airtel Manager</asp:ListItem>
                                                                            <asp:ListItem Value="02">Yes</asp:ListItem>
                                                                            <asp:ListItem Value="03">No</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group row">
                                                                    <label class="col-sm-3 form-control-label">
                                                                        Enter Reason Remarks *
                                                                    </label>
                                                                    <div class="col-sm-9">
                                                                        <asp:TextBox ID="txtremarkupdate" runat="server" class="form-control" Height="70px"
                                                                            placeholder="Enter Reason*" TextMode="MultiLine">
                                                                        </asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group row">
                                                                    <asp:Button ID="btnentrynotallwo" runat="server" class="btn btn-primary btn-sm pull-right"
                                                                        OnClick="btnentrynotallwo_Click" Text="Submit Status" />
                                                                </div>
                                                            </div>
                                                            <div class="clearfix pb5">
                                                            </div>
                                                            <div id="divcustomerdeniedforaudit" runat="server" visible="false">
                                                                <div class="form-group row">
                                                                    <label class="col-sm-3 form-control-label">
                                                                        Enter Reason Remarks *
                                                                    </label>
                                                                    <div class="col-sm-9">
                                                                        <asp:TextBox ID="txtreasoneremarkauditcustomer" runat="server" class="form-control"
                                                                            Height="70px" placeholder="Enter Reason" TextMode="MultiLine">
                                                                        </asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group row">
                                                                    <label class="col-sm-3 form-control-label">
                                                                        Enter Person Met Name
                                                                    </label>
                                                                    <div class="col-sm-9">
                                                                        <asp:TextBox ID="txtcustomerdeniedmetname" runat="server" placeholder="Enter Person Met Name"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group row">
                                                                    <label class="col-sm-3 form-control-label">
                                                                        Enter Person Met Designation
                                                                    </label>
                                                                    <div class="col-sm-9">
                                                                        <asp:TextBox ID="txtcustomerdeniedmetdesi" runat="server" placeholder="Enter Person Met Designation"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group row">
                                                                    <label class="col-sm-3 form-control-label">
                                                                        Enter Person Met Email
                                                                    </label>
                                                                    <div class="col-sm-9">
                                                                        <asp:TextBox ID="txtcustomerdeniedmetemail" runat="server" placeholder="Enter Person Met Email"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="Clearfix pb5">
                                                                </div>
                                                                <div class="form-group row">
                                                                    <asp:Button ID="btnsubcustdeniedaudit" runat="server" class="btn btn-primary btn-sm pull-right"
                                                                        OnClick="btnsubcustdeniedaudit_Click" Text="Submit Status" />
                                                                </div>
                                                            </div>
                                                            <div class="clearfix pb10">
                                                            </div>
                                                            <div class="form-group row" runat="server" id="panaddressorcompclose" visible="false">
                                                                <label class="col-sm-3 form-control-label">
                                                                    Enter Remarks *
                                                                </label>
                                                                <div class="col-sm-9">
                                                                    <asp:TextBox ID="txtremarksaddressnotfoundorcompclose" runat="server" TextMode="MultiLine"
                                                                        Height="100px" class="form-control" placeholder="Remearks"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="clearfix pb10">
                                                            </div>
                                                            <div class="form-group row">
                                                                <asp:Button ID="btnsubmit" runat="server" class="btn btn-primary pull-right mr10"
                                                                    OnClick="btnsubmit_Click" Text="Submit Status" Visible="false" />
                                                            </div>
                                                            <div class="clearfix pb10">
                                                            </div>
                                                            <div class="modal-footer">
                                                                <button class="btn btn-default" data-dismiss="modal" type="button">
                                                                    Close
                                                                </button>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <%--<asp:AsyncPostBackTrigger ControlID="ddlCountry" EventName="SelectedIndexChanged" />
                                                            <asp:AsyncPostBackTrigger ControlID="btnsubmit" EventName="Click" />--%>
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="updatepanel1">
                <ProgressTemplate>
                    <div class="overlay  text-center">
                        <div style="z-index: 999; margin-left: 650px; margin-top: 300px; opacity: 0.3; -moz-opacity: 0.3;">
                            <img alt="" src="img/loader.gif" />
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
    </div>
</asp:Content>
