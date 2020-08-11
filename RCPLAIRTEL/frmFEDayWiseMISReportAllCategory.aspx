<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmFEDayWiseMISReportAllCategory.aspx.cs" Inherits="frmFEDayWiseMISReportAllCategory"
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
                        <div class="col-sm-9">
                            <asp:TextBox ID="txtdate" runat="server" class="form-control" type="date"></asp:TextBox>
                        </div>
                        <div class="col-sm-3">
                            <asp:Button ID="btnsearch" runat="server" class="btn btn-danger btn-sm btn-block"
                                Text="Search" OnClick="btnsearch_Click" />
                        </div>
                        <div class="clearfix mt10">
                        </div>
                        <div class="col-sm-9">
                        </div>
                        <div class="col-sm-3">
                            <asp:Button ID="btngetexcelsendmail" runat="server" class="btn btn-danger btn-sm btn-block"
                                Text="Send Mail & Excel" OnClick="btngetexcelsendmail_Click" />
                        </div>
                        <div class="clearfix mt10">
                        </div>

                        <h4 class="text-center">Leased Line Report</h4>
                        <div class="clearfix">
                        </div>
                        <div style="overflow: auto;">
                            <asp:GridView ID="gvll" runat="server" AutoGenerateColumns="false" Width="100%" class="table table-responsive"
                                OnRowDataBound="gvll_RowDataBound">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex +1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Fe_Name" HeaderText="Fe Name" />
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
                        <div class="clearfix mt10">
                        </div>
                        <h4 class="text-center">MO Report</h4>
                        <div class="clearfix">
                        </div>
                        <div style="overflow: auto;">
                            <asp:GridView ID="gvmo" runat="server" AutoGenerateColumns="false" Width="100%" class="table table-responsive"
                                OnRowDataBound="gvmo_RowDataBound">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex +1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Fe_Name" HeaderText="Fe Name" />
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
                        <div class="clearfix mt10">
                        </div>
                        <h4 class="text-center">PRI-Fixed Line Report</h4>
                        <div class="clearfix">
                        </div>
                        <div style="overflow: auto;">
                            <asp:GridView ID="gvpri" runat="server" AutoGenerateColumns="false" Width="100%" class="table table-responsive"
                                OnRowDataBound="gvpri_RowDataBound">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex +1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Fe_Name" HeaderText="Fe Name" />
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
                        <div class="clearfix mt10">
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
