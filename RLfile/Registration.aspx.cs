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

public partial class RLfile_Registration : System.Web.UI.Page
{
    // Database connection string
    private OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\OHB.mdb");
    private OleDbCommand cmd;
    private OleDbDataAdapter da;
    private DataSet ds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) // Prevents reloading logic on postback
        {
            GenerateID();
        }
    }

    private void GenerateID()
    {
        try
        {
            // Open connection
            con.Open();

            // Query to fetch the maximum ID
            string query = "SELECT MAX(ID) AS ID FROM RGTBL";
            da = new OleDbDataAdapter(query, con);
            da.Fill(ds);

            int id1 = 1; // Default ID
            if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["ID"] != DBNull.Value)
            {
                id1 = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString()) + 1;
            }

            lblid.Text = id1.ToString();
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('Error generating ID: " + ex.Message + "')</script>");
        }
        finally
        {
            // Ensure connection is closed
            con.Close();
        }
    }

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        try
        {
            // Convert mobile to integer if necessary
            int mobile;
            if (!int.TryParse(txt_mobile.Text, out mobile))
            {
                Response.Write("<script>alert('Invalid mobile number. Please enter a valid number.')</script>");
                return;
            }

            // Prepare the SQL query
            string str1 = "insert into RGTBL (ID, uname, mobile, email, pass) values (" +
                lblid.Text + ", '" + txt_username.Text + "', " + mobile + ", '" + txt_email.Text + "', '" + txt_password.Text + "')";

            System.Diagnostics.Debug.WriteLine("Executing Query: " + str1);

            // Execute the query
            cmd = new OleDbCommand(str1, con);
            con.Open();
            cmd.ExecuteNonQuery();

            Response.Write("<script>alert('Registration Successfully')</script>");
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('Error: " + ex.Message + "')</script>");
        }
        finally
        {
            con.Close();
        }
    }

}