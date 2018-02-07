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

public partial class Backend_costresults : System.Web.UI.Page
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
                    showchart();
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
    protected void showchart()
    {
        cn.Open();

        int i = 1;
        int j = 1;

        string[] colors = { "#CD5C5C", "#000080", "#008080", "#008000", "#000000", "#C0C0C0", "#95B75D", "#3FBAE4", "FEA223" };

        StringBuilder sb = new StringBuilder();

        SqlCommand cmd1 = new SqlCommand("select count(cost_id) from cost where project_id=@projectid;", cn);
        cmd1.Parameters.Add(new SqlParameter("@projectid", Convert.ToInt32(Request.QueryString["projectid"])));
        int count = Convert.ToInt32(cmd1.ExecuteScalar());

        if (count != 0)
        {
            int total_cost = 0;
            SqlCommand cmd = new SqlCommand("select * from cost where project_id=@projectid;", cn);
            cmd.Parameters.Add(new SqlParameter("@projectid", Convert.ToInt32(Request.QueryString["projectid"])));
            SqlDataReader dr = cmd.ExecuteReader();
            sb.Append("var morrisCharts = function () {\r\n");
            sb.Append("Morris.Donut({\r\n");
            sb.Append("element: 'morris-donut-example',\r\n");
            sb.Append("data: [\r\n");
            while (dr.Read())
            {
                if (i == count)
                {
                    sb.Append("{ label: '" + dr[2].ToString() + "', value: " + dr[3].ToString() + " }\r\n");
                }
                else
                {
                    sb.Append("{ label: '" + dr[2].ToString() + "', value: " + dr[3].ToString() + " },\r\n");
                }
                i++;
                total_cost = total_cost + Convert.ToInt32(dr[3]);
            }


            dr.Close();

            sb.Append("],\r\n");
            sb.Append("colors: [");

            SqlDataReader dr1 = cmd.ExecuteReader();
            while (dr1.Read())
            {
                if (j == count)
                {
                    sb.Append("'" + colors[j] + "'");
                }
                else
                {
                    sb.Append("'" + colors[j] + "',");
                }
                j++;
            }
            dr1.Close();

            sb.Append("]\r\n");
            sb.Append("});\r\n");
            sb.Append("}();\r\n");

            cn.Close();

            FileInfo file2 = new FileInfo(Server.MapPath("js/cost_chart.js"));
            file2.Delete();

            System.IO.File.WriteAllText(Server.MapPath("js/cost_chart.js"), sb.ToString());

            StringBuilder sb1 = new StringBuilder();

            sb1.Append("<script type='text/javascript' src='js/cost_chart.js'></script>");
            Literal1.Text = sb1.ToString();
            Label3.Visible = true;
            Label3.Text = total_cost.ToString();
            Label2.Visible = false;
        }
        else
        {
            Label2.Visible = true;
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("cost.aspx?projectid=" + Request.QueryString["projectid"]);
    }
}