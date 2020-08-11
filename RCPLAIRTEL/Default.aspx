<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Login</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="shortcut icon" href="~/assets/images/favicon.ico">
    <link href="~/assets/css/bootstrap.css" rel="stylesheet">
    <link href="~/assets/css/all.min.css" rel="stylesheet" type="text/css">
    <link href="~/assets/css/style.css" rel="stylesheet" type="text/css">
    <link href="~/assets/css/custom.css" rel="stylesheet" type="text/css">
    <link href="~/assets/css/responsive.css" rel="stylesheet" type="text/css">
    <style>
        .swal2-container.swal2-center.swal2-shown {
            z-index: 22222;
        }

        .modal {
            margin-top: 70px !important;
        }
    </style>
    <style type="text/css">
        .form-group {
            margin: 8px 0px;
        }

        .hhead {
            background-color: #f5f5f5;
            color: #000;
            border: 0px;
            margin-top: 8px !important;
            margin-bottom: 8px !important;
            padding: 10px 10px;
            border-radius: 0px;
            font: normal 14px/18px Arial, Helvetica, sans-serif;
        }
    </style>
    <script src="assets/js/jquery-3.4.1.js"></script>
    <script>
        function ErrorMssgPopup(data) {
            $("body").addClass('CaptchaError');
            $("#alertPopup").show();
            $("#alertPopup .alertMsg").append(data);
            return false;
        }
        //Hide Alert Pop up
        $('.close_alert').on('click', function () {
            $("body").css('overflow', 'visible');
            $('.alert-overlay-error').hide();
        });

    </script>
</head>
<body>
    <form id="form1" runat="server" method="post" asp-antiforgery="false">
        <asp:ScriptManager runat="server" ID="sc"></asp:ScriptManager>
        <nav class="navbar" role="navigation">
            <div class="navbar-header">
                <a class="navbar-brand" href="#">
                    <span class="main-logo" title="Department of Defense Product">RCPL</span>
                </a>
            </div>
        </nav>
        <div class="container" style="margin-top: 100px;">
            <div class="row">
                <div class="loginBg clearfix">
                    <h3>Login</h3>
                    <asp:Panel ID="Panel1" runat="server" DefaultButton="btnLogin">
                        <label for="uname" class=" tetLable">
                            Email
                        </label>
                        <asp:TextBox runat="server" ID="txtUserName" class="form-control" TabIndex="1" focus="true" autocomplete="off" type="email" placeholder="Email" ToolTip="Please enter valid registerd email id." required="" autofocus=""></asp:TextBox>
                        <label for="psw" class=" tetLable">
                            Password
                        </label>
                        <span class="passbox">
                            <asp:TextBox runat="server" ID="txtPwd" TabIndex="2" name="txtPwd" class="form-control passField" autocomplete="off" ToolTip="Please enter valid password (case sensitive)." placeholder="Password" type="password" required=""></asp:TextBox>
                            <span toggle="#password-field" class="fa fa-fw fa-eye field-icon toggle-password"></span>
                        </span>
                        <div class="clearfix">
                            <br />
                        </div>
                        <asp:LinkButton ID="btnLogin" runat="server" TabIndex="4" CssClass="btn btn-info" ToolTip="After validate your username or password we will redirect to your dashboard."
                            OnClick="btnLogin_Click" Text="Login"></asp:LinkButton>
                        <div class="clearfix" style="margin-top: 15px;">
                        </div>
                        <p>Note:- For better site experiance please use Google Chrome,Mozila FireFox,Internet Edge,Safari.</p>
                    </asp:Panel>
                </div>
            </div>
        </div>
        <div id="alertPopup" class="alert-overlay alert-overlay-error" style="display: none">
            <div class="alert-box">
                <div class="box">
                    <div class="error-checkmark">
                        <i class="fa fa-exclamation-triangle" aria-hidden="true"></i>
                    </div>
                    <div class="alert alertMsg">
                    </div>
                    <button class="btn btn-success close_alert">Close</button>
                </div>
            </div>
        </div>
    </form>
    <script src="assets/js/jquery-3.4.1.js"></script>
    <script src="assets/js/bootstrap.min.js"></script>
    <script src="assets/js/custom.js"></script>
</body>
</html>
