<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmFEDashBoard.aspx.cs" Inherits="frmFEDashBoard"
    MasterPageFile="~/MasterPage2.master" %>

<asp:Content ID="conheader" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="conplace" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="right_col" role="main">
        <div class="">
            <div class="title_left">
                <h3>
                    FE Dashboard</h3>
            </div>
            <div class="row top_tiles" runat="server" visible="false" id="divleasedline">
                <div class="animated flipInY col-lg-4 col-md-4 col-sm-6 col-xs-12">
                    <div class="tile-stats">
                        <div class="icon pt5">
                            <i class="fa fa-users"></i>
                        </div>
                        <div class="count" runat="server" id="divcompany">
                        </div>
                        <h3>
                            Total Client</h3>
                        <p>
                            Total client for visit</p>
                    </div>
                </div>
                <div class="animated flipInY col-lg-4 col-md-4 col-sm-6 col-xs-12">
                    <div class="tile-stats">
                        <div class="icon pt5">
                            <i class="fa fa-user-plus"></i>
                        </div>
                        <div class="count" runat="server" id="divproduct">
                        </div>
                        <h3>
                            Total Visit Done</h3>
                        <p>
                            Total visit done till yet</p>
                    </div>
                </div>
                <div class="animated flipInY col-lg-4 col-md-4 col-sm-6 col-xs-12">
                    <div class="tile-stats">
                        <div class="icon pt5">
                            <i class="fa fa-user-secret"></i>
                        </div>
                        <div class="count" runat="server" id="divprotype">
                        </div>
                        <h3>
                            Pending Visit</h3>
                        <p>
                            Total client visit pending</p>
                    </div>
                </div>
                <div class="animated flipInY col-lg-4 col-md-4 col-sm-6 col-xs-12">
                    <div class="tile-stats">
                        <div class="icon pt5">
                            <i class="fa fa-lock"></i>
                        </div>
                        <div class="count" runat="server" id="divrevisit">
                        </div>
                        <h3>
                            Pending Revisit Case</h3>
                        <p>
                            Total client revisit case</p>
                    </div>
                </div>
                <div class="animated flipInY col-lg-4 col-md-4 col-sm-6 col-xs-12">
                    <div class="tile-stats">
                        <div class="icon pt5">
                            <i class="fa fa-user-plus"></i>
                        </div>
                        <div class="count" runat="server" id="divpreschedule">
                        </div>
                        <h3>
                            Pre schedule Appointment</h3>
                        <p>
                            Total Preschedule-Appointment case</p>
                    </div>
                </div>
            </div>
            <div class="row top_tiles" runat="server" visible="false" id="divMobility">
                <div class="animated flipInY col-lg-4 col-md-4 col-sm-6 col-xs-12">
                    <div class="tile-stats">
                        <div class="icon pt5">
                            <i class="fa fa-users"></i>
                        </div>
                        <div class="count" runat="server" id="divmototal">
                        </div>
                        <h3>
                            Total Client</h3>
                        <p>
                            Total client for visit</p>
                    </div>
                </div>
                <div class="animated flipInY col-lg-4 col-md-4 col-sm-6 col-xs-12">
                    <div class="tile-stats">
                        <div class="icon pt5">
                            <i class="fa fa-user-plus"></i>
                        </div>
                        <div class="count" runat="server" id="divmovisitdone">
                        </div>
                        <h3>
                            Total Visit Done</h3>
                        <p>
                            Total visit done till yet</p>
                    </div>
                </div>
                <div class="animated flipInY col-lg-4 col-md-4 col-sm-6 col-xs-12">
                    <div class="tile-stats">
                        <div class="icon pt5">
                            <i class="fa fa-user-secret"></i>
                        </div>
                        <div class="count" runat="server" id="divmovisipen">
                        </div>
                        <h3>
                            Pending Visit</h3>
                        <p>
                            Total client visit pending</p>
                    </div>
                </div>
                <div class="animated flipInY col-lg-4 col-md-4 col-sm-6 col-xs-12">
                    <div class="tile-stats">
                        <div class="icon pt5">
                            <i class="fa fa-lock"></i>
                        </div>
                        <div class="count" runat="server" id="divvisitrevi">
                        </div>
                        <h3>
                            Pending Revisit Case</h3>
                        <p>
                            Total client revisit case</p>
                    </div>
                </div>
                <div class="animated flipInY col-lg-4 col-md-4 col-sm-6 col-xs-12">
                    <div class="tile-stats">
                        <div class="icon pt5">
                            <i class="fa fa-user-plus"></i>
                        </div>
                        <div class="count" runat="server" id="divpresschdulemo">
                        </div>
                        <h3>
                            Pre schedule Appointment</h3>
                        <p>
                            Total Preschedule-Appointment case</p>
                    </div>
                </div>
            </div>
            <div class="row top_tiles" runat="server" visible="false" id="divpri">
                <div class="animated flipInY col-lg-4 col-md-4 col-sm-6 col-xs-12">
                    <div class="tile-stats">
                        <div class="icon pt5">
                            <i class="fa fa-users"></i>
                        </div>
                        <div class="count" runat="server" id="divtotalpri">
                        </div>
                        <h3>
                            Total Client</h3>
                        <p>
                            Total client for visit</p>
                    </div>
                </div>
                <div class="animated flipInY col-lg-4 col-md-4 col-sm-6 col-xs-12">
                    <div class="tile-stats">
                        <div class="icon pt5">
                            <i class="fa fa-user-plus"></i>
                        </div>
                        <div class="count" runat="server" id="divtotalvisitpri">
                        </div>
                        <h3>
                            Total Visit Done</h3>
                        <p>
                            Total visit done till yet</p>
                    </div>
                </div>
                <div class="animated flipInY col-lg-4 col-md-4 col-sm-6 col-xs-12">
                    <div class="tile-stats">
                        <div class="icon pt5">
                            <i class="fa fa-user-secret"></i>
                        </div>
                        <div class="count" runat="server" id="divtotalpendingpri">
                        </div>
                        <h3>
                            Pending Visit</h3>
                        <p>
                            Total client visit pending</p>
                    </div>
                </div>
                <div class="animated flipInY col-lg-4 col-md-4 col-sm-6 col-xs-12">
                    <div class="tile-stats">
                        <div class="icon pt5">
                            <i class="fa fa-lock"></i>
                        </div>
                        <div class="count" runat="server" id="divtotalpenpri">
                        </div>
                        <h3>
                            Pending Revisit Case</h3>
                        <p>
                            Total client revisit case</p>
                    </div>
                </div>
                <div class="animated flipInY col-lg-4 col-md-4 col-sm-6 col-xs-12">
                    <div class="tile-stats">
                        <div class="icon pt5">
                            <i class="fa fa-user-plus"></i>
                        </div>
                        <div class="count" runat="server" id="divprepri">
                        </div>
                        <h3>
                            Pre schedule Appointment</h3>
                        <p>
                            Total Preschedule-Appointment case</p>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
