<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmMonthWiseFeWorkReport.aspx.cs"
    Inherits="frmMonthWiseFeWorkReport" MasterPageFile="~/MasterPage2.master" %>

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
                        Reports of FE Work</h3>
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
                            <div runat="server" id="divworkfeavgperday">
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlmonth" runat="server" class="form-control">
                                        <asp:ListItem Selected="True" Value="01">Select Month</asp:ListItem>
                                        <asp:ListItem Value="02">January</asp:ListItem>
                                        <asp:ListItem Value="03">February</asp:ListItem>
                                        <asp:ListItem Value="04">March</asp:ListItem>
                                        <asp:ListItem Value="05">April</asp:ListItem>
                                        <asp:ListItem Value="06">May</asp:ListItem>
                                        <asp:ListItem Value="07">June</asp:ListItem>
                                        <asp:ListItem Value="08">July</asp:ListItem>
                                        <asp:ListItem Value="09">August</asp:ListItem>
                                        <asp:ListItem Value="10">September</asp:ListItem>
                                        <asp:ListItem Value="11">October</asp:ListItem>
                                        <asp:ListItem Value="12">November</asp:ListItem>
                                        <asp:ListItem Value="13">December</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-6">
                                    <asp:Button ID="btnsearch" runat="server" Text="Get Monthly Report" class="btn btn-sm btn-block btn-primary"
                                        OnClick="btnsearch_Click" />
                                </div>
                            </div>
                            <div class="clearfix pb20">
                            </div>
                            <div class="table-responsive" runat="server" id="divexcel">
                                <asp:Button ID="btnreport" runat="server" class="btn btn-sm btn-success pull-right"
                                    Text="Send Mail" OnClick="btnreport_Click" />
                            </div>
                            <div class="clearfix pb10">
                            </div>
                            <strong>Leased Line Report (Current Month Day-Wise Work)</strong>
                            <asp:GridView ID="gvfedailyreport" runat="server" class="table" BackColor="White"
                                BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical"
                                ShowFooter="true">
                                <AlternatingRowStyle BackColor="#DCDCDC" />
                                <Columns>
                                </Columns>
                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#0000A9" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#000065" />
                            </asp:GridView>
                            <div class="clearfix pb10">
                            </div>
                            <strong>MO-Report (Current Month Day-Wise Work)</strong>
                            <div class="table-responsive" runat="server" id="div1">
                                <asp:GridView ID="gridviewMo" runat="server" class="table" BackColor="White" BorderColor="#999999"
                                    BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowFooter="true">
                                    <AlternatingRowStyle BackColor="#DCDCDC" />
                                    <Columns>
                                    </Columns>
                                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#0000A9" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#000065" />
                                </asp:GridView>
                            </div>
                            <div class="clearfix pb10">
                            </div>
                            <strong>PRI-Fixed Line (Current Month Day-Wise Work)</strong>
                            <asp:GridView ID="gvpri" runat="server" class="table" BackColor="White" BorderColor="#999999"
                                BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowFooter="true">
                                <AlternatingRowStyle BackColor="#DCDCDC" />
                                <Columns>
                                </Columns>
                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#0000A9" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#000065" />
                            </asp:GridView>
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
