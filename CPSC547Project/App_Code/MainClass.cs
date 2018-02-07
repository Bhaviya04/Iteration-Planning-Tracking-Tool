using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

/// <summary>
/// Summary description for MainClass
/// </summary>
public class MainClass
{
    SqlConnection con;

    public MainClass()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public SqlConnection Connect()
    {
        con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["con"].ConnectionString);
        return con;
    }

    public bool DoesTableExist(string tableName)
    {
        string exists;
        exists = tableName;
        DataTable db1 = con.GetSchema("Tables");
        if (!(db1.Rows.Count == 0))
            return false;
        else
            return true;
    }
    public void CreateTable(string table,string createquery)
    {
        if (!DoesTableExist(table))
        {
            SqlCommand cmdcreate = new SqlCommand(createquery, con);
            cmdcreate.ExecuteNonQuery();
        }
    }
    public void DropTable(string table,string dropquery)
    {
        if (!DoesTableExist(table))
        {
            SqlCommand cmddrop = new SqlCommand(dropquery, con);
            cmddrop.ExecuteNonQuery();
        }
    }
    public void AlterTable(string alterquery)
    {
        SqlCommand cmdalter = new SqlCommand(alterquery, con);
        cmdalter.ExecuteNonQuery();
    }
    public void UpdateTable(string updatequery)
    {
        SqlCommand cmdupdate = new SqlCommand(updatequery , con);
        cmdupdate.ExecuteNonQuery();
    }
    public void InsertIntoTable(string insertquery)
    {
        SqlCommand cmdinsert = new SqlCommand(insertquery, con);
        cmdinsert.ExecuteNonQuery();
    }
    public DataSet ShowData(string showtable)
    {
        SqlDataAdapter daselect = new SqlDataAdapter(showtable,con);
        DataSet ds = new DataSet();
        daselect.Fill(ds);
        return ds;
    }
    

    public void DeleteFromTable(string deletequery)
    {
        SqlCommand cmddelete = new SqlCommand(deletequery, con);
        cmddelete.ExecuteNonQuery();
    }


    public void Email(string receiverEmailID,string fname,string email,string subject,string message)
    {
        System.Net.Mail.MailMessage myMessage = new System.Net.Mail.MailMessage();
        ////MailMessage myMessage = new MailMessage();
        myMessage.From = new MailAddress(email);
        myMessage.To.Add(receiverEmailID);
        myMessage.Subject = subject;
        // myMessage.IsBodyHtml = true;
        myMessage.Body = message;
        myMessage.Priority = MailPriority.High;
        System.Net.Mail.SmtpClient mSmtpClient = new System.Net.Mail.SmtpClient();
        mSmtpClient.UseDefaultCredentials = false;
        mSmtpClient.Credentials = new System.Net.NetworkCredential("paridemo2012@gmail.com", "paridemo");
        mSmtpClient.Port = 587;
        mSmtpClient.Host = "smtp.gmail.com";
        mSmtpClient.EnableSsl = true;
        object userstate = myMessage;
        mSmtpClient.Send(myMessage);
        
    }

    public DataSet ListDemo(string datalistcmd)
    {
        SqlDataAdapter daselectList = new SqlDataAdapter(datalistcmd, con);
        DataSet ds = new DataSet();
        daselectList.Fill(ds);
        return ds;
    }



    public static void ResizeImage(string FileNameInput, string FileNameOutput, double ResizeHeight, double ResizeWidth, ImageFormat OutputFormat)
    {
        using (System.Drawing.Image photo = new Bitmap(FileNameInput))
        {
            int sourceWidth = photo.Width;
            int sourceHeight = photo.Height;
            int sourceX = 0;
            int sourceY = 0;
            int destX = 0;
            int destY = 0;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)ResizeWidth / (float)sourceWidth);
            nPercentH = ((float)ResizeHeight / (float)sourceHeight);

            //if we have to pad the height pad both the top and the bottom
            //with the difference between the scaled height and the desired height
            if (nPercentH < nPercentW)
            {
                nPercent = nPercentH;
                destX = (int)((ResizeWidth - (sourceWidth * nPercent)) / 2);
            }
            else
            {
                nPercent = nPercentW;
                destY = (int)((ResizeHeight - (sourceHeight * nPercent)) / 2);
            }

            int newWidth = (int)(sourceWidth * nPercent);
            int newHeight = (int)(sourceHeight * nPercent);



            using (Bitmap bmp = new Bitmap(newWidth, newHeight))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.CompositingQuality = CompositingQuality.HighQuality;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                    g.DrawImage(photo, 0, 0, newWidth, newHeight);

                    if (ImageFormat.Png.Equals(OutputFormat))
                    {
                        bmp.Save(FileNameOutput, OutputFormat);
                    }
                    else if (ImageFormat.Jpeg.Equals(OutputFormat))
                    {
                        ImageCodecInfo[] info = ImageCodecInfo.GetImageEncoders();
                        EncoderParameters encoderParameters;
                        using (encoderParameters = new System.Drawing.Imaging.EncoderParameters(1))
                        {
                            // use jpeg info[1] and set quality to 90
                            encoderParameters.Param[0] = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 90L);
                            bmp.Save(FileNameOutput, info[1], encoderParameters);
                        }
                    }
                }
            }
        }
    }

    public static Image ResizeFixedSize(Image imgPhoto, int Width, int Height)
    {
        int sourceWidth = imgPhoto.Width;
        int sourceHeight = imgPhoto.Height;
        int sourceX = 0;
        int sourceY = 0;
        int destX = 0;
        int destY = 0;

        float nPercent = 0;
        float nPercentW = 0;
        float nPercentH = 0;

        nPercentW = ((float)Width / (float)sourceWidth);
        nPercentH = ((float)Height / (float)sourceHeight);

        //if we have to pad the height pad both the top and the bottom
        //with the difference between the scaled height and the desired height
        if (nPercentH < nPercentW)
        {
            nPercent = nPercentH;
            destX = (int)((Width - (sourceWidth * nPercent)) / 2);
        }
        else
        {
            nPercent = nPercentW;
            destY = (int)((Height - (sourceHeight * nPercent)) / 2);
        }

        int destWidth = (int)(sourceWidth * nPercent);
        int destHeight = (int)(sourceHeight * nPercent);

        Bitmap bmPhoto = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);
        bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

        Graphics grPhoto = Graphics.FromImage(bmPhoto);

        grPhoto.Clear(System.Drawing.ColorTranslator.FromHtml("#F4F4F4"));
        grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

        grPhoto.DrawImage(imgPhoto,
            new Rectangle(destX, destY, destWidth, destHeight),
            new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
            GraphicsUnit.Pixel);

        grPhoto.Dispose();
        return bmPhoto;
    }


}