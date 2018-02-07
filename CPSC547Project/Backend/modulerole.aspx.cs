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
using System.IO;
using System.Net;
using System.Net.Mail;

public partial class Backend_modulerole : System.Web.UI.Page
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

                    //select SUM(mr.days) as 'Remaining Days' from module_role_mapping mr join module m
                    //on mr.module_id = m.module_id
                    //join duration p on m.phase_id = p.phase_id
                    //where m.phase_id = 2;

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

        ListItem defaultmoduleitem = new ListItem("Choose option");
        ddlmodule.Items.Add(defaultmoduleitem);

        SqlCommand cmd9 = new SqlCommand("select module_id,name from module where project_id=@projectid;", cn);
        cmd9.Parameters.Add(new SqlParameter("@projectid", Convert.ToInt32(Request.QueryString["projectid"])));
        SqlDataReader dr9 = cmd9.ExecuteReader();

        while (dr9.Read())
        {
            if (Request.QueryString["mode"] == "Edit")
            {
                if (ddlmodule.SelectedValue != dr9[0].ToString())
                {
                    ListItem item = new ListItem(dr9[1].ToString(), dr9[0].ToString());
                    ddlmodule.Items.Add(item);
                }
            }
            else
            {
                ListItem item = new ListItem(dr9[1].ToString(), dr9[0].ToString());
                ddlmodule.Items.Add(item);
            }
        }
        dr9.Close();

        ListItem defaultroleitem = new ListItem("Choose option");
        ddlrole.Items.Add(defaultroleitem);

        SqlCommand cmd1 = new SqlCommand("select role_id,name from role where project_id=@projectid;", cn);
        cmd1.Parameters.Add(new SqlParameter("@projectid", Convert.ToInt32(Request.QueryString["projectid"])));
        SqlDataReader dr1 = cmd1.ExecuteReader();

        while (dr1.Read())
        {
            if (Request.QueryString["mode"] == "Edit")
            {
                if (ddlrole.SelectedValue != dr1[0].ToString())
                {
                    ListItem item = new ListItem(dr1[1].ToString(), dr1[0].ToString());
                    ddlrole.Items.Add(item);
                }
            }
            else
            {
                ListItem item = new ListItem(dr1[1].ToString(), dr1[0].ToString());
                ddlrole.Items.Add(item);
            }
        }
        dr1.Close();

        cn.Close();
    }
    protected void showgrid()
    {
        SqlDataAdapter da = new SqlDataAdapter("select m.module_id,m.name as modulename,r.role_id,r.name as rolename,mr.description,mr.days,mr.mapping_id from module m join module_role_mapping mr on m.module_id = mr.module_id join role r on mr.role_id = r.role_id where mr.project_id = " + Convert.ToInt32(Request.QueryString["projectid"]), cn);
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

            SqlCommand cmd_role = new SqlCommand("select role_id from module_role_mapping where mapping_id=@mappingid;", cn);
            cmd_role.Parameters.Add(new SqlParameter("@mappingid", Convert.ToInt32(Request.QueryString["id"])));
            int existing_role = Convert.ToInt32(cmd_role.ExecuteScalar());

            SqlCommand cmd = new SqlCommand("update module_role_mapping set module_id=@moduleid,role_id=@roleid,description=@description,days=@days where mapping_id=@mappingid;", cn);
            cmd.Parameters.Add(new SqlParameter("@mappingid", Convert.ToInt32(Request.QueryString["id"])));
            cmd.Parameters.Add(new SqlParameter("@moduleid", Convert.ToInt32(ddlmodule.SelectedValue)));
            cmd.Parameters.Add(new SqlParameter("@roleid", Convert.ToInt32(ddlrole.SelectedValue)));
            cmd.Parameters.Add(new SqlParameter("@description", txtdesc.Text));
            cmd.Parameters.Add(new SqlParameter("@days", Convert.ToInt32(txtdays.Text)));

            cmd.ExecuteNonQuery();

            string toemail = "";

            SqlCommand cmd1 = new SqlCommand("select email from role where role_id=@roleid;", cn);
            cmd1.Parameters.Add(new SqlParameter("@roleid", Convert.ToInt32(ddlrole.SelectedValue)));
            SqlDataReader dr1 = cmd1.ExecuteReader();
            while (dr1.Read())
            {
                toemail = dr1[0].ToString();
            }
            dr1.Close();

            string modulename = "";

            SqlCommand cmd2 = new SqlCommand("select name from module where module_id=@moduleid;", cn);
            cmd2.Parameters.Add(new SqlParameter("@moduleid", Convert.ToInt32(ddlmodule.SelectedValue)));
            SqlDataReader dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {
                modulename = dr2[0].ToString();
            }
            dr2.Close();

            cn.Close();

            Session["update"] = "updated";
            SendEmail_Update(toemail,modulename,existing_role,Convert.ToInt32(ddlrole.SelectedValue));
            Response.Redirect("modulerole.aspx?projectid=" + Request.QueryString["projectid"]);
        }
        else
        {
            cn.Open();

            SqlCommand cmd = new SqlCommand("insert into module_role_mapping values(@projectid,@moduleid,@roleid,@description,@days);", cn);
            cmd.Parameters.Add(new SqlParameter("@projectid", Convert.ToInt32(Request.QueryString["projectid"])));
            cmd.Parameters.Add(new SqlParameter("@moduleid", Convert.ToInt32(ddlmodule.SelectedValue)));
            cmd.Parameters.Add(new SqlParameter("@roleid", Convert.ToInt32(ddlrole.SelectedValue)));
            cmd.Parameters.Add(new SqlParameter("@description", txtdesc.Text));
            cmd.Parameters.Add(new SqlParameter("@days", Convert.ToInt32(txtdays.Text)));
            cmd.ExecuteNonQuery();

            string toemail = "";

            SqlCommand cmd1 = new SqlCommand("select email from role where role_id=@roleid;",cn);
            cmd1.Parameters.Add(new SqlParameter("@roleid", Convert.ToInt32(ddlrole.SelectedValue)));
            SqlDataReader dr1 = cmd1.ExecuteReader();
            while(dr1.Read())
            {
                toemail = dr1[0].ToString();
            }
            dr1.Close();

            string modulename = "";

            SqlCommand cmd2 = new SqlCommand("select name from module where module_id=@moduleid;", cn);
            cmd2.Parameters.Add(new SqlParameter("@moduleid", Convert.ToInt32(ddlmodule.SelectedValue)));
            SqlDataReader dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {
                modulename = dr2[0].ToString();
            }
            dr2.Close();

            cn.Close();

            Session["insert"] = "inserted";
            SendEmail_Insert(toemail,modulename);
            Response.Redirect("modulerole.aspx?projectid=" + Request.QueryString["projectid"]);
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "edit")
        {
            Response.Redirect("modulerole.aspx?mode=Edit&projectid=" + Request.QueryString["projectid"] + "&id=" + e.CommandArgument);
        }
    }
    protected void getdetails()
    {
        cn.Open();

        ddlmodule.Items.Clear();
        ddlrole.Items.Clear();

        SqlCommand cmd4 = new SqlCommand("select m.module_id,m.name,r.role_id,r.name,mr.description,mr.days from module m join module_role_mapping mr on m.module_id = mr.module_id join role r on mr.role_id = r.role_id where mr.mapping_id =@id;", cn);
        cmd4.Parameters.Add(new SqlParameter("@id", Request.QueryString["id"]));
        SqlDataReader dr4 = cmd4.ExecuteReader();
        if (dr4.Read())
        {
            txtdesc.Text = dr4[4].ToString();
            txtdays.Text = dr4[5].ToString();
            ddlmodule.Items.Add(new ListItem(dr4[1].ToString(), dr4[0].ToString()));
            ddlrole.Items.Add(new ListItem(dr4[3].ToString(), dr4[2].ToString()));
            dr4.Close();
        }
        else
        {
            dr4.Close();
        }

        cn.Close();

        showdropdown();
    }
    protected void SendEmail_Insert(string toemail, string modulename)
    {
        using (MailMessage mm = new MailMessage("bhaviyagandani@gmail.com", toemail))
        {
            mm.Subject = "New Task For The Project - " + Label1.Text;

            mm.Body = "New Task is assigned for the project. The Task is mentioned below along with the Description.<br/><br/><table border='1px' style='font - family: Calibri; font - size: large; color: #000000'>" +
              "<tr>" +
               "<td>Project</td>" +
               "<td>Module</td>" +
               "<td>Description</td>" +
               "<td>Days to Complete</td>" +
              "</tr>" +
              "<tr>" +
               "<td>" + Label1.Text + "</td>" +
               "<td>" + modulename + "</td>" +
               "<td>" + txtdesc.Text + "</td>" +
               "<td>" + txtdays.Text + "</td>" +
              "</tr>" +
             "</table>";


            mm.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            NetworkCredential NetworkCred = new NetworkCredential("bhaviyagandani@gmail.com", "bhaviya0408");
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            smtp.Port = 587;
            smtp.Send(mm);
        }
    }
    protected void SendEmail_Update(string toemail, string modulename, int existing_role, int current_role)
    {
        using (MailMessage mm = new MailMessage("bhaviyagandani@gmail.com", toemail))
        {
            if(existing_role != current_role)
            {
                string existing_role_email = "";

                cn.Open();

                SqlCommand cmd = new SqlCommand("select email from role where role_id=@roleid;",cn);
                cmd.Parameters.Add(new SqlParameter("@roleid", existing_role));
                SqlDataReader dr = cmd.ExecuteReader();
                while(dr.Read())
                {
                    existing_role_email = dr[0].ToString();
                }
                dr.Close();
                cn.Close();

                mm.To.Add(existing_role_email);
            }

            cn.Open();

            string to_name = "";

            SqlCommand cmd1 = new SqlCommand("select name from role where role_id=@roleid;", cn);
            cmd1.Parameters.Add(new SqlParameter("@roleid", current_role));
            SqlDataReader dr1 = cmd1.ExecuteReader();
            while (dr1.Read())
            {
                to_name = dr1[0].ToString();
            }
            dr1.Close();

            cn.Close();

            mm.Subject = "Task Update For The Project - " + Label1.Text;

            mm.Body = "Task is updated for the project. The Task is mentioned below along with the Description.<br/><br/><table border='1px' style='font - family: Calibri; font - size: large; color: #000000'>" +
              "<tr>" +
               "<td>Project</td>" +
               "<td>Module</td>" +
               "<td>Assignee</td>" +
               "<td>Description</td>" +
               "<td>Days to Complete</td>" +
              "</tr>" +
              "<tr>" +
               "<td>" + Label1.Text + "</td>" +
               "<td>" + modulename + "</td>" +
               "<td>" + to_name + "</td>" +
               "<td>" + txtdesc.Text + "</td>" +
               "<td>" + txtdays.Text + "</td>" +
              "</tr>" +
             "</table>";


            mm.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            NetworkCredential NetworkCred = new NetworkCredential("bhaviyagandani@gmail.com", "bhaviya0408");
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            smtp.Port = 587;
            smtp.Send(mm);
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("planning.aspx?projectid=" + Request.QueryString["projectid"]);
    }
}