<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmAllWorkMonthReport.aspx.cs" Inherits="frmAllWorkMonthReport"
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
                                    <asp:Label ID="lblPageName" runat="server" Text="Reports of Monthly Work (By- Status Update Date)"></asp:Label>
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
                        <div class="table-responsive" runat="server" id="divexcel">
                            <asp:Button ID="btnreport" runat="server" class="btn btn-sm btn-success pull-right"
                                Text="Send Mail" OnClick="btnreport_Click" />
                            <div class="clearfix mt10">
                            </div>
                            <div class="ml10">
                                <h4 class="text-center">Leased Line</h4>
                            </div>
                            <div class="clearfix mt10">
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
                            <div class="clearfix mt10">
                            </div>
                            <div class="ml10">
                                <h4 class="text-center">MO</h4>
                            </div>
                            <div class="clearfix mt10">
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
                            <div class="clearfix mt10">
                            </div>
                            <div class="ml10">
                                <h4 class="text-center">PRI-Fixed Line</h4>
                            </div>
                            <div class="clearfix mt10">
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
                            <div class="clearfix mt10">
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
