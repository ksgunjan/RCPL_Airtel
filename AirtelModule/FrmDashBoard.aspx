<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmDashBoard.aspx.cs" Inherits="FrmDashBoard"
    MasterPageFile="~/MasterPage2.master" %>

<asp:Content ID="conheader" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="conplace" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="right_col" role="main">
        <div class="">
            <div class="title_left">
                <h3>
                    Dashboard</h3>
            </div>
            <div class="row top_tiles">
                <div class="animated flipInY col-lg-3 col-md-3 col-sm-6 col-xs-12">
                    <div class="tile-stats">
                        <div class="icon pt5">
                            <i class="fa fa-industry"></i>
                        </div>
                        <div class="count" runat="server" id="divcompany">
                        </div>
                        <h3>
                            Registered Company</h3>
                        <p>
                            Total registerd companies with us.</p>
                    </div>
                </div>
                <div class="animated flipInY col-lg-3 col-md-3 col-sm-6 col-xs-12">
                    <div class="tile-stats">
                        <div class="icon pt5">
                            <i class="fa fa-product-hunt"></i>
                        </div>
                        <div class="count" runat="server" id="divproduct">
                        </div>
                        <h3>
                            Total Products</h3>
                        <p>
                            Total comapnies products.</p>
                    </div>
                </div>
                <div class="animated flipInY col-lg-3 col-md-3 col-sm-6 col-xs-12">
                    <div class="tile-stats">
                        <div class="icon pt5">
                            <i class="fa fa-product-hunt"></i>
                        </div>
                        <div class="count" runat="server" id="divprotype">
                        </div>
                        <h3>
                            Total Product Type</h3>
                        <p>
                            Total product type of products.</p>
                    </div>
                </div>
                <div class="animated flipInY col-lg-3 col-md-3 col-sm-6 col-xs-12">
                    <div class="tile-stats">
                        <div class="icon pt5">
                            <i class="fa fa-map-marker"></i>
                        </div>
                        <div class="count" runat="server" id="divzone">
                        </div>
                        <h3>
                            Total Zone</h3>
                        <p>
                            Total registerd zone.</p>
                    </div>
                </div>
                <div class="animated flipInY col-lg-3 col-md-3 col-sm-6 col-xs-12">
                    <div class="tile-stats">
                        <div class="icon pt5">
                            <i class="fa fa-map-marker"></i>
                        </div>
                        <div class="count" runat="server" id="divzonearea">
                        </div>
                        <h3>
                            Total Zone Area</h3>
                        <p>
                            Total zone area.</p>
                    </div>
                </div>
                <%--<div class="animated flipInY col-lg-3 col-md-3 col-sm-6 col-xs-12">
                    <div class="tile-stats">
                        <div class="icon pt5">
                            <i class="fa fa-location-arrow"></i>
                        </div>
                        <div class="count" runat="server" id="divzonemapping">
                        </div>
                        <h3>
                            Total Zone Mapping</h3>
                        <p>
                            Total Zone Unders Zones Area</p>
                    </div>
                </div>--%>
                <div class="animated flipInY col-lg-3 col-md-3 col-sm-6 col-xs-12">
                    <div class="tile-stats">
                        <div class="icon pt5">
                            <i class="fa fa-database"></i>
                        </div>
                        <div class="count" runat="server" id="divrowdata">
                        </div>
                        <h3>
                            Total Raw Data</h3>
                        <p>
                            Total raw data inserted through excel</p>
                    </div>
                </div>
                <div class="animated flipInY col-lg-3 col-md-3 col-sm-6 col-xs-12">
                    <div class="tile-stats">
                        <div class="icon pt5">
                            <i class="fa fa-user-plus"></i>
                        </div>
                        <div class="count" runat="server" id="divuser">
                        </div>
                        <h3>
                            Total Users</h3>
                        <p>
                            Total registerd FE, Managers and Admin</p>
                    </div>
                </div>
                <div class="animated flipInY col-lg-3 col-md-3 col-sm-6 col-xs-12">
                    <div class="tile-stats">
                        <div class="icon pt5">
                            <i class="fa fa-mail-forward"></i>
                        </div>
                        <div class="count" runat="server" id="divemail">
                        </div>
                        <h3>
                            Total Emails</h3>
                        <p>
                            Total registerd emails with us.</p>
                    </div>
                </div>
                <div class="clearfix pb10">
                </div>
                <div class=" table-responsive bg-info">
                    <%--<asp:Button ID="btnsendmail" runat="server" class="btn btn-danger pull-right mr10 mt5"
                        Text="Send Mail" onclick="btnsendmail_Click" />--%>
                    <div class="clearfix pb10">
                    </div>
                    <h3>
                        Leased Line Fe Details</h3>
                    <div class="clearfix pb10">
                    </div>
                    <asp:GridView ID="gvdetailleasedline" runat="server" AutoGenerateColumns="false"
                        class="table table-responsive" OnRowDataBound="gvdetailleasedline_RowDataBound">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fe Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblfename" runat="server" Text='<%#Eval("Fe Name") %>'></asp:Label>
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
                    <h3>
                        Mobility Fe Details</h3>
                    <div class="clearfix pb10">
                    </div>
                    <asp:GridView ID="gvdetailMo" runat="server" AutoGenerateColumns="false" class="table table-responsive"
                        OnRowDataBound="gvdetailMo_RowDataBound">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fe Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblfename1" runat="server" Text='<%#Eval("Fe Name") %>'></asp:Label>
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
</asp:Content>
