<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BulkEmailSMS.aspx.cs" Inherits="BulkEmailSMS"
    MasterPageFile="MasterPage2.master" %>

<%@ Register Assembly="Winthusiasm.HtmlEditor" Namespace="Winthusiasm.HtmlEditor"
    TagPrefix="cc1" %>
<asp:Content ID="conhead" runat="server" ContentPlaceHolderID="head">
    <!----------------Bootstrp js-------------------->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <!--------------------------Page Loader------------------->
    <style>
        #loader
        {
            position: fixed;
            left: 0px;
            top: 0px;
            width: 100%;
            height: 100%;
            z-index: 9999;
            background: url('img/loader.gif') 50% 50% no-repeat rgb(249,249,249);
        }
    </style>
    <script type="text/javascript">
        function showPopup() {
            $('#myModal').modal('show');
        }
    </script>
    <script src="http://code.jquery.com/jquery-1.8.2.js"></script>
    <script type="text/javascript">
        $(window).load(function () {
            $("#loader").fadeOut(1000);
        });
    </script>
    <!---------------------End--------------------------------->
    <!---------------------------------------------------------Update panel progress---------------------->
    <script type="text/javascript">
        function showProgress() {
            var updateProgress = $get("<%= UpdateProgress.ClientID %>");
            updateProgress.style.display = "block";
        }
    </script>
    <style type="text/css">
        .overlay
        {
            position: fixed;
            z-index: 999999;
            height: 100%;
            width: 100%;
            top: 0;
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.2;
            -moz-opacity: 0.2;
            margin-left: -280px;
            margin-top: 0px;
        }
    </style>
    <!----------------------------End----------------------------------->
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="loader">
    </div>
    <asp:UpdatePanel ID="updatepanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="right_col" role="main">
                <div class="">
                    <div class="page-title">
                        <div class="title_left">
                            <h3>
                                Bulk Email</h3>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="form-horizontal">
                            <div class="clearfix pb10">
                            </div>
                            <div class="form-group row">
                                <div runat="server" id="diverror" class="col-sm-12">
                                </div>
                            </div>
                            <div class="clearfix pb10">
                            </div>
                            <div runat="server" id="divsendsmsoremail">
                                <div class="text-center hidden">
                                    <asp:RadioButtonList ID="rbsendtype" runat="server" RepeatColumns="3" class="radio-inline"
                                        RepeatDirection="Horizontal" RepeatLayout="Flow" AutoPostBack="true" OnSelectedIndexChanged="rbsendtype_SelectedIndexChanged">
                                        <asp:ListItem Value="email" style="margin-left: 25px;">Send Email</asp:ListItem>
                                        <asp:ListItem Value="sms" style="margin-left: 25px;">Send SMS</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                                <div class="clearfix pb10">
                                </div>
                                <div>
                                    <asp:LinkButton ID="button" runat="server" class="btn btn-primary pull-right mr10"
                                        Text="Send Email/SMS" OnClick="btnsendsmsoremail_Click"></asp:LinkButton>
                                </div>
                                <div class="clearfix pb10">
                                </div>
                                <div class="table table-responsive">
                                    <asp:GridView ID="gvdetail" runat="server" class="table table-hover" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Check All">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkHeader" runat="server" AutoPostBack="True" OnCheckedChanged="chkHeader_CheckedChanged" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkRow" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sr.No">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Email id">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblemail" runat="server" Text='<%#Eval("EmailId1") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Mobile No" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblmob" runat="server" Text='<%#Eval("MobileNo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:TemplateField HeaderText="Contact Person">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcontactperson" runat="server" Text='<%#Eval("ContactPerson") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                        </Columns>
                                    </asp:GridView>
                                    <div class="clearfix pb10">
                                    </div>
                                    <div>
                                        <asp:LinkButton ID="btnsendsmsoremail" runat="server" class="btn btn-primary pull-right mr10"
                                            Text="Send Email/SMS" OnClick="btnsendsmsoremail_Click"></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="clearfix pb10">
                            </div>
                        </div>
                    </div>
                    <div class="clearfix pb10">
                    </div>
                </div>
            </div>
            <div id="myModal" class="modal fade">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                &times;</button>
                            <h3 class="modal-title" id="myModalLabel">
                                Bulk mail Panel</h3>
                        </div>
                        <div class="modal-body">
                            <div class="col-sm-12">
                                <div class="col-sm-12" id="subject" runat="server">
                                    <h4>
                                        Subject :
                                    </h4>
                                    <div class="form-group">
                                        <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control"></asp:TextBox></div>
                                </div>
                                <div class="col-sm-12 box-danger">
                                    <h4>
                                        Message :
                                    </h4>
                                    <div class="form-group">
                                        <asp:TextBox ID="txtsms" runat="server" CssClass="form-control" Placeholder="Enter Your Sms/Message"
                                            MaxLength="160" Visible="false" Height="50"></asp:TextBox>
                                        <h6 class="pull-right" id="count_message">
                                        </h6>
                                        <cc1:HtmlEditor ID="txtmsg" runat="server" CssClass="editor"></cc1:HtmlEditor>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                </div>
                                <div id="attach" runat="server">
                                    <h4>
                                        Attachment :
                                    </h4>
                                    <div class="form-group">
                                        <asp:FileUpload ID="fufile" runat="server" CssClass="form-control" />
                                    </div>
                                    <div class="clearfix navbar-btn">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="clearfix">
                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton ID="btnSend" runat="server" Text="Send Email / SMS" OnClick="btnSend_Click"
                                CssClass="btn btn-primary btn-flat"></asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSend" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="updatepanel1">
        <ProgressTemplate>
            <div class="overlay">
                <div style="z-index: 999; margin-left: 650px; margin-top: 300px; opacity: 0.3; -moz-opacity: 0.3;">
                    <img alt="" src="~/images/loader.gif" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
