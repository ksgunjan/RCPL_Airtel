<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmUpdateStatusSubmitManager.aspx.cs" Inherits="frmUpdateStatusSubmitManager" MasterPageFile="~/MasterPage.master" %>

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
                                    <asp:Label ID="lblPageName" runat="server" Text="Update Status"></asp:Label>
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
                            <div class="col-sm-4">
                                <label class="col-sm-12 form-control-label">
                                    Start Date</label>
                                <div class="col-sm-12">
                                    <asp:TextBox ID="txtdate" runat="server" class="form-control" type="date"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <label class="col-sm-12 form-control-label">
                                    End Date</label>
                                <div class="col-sm-12">
                                    <asp:TextBox ID="txtenddate" runat="server" class="form-control" type="date"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-4">
                                <label class="col-sm-12 form-control-label">
                                    Select Product Type</label>
                                <div class="col-sm-12">
                                    <asp:DropDownList ID="ddlprodtype" runat="server" class="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="form-group row">
                            <label class="col-sm-9 form-control-label">
                            </label>
                            <div class="col-sm-3">
                                <asp:Button ID="btnsub" runat="server" class="btn btn-sm btn-primary pull-right"
                                    Text="Search & Update" OnClick="btnsub_Click" />
                            </div>
                        </div>
                        <div class="form-group row">
                            <asp:GridView ID="gvexcel" runat="server" AutoGenerateColumns="false" class="table table-hover"
                                OnRowCommand="gvexcel_RowCommand">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkHeader" runat="server" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkRow" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Customer Name" HeaderText="Customer Name" />
                                    <asp:BoundField DataField="CRM ORDER ID" HeaderText="CRM ORDER ID" />
                                    <asp:BoundField DataField="Product Name" HeaderText="Product Name" />
                                    <asp:BoundField DataField="Project Manager" HeaderText="Project Manager" />
                                    <asp:BoundField DataField="FE Name" HeaderText="FE Name" />
                                    <asp:BoundField DataField="ComplainceDate" DataFormatString="{0:dd/MMM/yyyy}" HeaderText="Compliance Date" />
                                   
                                    <asp:TemplateField HeaderText="Update Status">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblstatus" runat="server" class="fa fa-edit fa-2x" CommandArgument='<%#Eval("UniqueID") %>'
                                                CommandName="UpdateStatus"></asp:LinkButton>
                                            <asp:HiddenField ID="hfLocID" runat="server" Value='<%#Eval("UniqueID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <div id="myModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel"
                                aria-hidden="true" class="modal fade text-left">
                                <div role="document" class="modal-dialog">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <button type="button" data-dismiss="modal" aria-label="Close" class="close">
                                                <span aria-hidden="true">×</span></button>
                                        </div>
                                        <div class="modal-body">
                                            <div runat="server" id="divupdatebyfe">
                                                <h3>Update Status as per FE response.</h3>
                                                <form>
                                                    <div>
                                                        <p>
                                                            All fields fill are mandatory(*)
                                                        </p>
                                                        <div class="Clearfix pb5">
                                                        </div>
                                                        <div class="form-group row">
                                                            <label class="col-sm-3 form-control-label">
                                                                Is Scanned
                                                            </label>
                                                            <div class="col-sm-9">
                                                                <asp:DropDownList ID="ddlstatus" runat="server" class="form-control">
                                                                    <asp:ListItem>Select Scanned Status</asp:ListItem>
                                                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                                    <asp:ListItem Value="N">No</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <label class="col-sm-3 form-control-label">
                                                                Scanned Date
                                                            </label>
                                                            <div class="col-sm-9">
                                                                <asp:TextBox ID="txtscanneddate" runat="server" class="form-control" type="date"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <label class="col-sm-3 form-control-label">
                                                                Is Submitted
                                                            </label>
                                                            <div class="col-sm-9">
                                                                <asp:DropDownList ID="ddlissubmited" runat="server" class="form-control">
                                                                    <asp:ListItem>Select Submitted Status</asp:ListItem>
                                                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                                    <asp:ListItem Value="N">No</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <label class="col-sm-3 form-control-label">
                                                                Submitted Date
                                                            </label>
                                                            <div class="col-sm-9">
                                                                <asp:TextBox ID="txtsubmiteddate" runat="server" class="form-control" type="date"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <label class="col-sm-7 form-control-label">
                                                            </label>
                                                            <div class="col-sm-5">
                                                                <asp:Button ID="btnsubmitstatus" runat="server" class="btn btn-primary btn-sm btn-block"
                                                                    Text="Submit Status" OnClick="btnsubmitstatus_Click" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </form>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
