<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmFEDayWiseMISReportAllCategory.aspx.cs"
    Inherits="frmFEDayWiseMISReportAllCategory" MasterPageFile="~/MasterPage2.master" %>

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
                            <div class="col-sm-9">
                                <asp:TextBox ID="txtdate" runat="server" class="form-control" type="date"></asp:TextBox>
                            </div>
                            <div class="col-sm-3">
                                <asp:Button ID="btnsearch" runat="server" class="btn btn-danger btn-sm btn-block"
                                    Text="Search" OnClick="btnsearch_Click" />
                            </div>
                            <div class="clearfix pb10">
                            </div>
                            <div class="col-sm-9">
                            </div>
                            <div class="col-sm-3">
                                <asp:Button ID="btngetexcelsendmail" runat="server" class="btn btn-danger btn-sm btn-block"
                                    Text="Send Mail & Excel" OnClick="btngetexcelsendmail_Click" />
                            </div>
                            <div class="clearfix pb10">
                            </div>
                            <div class="table table-responsive">
                                <b>Leased Line Report</b>
                                <div class="clearfix pb5">
                                </div>
                                <asp:GridView ID="gvll" runat="server" AutoGenerateColumns="false" Width="100%" class="table table-responsive"
                                    OnRowDataBound="gvll_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex +1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Fe Name" HeaderText="Fe Name" />
                                        <asp:TemplateField HeaderText="Positive Audit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpositive" runat="server" Text='<%#Eval("Positive") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Negative Audit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnegative" runat="server" Text='<%#Eval("Negative") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Revisit Audit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpending" runat="server" Text='<%#Eval("Pending") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Grand Total" SortExpression="total">
                                            <FooterTemplate>
                                                <asp:Label ID="lbltotal" runat="server" Text=""></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="clearfix pb10">
                            </div>
                            <div class="table table-responsive">
                                <b>MO Report</b>
                                <div class="clearfix pb5">
                                </div>
                                <asp:GridView ID="gvmo" runat="server" AutoGenerateColumns="false" Width="100%" class="table table-responsive"
                                    OnRowDataBound="gvmo_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex +1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Fe Name" HeaderText="Fe Name" />
                                        <asp:TemplateField HeaderText="Positive Audit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpositivemo" runat="server" Text='<%#Eval("Positive") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Negative Audit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnegativemo" runat="server" Text='<%#Eval("Negative") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Revisit Audit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpendingmo" runat="server" Text='<%#Eval("Pending") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Grand Total" SortExpression="total">
                                            <FooterTemplate>
                                                <asp:Label ID="lbltotalmo" runat="server" Text=""></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="clearfix pb10">
                            </div>
                            <div class="table table-responsive">
                                <b>PRI-Fixed Line Report</b>
                                <div class="clearfix pb5">
                                </div>
                                <asp:GridView ID="gvpri" runat="server" AutoGenerateColumns="false" Width="100%" class="table table-responsive"
                                    OnRowDataBound="gvpri_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex +1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Fe Name" HeaderText="Fe Name" />
                                        <asp:TemplateField HeaderText="Positive Audit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpositivepri" runat="server" Text='<%#Eval("Positive") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Negative Audit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnegativepri" runat="server" Text='<%#Eval("Negative") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Revisit Audit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpendingpri" runat="server" Text='<%#Eval("Pending") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Grand Total" SortExpression="total">
                                            <FooterTemplate>
                                                <asp:Label ID="lbltotalpri" runat="server" Text=""></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="clearfix pb10">
                            </div>
                        </div>
                        <div class="clearfix pb10">
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
