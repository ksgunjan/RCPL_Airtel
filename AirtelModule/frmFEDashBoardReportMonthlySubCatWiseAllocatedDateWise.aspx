<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmFEDashBoardReportMonthlySubCatWiseAllocatedDateWise.aspx.cs"
    Inherits="frmFEDashBoardReportMonthlySubCatWiseAllocatedDateWise" MasterPageFile="~/MasterPage2.master" %>

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
                    <strong>Reports of Monthly Work (By- Allocation Date Wise)</strong>
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
                                <asp:Button ID="btnreport" runat="server" Visible="false" class="btn btn-sm btn-success pull-right"
                                    Text="Send Mail" OnClick="btnreport_Click" />
                                <div class="clearfix pb10">
                                </div>
                                <div class="ml10">
                                    <b>Leased Line-Initial</b>
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
                                        <asp:TemplateField HeaderText="Pending Audit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpending" runat="server" HeaderText="Revisit Audit" Text='<%#Eval("Pending") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Revisit Audit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrevisit" runat="server" HeaderText="Revisit Audit" Text='<%#Eval("Revisit") %>'></asp:Label>
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
                                    <b>Leased Line-Periodic</b>
                                </div>
                                <div class="clearfix pb10">
                                </div>
                                <asp:GridView ID="gvllcat2" runat="server" AutoGenerateColumns="false" class="table"
                                    BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                                    CellPadding="3" GridLines="Vertical" OnRowDataBound="gvllcat2_RowDataBound">
                                    <AlternatingRowStyle BackColor="#DCDCDC" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr.No.">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Month">
                                            <ItemTemplate>
                                                <asp:Label ID="lblmonthlcat2" runat="server" HeaderText="Negative Audit" Text='<%#Eval("Month") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Not Eligible">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnoteligiblelcat2" runat="server" HeaderText="Not Eligible" Text='<%#Eval("Not Eligible") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Negative Audit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnegativelcat2" runat="server" HeaderText="Negative Audit" Text='<%#Eval("Negative") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Positive Audit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpositivelcat2" runat="server" HeaderText="Positive Audit" Text='<%#Eval("Positive") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Pending Audit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpendinglcat2" runat="server" HeaderText="Revisit Audit" Text='<%#Eval("Pending") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Revisit Audit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrevisitlcat2" runat="server" HeaderText="Revisit Audit" Text='<%#Eval("Revisit") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total" SortExpression="total">
                                            <FooterTemplate>
                                                <asp:Label ID="lbltotallcat2" runat="server" Text=""></asp:Label>
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
                                    <b>MO-Periodic</b>
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
                                        <asp:TemplateField HeaderText="Pending Audit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpendingmo" runat="server" HeaderText="Pending Audit" Text='<%#Eval("Pending") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Revisit Audit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrevisitmo" runat="server" HeaderText="Revisit Audit" Text='<%#Eval("Revisit") %>'></asp:Label>
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
                                    <b>MO-Quaterly</b>
                                </div>
                                <div class="clearfix pb10">
                                </div>
                                <asp:GridView ID="gvmocat2" runat="server" AutoGenerateColumns="false" class="table"
                                    BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                                    CellPadding="3" GridLines="Vertical" OnRowDataBound="gvmocat2_RowDataBound">
                                    <AlternatingRowStyle BackColor="#DCDCDC" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr.No.">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Month">
                                            <ItemTemplate>
                                                <asp:Label ID="lblmonthmomcat2" runat="server" HeaderText="Negative Audit" Text='<%#Eval("Month") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Not Eligible">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnoteligiblemomcat2" runat="server" HeaderText="Not Eligible" Text='<%#Eval("Not Eligible") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Negative Audit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnegativemomcat2" runat="server" HeaderText="Negative Audit" Text='<%#Eval("Negative") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Positive Audit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpositivemomcat2" runat="server" HeaderText="Positive Audit" Text='<%#Eval("Positive") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Pending Audit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpendingmomcat2" runat="server" HeaderText="Pending Audit" Text='<%#Eval("Pending") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Revisit Audit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrevisitmomcat2" runat="server" HeaderText="Revisit Audit" Text='<%#Eval("Revisit") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total" SortExpression="total">
                                            <FooterTemplate>
                                                <asp:Label ID="lbltotalmomcat2" runat="server" Text=""></asp:Label>
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
                                    <b>PRI-Fiexd Line</b>
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
                                        <asp:TemplateField HeaderText="Pending Audit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpendingpri" runat="server" HeaderText="Revisit Audit" Text='<%#Eval("Pending") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Revisit Audit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrevisitpri" runat="server" HeaderText="Revisit Audit" Text='<%#Eval("Revisit") %>'></asp:Label>
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
