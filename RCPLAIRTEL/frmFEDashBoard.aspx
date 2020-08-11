<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmFEDashBoard.aspx.cs" Inherits="frmFEDashBoard" MasterPageFile="~/MasterPage.master" %>

<asp:Content ContentPlaceHolderID="head" runat="server" ID="header"></asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server" ID="inner">
    <div class="content oem-content">
        <div class="sideBg">
            <div class="row">
                <div class="col-mod-12 padding_0">
                    <ul class="breadcrumb">
                        <li>
                            <asp:Label ID="lblPageName" runat="server" Text="FE Dashboard"></asp:Label>
                        </li>
                    </ul>
                </div>
            </div>
            <div class="addfdi">
                <div class="admin-dashboard">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="row">
                                <h4 class="heading-secondary">Leased Line Detail</h4>
                         
                                <div class="col-lg-4 col-sm-6 col-xs-12">
                                    <div class="white-box analytics-info total-comp">
                                        <ul class="list-inline two-part">
                                            <li>
                                                <div class="icon-box">
                                                    <i class="far fa-building"></i>
                                                </div>
                                                <div class="compName">
                                                    <h3 class="box-title">Total Client</h3>
                                                    <div class="Number">

                                                        <asp:Label ID="lbltotclient" CssClass="comp_number" runat="server" Text="0"></asp:Label>
                                                    </div>
                                                </div>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                                <div class="col-lg-4 col-sm-6 col-xs-12">
                                    <div class="white-box analytics-info total-fdi">
                                        <ul class="list-inline two-part">
                                            <li>
                                                <div class="icon-box">
                                                    <i class="far fa-building"></i>
                                                </div>
                                                <div class="compName">
                                                    <h3 class="box-title">Total Visit Done</h3>
                                                    <div class="Number">
                                                        <asp:Label ID="lbltotalvisitdone" runat="server" CssClass="comp_number" Text="0"></asp:Label>
                                                    </div>
                                                </div>
                                            </li>
                                        </ul>

                                    </div>
                                </div>
                                <div class="col-lg-4 col-sm-6 col-xs-12">
                                    <div class="white-box analytics-info last-fdi">
                                        <ul class="list-inline two-part">
                                            <li>
                                                <div class="icon-box">
                                                    <i class="far fa-building"></i>
                                                </div>
                                                <div class="compName">
                                                    <h3 class="box-title">Pending Visit</h3>
                                                    <div class="Number">
                                                        <asp:Label ID="lbltotalpendingvisit" runat="server" CssClass="comp_number" Text="0"></asp:Label>
                                                    </div>
                                                </div>
                                            </li>
                                        </ul>

                                    </div>
                                </div>
                                <div class="clearfix mt10"></div>
                                <div class="col-lg-4 col-sm-6 col-xs-12">
                                    <div class="white-box analytics-info last-fdi">
                                        <ul class="list-inline two-part">
                                            <li>
                                                <div class="icon-box">
                                                    <i class="fa fa-users" aria-hidden="true"></i>
                                                </div>
                                                <div class="compName">
                                                    <h3 class="box-title">Pending Revisit Case</h3>
                                                    <div class="Number">
                                                        <asp:LinkButton ID="lbltotalrevisit" runat="server" CssClass="comp_number" Text="0"></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </li>
                                        </ul>

                                    </div>
                                </div>
                                <div class="col-lg-4 col-sm-6 col-xs-12">
                                    <div class="white-box analytics-info last-fdi">
                                        <ul class="list-inline two-part">
                                            <li>
                                                <div class="icon-box">
                                                    <i class="fa fa-users" aria-hidden="true"></i>
                                                </div>
                                                <div class="compName">
                                                    <h3 class="box-title">Pre schedule Appointment</h3>
                                                    <div class="Number">
                                                        <asp:LinkButton ID="lbltotalpresche" runat="server" CssClass="comp_number" Text="0"></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </li>
                                        </ul>

                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <h4 class="heading-secondary">MO Detail</h4>
                              
                                <div class="col-lg-4 col-sm-6 col-xs-12">
                                    <div class="white-box analytics-info total-comp">
                                        <ul class="list-inline two-part">
                                            <li>
                                                <div class="icon-box">
                                                    <i class="far fa-building"></i>
                                                </div>
                                                <div class="compName">
                                                    <h3 class="box-title">Total Client</h3>
                                                    <div class="Number">

                                                        <asp:Label ID="lbltotalclientmo" CssClass="comp_number" runat="server" Text="0"></asp:Label>
                                                    </div>
                                                </div>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                                <div class="col-lg-4 col-sm-6 col-xs-12">
                                    <div class="white-box analytics-info total-fdi">
                                        <ul class="list-inline two-part">
                                            <li>
                                                <div class="icon-box">
                                                    <i class="far fa-building"></i>
                                                </div>
                                                <div class="compName">
                                                    <h3 class="box-title">Total Visit Done</h3>
                                                    <div class="Number">
                                                        <asp:Label ID="lbltotalvisitidonemo" runat="server" CssClass="comp_number" Text="0"></asp:Label>
                                                    </div>
                                                </div>
                                            </li>
                                        </ul>

                                    </div>
                                </div>
                                <div class="col-lg-4 col-sm-6 col-xs-12">
                                    <div class="white-box analytics-info last-fdi">
                                        <ul class="list-inline two-part">
                                            <li>
                                                <div class="icon-box">
                                                    <i class="far fa-building"></i>
                                                </div>
                                                <div class="compName">
                                                    <h3 class="box-title">Pending Visit</h3>
                                                    <div class="Number">
                                                        <asp:Label ID="lblpenvisitmo" runat="server" CssClass="comp_number" Text="0"></asp:Label>
                                                    </div>
                                                </div>
                                            </li>
                                        </ul>

                                    </div>
                                </div>
                                <div class="clearfix mt10"></div>
                                <div class="col-lg-4 col-sm-6 col-xs-12">
                                    <div class="white-box analytics-info last-fdi">
                                        <ul class="list-inline two-part">
                                            <li>
                                                <div class="icon-box">
                                                    <i class="fa fa-users" aria-hidden="true"></i>
                                                </div>
                                                <div class="compName">
                                                    <h3 class="box-title">Pending Revisit Case</h3>
                                                    <div class="Number">
                                                        <asp:LinkButton ID="lblrevisitmo" runat="server" CssClass="comp_number" Text="0"></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </li>
                                        </ul>

                                    </div>
                                </div>
                                <div class="col-lg-4 col-sm-6 col-xs-12">
                                    <div class="white-box analytics-info last-fdi">
                                        <ul class="list-inline two-part">
                                            <li>
                                                <div class="icon-box">
                                                    <i class="fa fa-users" aria-hidden="true"></i>
                                                </div>
                                                <div class="compName">
                                                    <h3 class="box-title">Pre schedule Appointment</h3>
                                                    <div class="Number">
                                                        <asp:LinkButton ID="lblpreschmo" runat="server" CssClass="comp_number" Text="0"></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </li>
                                        </ul>

                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <h4 class="heading-secondary">PRI-Fixed Line</h4>
                            
                                <div class="col-lg-4 col-sm-6 col-xs-12">
                                    <div class="white-box analytics-info total-comp">
                                        <ul class="list-inline two-part">
                                            <li>
                                                <div class="icon-box">
                                                    <i class="far fa-building"></i>
                                                </div>
                                                <div class="compName">
                                                    <h3 class="box-title">Total Client</h3>
                                                    <div class="Number">

                                                        <asp:Label ID="lbltotalclientpri" CssClass="comp_number" runat="server" Text="0"></asp:Label>
                                                    </div>
                                                </div>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                                <div class="col-lg-4 col-sm-6 col-xs-12">
                                    <div class="white-box analytics-info total-fdi">
                                        <ul class="list-inline two-part">
                                            <li>
                                                <div class="icon-box">
                                                    <i class="far fa-building"></i>
                                                </div>
                                                <div class="compName">
                                                    <h3 class="box-title">Total Visit Done</h3>
                                                    <div class="Number">
                                                        <asp:Label ID="lbltotalvisitdonepri" runat="server" CssClass="comp_number" Text="0"></asp:Label>
                                                    </div>
                                                </div>
                                            </li>
                                        </ul>

                                    </div>
                                </div>
                                <div class="col-lg-4 col-sm-6 col-xs-12">
                                    <div class="white-box analytics-info last-fdi">
                                        <ul class="list-inline two-part">
                                            <li>
                                                <div class="icon-box">
                                                    <i class="far fa-building"></i>
                                                </div>
                                                <div class="compName">
                                                    <h3 class="box-title">Pending Visit</h3>
                                                    <div class="Number">
                                                        <asp:Label ID="lblpendingvisitpri" runat="server" CssClass="comp_number" Text="0"></asp:Label>
                                                    </div>
                                                </div>
                                            </li>
                                        </ul>

                                    </div>
                                </div>
                                <div class="clearfix mt10"></div>
                                <div class="col-lg-4 col-sm-6 col-xs-12">
                                    <div class="white-box analytics-info last-fdi">
                                        <ul class="list-inline two-part">
                                            <li>
                                                <div class="icon-box">
                                                    <i class="fa fa-users" aria-hidden="true"></i>
                                                </div>
                                                <div class="compName">
                                                    <h3 class="box-title">Pending Revisit Case</h3>
                                                    <div class="Number">
                                                        <asp:LinkButton ID="lblpendrevisitpri" runat="server" CssClass="comp_number" Text="0"></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </li>
                                        </ul>

                                    </div>
                                </div>
                                <div class="col-lg-4 col-sm-6 col-xs-12">
                                    <div class="white-box analytics-info last-fdi">
                                        <ul class="list-inline two-part">
                                            <li>
                                                <div class="icon-box">
                                                    <i class="fa fa-users" aria-hidden="true"></i>
                                                </div>
                                                <div class="compName">
                                                    <h3 class="box-title">Pre schedule Appointment</h3>
                                                    <div class="Number">
                                                        <asp:LinkButton ID="lblpreschepri" runat="server" CssClass="comp_number" Text="0"></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </li>
                                        </ul>

                                    </div>
                                </div>
                            </div>
                            <div class="clearfix mt10">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
