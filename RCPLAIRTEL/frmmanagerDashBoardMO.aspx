<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmmanagerDashBoardMO.aspx.cs" Inherits="frmmanagerDashBoardMO"
    MasterPageFile="~/MasterPage.master" %>

<asp:Content ContentPlaceHolderID="head" runat="server" ID="header"></asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server" ID="inner">
    <div class="content oem-content">
        <div class="sideBg">
            <div class="row">
                <div class="col-mod-12 padding_0">
                    <ul class="breadcrumb">
                        <li>
                            <asp:Label ID="lblPageName" runat="server" Text="Dashboard MO"></asp:Label>
                        </li>
                    </ul>
                </div>
            </div>
            <div class="addfdi">
                <div class="admin-dashboard">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-lg-3 col-sm-6 col-xs-12">
                                    <div class="white-box analytics-info total-comp">
                                        <ul class="list-inline two-part">
                                            <li>
                                                <div class="icon-box">
                                                    <i class="far fa-building"></i>
                                                </div>
                                                <div class="compName">
                                                    <h3 class="box-title">Total Field Executive</h3>
                                                    <div class="Number">

                                                        <asp:Label ID="lbltotfe" CssClass="comp_number" runat="server" Text="0"></asp:Label>
                                                    </div>
                                                </div>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                                <div class="col-lg-3 col-sm-6 col-xs-12">
                                    <div class="white-box analytics-info total-fdi">
                                        <ul class="list-inline two-part">
                                            <li>
                                                <div class="icon-box">
                                                    <i class="far fa-building"></i>
                                                </div>
                                                <div class="compName">
                                                    <h3 class="box-title">Total Positive Audit</h3>
                                                    <div class="Number">
                                                        <asp:Label ID="lbltotpositive" runat="server" CssClass="comp_number" Text="0"></asp:Label>
                                                    </div>
                                                </div>
                                            </li>
                                        </ul>

                                    </div>
                                </div>
                                <div class="col-lg-3 col-sm-6 col-xs-12">
                                    <div class="white-box analytics-info last-fdi">
                                        <ul class="list-inline two-part">
                                            <li>
                                                <div class="icon-box">
                                                    <i class="far fa-building"></i>
                                                </div>
                                                <div class="compName">
                                                    <h3 class="box-title">Revisit Audit</h3>
                                                    <div class="Number">
                                                        <asp:Label ID="lblrevisit" runat="server" CssClass="comp_number" Text="0"></asp:Label>
                                                    </div>
                                                </div>
                                            </li>
                                        </ul>

                                    </div>
                                </div>
                                <div class="col-lg-3 col-sm-6 col-xs-12">
                                    <div class="white-box analytics-info last-fdi">
                                        <ul class="list-inline two-part">
                                            <li>
                                                <div class="icon-box">
                                                    <i class="fa fa-users" aria-hidden="true"></i>
                                                </div>
                                                <div class="compName">
                                                    <h3 class="box-title">Negative Audit</h3>
                                                    <div class="Number">
                                                        <asp:LinkButton ID="lblnegative" runat="server" CssClass="comp_number" Text="0"></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </li>
                                        </ul>

                                    </div>
                                </div>
                            </div>
                            <div class="row" style="margin-top: 10px;">
                                <div class="col-lg-3 col-sm-6 col-xs-12">
                                    <div class="white-box analytics-info last-fdi">
                                        <ul class="list-inline two-part">
                                            <li>
                                                <div class="icon-box">
                                                    <i class="fa fa-users"></i>
                                                </div>
                                                <div class="compName">
                                                    <h3 class="box-title">Total Pending Audit</h3>
                                                    <div class="Number">
                                                        <asp:Label ID="lblpending" runat="server" CssClass="comp_number" Text="0"></asp:Label>
                                                    </div>
                                                </div>
                                            </li>
                                        </ul>

                                    </div>
                                </div>
                                <div class="col-lg-3 col-sm-6 col-xs-12">
                                    <div class="white-box analytics-info last-fdi">
                                        <ul class="list-inline two-part">
                                            <li>
                                                <div class="icon-box">
                                                    <i class="fab fa-product-hunt" aria-hidden="true"></i>
                                                </div>
                                                <div class="compName">

                                                    <h3 class="box-title">Total Case Till Today</h3>
                                                    <div class="Number">
                                                        <asp:Label ID="lbltotalcase" runat="server" CssClass="comp_number" Text="0"></asp:Label>
                                                    </div>
                                                </div>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                                <div class="col-lg-3 col-sm-6 col-xs-12">
                                    <div class="white-box analytics-info last-fdi">
                                        <ul class="list-inline two-part">
                                            <li>
                                                <div class="icon-box">
                                                    <i class="fab fa-product-hunt" aria-hidden="true"></i>
                                                </div>
                                                <div class="compName">

                                                    <h3 class="box-title">Total Submitted Document</h3>
                                                    <div class="Number">
                                                        <asp:Label ID="lbltotsubdoc" runat="server" CssClass="comp_number" Text="0"></asp:Label>
                                                    </div>
                                                </div>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                                <div class="col-lg-3 col-sm-6 col-xs-12">
                                    <div class="white-box analytics-info last-fdi">
                                        <ul class="list-inline two-part">
                                            <li>
                                                <div class="icon-box">
                                                    <i class="fab fa-product-hunt" aria-hidden="true"></i>
                                                </div>
                                                <div class="compName">

                                                    <h3 class="box-title">Total scanned documents by managers</h3>
                                                    <div class="Number">
                                                        <asp:Label ID="lbltotscann" runat="server" CssClass="comp_number" Text="0"></asp:Label>
                                                    </div>
                                                </div>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                                <div class="clearfix mt10">
                                </div>
                                <div id="Div1" class="bg-white" runat="server">
                                    <div class="ac p10">
                                        <div class="col-sm-4">
                                            <asp:Label ID="lbltotaltodaysuccesscase" runat="server" class="btn btn-success btn-block"></asp:Label>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:Label ID="lbltodaynegativecase" runat="server" class="btn btn-danger btn-block"></asp:Label>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:Label ID="lbltodaypendingcase" runat="server" class="btn btn-info btn-block"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="clearfix mt10">
                                    </div>
                                </div>
                                <div class="clearfix mt10">
                                </div>
                                <div class="bg-white" runat="server" id="divfedetailmo">
                                    <div class=" bg-info mt10">
                                        <asp:DataList ID="dlmo" runat="server" RepeatColumns="1" RepeatDirection="Horizontal"
                                            RepeatLayout="Flow" OnItemDataBound="dlmo_ItemDataBound">
                                            <ItemTemplate>
                                                <asp:Label ID="lblnamemo" runat="server" class="btn btn-primary btn-sm pull-left"
                                                    Text='<%#Eval("FE_Name") %>'></asp:Label>
                                                <asp:Label ID="lbldatemo" runat="server" Visible="false" class="btn btn-primary btn-sm pull-right"
                                                    Text=""></asp:Label>
                                                <asp:Label ID="lbltimemo" runat="server" Visible="false" class="btn btn-primary btn-sm pull-right"
                                                    Text=""></asp:Label>
                                                <asp:GridView ID="gvfedetaillistmo" runat="server" class="table table-responsive table-hover table-bordered table-condensed"
                                                    AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                    <AlternatingRowStyle BackColor="White" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Billed_Ext_Id" HeaderText="Billed Ext ID" NullDisplayText="#" />
                                                        <asp:BoundField DataField="Party_Name" HeaderText="Company Name" NullDisplayText="#" />
                                                        <asp:BoundField DataField="Product_Name" HeaderText="Product" NullDisplayText="#" />
                                                        <asp:BoundField DataField="Prod_Type_Name" HeaderText="Product Type" NullDisplayText="#" />
                                                        <asp:BoundField DataField="Account_Manager" HeaderText="Account Manager" NullDisplayText="#" />
                                                        <asp:TemplateField HeaderText="Compliance Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcomp" runat="server" Text='<%# Eval("FE_Compliance_Date", "{0:dd/MMM/yyyy}").ToString() == "" ? "#" : Eval("FE_Compliance_Date", "{0:dd/MMM/yyyy}").ToString()%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Status_By_FE" HeaderText="Status" NullDisplayText="#" ItemStyle-ForeColor="Red" />
                                                    </Columns>
                                                    <EditRowStyle BackColor="#2461BF" />
                                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                    <RowStyle BackColor="#EFF3FB" />
                                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                                </asp:GridView>
                                                <asp:Label ID="lblmssgmo" runat="server" class="btn btn-sm- btn-danger" Visible="false"></asp:Label>
                                                <div class="mt10">
                                                </div>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="ddlmonth" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlmonth_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="col-sm-12 row">
                                        <div class="col-sm-6">
                                            <asp:Chart ID="crtAllocateddatewise" runat="server" Width="600px">
                                                <Titles>
                                                    <asp:Title ShadowOffset="3" Name="Allocated Date Wise Assign Job to FE Current Month" />
                                                </Titles>
                                                <Legends>
                                                    <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="true" Title="Allocated Date Wise Assign Job to FE"
                                                        Name="Default" LegendStyle="Row" />
                                                </Legends>
                                                <Series>
                                                    <asp:Series Name="Assign Job to Fe" IsValueShownAsLabel="true" Color="#669999" ChartType="StackedColumn"
                                                        YValuesPerPoint="2">
                                                    </asp:Series>
                                                </Series>
                                                <ChartAreas>
                                                    <asp:ChartArea Name="ChartArea1" BorderWidth="0"></asp:ChartArea>
                                                </ChartAreas>
                                            </asp:Chart>
                                        </div>
                                        <div class="col-sm-6">
                                            <asp:Chart ID="crtstatusupdatedatewise" runat="server" Width="600px">
                                                <Titles>
                                                    <asp:Title ShadowOffset="3" Name="Status Update Date Wise Assign Job to FE Current Month" />
                                                </Titles>
                                                <Legends>
                                                    <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="true" Title="Status Update Date Wise Assign Job to FE Current Month"
                                                        Name="SubDomian" LegendStyle="Row" />
                                                </Legends>
                                                <Series>
                                                    <asp:Series Name="Done Job by FE" IsValueShownAsLabel="true" Color="#ffff99" ChartType="StackedColumn">
                                                    </asp:Series>
                                                </Series>
                                                <ChartAreas>
                                                    <asp:ChartArea Name="ChartArea2" BorderWidth="0"></asp:ChartArea>
                                                </ChartAreas>
                                            </asp:Chart>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="footer">© 2019 <a href="#">GIP</a> </div>
</asp:Content>
