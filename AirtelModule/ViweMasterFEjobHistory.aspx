<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViweMasterFEjobHistory.aspx.cs"
    Inherits="ViweMasterFEjobHistory" MasterPageFile="~/MasterPage2.master" %>

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
            z-index: 999999;
            height: 100%;
            width: 100%;
            top: 0;
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.2;
            -moz-opacity: 0.2;
            margin-left: -300px;
            margin-top: 0px;
        }
    </style>
    <!----------------------------End----------------------------------->
</asp:Content>
<asp:Content ID="contentmain" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="loader">
    </div>
    <div class="right_col" role="main">
        <div class="">
            <div class="page-title">
                <div class="title_left">
                    <h3>
                        View Detail History of Assigned FE Data</h3>
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
                                </div>
                                <div class="form-group row" runat="server" id="divlastjobdonesearh">
                                    <h4>
                                        Field with mark * are mandatory</h4>
                                    <div class="clearfix">
                                    </div>
                                    <div class="col-sm-3">
                                        <label class="col-sm-12 form-control-label">
                                            Select Start Date *</label>
                                        <div class="col-sm-12">
                                            <asp:TextBox ID="txtdate1" runat="server" class="form-control" type="date"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <label class="col-sm-12 form-control-label">
                                            Select End Date *</label>
                                        <div class="col-sm-12">
                                            <asp:TextBox ID="txtenddate1" runat="server" class="form-control" type="date"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <label class="col-sm-12 form-control-label">
                                            Select FE
                                        </label>
                                        <div class="col-sm-12">
                                            <asp:DropDownList ID="ddlfe1" runat="server" class="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <label class="col-sm-12 form-control-label">
                                            Select Audit Status
                                        </label>
                                        <div class="col-sm-12">
                                            <asp:DropDownList ID="ddlauditstatus" runat="server" class="form-control">
                                                <asp:ListItem Selected="True" Value="01">Select Audit Status</asp:ListItem>
                                                <asp:ListItem Value="02">All</asp:ListItem>
                                                <asp:ListItem Value="Negative">Negative</asp:ListItem>
                                                <asp:ListItem Value="03">Pending</asp:ListItem>
                                                <asp:ListItem Value="Positive">Positive</asp:ListItem>
                                                <asp:ListItem Value="Pending">Revisit</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="clearfix pb10">
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-9">
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:Button ID="btnsearchlastjob" runat="server" class="btn btn-primary btn-sm btn-block"
                                                Text="Search FE Last Job History" OnClick="btnsearchlastjob_Click" />
                                        </div>
                                    </div>
                                </div>
                                <div class="Clearfix pb5">
                                </div>
                                <div class="bg-white" runat="server" id="divfejobdetail">
                                    <div class=" bg-info p10">
                                        <div>
                                            <div class="col-sm-12">
                                                <div class="col-sm-9">
                                                    <asp:Label ID="lbltotal" runat="server" class="btn btn-primary pull-right"></asp:Label>
                                                </div>
                                                <div class="col-sm-3">
                                                    <asp:DropDownList ID="ddlsort" runat="server" AutoPostBack="true" class="form-control pull-right"
                                                        OnSelectedIndexChanged="ddlsort_SelectedIndexChanged">
                                                        <asp:ListItem Selected="True" Value="01">Sort</asp:ListItem>
                                                        <asp:ListItem Value="[Customer Name]">Customer Name</asp:ListItem>
                                                        <asp:ListItem Value="[Logical Circuit ID]">Logical Circuit ID</asp:ListItem>
                                                        <asp:ListItem Value="[Allocated Date]">Date</asp:ListItem>
                                                        <asp:ListItem Value="[FE Name]">FE Name</asp:ListItem>
                                                        <asp:ListItem Value="[From Site]">Address</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="Clearfix pb5">
                                        </div>
                                        <asp:GridView ID="gvfedetaillist" runat="server" class="table table-responsive table-hover table-bordered table-condensed"
                                            AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Logical Circuit Id" HeaderText="Logical Circuit ID" NullDisplayText="#" />
                                                <asp:BoundField DataField="Party Name" HeaderText="Company Name" NullDisplayText="#" />
                                                <asp:BoundField DataField="ProductName" HeaderText="Product" NullDisplayText="#" />
                                                <asp:BoundField DataField="ProdTypeName" HeaderText="Product Type" NullDisplayText="#" />
                                                <asp:BoundField DataField="BILLING_BANDWIDTH" HeaderText="Bandwidth" NullDisplayText="#" />
                                                <asp:BoundField DataField="Commissioning Date" DataFormatString="{0:dd/MMM/yyyy}"
                                                    HeaderText="Commissioning Date" NullDisplayText="#" />
                                                <asp:BoundField DataField="Account Manager" HeaderText="Account Manager" NullDisplayText="#" />
                                                <asp:TemplateField HeaderText="Compliance Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblcomp" runat="server" Text='<%# Eval("FEComplianceDate", "{0:dd/MMM/yyyy}").ToString() == "" ? "#" : Eval("FEComplianceDate", "{0:dd/MMM/yyyy}").ToString()%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status Update Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblcompdate" runat="server" Text='<%# Eval("StatusUpdateDate", "{0:dd/MMM/yyyy}").ToString() == "" ? "#" : Eval("StatusUpdateDate", "{0:dd/MMM/yyyy}").ToString()%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status Update Time">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblcomptime" runat="server" Text='<%# Eval("StatusTime").ToString() == "" ? "#" : Eval("StatusTime").ToString()%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="StatusByFE" HeaderText="Status" NullDisplayText="#" ItemStyle-ForeColor="Red" />
                                            </Columns>
                                            <EditRowStyle BackColor="#2461BF" />
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#EFF3FB" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div id="myModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel"
                                    aria-hidden="true" class="modal fade text-left">
                                    <div role="document" class="modal-dialog">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <button type="button" data-dismiss="modal" aria-label="Close" class="close">
                                                    <span aria-hidden="true">×</span></button>
                                            </div>
                                            <div class="modal-body">
                                                <div runat="server" id="divshow1">
                                                    <h3>
                                                        A brief detail of client information.</h3>
                                                    <form>
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
                                                                    Alt Phone No</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("Alt Phone1").ToString() == "" ? "#" : Eval("Alt Phone1").ToString()%>
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
                                                                    Bill Plan</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("Bill Plan").ToString() == "" ? "#" : Eval("Bill Plan").ToString()%>
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
                                                                    Circuit BandWidth</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("Circuit BandWidth").ToString() == "" ? "#" : Eval("Circuit BandWidth").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    Commissioning Date</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("Commissioning Date", "{0:dd/MMM/yyyy}").ToString() == "" ? "#" : Eval("Commissioning Date", "{0:dd/MMM/yyyy}").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    Company Name</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("Company Name").ToString() == "" ? "#" : Eval("Company Name").ToString()%>
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
                                                                    Case Eligible/Not Eligible</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("Eligible/Not Eligible").ToString() == "" ? "#" : Eval("Eligible/Not Eligible").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    From Site</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("From Site").ToString() == "" ? "#" : Eval("From Site").ToString()%>
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
                                                                    Sudden Case Pre Meeting Date
                                                                </h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("PreMeetingDate", "{0:dd/MMM/yyyy}").ToString() == "" ? "#" : Eval("PreMeetingDate", "{0:dd/MMM/yyyy}").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    Sudden Case Pre Meeting Time</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("PreMeetingTime", "{0:hh:mm:ss}").ToString() == "" ? "#" : Eval("PreMeetingTime", "{0:hh:mm:ss}").ToString()%>
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
                                                                    Pincode</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("Pincode").ToString() == "" ? "#" : Eval("Pincode").ToString()%>
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
                                                                    Positive/Negative</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("Positive/Negative").ToString() == "" ? "#" : Eval("Positive/Negative").ToString()%>
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
                                                                    Case Reason by FE</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("ReasonOfStatus").ToString() == "" ? "#" : Eval("ReasonOfStatus").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    Status Remarks by FE</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("RemarksOfStatus").ToString() == "" ? "#" : Eval("RemarksOfStatus").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    Pending Case Revisit PerSon Name</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("Revisit PerSon Name").ToString() == "" ? "#" : Eval("Revisit PerSon Name").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    Pending Case Revisit Date Time</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("Revisit Date Time", "{0:dd/MMM/yyyy - hh:mm:ss}").ToString() == "" ? "#" : Eval("Revisit Date Time", "{0:dd/MMM/yyyy - hh:mm:ss}").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    Status By FE</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("StatusByFE").ToString() == "" ? "#" : Eval("StatusByFE").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    Case Status Update Time</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("StatusTime").ToString() == "" ? "#" : Eval("StatusTime").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    Case Status Update Date</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("StatusUpdateDate", "{0:dd/MMM/yyyy}").ToString() == "" ? "#" : Eval("StatusUpdateDate", "{0:dd/MMM/yyyy}").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    Sent Mail to Customer</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("SentCustMail").ToString() == "" ? "#" : Eval("SentCustMail").ToString()%>
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
                                                                    To Site</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("To Site").ToString() == "" ? "#" : Eval("To Site").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:DataList>
                                                    </form>
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
                        <div class="overlay">
                            <div style="z-index: 999; margin-left: 650px; margin-top: 300px; opacity: 0.3; -moz-opacity: 0.3;">
                                <img alt="" src="img/loader.gif" />
                            </div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
        </div>
    </div>
</asp:Content>
