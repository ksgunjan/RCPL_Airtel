<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmVerifiyUploadExcelAfterTL.aspx.cs"
    Inherits="frmVerifiyUploadExcelAfterTL" MasterPageFile="~/MasterPage2.master" %>

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
            margin-left: -280px;
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
                        Assign Job to Field Executive</h3>
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
                                <div class="Clearfix pb5">
                                </div>
                                <div class="form-group row">
                                    <div class="col-sm-9">
                                        <label class="form-control-label">
                                            Enter Allocated Date For Verify Upload Excel</label>
                                        <div>
                                            <asp:TextBox ID="txtdate" runat="server" class="form-control" type="date">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <label class="form-control-label">
                                            Click to find upload record for verify</label>
                                        <div>
                                            <asp:Button ID="btnSearch" runat="server" class="btn btn-sm btn-primary btn-block pull-right"
                                                Text="Search" OnClick="btnSearch_Click" />
                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix line">
                                </div>
                                <div runat="server" id="divbutton" visible="false">
                                    <b>
                                        <asp:Label ID="lbltoalrows" runat="server" class="text-info red"></asp:Label></b>
                                    <asp:Button ID="btndisapprove" runat="server" class="btn btn-danger btn-sm pull-right"
                                        Text="DisApprove" OnClick="btndisapprove_Click" />
                                    <asp:LinkButton ID="btnapprove" runat="server" class="btn btn-success btn-sm pull-right"
                                        Text="Approve" OnClick="btnapprove_Click"></asp:LinkButton>
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
                                        <asp:BoundField DataField="BILLING_BANDWIDTH" HeaderText="BILLING BANDWIDTH" NullDisplayText="#" />
                                        <asp:TemplateField HeaderText="Product Name">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblproduct" Text='<%#Eval("ProductName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ProdTypeName" HeaderText="PRODUCT TYPE" NullDisplayText="#" />
                                        <asp:TemplateField HeaderText="FE Name">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblfename" Text='<%#Eval("FE NAME") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Eligible Status">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblfenameqw" Text='<%#Eval("Eligible/Not Eligible") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="View Detail">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lblview" runat="server" class="fa fa-eye fa-2x" CommandArgument='<%#Eval("UniqueID") %>'
                                                    CommandName="ViewDetail"></asp:LinkButton>
                                                <asp:HiddenField ID="hdnIDGridRawData" runat="server" Value='<%#Eval("Logical Circuit Id") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <div class="clearfix line">
                                </div>
                                <br />
                                <div id="myModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel"
                                    aria-hidden="true" class="modal fade text-left">
                                    <div role="document" class="modal-dialog">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <button type="button" data-dismiss="modal" aria-label="Close" class="close">
                                                    <span aria-hidden="true">×</span></button>
                                            </div>
                                            <div class="modal-body">
                                                <div runat="server" id="divshowdetail" visible="false">
                                                    <p>
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
                                                                        Account Caf No
                                                                    </h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Account Caf No").ToString() == "" ? "#" : Eval("Account Caf No").ToString()%>
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
                                                                        Alt Phone No</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Alt Phone1").ToString() == "" ? "#" : Eval("Alt Phone1").ToString()%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Alt Phone No-1</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Alt Phone2").ToString() == "" ? "#" : Eval("Alt Phone2").ToString()%>
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
                                                                        Bill Plan</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Bill Plan").ToString() == "" ? "#" : Eval("Bill Plan").ToString()%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Bill UOM</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Bill UOM").ToString() == "" ? "#" : Eval("Bill UOM").ToString()%>
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
                                                                        Billed Act Id</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Billed Act Id").ToString() == "" ? "#" : Eval("Billed Act Id").ToString()%>
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
                                                                    <%# Eval("ComplainceDate", "{0:dd/MMM/yyyy}").ToString() == "" ? "#" : Eval("ComplainceDate", "{0:dd/MMM/yyyy}").ToString()%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        FE Complaince Date</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("FEComplianceDate", "{0:dd/MMM/yyyy}").ToString() == "" ? "#" : Eval("FEComplianceDate", "{0:dd/MMM/yyyy}").ToString()%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Charge Name</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Charge Name").ToString() == "" ? "#" : Eval("Charge Name").ToString()%>
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
                                                                        Party Name</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Party Name").ToString() == "" ? "#" : Eval("Party Name").ToString()%>
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
                                                                        Contact Phone2</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Contact Phone2").ToString() == "" ? "#" : Eval("Contact Phone2").ToString()%>
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
                                                                        Cust account No</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Cust account No").ToString() == "" ? "#" : Eval("Cust account No").ToString()%>
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
                                                                        External id type</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("External id type").ToString() == "" ? "#" : Eval("External id type").ToString()%>
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
                                                                        Document Scanned By TL</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("IsScanned").ToString() == "" ? "#" : Eval("IsScanned").ToString()%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Document Submit Status by TL
                                                                    </h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("IsSubmitted").ToString() == "" ? "#" : Eval("IsSubmitted").ToString()%>
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
                                                                        Line Item Description</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Line Item Description").ToString() == "" ? "#" : Eval("Line Item Description").ToString()%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Line Name
                                                                    </h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Line_Name").ToString() == "" ? "#" : Eval("Line_Name").ToString()%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        LOB</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("LOB").ToString() == "" ? "#" : Eval("LOB").ToString()%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        LOC Created Date By PM</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("LOC Created Date By PM").ToString() == "" ? "#" : Eval("LOC Created Date By PM").ToString()%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        LOC Status</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("LOC Status").ToString() == "" ? "#" : Eval("LOC Status").ToString()%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        LOC Submiited Date By PM</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("LOC Submiited Date By PM").ToString() == "" ? "#" : Eval("LOC Submiited Date By PM").ToString()%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Location</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Location").ToString() == "" ? "#" : Eval("Location").ToString()%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Location Secondary</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Location Secondary").ToString() == "" ? "#" : Eval("Location Secondary").ToString()%>
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
                                                                        Mkt Code</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Mkt Code").ToString() == "" ? "#" : Eval("Mkt Code").ToString()%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        FE Location ID</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("MappingID").ToString() == "" ? "#" : Eval("MappingID").ToString()%>
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
                                                                        New Connection Type
                                                                    </h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("New_Connection_Type").ToString() == "" ? "#" : Eval("New_Connection_Type").ToString()%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Num channel</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Num channel").ToString() == "" ? "#" : Eval("Num channel").ToString()%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Order Type
                                                                    </h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Order Type").ToString() == "" ? "#" : Eval("Order Type").ToString()%>
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
                                                                        Overall DD Completion Date</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Overall DD Completion Date").ToString() == "" ? "#" : Eval("Overall DD Completion Date").ToString()%>
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
                                                                        PO Number</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("PO Number").ToString() == "" ? "#" : Eval("PO Number").ToString()%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        POP 1</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("POP 1").ToString() == "" ? "#" : Eval("POP 1").ToString()%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        POP 2</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("POP 2").ToString() == "" ? "#" : Eval("POP 2").ToString()%>
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
                                                                        Case File Recieved Date</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("RecievedDate", "{0:dd/MMM/yyyy}").ToString() == "" ? "#" : Eval("RecievedDate", "{0:dd/MMM/yyyy}").ToString()%>
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
                                                                    <%# Eval("StatusTime", "{0:hh:mm:ss}").ToString() == "" ? "#" : Eval("StatusTime", "{0:hh:mm:ss}").ToString()%>
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
                                                                        Case Document Scanned Date
                                                                    </h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("ScannedDate", "{0:dd/MMM/yyyy}").ToString() == "" ? "#" : Eval("ScannedDate", "{0:dd/MMM/yyyy}").ToString()%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Case Submitted Date</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("SubmittedDate", "{0:dd/MMM/yyyy}").ToString() == "" ? "#" : Eval("SubmittedDate", "{0:dd/MMM/yyyy}").ToString()%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Send Negative Mail to All</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("SendNegativeMailtoAll").ToString() == "" ? "#" : Eval("SendNegativeMailtoAll").ToString()%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Send Mail to Airtel</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("SentMailAirtel", "{0:==Y ? Yes: NO}").ToString() == "" ? "#" : Eval("SentMailAirtel", "{0:==Y ? Yes: NO}").ToString()%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Send Mail to SAM</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("SentSamMail", "{0:==Y ? Yes: NO}").ToString() == "" ? "#" : Eval("SentSamMail", "{0:==Y ? Yes: NO}").ToString()%>
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
                                                                        Service City</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Service City").ToString() == "" ? "#" : Eval("Service City").ToString()%>
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
                                                                        Service Start</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Service Start").ToString() == "" ? "#" : Eval("Service Start").ToString()%>
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
                                                                        Sub Product Name</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Sub Product Name").ToString() == "" ? "#" : Eval("Sub Product Name").ToString()%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Subs Del No</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Subs Del No").ToString() == "" ? "#" : Eval("Subs Del No").ToString()%>
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
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Unique Company</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Unique Company").ToString() == "" ? "#" : Eval("Unique Company").ToString()%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Unique Installation Address</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Unique_Installation_Address").ToString() == "" ? "#" : Eval("Unique_Installation_Address").ToString()%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Verification</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Verification").ToString() == "" ? "#" : Eval("Verification").ToString()%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Verification Agent</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Verification Agent").ToString() == "" ? "#" : Eval("Verification Agent").ToString()%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Verification Code</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Verification Code").ToString() == "" ? "#" : Eval("Verification Code").ToString()%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Verification Date</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Verification Date", "{0:dd/MMM/yyyy}").ToString() == "" ? "#" : Eval("Verification Date", "{0:dd/MMM/yyyy}").ToString()%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Verification Status</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Verification Status").ToString() == "" ? "#" : Eval("Verification Status").ToString()%>
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
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Case Closed</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("IsClosed").ToString() == "" ? "#" : Eval("IsClosed").ToString()%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Case Active</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("IsActive").ToString() == "" ? "#" : Eval("IsActive").ToString()%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:DataList>
                                                        </form>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                        <p>
                                                        </p>
                                                    </p>
                                                </div>
                                                <asp:UpdatePanel ID="upanel" runat="server" ChildrenAsTriggers="true">
                                                    <ContentTemplate>
                                                        <div class="col-sm-12" runat="server" id="popuperror">
                                                        </div>
                                                        <div class="clearfix">
                                                        </div>
                                                        <div runat="server" id="diverrormssg" visible="false">
                                                            <asp:Label ID="lbltotalrowselected" runat="server" class="label label-warning block"></asp:Label>
                                                            <div class="clearfix pb20">
                                                            </div>
                                                            <asp:TextBox ID="txtreasone" runat="server" class="form-control" TextMode="MultiLine"
                                                                Height="100px" placeholder="Reason for delete excel"></asp:TextBox>
                                                            <div class="clear pb10">
                                                            </div>
                                                            <asp:Button ID="btndelete" runat="server" class="btn btn-primary pull-right" Text="Delete Selected Excel Record"
                                                                OnClick="btndelete_Click" />
                                                            <div class="clear pb10">
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="modal-footer">
                                                <button type="button" data-dismiss="modal" class="btn btn-secondary">
                                                    Close</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <%--<asp:PostBackTrigger ControlID="btnapprove" />--%>
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
