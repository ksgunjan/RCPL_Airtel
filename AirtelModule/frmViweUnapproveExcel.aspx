<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmViweUnapproveExcel.aspx.cs"
    Inherits="frmViweUnapproveExcel" MasterPageFile="~/MasterPage2.master" %>

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
                        View UnApproved Upload Excel</h3>
                </div>
            </div>
            <div class="card">
                <asp:UpdatePanel ID="updatepanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="card-body">
                            <div class="clearfix pb10">
                            </div>
                            <div runat="server" id="diverror" class="col-sm-12">
                            </div>
                            <div class="clearfix pb10">
                            </div>
                            <div id="divhideonallexcelclick" runat="server">
                                <h4>
                                    For search record as per Date please select start month date or end month date.
                                </h4>
                                <div class="clearfix">
                                </div>
                                <div class="row">
                                    <div class="col-md-4">
                                        <h5>
                                            Upload Excel Start Date</h5>
                                        <asp:TextBox ID="txtstartdate" runat="server" class="form-control" Placeholder="Select start date"
                                            type="date"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        <h5>
                                            Upload Excel End Date</h5>
                                        <asp:TextBox ID="txtenddate" runat="server" class="form-control" Placeholder="Select end date"
                                            type="date"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        <h5>
                                            Select Case Status</h5>
                                        <asp:DropDownList ID="ddlcasestatus" runat="server" class="form-control">
                                            <asp:ListItem Selected="True" Value="01">Select Case</asp:ListItem>
                                            <asp:ListItem Value="05">All</asp:ListItem>
                                            <asp:ListItem Value="02">Negative</asp:ListItem>
                                            <asp:ListItem Value="03">Pending</asp:ListItem>
                                            <asp:ListItem Value="04">Positive</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="clearfix pb10">
                                </div>
                                <asp:Button ID="btnsearch" runat="server" class="btn btn-success pull-right" OnClick="btnsearch_Click"
                                    Text="View/Search Upload Excel" />
                            </div>
                            <div class="pb20">
                            </div>
                            <hr />
                            <div class="table table-responsive">
                                <div>
                                    <h4 id="h4message" runat="server">
                                    </h4>
                                </div>
                                <div class="pb-2">
                                </div>
                                <asp:GridView ID="gvexcel" runat="server" AutoGenerateColumns="false" class="table table-hover">
                                    <Columns>
                                        <asp:BoundField DataField="Allocated Date" HeaderText="Allocated Date" DataFormatString="{0:dd-MMM-yyyy}"
                                            NullDisplayText="#" />
                                        <asp:BoundField DataField="ComplainceDate" DataFormatString="{0:dd/MMM/yyyy}" HeaderText="Compliance Date" />
                                        <asp:BoundField DataField="Logical Circuit Id" HeaderText="Logical Circuit Id" NullDisplayText="#" />
                                        <asp:BoundField DataField="Billed Ext ID" HeaderText="Billed Ext ID" NullDisplayText="#" />
                                        <asp:BoundField DataField="Subs Del No" HeaderText="Subs Del No" NullDisplayText="#" />
                                        <asp:BoundField DataField="Customer Name" HeaderText="Customer Name" NullDisplayText="#" />
                                        <asp:BoundField DataField="BILLING_BANDWIDTH" HeaderText="BILLING BANDWIDTH" NullDisplayText="#" />
                                        <asp:BoundField DataField="ProductName" HeaderText="Product Name" NullDisplayText="#" />
                                        <asp:BoundField DataField="ProdTypeName" HeaderText="PRODUCT TYPE" NullDisplayText="#" />
                                        <asp:BoundField DataField="FE Name" HeaderText="FE Name" />
                                        <asp:TemplateField HeaderText="View Detail" Visible="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lblview" runat="server" class="fa fa-eye fa-2x" CommandArgument='<%#Eval("UniqueID") %>'
                                                    CommandName="ViewDetail"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </ContentTemplate>
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
