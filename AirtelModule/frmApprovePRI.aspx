<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmApprovePRI.aspx.cs" Inherits="frmApprovePRI"
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
                        Assign Job to Field Executive</h3>
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
                                <div class="Clearfix pb5">
                                </div>
                                <div class="form-group row">
                                    <div class="col-sm-9">
                                        <label class="form-control-label">
                                            Enter Allocated Date For Verify Upload Excel</label>
                                        <div>
                                            <asp:TextBox ID="txtdate" runat="server" class="form-control" type="date">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <label class="form-control-label">
                                            Click to find upload record for verify</label>
                                        <div>
                                            <asp:Button ID="btnSearch" runat="server" class="btn btn-sm btn-primary btn-block pull-right"
                                                Text="Search" OnClick="btnSearch_Click" />
                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix line">
                                </div>
                                <div runat="server" id="divbutton" visible="false">
                                    <b>
                                        <asp:Label ID="lbltoalrows" runat="server" class="text-info red"></asp:Label></b>
                                    <asp:Button ID="btndisapprove" runat="server" class="btn btn-danger btn-sm pull-right"
                                        Text="DisApprove" OnClick="btndisapprove_Click" />
                                    <asp:Button ID="btnapprove" runat="server" class="btn btn-success btn-sm pull-right"
                                        Text="Approve" OnClick="btnapprove_Click" />
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
                                        <asp:TemplateField HeaderText="ComplainceDate">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcompdate" runat="server" Text='<%#Eval("ComplainceDate","{0:dd/MMM/yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Subs Del No" HeaderText="Subs Del No" NullDisplayText="#" />
                                        <asp:BoundField DataField="Customer Name" HeaderText="Customer Name" NullDisplayText="#" />
                                        <asp:BoundField DataField="Account Number" HeaderText="Account Number" NullDisplayText="#" />
                                        <asp:TemplateField HeaderText="Product Name">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblproduct" Text='<%#Eval("ProductName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FE Name">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblfename" Text='<%#Eval("FE NAME") %>'></asp:Label>
                                                <asp:HiddenField ID="hdnIDGridRawData" runat="server" Value='<%#Eval("Subs Del No") %>' />
                                                <asp:HiddenField ID="hfbilledextendedid" runat="server" Value='<%#Eval("Account Number") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="KeyDup" HeaderText="Key Dup" NullDisplayText="#" />
                                    </Columns>
                                </asp:GridView>
                                <div class="clearfix line">
                                </div>
                                <br />
                                <div id="myModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel"
                                    aria-hidden="true" class="modal fade text-left">
                                    <div role="document" class="modal-dialog">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <button type="button" data-dismiss="modal" aria-label="Close" class="close">
                                                    <span aria-hidden="true">×</span></button>
                                            </div>
                                            <div class="modal-body">
                                                <asp:UpdatePanel ID="upanel" runat="server" ChildrenAsTriggers="true">
                                                    <ContentTemplate>
                                                        <div class="col-sm-12" runat="server" id="popuperror">
                                                        </div>
                                                        <div class="clearfix">
                                                        </div>
                                                        <div runat="server" id="diverrormssg" visible="false">
                                                            <asp:Label ID="lbltotalrowselected" runat="server" class="label label-warning block"></asp:Label>
                                                            <div class="clearfix pb20">
                                                            </div>
                                                            <asp:TextBox ID="txtreasone" runat="server" class="form-control" TextMode="MultiLine"
                                                                Height="100px" placeholder="Reason for delete excel"></asp:TextBox>
                                                            <div class="clear pb10">
                                                            </div>
                                                            <asp:Button ID="btndelete" runat="server" class="btn btn-primary pull-right" Text="Delete Selected Excel Record"
                                                                OnClick="btndelete_Click" />
                                                            <div class="clear pb10">
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="modal-footer">
                                                <button type="button" data-dismiss="modal" class="btn btn-secondary">
                                                    Close</button>
                                            </div>
                                        </div>
                                    </div>
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
