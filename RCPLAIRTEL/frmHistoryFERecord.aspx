<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmHistoryFERecord.aspx.cs" Inherits="frmHistoryFERecord"
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
                                    <asp:Label ID="lblPageName" runat="server" Text="View Detail History of Assigned Data"></asp:Label>
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
                        <div class="form-group row">
                            <div class="col-sm-12 text-center">
                                <h4>
                                    <asp:RadioButtonList runat="server" ID="rbcheck" class="" RepeatColumns="4" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow">
                                        <asp:ListItem Selected="Yes" Value="All" class="btn btn-round btn-sm btn-primary">All</asp:ListItem>
                                        <asp:ListItem Value="Positive" class="btn btn-round btn-sm btn-primary">Positive</asp:ListItem>
                                        <asp:ListItem Value="Negative" style="margin-left: 1px;" class="btn btn-round btn-sm btn-primary">Negative</asp:ListItem>
                                        <asp:ListItem Value="Pending" style="margin-left: 1px;" class="btn btn-round btn-sm btn-primary">Pending</asp:ListItem>
                                    </asp:RadioButtonList>
                                </h4>
                            </div>
                        </div>
                        <div class="Clearfix mt10">
                        </div>
                        <div class="form-group row">
                            <div class="col-sm-4">
                                <label class="col-sm-12 form-control-label">
                                    Select Product Type *</label>
                                <div class="col-sm-12">
                                    <asp:DropDownList ID="ddlprod" runat="server" class="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <label class="col-sm-12 form-control-label">
                                    Select Start Date *</label>
                                <div class="col-sm-12">
                                    <asp:TextBox ID="txtdate" runat="server" class="form-control" type="date"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <label class="col-sm-12 form-control-label">
                                    Select End Date *</label>
                                <div class="col-sm-12">
                                    <asp:TextBox ID="txtenddate" runat="server" class="form-control" type="date"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="Clearfix mt10">
                        </div>
                        <div class="form-group row">
                            <label class="col-sm-9 form-control-label">
                            </label>
                            <div class="col-sm-3">
                                <asp:Button ID="btnsearch" runat="server" class="btn btn-primary btn-sm btn-block"
                                    Text="Search" OnClick="btnsearch_Click" />
                            </div>
                        </div>
                        <div class="Clearfix mt10">
                        </div>
                        <div class="col-sm-9">
                            <h4>
                                <asp:Label ID="lbltotal" runat="server" class="label label-primary pull-right"></asp:Label>
                            </h4>
                        </div>
                        <div class="clearfix mt10"></div>
                        <asp:GridView ID="gvexcel" runat="server" AutoGenerateColumns="false" class="commonAjaxTbl dataTable master-company-table ViewProductTable table 
                                        display responsive no-wrap table-hover manage-user Grid table-responsive"
                            OnRowDataBound="gvexcel_RowDataBound" OnRowCreated="gvexcel_RowCreated">
                            <Columns>
                                <asp:BoundField DataField="Customer_Name" HeaderText="Customer Name" NullDisplayText="#" />
                                <asp:BoundField DataField="UniqueEID" HeaderText="Logical/Billed/Sub Del/No" NullDisplayText="#" />
                                <asp:BoundField DataField="From_Site" HeaderText="Customer Address-1" NullDisplayText="#" />
                                <asp:BoundField DataField="Product_Name" HeaderText="Product Name" NullDisplayText="#" />
                                <%--<asp:BoundField DataField="Commissioning_Date" HeaderText="Commissioning Date" DataFormatString="{0:dd/MMM/yyyy}"
                                    NullDisplayText="#" />--%>
                                <%--<asp:BoundField DataField="Account_Manager" HeaderText="Account Manager" NullDisplayText="#" />--%>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Label ID="hfstatusfe" runat="server" Text='<%#Eval("Status_By_FE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Revisit Date">
                                    <ItemTemplate>
                                        <asp:Label ID="hfrevisitdate" runat="server" Text='<%#Eval("Revisit_Date_Time", "{0:dd/MMM/yyyy}").ToString() == "" ? "#" : Eval("Revisit_Date_Time", "{0:dd/MMM/yyyy}").ToString()%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Compliance Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcomp" runat="server" Text='<%#Eval("FE_Compliance_Date", "{0:dd/MMM/yyyy}").ToString() == "" ? "#" : Eval("FE_Compliance_Date", "{0:dd/MMM/yyyy}").ToString()%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="clearfix mt10"></div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
