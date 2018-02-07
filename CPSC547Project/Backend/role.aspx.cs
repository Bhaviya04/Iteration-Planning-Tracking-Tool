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

public partial class Backend_role : System.Web.UI.Page
{
    MainClass mc = new MainClass();
    SqlConnection cn;
    protected void Page_Load(object sender, EventArgs e)
    {
        cn = mc.Connect();

        if (!(Page.IsPostBack))
        {
            if (Session["email"] != "")
            {
                if (Session["email"] != null)
                {
                    projectname();
                    showgrid();

                    if (Session["insert"] == "inserted")
                    {
                        Session["insert"] = "";
                        Response.Write("<script language='javascript'>window.alert('Your information added.');</script>");

                    }

                    if (Session["update"] == "updated")
                    {
                        Session["update"] = "";
                        Response.Write("<script language='javascript'>window.alert('Your information updated.');</script>");

                    }

                    if (Request.QueryString["mode"] == "Edit")
                    {
                        getdetails();
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
    }
    protected void projectname()
    {
        cn.Open();

        StringBuilder sb = new StringBuilder();

        SqlCommand cmd = new SqlCommand("select project_name from project where project_id=@projectid;", cn);
        cmd.Parameters.Add(new SqlParameter("@projectid", Convert.ToInt32(Request.QueryString["projectid"])));
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            Label1.Text = dr[0].ToString();
        }

        dr.Close();
        cn.Close();
    }
    protected void showgrid()
    {
        SqlDataAdapter da = new SqlDataAdapter("select * from role where project_id=" + Convert.ToInt32(Request.QueryString["projectid"]), cn);
        DataSet ds = new DataSet();
        da.Fill(ds);
        GridView1.DataSource = ds;
        GridView1.DataBind();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["mode"] == "Edit")
        {
            cn.Open();

            SqlCommand cmd = new SqlCommand("update role set name=@name,email=@email,role=@role where role_id=@roleid;", cn);
            cmd.Parameters.Add(new SqlParameter("@roleid", Convert.ToInt32(Request.QueryString["id"])));
            cmd.Parameters.Add(new SqlParameter("@name", txtempname.Text));
            cmd.Parameters.Add(new SqlParameter("@email", txtemail.Text));
            cmd.Parameters.Add(new SqlParameter("@role", txtrolename.Text));

            cmd.ExecuteNonQuery();

            cn.Close();

            Session["update"] = "updated";
            Response.Redirect("role.aspx?projectid=" + Request.QueryString["projectid"]);
        }
        else
        {
            cn.Open();

            SqlCommand cmd = new SqlCommand("insert into role values(@projectid,@name,@email,@role);", cn);
            cmd.Parameters.Add(new SqlParameter("@projectid", Convert.ToInt32(Request.QueryString["projectid"])));
            cmd.Parameters.Add(new SqlParameter("@name", txtempname.Text));
            cmd.Parameters.Add(new SqlParameter("@email", txtemail.Text));
            cmd.Parameters.Add(new SqlParameter("@role", txtrolename.Text));
            cmd.ExecuteNonQuery();

            cn.Close();

            Session["insert"] = "inserted";
            Response.Redirect("role.aspx?projectid=" + Request.QueryString["projectid"]);
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "edit")
        {
            Response.Redirect("role.aspx?mode=Edit&projectid=" + Request.QueryString["projectid"] + "&id=" + e.CommandArgument);
        }
    }
    protected void getdetails()
    {
        cn.Open();
        SqlCommand cmd4 = new SqlCommand("select * from role where role_id=@id;", cn);
        cmd4.Parameters.Add(new SqlParameter("@id", Request.QueryString["id"]));
        SqlDataReader dr4 = cmd4.ExecuteReader();
        if (dr4.Read())
        {
            txtrolename.Text = dr4[4].ToString();
            txtempname.Text = dr4[2].ToString();
            txtemail.Text = dr4[3].ToString();
            dr4.Close();
        }
        else
        {
            dr4.Close();
        }
        cn.Close();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("planning.aspx?projectid=" + Request.QueryString["projectid"]);
    }
}