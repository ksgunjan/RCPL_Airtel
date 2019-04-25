<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmAssignJobFEByManagerPRI.aspx.cs" Inherits="frmAssignJobFEByManagerPRI" MasterPageFile="~/MasterPage2.master" %>

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
            z-index: 9999999;
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
            width: 150%;
            top: 0;
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.2;
            -moz-opacity: 0.2;
            margin-left: -300px;
            margin-top: 0px;
        }
    </style>
    <!----------------------------End----------------------------------->
    <!------------------------------------CheckGrid----------------------------------------------------------->
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $("[id*=chkHeader]").live("click", function () {
            var chkHeader = $(this);
            var grid = $(this).closest("table");
            $("input[type=checkbox]", grid).each(function () {
                if (chkHeader.is(":checked")) {
                    $(this).attr("checked", "checked");
                    $("td", $(this).closest("tr")).addClass("selected");
                } else {
                    $(this).removeAttr("checked");
                    $("td", $(this).closest("tr")).removeClass("selected");
                }
            });
        });
        $("[id*=chkRow]").live("click", function () {
            var grid = $(this).closest("table");
            var chkHeader = $("[id*=chkHeader]", grid);
            if (!$(this).is(":checked")) {
                $("td", $(this).closest("tr")).removeClass("selected");
                chkHeader.removeAttr("checked");
            } else {
                $("td", $(this).closest("tr")).addClass("selected");
                if ($("[id*=chkRow]", grid).length == $("[id*=chkRow]:checked", grid).length) {
                    chkHeader.attr("checked", "checked");
                }
            }
        });
    </script>
    <!------------------------------------------------End CheckListBox------------------------------------------------->
</asp:Content>
<asp:Content ID="contentmain" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="loader">
    </div>
    <div class="right_col" role="main">
        <div class="">
            <div class="page-title">
                <div class="title_left">
                    <h3>
                        Assign Job to Field Executive Leased Line</h3>
                </div>
            </div>
            <div class="card">
                <asp:UpdatePanel ID="updatepanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
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
                                <h4>
                                    1.Select FE</h4>
                                <div class="Clearfix pb5">
                                </div>
                                <h4>
                                    2.Check on grid list and click submit to update or assign FE.</h4>
                                <div class="Clearfix pb5">
                                </div>
                                <h4>
                                    3.Assign job to new FE.</h4>
                                <div class="Clearfix pb5">
                                </div>
                                <br />
                                <div class="form-group row">
                                    <div class="col-sm-6">
                                        <label class="form-control-label">
                                            Select Field Executive</label>
                                        <div>
                                            <asp:DropDownList ID="ddlfe1" runat="server" class="form-control focus" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlfe1_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix line">
                                </div>
                                <asp:GridView ID="gvexcel" runat="server" AutoGenerateColumns="false" class="table table-hover">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkHeader" runat="server" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkRow" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Allocated Date" HeaderText="Allocated Date" DataFormatString="{0:dd/MMM/yyyy}"
                                            NullDisplayText="#" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="lblcompdate" runat="server" Text='<%#Eval("ComplainceDate","{0:dd/MMM/yyyy}") %>'></asp:Label>
                                                <asp:HiddenField ID="hdnIDGridRawData" runat="server" Value='<%#Eval("Subs Del No") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Subs Del No" HeaderText="Subs Del No" NullDisplayText="#" />
                                        <asp:BoundField DataField="Customer Name" HeaderText="Customer Name" NullDisplayText="#" />
                                        <asp:BoundField DataField="KeyDup" HeaderText="KeyDup" NullDisplayText="#" />
                                        <asp:BoundField DataField="ProductName" HeaderText="Product Name" NullDisplayText="#" />
                                        <%--<asp:BoundField DataField="ProdTypeName" HeaderText="PRODUCT TYPE" NullDisplayText="#" />--%>
                                        <asp:BoundField DataField="FE Name" HeaderText="FE Name" />
                                    </Columns>
                                </asp:GridView>
                                <div class="clearfix line">
                                </div>
                                <div class="form-group row" runat="server" id="divfename" visible="false">
                                    <div class="col-sm-9">
                                        <asp:DropDownList ID="ddlfe" runat="server" AutoPostBack="true" class="form-control focus">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:Button ID="btnsubmit" runat="server" class="btn btn-sm btn-primary btn-block"
                                            Text="Assign Job to FE" OnClick="btnsubmit_Click" />
                                    </div>
                                </div>
                                <div class="clearfix">
                                </div>
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
