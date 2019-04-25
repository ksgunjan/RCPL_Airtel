<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmviewUser.aspx.cs" Inherits="frmviewUser"
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
<asp:Content ID="coinner" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="loader">
    </div>
    <div class="right_col" role="main">
        <div class="">
            <div class="page-title">
                <div class="title_left">
                    <h3>
                        View User</h3>
                </div>
            </div>
            <div class="card">
                <asp:UpdatePanel ID="updatepanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="card-body">
                            <div class="form-horizontal">
                                <div class="pb10 clearfix">
                                </div>
                                <div runat="server" id="diverror">
                                </div>
                                <div class="pb10 clearfix">
                                </div>
                                <div class="table table-responsive   ">
                                    <asp:GridView ID="gvcomp" runat="server" AutoGenerateColumns="false" class="table table-hover"
                                        OnRowCommand="gvcomp_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr.No">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Profile Pic">
                                                <ItemTemplate>
                                                    <asp:Image ID="img" runat="server" class="image img-thumbnail" src='<%#Eval("ProfilePic","ProfileImage/{0}") %>'
                                                        Height="120px" Width="120px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Category">
                                                <ItemTemplate>
                                                    <%#Eval("Category")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Name">
                                                <ItemTemplate>
                                                    <%#Eval("Name")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Email">
                                                <ItemTemplate>
                                                    <%#Eval("UserName")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Contact No">
                                                <ItemTemplate>
                                                    <%#Eval("ContactNo")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Address">
                                                <ItemTemplate>
                                                    <%#Eval("Address")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Last Login Time">
                                                <ItemTemplate>
                                                    <%#Eval("LastLoginTime")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="FE Zone Area">
                                                <ItemTemplate>
                                                    <%# Eval("FEZoneAreaLocation").ToString() == "" ? "EMPTY" : Eval("FEZoneAreaLocation").ToString()%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Password">
                                                <ItemTemplate>
                                                    <%#Eval("Password")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblupdate" runat="server" CommandArgument='<%#Eval("EmpID") %>'
                                                        CommandName="up" class="fa fa-edit" OnClientClick="return confirm('Click ok for edit this record?');"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Active/Deactive">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbisacti" runat="server" CssClass="m-lg-r" Text='<%#Eval("Text") %>'></asp:Label>
                                                    <asp:LinkButton ID="lblactive" runat="server" OnClientClick="return confirm('click ok for modify this record?');"
                                                        class='<%#Eval("button") %>' CommandArgument='<%#Eval("EmpID") %>' CommandName="active"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
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
</asp:Content>
