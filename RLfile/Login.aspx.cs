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
using System.Data.OleDb;

public partial class RLfile_Login : System.Web.UI.Page
{
    OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\OHB.mdb");
    OleDbCommand cmd;
    string str;
    OleDbDataAdapter daa=new OleDbDataAdapter();
    DataTable dtt=new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("../RLfile/Registration.aspx");
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Rlfile/Forgotpass.aspx");
    }
    protected void btn_login_Click(object sender, EventArgs e)
    {
        try
        {
            conn.Open();
            string str1="select * from RGTBL where uname='"+txt_login_username.Text+"' and pass='"+txt_login_password.Text+"'";
            daa=new OleDbDataAdapter(str1,conn);
            daa.Fill(dtt);

            if(dtt.Rows.Count > 0)
            {
                Response.Redirect("../users/Booking.aspx");
            }

        }
        catch(Exception exx)
        {
            Response.Write(exx.ToString());
        }
        finally
        {
            conn.Close();
        }
    }
}
