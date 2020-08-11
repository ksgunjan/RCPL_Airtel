<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmViweUnapproveExcel.aspx.cs" Inherits="frmViweUnapproveExcel"
    MasterPageFile="~/MasterPage.master" %>

<asp:Content ContentPlaceHolderID="head" runat="server" ID="header"></asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server" ID="inner">
    <div class="content oem-content">
        <asp:UpdatePanel runat="server" ID="updatepan">
            <ContentTemplate>
                <div class="sideBg">
                    <div class="row">
                        <div class="col-md-12 padding_0">
                            <ul class="breadcrumb">
                                <li>
                                    <asp:Label ID="lblPageName" runat="server" Text="View UnApproved Upload Excel"></asp:Label>
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
                        <b>For search record as per Date please select start month date or end month date.
                        </b>
                        <div class="clearfix">
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <h5>Upload Excel Start Date</h5>
                                <asp:TextBox ID="txtstartdate" runat="server" class="form-control" Placeholder="Select start date"
                                    type="date"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <h5>Upload Excel End Date</h5>
                                <asp:TextBox ID="txtenddate" runat="server" class="form-control" Placeholder="Select end date"
                                    type="date"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <h5>Select Product</h5>
                                <asp:DropDownList ID="ddlproduct" runat="server" class="form-control">
                                    <asp:ListItem Selected="True" Value="01">Select Product</asp:ListItem>
                                    <asp:ListItem Value="05">Leased Line</asp:ListItem>
                                    <asp:ListItem Value="02">MO</asp:ListItem>
                                    <asp:ListItem Value="03">PRI-Fixed Line</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="clearfix mt10">
                        </div>
                        <asp:Button ID="btnsearch" runat="server" class="btn btn-success pull-right" OnClick="btnsearch_Click"
                            Text="View/Search Upload Excel" />
                        <div class="clearfix mt10">
                        </div>
                        <div style="overflow: auto;">
                            <div class="clearfix mt10">
                            </div>
                            <b>
                                <asp:Label ID="lbltot" runat="server"></asp:Label></b>
                            <div class="clearfix mt10">
                            </div>
                            <asp:GridView ID="gvexcel" runat="server" AutoGenerateColumns="false" class="commonAjaxTbl dataTable master-company-table ViewProductTable table 
                                        display responsive no-wrap table-hover manage-user Grid table-responsive"
                                OnRowCreated="gvexcel_RowCreated">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr.No">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Allocated_Date" HeaderText="Allocated Date" DataFormatString="{0:dd-MMM-yyyy}"
                                        NullDisplayText="#" />
                                    <asp:BoundField DataField="Complaince_Date" DataFormatString="{0:dd/MMM/yyyy}" HeaderText="Compliance Date" />
                                    <asp:BoundField DataField="UniqueID" HeaderText="Logical or Billed No" NullDisplayText="#" />
                                    <asp:BoundField DataField="Subs_Del_No" HeaderText="Subs Del No" NullDisplayText="#" />
                                    <asp:BoundField DataField="Customer_Name" HeaderText="Customer Name" NullDisplayText="#" />
                                    <asp:BoundField DataField="Product_Name" HeaderText="Product Name" NullDisplayText="#" />
                                    <asp:BoundField DataField="Prod_Type_Name" HeaderText="PRODUCT TYPE" NullDisplayText="#" />
                                    <asp:BoundField DataField="FE_Name" HeaderText="FE Name" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="clearfix mt10"></div>
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
