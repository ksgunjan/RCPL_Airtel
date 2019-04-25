<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmUploadExcel.aspx.cs" Inherits="frmUploadExcel"
    MasterPageFile="~/MasterPage2.master" %>

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
                        Upload Excel</h3>
                </div>
            </div>
            <div class="card">
                <asp:UpdatePanel ID="updatepanel1" runat="server" UpdateMode="Conditional">
                    <contenttemplate>
                        <div class="card-body">
                            <div class="form-horizontal">
                                <div class="form-group row">
                                    <div class="clearfix pb10">
                                    </div>
                                    <div runat="server" id="diverror" class="col-sm-12">
                                    </div>
                                    <div class="clearfix pb10">
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-sm-4">
                                        <label class="col-sm-12 form-control-label">
                                            Select Company</label>
                                        <div class="col-sm-12">
                                            <asp:DropDownList ID="ddlcomp" runat="server" class="form-control focus" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlcomp_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <label class="col-sm-12 form-control-label">
                                            Select Product</label>
                                        <div class="col-sm-12">
                                            <asp:DropDownList ID="ddlproduct" runat="server" class="form-control focus">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <label class="col-sm-12 form-control-label">
                                            Select Product Type</label>
                                        <div class="col-sm-12">
                                            <asp:DropDownList ID="ddlproducttype" runat="server" class="form-control focus">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="Clearfix pb5">
                                </div>
                                <div class="form-group row">
                                    <label class="col-sm-3 form-control-label">
                                        Select Excel File</label>
                                    <div class="col-sm-9">
                                        <asp:FileUpload runat="server" class="form-control" ID="fuexcel" />
                                    </div>
                                </div>
                                <div class="Clearfix pb5">
                                </div>
                                <div>
                                    <b runat="server" id="lblRowCount"></b>
                                </div>
                                <div class="clearfix pb-1">
                                </div>
                                <div runat="server" id="divchecks">
                                    <h4>
                                        Please verify and check all manadatory field are checked correctly</h4>
                                    <div class="clearfix pb0">
                                    </div>
                                    <h4>
                                        Check Contact no or email in correct format do not fill with (-,NA,or in Email contactno
                                        or in conatct no email)</h4>
                                    <div class="clearfix" style="padding:10px;">
                                    </div>
                                    <asp:CheckBoxList ID="chklist" runat="server" class="checkbox" RepeatColumns="8"
                                        RepeatDirection="Vertical" RepeatLayout="Table">
                                        <asp:ListItem>Allocated Date</asp:ListItem>
                                        <asp:ListItem style="margin-left: 25px;">Cust Name</asp:ListItem>
                                        <asp:ListItem style="margin-left: 25px;">Cust Email</asp:ListItem>
                                        <asp:ListItem style="margin-left: 25px;">Compliance Date</asp:ListItem>
                                        <asp:ListItem style="margin-left: 25px;">Fe Name</asp:ListItem>
                                        <asp:ListItem style="margin-left: 25px;">Eligible/Not Eligible</asp:ListItem>
                                        <asp:ListItem style="margin-left: 25px;">Kam Contact No</asp:ListItem>
                                        <asp:ListItem style="margin-left: 25px;">In Excel Field</asp:ListItem>
                                        <asp:ListItem style="margin-left: 25px;">Sam Email</asp:ListItem>
                                        <asp:ListItem style="margin-left: 25px;">TL Email</asp:ListItem>
                                    </asp:CheckBoxList>
                                  <div class="clearfix" style="padding:10px;">
                                    </div>
                                </div>
                                <div class="clearfix pb-2">
                                </div>
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
                            </div>
                        </div>
                    </contenttemplate>
                    <triggers>
                        <asp:PostBackTrigger ControlID="btnexcel" />
                    </triggers>
                </asp:UpdatePanel>
                <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="updatepanel1">
                    <progresstemplate>
                        <div class="overlay">
                            <div style="z-index: 999; margin-left: 650px; margin-top: 300px; opacity: 0.3; -moz-opacity: 0.3;">
                                <img alt="" src="img/loader.gif" />
                            </div>
                        </div>
                    </progresstemplate>
                </asp:UpdateProgress>
            </div>
        </div>
    </div>
</asp:Content>
