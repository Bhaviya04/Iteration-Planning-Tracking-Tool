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

public partial class Backend_dashboard : System.Web.UI.Page
{
    MainClass mc = new MainClass();
    SqlConnection cn;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["email"] != "")
        {
            if (Session["email"] != null)
            {
                cn = mc.Connect();
                showgrid();
                if (Request.QueryString["insert"] == "yes")
                {
                    Response.Write("<script language='javascript'>window.alert('Project Has Been Added');</script>");
                }
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

    protected void showgrid()
    {
        SqlDataAdapter da = new SqlDataAdapter("select * from project;", cn);
        DataSet ds = new DataSet();
        da.Fill(ds);
        GridView1.DataSource = ds;
        GridView1.DataBind();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        cn.Open();

        SqlCommand cmd = new SqlCommand("insert into project values(@projectname,@projectdescription);",cn);
        cmd.Parameters.Add(new SqlParameter("@projectname", txtprojectname.Text));
        cmd.Parameters.Add(new SqlParameter("@projectdescription", txtdescription.Text));

        cmd.ExecuteNonQuery();

        cn.Close();

        Response.Redirect("dashboard.aspx?insert=yes");
    }
}