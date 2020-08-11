<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExcelMonthReport.aspx.cs" Inherits="ExcelMonthReport"
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
                                    <asp:Label ID="lblPageName" runat="server" Text="Excel Monthly Report"></asp:Label>
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
                        <div class="form-horizontal">
                            <div class="col-sm-4">
                                <div class="form-group row">
                                    <label class="form-control-label">
                                        Date Start</label>
                                    <div class="">
                                        <asp:TextBox runat="server" ID="txtdatestart" class="form-control" type="date" required=""
                                            TabIndex="0" placeholder="Start Date"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group row">
                                    <label class="form-control-label">
                                        Date End</label>
                                    <div class="">
                                        <asp:TextBox runat="server" ID="txtdateend" class="form-control" type="date" required=""
                                            TabIndex="1" placeholder="End Date"></asp:TextBox>
                                        <span class="help-block-none"></span>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group row">
                                    <label class=" form-control-label">
                                        Category</label>
                                    <div class="">
                                        <asp:DropDownList ID="ddlproduct" runat="server" class="form-control" TabIndex="2">
                                            <asp:ListItem Selected="True" Value="1">Leased Line</asp:ListItem>
                                            <asp:ListItem Value="2">PRI-Fixed Line</asp:ListItem>
                                            <asp:ListItem Value="3">MO</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="clearfix pb10">
                            </div>
                            <div class="col-sm-12">
                                <asp:Button ID="btnsearch" runat="server" class="btn btn-primary pull-right" Text="Submit"
                                    OnClick="btnsearch_Click" />
                                <asp:Button ID="btnclear" runat="server" UseSubmitBehavior="false" CausesValidation="false"
                                    class="btn btn-default pull-right" Style="mrgin-right:10px;" Text="Clear" OnClick="btnclear_Click" />
                            </div>
                        </div>
                        <div class="clearfix mt10">
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnsearch" />
                <asp:AsyncPostBackTrigger ControlID="btnclear" />
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
