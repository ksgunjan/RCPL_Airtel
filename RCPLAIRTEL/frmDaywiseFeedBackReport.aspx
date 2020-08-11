<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmDaywiseFeedBackReport.aspx.cs" Inherits="frmDaywiseFeedBackReport"
    MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="conhead" runat="server" ContentPlaceHolderID="head">
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
                                    <asp:Label ID="lblPageName" runat="server" Text="Fe Day Wise FeedBack Report"></asp:Label>
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
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtsearch" runat="server" class="form-control" type="date"></asp:TextBox>
                        </div>
                        <div class="col-sm-4">
                            <asp:Button ID="btnsearch" runat="server" class="btn btn-info" Text="Search" OnClick="btnsearch_Click" />
                        </div>
                        <div class="clearfix mt10">
                        </div>
                        <asp:Button ID="btnmail" runat="server" class="btn btn-danger btn-sm pull-right mr10"
                            Text="Send Mail" OnClick="btnmail_Click" />
                        <div class="clearfix mt10">
                        </div>

                        <h4 class="text-center">Leased Line</h4>
                        <div class="clearfix">
                        </div>
                        <div style="overflow: auto;">
                            <asp:GridView ID="gvll" runat="server" class="commonAjaxTbl dataTable master-company-table ViewProductTable table 
                                        display responsive no-wrap table-hover manage-user Grid table-responsive"
                                AutoGenerateColumns="false" OnRowCreated="gvll_RowCreated">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Logical_Circuit_ID" HeaderText="Logical Circuit ID" NullDisplayText="#" />
                                    <asp:BoundField DataField="Party_Name" HeaderText="Company Name" NullDisplayText="#" />
                                    <asp:BoundField DataField="FE_Name" HeaderText="FE Name" NullDisplayText="#" />
                                    <asp:BoundField DataField="Status_By_Fe" HeaderText="Audit Status" NullDisplayText="#" />
                                    <asp:BoundField DataField="Code" HeaderText="Code" NullDisplayText="#" />
                                    <asp:BoundField DataField="Remarks" HeaderText="Remark" NullDisplayText="#" />
                                    <asp:BoundField DataField="Status_Update_Date" HeaderText="Date" DataFormatString="{0:dd/MMM/yyyy}"
                                        NullDisplayText="#" />
                                    <asp:BoundField DataField="Status_Time" HeaderText="Time" NullDisplayText="#" />
                                    <asp:BoundField DataField="Customer_Name" HeaderText="Cust. Name" NullDisplayText="#" />
                                    <asp:BoundField DataField="Mobile_No" HeaderText="Mobile No" NullDisplayText="#" />
                                    <asp:BoundField DataField="Person_Met_Mobile_No" HeaderText="OTP No" NullDisplayText="#" />
                                    <asp:BoundField DataField="Person_Met_Email" HeaderText="Contact Person Email" NullDisplayText="#" />
                                </Columns>
                            </asp:GridView>
                        </div>

                        <div class="clearfix mt10">
                        </div>

                        <h4 class="text-center">MO</h4>
                        <div class="clearfix">
                        </div>
                        <div style="overflow: auto;">
                            <asp:GridView ID="gvmo" runat="server" class="commonAjaxTbl dataTable master-company-table ViewProductTable table 
                                        display responsive no-wrap table-hover manage-user Grid table-responsive"
                                AutoGenerateColumns="false" OnRowCreated="gvmo_RowCreated" Style="overflow: auto;">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Billed_Ext_ID" HeaderText="Billed Ext ID" NullDisplayText="#" />
                                    <asp:BoundField DataField="Party_Name" HeaderText="Company Name" NullDisplayText="#" />
                                    <asp:BoundField DataField="FE_Name" HeaderText="FE Name" NullDisplayText="#" />
                                    <asp:BoundField DataField="Status_By_Fe" HeaderText="Audit Status" NullDisplayText="#" />
                                    <asp:BoundField DataField="Code" HeaderText="Code" NullDisplayText="#" />
                                    <asp:BoundField DataField="Remarks" HeaderText="Remark" NullDisplayText="#" />
                                    <asp:BoundField DataField="Status_Update_Date" HeaderText="Date" DataFormatString="{0:dd/MMM/yyyy}"
                                        NullDisplayText="#" />
                                    <asp:BoundField DataField="Status_Time" HeaderText="Time" NullDisplayText="#" />
                                    <asp:BoundField DataField="Person_Met_Name" HeaderText="Cust. Name" NullDisplayText="#" />
                                    <asp:BoundField DataField="Coordinator_Contact_Number" HeaderText="Contact No" NullDisplayText="#" />
                                    <asp:BoundField DataField="Person_Met_Mobile_No" HeaderText="OTP No" NullDisplayText="#" />
                                    <asp:BoundField DataField="Person_Met_Email" HeaderText="Contact Person Email" NullDisplayText="#" />
                                </Columns>
                            </asp:GridView>

                        </div>
                        <div class="clearfix mt10">
                        </div>

                        <h4 class="text-center">PRI-Fixed Line</h4>
                        <div class="clearfix">
                        </div>
                        <div style="overflow: auto;">
                            <asp:GridView ID="gvpri" runat="server" class="commonAjaxTbl dataTable master-company-table ViewProductTable table 
                                        display responsive no-wrap table-hover manage-user Grid table-responsive"
                                AutoGenerateColumns="false" OnRowCreated="gvpri_RowCreated" Style="overflow: auto;">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Subs_Del_No" HeaderText="Subs Del No" NullDisplayText="#" />
                                    <asp:BoundField DataField="Party_Name" HeaderText="Company Name" NullDisplayText="#" />
                                    <asp:BoundField DataField="FE_Name" HeaderText="FE Name" NullDisplayText="#" />
                                    <asp:BoundField DataField="Status_By_Fe" HeaderText="Audit Status" NullDisplayText="#" />
                                    <asp:BoundField DataField="Code" HeaderText="Code" NullDisplayText="#" />
                                    <asp:BoundField DataField="Remarks" HeaderText="Remark" NullDisplayText="#" />
                                    <asp:BoundField DataField="Status_Update_Date" HeaderText="Date" DataFormatString="{0:dd/MMM/yyyy}"
                                        NullDisplayText="#" />
                                    <asp:BoundField DataField="Status_Time" HeaderText="Time" NullDisplayText="#" />
                                    <asp:BoundField DataField="Customer_Name" HeaderText="Cust. Name" NullDisplayText="#" />
                                    <asp:BoundField DataField="Coordinator_Contact_Number" HeaderText="Contact No" NullDisplayText="#" />
                                    <asp:BoundField DataField="Person_Met_Mobile_No" HeaderText="OTP No" NullDisplayText="#" />
                                    <asp:BoundField DataField="Person_Met_Email" HeaderText="Contact Person Email" NullDisplayText="#" />
                                </Columns>
                            </asp:GridView>

                        </div>
                        <div class="clearfix mt10">
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
