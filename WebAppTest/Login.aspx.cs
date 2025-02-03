using System;
using System.Configuration;
using System.Data.SqlClient;

namespace WebAppTest
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Clear();
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

               // Validere brukernavn og passord
            if (ValidateUser(username, password))
            {
                // Lagre brukernavn i session
                Session["Username"] = username;

                // Send til admin.aspx
                Response.Redirect("Admin.aspx");
            }
            else
            {
                lblMessage.Text = "Feil brukernavn eller passord.";
            }
        }

        private bool ValidateUser(string username, string password)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnCms"].ConnectionString;
            string query = "SELECT COUNT(*) FROM brukere WHERE navn = @Username AND passord = @Password";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);

                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();
                    conn.Close();

                    return count > 0; // If count > 0, finner bruker
                }
            }
        }
    }
}