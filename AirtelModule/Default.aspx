<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>Login</title>
    <meta name="description" content="">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="robots" content="all,follow">
    <link href="vendors/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="vendors/bootstrap/dist/css/fontastic.css">
    <link href="vendors/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Poppins:300,400,700">
    <link rel="stylesheet" href="vendors/bootstrap/dist/css/style.default.css">
    <link href="vendors/bootstrap/dist/css/aaryan.css" rel="stylesheet" type="text/css" />
    <link rel="shortcut icon" href="favicon.png">
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
        <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script><![endif]-->
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
            margin-left: -520px;
            margin-top: 0px;
        }
    </style>
    <!----------------------------End----------------------------------->
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="Sc" runat="server">
    </asp:ScriptManager>
    <div id="loader">
    </div>
    <div class="page login-page">
        <div class="container d-flex align-items-center">
            <div class="form-holder has-shadow">
                <div class="row">
                    <!-- Logo & Information Panel-->
                    <div class="col-lg-4">
                        <div class="info d-flex align-items-center">
                            <div class="content">
                                <div class="logo text-center">
                                    <div class="">
                                        <div class="clearfix" style="margin: 50px;">
                                        </div>
                                        <img src="img/reliable.jpg" style="text-align: center; margin-left: 30px;" />
                                    </div>
                                    <h1>
                                        Login</h1>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- Form Panel    -->
                    <div class="col-lg-8 bg-white">
                        <div class="form d-flex align-items-center">
                            <div class="content">
                                <div id="divErr" runat="server">
                                </div>
                                <div class="clearfix" style="margin: 50px;">
                                </div>
                                <asp:UpdatePanel ID="updatepanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="form-group">
                                            <asp:HiddenField ID="hdfMobile" runat="server" />
                                            <asp:HiddenField ID="hdfEmail" runat="server" />
                                            <label for="login-username" class="label-material">
                                                Email</label>
                                            <asp:TextBox ID="txtusername" runat="server" required="" Placeholder="Enter valid email"
                                                ToolTip="Plase enter valid registerd email-id (@ex - test@test.com)" class="input-material"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label for="login-password" class="label-material">
                                                Password</label>
                                            <asp:TextBox ID="txtpass" runat="server" MaxLength="15" name="loginPassword" placeholder="Enter password"
                                                TextMode="Password" required="" class="input-material"></asp:TextBox>
                                        </div>
                                        <asp:Button ID="btnlog" runat="server" class="btn btn-primary" Text="Login" OnClick="btnlog_Click" />
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnlog" />
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
            <%--<div class="copyrights text-center">
                <p>
                    Design by <a href="http://gipinfosystems.com" class="external">GipInfoSystems Pvt.Ltd</a></p>
            </div>--%>
        </div>
    </div>
    <!-- Javascript files-->
    <script src="https://code.jquery.com/jquery-3.2.1.min.js"></script>
    <script src="vendors/bootstrap/dist/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="build/js/front.js" type="text/javascript"></script>
    <script src="build/js/popper.min.js" type="text/javascript"></script>
    <script src="build/js/jquery.cookie.js" type="text/javascript"></script>
    <script src="build/js/jquery.validate.min.js" type="text/javascript"></script>
    </form>
</body>
</html>
