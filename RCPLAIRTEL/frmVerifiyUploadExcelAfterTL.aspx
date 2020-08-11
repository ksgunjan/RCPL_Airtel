<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmVerifiyUploadExcelAfterTL.aspx.cs" Inherits="frmVerifiyUploadExcelAfterTL"
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
                                    <asp:Label ID="lblPageName" runat="server" Text="Verify Upload Excel 2nd Level"></asp:Label>
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
                        <div class="card-body">
                            <div class="form-horizontal">
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
                                <div class="clearfix mt5">
                                </div>
                                <div runat="server" id="divbutton" visible="false">
                                    <b>
                                        <asp:Label ID="lbltoalrows" runat="server" class="text-info red"></asp:Label></b>
                                    <asp:Button ID="btndisapprove" runat="server" class="btn btn-danger btn-sm pull-right"
                                        Text="DisApprove" OnClick="btndisapprove_Click" />
                                    <asp:LinkButton ID="btnapprove" runat="server" class="btn btn-success btn-sm pull-right"
                                        Text="Approve" OnClick="btnapprove_Click"></asp:LinkButton>
                                </div>
                                <div class="clearfix mt5">
                                </div>
                                <div style="overflow: scroll;">
                                    <asp:GridView ID="gvexcel" runat="server" AutoGenerateColumns="false" class="commonAjaxTbl dataTable master-company-table ViewProductTable table 
                                        display responsive no-wrap table-hover manage-user Grid table-responsive"
                                        OnRowCommand="gvexcel_RowCommand" OnRowCreated="gvexcel_RowCreated">
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
                                            <asp:BoundField DataField="UniqueID" HeaderText="Logical Circuit Id" NullDisplayText="#" />
                                            <%--<asp:BoundField DataField="Customer_Name" HeaderText="Customer Name" NullDisplayText="#" />--%>
                                            <%--<asp:BoundField DataField="BILLING_BANDWIDTH" HeaderText="BILLING BANDWIDTH" NullDisplayText="#" />--%>
                                            <asp:TemplateField HeaderText="Product Name">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblproduct" Text='<%#Eval("Product_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Prod_Type_Name" HeaderText="PRODUCT TYPE" NullDisplayText="#" />
                                            <asp:TemplateField HeaderText="FE Name">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblfename" Text='<%#Eval("FE_NAME") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Eligible Status">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblfenameqw" Text='<%#Eval("Eligible_Not_Eligible") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="View Detail">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblview" runat="server" class="fa fa-eye fa-2x" CommandArgument='<%#Eval("ExcelRawID") %>'
                                                        CommandName="ViewDetail"></asp:LinkButton>
                                                    <asp:HiddenField ID="hdnIDGridRawData" runat="server" Value='<%#Eval("UniqueID") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <div class="clearfix line">
                                    </div>
                                </div>
                                <div class="clearfix mt5">
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
                                                <div runat="server" id="divshowdetail" visible="false">
                                                    <h3>A brief detail of client information.</h3>
                                                    <form>
                                                        <asp:DataList ID="dluploadexcelrecord" runat="server" RepeatColumns="1" RepeatDirection="Horizontal"
                                                            RepeatLayout="Flow">
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
                                                                    <h4>FE Compliance Date</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("FE_Compliance_Date", "{0:dd/MMM/yyyy}").ToString() == "" ? "#" : Eval("FE_Compliance_Date", "{0:dd/MMM/yyyy}").ToString()%>
                                                                </div>
                                                                <div class="clearfix mt5">
                                                                </div>

                                                                <div class="clearfix mt5">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>Account Manager
                                                                    </h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Account_Manager").ToString() == "" ? "#" : Eval("Account_Manager").ToString()%>
                                                                </div>
                                                                <div class="clearfix mt5">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>Account Number
                                                                    </h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Account_Number").ToString() == "" ? "#" : Eval("Account_Number").ToString()%>
                                                                </div>
                                                                <div class="clearfix mt5">
                                                                </div>


                                                                <div class="col-sm-6">
                                                                    <h4>B2B Email</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("B2B_Email").ToString() == "" ? "#" : Eval("B2B_Email").ToString()%>
                                                                </div>
                                                                <div class="clearfix mt5">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>B2B Head Email</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("B2B_Head_Email").ToString() == "" ? "#" : Eval("B2B_Head_Email").ToString()%>
                                                                </div>
                                                                <div class="clearfix mt5">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>B2B Head Name</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("B2B_Head_Name").ToString() == "" ? "#" : Eval("B2B_Head_Name").ToString()%>
                                                                </div>
                                                                <div class="clearfix mt5">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>Bill Company</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Bill_Company").ToString() == "" ? "#" : Eval("Bill_Company").ToString()%>
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
                                                                    <h4>Billed Act Id</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Billed_Act_Id").ToString() == "" ? "#" : Eval("Billed_Act_Id").ToString()%>
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
                                                                    <h4>Billing Trig Date</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Billing_Trig_Date", "{0:dd/MMM/yyyy}").ToString() == "" ? "#" : Eval("Billing_Trig_Date", "{0:dd/MMM/yyyy}").ToString()%>
                                                                </div>
                                                                <div class="clearfix mt5">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>BILLING BANDWIDTH</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("BILLING_BANDWIDTH").ToString() == "" ? "#" : Eval("BILLING_BANDWIDTH").ToString()%>
                                                                </div>

                                                                <div class="clearfix mt5">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>Billing Contact Number</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Billing_Contact_Number").ToString() == "" ? "#" : Eval("Billing_Contact_Number").ToString()%>
                                                                </div>
                                                                <div class="clearfix mt5">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>Billing Email Id</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Billing_Email_Id").ToString() == "" ? "#" : Eval("Billing_Email_Id").ToString()%>
                                                                </div>
                                                                <div class="clearfix mt5">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>Complaince Date</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Complaince_Date", "{0:dd/MMM/yyyy}").ToString() == "" ? "#" : Eval("Complaince_Date", "{0:dd/MMM/yyyy}").ToString()%>
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
                                                                    <h4>Contact Phone</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Contact_Phone").ToString() == "" ? "#" : Eval("Contact_Phone").ToString()%>
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
                                                                    <h4>Cust account No</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Cust_account_No").ToString() == "" ? "#" : Eval("Cust_account_No").ToString()%>
                                                                </div>
                                                                <div class="clearfix mt5">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>Cust Email</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Cust_Email").ToString() == "" ? "#" : Eval("Cust_Email").ToString()%>
                                                                </div>
                                                                <div class="clearfix mt5">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>Customer Name</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Customer_Name").ToString() == "" ? "#" : Eval("Customer_Name").ToString()%>
                                                                </div>
                                                                <div class="clearfix mt5">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>Customer Segment</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Customer_Segment").ToString() == "" ? "#" : Eval("Customer_Segment").ToString()%>
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
                                                                    <h4>From Site</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("From_Site").ToString() == "" ? "#" : Eval("From_Site").ToString()%>
                                                                </div>
                                                                <div class="clearfix mt5">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>FE Name</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("FE_Name").ToString() == "" ? "#" : Eval("FE_Name").ToString()%>
                                                                </div>
                                                                <div class="clearfix mt5">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>Document Scanned By TL</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Is_Scanned").ToString() == "" ? "#" : Eval("Is_Scanned").ToString()%>
                                                                </div>
                                                                <div class="clearfix mt5">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>Document Submit Status by TL
                                                                    </h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Is_Submitted").ToString() == "" ? "#" : Eval("Is_Submitted").ToString()%>
                                                                </div>
                                                                <div class="clearfix mt5">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>KAM Contact Number</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("KAM_Contact_Number").ToString() == "" ? "#" : Eval("KAM_Contact_Number").ToString()%>
                                                                </div>
                                                                <div class="clearfix mt5">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>KAM Email</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("KAM_Email").ToString() == "" ? "#" : Eval("KAM_Email").ToString()%>
                                                                </div>
                                                                <div class="clearfix mt5">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>KAM Name</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("KAM_Name").ToString() == "" ? "#" : Eval("KAM_Name").ToString()%>
                                                                </div>
                                                                <div class="clearfix mt5">
                                                                </div>

                                                                <div class="col-sm-6">
                                                                    <h4>Logical Circuit Id</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Logical_Circuit_Id").ToString() == "" ? "#" : Eval("Logical_Circuit_Id").ToString()%>
                                                                </div>
                                                                <div class="clearfix mt5">
                                                                </div>

                                                                <div class="col-sm-6">
                                                                    <h4>Verify Client Mobile OTP
                                                                    </h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Mobile_OTP").ToString() == "" ? "#" : Eval("Mobile_OTP").ToString()%>
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
                                                                    <h4>Sudden Case Pre Meeting Date
                                                                    </h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Pre_Meeting_Date", "{0:dd/MMM/yyyy}").ToString() == "" ? "#" : Eval("Pre_Meeting_Date", "{0:dd/MMM/yyyy}").ToString()%>
                                                                </div>
                                                                <div class="clearfix mt5">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>Sudden Case Pre Meeting Time</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Pre_Meeting_Time", "{0:hh:mm:ss}").ToString() == "" ? "#" : Eval("Pre_Meeting_Time", "{0:hh:mm:ss}").ToString()%>
                                                                </div>
                                                                <div class="clearfix mt5">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>Party Name</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Party_Name").ToString() == "" ? "#" : Eval("Party_Name").ToString()%>
                                                                </div>

                                                                <div class="clearfix mt5">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>Primary Address</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Primary_Address").ToString() == "" ? "#" : Eval("Primary_Address").ToString()%>
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
                                                                    <h4>Positive/Negative</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Positive_Negative").ToString() == "" ? "#" : Eval("Positive_Negative").ToString()%>
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
                                                                    <h4>Case Reason by FE</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Reason_Of_Status").ToString() == "" ? "#" : Eval("Reason_Of_Status").ToString()%>
                                                                </div>
                                                                <div class="clearfix mt5">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>Status Remarks by FE</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Remarks_Of_Status").ToString() == "" ? "#" : Eval("Remarks_Of_Status").ToString()%>
                                                                </div>
                                                                <div class="clearfix mt5">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>Pending Case Revisit PerSon Name</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Revisit_PerSon_Name").ToString() == "" ? "#" : Eval("Revisit_PerSon_Name").ToString()%>
                                                                </div>
                                                                <div class="clearfix mt5">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>Pending Case Revisit Date Time</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Revisit_Date_Time", "{0:dd/MMM/yyyy - hh:mm:ss}").ToString() == "" ? "#" : Eval("Revisit_Date_Time", "{0:dd/MMM/yyyy - hh:mm:ss}").ToString()%>
                                                                </div>
                                                                <div class="clearfix mt5">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>Status By FE</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Status_By_FE").ToString() == "" ? "#" : Eval("Status_By_FE").ToString()%>
                                                                </div>
                                                                <div class="clearfix mt5">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>Case Status Update Time</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Status_Time", "{0:hh:mm:ss}").ToString() == "" ? "#" : Eval("Status_Time", "{0:hh:mm:ss}").ToString()%>
                                                                </div>
                                                                <div class="clearfix mt5">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>Case Status Update Date</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Status_Update_Date", "{0:dd/MMM/yyyy}").ToString() == "" ? "#" : Eval("Status_Update_Date", "{0:dd/MMM/yyyy}").ToString()%>
                                                                </div>
                                                                <div class="clearfix mt5">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>Case Document Scanned Date
                                                                    </h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Scanned_Date", "{0:dd/MMM/yyyy}").ToString() == "" ? "#" : Eval("Scanned_Date", "{0:dd/MMM/yyyy}").ToString()%>
                                                                </div>
                                                                <div class="clearfix mt5">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>Case Submitted Date</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Submitted_Date", "{0:dd/MMM/yyyy}").ToString() == "" ? "#" : Eval("Submitted_Date", "{0:dd/MMM/yyyy}").ToString()%>
                                                                </div>
                                                                <div class="clearfix mt5">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>Send Negative Mail to All</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Send_Negative_Mail_to_All").ToString() == "" ? "#" : Eval("Send_Negative_Mail_to_All").ToString()%>
                                                                </div>
                                                                <div class="clearfix mt5">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>Send Mail to Airtel</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Sent_Mail_Airtel", "{0:==Y ? Yes: NO}").ToString() == "" ? "#" : Eval("Sent_Mail_Airtel", "{0:==Y ? Yes: NO}").ToString()%>
                                                                </div>
                                                                <div class="clearfix mt5">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>Send Mail to SAM</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Sent_Sam_Mail", "{0:==Y ? Yes: NO}").ToString() == "" ? "#" : Eval("Sent_Sam_Mail", "{0:==Y ? Yes: NO}").ToString()%>
                                                                </div>
                                                                <div class="clearfix mt5">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>Sent Mail to Customer</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Sent_Cust_Mail").ToString() == "" ? "#" : Eval("Sent_Cust_Mail").ToString()%>
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

                                                                <div class="col-sm-6">
                                                                    <h4>Segment</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Segment").ToString() == "" ? "#" : Eval("Segment").ToString()%>
                                                                </div>
                                                                <div class="clearfix mt5">
                                                                </div>

                                                                <div class="col-sm-6">
                                                                    <h4>Subs Del No</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Subs_Del_No").ToString() == "" ? "#" : Eval("Subs_Del_No").ToString()%>
                                                                </div>
                                                                <div class="clearfix mt5">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>TL Name</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("TL_Name").ToString() == "" ? "#" : Eval("TL_Name").ToString()%>
                                                                </div>
                                                                <div class="clearfix mt5">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>TL Contact Number</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("TL_Contact_Number").ToString() == "" ? "#" : Eval("TL_Contact_Number").ToString()%>
                                                                </div>
                                                                <div class="clearfix mt5">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>TL Email</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("TL_Email").ToString() == "" ? "#" : Eval("TL_Email").ToString()%>
                                                                </div>
                                                                <div class="clearfix mt5">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>Verification</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Verification").ToString() == "" ? "#" : Eval("Verification").ToString()%>
                                                                </div>
                                                                <div class="clearfix mt5">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>VH Contact Number</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("VH_Contact_Number").ToString() == "" ? "#" : Eval("VH_Contact_Number").ToString()%>
                                                                </div>
                                                                <div class="clearfix mt5">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>VH Email</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("VH_Email").ToString() == "" ? "#" : Eval("VH_Email").ToString()%>
                                                                </div>
                                                                <div class="clearfix mt5">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>VH Name</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("VH_Name").ToString() == "" ? "#" : Eval("VH_Name").ToString()%>
                                                                </div>
                                                                <div class="clearfix mt5">
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <h4>Case Active</h4>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <%# Eval("Is_Active").ToString() == "" ? "#" : Eval("Is_Active").ToString()%>
                                                                </div>
                                                                <div class="clearfix mt5">
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:DataList>
                                                    </form>
                                                </div>
                                                <asp:UpdatePanel ID="upanel" runat="server" ChildrenAsTriggers="true">
                                                    <ContentTemplate>
                                                        <div runat="server" id="divreject" visible="false">
                                                            <asp:Label ID="lbltotalrowselected" runat="server" class="label label-warning block"></asp:Label>
                                                            <div class="clearfix mt10">
                                                            </div>
                                                            <asp:TextBox ID="txtreasone" runat="server" class="form-control" TextMode="MultiLine"
                                                                Height="100px" placeholder="Reason for delete excel"></asp:TextBox>
                                                            <div class="clear mt5">
                                                            </div>
                                                            <asp:Button ID="btndelete" runat="server" class="btn btn-primary pull-right" Text="Delete Selected Excel Record"
                                                                OnClick="btndelete_Click" />
                                                            <div class="clear mt5">
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
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
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
