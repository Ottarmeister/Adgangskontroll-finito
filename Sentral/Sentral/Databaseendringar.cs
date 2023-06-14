using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sentral
{
    public partial class Databaseendringar : Form
    {
        // This form handles all the database views and editing that was mentioned in the task.

        // In the database connection under, please change the connection settings to match your database.
        NpgsqlConnection conn = new NpgsqlConnection("Host=localhost;Username=postgres;Password=Kiwivin123!;Database=postgres"); //Setting up connection to database
        
        public Databaseendringar()
        {
            InitializeComponent();
            //Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("no-NO");
            texbox_Status_addstate(); // Setting initial state of user info section
            access_init_state(); // Setting initial state of access info section
        }

        // This Region handles every query that was asked for in the task
        #region RapportGenerering
        private void Generate_userinfo_Click(object sender, EventArgs e) 
        {
            conn.Open();
            NpgsqlCommand comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            if (Report_CardID.Text == "") // If nothing is selected then select everything from the user table
                comm.CommandText = "SELECT * FROM bruker";
            else
                comm.CommandText = "SELECT * FROM bruker WHERE kortid = '" + Report_CardID.Text + "'";
            NpgsqlDataReader dr = comm.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridView1.DataSource = dt;
            comm.Dispose();
            conn.Close();
        }
        private void Generate_Accesslogg_Click(object sender, EventArgs e)
        {
            conn.Open();
            NpgsqlCommand comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            if (Report_AccessLogCardID.Text == "" && Report_AccessLogDateFrom.Text == "" && Report_AccessLogDateTo.Text == "")
            {
                comm.CommandText = "SELECT * FROM forespørsler";
            }
            else
            {
                comm.CommandText = "SELECT * FROM forespørsler " +
                "WHERE kortID = '" + Report_AccessLogCardID.Text + "' " +
                "AND dato >= '" + Report_AccessLogDateFrom.Text + "' AND dato <= '" + Report_AccessLogDateTo.Text + "'";
            }

            try
            {
                NpgsqlDataReader dr = comm.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Skriv på riktig format, og pass på at ingen eller alle ruter er fyllt ut!", "Adgangslogg Feil", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
           
            comm.Dispose();
            conn.Close();

        }

        private void Generate_PassingDeniedLog_Click(object sender, EventArgs e)
        {
            conn.Open();
            NpgsqlCommand comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            if (Report_doornumber.Text == "")
            {
                comm.CommandText = "SELECT forespørsler.ForespørselID, forespørsler.Status, forespørsler.Dato, forespørsler.kortid, forespørsler.NummerID, kortleser.romnummer " +
                "FROM forespørsler, kortleser " +
                "WHERE kortleser.nummerid = forespørsler.nummerid " +
                "AND Status = 'Avvist'";
                //bruker.kortid = forespørsler.kortid AND // ska vere etter where
            }
            else
            {
                comm.CommandText = "SELECT forespørsler.ForespørselID, forespørsler.Status, forespørsler.Dato, forespørsler.kortid, forespørsler.NummerID, kortleser.romnummer " +
                    "FROM forespørsler, kortleser " +
                    "WHERE kortleser.nummerid = forespørsler.nummerid " +
                    "AND Status = 'Avvist' " +
                    "AND kortleser.nummerid = '" + Report_doornumber.Text + "'";
            }
            NpgsqlDataReader dr = comm.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridView1.DataSource = dt;
            comm.Dispose();
            conn.Close();
        }

        private void Generate_AlarmsTwoDates_Click(object sender, EventArgs e)
        {
            conn.Open();
            NpgsqlCommand comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            if (Report_DateFrom.Text == "" && Report_DateTo.Text == "")
                comm.CommandText = "SELECT * FROM alarm;";
            else
                comm.CommandText = "SELECT * FROM alarm WHERE dato >= '" + Report_DateFrom.Text + "' AND dato <= '" + Report_DateTo.Text + "'";

            try
            {
                NpgsqlDataReader dr = comm.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Skriv på riktig format, og pass på at ingen eller alle ruter er fyllt ut!", "Adgangslogg Feil", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            comm.Dispose();
            conn.Close();
        }
    

        private void Generate_FirstLastAccess_Click(object sender, EventArgs e)
        {
            conn.Open();
            NpgsqlCommand comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            if (Report_RoomNumber.Text == "")
                comm.CommandText = "SELECT * FROM adgangrom;";
            else
                comm.CommandText = "SELECT * FROM adgangrom WHERE romnummer = '" + Report_RoomNumber.Text + "'";
            NpgsqlDataReader dr = comm.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridView1.DataSource = dt;
            comm.Dispose();
            conn.Close();
        }

        private void Generate_Useraccess_Click(object sender, EventArgs e) // Custom view over all users and their cardreader access
        {
            conn.Open();
            NpgsqlCommand comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            comm.CommandText = "SELECT * FROM brukertilgang;";
            NpgsqlDataReader dr = comm.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridView1.DataSource = dt;
            comm.Dispose();
            conn.Close();
        }
        private void Generate_OnlyUserAccess_Click(object sender, EventArgs e) // Just a view of the Many to Many table with user access
        {
            conn.Open();
            NpgsqlCommand comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            comm.CommandText = "SELECT * FROM Bruker_KortleserMM;";
            NpgsqlDataReader dr = comm.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridView1.DataSource = dt;
            comm.Dispose();
            conn.Close();
        }

        #endregion RapportGenerering

        // This region handles adding/deleting and editing user info
        #region Edit User Info
        private void change_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (deleteUser_CheckBox.Checked == true)
            {
                changeUser_CheckBox.Checked = false;
                texbox_Status_delete();
                
            }
            else
            {
                texbox_Status_addstate();
            }
        }

        private void changeUser_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (changeUser_CheckBox.Checked == true)
            {
                deleteUser_CheckBox.Checked = false;
                texbox_Status_change();
                
            }
            else
            {
                texbox_Status_addstate();
            }
        }

        private void button_UserExecute_Click(object sender, EventArgs e)
        {
            conn.Open();
            NpgsqlCommand comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;

            if (deleteUser_CheckBox.Checked == false && changeUser_CheckBox.Checked == false)
            {
                if (change_LastName.Text == "" || change_Name.Text == "")
                {
                    MessageBox.Show("Pass på å fylle ut alle omrpdene med * under insetting av ny bruker!", "Feil ved innlegging av bruker", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    comm.Dispose();
                    conn.Close();
                    return;
                }
                else
                {
                    var sql = "INSERT INTO bruker(etternavn, fornavn, epost_adresse, kortid, pin, gyldighetfra, gyldighettil) VALUES(@etternavn, @fornavn, @epost_adresse, @kortid, @pin, @gyldighetfra, @gyldighettil);";
                    using var cmd = new NpgsqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("etternavn", change_LastName.Text);
                    cmd.Parameters.AddWithValue("fornavn", change_Name.Text);
                    cmd.Parameters.AddWithValue("epost_adresse", change_Mail.Text);
                    cmd.Parameters.AddWithValue("kortid", change_CardID.Text);
                    cmd.Parameters.AddWithValue("pin", change_PIN.Text);
                    if (change_ValidFrom.Text == "" || change_ValidTo.Text == "")
                    {
                       cmd.Parameters.AddWithValue("gyldighetfra", DateTime.MinValue);
                       cmd.Parameters.AddWithValue("gyldighettil", DateTime.MaxValue);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("gyldighetfra", change_ValidFrom.Text);
                        cmd.Parameters.AddWithValue("gyldighettil", change_ValidTo.Text);
                    }
                    
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                } 
            }
            else if (deleteUser_CheckBox.Checked == true)
            {
                if (change_UserId.Text == "")
                {
                    MessageBox.Show("Pass på å definer bruker id på brukeren du vil slette!", "Feil ved sletting", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    comm.Dispose();
                    conn.Close();
                    return;
                }
                else
                {
                    comm.CommandText = "DELETE FROM Bruker_KortleserMM WHERE BrukerID = '" + change_UserId.Text + "'";
                    comm.Prepare();
                    comm.ExecuteNonQuery();
                    comm.CommandText = "DELETE FROM Bruker WHERE BrukerID = '" + change_UserId.Text + "'";
                    comm.Prepare();
                    comm.ExecuteNonQuery();
                }
                    
            }
            else if (changeUser_CheckBox.Checked == true)
            {
                var sql = "UPDATE  bruker SET etternavn = @etternavn, fornavn =  @fornavn, epost_adresse = @epost_adresse, kortid = @kortid, pin =  @pin, gyldighetfra = @gyldighetfra, gyldighettil = @gyldighettil WHERE brukerid = @brukerid;";
                using var cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("etternavn", change_LastName.Text);
                cmd.Parameters.AddWithValue("fornavn", change_Name.Text);
                cmd.Parameters.AddWithValue("epost_adresse", change_Mail.Text);
                cmd.Parameters.AddWithValue("kortid", change_CardID.Text);
                cmd.Parameters.AddWithValue("pin", change_PIN.Text);
                if (change_ValidFrom.Text == "" || change_ValidTo.Text == "")
                {
                    cmd.Parameters.AddWithValue("gyldighetfra", DateTime.MinValue);
                    cmd.Parameters.AddWithValue("gyldighettil", DateTime.MaxValue);
                }
                else
                {
                    cmd.Parameters.AddWithValue("gyldighetfra", change_ValidFrom.Text);
                    cmd.Parameters.AddWithValue("gyldighettil", change_ValidTo.Text);
                }
                cmd.Parameters.AddWithValue("brukerid", Convert.ToInt32(change_UserId.Text));

                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }

            comm.Dispose();
            conn.Close();
        }

        private void texbox_Status_delete()
        {
            change_UserId.ReadOnly = false;
            change_LastName.ReadOnly = true;
            change_Name.ReadOnly = true;
            change_Mail.ReadOnly = true;
            change_CardID.ReadOnly = true;
            change_PIN.ReadOnly = true;
            change_ValidFrom.ReadOnly = true;
            change_ValidTo.ReadOnly = true;
        }

        private void texbox_Status_change()
        {
            change_UserId.ReadOnly = false;
            change_LastName.ReadOnly = false;
            change_Name.ReadOnly = false;
            change_Mail.ReadOnly = false;
            change_CardID.ReadOnly = false;
            change_PIN.ReadOnly = false;
            change_ValidFrom.ReadOnly = false;
            change_ValidTo.ReadOnly = false;
        }

        private void texbox_Status_addstate()
        {
            change_UserId.ReadOnly = true;
            change_LastName.ReadOnly = false;
            change_Name.ReadOnly = false;
            change_Mail.ReadOnly = false;
            change_CardID.ReadOnly = false;
            change_PIN.ReadOnly = false;
            change_ValidFrom.ReadOnly = false;
            change_ValidTo.ReadOnly = false;
        }
        #endregion Edit User Info

        // This region handles adding/deleting and editing user access, in case a user wants access to multiple doors/cardreaders
        #region Edit User Cardreader Access
        private void access_Delete_CheckedChanged(object sender, EventArgs e) // Setting states for delete checkmark
        {
            if (access_Delete.Checked == true)
            {
                access_Change.Checked = false;
                access_DoorID.ReadOnly = true;
                access_UserID.ReadOnly = true;
                access_ID.ReadOnly = false;
            }
            else
            {
                access_DoorID.ReadOnly = false;
                access_UserID.ReadOnly = false;
                access_ID.ReadOnly = true;
            }
        }

        private void access_Change_CheckedChanged(object sender, EventArgs e) // Setting states for the edit checkmark
        {
            if (access_Change.Checked == true)
            {
                access_Delete.Checked = false;
                access_DoorID.ReadOnly = false;
                access_UserID.ReadOnly = false;
                access_ID.ReadOnly = false;
            }
            else
            {
                access_DoorID.ReadOnly = false;
                access_UserID.ReadOnly = false;
                access_ID.ReadOnly = true;
            }
        }

        private void access_init_state() // Initial state for the access editing section
        {
            access_DoorID.ReadOnly = false;
            access_UserID.ReadOnly = false;
            access_ID.ReadOnly = true;
        }


        private void access_Button_Click(object sender, EventArgs e) // Handling of access button press
        {
            // Establish connection to database
            conn.Open();
            
            // Different cases for handling empty or incomplete queries
            if (access_Delete.Checked == false && access_Change.Checked == false)
            {
                if (access_DoorID.Text == "" || access_UserID.Text == "")
                {
                    MessageBox.Show("Pass på å fylle ut alle felta med * under insetting av ny tilgong!", "Feil ved innlegging av tilgong", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    
                    conn.Close();
                    return;
                }
                else
                {
                    var sql = "INSERT INTO bruker_kortlesermm(brukerid, nummerid) VALUES(@brukerid, @nummerid);";
                    using var cmd = new NpgsqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("brukerid", Convert.ToInt32(access_UserID.Text));
                    cmd.Parameters.AddWithValue("nummerid", access_DoorID.Text);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }

            }
            else if (access_Delete.Checked == true)
            {
                if (access_ID.Text == "")
                {
                    MessageBox.Show("Pass på å fylle ut alle felta med * ved sletting av tilgongar!", "Feil ved sletting av tilgong", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    
                    conn.Close();
                    return;
                }
                else
                {
                    var sql = "DELETE FROM Bruker_KortleserMM WHERE tilgangID = @tilgangID"; ;
                    using var cmd = new NpgsqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("tilgangID", Convert.ToInt32(access_ID.Text));
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }
                
            }
            else if (access_Change.Checked == true)
            {
                if (access_DoorID.Text == "" || access_UserID.Text == "")
                {
                    MessageBox.Show("Pass på å fylle ut alle felta med * under endring av tilgong!", "Feil ved endring av tilgong", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    
                    conn.Close();
                    return;
                }
                else
                {
                    var sql = "UPDATE  bruker_kortlesermm SET brukerid = @brukerid, nummerid =  @nummerid where tilgangid = @tilgangid;";
                    using var cmd = new NpgsqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("brukerid", Convert.ToInt32(access_UserID.Text));
                    cmd.Parameters.AddWithValue("nummerid", access_DoorID.Text);
                    cmd.Parameters.AddWithValue("tilgangid", Convert.ToInt32(access_ID.Text));

                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }
                
            }
            // Close database connetion
            
            conn.Close();
        }

        #endregion Edit User Cardreader Access


    }
}
