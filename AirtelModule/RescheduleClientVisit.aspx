<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RescheduleClientVisit.aspx.cs"
    Inherits="RescheduleClientVisit" MasterPageFile="~/MasterPage2.master" %>

<asp:Content ID="conhead" runat="server" ContentPlaceHolderID="head">
    <!----------------Bootstrp js-------------------->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <!--------------------------Page Loader------------------->
    <%--<style>
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
    <script src="http://code.jquery.com/jquery-1.8.2.js"></script>--%>
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
<asp:Content ID="coninner" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="loader">
    </div>
    <div class="right_col" role="main">
        <div class="">
            <div class="page-title">
                <div class="title_left">
                    <h3>
                        Update Rescheduled Client Appointment</h3>
                </div>
            </div>
            <asp:UpdatePanel ID="updatepanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="card-body">
                        <div class="form-horizontal">
                            <div class="clearfix pb10">
                            </div>
                            <div runat="server" id="diverror">
                            </div>
                            <div class="clearfix pb10">
                            </div>
                            <div>
                                1. In Case of Mo Please Enter (Company/Party Name)
                                <div class="clearfix">
                                </div>
                                2. Other Case you can search record by enter Customer/Company/Party Name
                            </div>
                            <div class="clearfix pb10">
                            </div>
                            <div class="col-sm-5">
                                <asp:DropDownList ID="ddlprductname" runat="server" class="form-control">
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-5">
                                <asp:TextBox ID="txtsearch" runat="server" class="form-control" required="" placeholder="Please enter Customer Name or Logical CircuitId/Billed Ext ID"> </asp:TextBox>
                            </div>
                            <div class="col-sm-2">
                                <asp:Button ID="btnsearch" runat="server" class="btn btn-primary btn-block btn-sm"
                                    Text="Search Customer" OnClick="btnsearch_Click" />
                            </div>
                            <div class="clearfix pb10">
                            </div>
                            <div>
                                <asp:HiddenField ID="hfmappingID" runat="server" />
                                <div class="table table-responsive">
                                    <asp:GridView ID="gvexcel" runat="server" AutoGenerateColumns="false" class="table table-hover"
                                        OnRowCommand="gvexcel_RowCommand" OnRowDataBound="gvexcel_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="Customer Name" HeaderText="Customer Name" />
                                            <asp:TemplateField HeaderText="Logical CircuitId/SubDel No/Billed Ext ID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbllogcirid" runat="server" Text='<%#Eval("Logical Circuit Id") %>'
                                                        Visible="false"></asp:Label>
                                                    <asp:Label ID="lblbillextid" runat="server" Text='<%#Eval("Billed Ext Id") %>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lblsubs" runat="server" Text='<%#Eval("Subs Del No") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Party Name" HeaderText="Party Name" />
                                            <asp:BoundField DataField="From Site" HeaderText="Customer Address-1" />
                                            <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                                            <asp:TemplateField HeaderText="Compliance Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcomp" runat="server" Text='<%#Eval("ComplainceDate","{0:dd/MMM/yyyy}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Update Status">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblstatus" runat="server" class="fa fa-edit fa-2x" CommandArgument='<%#Eval("UniqueID") %>'
                                                        CommandName="UpdateStatus"></asp:LinkButton>
                                                    <asp:HiddenField ID="hfstatusfe" runat="server" Value='<%#Eval("StatusByFE") %>' />
                                                    <asp:HiddenField ID="hfemail" runat="server" Value='<%#Eval("Cust EMail") %>' />
                                                    <asp:HiddenField ID="Customer" runat="server" Value='<%#Eval("Customer Name") %>' />
                                                    <asp:HiddenField ID="hfrevisitdate" runat="server" Value='<%#Eval("Revisit Date Time") %>' />
                                                    <asp:HiddenField ID="hfpartyname" runat="server" Value='<%#Eval("Party Name") %>' />
                                                     <asp:HiddenField ID="hflaststatusupdatedate" runat="server" Value='<%#Eval("StatusUpdateDate") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div id="myModal" aria-hidden="true" aria-labelledby="exampleModalLabel" class="modal fade text-left"
                                    role="dialog" tabindex="-1">
                                    <div class="modal-dialog" role="document">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <button aria-label="Close" class="close" data-dismiss="modal" type="button">
                                                    <span aria-hidden="true">×</span>
                                                </button>
                                            </div>
                                            <div class="modal-body">
                                                <asp:UpdatePanel ID="upanel" runat="server" ChildrenAsTriggers="true">
                                                    <ContentTemplate>
                                                        <div class="clearfix pb5">
                                                        </div>
                                                        <div id="divmssgpopup" runat="server">
                                                        </div>
                                                        <div class="clearfix pb5">
                                                        </div>
                                                        <asp:HiddenField ID="hflocsrid" runat="server" />
                                                        <asp:HiddenField ID="hfEmailCustomer" runat="server" />
                                                        <asp:HiddenField ID="hfNameCustomer" runat="server" />
                                                        <asp:HiddenField ID="hfPartyNameDisplay" runat="server" />
                                                          <asp:HiddenField ID="hfupdatedate" runat="server" />
                                                        <div id="divupdatebyfe" runat="server">
                                                            <h3>
                                                                Update Status as per client response.</h3>
                                                            <div>
                                                                <p>
                                                                    Select FE to assign reschedule appointment</p>
                                                                <div class="form-group row">
                                                                    <label class="col-sm-3 form-control-label">
                                                                        Select Field Executive
                                                                    </label>
                                                                    <div class="col-sm-9">
                                                                        <asp:DropDownList ID="ddlselectfe" runat="server" class="form-control">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group row">
                                                                    <label class="col-sm-3 form-control-label">
                                                                        AppointMent Date
                                                                    </label>
                                                                    <div class="col-sm-9">
                                                                        <asp:TextBox ID="txtdate" runat="server" class="form-control" type="Date"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group row">
                                                                    <label class="col-sm-3 form-control-label">
                                                                        AppointMent Time
                                                                    </label>
                                                                    <div class="col-sm-9">
                                                                        <asp:TextBox ID="txttime" runat="server" class="form-control" type="time"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group row">
                                                                    <asp:Button ID="btnupdateprescheduledtimedate" runat="server" class="btn btn-primary pull-right btn-sm"
                                                                        Text="Update Preschedule AppointMent to FE" OnClick="btnupdateprescheduledtimedate_Click" />
                                                                </div>
                                                            </div>
                                                            <div class="Clearfix pb5">
                                                            </div>
                                                            <div class="modal-footer">
                                                                <button class="btn btn-default" data-dismiss="modal" type="button">
                                                                    Close
                                                                </button>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                    </div>
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
</asp:Content>
