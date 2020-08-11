<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RescheduleClientVisit.aspx.cs" Inherits="RescheduleClientVisit"
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
                                    <asp:Label ID="lblPageName" runat="server" Text="Update Rescheduled Client Appointment"></asp:Label>
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
                        <div>
                            1. In Case of Mo Please Enter (Company/Party Name)
                                <div class="clearfix mt10">
                                </div>
                            2. Other Case you can search record by enter Customer/Company/Party Name
                        </div>
                        <div class="clearfix mt10">
                        </div>
                        <div class="col-sm-5">
                            <asp:DropDownList ID="ddlprductname" runat="server" class="form-control">
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-5">
                            <asp:TextBox ID="txtsearch" runat="server" class="form-control" required="" placeholder="Please enter Customer Name or Logical CircuitId/Billed Ext ID"> </asp:TextBox>
                        </div>
                        <div class="col-sm-2">
                            <asp:Button ID="btnsearch" runat="server" class="btn btn-primary btn-block btn-sm"
                                Text="Search Customer" OnClick="btnsearch_Click" />
                        </div>
                        <div class="clearfix mt10">
                        </div>
                        <div>
                            <asp:HiddenField ID="hfmappingID" runat="server" />
                            <div class="table table-responsive">
                                <asp:GridView ID="gvexcel" runat="server" AutoGenerateColumns="false" class="table table-hover"
                                    OnRowCommand="gvexcel_RowCommand" OnRowDataBound="gvexcel_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="Customer_Name" HeaderText="Customer Name" />
                                        <asp:TemplateField HeaderText="Logical/SubDel/Billed No">
                                            <ItemTemplate>
                                                <asp:Label ID="lbluid" runat="server" Text='<%#Eval("UniqueEID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Party_Name" HeaderText="Party Name" />
                                        <asp:BoundField DataField="From_Site" HeaderText="Customer Address-1" />
                                        <asp:BoundField DataField="Product_Name" HeaderText="Product Name" />
                                        <asp:TemplateField HeaderText="Compliance Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcomp" runat="server" Text='<%#Eval("Complaince_Date","{0:dd/MMM/yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Update Status">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lblstatus" runat="server" class="fa fa-edit fa-2x" CommandArgument='<%#Eval("ExcelRawID") %>'
                                                    CommandName="UpdateStatus"></asp:LinkButton>
                                                <asp:HiddenField ID="hfstatusfe" runat="server" Value='<%#Eval("Status_By_FE") %>' />
                                                <asp:HiddenField ID="hfemail" runat="server" Value='<%#Eval("Cust_EMail") %>' />
                                                <asp:HiddenField ID="Customer" runat="server" Value='<%#Eval("Customer_Name") %>' />
                                                <asp:HiddenField ID="hfrevisitdate" runat="server" Value='<%#Eval("Revisit_Date_Time") %>' />
                                                <asp:HiddenField ID="hfpartyname" runat="server" Value='<%#Eval("Party_Name") %>' />
                                                <asp:HiddenField ID="hflaststatusupdatedate" runat="server" Value='<%#Eval("Status_Update_Date") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
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
                                            <asp:UpdatePanel ID="upanel" runat="server" ChildrenAsTriggers="true">
                                                <ContentTemplate>
                                                    <div class="clearfix pb5">
                                                    </div>
                                                    <div id="divmssgpopup" runat="server">
                                                    </div>
                                                    <div class="clearfix pb5">
                                                    </div>
                                                    <asp:HiddenField ID="hflocsrid" runat="server" />
                                                    <asp:HiddenField ID="hfEmailCustomer" runat="server" />
                                                    <asp:HiddenField ID="hfNameCustomer" runat="server" />
                                                    <asp:HiddenField ID="hfPartyNameDisplay" runat="server" />
                                                    <asp:HiddenField ID="hfupdatedate" runat="server" />
                                                    <div id="divupdatebyfe" runat="server">
                                                        <h3>Update Status as per client response.</h3>
                                                        <div>
                                                            <p>
                                                                Select FE to assign reschedule appointment
                                                            </p>
                                                            <div class="form-group row">
                                                                <label class="col-sm-3 form-control-label">
                                                                    Select Field Executive
                                                                </label>
                                                                <div class="col-sm-9">
                                                                    <asp:DropDownList ID="ddlselectfe" runat="server" class="form-control">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="form-group row">
                                                                <label class="col-sm-3 form-control-label">
                                                                    AppointMent Date
                                                                </label>
                                                                <div class="col-sm-9">
                                                                    <asp:TextBox ID="txtdate" runat="server" class="form-control" type="Date"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group row">
                                                                <label class="col-sm-3 form-control-label">
                                                                    AppointMent Time
                                                                </label>
                                                                <div class="col-sm-9">
                                                                    <asp:TextBox ID="txttime" runat="server" class="form-control" type="time"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group row">
                                                                <asp:LinkButton ID="btnupdateprescheduledtimedate" runat="server" class="btn btn-primary pull-right btn-sm"
                                                                    Text="Update Preschedule AppointMent to FE" OnClick="btnupdateprescheduledtimedate_Click"></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                        <div class="Clearfix pb5">
                                                        </div>
                                                        <div class="modal-footer">
                                                            <button class="btn btn-default" data-dismiss="modal" type="button">
                                                                Close
                                                            </button>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnsearch" />
                <asp:PostBackTrigger ControlID="btnupdateprescheduledtimedate" />
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
