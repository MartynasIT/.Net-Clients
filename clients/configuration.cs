using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace clients
{
    public partial class configuration : Form
    {
        public configuration()
        {
            InitializeComponent();
        }

 

        private void configuration_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.api_saved = api.Text;
            Properties.Settings.Default.database_saved = database.Text;
            Properties.Settings.Default.Save();
        }

        private void configuration_Load(object sender, EventArgs e)
        {
            api.Text = Properties.Settings.Default.api_saved;
            database.Text = Properties.Settings.Default.database_saved;
        }
    }
}
