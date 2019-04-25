<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AssignJobPRI.aspx.cs" Inherits="AssignJobPRI"
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
            left: 0px;
            top: 0px;
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.2;
            -moz-opacity: 0.2;
        }
    </style>
    <!----------------------------End----------------------------------->
    <script type="text/javascript">
        function googleTranslateElementInit() {
            new google.translate.TranslateElement({
                pageLanguage: 'en'
            }, 'google_translate_element');
        }
    </script>
    <script type="text/javascript" src="//translate.google.com/translate_a/element.js?cb=googleTranslateElementInit"></script>
</asp:Content>
<asp:Content ID="contentmain" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="loader">
    </div>
    <div class="right_col" role="main">
        <div class="page-title">
            <div class="title_left">
                <h3>
                    Assign Job-PRI</h3>
            </div>
            <div class="pull-right">
                <div id="google_translate_element">
                    <div class="skiptranslate goog-te-gadget" dir="ltr">
                    </div>
                </div>
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
                                    <div class="label label-info fs14">
                                        Today's Prescheduled Appointments.</div>
                                    <div class="label label-info ml10 fs14" runat="server" id="totalcount">
                                    </div>
                                </div>
                                <div class="clearfix pb10">
                                </div>
                                <div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-6 row">
                                            <asp:TextBox ID="txtsearch" runat="server" class="form-control" Placeholder="Enter Subs Del No or Party Name"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2 row">
                                            <asp:Button ID="btnsearch" runat="server" class="btn btn-success" OnClick="btnsearch_Click"
                                                Text="Search" />
                                        </div>
                                        <div class="col-sm-4 row">
                                            <asp:Label ID="lblcounttotal" runat="server" class="label label-primary fs14 pull-right"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="clearfix pb10">
                                    </div>
                                    <div class="table-responsive">
                                        <asp:HiddenField ID="hfFeName" runat="server" />
                                        <asp:HiddenField ID="hfbilledextid1" runat="server" />
                                        <asp:HiddenField ID="hfotp" runat="server" />
                                        <asp:HiddenField ID="hfcustomerorpartyname" runat="server" />
                                        <asp:HiddenField ID="hfNameCustomer" runat="server" />
                                        <asp:HiddenField ID="hflogicalcircuitid" runat="server" />
                                        <asp:HiddenField ID="hfPartyName" runat="server" />
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvexcel" runat="server" AutoGenerateColumns="false" class="table text-nowrap"
                                                OnRowCommand="gvexcel_RowCommand" OnRowDataBound="gvexcel_RowDataBound">
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
                                                            <asp:HiddenField ID="hfbilledextid" runat="server" Value='<%#Eval("[Subs Del No]") %>' />
                                                            <asp:HiddenField ID="hfstatusfe" runat="server" Value='<%#Eval("StatusByFE") %>' />
                                                            <asp:HiddenField ID="hfemail" runat="server" Value='<%#Eval("[Cust Email]") %>' />
                                                            <asp:HiddenField ID="Customer" runat="server" Value='<%#Eval("[Customer Name]") %>' />
                                                            <asp:HiddenField ID="hfrevisitdate" runat="server" Value='<%#Eval("[Revisit Date Time]","{0:dd/MMM/yyyy}") %>' />
                                                            <asp:HiddenField ID="hfPreMeetingDate" runat="server" Value='<%#Eval("PreMeetingDate","{0:dd/MMM/yyyy}") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Visit Company Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblcompnamemo" runat="server" Text='<%# Eval("Party Name").ToString() == "" ? "#" : Eval("Party Name").ToString()%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Subs Del No" HeaderText="Subs Del No" NullDisplayText="#" />
                                                    <asp:BoundField DataField="Billing Address" HeaderText="Customer Address-1" NullDisplayText="#" />
                                                    <asp:BoundField DataField="PRODUCT_TYPE" HeaderText="Product Name" NullDisplayText="#" />
                                                    <asp:BoundField DataField="ProdTypeName" HeaderText="Type" NullDisplayText="#" />
                                                    <asp:BoundField DataField="Segment" HeaderText="Segment" NullDisplayText="#" />
                                                    <asp:BoundField DataField="Status" HeaderText="Status" NullDisplayText="#" />
                                                    <asp:BoundField DataField="Coordinator_Name" HeaderText="Coordinator Name" NullDisplayText="#" />
                                                    <asp:BoundField DataField="Coordinator_Contact_number" HeaderText="Coordinator Number"
                                                        NullDisplayText="#" />
                                                    <asp:TemplateField HeaderText="Appoinment Mail Date or Email">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblappointmentmailsenddate" runat="server" Text='<%# Eval("AppointmentMailDatetime", "{0:dd/MMM/yyyy}").ToString() == "" ? "#" : Eval("AppointmentMailDatetime", "{0:dd/MMM/yyyy}").ToString()%>'></asp:Label>
                                                            <div class="clearfix">
                                                            </div>
                                                            <asp:Label ID="lblemailcustomer" runat="server" Text='<%#Eval("[Cust Email]") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Compliance Date">
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
                                                                    Bill Plan</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("Bill Plan").ToString() == "" ? "#" : Eval("Bill Plan").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    Bill_City</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("Bill_City").ToString() == "" ? "#" : Eval("Bill_City").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    Billed Ext Id</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("Billed Ext Id").ToString() == "" ? "#" : Eval("Billed Ext Id").ToString()%>
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
                                                                    Complaince Date</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("FEComplianceDate", "{0:dd/MMM/yyyy}").ToString() == "" ? "#" : Eval("FEComplianceDate", "{0:dd/MMM/yyyy}").ToString()%>
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
                                                                    Coordinator_Contact_Email</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("Coordinator_Contact_Email").ToString() == "" ? "#" : Eval("Coordinator_Contact_Email").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    Coordinator_Contact_Number</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("Coordinator_Contact_Number").ToString() == "" ? "#" : Eval("Coordinator_Contact_Number").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    Coordinator_Name</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("Coordinator_Name").ToString() == "" ? "#" : Eval("Coordinator_Name").ToString()%>
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
                                                                    Record Inserted Date</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("InsertedDate", "{0:dd/MMM/yyyy}").ToString() == "" ? "#" : Eval("InsertedDate", "{0:dd/MMM/yyyy}").ToString()%>
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
                                                                    Orig Service Start Date
                                                                </h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("Orig Service Start Date").ToString() == "" ? "#" : Eval("Orig Service Start Date").ToString()%>
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
                                                                    Phone Nos</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("Phone Nos").ToString() == "" ? "#" : Eval("Phone Nos").ToString()%>
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
                                                                    Send Mail to SAM</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("SentSamMail").ToString() == "" ? "#" : Eval("SentSamMail").ToString()%>
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
                                                                    Segment</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("Segment").ToString() == "" ? "#" : Eval("Segment").ToString()%>
                                                            </div>
                                                            <div class="clearfix pb-2">
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <h4>
                                                                    Status</h4>
                                                            </div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("Status").ToString() == "" ? "#" : Eval("Status").ToString()%>
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
                                                <div id="div2" runat="server">
                                                    <asp:UpdatePanel ID="upanel" runat="server" ChildrenAsTriggers="true">
                                                        <ContentTemplate>
                                                            <div class="clearfix pb5">
                                                            </div>
                                                            <div id="divmssgpopup" runat="server">
                                                            </div>
                                                            <div class="clearfix pb5">
                                                            </div>
                                                            <asp:Panel ID="panmoallpanel" runat="server" Visible="false">
                                                                <div>
                                                                    <asp:HiddenField ID="hflocsrid" runat="server" />
                                                                    <asp:HiddenField ID="hfEmailCustomer" runat="server" />
                                                                    <%--<asp:HiddenField ID="hfNameCustomer" runat="server" />--%>
                                                                    <asp:HiddenField ID="hfcompname" runat="server" />
                                                                    <p class="pull-right">
                                                                        <strong>Mark with * field fill mandatory</strong></p>
                                                                    <div class="clearfix pb5">
                                                                    </div>
                                                                    <div class="form-group row">
                                                                        <label class="col-sm-3 form-control-label">
                                                                            Select Audit Case *
                                                                        </label>
                                                                        <div class="col-sm-9">
                                                                            <asp:DropDownList ID="ddlcondition" runat="server" AutoPostBack="true" class="form form-control"
                                                                                OnSelectedIndexChanged="ddlcondition_SelectedIndexChanged">
                                                                                <asp:ListItem Selected="True" Value="01">Select audit case</asp:ListItem>
                                                                                <asp:ListItem Value="02">Customer Shifted</asp:ListItem>
                                                                                <asp:ListItem Value="03">Company Closed</asp:ListItem>
                                                                                <asp:ListItem Value="12">Address Not Found</asp:ListItem>
                                                                                <asp:ListItem Value="04">Connection Disconnected Voluntarily</asp:ListItem>
                                                                                <asp:ListItem Value="05">Entry Not Allowed</asp:ListItem>
                                                                                <asp:ListItem Value="06">Customer Denied Audit</asp:ListItem>
                                                                                <asp:ListItem Value="07">Not in Use</asp:ListItem>
                                                                                <asp:ListItem Value="08">Already Done</asp:ListItem>
                                                                                <asp:ListItem Value="09">Negative RM Approval</asp:ListItem>
                                                                                <asp:ListItem Value="10">Revisit Required</asp:ListItem>
                                                                                <asp:ListItem Value="11">None of the Above</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                    <div class="clearfix pb10">
                                                                    </div>
                                                                    <asp:Panel ID="pannegativecondition" runat="server" Visible="false">
                                                                        <asp:Panel ID="pannegativefirststep" runat="server">
                                                                            <div id="diventrynotallowd" runat="server" visible="false">
                                                                                <div class="form-group row">
                                                                                    <label class="col-sm-3 form-control-label">
                                                                                        Call to Coordinator *
                                                                                    </label>
                                                                                    <div class="col-sm-9">
                                                                                        <asp:DropDownList ID="ddltlper" runat="server" class="form-control">
                                                                                            <asp:ListItem Value="01">Select Call to Coordinator</asp:ListItem>
                                                                                            <asp:ListItem Value="02">Yes</asp:ListItem>
                                                                                            <asp:ListItem Value="03">No</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="form-group row">
                                                                                    <label class="col-sm-3 form-control-label">
                                                                                        Call to RM *
                                                                                    </label>
                                                                                    <div class="col-sm-9">
                                                                                        <asp:DropDownList ID="ddlsamper" runat="server" class="form-control">
                                                                                            <asp:ListItem Value="01">Select Call to RM</asp:ListItem>
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
                                                                            </div>
                                                                            <div id="divcustomershifted" runat="server" class="form-group row" visible="false">
                                                                                <label class="col-sm-3 form-control-label">
                                                                                    Enter Address *
                                                                                </label>
                                                                                <div class="col-sm-9">
                                                                                    <asp:TextBox ID="txtnewaddress" runat="server" class="form-control" Height="70px"
                                                                                        placeholder="Enter Address" TextMode="MultiLine"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <div id="divnegativeotp" runat="server">
                                                                                <div id="Div1" runat="server" class="form-group row">
                                                                                    <label class="col-sm-3 form-control-label">
                                                                                        Enter Remarks *
                                                                                    </label>
                                                                                    <div class="col-sm-9">
                                                                                        <asp:TextBox ID="txtremarkcust" runat="server" class="form-control" Height="70px"
                                                                                            placeholder="Enter Remarks" TextMode="MultiLine"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="form-group row">
                                                                                    <label class="col-sm-3 form-control-label">
                                                                                        Person Met Name
                                                                                    </label>
                                                                                    <div class="col-sm-9">
                                                                                        <asp:TextBox ID="txtpersonmetname" runat="server" class="form-control" placeholder="Person Met Name"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="form-group row">
                                                                                    <label class="col-sm-3 form-control-label">
                                                                                        Person Met Designation
                                                                                    </label>
                                                                                    <div class="col-sm-9">
                                                                                        <asp:TextBox ID="txtpersonmetdesig" runat="server" class="form-control" placeholder="Person Met Designation"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="form-group row">
                                                                                    <label class="col-sm-3 form-control-label">
                                                                                        Person Met Email-ID
                                                                                    </label>
                                                                                    <div class="col-sm-9">
                                                                                        <asp:TextBox ID="txtpersonmetemailid" runat="server" class="form-control" placeholder="Person Met Email-ID"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="form-group row">
                                                                                    <label class="col-sm-3 form-control-label">
                                                                                        Person Met Mobile-No
                                                                                    </label>
                                                                                    <div class="col-sm-9">
                                                                                        <asp:TextBox ID="txtpersonmetmobno" runat="server" class="form-control" placeholder="Person Met Mobile-No"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="clearfix pb5">
                                                                            </div>
                                                                            <asp:LinkButton ID="btnnextnegative" runat="server" class="btn btn-primary btn-sm pull-right mr5"
                                                                                OnClick="btnnextnegative_Click" Text="Next"></asp:LinkButton>
                                                                            <div class="clearfix pb5">
                                                                            </div>
                                                                        </asp:Panel>
                                                                        <asp:Panel ID="pannegativesecondstep" runat="server" Visible="false">
                                                                            <div class="form-group row">
                                                                                <label class="col-sm-3 form-control-label">
                                                                                    <h4>
                                                                                        Select Mobile</h4>
                                                                                </label>
                                                                                <div class="col-sm-9">
                                                                                    <div class="col-sm-2">
                                                                                        <asp:CheckBox ID="chkotpshowddl" runat="server" AutoPostBack="true" Checked="false"
                                                                                            OnCheckedChanged="chkotpshowddl_CheckedChanged" />
                                                                                    </div>
                                                                                    <div class="col-sm-7">
                                                                                        <asp:DropDownList ID="ddlnum" runat="server" class="form-control" Enabled="false">
                                                                                            <asp:ListItem Value="OTP">Select OTP Contact</asp:ListItem>
                                                                                            <asp:ListItem Value="Rahul">7053919406</asp:ListItem>
                                                                                            <asp:ListItem Value="Soni">9711616720</asp:ListItem>
                                                                                            <asp:ListItem Value="Gagan">9811020978</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </div>
                                                                                    <div class="col-sm-3">
                                                                                        <asp:Button ID="sendotp" runat="server" Class="btn btn-sm btn-primary" Enabled="false"
                                                                                            OnClick="sendotp_Click" Text="Send OTP" />
                                                                                        <div class="clearfix pb5">
                                                                                        </div>
                                                                                        <asp:LinkButton ID="lblresendotpnegative" runat="server" class="links pull-right mr5"
                                                                                            Enabled="false" OnClick="lblresendotpnegative_Click">Resend OTP</asp:LinkButton>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group row">
                                                                                <label class="col-sm-3 form-control-label">
                                                                                    <h4>
                                                                                        Enter OTP *</h4>
                                                                                </label>
                                                                                <div class="col-sm-9">
                                                                                    <asp:TextBox ID="txtotpnegative" runat="server" placeholder="Enter OTP"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <div class="clearfix pb5">
                                                                            </div>
                                                                            <asp:Button ID="btnsubmitnegative" runat="server" class="btn btn-success btn-sm pull-right mr5"
                                                                                OnClick="btnsubmitnegative_Click" Text="Submit Case" />
                                                                            <div class="clearfix pb5">
                                                                            </div>
                                                                        </asp:Panel>
                                                                    </asp:Panel>
                                                                    <asp:Panel runat="server" ID="panaddressorcompclose">
                                                                        <div class="form-group row">
                                                                            <label class="col-sm-3 form-control-label">
                                                                                Enter Remarks *
                                                                            </label>
                                                                            <div class="col-sm-9">
                                                                                <asp:TextBox ID="txtremarksaddressnotfoundorcompclose" runat="server" TextMode="MultiLine"
                                                                                    Height="100px" class="form-control" placeholder="Remearks"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group row">
                                                                            <asp:LinkButton ID="btnsubremarksaddressnotfoundorcompclose" runat="server" class="btn btn-primary pull-right mr10"
                                                                                Text="Submit" OnClick="btnsubremarksaddressnotfoundorcompclose_Click"></asp:LinkButton>
                                                                        </div>
                                                                    </asp:Panel>
                                                                    <asp:Panel ID="panrevisitcondition" runat="server" Visible="false">
                                                                        <div id="revisittimedate" runat="server">
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
                                                                                    Person Met Name
                                                                                </label>
                                                                                <div class="col-sm-9">
                                                                                    <asp:TextBox ID="txtrevisitpersonmetname" runat="server" class="form-control" placeholder="Person Met Name"
                                                                                        reuqired=""></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group row">
                                                                                <label class="col-sm-3 form-control-label">
                                                                                    Person Met Email-ID
                                                                                </label>
                                                                                <div class="col-sm-9">
                                                                                    <asp:TextBox ID="txtrevisitpersonmetemail" runat="server" class="form-control" placeholder="Person Met Email-ID"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group row">
                                                                                <label class="col-sm-3 form-control-label">
                                                                                    Person Met Designation
                                                                                </label>
                                                                                <div class="col-sm-9">
                                                                                    <asp:TextBox ID="txtrevisitpersonmetdesig" runat="server" class="form-control" placeholder="Person Met Designation"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group row">
                                                                                <label class="col-sm-3 form-control-label">
                                                                                    Person Met Mobile No
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
                                                                                    Enter OTP *</h4>
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
                                                                            <asp:LinkButton ID="lbresendotprevisit" runat="server" class="blue pull-right mr5"
                                                                                OnClick="lbresendotprevisit_Click">Resend OTP</asp:LinkButton>
                                                                        </div>
                                                                    </asp:Panel>
                                                                    <asp:Panel ID="pansuccess" runat="server" Visible="false">
                                                                        <div id="divsuccessfirst" runat="server">
                                                                            <div class="form-group row">
                                                                                <label class="col-sm-3 form-control-label">
                                                                                    <h4>
                                                                                        Person Met Name</h4>
                                                                                </label>
                                                                                <div class="col-sm-9">
                                                                                    <asp:TextBox ID="txtpersonmetnamesuccess" runat="server" class="form-control" placeholder="Person Met Name"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group row">
                                                                                <label class="col-sm-3 form-control-label">
                                                                                    <h4>
                                                                                        Person Met Email-ID</h4>
                                                                                </label>
                                                                                <div class="col-sm-9">
                                                                                    <asp:TextBox ID="txtpersonmetemailidsuccess" runat="server" class="form-control"
                                                                                        placeholder="Person Met Email-ID"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group row">
                                                                                <label class="col-sm-3 form-control-label">
                                                                                    <h4>
                                                                                        Person Met Designation</h4>
                                                                                </label>
                                                                                <div class="col-sm-9">
                                                                                    <asp:TextBox ID="txtsuccpersonmetdesig" runat="server" class="form-control" placeholder="Person Met Designation"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group row">
                                                                                <label class="col-sm-3 form-control-label">
                                                                                    <h4>
                                                                                        Person Met Mobile-No</h4>
                                                                                </label>
                                                                                <div class="col-sm-9">
                                                                                    <asp:TextBox ID="txtpersonemetmobilenosuccess" runat="server" class="form-control"
                                                                                        placeholder="Person Met mobile-No"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <div class="clearfix pb5">
                                                                            </div>
                                                                            <asp:Button ID="btnsuccessotpstep1" runat="server" class="btn btn-primary pull-right mr5"
                                                                                OnClick="btnsuccessotpstep1_Click" Text="Next" />
                                                                            <div class="clearfix pb5">
                                                                            </div>
                                                                        </div>
                                                                        <div id="divsuccessoptorsubmit" runat="server" visible="false">
                                                                            <div class="form-group row">
                                                                                <label class="col-sm-3 form-control-label">
                                                                                    <h4>
                                                                                        Select Mobile</h4>
                                                                                </label>
                                                                                <div class="col-sm-9">
                                                                                    <div class="col-sm-2">
                                                                                        <asp:CheckBox ID="chkboxposi" runat="server" AutoPostBack="true" Checked="false"
                                                                                            OnCheckedChanged="chkboxposi_CheckedChanged" />
                                                                                    </div>
                                                                                    <div class="col-sm-7">
                                                                                        <asp:DropDownList ID="ddlnump" runat="server" class="form-control" Enabled="false">
                                                                                            <asp:ListItem Value="OTP">Select OTP Contact</asp:ListItem>
                                                                                            <asp:ListItem Value="Rahul">7053919406</asp:ListItem>
                                                                                            <asp:ListItem Value="Soni">9711616720</asp:ListItem>
                                                                                            <asp:ListItem Value="Gagan">9811020978</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </div>
                                                                                    <div class="col-sm-3">
                                                                                        <asp:Button ID="sendotps" runat="server" Class="btn btn-sm btn-primary" Enabled="false"
                                                                                            OnClick="sendotps_Click" Text="Send OTP" />
                                                                                        <div class="clearfix pb5">
                                                                                        </div>
                                                                                        <asp:LinkButton ID="lblresendotppositive" runat="server" class="links pull-right mr5"
                                                                                            Enabled="false" OnClick="lblresendotppositive_Click">Resend OTP</asp:LinkButton>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-group row">
                                                                                <label class="col-sm-3 form-control-label">
                                                                                    <h4>
                                                                                        Enter OTP *</h4>
                                                                                </label>
                                                                                <div class="col-sm-9">
                                                                                    <asp:TextBox ID="txtoptsuces" runat="server" class="form-control" MaxLength="6" placeholder="Enter OTP"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                            <div class="clearfix pb5">
                                                                            </div>
                                                                            <asp:Button ID="btnsuccsubmit" runat="server" class="btn btn-success btn-sm pull-right mr5"
                                                                                OnClick="btnsuccsubmit_Click" Text="Success" />
                                                                            <div class="clearfix pb5">
                                                                            </div>
                                                                        </div>
                                                                    </asp:Panel>
                                                                </div>
                                                            </asp:Panel>
                                                            <div class="clearfix pb5">
                                                            </div>
                                                            <div class="modal-footer">
                                                                <button class="btn btn-default" data-dismiss="modal" type="button">
                                                                    Close
                                                                </button>
                                                            </div>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>
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
</asp:Content>
