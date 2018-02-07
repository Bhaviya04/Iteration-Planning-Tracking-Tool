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

public partial class Backend_module : System.Web.UI.Page
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
                    showdropdown();
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
    protected void showdropdown()
    {
        cn.Open();

        ListItem defaultitem = new ListItem("Choose option");
        ddlphase.Items.Add(defaultitem);

        SqlCommand cmd9 = new SqlCommand("select phase_id,name from duration;", cn);
        SqlDataReader dr9 = cmd9.ExecuteReader();

        while (dr9.Read())
        {
            if (Request.QueryString["mode"] == "Edit")
            { 
                if(ddlphase.SelectedValue != dr9[0].ToString())
                { 
                    ListItem item = new ListItem(dr9[1].ToString(), dr9[0].ToString());
                    ddlphase.Items.Add(item);
                }
            }
            else
            {
                ListItem item = new ListItem(dr9[1].ToString(), dr9[0].ToString());
                ddlphase.Items.Add(item);
            }
        }
        dr9.Close();

        cn.Close();
    }
    protected void showgrid()
    {
        SqlDataAdapter da = new SqlDataAdapter("select p.name as phasename,p.phase_id,m.name as modulename,m.module_id from duration p join module m on p.phase_id=m.phase_id where m.project_id=" + Convert.ToInt32(Request.QueryString["projectid"]), cn);
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

            SqlCommand cmd = new SqlCommand("update module set name=@name,phase_id=@phaseid where module_id=@moduleid;", cn);
            cmd.Parameters.Add(new SqlParameter("@moduleid", Convert.ToInt32(Request.QueryString["id"])));
            cmd.Parameters.Add(new SqlParameter("@name", txtmodulename.Text));
            cmd.Parameters.Add(new SqlParameter("@phaseid", Convert.ToInt32(ddlphase.SelectedValue)));

            cmd.ExecuteNonQuery();

            cn.Close();

            Session["update"] = "updated";
            Response.Redirect("module.aspx?projectid=" + Request.QueryString["projectid"]);
        }
        else
        {
            cn.Open();

            SqlCommand cmd = new SqlCommand("insert into module values(@projectid,@phaseid,@name);", cn);
            cmd.Parameters.Add(new SqlParameter("@projectid", Convert.ToInt32(Request.QueryString["projectid"])));
            cmd.Parameters.Add(new SqlParameter("@phaseid", Convert.ToInt32(ddlphase.SelectedValue)));
            cmd.Parameters.Add(new SqlParameter("@name", txtmodulename.Text));
            cmd.ExecuteNonQuery();

            cn.Close();

            Session["insert"] = "inserted";
            Response.Redirect("module.aspx?projectid=" + Request.QueryString["projectid"]);
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "edit")
        {
            Response.Redirect("module.aspx?mode=Edit&projectid=" + Request.QueryString["projectid"] + "&id=" + e.CommandArgument);
        }
    }

    protected void getdetails()
    {
        cn.Open();

        ddlphase.Items.Clear();

        SqlCommand cmd4 = new SqlCommand("select m.module_id,m.name,p.phase_id,p.name from module m join duration p on m.phase_id=p.phase_id where module_id=@id;", cn);
        cmd4.Parameters.Add(new SqlParameter("@id", Request.QueryString["id"]));
        SqlDataReader dr4 = cmd4.ExecuteReader();
        if (dr4.Read())
        {
            txtmodulename.Text = dr4[1].ToString();
            ddlphase.Items.Add(new ListItem(dr4[3].ToString(), dr4[2].ToString()));
            dr4.Close();
        }
        else
        {
            dr4.Close();
        }

        cn.Close();

        showdropdown();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("planning.aspx?projectid=" + Request.QueryString["projectid"]);
    }
}