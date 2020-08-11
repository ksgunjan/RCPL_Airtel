<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmDashBoard.aspx.cs" Inherits="FrmDashBoard"
    MasterPageFile="~/MasterPage.master" %>

<asp:Content ContentPlaceHolderID="head" runat="server" ID="header">
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server" ID="inner">
    <div class="content oem-content">
        <div class="sideBg">
            <div class="row">
                <div class="col-mod-12 padding_0">
                    <ul class="breadcrumb">
                        <li>
                            <asp:Label ID="lblPageName" runat="server" Text="Dashboard"></asp:Label>
                        </li>
                    </ul>
                </div>
            </div>
            <div class="addfdi">
                <div class="admin-dashboard">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-lg-3 col-sm-6 col-xs-12">
                                    <div class="white-box analytics-info total-comp">
                                        <ul class="list-inline two-part">
                                            <li>
                                                <div class="icon-box">
                                                    <i class="far fa-building"></i>
                                                </div>
                                                <div class="compName">
                                                    <h3 class="box-title">Registered Company</h3>
                                                    <div class="Number">

                                                        <asp:Label ID="lbltotoalcomp" CssClass="comp_number" runat="server" Text="0"></asp:Label>
                                                    </div>
                                                </div>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                                <div class="col-lg-3 col-sm-6 col-xs-12">
                                    <div class="white-box analytics-info total-fdi">
                                        <ul class="list-inline two-part">
                                            <li>
                                                <div class="icon-box">
                                                    <i class="far fa-building"></i>
                                                </div>
                                                <div class="compName">
                                                    <h3 class="box-title">Total Products</h3>
                                                    <div class="Number">
                                                        <asp:Label ID="lbltotalproduct" runat="server" CssClass="comp_number" Text="0"></asp:Label>
                                                    </div>
                                                </div>
                                            </li>
                                        </ul>

                                    </div>
                                </div>
                                <div class="col-lg-3 col-sm-6 col-xs-12">
                                    <div class="white-box analytics-info last-fdi">
                                        <ul class="list-inline two-part">
                                            <li>
                                                <div class="icon-box">
                                                    <i class="far fa-building"></i>
                                                </div>
                                                <div class="compName">
                                                    <h3 class="box-title">Total Product Type</h3>
                                                    <div class="Number">
                                                        <asp:Label ID="lblprodtype" runat="server" CssClass="comp_number" Text="0"></asp:Label>
                                                    </div>
                                                </div>
                                            </li>
                                        </ul>

                                    </div>
                                </div>
                                <div class="col-lg-3 col-sm-6 col-xs-12">
                                    <div class="white-box analytics-info last-fdi">
                                        <ul class="list-inline two-part">
                                            <li>
                                                <div class="icon-box">
                                                    <i class="fa fa-users" aria-hidden="true"></i>
                                                </div>
                                                <div class="compName">
                                                    <h3 class="box-title">Total Emails</h3>
                                                    <div class="Number">
                                                        <asp:LinkButton ID="lbltotalemail" runat="server" CssClass="comp_number" Text="0"></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </li>
                                        </ul>

                                    </div>
                                </div>
                            </div>
                            <div class="row" style="margin-top: 10px;">
                                <div class="col-lg-6 col-sm-6 col-xs-12">
                                    <div class="white-box analytics-info last-fdi">
                                        <ul class="list-inline two-part">
                                            <li>
                                                <div class="icon-box">
                                                    <i class="fa fa-users"></i>
                                                </div>
                                                <div class="compName">
                                                    <h3 class="box-title">Total Users</h3>
                                                    <div class="Number">
                                                        <asp:Label ID="lbltotaluser" runat="server" CssClass="comp_number" Text="0"></asp:Label>
                                                    </div>
                                                </div>
                                            </li>
                                        </ul>

                                    </div>
                                </div>
                                <div class="col-lg-6 col-sm-6 col-xs-12">
                                    <div class="white-box analytics-info last-fdi">
                                        <ul class="list-inline two-part">
                                            <li>
                                                <div class="icon-box">
                                                    <i class="fab fa-product-hunt" aria-hidden="true"></i>
                                                </div>
                                                <div class="compName">

                                                    <h3 class="box-title">Total Raw Data</h3>
                                                    <div class="Number">
                                                        <asp:Label ID="lblrowdatall" runat="server" CssClass="comp_number" Text="0"></asp:Label>
                                                    </div>
                                                </div>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                            <div class="row" style="margin-top: 10px;">
                                <div class="col-lg-6 col-sm-6 col-xs-12">
                                    <div class="white-box analytics-info last-fdi">
                                        <ul class="list-inline two-part">
                                            <li>
                                                <div class="icon-box">
                                                    <i class="fab fa-product-hunt"></i>
                                                </div>
                                                <div class="compName">
                                                    <h3 class="box-title">Total RawData PRI</h3>
                                                    <div class="Number">
                                                        <asp:Label ID="lblpritotal" runat="server" CssClass="comp_number" Text="0"></asp:Label>
                                                    </div>
                                                </div>
                                            </li>
                                        </ul>

                                    </div>
                                </div>
                                <div class="col-lg-6 col-sm-6 col-xs-12">
                                    <div class="white-box analytics-info last-fdi">
                                        <ul class="list-inline two-part">
                                            <li>
                                                <div class="icon-box">
                                                    <i class="fab fa-product-hunt" aria-hidden="true"></i>
                                                </div>
                                                <div class="compName">

                                                    <h3 class="box-title">Total Raw Data MO</h3>
                                                    <div class="Number">
                                                        <asp:Label ID="lbltotalmo" runat="server" CssClass="comp_number" Text="0"></asp:Label>
                                                    </div>
                                                </div>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                            <div class="row" style="margin-top: 10px;">
                                <div class="col-md-12">
                                    <h3>Leased Line Fe Details</h3>
                                    <asp:GridView ID="gvdetailleasedline" runat="server" AutoGenerateColumns="false"
                                        class="commonAjaxTbl dataTable master-company-table ViewProductTable table 
                                        display responsive no-wrap table-hover manage-user Grid table-responsive"
                                        OnRowDataBound="gvdetailleasedline_RowDataBound" OnRowCreated="gvdetailleasedline_RowCreated">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Fe Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblfename" runat="server" Text='<%#Eval("Fe_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Positive Audit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblpositive" runat="server" Text='<%#Eval("Positive") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Negative Audit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblneg" runat="server" Text='<%#Eval("Negative") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Revisit Audit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblpend" runat="server" Text='<%#Eval("Pending") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remaining Audit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblremaintotal" runat="server" Text='<%#Eval("PendingAudit") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total AssignJob">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbltotal" runat="server" Text='<%#Eval("TotalAssignJob") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <div class="clearfix pb10">
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <h3>PRI Fe Details</h3>
                                    <asp:GridView ID="gvdetailpri" runat="server" AutoGenerateColumns="false" class="commonAjaxTbl dataTable master-company-table ViewProductTable table 
                                        display responsive no-wrap table-hover manage-user Grid table-responsive"
                                        OnRowDataBound="gvdetailpri_RowDataBound" OnRowCreated="gvdetailpri_RowCreated">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Fe Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblfename1" runat="server" Text='<%#Eval("Fe_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Positive Audit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblpositive1" runat="server" Text='<%#Eval("Positive") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Negative Audit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblneg1" runat="server" Text='<%#Eval("Negative") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Revisit Audit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblpend1" runat="server" Text='<%#Eval("Pending") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remaining Audit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblremaintotal1" runat="server" Text='<%#Eval("PendingAudit") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total AssignJob">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbltotal1" runat="server" Text='<%#Eval("TotalAssignJob") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="footer">© 2019 <a href="#">GIP</a> </div>
</asp:Content>
