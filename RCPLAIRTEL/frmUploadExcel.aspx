<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmUploadExcel.aspx.cs" Inherits="frmUploadExcel" MasterPageFile="~/MasterPage.master" %>

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
                                    <asp:Label ID="lblPageName" runat="server" Text="Upload Excel"></asp:Label>
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
                            <div class="col-sm-3">
                                <label class="col-sm-12 form-control-label">
                                    Select Company</label>
                                <div class="col-sm-12">
                                    <asp:DropDownList ID="ddlcomp" runat="server" class="form-control focus">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-3">
                                <label class="col-sm-12 form-control-label">
                                    Select Product</label>
                                <div class="col-sm-12">
                                    <asp:DropDownList ID="ddlproduct" runat="server" class="form-control focus">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-3">
                                <label class="col-sm-12 form-control-label">
                                    Select Product Type</label>
                                <div class="col-sm-12">
                                    <asp:DropDownList ID="ddlproducttype" runat="server" class="form-control focus">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-3">
                                <label class="col-sm-12 form-control-label">
                                    Select Excel File</label>
                                <div class="col-sm-12">
                                    <asp:FileUpload runat="server" class="form-control" ID="fuexcel" />
                                </div>
                            </div>
                        </div>
                        <div class="clearfix mt10"></div>
                        <div runat="server" id="divchecks">
                            <b>Please verify and check all manadatory field are checked correctly.Check contact no or email in correct format do not fill with (-,NA,or in Email contactno or in conatct no email)</b>
                            <div class="clearfix mt10">
                            </div>
                            <asp:CheckBoxList ID="chklist" runat="server" RepeatColumns="5" RepeatDirection="Vertical" RepeatLayout="Table">
                                <asp:ListItem>Allocated Date</asp:ListItem>
                                <asp:ListItem style="margin-left: 5px;">Cust Name</asp:ListItem>
                                <asp:ListItem style="margin-left: 5px;">Cust Email</asp:ListItem>
                                <asp:ListItem style="margin-left: 5px;">Compliance Date</asp:ListItem>
                                <asp:ListItem style="margin-left: 5px;">Fe Name</asp:ListItem>
                                <asp:ListItem style="margin-left: 5px;">Eligible/Not Eligible</asp:ListItem>
                                <asp:ListItem style="margin-left: 5px;">Kam Contact No</asp:ListItem>
                                <asp:ListItem style="margin-left: 5px;">In Excel Field</asp:ListItem>
                                <asp:ListItem style="margin-left: 5px;">Sam Email</asp:ListItem>
                                <asp:ListItem style="margin-left: 5px;">TL Email</asp:ListItem>
                            </asp:CheckBoxList>
                        </div>
                        <div class="clearfix mt10"></div>
                        <div>
                            <b runat="server" id="lblRowCount"></b>
                        </div>
                        <div class="clearfix mt10"></div>
                        <div class="form-group row">
                            <label class="col-sm-9 form-control-label">
                                <h4>
                                    <asp:Label ID="lblpathname" runat="server" class="label label-info"></asp:Label>
                                </h4>
                            </label>
                            <div class="col-sm-3">
                                <asp:Button ID="btnexcel" runat="server" class="btn btn-primary btn-sm btn-block"
                                    Text="Upload Excel Sheet File" OnClick="btnexcel_Click" />
                            </div>
                        </div>
                        <div class="clearfix mt10"></div>
                        <b runat="server" id="msgpopup"></b>
                        <div class="clearfix mt10"></div>
                    </div>
                </div>
                </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnexcel" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
