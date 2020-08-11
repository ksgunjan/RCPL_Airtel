<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmAddProduct.aspx.cs" Inherits="frmAddProduct" MasterPageFile="~/MasterPage.master" %>

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
                                    <asp:Label ID="lblPageName" runat="server" Text="Add/View Product"></asp:Label>
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
                        <asp:HiddenField ID="hfproduct" runat="server" />
                        <div class="form-group row">
                            <label class="col-sm-3 form-control-label">
                                Company Name</label>
                            <div class="col-sm-9">
                                <asp:DropDownList runat="server" ID="ddlcomp" class="form-control" required="">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="Clearfix pb5">
                        </div>
                        <div class="form-group row">
                            <label class="col-sm-3 form-control-label">
                                Product Name</label>
                            <div class="col-sm-9">
                                <asp:TextBox runat="server" ID="txtproductname" class="form-control" required=""
                                    placeholder="Product Name"></asp:TextBox>
                                <span class="help-block-none"></span>
                            </div>
                        </div>
                        <div class="Clearfix pb5">
                        </div>
                        <div class="form-group row">
                            <div class="col-sm-9">
                            </div>
                            <div class="col-sm-3">
                                <asp:Button ID="btnsub" runat="server" class="btn btn-primary pull-right" Text="Submit"
                                    OnClick="btnsub_Click" />
                                <asp:Button ID="btnclear" runat="server" UseSubmitBehavior="false" CausesValidation="false"
                                    class="btn btn-default ml-2 pull-right" Text="Clear" OnClick="btnclear_Click" />
                            </div>
                        </div>
                        <div class="Clearfix pb5">
                        </div>
                        <asp:GridView ID="gvcomp" runat="server" AutoGenerateColumns="false" class="table table-hover"
                            OnRowCommand="gvcomp_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="Sr.No">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Company Name">
                                    <ItemTemplate>
                                        <%#Eval("CompName") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprodname" runat="server" Text='<%#Eval("ProductName") %>'></asp:Label>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Edit">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lblupdate" runat="server" CommandArgument='<%#Eval("ProductId") %>'
                                            CommandName="up" class="fa fa-edit" OnClientClick="return confirm('Click ok for edit this record?');"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Active/Deactive">
                                    <ItemTemplate>
                                        <asp:Label ID="lbisacti" runat="server" CssClass="m-lg-r" Text='<%#Eval("Text") %>'></asp:Label>
                                        <asp:LinkButton ID="lblactive" runat="server" OnClientClick="return confirm('click ok for modify this record?');"
                                            class='<%#Eval("button") %>' CommandArgument='<%#Eval("ProductId") %>' CommandName="active"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <div class="Clearfix pb5">
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

