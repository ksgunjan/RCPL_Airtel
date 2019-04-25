<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmDaywiseFeedBackReport.aspx.cs"
    Inherits="frmDaywiseFeedBackReport" MasterPageFile="~/MasterPage2.master" %>

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
                        Fe Day Wise FeedBack Report</h3>
                </div>
            </div>
            <asp:UpdatePanel ID="updatepanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="card-body">
                        <div class="form-horizontal">
                            <div class="clearfix pb10">
                            </div>
                            <div class="form-group row">
                                <div runat="server" id="diverror" class="col-sm-12">
                                </div>
                            </div>
                            <div class="clearfix pb10">
                            </div>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtsearch" runat="server" class="form-control" type="date"></asp:TextBox>
                            </div>
                            <div class="col-sm-4">
                                <asp:Button ID="btnsearch" runat="server" class="btn btn-info" Text="Search" OnClick="btnsearch_Click" />
                            </div>
                            <div class="clearfix pb10">
                            </div>
                            <asp:Button ID="btnmail" runat="server" class="btn btn-danger btn-sm pull-right mr10"
                                Text="Send Mail" OnClick="btnmail_Click" />
                            <div class="clearfix pb10">
                            </div>
                            <div class="table table-responsive">
                                <b>Leased Line</b>
                                <div class="clearfix">
                                </div>
                                <asp:GridView ID="gvll" runat="server" class="table table-responsive" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Logical Circuit ID" HeaderText="Logical Circuit ID" NullDisplayText="#" />
                                        <asp:BoundField DataField="Party Name" HeaderText="Company Name" NullDisplayText="#" />
                                        <asp:BoundField DataField="FE Name" HeaderText="FE Name" NullDisplayText="#" />
                                        <asp:BoundField DataField="StatusByFe" HeaderText="Audit Status" NullDisplayText="#" />
                                        <asp:BoundField DataField="Code" HeaderText="Code" NullDisplayText="#" />
                                        <asp:BoundField DataField="Remarks" HeaderText="Remark" NullDisplayText="#" />
                                        <asp:BoundField DataField="StatusUpdateDate" HeaderText="Date" DataFormatString="{0:dd/MMM/yyyy}"
                                            NullDisplayText="#" />
                                        <asp:BoundField DataField="StatusTime" HeaderText="Time" NullDisplayText="#" />
                                        <asp:BoundField DataField="Customer Name" HeaderText="Cust. Name" NullDisplayText="#" />
                                        <asp:BoundField DataField="Mobile No" HeaderText="Mobile No" NullDisplayText="#" />
                                        <asp:BoundField DataField="PersonMetMobileNo" HeaderText="OTP No" NullDisplayText="#" />
                                        <asp:BoundField DataField="PersonMetEmail" HeaderText="Contact Person Email" NullDisplayText="#" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="clearfix pb10">
                            </div>
                            <div class="table table-responsive">
                                <b>MO</b>
                                <div class="clearfix">
                                </div>
                                <asp:GridView ID="gvmo" runat="server" class="table table-responsive" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Billed_Ext_ID" HeaderText="Billed Ext ID" NullDisplayText="#" />
                                        <asp:BoundField DataField="Party_Name" HeaderText="Company Name" NullDisplayText="#" />
                                        <asp:BoundField DataField="FE_Name" HeaderText="FE Name" NullDisplayText="#" />
                                        <asp:BoundField DataField="StatusByFe" HeaderText="Audit Status" NullDisplayText="#" />
                                        <asp:BoundField DataField="Code" HeaderText="Code" NullDisplayText="#" />
                                        <asp:BoundField DataField="Remarks" HeaderText="Remark" NullDisplayText="#" />
                                        <asp:BoundField DataField="StatusUpdateDate" HeaderText="Date" DataFormatString="{0:dd/MMM/yyyy}"
                                            NullDisplayText="#" />
                                        <asp:BoundField DataField="StatusTime" HeaderText="Time" NullDisplayText="#" />
                                        <asp:BoundField DataField="PersonMetName" HeaderText="Cust. Name" NullDisplayText="#" />
                                        <asp:BoundField DataField="Coordinator_Contact_Number" HeaderText="Contact No" NullDisplayText="#" />
                                        <asp:BoundField DataField="PersonMetMobileNo" HeaderText="OTP No" NullDisplayText="#" />
                                        <asp:BoundField DataField="PersonMetEmail" HeaderText="Contact Person Email" NullDisplayText="#" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="clearfix pb10">
                            </div>
                            <div class="table table-responsive">
                                <b>PRI-Fixed Line</b>
                                <div class="clearfix">
                                </div>
                                <asp:GridView ID="gvpri" runat="server" class="table table-responsive" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Subs Del No" HeaderText="Subs Del No" NullDisplayText="#" />
                                        <asp:BoundField DataField="Party Name" HeaderText="Company Name" NullDisplayText="#" />
                                        <asp:BoundField DataField="FE Name" HeaderText="FE Name" NullDisplayText="#" />
                                        <asp:BoundField DataField="StatusByFe" HeaderText="Audit Status" NullDisplayText="#" />
                                        <asp:BoundField DataField="Code" HeaderText="Code" NullDisplayText="#" />
                                        <asp:BoundField DataField="Remarks" HeaderText="Remark" NullDisplayText="#" />
                                        <asp:BoundField DataField="StatusUpdateDate" HeaderText="Date" DataFormatString="{0:dd/MMM/yyyy}"
                                            NullDisplayText="#" />
                                        <asp:BoundField DataField="StatusTime" HeaderText="Time" NullDisplayText="#" />
                                        <asp:BoundField DataField="Customer Name" HeaderText="Cust. Name" NullDisplayText="#" />
                                        <asp:BoundField DataField="Coordinator_Contact_Number" HeaderText="Contact No" NullDisplayText="#" />
                                        <asp:BoundField DataField="PersonMetMobileNo" HeaderText="OTP No" NullDisplayText="#" />
                                        <asp:BoundField DataField="PersonMetEmail" HeaderText="Contact Person Email" NullDisplayText="#" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="clearfix pb10">
                            </div>
                        </div>
                    </div>
                    <div class="clearfix pb10">
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
