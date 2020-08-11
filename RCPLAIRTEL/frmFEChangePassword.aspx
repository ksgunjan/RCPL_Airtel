<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmFEChangePassword.aspx.cs" Inherits="frmFEChangePassword" MasterPageFile="~/MasterPage.master" %>

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
                                    <asp:Label ID="lblPageName" runat="server" Text="Change Password"></asp:Label>
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

                        <asp:HiddenField ID="hfempid" runat="server" />
                        <div class="form-group row">
                            <label class="col-sm-3 form-control-label">
                                Enter valid registerd email id.</label>
                            <div class="col-sm-9">
                                <asp:TextBox runat="server" ID="txtemail" class="form-control" required="" placeholder="Registerd Email ID"></asp:TextBox>
                                <span class="help-block-none"></span>
                            </div>
                        </div>
                        <div class="Clearfix mt10">
                        </div>
                        <div class="form-group row">
                            <label class="col-sm-3 form-control-label">
                                Enter your old password</label>
                            <div class="col-sm-9">
                                <asp:TextBox runat="server" ID="txtoldpassword" class="form-control" required="" placeholder="Old Password"></asp:TextBox>
                            </div>
                        </div>
                        <div class="clearfix mt10">
                        </div>
                        <div class="form-group row">
                            <label class="col-sm-3 form-control-label">
                                Enter new password</label>
                            <div class="col-sm-9">
                                <asp:TextBox runat="server" ID="txtnewpassword" class="form-control" required=""
                                    placeholder="New Password"></asp:TextBox>
                            </div>
                        </div>
                        <div class="clearfix mt10">
                        </div>
                        <div class="col-sm-12">
                            <asp:Button ID="btnsub" runat="server" class="btn btn-primary pull-right"
                                Text="Submit" OnClick="btnsub_Click" />
                            <asp:Button ID="btnclear" runat="server" UseSubmitBehavior="false" CausesValidation="false"
                                class="btn btn-default pull-right" Text="Clear" OnClick="btnclear_Click" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnsub" />
                <asp:AsyncPostBackTrigger ControlID="btnclear" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
