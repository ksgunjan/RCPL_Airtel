<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmmanagerDashboard.aspx.cs"
    Inherits="frmmanagerDashboard" MasterPageFile="~/MasterPage2.master" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="conheader" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="conplace" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="right_col" role="main">
        <div class="">
            <div class="title_left">
                <h3>
                    Dashboard</h3>
            </div>
            <div class="accordion" id="accordion" role="tablist" aria-multiselectable="true">
                <div class="panel">
                    <a class="panel-heading" role="tab" id="headingOne" data-toggle="collapse" data-parent="#accordion"
                        href="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                        <h4 class="panel-title">
                            Leased Line</h4>
                    </a>
                    <div id="collapseOne" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingOne">
                        <div class="panel-body">
                            <div class="row top_tiles">
                                <div class="animated flipInY col-lg-4 col-md-4 col-sm-6 col-xs-12">
                                    <div class="tile-stats">
                                        <div class="icon pt5">
                                            <i class="fa fa-user-secret"></i>
                                        </div>
                                        <div class="count" runat="server" id="divfe">
                                        </div>
                                        <h3>
                                            Total Field Executive</h3>
                                        <p>
                                            Total active fields excutive.</p>
                                    </div>
                                </div>
                                <div class="animated flipInY col-lg-4 col-md-4 col-sm-6 col-xs-12">
                                    <div class="tile-stats">
                                        <div class="icon pt5">
                                            <i class="fa fa-history"></i>
                                        </div>
                                        <div class="count" runat="server" id="divcompfe">
                                        </div>
                                        <h3>
                                            Total Positive Audit</h3>
                                        <p>
                                            Total visit done by FE's</p>
                                    </div>
                                </div>
                                <div class="animated flipInY col-lg-4 col-md-4 col-sm-6 col-xs-12">
                                    <div class="tile-stats">
                                        <div class="icon pt5">
                                            <i class="fa fa-pause-circle"></i>
                                        </div>
                                        <div class="count" runat="server" id="divcompfenot">
                                        </div>
                                        <h3>
                                            Revisit Audit</h3>
                                        <p>
                                            Total pending plans by FE's</p>
                                    </div>
                                </div>
                                <div class="animated flipInY col-lg-4 col-md-4 col-sm-6 col-xs-12">
                                    <div class="tile-stats">
                                        <div class="icon pt5">
                                            <i class="fa fa-trash-o"></i>
                                        </div>
                                        <div class="count" runat="server" id="dvnegatve">
                                        </div>
                                        <h3>
                                            Negative Audit</h3>
                                        <p>
                                            Total Negative plans by FE's</p>
                                    </div>
                                </div>
                                <div class="animated flipInY col-lg-4 col-md-4 col-sm-6 col-xs-12">
                                    <div class="tile-stats">
                                        <div class="icon pt5">
                                            <i class="fa fa-clock-o"></i>
                                        </div>
                                        <div class="count" runat="server" id="divvisitreming">
                                        </div>
                                        <h3>
                                            Total Pending Audit</h3>
                                        <p>
                                            Total case remaining for visit by FE's</p>
                                    </div>
                                </div>
                                <div class="animated flipInY col-lg-4 col-md-4 col-sm-6 col-xs-12">
                                    <div class="tile-stats">
                                        <div class="icon pt5">
                                            <i class="fa fa-clock-o"></i>
                                        </div>
                                        <div class="count" runat="server" id="divtotalcase">
                                        </div>
                                        <h3>
                                            Total Case Till Today</h3>
                                        <p>
                                            Total case we have from start.</p>
                                    </div>
                                </div>
                                <div class="animated flipInY col-lg-4 col-md-4 col-sm-6 col-xs-12">
                                    <div class="tile-stats">
                                        <div class="icon pt5">
                                            <i class="fa fa-user-secret"></i>
                                        </div>
                                        <div class="count" runat="server" id="divsubmitted">
                                        </div>
                                        <h3>
                                            Total Submitted Document</h3>
                                        <p>
                                            Total submitted document by managers</p>
                                    </div>
                                </div>
                                <div class="animated flipInY col-lg-4 col-md-4 col-sm-6 col-xs-12">
                                    <div class="tile-stats">
                                        <div class="icon pt5">
                                            <i class="fa fa-history"></i>
                                        </div>
                                        <div class="count" runat="server" id="divsccaned">
                                        </div>
                                        <h3>
                                            Total Scanned Document</h3>
                                        <p>
                                            Total scanned documents by managers</p>
                                    </div>
                                </div>
                            </div>
                            <div class="clearfix pb10">
                            </div>
                            <div id="divassignjobfeall" class="bg-white" runat="server">
                                <div class=" ac p10">
                                    <div class="col-sm-12 bg-info p10">
                                        <h4>
                                            Total Assign audit to FE*</h4>
                                        <div class="">
                                            <b>Leased Line</b>
                                            <div class="p0-10">
                                            </div>
                                            <div class="col-sm-6">
                                                <h3>
                                                    <asp:Label ID="lblmonth" runat="server" class="btn btn-warning btn-block pull-left"></asp:Label>
                                                </h3>
                                            </div>
                                            <div class="col-sm-6 pt10">
                                                <asp:DropDownList ID="ddlmonth" runat="server" AutoPostBack="true" class="form-control"
                                                    OnSelectedIndexChanged="ddlmonth_SelectedIndexChanged">
                                                    <asp:ListItem Selected="True" Value="01">Select Month</asp:ListItem>
                                                    <asp:ListItem Value="02">January</asp:ListItem>
                                                    <asp:ListItem Value="03">February</asp:ListItem>
                                                    <asp:ListItem Value="04">March</asp:ListItem>
                                                    <asp:ListItem Value="05">April</asp:ListItem>
                                                    <asp:ListItem Value="06">May</asp:ListItem>
                                                    <asp:ListItem Value="07">June</asp:ListItem>
                                                    <asp:ListItem Value="08">July</asp:ListItem>
                                                    <asp:ListItem Value="09">August</asp:ListItem>
                                                    <asp:ListItem Value="10">September</asp:ListItem>
                                                    <asp:ListItem Value="11">October</asp:ListItem>
                                                    <asp:ListItem Value="12">November</asp:ListItem>
                                                    <asp:ListItem Value="13">December</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="clearfix pb10">
                                            </div>
                                            <asp:Chart ID="Chart1" runat="server" Height="300px" Width="900px">
                                                <Titles>
                                                    <asp:Title ShadowOffset="3" Name="Items" />
                                                </Titles>
                                                <Legends>
                                                    <asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="False" Name="Default"
                                                        LegendStyle="Row" />
                                                </Legends>
                                                <Series>
                                                    <asp:Series Name="Default" ChartType="Pie" IsValueShownAsLabel="true" YValuesPerPoint="2">
                                                    </asp:Series>
                                                </Series>
                                                <ChartAreas>
                                                    <asp:ChartArea Name="ChartArea1" BorderWidth="0">
                                                    </asp:ChartArea>
                                                </ChartAreas>
                                            </asp:Chart>
                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix pb10">
                                </div>
                            </div>
                            <div class="clearfix pb10">
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
                                        <asp:Label ID="lbltodaypendingcase" runat="server" class="btn btn-warning btn-block"></asp:Label>
                                    </div>
                                </div>
                                <div class="clearfix pb10">
                                </div>
                            </div>
                            <div class="clearfix pb10">
                            </div>
                            <div class="bg-white" runat="server" id="divfejobdetail">
                                <div class=" bg-info p10">
                                    <asp:DataList ID="dlfedetail" runat="server" RepeatColumns="1" RepeatDirection="Horizontal"
                                        RepeatLayout="Flow" OnItemDataBound="dlfedetail_ItemDataBound">
                                        <ItemTemplate>
                                            <asp:Label ID="lblname1" runat="server" class="btn btn-primary btn-sm pull-left"
                                                Text='<%#Eval("FE Name") %>'></asp:Label>
                                            <asp:Label ID="lbldate" runat="server" Visible="false" class="btn btn-primary btn-sm pull-right"
                                                Text=""></asp:Label>
                                            <asp:Label ID="lbltime" runat="server" Visible="false" class="btn btn-primary btn-sm pull-right"
                                                Text=""></asp:Label>
                                            <asp:GridView ID="gvfedetaillist" runat="server" class="table table-responsive table-hover table-bordered table-condensed"
                                                AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Logical Circuit Id" HeaderText="Logical Circuit ID" NullDisplayText="#" />
                                                    <asp:BoundField DataField="Party Name" HeaderText="Company Name" NullDisplayText="#" />
                                                    <asp:BoundField DataField="ProductName" HeaderText="Product" NullDisplayText="#" />
                                                    <asp:BoundField DataField="ProdTypeName" HeaderText="Product Type" NullDisplayText="#" />
                                                    <asp:BoundField DataField="BILLING_BANDWIDTH" HeaderText="Bandwidth" NullDisplayText="#" />
                                                    <asp:BoundField DataField="Commissioning Date" DataFormatString="{0:dd/MMM/yyyy}"
                                                        HeaderText="Commissioning Date" NullDisplayText="#" />
                                                    <asp:BoundField DataField="Account Manager" HeaderText="Account Manager" NullDisplayText="#" />
                                                    <asp:TemplateField HeaderText="Compliance Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblcomp" runat="server" Text='<%# Eval("FEComplianceDate", "{0:dd/MMM/yyyy}").ToString() == "" ? "#" : Eval("FEComplianceDate", "{0:dd/MMM/yyyy}").ToString()%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="StatusByFE" HeaderText="Status" NullDisplayText="#" ItemStyle-ForeColor="Red" />
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
                                            <asp:Label ID="lblmssg" runat="server" class="btn btn-sm- btn-danger" Visible="false"></asp:Label>
                                            <div class="border5">
                                            </div>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- end of accordion -->
        </div>
    </div>
</asp:Content>
