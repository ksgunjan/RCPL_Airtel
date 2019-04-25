<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmAllWorkMonthReport.aspx.cs"
    Inherits="frmAllWorkMonthReport" MasterPageFile="~/MasterPage2.master" %>

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
                    <strong>Reports of Monthly Work (By- Status Update Date)</strong>
                </div>
            </div>
            <asp:UpdatePanel ID="updatepanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="card-body">
                        <div class="form-horizontal bg-info border-aero">
                            <div class="clearfix pb2">
                            </div>
                            <div class="form-group row">
                                <div runat="server" id="diverror" class="col-sm-12">
                                </div>
                            </div>
                            <div class="clearfix pb5">
                            </div>
                            <div class="table-responsive" runat="server" id="divexcel">
                                <asp:Button ID="btnreport" runat="server" class="btn btn-sm btn-success pull-right"
                                    Text="Send Mail" OnClick="btnreport_Click" />
                                <div class="clearfix pb10">
                                </div>
                                <div class="ml10">
                                    <b>Leased Line</b>
                                </div>
                                <div class="clearfix pb10">
                                </div>
                                <asp:GridView ID="gvfemonthwisereport" runat="server" AutoGenerateColumns="false"
                                    class="table" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                                    CellPadding="3" GridLines="Vertical" OnRowDataBound="gvfemonthwisereport_RowDataBound">
                                    <AlternatingRowStyle BackColor="#DCDCDC" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr.No.">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Month">
                                            <ItemTemplate>
                                                <asp:Label ID="lblmonth" runat="server" HeaderText="Negative Audit" Text='<%#Eval("Month") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Not Eligible">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnoteligible" runat="server" HeaderText="Not Eligible" Text='<%#Eval("Not Eligible") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Negative Audit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnegative" runat="server" HeaderText="Negative Audit" Text='<%#Eval("Negative") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Positive Audit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpositive" runat="server" HeaderText="Positive Audit" Text='<%#Eval("Positive") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Revisit Audit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrevist" runat="server" HeaderText="Revisit Audit" Text='<%#Eval("Revisit") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total" SortExpression="total">
                                            <FooterTemplate>
                                                <asp:Label ID="lbltotal" runat="server" Text=""></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
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
                                <div class="ml10">
                                    <b>MO</b>
                                </div>
                                <div class="clearfix pb10">
                                </div>
                                <asp:GridView ID="gvmo" runat="server" AutoGenerateColumns="false" class="table"
                                    BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                                    CellPadding="3" GridLines="Vertical" OnRowDataBound="gvmo_RowDataBound">
                                    <AlternatingRowStyle BackColor="#DCDCDC" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr.No.">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Month">
                                            <ItemTemplate>
                                                <asp:Label ID="lblmonthmo" runat="server" HeaderText="Negative Audit" Text='<%#Eval("Month") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Not Eligible">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnoteligiblemo" runat="server" HeaderText="Not Eligible" Text='<%#Eval("Not Eligible") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Negative Audit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnegativemo" runat="server" HeaderText="Negative Audit" Text='<%#Eval("Negative") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Positive Audit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpositivemo" runat="server" HeaderText="Positive Audit" Text='<%#Eval("Positive") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Revisit Audit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrevistmo" runat="server" HeaderText="Revisit Audit" Text='<%#Eval("Revisit") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total" SortExpression="total">
                                            <FooterTemplate>
                                                <asp:Label ID="lbltotalmo" runat="server" Text=""></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
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
                                <div class="ml10">
                                    <b>PRI-Fixed Line</b>
                                </div>
                                <div class="clearfix pb10">
                                </div>
                                <asp:GridView ID="gvprifixedline" runat="server" AutoGenerateColumns="false" class="table"
                                    BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                                    CellPadding="3" GridLines="Vertical" OnRowDataBound="gvprifixedline_RowDataBound">
                                    <AlternatingRowStyle BackColor="#DCDCDC" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr.No.">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Month">
                                            <ItemTemplate>
                                                <asp:Label ID="lblmonthpri" runat="server" HeaderText="Negative Audit" Text='<%#Eval("Month") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Not Eligible">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnoteligiblepri" runat="server" HeaderText="Not Eligible" Text='<%#Eval("Not Eligible") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Negative Audit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnegativepri" runat="server" HeaderText="Negative Audit" Text='<%#Eval("Negative") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Positive Audit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpositivepri" runat="server" HeaderText="Positive Audit" Text='<%#Eval("Positive") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Revisit Audit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrevistpri" runat="server" HeaderText="Revisit Audit" Text='<%#Eval("Revisit") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total" SortExpression="total">
                                            <FooterTemplate>
                                                <asp:Label ID="lbltotalpri" runat="server" Text=""></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
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
