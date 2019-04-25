<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmUpdateStatusSubmitManager.aspx.cs"
    Inherits="frmUpdateStatusSubmitManager" MasterPageFile="~/MasterPage2.master" %>

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
                        Update Status</h3>
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
                                <div class="form-group row">
                                    <div class="col-sm-4">
                                        <label class="col-sm-12 form-control-label">
                                            Start Date</label>
                                        <div class="col-sm-12">
                                            <asp:TextBox ID="txtdate" runat="server" class="form-control" type="date"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <label class="col-sm-12 form-control-label">
                                            End Date</label>
                                        <div class="col-sm-12">
                                            <asp:TextBox ID="txtenddate" runat="server" class="form-control" type="date"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <label class="col-sm-12 form-control-label">
                                            Select Product Type</label>
                                        <div class="col-sm-12">
                                            <asp:DropDownList ID="ddlprodtype" runat="server" class="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label class="col-sm-9 form-control-label">
                                    </label>
                                    <div class="col-sm-3">
                                        <asp:Button ID="btnsub" runat="server" class="btn btn-sm btn-primary pull-right"
                                            Text="Search & Update" OnClick="btnsub_Click" />
                                    </div>
                                </div>
                                <div class="form-group row">
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
                                            <asp:BoundField DataField="LOC Sr No" HeaderText="LOC Sr No" />
                                            <asp:BoundField DataField="LOC Status" HeaderText="LOC Status" />
                                            <asp:BoundField DataField="Customer Name" HeaderText="Customer Name" />
                                            <asp:BoundField DataField="CRM ORDER ID" HeaderText="CRM ORDER ID" />
                                            <asp:BoundField DataField="Product Name" HeaderText="Product Name" />
                                            <asp:BoundField DataField="Project Manager" HeaderText="Project Manager" />
                                            <asp:BoundField DataField="FE Name" HeaderText="FE Name" />
                                            <asp:BoundField DataField="ComplainceDate" DataFormatString="{0:dd/MMM/yyyy}" HeaderText="Compliance Date" />
                                            <asp:TemplateField HeaderText="View Detail">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblview" runat="server" class="fa fa-eye fa-2x" CommandArgument='<%#Eval("UniqueID") %>'
                                                        CommandName="ViewDetail"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Update Status">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblstatus" runat="server" class="fa fa-edit fa-2x" CommandArgument='<%#Eval("[LOC Sr No]") %>'
                                                        CommandName="UpdateStatus"></asp:LinkButton>
                                                    <asp:HiddenField ID="hfLocID" runat="server" Value='<%#Eval("[LOC Sr No]") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
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
                                                                        LOC Sr No</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("LOC Sr No") %>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        LOC Status
                                                                    </h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("LOC Status")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Customer Name
                                                                    </h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("Customer Name")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        CRM ORDER ID
                                                                    </h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("CRM ORDER ID")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        COPC APPROVED DATE</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("COPC APPROVED DATE", "{0:dd/MMM/yyyy}")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Logical Circuit Id</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("Logical CircuitId")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Dup</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("Dup")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Product Name</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("Product Name")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Sub Product Name</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("Sub Product Name")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        From Site</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("From Site")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        To Site</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("To Site")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Order Type</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("Order Type")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Circuit BandWidth</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("Circuit BandWidth")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Line Item Amount</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("Line Item Amount")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Project Manager</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("Project Manager")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        PM Oracle Id</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("PM OracleId")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Account Manager</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("Account Manager")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Zone</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("Zone")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Region</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("Region")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Vertical</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("Vertical")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Account Category</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("Account Category")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Customer PO Number</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("Customer PO Number")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Customer PO Date</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("Customer PODate","{0:dd/MMM/yyyy}")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Order Entry Date</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("Order Entry Date", "{0:dd/MMM/yyyy}")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        PM APPROVAL DATE</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("PM APPROVAL DATE", "{0:dd/MMM/yyyy}")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        ACCOUNT MANAGER RECEIVE DATE</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("ACCOUNT MANAGER RECEIVE DATE", "{0:dd/MMM/yyyy}")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        NIO DATE</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("NIO DATE", "{0:dd/MMM/yyyy}")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        CUSTOMER RFS DATE</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("CUSTOMER RFS DATE", "{0:dd/MMM/yyyy}")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Order Category</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("Order Category")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Order Reporting Status</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("Order Reporting Status")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Line Item Description</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("Line Item Description")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Order Line Name</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("Order Line Name")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Order Line Type</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("Order Line Type")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Charge Name</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("Charge Name")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        PK CHARGES ID</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("PK_CHARGES_ID")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Billing Entity</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("Billing Entity")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Contract Period</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("Contract Period")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Cyclic Non Cyclic</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("Cyclic NonCyclic")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Billing Bandwidth</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("Billing Bandwidth")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Bill UOM</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("Bill UOM")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Currency</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("Currency")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        RA ORDER NUMBER</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("RA ORDER NUMBER")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Commissioning Date</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("Commissioning Date", "{0:dd/MMM/yyyy}")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Overall DD Completion Date</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("Overall DD Completion Date", "{0:dd/MMM/yyyy}")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        LOC Created Date By PM</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("LOC Created Date By PM")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        LOC SignOffDate</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("LOC SignOffDate", "{0:dd/MMM/yyyy}")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        LOC Submiited Date By PM</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("LOC Submiited Date By PM", "{0:dd/MMM/yyyy}")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        LOC Received date at PMO</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("LOC Received date at PMO", "{0:dd/MMM/yyyy}")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        LOC Submitted date to Billing</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("LOC Submitted date to Billing", "{0:dd/MMM/yyyy}")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Billing Trigger Date</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("Billing Trigger Date", "{0:dd/MMM/yyyy}")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Invoice Date</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("Invoice Date", "{0:dd/MMM/yyyy}")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Invoice No</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("Invoice No")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Invoice Amount
                                                                    </h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("Invoice Amount")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Customer Segment</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("Customer Segment")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Project Region</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("Project Region")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Annualized (CWN Value)</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("Annualized")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Annualized (CWN Value) in INR Mn</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("AnnualizedINR")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Gain/Loss</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("Gain/Loss")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        LOC Delay Reason-Commissioning</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("LOC Delay Reason-Commissioning")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        LOC Delay Reason-SignOff</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("LOC Delay Reason-SignOff")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Final ACD</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("Final ACD")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Final CND</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("Final CND")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Party City
                                                                    </h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("Party City")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Party Region</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("Party Region")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Service Segment
                                                                    </h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("Service Segment")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Media</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("Media")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        OnNet/OffNet
                                                                    </h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("OnNet/OffNet")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Eligible/Not Eligible
                                                                    </h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("Eligible/Not Eligible")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Month</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("Month")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Zone1
                                                                    </h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("Zone1")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Location</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("Location")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        FOS Name</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("FOS Name")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Allocated Date</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("Allocated Date", "{0:dd/MMM/yyyy}")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        FE Name</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("FE Name")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Positive/Negative</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("Positive/Negative")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Code</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("Code")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Remarks</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("Remarks")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Date</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("Date", "{0:dd/MMM/yyyy}")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Cust Name</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("Cust Name")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Cust No</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("Cust No")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Cust Mail ID</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("Cust Mail ID")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        SAM Name</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("SAM Name")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        SAM Mail ID</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("SAM Mail ID")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        SAM No</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("SAM No")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        SAM TL Name</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("SAM TL Name")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        SAM TL Email</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("SAM TL Email")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        SAM TL Contact</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("SAM TL Contact")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        SAM Mails Status</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("SAM Mails Status")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        SAM Mail Date</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("SAM Mail Date", "{0:dd/MMM/yyyy}")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        SAM Revert</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("SAM Revert")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        SAM Revert Date</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("SAM Revert Date", "{0:dd/MMM/yyyy}")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Scanning Status</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("Scanning Status")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        ProdType Name</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("ProdTypeName")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Inserted Date</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("InsertedDate", "{0:dd/MMM/yyyy}")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Recieved Date</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("RecievedDate", "{0:dd/MMM/yyyy}")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Compliance Date
                                                                    </h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("ComplainceDate", "{0:dd/MMM/yyyy}")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Reason Of Status</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("ReasonOfStatus")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Remarks Of Status</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("RemarksOfStatus")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Status By FE</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("StatusByFE")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Status Time</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("StatusTime")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Scanned</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("IsScanned")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Scanned Date</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("ScannedDate", "{0:dd/MMM/yyyy}")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Submitted</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("IsSubmitted")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Submitted Date</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("SubmittedDate", "{0:dd/MMM/yyyy}")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        Closed</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("IsClosed")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        SentSAMEmail</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("SentSAMEmail")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>
                                                                        SentCustMail</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%#Eval("SentCustMail")%>
                                                                </div>
                                                                <div class="clearfix pb-2">
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:DataList>
                                                        </form>
                                                    </div>
                                                    <div runat="server" id="divupdatebyfe">
                                                        <h3>
                                                            Update Status as per FE response.</h3>
                                                        <form>
                                                        <div>
                                                            <p>
                                                                All fields fill are mandatory(*)</p>
                                                            <div class="Clearfix pb5">
                                                            </div>
                                                            <div class="form-group row">
                                                                <label class="col-sm-3 form-control-label">
                                                                    Is Scanned
                                                                </label>
                                                                <div class="col-sm-9">
                                                                    <asp:DropDownList ID="ddlstatus" runat="server" class="form-control">
                                                                        <asp:ListItem>Select Scanned Status</asp:ListItem>
                                                                        <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                                        <asp:ListItem Value="N">No</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="form-group row">
                                                                <label class="col-sm-3 form-control-label">
                                                                    Scanned Date
                                                                </label>
                                                                <div class="col-sm-9">
                                                                    <asp:TextBox ID="txtscanneddate" runat="server" class="form-control" type="date"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group row">
                                                                <label class="col-sm-3 form-control-label">
                                                                    Is Submitted
                                                                </label>
                                                                <div class="col-sm-9">
                                                                    <asp:DropDownList ID="ddlissubmited" runat="server" class="form-control">
                                                                        <asp:ListItem>Select Submitted Status</asp:ListItem>
                                                                        <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                                        <asp:ListItem Value="N">No</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="form-group row">
                                                                <label class="col-sm-3 form-control-label">
                                                                    Submitted Date
                                                                </label>
                                                                <div class="col-sm-9">
                                                                    <asp:TextBox ID="txtsubmiteddate" runat="server" class="form-control" type="date"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group row">
                                                                <label class="col-sm-7 form-control-label">
                                                                </label>
                                                                <div class="col-sm-5">
                                                                    <asp:Button ID="btnsubmitstatus" runat="server" class="btn btn-primary btn-sm btn-block"
                                                                        Text="Submit Status" OnClick="btnsubmitstatus_Click" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        </form>
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
    </div>
</asp:Content>
