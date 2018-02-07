<%@ Page Title="" Language="C#" MasterPageFile="~/Backend/admin.master" AutoEventWireup="true" CodeFile="durationresults.aspx.cs" Inherits="Backend_durationresults" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h3 class="panel-title">Duration (In Days) - <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></h3>                                
                                </div>
                                <div class="panel-body">
                                    <center><h1><asp:Label ID="Label2" runat="server" Text="No Duration Added Yet" Visible="false"></asp:Label></h1></center>
                                    <div id="morris-donut-example" style="height: 300px;"></div>

                                    <br /><br />
                                    <div class="form-group">
                                            <label class="col-md-6 control-label" style="font-size: large">Estimated Duration For Project: <asp:Label ID="Label3" runat="server" Visible="false"></asp:Label> Days</label>
                                        </div>


                                </div>
                            </div>

    <div class="col-md-12">
                                          
                                 <div class="col-md-3">
                                     <asp:Button ID="Button3" runat="server" Text="Back" class="btn btn-primary" OnClick="Button3_Click" AutoPostBack="true" />
                                          <br /><br />
                                </div>
                                
                               
                                </div>

    <script type="text/javascript" src="js/plugins/jquery/jquery.min.js"></script>
    <script type="text/javascript" src="js/plugins/morris/raphael-min.js"></script>
    <script type="text/javascript" src="js/plugins/morris/morris.min.js"></script>
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
</asp:Content>

