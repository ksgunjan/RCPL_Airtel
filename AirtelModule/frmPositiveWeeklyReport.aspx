<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmPositiveWeeklyReport.aspx.cs"
    Inherits="frmPositiveWeeklyReport" MasterPageFile="~/MasterPage2.master" %>

<asp:Content ID="conhead" runat="server" ContentPlaceHolderID="head">
    <link href="vendors/bootstrap/dist/css/nprogress.css" rel="stylesheet" type="text/css" />
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
                        Weekly Positive Case Report</h3>
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
                                        <asp:BoundField DataField="Party Name" HeaderText="Company Name" NullDisplayText="#" />
                                        <asp:BoundField DataField="StatusByFe" HeaderText="Audit Status" NullDisplayText="#" />
                                        <asp:BoundField DataField="Code" HeaderText="Code" NullDisplayText="#" />
                                        <asp:BoundField DataField="Remarks" HeaderText="Remarks" NullDisplayText="#" />
                                        <asp:BoundField DataField="StatusUpdateDate" DataFormatString="{0:dd/MMM/yyyy}" HeaderText="Date"
                                            NullDisplayText="#" />
                                        <asp:BoundField DataField="StatusTime" HeaderText="Time" NullDisplayText="#" />
                                        <asp:BoundField DataField="PersonMetName" HeaderText="Cust. Name" NullDisplayText="#" />
                                        <asp:BoundField DataField="PersonMetMobileNo" HeaderText="Contact No" NullDisplayText="#" />
                                        <asp:BoundField DataField="PersonMetEmail" HeaderText="Cust Mail Id" NullDisplayText="#" />
                                        <asp:BoundField DataField="AppointmentMailDatetime" DataFormatString="{0:dd/MMM/yyyy}"
                                            HeaderText="Appointment Mail Date" NullDisplayText="#" />
                                        <asp:BoundField DataField="CustSamKamMailDatetime" DataFormatString="{0:dd/MMM/yyyy}"
                                            HeaderText="Sam/Sam-TL Mail Date" NullDisplayText="#" />
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
                                        <asp:BoundField DataField="Party_Name" HeaderText="Company Name" NullDisplayText="#" />
                                        <asp:BoundField DataField="StatusByFe" HeaderText="Audit Status" NullDisplayText="#" />
                                        <asp:BoundField DataField="Code" HeaderText="Code" NullDisplayText="#" />
                                        <asp:BoundField DataField="Remarks" HeaderText="Remarks" NullDisplayText="#" />
                                        <asp:BoundField DataField="StatusUpdateDate" DataFormatString="{0:dd/MMM/yyyy}" HeaderText="Date"
                                            NullDisplayText="#" />
                                        <asp:BoundField DataField="StatusTime" HeaderText="Time" NullDisplayText="#" />
                                        <asp:BoundField DataField="PersonMetName" HeaderText="Cust. Name" NullDisplayText="#" />
                                        <asp:BoundField DataField="PersonMetMobileNo" HeaderText="Contact No" NullDisplayText="#" />
                                        <asp:BoundField DataField="PersonMetEmail" HeaderText="Cust Mail Id" NullDisplayText="#" />
                                        <asp:BoundField DataField="AppointmentMailDatetime" DataFormatString="{0:dd/MMM/yyyy}"
                                            HeaderText="Appointment Mail Date" NullDisplayText="#" />
                                        <asp:BoundField DataField="CustSamKamMailDatetime" DataFormatString="{0:dd/MMM/yyyy}"
                                            HeaderText="RM Mail Date" NullDisplayText="#" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="clearfix pb10">
                            </div>
                            <div class="table table-responsive">
                                <b>PRI-Fixeds Line</b>
                                <div class="clearfix">
                                </div>
                                <asp:GridView ID="gvpri" runat="server" class="table table-responsive" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Party Name" HeaderText="Company Name" NullDisplayText="#" />
                                        <asp:BoundField DataField="StatusByFe" HeaderText="Audit Status" NullDisplayText="#" />
                                        <asp:BoundField DataField="Code" HeaderText="Code" NullDisplayText="#" />
                                        <asp:BoundField DataField="Remarks" HeaderText="Remarks" NullDisplayText="#" />
                                        <asp:BoundField DataField="StatusUpdateDate" DataFormatString="{0:dd/MMM/yyyy}" HeaderText="Date"
                                            NullDisplayText="#" />
                                        <asp:BoundField DataField="StatusTime" HeaderText="Time" NullDisplayText="#" />
                                        <asp:BoundField DataField="PersonMetName" HeaderText="Cust. Name" NullDisplayText="#" />
                                        <asp:BoundField DataField="PersonMetMobileNo" HeaderText="Contact No" NullDisplayText="#" />
                                        <asp:BoundField DataField="PersonMetEmail" HeaderText="Cust Mail Id" NullDisplayText="#" />
                                        <asp:BoundField DataField="AppointmentMailDatetime" DataFormatString="{0:dd/MMM/yyyy}"
                                            HeaderText="Appointment Mail Date" NullDisplayText="#" />
                                        <asp:BoundField DataField="CustSamKamMailDatetime" DataFormatString="{0:dd/MMM/yyyy}"
                                            HeaderText="RM Mail Date" NullDisplayText="#" />
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
