<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BulkEmailSMS.aspx.cs" Inherits="BulkEmailSMS"
    MasterPageFile="~/MasterPage.master" %>

<%@ Register Assembly="Winthusiasm.HtmlEditor" Namespace="Winthusiasm.HtmlEditor"
    TagPrefix="cc1" %>
<asp:Content ID="conhead" runat="server" ContentPlaceHolderID="head">
    <!------------------------------------CheckGrid----------------------------------------------------------->
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $("[id*=chkHeader]").live("click", function () {
            var chkHeader = $(this);
            var grid = $(this).closest("table");
            $("input[type=checkbox]", grid).each(function () {
                if (chkHeader.is(":checked")) {
                    $(this).attr("checked", "checked");
                    $("td", $(this).closest("tr")).addClass("selected");
                } else {
                    $(this).removeAttr("checked");
                    $("td", $(this).closest("tr")).removeClass("selected");
                }
            });
        });
        $("[id*=chkRow]").live("click", function () {
            var grid = $(this).closest("table");
            var chkHeader = $("[id*=chkHeader]", grid);
            if (!$(this).is(":checked")) {
                $("td", $(this).closest("tr")).removeClass("selected");
                chkHeader.removeAttr("checked");
            } else {
                $("td", $(this).closest("tr")).addClass("selected");
                if ($("[id*=chkRow]", grid).length == $("[id*=chkRow]:checked", grid).length) {
                    chkHeader.attr("checked", "checked");
                }
            }
        });
    </script>
    <!------------------------------------------------End CheckListBox------------------------------------------------->
</asp:Content>
<asp:Content ID="contentmain" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="content oem-content">
        <asp:UpdatePanel runat="server" ID="updatepan" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="sideBg">
                    <div class="row">
                        <div class="col-md-12 padding_0">
                            <ul class="breadcrumb">
                                <li>
                                    <asp:Label ID="lblPageName" runat="server" Text="Bulk SMS or Email"></asp:Label>
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
                        <div class="text-center">
                            <asp:RadioButtonList ID="rblisttype" runat="server" RepeatColumns="2"
                                RepeatDirection="Horizontal" RepeatLayout="Flow" AutoPostBack="true" OnSelectedIndexChanged="rblisttype_SelectedIndexChanged">
                                <asp:ListItem Selected="True" Value="1">Leased Line/PRI</asp:ListItem>
                                <asp:ListItem Value="2" style="margin-left: 25px;">MO</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        <div class="clearfix mt10">
                        </div>
                        <div runat="server" id="divsendsmsoremail">
                            <div class="text-center">
                                <asp:RadioButtonList ID="rbsendtype" runat="server" RepeatColumns="3"
                                    RepeatDirection="Horizontal" RepeatLayout="Flow">
                                    <asp:ListItem Value="both" Selected="True">Both</asp:ListItem>
                                    <asp:ListItem Value="email" style="margin-left: 10px;">Send Email</asp:ListItem>
                                    <asp:ListItem Value="sms" style="margin-left: 10px;">Send SMS</asp:ListItem>
                                    <%-- <asp:ListItem Value="email" style="margin-left: 10px;">Send Email</asp:ListItem>
                                    <asp:ListItem Value="sms" style="margin-left: 10px;">Send SMS</asp:ListItem>--%>
                                </asp:RadioButtonList>
                            </div>
                            <div class="clearfix mt10">
                            </div>
                            <div>
                                <asp:Button ID="Button1" runat="server" class="btn btn-primary pull-right mr10"
                                    Text="Send Email/SMS" OnClick="btnsendsmsoremail_Click" />
                            </div>
                            <div class="clearfix mt10">
                            </div>
                            <asp:GridView ID="gvdetail" runat="server" class="commonAjaxTbl dataTable master-company-table ViewProductTable table 
                                        display responsive no-wrap table-hover manage-user Grid table-responsive"
                                AutoGenerateColumns="false" OnRowCreated="gvdetail_RowCreated">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkHeader" runat="server" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkRow" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Email">
                                        <ItemTemplate>
                                            <asp:Label ID="lblemail" runat="server" Text='<%#Eval("Email") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Mobile No">
                                        <ItemTemplate>
                                           <asp:Label ID="lblmob" runat="server" Text='<%#Eval("Mobile") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <div class="clearfix mt10">
                            </div>
                            <div>
                                <asp:Button ID="btnsendsmsoremail" runat="server" class="btn btn-primary pull-right mr10"
                                    Text="Send Email/SMS" OnClick="btnsendsmsoremail_Click" />
                            </div>
                        </div>
                        <div class="clearfix mt10">
                        </div>
                    </div>
                </div>
                <div id="myModal" class="modal fade">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                    &times;</button>
                                <h3 class="modal-title" id="myModalLabel">SMS/Mail Panel</h3>
                            </div>
                            <div class="modal-body">
                                <div class="col-sm-12">
                                    <div class="col-sm-12" id="subject" runat="server">
                                        <h4>Subject :
                                        </h4>
                                        <div class="form-group">
                                            <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 box box-danger">
                                        <h4>Message :
                                        </h4>
                                        <div class="form-group">
                                            <asp:TextBox ID="txtsms" runat="server" CssClass="form-control" Placeholder="Enter Your Sms/Message"
                                                MaxLength="160" Visible="false" Height="50"></asp:TextBox>
                                            <h6 class="pull-right" id="count_message"></h6>
                                            <cc1:HtmlEditor ID="txtmsg" runat="server" CssClass="editor" />
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                    <div id="attach" runat="server">
                                        <h4>Attachment :
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
        <asp:UpdateProgress ID="UpdateProgress6" runat="server" AssociatedUpdatePanelID="updatepan">
            <ProgressTemplate>
                <div class="overlay-progress">
                    <div class="custom-progress-bar blue stripes">
                        <span></span>
                        <p>Please wait we are processing your request</p>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
</asp:Content>

