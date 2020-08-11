<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmAddLoginRegistration.aspx.cs" Inherits="FrmAddLoginRegistration" MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="conhead" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="contentmain" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="content oem-content">
        <asp:updatepanel runat="server" id="updatepan">
            <ContentTemplate>
        <div class="sideBg">
            <div class="row">
                <div class="col-md-12 padding_0">
                    <ul class="breadcrumb">
                        <li>
                            <asp:label id="lblPageName" runat="server" text="Add/View User"></asp:label>
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
                <div class="col-sm-6 col-md-6 col-lg-6">
                    <asp:hiddenfield id="hfEmpId" runat="server" />
                    <label class="form-control-label">
                        Select Category</label>
                    <div class="select">
                        <asp:dropdownlist id="ddlcategory" runat="server" class="form-control">
                        </asp:dropdownlist>
                    </div>
                    <label class="form-control-label">
                        Name</label>
                    <asp:textbox id="txtname" runat="server" class="form-control" maxlength="250" required=""
                        placeholder="Name"></asp:textbox>
                    <label class="form-control-label">
                        Email</label>
                    <asp:textbox id="txtemail" runat="server" maxlength="70" autopostback="true" class="form-control"
                        placeholder="Email Address" ontextchanged="txtemail_TextChanged"></asp:textbox>
                    <label class="form-control-label">
                        Password</label>
                    <asp:textbox id="txtpassword" runat="server" maxlength="15" class="form-control"
                        required="" placeholder="Password"></asp:textbox>
                    <label class="form-control-label">
                        Contact No</label>
                    <asp:textbox id="txtmobno" runat="server" class="form-control" maxlength="10"
                        required="" placeholder="Mobile No/ Phone No"></asp:textbox>
                </div>
                <div class="col-sm-6 col-md-6 col-lg-6">
                    <label class="form-control-label">
                        Address</label>
                    <asp:textbox id="txtaddress" runat="server" class="form-control" height="100px" textmode="MultiLine"
                        maxlength="350" placeholder="Address"></asp:textbox>
                    <div runat="server" id="divlocfe" visible="false">
                        <label class="form-control-label">
                            Location</label>
                        <asp:dropdownlist id="ddllocation" runat="server" class="form-control">
                                                    </asp:dropdownlist>
                    </div>
                    <label class="form-control-label">
                        Govt-IDNo</label>
                    <asp:textbox id="txtgovtidno" runat="server" class="form-control" maxlength="16"
                        placeholder="Govt. IDNo"></asp:textbox>
                    <label class="form-control-label">
                        Upload Photo</label>
                    <asp:fileupload id="fupic" runat="server" class="form-control" onchange="showpreview(this);" />
                    <asp:label id="lblpicdetail" runat="server" class="lable lable-info"></asp:label>
                    <img id="imgpreview" height="150" width="150" src="" style="border-width: 0px; visibility: hidden;" />
                    <asp:button id="btnuploadimage" runat="server" class="btn btn-default pull-right mt10"
                        causesvalidation="false" usesubmitbehavior="false" text="Upload Profile Pic"
                        onclick="btnuploadimage_Click" />
                </div>
                <div class="clearfix mt5"></div>
                <div class="form-group">
                    <asp:button id="btnsub" runat="server" class="btn btn-primary pull-right" text="Save New User"
                        onclick="btnsub_Click" />
                    <asp:button id="btnclear" runat="server" class="btn btn-default pull-right" causesvalidation="false"
                        usesubmitbehavior="false" text="Clear" onclick="btnclear_Click" />
                </div>
                <div class="clearfix mt10"></div>
                <div style="overflow:auto;">
                    <asp:gridview id="gvcomp" runat="server" autogeneratecolumns="false" class="commonAjaxTbl dataTable master-company-table ViewProductTable table 
                                        display responsive no-wrap table-hover manage-user Grid table-responsive"
                        onrowcommand="gvcomp_RowCommand" onrowcreated="gvcomp_RowCreated">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr.No">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Profile Pic">
                                                <ItemTemplate>
                                                    <asp:HiddenField runat="server" id="hfimg" Value='<%#Eval("ProfilePic") %>'/>
                                                    <asp:Image ID="img" runat="server" class="image img-thumbnail" src='<%#Eval("ProfilePic","ProfileImage/{0}") %>'
                                                        Height="120px" Width="120px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Category">
                                                <ItemTemplate>
                                                   <asp:Label ID="lblcat" runat="server" Class="black" Text='<%#Eval("Category") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Name">
                                                <ItemTemplate>
                                                       <asp:Label ID="lblname" Class="black" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Email">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblemail" runat="server" Class="black" Text='<%#Eval("Email") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Contact No">
                                                <ItemTemplate>
                                                       <asp:Label ID="lblphone" runat="server" Class="black" Text='<%#Eval("ContactNo") %>'></asp:Label>                                                   
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Address">
                                                <ItemTemplate>
                                                      <asp:Label ID="lbladd" runat="server" Class="black" Text='<%#Eval("Address") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Last Login Time">
                                                <ItemTemplate>
                                                    <%#Eval("LastLoginTime")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Password">
                                                <ItemTemplate>
                                                      <asp:Label ID="lblpass" runat="server" Class="black" Text='<%#Eval("Password") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblupdate" runat="server" CommandArgument='<%#Eval("EmpID") %>'
                                                        CommandName="up" class="fa fa-edit" OnClientClick="return confirm('Click ok for edit this record?');"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Active/Deactive">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbisacti" runat="server" CssClass="m-lg-r" Text='<%#Eval("Text") %>'></asp:Label>
                                                    <asp:LinkButton ID="lblactive" runat="server" OnClientClick="return confirm('click ok for modify this record?');"
                                                        class='<%#Eval("button") %>' CommandArgument='<%#Eval("EmpID") %>' CommandName="active"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:gridview>
                </div>
            </div>
        </div>
                </ContentTemplate>
              <Triggers>
                <asp:PostBackTrigger ControlID="btnuploadimage" />
            </Triggers>
            </asp:updatepanel>
    </div>
</asp:Content>
