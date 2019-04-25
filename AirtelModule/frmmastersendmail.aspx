<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmmastersendmail.aspx.cs"
    Inherits="frmmastersendmail" MasterPageFile="~/MasterPage2.master" %>

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
            margin-left: -250px;
            margin-top: 0px;
        }
    </style>
    <!----------------------------End----------------------------------->
    <!------------------------------------CheckGrid----------------------------------------------------------->
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $("[id*=chkHeader]").live("click", function () {
            var chkHeader = $(this);
            var grid = $(this).closest("table");
            $("input[type=checkbox]", grid).each(function () {
                if (chkHeader.is(":checked")) {
                    $(this).attr("checked", "checked");
                    $("td", $(this).closest("tr")).addClass("selected");
                } else {
                    $(this).removeAttr("checked");
                    $("td", $(this).closest("tr")).removeClass("selected");
                }
            });
        });
        $("[id*=chkRow]").live("click", function () {
            var grid = $(this).closest("table");
            var chkHeader = $("[id*=chkHeader]", grid);
            if (!$(this).is(":checked")) {
                $("td", $(this).closest("tr")).removeClass("selected");
                chkHeader.removeAttr("checked");
            } else {
                $("td", $(this).closest("tr")).addClass("selected");
                if ($("[id*=chkRow]", grid).length == $("[id*=chkRow]:checked", grid).length) {
                    chkHeader.attr("checked", "checked");
                }
            }
        });
    </script>
    <!------------------------------------------------End CheckListBox------------------------------------------------->
</asp:Content>
<asp:Content ID="contentmain" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="loader">
    </div>
    <div class="right_col" role="main">
        <div class="">
            <div class="page-title">
                <div class="title_left">
                    <h3>
                        Send Mail</h3>
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
                                <div class="col-sm-12">
                                    <div class="form-group row">
                                        <div class="col-sm-4">
                                            <label class=" form-control-label">
                                                Select Audit Case</label>
                                            <asp:DropDownList ID="ddlauditcase" runat="server" class="form-control">
                                                <asp:ListItem Selected="True" Value="01">Select Audit Case</asp:ListItem>
                                                <asp:ListItem Value="Negative">Negative</asp:ListItem>
                                                <asp:ListItem Value="Pending">Revisit Pending</asp:ListItem>
                                                <asp:ListItem Value="03">Remaining for audit</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-4">
                                            <label class="form-control-label">
                                                Enter Keyword for Search</label>
                                            <div class="">
                                                <asp:TextBox ID="txtsearch" runat="server" class="form-control" Placeholder="Logical Circuit ID,Customer Name"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <label class="form-control-label">
                                                Submit and Send Mail</label>
                                            <div class="">
                                                <asp:Button ID="btnsubmitorsend" runat="server" Text="Submit and Send Mail" class="btn btn-sm btn-block btn-primary"
                                                    OnClick="btnsubmitorsend_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="Clearfix pb10">
                                </div>
                                <div runat="server" id="divgrid" visible="false">
                                    <div class="col-sm-12">
                                        <div class="col-sm-9">
                                            <asp:Label ID="lbltotal" runat="server" class="btn btn-sm btn-warning pull-right"></asp:Label>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:Button ID="btnsendmailview" runat="server" class="btn btn-sm btn-success btn-block pull-right"
                                                Text="Send Mail" OnClick="btnsendmailview_Click" />
                                        </div>
                                    </div>
                                    <div class="Clearfix pb5">
                                    </div>
                                    <asp:GridView ID="gvexcel" runat="server" AutoGenerateColumns="false" class="table table-hover"
                                        OnRowCommand="gvexcel_RowCommand">
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkHeader" runat="server" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkRow" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Allocated Date" HeaderText="Allocated Date" DataFormatString="{0:dd/MMM/yyyy}"
                                                NullDisplayText="#" />
                                            <asp:TemplateField HeaderText="ComplainceDate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcompdate" runat="server" Text='<%#Eval("ComplainceDate","{0:dd/MMM/yyyy}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Logical Circuit Id" HeaderText="Logical Circuit Id" NullDisplayText="#" />
                                            <asp:BoundField DataField="Customer Name" HeaderText="Customer Name" NullDisplayText="#" />
                                            <asp:BoundField DataField="Party Name" HeaderText="Company Name" NullDisplayText="#" />
                                            <asp:BoundField DataField="BILLING_BANDWIDTH" HeaderText="BILLING BANDWIDTH" NullDisplayText="#" />
                                            <asp:BoundField DataField="StatusUpdateDate" HeaderText="Status Update Date" DataFormatString="{0:dd/MMM/yyyy}"
                                                NullDisplayText="#" />
                                            <asp:BoundField DataField="StatusByFE" HeaderText="Status" NullDisplayText="#" />
                                            <asp:TemplateField HeaderText="Product Name">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblproduct" Text='<%#Eval("ProductName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ProdTypeName" HeaderText="PRODUCT TYPE" NullDisplayText="#" />
                                            <asp:TemplateField HeaderText="View Detail">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblview" runat="server" class="fa fa-eye fa-2x" CommandArgument='<%#Eval("UniqueID") %>'
                                                        CommandName="ViewDetail"></asp:LinkButton>
                                                    <asp:HiddenField ID="hdnIDGridRawData" runat="server" Value='<%#Eval("Logical Circuit Id") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
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
                                        </div>
                                        <asp:UpdatePanel ID="up" runat="server" ChildrenAsTriggers="true">
                                            <ContentTemplate>
                                                <div runat="server" id="divsentmailclient" visible="false">
                                                    <div id="divmssgpopup" runat="server">
                                                    </div>
                                                    <div class="clearfix pb10">
                                                    </div>
                                                    <asp:Label ID="lbltotalselctedrow" runat="server" class="btn btn-block btn-sm btn-warning"></asp:Label>
                                                    <div class="clearfix pb10">
                                                    </div>
                                                    <p>
                                                        Select Send Mail Process</p>
                                                    <asp:DropDownList ID="ddlendmailprocess" runat="server" class="form-control">
                                                        <asp:ListItem Selected="True" Value="01">Select Send Mail Process</asp:ListItem>
                                                        <asp:ListItem Value="Airtel">Airtel</asp:ListItem>
                                                        <asp:ListItem Value="Client">Client</asp:ListItem>
                                                        <asp:ListItem Value="Both">Client or Airtel Both</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <div class="clearfix pb10">
                                                    </div>
                                                    <p>
                                                        Select Receiver Email ID's</p>
                                                    <asp:CheckBoxList ID="chkreceiver" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                                                        RepeatLayout="Flow">
                                                    </asp:CheckBoxList>
                                                    <div class="clearfix pb10">
                                                    </div>
                                                    <p>
                                                        Select CC Receiver Email ID's</p>
                                                    <asp:CheckBoxList ID="chkccrecemail" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                                                        RepeatLayout="Flow">
                                                    </asp:CheckBoxList>
                                                    <div class="clearfix pb10">
                                                    </div>
                                                    <asp:Button ID="btnsend" runat="server" class="btn btn-sm btn-primary pull-right mr10"
                                                        Text="Send Mail" OnClick="btnsend_Click" />
                                                    <div class="clearfix pb10">
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                    </Triggers>
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
