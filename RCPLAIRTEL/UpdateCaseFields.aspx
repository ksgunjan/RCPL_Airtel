<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdateCaseFields.aspx.cs" Inherits="UpdateCaseFields" MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="conhead" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="contentmain" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="content oem-content">

        <div class="sideBg">
            <div class="row">
                <div class="col-md-12 padding_0">
                    <ul class="breadcrumb">
                        <li>
                            <asp:Label ID="lblPageName" runat="server" Text="Update Fields"></asp:Label>
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
                <div>
                    1. In Case of Mo Please Enter (Company/Party Name)
                                <div class="clearfix mt10">
                                </div>
                    2. Other Case you can search record by enter Customer/Company/Party Name
                </div>
                <div class="clearfix mt10">
                </div>
                <div class="col-sm-4">
                    <asp:DropDownList ID="ddlcategory" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
                <div class="col-sm-4">
                    <asp:TextBox ID="txtsearch" runat="server" CssClass="form-control" Placeholder="Enter Search Criteria"></asp:TextBox>
                </div>
                <div class="col-sm-4">
                    <asp:Button ID="btnsearchrecord" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnsearchrecord_Click" />
                </div>
               <div class="clearfix" style="margin-bottom: 10px;"></div>
                <div>
                    <div class="table table-responsive">
                        <asp:GridView ID="gvexcel" runat="server" AutoGenerateColumns="false" class="table table-hover"
                            OnRowCommand="gvexcel_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="Logical/SubDel/Billed No">
                                    <ItemTemplate>
                                        <asp:Label ID="lbluid" runat="server" Text='<%#Eval("UniqueEID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Party_Name" HeaderText="Party Name" />
                                <asp:BoundField DataField="Coordinator_Contact_Email" HeaderText="Customer Email" />
                                <asp:BoundField DataField="Coordinator_Name" HeaderText="Customer Name" />
                                <asp:BoundField DataField="Coordinator_Contact_Number" HeaderText="Customer No." />
                                <asp:TemplateField HeaderText="Update Status">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lblstatus" runat="server" class="fa fa-edit fa-2x" CommandArgument='<%#Eval("ExcelRawID") %>'
                                            CommandName="UpdateStatus"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div runat="server" id="uppan" visible="false">
                        <div class="clearfix" style="margin-bottom: 10px;"></div>
                        <h3>Update Fileds.</h3>
                        <div>
                            <div class="form-group row">
                                <label class="col-sm-3 form-control-label">
                                    Customer  Name
                                </label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtname" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="col-sm-3 form-control-label">
                                    Customer Email
                                </label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtemail" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="col-sm-3 form-control-label">
                                    Customer Mobile No
                                </label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtmobile" runat="server" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <asp:LinkButton ID="btnupdateprescheduledtimedate" runat="server" class="btn btn-primary pull-right btn-sm"
                                    Text="Update Fields" OnClick="btnupdateprescheduledtimedate_Click"></asp:LinkButton>
                            </div>
                        </div>
                        <div class="clearfix" style="margin-bottom: 10px;"></div>
                    </div>
                </div>
                 <div class="clearfix" style="margin-bottom: 10px;"></div>
            </div>
            <div class="clearfix" style="margin-bottom: 10px;"></div>
        </div>
    </div>
</asp:Content>
