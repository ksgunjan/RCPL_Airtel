<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmAddProductType.aspx.cs" Inherits="frmAddProductType" MasterPageFile="~/MasterPage.master" %>

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
                                    <asp:Label ID="lblPageName" runat="server" Text="Add/View Product Type"></asp:Label>
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
                        <asp:HiddenField ID="hfprodtype" runat="server" />
                        <div class="form-group row">
                            <label class="col-sm-3 form-control-label">
                                ProductType Name</label>
                            <div class="col-sm-9">
                                <asp:TextBox runat="server" ID="txtprodtypename" class="form-control" required=""
                                    placeholder="ProductType Name"></asp:TextBox>
                            </div>
                        </div>
                        <div class="Clearfix pb5">
                        </div>
                        <div class="form-group row">

                            <asp:Button ID="btnsub" runat="server" class="btn btn-primary pull-right" Text="Submit"
                                OnClick="btnsub_Click" />
                            <asp:Button ID="btnclear" runat="server" UseSubmitBehavior="false" CausesValidation="false"
                                class="btn btn-default ml-2 pull-right" Text="Clear" OnClick="btnclear_Click" />

                        </div>
                        <div class="clearfix" style="margin-bottom: 10px;"></div>
                        <asp:GridView ID="gvcomp" runat="server" AutoGenerateColumns="false" class="table table-hover"
                            OnRowCommand="gvcomp_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="Sr.No">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ProductType Name">
                                    <ItemTemplate>
                                         <asp:Label ID="lblprodtype" runat="server" CssClass="m-lg-r" Text='<%#Eval("ProductTypeName") %>'></asp:Label>
                                    
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Edit">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lblupdate" runat="server" CommandArgument='<%#Eval("ProductTypeID") %>'
                                            CommandName="up" class="fa fa-edit" OnClientClick="return confirm('Click ok for edit this record?');"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Active/Deactive">
                                    <ItemTemplate>
                                        <asp:Label ID="lbisacti" runat="server" CssClass="m-lg-r" Text='<%#Eval("Text") %>'></asp:Label>
                                        <asp:LinkButton ID="lblactive" runat="server" OnClientClick="return confirm('click ok for modify this record?');"
                                            class='<%#Eval("button") %>' CommandArgument='<%#Eval("ProductTypeID") %>' CommandName="active"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <div class="clearfix" style="margin-bottom: 10px;"></div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
