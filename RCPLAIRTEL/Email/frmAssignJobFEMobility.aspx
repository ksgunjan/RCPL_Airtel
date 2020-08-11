<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmAssignJobFEMobility.aspx.cs" Inherits="frmAssignJobFEMobility"
    MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="conhead" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="contentmain" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="content oem-content">
        <asp:UpdatePanel runat="server" ID="updatepan" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="sideBg">
                    <div class="row">
                        <div class="col-md-12 padding_0">
                            <ul class="breadcrumb">
                                <li>
                                    <asp:Label ID="lblPageName" runat="server" Text="Assign Job FE Mobility"></asp:Label>
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
                        <div class="row">
                            <div class="col-sm-6">
                                <div>
                                    <span class="label label-info">Today's Prescheduled Appointments:- ||</span>
                                    <div class="label label-primary pull-right" runat="server" id="totalcount">
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6 ">
                                <div class="row">
                                    <div class="col-md-7" style="margin-top: 9px;">
                                        Select Audit Case (Category Wise)
                                    </div>
                                    <div class="col-md-5">
                                        <asp:DropDownList ID="ddlprodbycat" runat="server" AutoPostBack="true" class="form-control"
                                            OnSelectedIndexChanged="ddlprodbycat_SelectedIndexChanged">
                                            <asp:ListItem Selected="True" Value="0">All</asp:ListItem>
                                            <asp:ListItem Value="MO-Periodic">MO-Periodic</asp:ListItem>
                                            <asp:ListItem Value="MO-Quarterly">MO-Quarterly</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>


                            </div>
                        </div>

                        <div class="clearfix">
                        </div>
                        <asp:HiddenField ID="hfFeName" runat="server" />
                        <asp:HiddenField ID="hfbilledextid1" runat="server" />
                        <asp:HiddenField ID="hfotp" runat="server" />
                        <asp:HiddenField ID="hfcustomerorpartyname" runat="server" />
                        <asp:HiddenField ID="hfAllocationDate" runat="server" />
                        <asp:DataList ID="dlcomp" runat="server" class="" OnItemCommand="dlcomp_ItemCommand"
                            OnItemDataBound="dlcomp_ItemDataBound" RepeatColumns="1" RepeatDirection="Horizontal"
                            RepeatLayout="Flow">
                            <ItemTemplate>
                                <h4 style="display: inline-block">
                                    <asp:Label ID="lblcmpaname" runat="server" Style="margin-right: 5px" class="label label-primary pull-left ml5"
                                        Text='<%#Eval("Party_Name") %>'></asp:Label>

                                    <asp:Label ID="hfallocateddate" runat="server" Visible="false" Text='<%#Eval("Allocated_Date") %>' />
                                    <div class="label-primary" style="display: flex; align-items: center; padding: 0 0 0 5px;">
                                        <asp:LinkButton ID="lbviewnubmer" runat="server" class="fa fa-eye pull-right" CommandArgument='<%#Eval("Party_Name") %>'
                                            CommandName="Viewnum"></asp:LinkButton>
                                        <asp:Label ID="lbltotal" runat="server" class="label label-info pull-right mr5" Text='<%#Eval("Count") %>'></asp:Label>
                                    </div>
                                </h4>
                                <div class="clearfix">
                                </div>
                                <div class="table-responsive" style="margin-top: -5px; overflow: auto;">
                                    <asp:GridView ID="gvexcel" runat="server" AutoGenerateColumns="false" class="commonAjaxTbl dataTable master-company-table ViewProductTable table 
                                        display responsive no-wrap table-hover manage-user Grid table-responsive"
                                        OnRowCommand="gvexcel_RowCommand" OnRowDataBound="gvexcel_RowDataBound" OnRowCreated="gvexcel_RowCreated">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblview" runat="server" class="fa fa-eye" CommandArgument='<%#Eval("ExcelRawID") %>'
                                                        CommandName="ViewDetail"></asp:LinkButton>
                                                    <asp:HiddenField ID="hfbilledextid" runat="server" Value='<%#Eval("Billed_Ext_Id") %>' />
                                                    <asp:HiddenField ID="hfstatusfe" runat="server" Value='<%#Eval("Status_By_FE") %>' />
                                                    <asp:HiddenField ID="hfemail" runat="server" Value='<%#Eval("[Cust_Email]") %>' />
                                                    <asp:HiddenField ID="Customer" runat="server" Value='<%#Eval("[Customer_Name]") %>' />
                                                    <asp:HiddenField ID="hfrevisitdate" runat="server" Value='<%#Eval("[Revisit_Date_Time]","{0:dd/MMM/yyyy}") %>' />
                                                    <asp:HiddenField ID="hfPreMeetingDate" runat="server" Value='<%#Eval("Pre_Meeting_Date","{0:dd/MMM/yyyy}") %>' />
                                                    <asp:HiddenField ID="hfGridAllcationDate" runat="server" Value='<%#Eval("Allocated_Date","{0:dd/MMM/yyyy}") %>' />
                                                    <asp:LinkButton ID="lblstatus" runat="server" class="fa fa-edit" CommandArgument='<%#Eval("ExcelRawID") %>'
                                                        CommandName="UpdateStatus"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Visit Company Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcompnamemo" runat="server" Text='<%# Eval("Party_Name").ToString() == "" ? "#" : Eval("Party_Name").ToString()%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Billed_Ext_Id" HeaderText="Billed Ext Id" NullDisplayText="#" />
                                            <asp:BoundField DataField="Billing_Address" HeaderText="Customer Address-1" NullDisplayText="#" />
                                            <asp:BoundField DataField="PRODUCT_TYPE" HeaderText="Product Name" NullDisplayText="#" />
                                            <asp:BoundField DataField="Coordinator_Name" HeaderText="Coordinator Name" NullDisplayText="#" />
                                            <asp:BoundField DataField="Coordinator_Contact_number" HeaderText="Coordinator Number"
                                                NullDisplayText="#" />
                                            <asp:TemplateField HeaderText="Appoinment Mail Date or Email">
                                                <ItemTemplate>
                                                    (<asp:Label ID="lblappointmentmailsenddate" runat="server" Text='<%# Eval("Appointment_Mail_Datetime", "{0:dd/MMM/yyyy}").ToString() == "" ? "#" : Eval("Appointment_Mail_Datetime", "{0:dd/MMM/yyyy}").ToString()%>'></asp:Label>)
                                                    <asp:Label ID="lblemailcustomer" runat="server" Text='<%#Eval("[Cust_Email]") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Compliance Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcomp" runat="server" Text='<%# Eval("FE_Compliance_Date", "{0:dd/MMM/yyyy}").ToString() == "" ? "#" : Eval("FE_Compliance_Date", "{0:dd/MMM/yyyy}").ToString()%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                        <div class="clearfix">
                        </div>
                        <div id="myModal" aria-hidden="true" aria-labelledby="exampleModalLabel" class="modal fade text-left"
                            role="dialog" tabindex="-1" data-backdrop="static" data-keyboard="false">
                            <div class="modal-dialog" role="document">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button aria-label="Close" class="close" data-dismiss="modal" type="button">
                                            <span aria-hidden="true">×</span>
                                        </button>
                                    </div>
                                    <div class="modal-body">
                                        <div id="divshow1" runat="server">
                                            <h3>A brief detail of client information.</h3>
                                            <asp:DataList ID="dluploadexcelrecord" runat="server" RepeatColumns="1" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                <ItemTemplate>
                                                    <div class="col-sm-6">
                                                        <h4>Allocated Date</h4>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <%# Eval("Allocated_Date", "{0:dd/MMM/yyyy}").ToString() == "" ? "#" : Eval("Allocated_Date", "{0:dd/MMM/yyyy}").ToString()%>
                                                    </div>
                                                    <div class="clearfix mt5">
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <h4>Bill Plan</h4>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <%# Eval("Bill_Plan").ToString() == "" ? "#" : Eval("Bill_Plan").ToString()%>
                                                    </div>
                                                    <div class="clearfix mt5">
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <h4>Bill_City</h4>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <%# Eval("Bill_City").ToString() == "" ? "#" : Eval("Bill_City").ToString()%>
                                                    </div>
                                                    <div class="clearfix mt5">
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <h4>Billed Ext Id</h4>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <%# Eval("Billed_Ext_Id").ToString() == "" ? "#" : Eval("Billed_Ext_Id").ToString()%>
                                                    </div>
                                                    <div class="clearfix mt5">
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <h4>Billing Address</h4>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <%# Eval("Billing_Address").ToString() == "" ? "#" : Eval("Billing_Address").ToString()%>
                                                    </div>
                                                    <div class="clearfix mt5">
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <h4>Complaince Date</h4>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <%# Eval("FE_Compliance_Date", "{0:dd/MMM/yyyy}").ToString() == "" ? "#" : Eval("FE_Compliance_Date", "{0:dd/MMM/yyyy}").ToString()%>
                                                    </div>
                                                    <div class="clearfix mt5">
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <h4>Company Name</h4>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <%# Eval("Company_Name").ToString() == "" ? "#" : Eval("Company_Name").ToString()%>
                                                    </div>
                                                    <div class="clearfix mt5">
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <h4>Coordinator_Contact_Email</h4>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <%# Eval("Coordinator_Contact_Email").ToString() == "" ? "#" : Eval("Coordinator_Contact_Email").ToString()%>
                                                    </div>
                                                    <div class="clearfix mt5">
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <h4>Coordinator_Contact_Number</h4>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <%# Eval("Coordinator_Contact_Number").ToString() == "" ? "#" : Eval("Coordinator_Contact_Number").ToString()%>
                                                    </div>
                                                    <div class="clearfix mt5">
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <h4>Coordinator_Name</h4>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <%# Eval("Coordinator_Name").ToString() == "" ? "#" : Eval("Coordinator_Name").ToString()%>
                                                    </div>
                                                    <div class="clearfix mt5">
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <h4>Case Eligible/Not Eligible</h4>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <%# Eval("Eligible_Not_Eligible").ToString() == "" ? "#" : Eval("Eligible_Not_Eligible").ToString()%>
                                                    </div>
                                                    <div class="clearfix mt5">
                                                    </div>

                                                    <div class="col-sm-6">
                                                        <h4>Mobile No</h4>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <%# Eval("Mobile_No").ToString() == "" ? "#" : Eval("Mobile_No").ToString()%>
                                                    </div>
                                                    <div class="clearfix mt5">
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <h4>Visit Company Name</h4>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <%# Eval("Party_Name").ToString() == "" ? "#" : Eval("Party_Name").ToString()%>
                                                    </div>

                                                    <div class="clearfix mt5">
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <h4>Product Type by Excel</h4>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <%# Eval("PRODUCT_TYPE").ToString() == "" ? "#" : Eval("PRODUCT_TYPE").ToString()%>
                                                    </div>
                                                    <div class="clearfix mt5">
                                                    </div>

                                                    <div class="col-sm-6">
                                                        <h4>Product Name as Master Entry</h4>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <%# Eval("Product_Name").ToString() == "" ? "#" : Eval("Product_Name").ToString()%>
                                                    </div>
                                                    <div class="clearfix mt5">
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <h4>Prod Type Name as Master Entry</h4>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <%# Eval("Prod_Type_Name").ToString() == "" ? "#" : Eval("Prod_Type_Name").ToString()%>
                                                    </div>
                                                    <div class="clearfix mt5">
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <h4>RM Contact Number</h4>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <%# Eval("RM_Contact_Number").ToString() == "" ? "#" : Eval("RM_Contact_Number").ToString()%>
                                                    </div>
                                                    <div class="clearfix mt5">
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <h4>RM Email</h4>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <%# Eval("RM_Email").ToString() == "" ? "#" : Eval("RM_Email").ToString()%>
                                                    </div>
                                                    <div class="clearfix mt5">
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <h4>RM NAME</h4>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <%# Eval("RM_NAME").ToString() == "" ? "#" : Eval("RM_NAME").ToString()%>
                                                    </div>

                                                    <div class="clearfix mt5">
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <h4>SAM Contact Number</h4>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <%# Eval("SAM_Contact_Number").ToString() == "" ? "#" : Eval("SAM_Contact_Number").ToString()%>
                                                    </div>
                                                    <div class="clearfix mt5">
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <h4>SAM Email</h4>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <%# Eval("SAM_Email").ToString() == "" ? "#" : Eval("SAM_Email").ToString()%>
                                                    </div>
                                                    <div class="clearfix mt5">
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <h4>SAM Name</h4>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <%# Eval("SAM_Name").ToString() == "" ? "#" : Eval("SAM_Name").ToString()%>
                                                    </div>
                                                    <div class="clearfix mt5">
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <h4>SAM TL</h4>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <%# Eval("SAM_TL").ToString() == "" ? "#" : Eval("SAM_TL").ToString()%>
                                                    </div>
                                                    <div class="clearfix mt5">
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <h4>SAM TL Contact Number</h4>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <%# Eval("SAM_TL_Contact_Number").ToString() == "" ? "#" : Eval("SAM_TL_Contact_Number").ToString()%>
                                                    </div>
                                                    <div class="clearfix mt5">
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <h4>SAM TL EMAIL</h4>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <%# Eval("SAM_TL_EMAIL").ToString() == "" ? "#" : Eval("SAM_TL_EMAIL").ToString()%>
                                                    </div>

                                                    <div class="clearfix mt5">
                                                    </div>
                                                </ItemTemplate>
                                            </asp:DataList>
                                        </div>
                                        <div id="div2" runat="server">
                                            <asp:UpdatePanel ID="upanel" runat="server" ChildrenAsTriggers="true">
                                                <ContentTemplate>
                                                    <div class="clearfix mt5">
                                                    </div>
                                                    <div id="divmssgpopup" runat="server">
                                                    </div>
                                                    <div class="clearfix mt5">
                                                    </div>
                                                    <asp:Panel ID="panmoallpanel" runat="server" Visible="false">
                                                        <div>
                                                            <asp:HiddenField ID="hflocsrid" runat="server" />
                                                            <asp:HiddenField ID="hfEmailCustomer" runat="server" />
                                                            <%--<asp:HiddenField ID="hfNameCustomer" runat="server" />--%>
                                                            <asp:HiddenField ID="hfcompname" runat="server" />
                                                            <p class="pull-right">
                                                                <strong>Mark with * field fill mandatory</strong>
                                                            </p>
                                                            <div class="clearfix mt5">
                                                            </div>
                                                            <div class="form-group row">
                                                                <label class="col-sm-3 form-control-label">
                                                                    Select Audit Case *
                                                                </label>
                                                                <div class="col-sm-9">
                                                                    <asp:DropDownList ID="ddlcondition" runat="server" AutoPostBack="true" class="form form-control"
                                                                        OnSelectedIndexChanged="ddlcondition_SelectedIndexChanged">
                                                                        <asp:ListItem Selected="True" Value="01">Select audit case</asp:ListItem>
                                                                        <asp:ListItem Value="02">Customer Shifted</asp:ListItem>
                                                                        <asp:ListItem Value="03">Company Closed</asp:ListItem>
                                                                        <asp:ListItem Value="12">Address Not Found</asp:ListItem>
                                                                        <asp:ListItem Value="04">Connection Disconnected Voluntarily</asp:ListItem>
                                                                        <asp:ListItem Value="05">Entry Not Allowed</asp:ListItem>
                                                                        <asp:ListItem Value="06">Customer Denied Audit</asp:ListItem>
                                                                        <asp:ListItem Value="07">Not in Use</asp:ListItem>
                                                                        <asp:ListItem Value="08">Already Done</asp:ListItem>
                                                                        <asp:ListItem Value="09">Negative RM Approval</asp:ListItem>
                                                                        <asp:ListItem Value="10">Revisit Required</asp:ListItem>
                                                                        <asp:ListItem Value="11">None of the Above</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="clearfix mt10">
                                                            </div>
                                                            <asp:Panel ID="pannegativecondition" runat="server" Visible="false">
                                                                <asp:Panel ID="pannegativefirststep" runat="server">
                                                                    <div id="diventrynotallowd" runat="server" visible="false">
                                                                        <div class="form-group row">
                                                                            <label class="col-sm-3 form-control-label">
                                                                                Call to Coordinator *
                                                                            </label>
                                                                            <div class="col-sm-9">
                                                                                <asp:DropDownList ID="ddltlper" runat="server" class="form-control">
                                                                                    <asp:ListItem Value="01">Select Call to Coordinator</asp:ListItem>
                                                                                    <asp:ListItem Value="02">Yes</asp:ListItem>
                                                                                    <asp:ListItem Value="03">No</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group row">
                                                                            <label class="col-sm-3 form-control-label">
                                                                                Call to RM *
                                                                            </label>
                                                                            <div class="col-sm-9">
                                                                                <asp:DropDownList ID="ddlsamper" runat="server" class="form-control">
                                                                                    <asp:ListItem Value="01">Select Call to RM</asp:ListItem>
                                                                                    <asp:ListItem Value="02">Yes</asp:ListItem>
                                                                                    <asp:ListItem Value="03">No</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group row">
                                                                            <label class="col-sm-3 form-control-label">
                                                                                Call to Airtel Process Owner Rcpl *
                                                                            </label>
                                                                            <div class="col-sm-9">
                                                                                <asp:DropDownList ID="ddlairper" runat="server" class="form-control">
                                                                                    <asp:ListItem Value="01">Select Call to Airtel Process Owner Rcpl</asp:ListItem>
                                                                                    <asp:ListItem Value="02">Yes</asp:ListItem>
                                                                                    <asp:ListItem Value="03">No</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group row">
                                                                            <label class="col-sm-3 form-control-label">
                                                                                Call to Airtel Manager *
                                                                            </label>
                                                                            <div class="col-sm-9">
                                                                                <asp:DropDownList ID="ddlairmana" runat="server" class="form-control">
                                                                                    <asp:ListItem Value="01">Select Call to Airtel Manager</asp:ListItem>
                                                                                    <asp:ListItem Value="02">Yes</asp:ListItem>
                                                                                    <asp:ListItem Value="03">No</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div id="divcustomershifted" runat="server" class="form-group row" visible="false">
                                                                        <label class="col-sm-3 form-control-label">
                                                                            Enter Address *
                                                                        </label>
                                                                        <div class="col-sm-9">
                                                                            <asp:TextBox ID="txtnewaddress" runat="server" class="form-control" Height="70px"
                                                                                placeholder="Enter Address" TextMode="MultiLine"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div id="divnegativeotp" runat="server">
                                                                        <div runat="server" class="form-group row">
                                                                            <label class="col-sm-3 form-control-label">
                                                                                Enter Remarks *
                                                                            </label>
                                                                            <div class="col-sm-9">
                                                                                <asp:TextBox ID="txtremarkcust" runat="server" class="form-control" Height="70px"
                                                                                    placeholder="Enter Remarks" TextMode="MultiLine"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group row">
                                                                            <label class="col-sm-3 form-control-label">
                                                                                Person Met Name
                                                                            </label>
                                                                            <div class="col-sm-9">
                                                                                <asp:TextBox ID="txtpersonmetname" runat="server" class="form-control" placeholder="Person Met Name"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group row">
                                                                            <label class="col-sm-3 form-control-label">
                                                                                Person Met Designation
                                                                            </label>
                                                                            <div class="col-sm-9">
                                                                                <asp:TextBox ID="txtpersonmetdesig" runat="server" class="form-control" placeholder="Person Met Designation"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group row">
                                                                            <label class="col-sm-3 form-control-label">
                                                                                Person Met Email-ID
                                                                            </label>
                                                                            <div class="col-sm-9">
                                                                                <asp:TextBox ID="txtpersonmetemailid" runat="server" class="form-control" placeholder="Person Met Email-ID"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group row">
                                                                            <label class="col-sm-3 form-control-label">
                                                                                Person Met Mobile-No
                                                                            </label>
                                                                            <div class="col-sm-9">
                                                                                <asp:TextBox ID="txtpersonmetmobno" runat="server" class="form-control" placeholder="Person Met Mobile-No"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="clearfix mt5">
                                                                    </div>
                                                                    <asp:LinkButton ID="btnnextnegative" runat="server" class="btn btn-primary btn-sm pull-right mr5"
                                                                        OnClick="btnnextnegative_Click" Text="Next"></asp:LinkButton>
                                                                    <div class="clearfix mt5">
                                                                    </div>
                                                                </asp:Panel>
                                                                <asp:Panel ID="pannegativesecondstep" runat="server" Visible="false">
                                                                    <div class="form-group row">
                                                                        <label class="col-sm-3 form-control-label">
                                                                            <h4>Select Mobile</h4>
                                                                        </label>
                                                                        <div class="col-sm-9">
                                                                            <div class="col-sm-1">
                                                                                <asp:CheckBox ID="chkotpshowddl" runat="server" AutoPostBack="true" Checked="false"
                                                                                    OnCheckedChanged="chkotpshowddl_CheckedChanged" />
                                                                            </div>
                                                                            <div class="col-sm-8">
                                                                                <asp:DropDownList ID="ddlnum" runat="server" class="form-control" Enabled="false">
                                                                                    <asp:ListItem Value="OTP">Select OTP Contact</asp:ListItem>
                                                                                    <asp:ListItem Value="Rahul">7053919406</asp:ListItem>
                                                                                    <asp:ListItem Value="Soni">9711616720</asp:ListItem>
                                                                                    <asp:ListItem Value="Gagan">9811020978</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                            <div class="col-sm-3">
                                                                                <asp:Button ID="sendotp" runat="server" Class="btn btn-sm btn-primary" Enabled="false"
                                                                                    OnClick="sendotp_Click" Text="Send OTP" />
                                                                                <div class="clearfix mt5">
                                                                                </div>
                                                                                <asp:LinkButton ID="lblresendotpnegative" runat="server" class="links pull-right mr5"
                                                                                    Enabled="false" OnClick="lblresendotpnegative_Click">Resend OTP</asp:LinkButton>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group row">
                                                                        <label class="col-sm-3 form-control-label">
                                                                            <h4>Enter OTP *</h4>
                                                                        </label>
                                                                        <div class="col-sm-9">
                                                                            <asp:TextBox ID="txtotpnegative" runat="server" class="form form-control" placeholder="Enter OTP"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="clearfix mt5">
                                                                    </div>
                                                                    <asp:Button ID="btnsubmitnegative" runat="server" class="btn btn-success btn-sm pull-right mr5"
                                                                        OnClick="btnsubmitnegative_Click" Text="Submit Case" />
                                                                    <div class="clearfix mt5">
                                                                    </div>
                                                                </asp:Panel>
                                                            </asp:Panel>
                                                            <asp:Panel runat="server" ID="panaddressorcompclose" Visible="false">
                                                                <div class="form-group row">
                                                                    <label class="col-sm-3 form-control-label">
                                                                        Enter Remarks *
                                                                    </label>
                                                                    <div class="col-sm-9">
                                                                        <asp:TextBox ID="txtremarksaddressnotfoundorcompclose" runat="server" TextMode="MultiLine"
                                                                            Height="100px" class="form-control" placeholder="Remearks"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group row">
                                                                    <asp:LinkButton ID="btnsubremarksaddressnotfoundorcompclose" runat="server" class="btn btn-primary pull-right mr10"
                                                                        Text="Submit" OnClick="btnsubremarksaddressnotfoundorcompclose_Click"></asp:LinkButton>
                                                                </div>
                                                            </asp:Panel>
                                                            <asp:Panel ID="panrevisitcondition" runat="server" Visible="false">
                                                                <div id="revisittimedate" runat="server">
                                                                    <div class="form-group row">
                                                                        <label class="col-sm-3 form-control-label">
                                                                            On Which Date And Time Revisit Is Required *
                                                                        </label>
                                                                        <div class="col-sm-9">
                                                                            <asp:TextBox ID="txtrevisittimeanddate" runat="server" class="form-control" reuqired=""
                                                                                type="date"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group row">
                                                                        <label class="col-sm-3 form-control-label">
                                                                            On Which Time Revisit Is Required *
                                                                        </label>
                                                                        <div class="col-sm-9">
                                                                            <asp:TextBox ID="txttimerevisit" runat="server" class="form-control" placeholder="On Which Time Revisit Is Required"
                                                                                reuqired="" type="time"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group row">
                                                                        <label class="col-sm-3 form-control-label">
                                                                            Person Met Name
                                                                        </label>
                                                                        <div class="col-sm-9">
                                                                            <asp:TextBox ID="txtrevisitpersonmetname" runat="server" class="form-control" placeholder="Person Met Name"
                                                                                reuqired=""></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group row">
                                                                        <label class="col-sm-3 form-control-label">
                                                                            Person Met Email-ID
                                                                        </label>
                                                                        <div class="col-sm-9">
                                                                            <asp:TextBox ID="txtrevisitpersonmetemail" runat="server" class="form-control" placeholder="Person Met Email-ID"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group row">
                                                                        <label class="col-sm-3 form-control-label">
                                                                            Person Met Designation
                                                                        </label>
                                                                        <div class="col-sm-9">
                                                                            <asp:TextBox ID="txtrevisitpersonmetdesig" runat="server" class="form-control" placeholder="Person Met Designation"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group row">
                                                                        <label class="col-sm-3 form-control-label">
                                                                            Person Met Mobile No
                                                                        </label>
                                                                        <div class="col-sm-9">
                                                                            <asp:TextBox ID="txtrevisitpersonmobileno" runat="server" class="form-control" MaxLength="10"
                                                                                Placeholder="Mobile No" reuqired=""></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="clearfix mt10">
                                                                    </div>
                                                                    <asp:Button ID="btngetreviitotp" runat="server" class="btn btn-primary pull-right mr5"
                                                                        OnClick="btngetreviitotp_Click" Text="Next" />
                                                                    <div class="clearfix mt10">
                                                                    </div>
                                                                </div>
                                                                <div id="DivOTPRevisit" runat="server" visible="false">
                                                                    <label class="col-sm-3 form-control-label">
                                                                        <h4>Enter OTP *</h4>
                                                                    </label>
                                                                    <div class="col-sm-9">
                                                                        <asp:TextBox ID="txtotprevisitcustmer" runat="server" class="form-control" Placeholer="Enter Six Digit OTP"
                                                                            reuqired=""></asp:TextBox>
                                                                    </div>
                                                                    <div class="clearfix mt10">
                                                                    </div>
                                                                    <asp:Button ID="btnsubmitrevisitstatus" runat="server" class="btn btn-primary pull-right mr5"
                                                                        OnClick="btnsubmitrevisitstatus_Click" Text="Submit" />
                                                                    <div class="clearfix mt10">
                                                                    </div>
                                                                    <asp:LinkButton ID="lbresendotprevisit" runat="server" class="blue pull-right mr5"
                                                                        OnClick="lbresendotprevisit_Click">Resend OTP</asp:LinkButton>
                                                                </div>
                                                            </asp:Panel>
                                                            <asp:Panel ID="pansuccess" runat="server" Visible="false">
                                                                <div id="divsuccessfirst" runat="server">
                                                                    <div class="form-group row">
                                                                        <label class="col-sm-12 form-control-label text-info">
                                                                            <h4 id="totalnumbercount" runat="server"></h4>
                                                                        </label>
                                                                    </div>
                                                                    <div class="form-group row">
                                                                        <label class="col-sm-3 form-control-label">
                                                                            <h4>Total Active Number *</h4>
                                                                        </label>
                                                                        <div class="col-sm-9">
                                                                            <asp:TextBox ID="txtactivenuber" runat="server" class="form-control" placeholder="Total Active Number"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group row">
                                                                        <label class="col-sm-3 form-control-label">
                                                                            <h4>Total Deactive Number *</h4>
                                                                        </label>
                                                                        <div class="col-sm-9">
                                                                            <asp:TextBox ID="txtdeactivenumber" runat="server" class="form-control" placeholder="Total Deactive Number"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group row">
                                                                        <label class="col-sm-3 form-control-label">
                                                                            <h4>Total New Number *</h4>
                                                                        </label>
                                                                        <div class="col-sm-9">
                                                                            <asp:TextBox ID="txtnewnumber" runat="server" class="form-control" placeholder="Total New Number"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group row">
                                                                        <label class="col-sm-3 form-control-label">
                                                                            <h4>Person Met Name</h4>
                                                                        </label>
                                                                        <div class="col-sm-9">
                                                                            <asp:TextBox ID="txtpersonmetnamesuccess" runat="server" class="form-control" placeholder="Person Met Name"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group row">
                                                                        <label class="col-sm-3 form-control-label">
                                                                            <h4>Person Met Email-ID</h4>
                                                                        </label>
                                                                        <div class="col-sm-9">
                                                                            <asp:TextBox ID="txtpersonmetemailidsuccess" runat="server" class="form-control"
                                                                                placeholder="Person Met Email-ID"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group row">
                                                                        <label class="col-sm-3 form-control-label">
                                                                            <h4>Person Met Designation</h4>
                                                                        </label>
                                                                        <div class="col-sm-9">
                                                                            <asp:TextBox ID="txtsuccpersonmetdesig" runat="server" class="form-control" placeholder="Person Met Designation"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group row">
                                                                        <label class="col-sm-3 form-control-label">
                                                                            <h4>Person Met Mobile-No</h4>
                                                                        </label>
                                                                        <div class="col-sm-9">
                                                                            <asp:TextBox ID="txtpersonemetmobilenosuccess" runat="server" class="form-control"
                                                                                placeholder="Person Met mobile-No"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="clearfix mt5">
                                                                    </div>
                                                                    <asp:Button ID="btnsuccessotpstep1" runat="server" class="btn btn-primary pull-right mr5"
                                                                        OnClick="btnsuccessotpstep1_Click" Text="Next" />
                                                                    <div class="clearfix mt5">
                                                                    </div>
                                                                </div>
                                                                <div id="divsuccessoptorsubmit" runat="server" visible="false">
                                                                    <div class="form-group row">
                                                                        <label class="col-sm-3 form-control-label">
                                                                            <h4>Select Mobile</h4>
                                                                        </label>
                                                                        <div class="col-sm-9">
                                                                            <div class="col-sm-2">
                                                                                <asp:CheckBox ID="chkboxposi" runat="server" AutoPostBack="true" Checked="false"
                                                                                    OnCheckedChanged="chkboxposi_CheckedChanged" />
                                                                            </div>
                                                                            <div class="col-sm-7">
                                                                                <asp:DropDownList ID="ddlnump" runat="server" class="form-control" Enabled="false">
                                                                                    <asp:ListItem Value="OTP">Select OTP Contact</asp:ListItem>
                                                                                    <asp:ListItem Value="Rahul">7053919406</asp:ListItem>
                                                                                    <asp:ListItem Value="Soni">9711616720</asp:ListItem>
                                                                                    <asp:ListItem Value="Gagan">9811020978</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                            <div class="col-sm-3">
                                                                                <asp:Button ID="sendotps" runat="server" Class="btn btn-sm btn-primary" Enabled="false"
                                                                                    OnClick="sendotps_Click" Text="Send OTP" />
                                                                                <div class="clearfix mt5">
                                                                                </div>
                                                                                <asp:LinkButton ID="lblresendotppositive" runat="server" class="links pull-right mr5"
                                                                                    Enabled="false" OnClick="lblresendotppositive_Click">Resend OTP</asp:LinkButton>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group row">
                                                                        <label class="col-sm-3 form-control-label">
                                                                            <h4>Enter OTP *</h4>
                                                                        </label>
                                                                        <div class="col-sm-9">
                                                                            <asp:TextBox ID="txtoptsuces" runat="server" class="form-control" MaxLength="6" placeholder="Enter OTP"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="clearfix mt5">
                                                                    </div>
                                                                    <asp:Button ID="btnsuccsubmit" runat="server" class="btn btn-success btn-sm pull-right mr5"
                                                                        OnClick="btnsuccsubmit_Click" Text="Success" />
                                                                    <div class="clearfix mt5">
                                                                    </div>
                                                                </div>
                                                            </asp:Panel>
                                                        </div>
                                                    </asp:Panel>
                                                    <div class="clearfix mt5">
                                                    </div>
                                                    <div class="modal-footer">
                                                        <button class="btn btn-default" data-dismiss="modal" type="button">
                                                            Close
                                                        </button>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div id="panviewnum" runat="server" visible="false">
                                            <h3>A brief detail of client Number.</h3>
                                            <asp:GridView ID="gvnum" runat="server" AutoGenerateColumns="false" class="commonAjaxTbl dataTable master-company-table 
                                                ViewProductTable table display responsive no-wrap table-hover manage-user Grid table-responsive"
                                                Width="100%" OnRowCreated="gvnum_RowCreated">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sr.No">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Mobile_No" HeaderText="Mobile No" />
                                                </Columns>
                                            </asp:GridView>
                                            <div class="clearfix"></div>
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
