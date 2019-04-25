﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExcelMonthReport.aspx.cs"
    Inherits="ExcelMonthReport" MasterPageFile="~/MasterPage2.master" %>

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
<asp:Content ID="contentmain" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="loader">
    </div>
    <div class="right_col" role="main">
        <div class="">
            <div class="page-title">
                <div class="title_left">
                    <h3>
                        New Company</h3>
                </div>
            </div>
            <asp:UpdatePanel ID="updatepanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
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
                            <div class="col-sm-4">
                                <div class="form-group row">
                                    <label class="col-sm-3 form-control-label">
                                        Date Start</label>
                                    <div class="col-sm-9">
                                        <asp:TextBox runat="server" ID="txtdatestart" class="form-control" type="date" required=""
                                            TabIndex="0" placeholder="Start Date"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group row">
                                    <label class="col-sm-3 form-control-label">
                                        Date End</label>
                                    <div class="col-sm-9">
                                        <asp:TextBox runat="server" ID="txtdateend" class="form-control" type="date" required=""
                                            TabIndex="1" placeholder="End Date"></asp:TextBox>
                                        <span class="help-block-none"></span>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <div class="form-group row">
                                    <label class="col-sm-3 form-control-label">
                                        Category</label>
                                    <div class="col-sm-9">
                                        <asp:DropDownList ID="ddlproduct" runat="server" class="form-control" TabIndex="2">
                                            <asp:ListItem>All</asp:ListItem>
                                            <asp:ListItem>Leased Line</asp:ListItem>
                                            <asp:ListItem>MO</asp:ListItem>
                                            <asp:ListItem>PRI-Fixed Line</asp:ListItem>
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
                                    class="btn btn-default pull-right" Text="Clear" OnClick="btnclear_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="clearfix pb10">
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnsearch" />
                    <asp:AsyncPostBackTrigger ControlID="btnclear" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="updatepanel1">
                <ProgressTemplate>
                    <div class="overlay">
                        <div style="z-index: 999; margin-left: 650px; margin-top: 300px; opacity: 0.3; -moz-opacity: 0.3;">
                            <img alt="" src="img/loader.gif" />
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
    </div>
</asp:Content>