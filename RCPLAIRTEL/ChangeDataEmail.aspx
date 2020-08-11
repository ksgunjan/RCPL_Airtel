<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangeDataEmail.aspx.cs" Inherits="ChangeDataEmail"
    MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="conhead" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="contentmain" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="content oem-content">
                <div class="sideBg">
                    <div class="row">
                        <div class="col-md-12 padding_0">
                            <ul class="breadcrumb">
                                <li>
                                    <asp:Label ID="lblPageName" runat="server" Text="Update Change Mai"></asp:Label>
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
                        <div class="col-sm-4">
                            <asp:RadioButtonList ID="rbCatType" runat="server" CssClass="btn-block" RepeatDirection="Horizontal"
                                RepeatLayout="Flow"
                                RepeatColumns="5">
                            </asp:RadioButtonList>
                        </div>
                        <div class="col-sm-4">
                            <asp:TextBox ID="txtsearch" runat="server" CssClass="form-control btn-block" placeholder="Please enter party name or logical/billed/sub del no">
                            </asp:TextBox>
                        </div>
                        <div class="col-sm-4">
                            <asp:Button ID="btnsearch" runat="server" CssClass="btn btn-success btn-block" Text="Search" OnClick="btnsearch_Click" />
                        </div>
                        <div class="clearfix mt10"></div>
                        <asp:GridView ID="gvdetail" runat="server" AutoGenerateColumns="false" class="commonAjaxTbl dataTable master-company-table ViewProductTable table 
                                        display responsive no-wrap table-hover manage-user Grid table-responsive"
                            OnRowCommand="gvdetail_RowCommand" OnRowCreated="gvdetail_RowCreated">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lblview" runat="server" CssClass="fa fa-edit" CommandArgument='<%#Eval("ExcelRawID") %>'
                                            CommandName="Upd"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Party Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblpartyname" runat="server" Text='<%#Eval("Party_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Allocated Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblalocdatr" runat="server" Text='<%#Eval("Allocated_Date") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprodtype" runat="server" Text='<%#Eval("Product_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Prod_Type_Name" HeaderText="Product Type Name" />
                                <asp:BoundField DataField="Fe_Name" HeaderText="FE Name" />
                                <asp:BoundField DataField="Status_By_FE" HeaderText="Status" />
                                <asp:BoundField DataField="Sent_Cust_Mail" HeaderText="Send Cust Mail" />
                            </Columns>
                        </asp:GridView>

                    </div>
                </div>
            </div>
            <div id="myModal" aria-hidden="true" aria-labelledby="exampleModalLabel" class="modal fade text-left"
                role="dialog" tabindex="-1" data-backdrop="static" data-keyboard="false">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button aria-label="Close" class="close" data-dismiss="modal" type="button">
                                <span aria-hidden="true">×</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <h3>Check or Update Status</h3>
                            <div class="clearfix mt10">
                            </div>
                            <div>
                                <div class="form-group">
                                    <p>
                                        B2B Email
                                    </p>
                                    <asp:TextBox ID="txtb2bemail" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <p>
                                        B2B Head Email
                                    </p>
                                    <asp:TextBox ID="txtb2bheademail" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <p>
                                        Billing Email Id
                                    </p>
                                    <asp:TextBox ID="txtbillingemail" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <p>
                                        Coordinator Contact Email
                                    </p>
                                    <asp:TextBox ID="txtcoordinatoremail" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <p>
                                        Cust Email
                                    </p>
                                    <asp:TextBox ID="txtcustemail" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <p>
                                        KAM Email
                                    </p>
                                    <asp:TextBox ID="txtkamemail" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <p>
                                        RM Email
                                    </p>
                                    <asp:TextBox ID="txtrmemail" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <p>
                                        SAM Email
                                    </p>
                                    <asp:TextBox ID="txtsamemail" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <p>
                                        SAM TL EMAIL
                                    </p>
                                    <asp:TextBox ID="txtsamtlemail" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <p>
                                        TL Email
                                    </p>
                                    <asp:TextBox ID="txttlemail" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <p>
                                        VH Email
                                    </p>
                                    <asp:TextBox ID="txtvhemail" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="clearfix mt10"></div>
                                <asp:Button ID="btnupdate" runat="server" CssClass="btn btn-success pull-right" Text="Update Mails" OnClick="btnupdate_Click" />
                                <div class="clearfix mt10"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

