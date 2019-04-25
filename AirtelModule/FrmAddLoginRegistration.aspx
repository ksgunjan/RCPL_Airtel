<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmAddLoginRegistration.aspx.cs"
    Inherits="FrmAddLoginRegistration" MasterPageFile="~/MasterPage2.master" %>

<asp:Content ID="conhead" runat="server" ContentPlaceHolderID="head">
    <!----------------Bootstrp js-------------------->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <!------------------------end-------------------------->
    <script src="http://code.jquery.com/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function showpreview(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#imgpreview').css('visibility', 'visible');
                    $('#imgpreview').attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
            }
        }
    </script>
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
<asp:Content ID="conplace" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="loader">
    </div>
    <div class="right_col" role="main">
        <div class="">
            <div class="page-title">
                <div class="title_left">
                    <h3>
                        New Registration</h3>
                </div>
            </div>
            <!-- Page Header-->
            <div class="forms">
                <div class="container-fluid">
                    <div class="row">
                        <div class="card">
                            <asp:UpdatePanel ID="updatepanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="card-body">
                                        <div class="form-group">
                                            <div class="clearfix pb10">
                                            </div>
                                            <div id="divErr" runat="server">
                                            </div>
                                            <div class="clearfix pb10">
                                            </div>
                                            <div class="col-sm-6 col-md-6 col-lg-6">
                                                <asp:HiddenField ID="hfEmpId" runat="server" />
                                                <label class="form-control-label">
                                                    Select Category</label>
                                                <div class="select">
                                                    <asp:DropDownList ID="ddlcategory" runat="server" AutoPostBack="true" class="form-control"
                                                        OnSelectedIndexChanged="ddlcategory_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                                <label class="form-control-label">
                                                    Name</label>
                                                <asp:TextBox ID="txtname" runat="server" class="form-control" MaxLength="250" required=""
                                                    placeholder="Name"></asp:TextBox>
                                                <label class="form-control-label">
                                                    Email</label>
                                                <asp:TextBox ID="txtemail" runat="server" MaxLength="70" AutoPostBack="true" class="form-control"
                                                     placeholder="Email Address" OnTextChanged="txtemail_TextChanged"></asp:TextBox>
                                                <label class="form-control-label">
                                                    Password</label>
                                                <asp:TextBox ID="txtpassword" runat="server" MaxLength="15" class="form-control"
                                                    required="" placeholder="Password"></asp:TextBox>
                                                <label class="form-control-label">
                                                    Contact No</label>
                                                <asp:TextBox ID="txtmobno" runat="server" class="form-control" MaxLength="10" 
                                                    required="" placeholder="Mobile No/ Phone No"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-6 col-md-6 col-lg-6">
                                                <label class="form-control-label">
                                                    Address</label>
                                                <asp:TextBox ID="txtaddress" runat="server" class="form-control" Height="100px" TextMode="MultiLine"
                                                    MaxLength="350" placeholder="Address"></asp:TextBox>
                                                <div runat="server" id="divlocfe" visible="false">
                                                    <label class="form-control-label">
                                                        Location</label>
                                                    <asp:DropDownList ID="ddllocation" runat="server" class="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                                <label class="form-control-label">
                                                    Govt-IDNo</label>
                                                <asp:TextBox ID="txtgovtidno" runat="server" class="form-control" MaxLength="16"
                                                    placeholder="Govt. IDNo"></asp:TextBox>
                                                <label class="form-control-label">
                                                    Upload Photo</label>
                                                <asp:FileUpload ID="fupic" runat="server" class="form-control" onchange="showpreview(this);" />
                                                <asp:Label ID="lblpicdetail" runat="server" class="lable lable-info"></asp:Label>
                                                <img id="imgpreview" height="150" width="150" src="" style="border-width: 0px; visibility: hidden;" />
                                                <asp:Button ID="btnuploadimage" runat="server" class="btn btn-default pull-right mt10"
                                                    CausesValidation="false" UseSubmitBehavior="false" Text="Upload Profile Pic"
                                                    OnClick="btnuploadimage_Click" />
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <asp:Button ID="btnsub" runat="server" class="btn btn-primary pull-right" Text="Save New User"
                                                OnClick="btnsub_Click" />
                                            <asp:Button ID="btnclear" runat="server" class="btn btn-default pull-right" CausesValidation="false"
                                                UseSubmitBehavior="false" Text="Clear" OnClick="btnclear_Click" />
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnsub" />
                                    <asp:AsyncPostBackTrigger ControlID="btnclear" />
                                    <asp:PostBackTrigger ControlID="btnuploadimage" />
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
                </div>
            </div>
        </div>
    </div>
</asp:Content>
