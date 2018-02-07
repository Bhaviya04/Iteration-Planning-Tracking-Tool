<%@ Page Title="" Language="C#" MasterPageFile="~/Backend/admin.master" AutoEventWireup="true" CodeFile="planning.aspx.cs" Inherits="Backend_planning" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="panel-body">
      
                                        <div class="form-group">
                                            <div class="col-md-12">
                                                <br />
                                                <center><asp:Label ID="Label1" runat="server" ForeColor="Black" Font-Underline="True" Font-Bold="True" Font-Size="XX-Large" Font-Names="Calibri"></asp:Label></center>
                                            </div>
                                        </div>  
                                        <br /><br /><br /><br /> <br /><br /><br /><br /><br /> <br /><br /><br /><br /><br />                                                  
                                        <div class="form-group">                 
                                            <div class="col-md-6">
                                                <asp:Button ID="Button1" height="50px" class="btn btn-info btn-block" style="font-size: x-large" runat="server" text="Click Here To Add Project Modules" OnClick="Button1_Click" />
                                            </div>
                                            <div class="col-md-6">
                                                    <asp:Button ID="Button2" height="50px" class="btn btn-danger btn-block" style="font-size: x-large" runat="server" text="Click Here To Add Project Roles" OnClick="Button2_Click" />
                                            </div>                               
                                        </div>
                                          
                                        <div class="form-group">
                                                             
                                            <div class="col-md-3">
                                            </div>
                                            <div class="col-md-6">
                                                  <br />  <asp:Button ID="Button4" height="50px" class="btn btn-warning btn-block" style="font-size: x-large" runat="server" text="Click Here To Assign Module To Role" OnClick="Button4_Click" />
                                            </div>
                                            <div class="col-md-3">
                                            </div>                               
                                        </div>  
                                </div>
</asp:Content>

