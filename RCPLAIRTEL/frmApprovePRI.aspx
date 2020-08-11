<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmApprovePRI.aspx.cs" Inherits="frmApprovePRI"
    MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="conhead" runat="server" ContentPlaceHolderID="head">
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
    <div class="content oem-content">
        <asp:UpdatePanel runat="server" ID="updatepan">
            <ContentTemplate>
                <div class="sideBg">
                    <div class="row">
                        <div class="col-md-12 padding_0">
                            <ul class="breadcrumb">
                                <li>
                                    <asp:Label ID="lblPageName" runat="server" Text="Approved Excel 2nd Level PRI"></asp:Label>
                                </li>
                            </ul>
                        </div>
                        <div class="col-md-12">
                            <div class="clearfix"></div>
                            <div style="margin-top: 5px;">
                                <a class="fa fa-arrow-circle-left pull-right" href="javascript: history.go(-1)">&nbsp; &nbsp;Back</a>
                            </div>
                            <div class="clearfix"></div>
                        </div>
                    </div>
                    <div class="clearfix" style="margin-bottom: 10px;"></div>
                    <div class="section-pannel">
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
                        <asp:GridView ID="gvexcel" runat="server" AutoGenerateColumns="false" class="commonAjaxTbl dataTable master-company-table ViewProductTable table 
                                        display responsive no-wrap table-hover manage-user Grid table-responsive"
                            OnRowCreated="gvexcel_RowCreated">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkHeader" runat="server" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkRow" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Allocated_Date" HeaderText="Allocated Date" DataFormatString="{0:dd/MMM/yyyy}"
                                    NullDisplayText="#" />
                                <asp:TemplateField HeaderText="ComplainceDate">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcompdate" runat="server" Text='<%#Eval("Complaince_Date","{0:dd/MMM/yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Subs_Del_No" HeaderText="Subs Del No" NullDisplayText="#" />
                                <asp:BoundField DataField="Customer_Name" HeaderText="Customer Name" NullDisplayText="#" />
                                <asp:BoundField DataField="Account_Number" HeaderText="Account Number" NullDisplayText="#" />
                                <asp:TemplateField HeaderText="Product_Name">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblproduct" Text='<%#Eval("Product_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FE Name">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblfename" Text='<%#Eval("FE_NAME") %>'></asp:Label>
                                        <asp:HiddenField ID="hdnIDGridRawData" runat="server" Value='<%#Eval("Subs_Del_No") %>' />
                                        <asp:HiddenField ID="hfbilledextendedid" runat="server" Value='<%#Eval("Account_Number") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Key_Dup" HeaderText="Key Dup" NullDisplayText="#" />
                            </Columns>
                        </asp:GridView>
                        <div class="clearfix mt10">
                        </div>
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
        <asp:UpdateProgress ID="UpdateProgress6" runat="server" AssociatedUpdatePanelID="updatepan">
            <ProgressTemplate>
                <div class="overlay-progress">
                    <div class="custom-progress-bar blue stripes">
                        <span></span>
                        <p>Please wait we are processing your request</p>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
</asp:Content>
