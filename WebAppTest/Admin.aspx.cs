using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WebAppTest
{
    public partial class Admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if user is logged in
            if (Session["Username"] == null)
            {
                // Redirect to Login.aspx if not logged in
                Response.Redirect("Login.aspx");
            }
            else
            {
                // Only bind the GridView controls if it's not a postback
                if (!IsPostBack)
                {
                    BindGridViewRetter();
                    BindGridViewUkesmeny();
                    PopulateWeekDropdown();
                }
            }
        }

        // Database connection string
        private string connectionString = ConfigurationManager.ConnectionStrings["ConnCms"].ConnectionString;

        // Populate the week dropdown
        private void PopulateWeekDropdown()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT DISTINCT uke FROM ukesmeny ORDER BY uke";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);


            }
        }

        // Set the selected week as the current week
        protected void btnSetCurrentWeek_Click(object sender, EventArgs e)
        {
           

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Reset all weeks to not be the current week
                string resetQuery = "UPDATE ukesmeny SET denne_uken = 0";
                SqlCommand resetCmd = new SqlCommand(resetQuery, conn);
                conn.Open();
                resetCmd.ExecuteNonQuery();

                // Set the selected week as the current week
                string updateQuery = "UPDATE ukesmeny SET denne_uken = 1 WHERE uke = @uke";
                SqlCommand updateCmd = new SqlCommand(updateQuery, conn);
                updateCmd.Parameters.AddWithValue("@uke", selectedWeek);
                updateCmd.ExecuteNonQuery();
                conn.Close();
            }

            BindGridViewUkesmeny();
        }

        // Add a new week
        protected void btnAddNewWeek_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Get the highest week number and increment it by 1
                string maxWeekQuery = "SELECT MAX(uke) FROM ukesmeny";
                SqlCommand maxWeekCmd = new SqlCommand(maxWeekQuery, conn);
                conn.Open();
                int newWeek = (maxWeekCmd.ExecuteScalar() as int? ?? 0) + 1;

                // Insert the new week
                string insertQuery = "INSERT INTO ukesmeny (mandag, tirsdag, onsdag, torsdag, fredag, uke, denne_uken) VALUES ('', '', '', '', '', @uke, 0)";
                SqlCommand insertCmd = new SqlCommand(insertQuery, conn);
                insertCmd.Parameters.AddWithValue("@uke", newWeek);
                insertCmd.ExecuteNonQuery();
                conn.Close();
            }

            PopulateWeekDropdown();
            BindGridViewUkesmeny();
        }

        // Handle week dropdown selection change
        protected void ddlWeeks_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGridViewUkesmeny();
        }

        // Bind the GridView for retter
        private void BindGridViewRetter()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT id, rett, pris FROM retter";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                GridViewRetter.DataSource = dt;
                GridViewRetter.DataBind();
            }
        }

        // Bind the GridView for ukesmeny
        private void BindGridViewUkesmeny()
        {
            int selectedWeek = Convert.ToInt32(ddlWeeks.SelectedValue);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT id, mandag, tirsdag, onsdag, torsdag, fredag, uke, denne_uken FROM ukesmeny WHERE uke = @uke";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@uke", selectedWeek);
                DataTable dt = new DataTable();
                da.Fill(dt);

                GridViewUkesmeny.DataSource = dt;
                GridViewUkesmeny.DataBind();
            }
        }

        // Handle RowEditing for retter
        protected void GridViewRetter_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            GridViewRetter.EditIndex = e.NewEditIndex;
            BindGridViewRetter();
        }

        // Handle RowCancelingEdit for retter
        protected void GridViewRetter_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            GridViewRetter.EditIndex = -1;
            BindGridViewRetter();
        }

        // Handle RowUpdating for retter
        protected void GridViewRetter_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(GridViewRetter.DataKeys[e.RowIndex].Value);
            string rett = e.NewValues["rett"].ToString();
            decimal pris = Convert.ToDecimal(e.NewValues["pris"]);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE retter SET rett = @rett, pris = @pris WHERE id = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@rett", rett);
                cmd.Parameters.AddWithValue("@pris", pris);
                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            GridViewRetter.EditIndex = -1;
            BindGridViewRetter();
        }

        // Handle RowEditing for ukesmeny
        protected void GridViewUkesmeny_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            GridViewUkesmeny.EditIndex = e.NewEditIndex;
            BindGridViewUkesmeny();
        }

        // Handle RowCancelingEdit for ukesmeny
        protected void GridViewUkesmeny_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            GridViewUkesmeny.EditIndex = -1;
            BindGridViewUkesmeny();
        }

        // Handle RowUpdating for ukesmeny
        protected void GridViewUkesmeny_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(GridViewUkesmeny.DataKeys[e.RowIndex].Value);
            string mandag = e.NewValues["mandag"].ToString();
            string tirsdag = e.NewValues["tirsdag"].ToString();
            string onsdag = e.NewValues["onsdag"].ToString();
            string torsdag = e.NewValues["torsdag"].ToString();
            string fredag = e.NewValues["fredag"].ToString();
            int uke = Convert.ToInt32(e.NewValues["uke"]);
            bool denneUken = Convert.ToBoolean(e.NewValues["denne_uken"]);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE ukesmeny SET mandag = @mandag, tirsdag = @tirsdag, onsdag = @onsdag, torsdag = @torsdag, fredag = @fredag, uke = @uke, denne_uken = @denne_uken WHERE id = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@mandag", mandag);
                cmd.Parameters.AddWithValue("@tirsdag", tirsdag);
                cmd.Parameters.AddWithValue("@onsdag", onsdag);
                cmd.Parameters.AddWithValue("@torsdag", torsdag);
                cmd.Parameters.AddWithValue("@fredag", fredag);
                cmd.Parameters.AddWithValue("@uke", uke);
                cmd.Parameters.AddWithValue("@denne_uken", denneUken);
                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            GridViewUkesmeny.EditIndex = -1;
            BindGridViewUkesmeny();
        }
    }
}