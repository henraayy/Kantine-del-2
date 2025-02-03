using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppTest
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            VisUkeMeny();
            VisFasteVarer();
        }

        private void VisUkeMeny()
        {
            // Henter tilkoblingsstrengen fra konfigurasjonen
            var connectionString = ConfigurationManager.ConnectionStrings["ConnCms"].ConnectionString;
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // SQL-spørring for å hente ukesmenyen for uke 1
                SqlCommand cmd = new SqlCommand(@"
            SELECT 
                ukesmeny.mandag, 
                (SELECT pris FROM retter WHERE retter.rett = ukesmeny.mandag) AS mandag_pris,
                ukesmeny.tirsdag, 
                (SELECT pris FROM retter WHERE retter.rett = ukesmeny.tirsdag) AS tirsdag_pris,
                ukesmeny.onsdag, 
                (SELECT pris FROM retter WHERE retter.rett = ukesmeny.onsdag) AS onsdag_pris,
                ukesmeny.torsdag, 
                (SELECT pris FROM retter WHERE retter.rett = ukesmeny.torsdag) AS torsdag_pris,
                ukesmeny.fredag, 
                (SELECT pris FROM retter WHERE retter.rett = ukesmeny.fredag) AS fredag_pris
            FROM ukesmeny
            WHERE ukesmeny.uke = 1;", conn);
                cmd.CommandType = CommandType.Text;

                // Utfører spørringen og leser resultatene
                SqlDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
                reader.Close();
                conn.Close();
            }

            // Sjekker om det er data i DataTable
            if (dt.Rows.Count > 0)
            {
                // Setter tekst for hver dag i uken med rett og pris
                LabelMan.Text = dt.Rows[0]["mandag"].ToString() + " | Pris: " + dt.Rows[0]["mandag_pris"].ToString() + "kr";
                LabelTir.Text = dt.Rows[0]["tirsdag"].ToString() + " | Pris: " + dt.Rows[0]["tirsdag_pris"].ToString() + "kr";
                LabelOns.Text = dt.Rows[0]["onsdag"].ToString() + " | Pris: " + dt.Rows[0]["onsdag_pris"].ToString() + "kr";
                LabelTor.Text = dt.Rows[0]["torsdag"].ToString() + " | Pris: " + dt.Rows[0]["torsdag_pris"].ToString() + "kr";
                LabelFre.Text = dt.Rows[0]["fredag"].ToString() + " | Pris: " + dt.Rows[0]["fredag_pris"].ToString() + "kr";
            }
            else
            {
                // Håndterer tilfellet hvor det ikke er noen data
                LabelMan.Text = "Ingen meny tilgjengelig";
                LabelTir.Text = "Ingen meny tilgjengelig";
                LabelOns.Text = "Ingen meny tilgjengelig";
                LabelTor.Text = "Ingen meny tilgjengelig";
                LabelFre.Text = "Ingen meny tilgjengelig";
            }
        }

        private void VisFasteVarer()
        {
            // Henter tilkoblingsstrengen fra konfigurasjonen
            var connectionString = ConfigurationManager.ConnectionStrings["ConnCms"].ConnectionString;
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // SQL-spørring for å hente faste varer
                SqlCommand cmd = new SqlCommand("SELECT vare, pris from fastevarer", conn);
                cmd.CommandType = CommandType.Text;

                // Utfører spørringen og leser resultatene
                SqlDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
                reader.Close();
                conn.Close();
            }

            // Binder dataene til ListView for faste varer
            lvFastevarer.DataSource = dt;
            lvFastevarer.DataBind();
        }
    }
}