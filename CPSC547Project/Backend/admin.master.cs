using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Text;

public partial class Backend_admin : System.Web.UI.MasterPage
{
    MainClass mc = new MainClass();
    SqlConnection cn;
    StringBuilder sb = new StringBuilder();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["email"] != "")
        {
            if (Session["email"] != null)
            {
                cn = mc.Connect();
                listprojects();
            }
            else
            {
                Response.Redirect("login.aspx");
            }
        }
        else
        {
            Response.Redirect("login.aspx");
        }
    }
    protected void link_emailverify_click(object sender, EventArgs e)
    {
        Session["email"] = null;

        Response.Redirect("login.aspx");
    }
    protected void listprojects()
    {
        cn.Open();

        StringBuilder sb = new StringBuilder();

        SqlCommand cmd = new SqlCommand("select project_id,project_name from project order by 1 desc;",cn);
        SqlDataReader dr = cmd.ExecuteReader();
        while(dr.Read())
        {
            sb.Append("<li>");
            sb.Append("<a href='choosesection.aspx?projectid=" + dr[0].ToString() + "'>");
            sb.Append("<span class='fa fa-edit'>");
            sb.Append("</span>");
            sb.Append(dr[1].ToString());
            sb.Append("</a>");
            sb.Append("</li>");
        }

        dr.Close();
        cn.Close();

        Literal1.Text = sb.ToString();
    }
}
