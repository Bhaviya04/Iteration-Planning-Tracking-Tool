using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;

public partial class Backend_login : System.Web.UI.Page
{
    MainClass mc = new MainClass();
    SqlConnection cn;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(Page.IsPostBack))
        {
            if (Session["email"] != null)
            {
                Response.Redirect("dashboard.aspx");
            }
        }
    }
    protected void Save_Click(object sender, EventArgs e)
    {
        if (txtusername.Text == "bhaviya04@csu.fullerton.edu" && txtpassword.Text == "CPSC547")
        {
            Session["email"] = "bhaviyagandani@gmail.com";
            Response.Redirect("dashboard.aspx");
        }
        else
        {
            lbldisplay.Text = "* Invalid username and password";
        }
        
    }
}