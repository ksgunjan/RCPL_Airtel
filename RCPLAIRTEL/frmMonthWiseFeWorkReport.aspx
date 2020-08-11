<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmMonthWiseFeWorkReport.aspx.cs" Inherits="frmMonthWiseFeWorkReport"
    MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="conhead" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="contentmain" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="content oem-content">
        <asp:UpdatePanel runat="server" ID="updatepan">
            <ContentTemplate>
                <div class="sideBg">
                    <div class="row">
                        <div class="col-md-12 padding_0">
                            <ul class="breadcrumb">
                                <li>
                                    <asp:Label ID="lblPageName" runat="server" Text="Reports of FE Work"></asp:Label>
                                </li>
                            </ul>
                        </div>
                        <div class="col-md-12">
                            <div class="clearfix"></div>
                            <div style="margin-top: 5px;">
                                <a class="fa fa-arrow-circle-left pull-right" href="javascript: history.go(-1)">&nbsp; &nbsp;Back</a>
                            </div>
                            <div class="clearfix"></div>
                        </div>
                    </div>
                    <div class="clearfix" style="margin-bottom: 10px;"></div>
                    <div class="section-pannel">
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
                        <div class="clearfix mt10">
                        </div>
                        <div class="table-responsive" runat="server" id="divexcel">
                            <asp:Button ID="btnreport" runat="server" class="btn btn-sm btn-success pull-right"
                                Text="Send Mail" OnClick="btnreport_Click" />
                        </div>
                        <div class="clearfix mt10">
                        </div>
                        <h4 class="text-center">Leased Line Report (Current Month Day-Wise Work)</h4>
                        <asp:GridView ID="gvfedailyreport" runat="server" class="commonAjaxTbl dataTable master-company-table ViewProductTable table 
                                        display responsive no-wrap table-hover manage-user Grid table-responsive"
                            BackColor="White"
                            BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical"
                            ShowFooter="true" onrowcreated="gvfedailyreport_RowCreated">
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
                        <div class="clearfix mt10">
                        </div>
                        <h4 class="text-center">MO-Report (Current Month Day-Wise Work)</h4>
                        <div class="table-responsive" runat="server" id="div1">
                            <asp:GridView ID="gridviewMo" runat="server" class="commonAjaxTbl dataTable master-company-table ViewProductTable table 
                                        display responsive no-wrap table-hover manage-user Grid table-responsive"
                                BackColor="White" BorderColor="#999999"
                                BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowFooter="true" onrowcreated="gridviewMo_RowCreated">
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
                        <div class="clearfix mt10">
                        </div>
                        <h4 class="text-center">PRI-Fixed Line (Current Month Day-Wise Work)</h4>
                        <asp:GridView ID="gvpri" runat="server" class="commonAjaxTbl dataTable master-company-table ViewProductTable table 
                                        display responsive no-wrap table-hover manage-user Grid table-responsive"
                            BackColor="White" BorderColor="#999999"
                            BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" ShowFooter="true" onrowcreated="gvpri_RowCreated">
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
                        <div class="clearfix mt10">
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
