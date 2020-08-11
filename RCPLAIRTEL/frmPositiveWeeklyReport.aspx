<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmPositiveWeeklyReport.aspx.cs" Inherits="frmPositiveWeeklyReport"
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
                                    <asp:Label ID="lblPageName" runat="server" Text="Weekly Positive Case Report"></asp:Label>
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
                        <asp:Button ID="btnmail" runat="server" class="btn btn-danger btn-sm pull-right mr10"
                            Text="Send Mail" OnClick="btnmail_Click" />
                        <div class="clearfix mt10">
                        </div>
                        <h4 class="text-center">Leased Line</h4>
                        <div class="clearfix">
                        </div>
                        <div style="overflow: auto">
                            <asp:GridView ID="gvll" runat="server" class="commonAjaxTbl dataTable master-company-table ViewProductTable table 
                                        display responsive no-wrap table-hover manage-user Grid table-responsive"
                                AutoGenerateColumns="false" OnRowCreated="gvpri_RowCreated">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Party_Name" HeaderText="Company Name" NullDisplayText="#" />
                                    <asp:BoundField DataField="Status_By_Fe" HeaderText="Audit Status" NullDisplayText="#" />
                                    <asp:BoundField DataField="Code" HeaderText="Code" NullDisplayText="#" />
                                    <asp:BoundField DataField="Remarks" HeaderText="Remarks" NullDisplayText="#" />
                                    <asp:BoundField DataField="Status_Update_Date" DataFormatString="{0:dd/MMM/yyyy}" HeaderText="Date"
                                        NullDisplayText="#" />
                                    <asp:BoundField DataField="Status_Time" HeaderText="Time" NullDisplayText="#" />
                                    <asp:BoundField DataField="Person_Met_Name" HeaderText="Cust. Name" NullDisplayText="#" />
                                    <asp:BoundField DataField="Person_Met_Mobile_No" HeaderText="Contact No" NullDisplayText="#" />
                                    <asp:BoundField DataField="Person_Met_Email" HeaderText="Cust Mail Id" NullDisplayText="#" />
                                    <asp:BoundField DataField="Appointment_Mail_Datetime" DataFormatString="{0:dd/MMM/yyyy}"
                                        HeaderText="Appointment Mail Date" NullDisplayText="#" />
                                    <asp:BoundField DataField="Cust_Sam_Kam_Mail_Datetime" DataFormatString="{0:dd/MMM/yyyy}"
                                        HeaderText="Sam/Sam-TL Mail Date" NullDisplayText="#" />
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div class="clearfix mt10">
                        </div>

                        <h4 class="text-center">MO</h4>
                        <div class="clearfix">
                        </div>
                        <div style="overflow: auto">
                            <asp:GridView ID="gvmo" runat="server" class="commonAjaxTbl dataTable master-company-table ViewProductTable table 
                                        display responsive no-wrap table-hover manage-user Grid table-responsive"
                                AutoGenerateColumns="false" OnRowCreated="gvpri_RowCreated">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Party_Name" HeaderText="Company Name" NullDisplayText="#" />
                                    <asp:BoundField DataField="Status_By_Fe" HeaderText="Audit Status" NullDisplayText="#" />
                                    <asp:BoundField DataField="Code" HeaderText="Code" NullDisplayText="#" />
                                    <asp:BoundField DataField="Remarks" HeaderText="Remarks" NullDisplayText="#" />
                                    <asp:BoundField DataField="Status_Update_Date" DataFormatString="{0:dd/MMM/yyyy}" HeaderText="Date"
                                        NullDisplayText="#" />
                                    <asp:BoundField DataField="Status_Time" HeaderText="Time" NullDisplayText="#" />
                                    <asp:BoundField DataField="Person_Met_Name" HeaderText="Cust. Name" NullDisplayText="#" />
                                    <asp:BoundField DataField="Person_Met_Mobile_No" HeaderText="Contact No" NullDisplayText="#" />
                                    <asp:BoundField DataField="Person_Met_Email" HeaderText="Cust Mail Id" NullDisplayText="#" />
                                    <asp:BoundField DataField="Appointment_Mail_Datetime" DataFormatString="{0:dd/MMM/yyyy}"
                                        HeaderText="Appointment Mail Date" NullDisplayText="#" />
                                    <asp:BoundField DataField="Cust_Sam_Kam_Mail_Datetime" DataFormatString="{0:dd/MMM/yyyy}"
                                        HeaderText="RM Mail Date" NullDisplayText="#" />
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div class="clearfix mt10">
                        </div>

                        <h4 class="text-center">PRI-Fixeds Line</h4>
                        <div class="clearfix">
                        </div>
                        <div style="overflow: auto">
                            <asp:GridView ID="gvpri" runat="server" class="commonAjaxTbl dataTable master-company-table ViewProductTable table 
                                        display responsive no-wrap table-hover manage-user Grid table-responsive"
                                AutoGenerateColumns="false" OnRowCreated="gvpri_RowCreated">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Party_Name" HeaderText="Company Name" NullDisplayText="#" />
                                    <asp:BoundField DataField="Status_By_Fe" HeaderText="Audit Status" NullDisplayText="#" />
                                    <asp:BoundField DataField="Code" HeaderText="Code" NullDisplayText="#" />
                                    <asp:BoundField DataField="Remarks" HeaderText="Remarks" NullDisplayText="#" />
                                    <asp:BoundField DataField="Status_Update_Date" DataFormatString="{0:dd/MMM/yyyy}" HeaderText="Date"
                                        NullDisplayText="#" />
                                    <asp:BoundField DataField="Status_Time" HeaderText="Time" NullDisplayText="#" />
                                    <asp:BoundField DataField="Person_Met_Name" HeaderText="Cust. Name" NullDisplayText="#" />
                                    <asp:BoundField DataField="Person_Met_Mobile_No" HeaderText="Contact No" NullDisplayText="#" />
                                    <asp:BoundField DataField="Person_Met_Email" HeaderText="Cust Mail Id" NullDisplayText="#" />
                                    <asp:BoundField DataField="Appointment_Mail_Datetime" DataFormatString="{0:dd/MMM/yyyy}"
                                        HeaderText="Appointment Mail Date" NullDisplayText="#" />
                                    <asp:BoundField DataField="Cust_Sam_Kam_Mail_Datetime" DataFormatString="{0:dd/MMM/yyyy}"
                                        HeaderText="RM Mail Date" NullDisplayText="#" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
