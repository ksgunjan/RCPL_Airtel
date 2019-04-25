<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UploadPreScheduleExcel.aspx.cs" Inherits="UploadPreScheduleExcel"  MasterPageFile="~/MasterPage2.master" %>

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
</asp:Content>
<asp:Content ID="contentmain" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="loader">
    </div>
    <div class="right_col" role="main">
        <div class="">
            <div class="page-title">
                <div class="title_left">
                    <h3>
                        Upload Excel</h3>
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
                                <asp:HiddenField ID="hfexcel" runat="server" />
                                <div class="form-group row">
                                    <div class="col-sm-3">
                                        <label class="col-sm-12 form-control-label">
                                            Select Company</label>
                                        <div class="col-sm-12">
                                            <asp:DropDownList ID="ddlcomp" runat="server" class="form-control focus" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlcomp_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <label class="col-sm-12 form-control-label">
                                            Select Product</label>
                                        <div class="col-sm-12">
                                            <asp:DropDownList ID="ddlproduct" runat="server" AutoPostBack="true" class="form-control focus"
                                                OnSelectedIndexChanged="ddlproduct_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <label class="col-sm-12 form-control-label">
                                            Select Product Type</label>
                                        <div class="col-sm-12">
                                            <asp:DropDownList ID="ddlproducttype" runat="server" AutoPostBack="true" 
                                                class="form-control focus" 
                                                onselectedindexchanged="ddlproducttype_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                     <div class="col-sm-3">
                                        <label class="col-sm-12 form-control-label">
                                            Select Field Executive</label>
                                        <div class="col-sm-12">
                                            <asp:DropDownList ID="ddlfe" runat="server" class="form-control focus">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="Clearfix pb5">
                                </div>
                                <div class="form-group row">
                                    <label class="col-sm-3 form-control-label">
                                        Select Excel File</label>
                                    <div class="col-sm-9">
                                        <asp:FileUpload runat="server" class="form-control" ID="fuexcel" />
                                    </div>
                                </div>
                                <div class="Clearfix pb5">
                                </div>
                                <div class="form-group row">
                                    <label class="col-sm-9 form-control-label">
                                        <h4>
                                            <asp:Label ID="lblpathname" runat="server" class="label label-info"></asp:Label>
                                        </h4>
                                    </label>
                                    <div class="col-sm-3">
                                        <asp:Button ID="btnexcel" runat="server" class="btn btn-primary btn-sm btn-block"
                                            Text="Upload Excel Sheet File" OnClick="btnexcel_Click" />
                                    </div>
                                </div>
                                <div class="clearfix pb-1">
                                </div>
                                <div class="table table-responsive" runat="server" id="gvexcelpanel">
                                    <div>
                                        <p>
                                            Please review your excel before click on final upload.
                                        </p>
                                        <p>
                                            Make sue all column name or fields value are corrrect(Database does not accept (')
                                            in any fields
                                        </p>
                                        <p>
                                            if any error come it will show you on message please delete before row where error
                                            come and upload again excel sheet.
                                        </p>
                                        <p>
                                            Please wait for sometime after click on upload button it will take some time to
                                            upload record on server.
                                        </p>
                                        <div class="clearfix pb-1">
                                        </div>
                                        <div class="form-group row">
                                            <div class="col-sm-9">
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:Button ID="btnfinalexcelupload" runat="server" class="btn btn-success pull-right"
                                                    Text="Upload Final Review Excel" OnClick="btnfinalexcelupload_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="clearfix pb-1">
                                    </div>
                                    <asp:GridView ID="gvexcel" runat="server" class="table table-hover" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:BoundField DataField="LOC Sr No" HeaderText="LOC Sr No" />
                                            <asp:BoundField DataField="LOC Status" HeaderText="LOC Status" />
                                            <asp:BoundField DataField="Customer Name" HeaderText="Customer Name" />
                                            <asp:BoundField DataField="CRM ORDER ID" HeaderText="CRM ORDER ID" />
                                            <asp:BoundField DataField="COPC APPROVED DATE" HeaderText="COPC APPROVED DATE" DataFormatString="{0:dd-MMM-yyyy}" />
                                            <asp:BoundField DataField="Logical CircuitId" HeaderText="Logical Circuit Id" />
                                            <asp:BoundField DataField="Dup" HeaderText="Dup" />
                                            <asp:BoundField DataField="Product Name" HeaderText="Product Name" />
                                            <asp:BoundField DataField="Sub Product Name" HeaderText="Sub Product Name" />
                                            <asp:BoundField DataField="From Site" HeaderText="From Site" />
                                            <asp:BoundField DataField="To Site" HeaderText="To Site" />
                                            <asp:BoundField DataField="Order Type" HeaderText="Order Type" />
                                            <asp:BoundField DataField="Circuit BandWidth" HeaderText="Circuit BandWidth" />
                                            <asp:BoundField DataField="Line Item Amount" HeaderText="Line Item Amount" />
                                            <asp:BoundField DataField="Project Manager" HeaderText="Project Manager" />
                                            <asp:BoundField DataField="PM OracleId" HeaderText="PM OracleId" />
                                            <asp:BoundField DataField="Account Manager" HeaderText="Account Manager" />
                                            <asp:BoundField DataField="Zone" HeaderText="Zone" />
                                            <asp:BoundField DataField="Region" HeaderText="Region" />
                                            <asp:BoundField DataField="Vertical" HeaderText="Vertical" />
                                            <asp:BoundField DataField="Account Category" HeaderText="Account Category" />
                                            <asp:BoundField DataField="Customer PO Number" HeaderText="Customer PO Number" />
                                            <asp:BoundField DataField="Customer PODate" HeaderText="Customer PODate" DataFormatString="{0:dd-MMM-yyyy}" />
                                            <asp:BoundField DataField="Order Entry Date" HeaderText="Order Entry Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                            <asp:BoundField DataField="PM APPROVAL DATE" HeaderText="PM APPROVAL DATE" DataFormatString="{0:dd-MMM-yyyy}" />
                                            <asp:BoundField DataField="ACCOUNT MANAGER RECEIVE DATE" HeaderText="ACCOUNT MANAGER RECEIVE DATE"
                                                DataFormatString="{0:dd-MMM-yyyy}" />
                                            <asp:BoundField DataField="NIO DATE" HeaderText="NIO DATE" DataFormatString="{0:dd-MMM-yyyy}" />
                                            <asp:BoundField DataField="CUSTOMER RFS DATE" HeaderText="CUSTOMER RFS DATE" DataFormatString="{0:dd-MMM-yyyy}" />
                                            <asp:BoundField DataField="Order Category" HeaderText="Order Category" />
                                            <asp:BoundField DataField="Order Reporting Status" HeaderText="Order Reporting Status" />
                                            <asp:BoundField DataField="Line Item Description" HeaderText="Line Item Description" />
                                            <asp:BoundField DataField="Order Line Name" HeaderText="Order Line Name" />
                                            <asp:BoundField DataField="Order Line Type" HeaderText="Order Line Type" />
                                            <asp:BoundField DataField="Charge Name" HeaderText="Charge Name" />
                                            <asp:BoundField DataField="PK_CHARGES_ID" HeaderText="PK_CHARGES_ID" />
                                            <asp:BoundField DataField="Billing Entity" HeaderText="Billing Entity" />
                                            <asp:BoundField DataField="Contract Period" HeaderText="Contract Period" />
                                            <asp:BoundField DataField="Cyclic NonCyclic" HeaderText="Cyclic NonCyclic" />
                                            <asp:BoundField DataField="Billing Bandwidth" HeaderText="Billing Bandwidth" />
                                            <asp:BoundField DataField="Bill UOM" HeaderText="Bill UOM" />
                                            <asp:BoundField DataField="Currency" HeaderText="Currency" />
                                            <asp:BoundField DataField="RA ORDER NUMBER" HeaderText="RA ORDER NUMBER" />
                                            <asp:BoundField DataField="Commissioning Date" HeaderText="Commissioning Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                            <asp:BoundField DataField="Overall DD Completion Date" HeaderText="Overall DD Completion Date"
                                                DataFormatString="{0:dd-MMM-yyyy}" />
                                            <asp:BoundField DataField="LOC Created Date By PM" HeaderText="LOC Created Date By PM"
                                                DataFormatString="{0:dd-MMM-yyyy}" />
                                            <asp:BoundField DataField="LOC SignOffDate" HeaderText="LOC SignOffDate" DataFormatString="{0:dd-MMM-yyyy}" />
                                            <asp:BoundField DataField="LOC Submiited Date By PM" HeaderText="LOC Submiited Date By PM"
                                                DataFormatString="{0:dd-MMM-yyyy}" />
                                            <asp:BoundField DataField="LOC Received date at PMO" HeaderText="LOC Received date at PMO"
                                                DataFormatString="{0:dd-MMM-yyyy}" />
                                            <asp:BoundField DataField="LOC Submitted date to Billing" HeaderText="LOC Submitted date to Billing"
                                                DataFormatString="{0:dd-MMM-yyyy}" />
                                            <asp:BoundField DataField="Billing Trigger Date" HeaderText="Billing Trigger Date"
                                                DataFormatString="{0:dd-MMM-yyyy}" />
                                            <asp:BoundField DataField="Invoice Date" HeaderText="Invoice Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                            <asp:BoundField DataField="Invoice No" HeaderText="Invoice No" />
                                            <asp:BoundField DataField="Invoice Amount" HeaderText="Invoice Amount" />
                                            <asp:BoundField DataField="Customer Segment" HeaderText="Customer Segment" />
                                            <asp:BoundField DataField="Project Region" HeaderText="Project Region" />
                                            <asp:BoundField DataField="Annualized (CWN Value)" HeaderText="Annualized (CWN Value)" />
                                            <asp:BoundField DataField="Annualized (CWN Value) in INR Mn" HeaderText="Annualized (CWN Value) in INR Mn" />
                                            <asp:BoundField DataField="Gain/Loss" HeaderText="Gain/Loss" />
                                            <asp:BoundField DataField="LOC Delay Reason-Commissioning" HeaderText="LOC Delay Reason-Commissioning" />
                                            <%-- <asp:BoundField DataField="LOC Delay Reason-SignOffFinal ACD" HeaderText="1" />--%>
                                            <asp:BoundField DataField="Final CND" HeaderText="Final CND" />
                                            <asp:BoundField DataField="Party City" HeaderText="Party City" />
                                            <asp:BoundField DataField="Party Region" HeaderText="Party Region" />
                                            <asp:BoundField DataField="Service Segment" HeaderText="Service Segment" />
                                            <asp:BoundField DataField="Media" HeaderText="Media" />
                                            <asp:BoundField DataField="OnNet/OffNet" HeaderText="OnNet/OffNet" />
                                            <asp:BoundField DataField="Eligible/Not Eligible" HeaderText="Eligible/Not Eligible" />
                                            <asp:BoundField DataField="Month" HeaderText="Month" />
                                            <asp:BoundField DataField="Zone1" HeaderText="Zone1" />
                                            <asp:BoundField DataField="Location" HeaderText="Location" />
                                            <asp:BoundField DataField="FOS Name" HeaderText="FOS Name" />
                                            <asp:BoundField DataField="Allocated Date" HeaderText="Allocated Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                            <asp:BoundField DataField="FE Name" HeaderText="FE Name" />
                                            <asp:BoundField DataField="Positive/Negative" HeaderText="Positive/Negative" />
                                            <asp:BoundField DataField="Code" HeaderText="Code" />
                                            <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                                            <asp:BoundField DataField="Date" HeaderText="Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                            <asp:BoundField DataField="Cust Name" HeaderText="Cust Name" />
                                            <asp:BoundField DataField="Cust No" HeaderText="Cust No" />
                                            <asp:BoundField DataField="Cust Mail ID" HeaderText="Cust Mail ID" />
                                            <asp:BoundField DataField="SAM Name" HeaderText="SAM Name" />
                                            <asp:BoundField DataField="SAM Mail ID" HeaderText="SAM Mail ID" />
                                            <asp:BoundField DataField="SAM No" HeaderText="SAM No" />
                                            <asp:BoundField DataField="SAM TL Name" HeaderText="SAM TL Name" />
                                            <asp:BoundField DataField="SAM TL Email" HeaderText="SAM TL Email" />
                                            <asp:BoundField DataField="SAM TL Contact" HeaderText="SAM TL Contact" />
                                            <asp:BoundField DataField="SAM Mails Status" HeaderText="SAM Mails Status" />
                                            <asp:BoundField DataField="SAM Mail Date" HeaderText="SAM Mail Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                            <asp:BoundField DataField="SAM Revert" HeaderText="SAM Revert" />
                                            <asp:BoundField DataField="SAM Revert Date" HeaderText="SAM Revert Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                            <asp:BoundField DataField="Scanning Status" HeaderText="Scanning Status" />
                                            <asp:BoundField DataField="RecievedDate" HeaderText="Recieved Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                            <asp:BoundField DataField="ComplainceDate" HeaderText="Compliance Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnexcel" />
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
