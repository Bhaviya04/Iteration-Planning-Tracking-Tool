<%@ Page Title="" Language="C#" MasterPageFile="~/Backend/admin.master" AutoEventWireup="true" CodeFile="estimation.aspx.cs" Inherits="Backend_estimation" %>

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
                                                <asp:Button ID="Button1" height="50px" class="btn btn-info btn-block" style="font-size: x-large" runat="server" text="Click Here To Estimate Project Duration" OnClick="Button1_Click" />
                                            </div>
                                            <div class="col-md-6">
                                                    <asp:Button ID="Button2" height="50px" class="btn btn-danger btn-block" style="font-size: x-large" runat="server" text="Click Here To Estimate Project Cost" OnClick="Button2_Click" />
                                            </div>                               
                                        </div>  
                                </div>
</asp:Content>

