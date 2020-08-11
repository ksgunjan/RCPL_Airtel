<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmAssignJobFEByManagerMobility.aspx.cs" Inherits="frmAssignJobFEByManagerMobility"
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
                                    <asp:Label ID="lblPageName" runat="server" Text="Assign Job to Field Executive Mobility"></asp:Label>
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
                        <h4>1.Select FE</h4>
                        <div class="Clearfix mt5">
                        </div>
                        <h4>2.Check on grid list and click submit to update or assign FE.</h4>
                        <div class="Clearfix mt5">
                        </div>
                        <h4>3.Assign job to new FE.</h4>
                        <div class="Clearfix mt5">
                        </div>
                        <br />
                        <div class="form-group row">
                            <div class="col-sm-3">
                                <label class="form-control-label">
                                    Allocation Date</label>
                                <div>
                                    <asp:TextBox ID="txtallocationdate" runat="server" class="form-control" type="date"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-3">
                                <label class="form-control-label">
                                    Select Field Executive</label>
                                <div>
                                    <asp:DropDownList ID="ddlfe1" runat="server" class="form-control focus">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-3">
                                <label class="form-control-label">
                                    Bill City</label>
                                <div>
                                    <asp:TextBox ID="txtbillcity" runat="server" class="form-control focus" placeholder="Bill City"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-3">
                                <label class="form-control-label pull-right">
                                    Search</label>
                                <div class="clearfix"></div>
                                <div>
                                    <asp:Button ID="btnsearch" runat="server" class="btn btn-primary pull-right" Text="Seach"
                                        OnClick="btnsearch_Click" />
                                </div>
                            </div>
                        </div>
                         <div class="clearfix mt10">
                        </div>
                        <asp:Label ID="lbltotal" runat="server" CssClass="label label-info"></asp:Label>
                        <div class="clearfix mt10">
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
                                <asp:BoundField DataField="Allocated_Date" HeaderText="Allocated Date" DataFormatString="{0:dd/MMM/yyyy}"
                                    NullDisplayText="#" />
                                <asp:TemplateField HeaderText="Complaince Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcompdate" runat="server" Text='<%#Eval("Complaince_Date","{0:dd/MMM/yyyy}") %>'></asp:Label>
                                        <asp:HiddenField ID="hdnIDGridRawData" runat="server" Value='<%#Eval("Party_Name") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Billed_Ext_Id" HeaderText="Billed Ext Id" NullDisplayText="#" />
                                <asp:BoundField DataField="Party_Name" HeaderText="Company Name" NullDisplayText="#" />
                                <asp:BoundField DataField="Customer_Name" HeaderText="Customer Name" NullDisplayText="#" />
                                <asp:BoundField DataField="Cust_Email" HeaderText="Cust Email" NullDisplayText="#" />
                                <asp:BoundField DataField="Product_Name" HeaderText="Product Name" NullDisplayText="#" />
                                <asp:BoundField DataField="Prod_Type_Name" HeaderText="PRODUCT TYPE" NullDisplayText="#" />
                                <asp:BoundField DataField="FE_Name" HeaderText="FE Name" />
                            </Columns>
                        </asp:GridView>
                        <div class="clearfix mt10">
                        </div>
                        <div class="form-group row" runat="server" id="divfename" visible="false">
                            <div class="col-sm-9">
                                <asp:DropDownList ID="ddlfe" runat="server" class="form-control focus">
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-3">
                                <asp:Button ID="btnsubmit" runat="server" class="btn btn-sm btn-primary btn-block"
                                    Text="Assign Job to FE" OnClick="btnsubmit_Click" />
                            </div>
                        </div>
                        <div class="clearfix mt10">
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnsubmit" />
            </Triggers>
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
