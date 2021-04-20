using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace clients
{
    public partial class data_window : Form
    {
        private String DATABASE = "Data Source=DESKTOP-9HN26R3\\MSSQLSERVER1;Initial Catalog=master;Integrated Security=True";
        private string DB_ERROR = "Problem with database";
        public data_window()
        {
            InitializeComponent();
            populate_data();
        }

        private void populate_data()
        {
            SqlConnection connection = new SqlConnection(DATABASE);
            connection.Open();
            SqlCommand cmd = new SqlCommand("select * from dbo.Clients", connection);
            SqlDataReader reader;

            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    {
                        string name = reader.GetString(reader.GetOrdinal("name")).ToString();
                        string address = reader.GetString(reader.GetOrdinal("address")).ToString();
                        string post = reader.GetString(reader.GetOrdinal("post_code")).ToString();
                        clients.Rows.Add(name, address, post);
                    
                    };

                }
                reader.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(DB_ERROR);
            }
        }
    }
}
