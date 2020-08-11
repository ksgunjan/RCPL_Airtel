<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViweMasterFEjobHistory.aspx.cs" Inherits="ViweMasterFEjobHistory"
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
                                    <asp:Label ID="lblPageName" runat="server" Text="View Detail History of Assigned FE Data"></asp:Label>
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
                    <div class="clearfix" style="margin-top: 10px;"></div>
                    <div class="section-pannel">
                        <h4>Field with mark * are mandatory</h4>
                        <div class="clearfix" style="margin-top: 10px;">
                        </div>
                        <div class="col-sm-3">
                            <label class="col-sm-12 form-control-label">
                                Select Start Date *</label>
                            <div class="col-sm-12">
                                <asp:TextBox ID="txtdate1" runat="server" class="form-control" type="date"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <label class="col-sm-12 form-control-label">
                                Select End Date *</label>
                            <div class="col-sm-12">
                                <asp:TextBox ID="txtenddate1" runat="server" class="form-control" type="date"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-2">
                            <label class="col-sm-12 form-control-label">
                                Select FE
                            </label>
                            <div class="col-sm-12">
                                <asp:DropDownList ID="ddlfe1" runat="server" class="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-2">
                            <label class="col-sm-12 form-control-label">
                                Select Product
                            </label>
                            <div class="col-sm-12">
                                <asp:DropDownList ID="ddlproduct" runat="server" class="form-control">
                                    <asp:ListItem Selected="True" Value="Leased Line">Leased Line</asp:ListItem>
                                    <asp:ListItem Value="MO">MO</asp:ListItem>
                                    <asp:ListItem Value="PRI-Fixed Line">PRI-Fixed Line</asp:ListItem>

                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-2">
                            <label class="col-sm-12 form-control-label">
                                Select Audit Status
                            </label>
                            <div class="col-sm-12">
                                <asp:DropDownList ID="ddlauditstatus" runat="server" class="form-control">
                                    <asp:ListItem Selected="True" Value="01">Select Audit Status</asp:ListItem>
                                    <asp:ListItem Value="02">All</asp:ListItem>
                                    <asp:ListItem Value="Negative">Negative</asp:ListItem>
                                    <asp:ListItem Value="03">Pending</asp:ListItem>
                                    <asp:ListItem Value="Positive">Positive</asp:ListItem>
                                    <asp:ListItem Value="Pending">Revisit</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="clearfix" style="margin-top: 10px;">
                        </div>
                        <asp:Button ID="btnsearchlastjob" runat="server" class="btn btn-primary pull-right btn-sm"
                            Text="Search FE Last Job History" OnClick="btnsearchlastjob_Click" />
                        <div class="Clearfix" style="margin-top: 10px;">
                        </div>
                        <asp:Label ID="lbltotal" runat="server" class="btn btn-primary pull-left text-center"></asp:Label>

                        <div class="Clearfix" style="margin-top: 10px;">
                        </div>
                        <div style="overflow: auto;">
                            <asp:GridView ID="gvfedetaillist" runat="server" class="commonAjaxTbl dataTable master-company-table ViewProductTable table 
                                        display responsive no-wrap table-hover manage-user Grid table-responsive"
                                AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCreated="gvfedetaillist_RowCreated">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="UniqueID" HeaderText="Logical Circuit ID" NullDisplayText="#" />
                                    <asp:BoundField DataField="Party_Name" HeaderText="Company Name" NullDisplayText="#" />
                                    <asp:BoundField DataField="Product_Name" HeaderText="Product" NullDisplayText="#" />
                                    <asp:BoundField DataField="Prod_Type_Name" HeaderText="Product Type" NullDisplayText="#" />
                                    <asp:BoundField DataField="BILLING_BANDWIDTH" HeaderText="Bandwidth" NullDisplayText="#" />
                                    <asp:BoundField DataField="Account_Manager" HeaderText="Account Manager" NullDisplayText="#" />
                                    <asp:TemplateField HeaderText="Compliance Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcomp" runat="server" Text='<%# Eval("FE_Compliance_Date", "{0:dd/MMM/yyyy}").ToString() == "" ? "#" : Eval("FE_Compliance_Date", "{0:dd/MMM/yyyy}").ToString()%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status Update Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcompdate" runat="server" Text='<%# Eval("Status_Update_Date", "{0:dd/MMM/yyyy}").ToString() == "" ? "#" : Eval("Status_Update_Date", "{0:dd/MMM/yyyy}").ToString()%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status Update Time">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcomptime" runat="server" Text='<%# Eval("Status_Time").ToString() == "" ? "#" : Eval("Status_Time").ToString()%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Status_By_FE" HeaderText="Status" NullDisplayText="#" ItemStyle-ForeColor="Red" />
                                </Columns>
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#EFF3FB" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                            </asp:GridView>

                        </div>
                    </div>
                    <div class="Clearfix mt10">
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
