<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmNotEligibleDataMail.aspx.cs"
    Inherits="frmNotEligibleDataMail" MasterPageFile="~/MasterPage2.master" %>

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
                        New Company</h3>
                </div>
            </div>
            <asp:UpdatePanel ID="updatepanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="card-body">
                        <div class="form-horizontal">
                            <div class="clearfix pb10">
                            </div>
                            <div class="form-group row">
                                <div runat="server" id="diverror" class="col-sm-12">
                                </div>
                            </div>
                            <div class="clearfix pb10">
                            </div>
                            <div class="form-group row">
                                <div class="col-sm-4">
                                    <p>
                                        Select Allocated Date</p>
                                    <asp:TextBox ID="txtallocatedDate" runat="server" class="form-control" type="date"></asp:TextBox>
                                </div>
                                <div class="col-sm-4">
                                    <p>
                                        Select Product</p>
                                    <asp:DropDownList ID="ddlcategory" runat="server" class="form-control" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlcategory_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-4">
                                    <p>
                                        Select Sub Product</p>
                                    <asp:DropDownList ID="ddlsubprod" runat="server" class="form-control" Enabled="false">
                                    </asp:DropDownList>
                                </div>
                                <div class="clearfix pb10">
                                </div>
                                <asp:Button ID="btnclear" runat="server" Text="Clear" class="btn btn-warning pull-right mr10"
                                    OnClick="btnclear_Click" />
                                <asp:Button ID="btnsearch" runat="server" Text="Search" class="btn btn-success pull-right mr10"
                                    OnClick="btnsearch_Click" />
                                <div class="clearfix">
                                </div>
                            </div>
                            <div class="clear pb10">
                            </div>
                            <div>
                                <asp:Button ID="btnsendmail" runat="server" Visible="false" class="btn btn-primary pull-right mr10"
                                    Text="Add Comment" OnClick="btnsendmail_Click" />
                            </div>
                            <div class="clearfix pb10">
                            </div>
                            <div runat="server" id="gvprileasedline">
                                <div class="table table-responsive">
                                    <asp:GridView ID="gvnoteligible" runat="server" class="table table-hover" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Allocated Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblallocateddate" runat="server" Text='<%#Eval("[Allocated Date]","{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Logical/Sub ID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbllogid" runat="server" Text='<%#Eval("[Logical Circuit ID]") %>'></asp:Label>
                                                    <asp:Label ID="lblbill" runat="server" Text='<%#Eval("[Billed Ext ID]") %>'></asp:Label>
                                                    <asp:Label ID="lblsubs" runat="server" Text='<%#Eval("[Subs Del No]") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Company">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblpartyname" runat="server" Text='<%#Eval("[Party Name]") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                                            <asp:BoundField DataField="ProdTypeName" HeaderText="Prodauct Type" />
                                            <asp:BoundField DataField="Eligible/Not Eligible" HeaderText="Eligible" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                            <div runat="server" id="dlmo">
                                <div>
                                    <asp:Button ID="btnsendmailmo" runat="server" Visible="false" class="btn btn-primary pull-right mr10"
                                        Text="Add Comment" OnClick="btnsendmailmo_Click" />
                                </div>
                                <div class="clearfix pb10">
                                </div>
                                <asp:DataList ID="datalistMo" runat="server" RepeatColumns="1" RepeatDirection="Horizontal"
                                    RepeatLayout="Flow" OnItemDataBound="datalistMo_ItemDataBound">
                                    <ItemTemplate>
                                        <div>
                                            <b>
                                                <asp:Label ID="lblcompdl" runat="server" Text='<%#Eval("[Party Name]") %>'></asp:Label></b>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                        <div class="table table-responsive">
                                            <asp:GridView ID="gvmo" runat="server" class="table table-hover" AutoGenerateColumns="false">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Allocated Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblallocateddatemo" runat="server" Text='<%#Eval("[Allocated Date]","{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Billed Ext ID">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblbillmo" runat="server" Text='<%#Eval("[Billed Ext ID]") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Company">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblpartynamemo" runat="server" Text='<%#Eval("[Party Name]") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                                                    <asp:BoundField DataField="ProdTypeName" HeaderText="Prodauct Type" />
                                                    <asp:BoundField DataField="Eligible/Not Eligible" HeaderText="Eligible" />
                                                    <asp:BoundField DataField="EligibleRemarks" HeaderText="Remark" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </ItemTemplate>
                                </asp:DataList>
                            </div>
                            <div class="clearfix pb10">
                            </div>
                        </div>
                        <div class="clearfix pb10">
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
            <div id="myModal" aria-hidden="true" aria-labelledby="exampleModalLabel" class="modal fade text-left"
                role="dialog" tabindex="-1">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button aria-label="Close" class="close" data-dismiss="modal" type="button">
                                <span aria-hidden="true">×</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div id="divshow" runat="server">
                                <h3>
                                    A brief detail of client information.</h3>
                                <div class="clearfix">
                                </div>
                                <div runat="server" id="diverrorpopup">
                                </div>
                                <div class="clearfix pb10">
                                </div>
                                <asp:Label ID="lbltotalrowselected" runat="server" Text=""></asp:Label>
                                <div class="clearfix pb10">
                                </div>
                                <p>
                                    Please Describe Not Eligible Reasone
                                    <asp:TextBox ID="txtremark" runat="server" PleaceHolder="Remark" TextMode="MultiLine"
                                        Height="100px" class="form-control"></asp:TextBox></p>
                                <div class="clearfix">
                                </div>
                                <div>
                                    <asp:Button ID="btnsub" runat="server" class="btn btn-success pull-right mr10" Text="Send Mail"
                                        OnClick="btnsub_Click" />
                                </div>
                                <div class="clearfix pb10">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
