
using System;

namespace clients
{
    partial class ClientWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.import_clients = new System.Windows.Forms.Button();
            this.update_post_codes = new System.Windows.Forms.Button();
            this.show_clients = new System.Windows.Forms.Button();
            this.Configuration = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // import_clients
            // 
            this.import_clients.Location = new System.Drawing.Point(388, 271);
            this.import_clients.Name = "import_clients";
            this.import_clients.Size = new System.Drawing.Size(207, 87);
            this.import_clients.TabIndex = 0;
            this.import_clients.Text = "Importuoti klientus";
            this.import_clients.UseVisualStyleBackColor = true;
            this.import_clients.Click += new System.EventHandler(this.store_data_Click);
            // 
            // update_post_codes
            // 
            this.update_post_codes.Location = new System.Drawing.Point(623, 271);
            this.update_post_codes.Name = "update_post_codes";
            this.update_post_codes.Size = new System.Drawing.Size(206, 87);
            this.update_post_codes.TabIndex = 1;
            this.update_post_codes.Text = "Atnaujinti pašto indeksus";
            this.update_post_codes.UseVisualStyleBackColor = true;
            this.update_post_codes.Click += new System.EventHandler(this.update_posts_Click);
            // 
            // show_clients
            // 
            this.show_clients.AccessibleName = "clients_button";
            this.show_clients.Location = new System.Drawing.Point(878, 271);
            this.show_clients.Name = "show_clients";
            this.show_clients.Size = new System.Drawing.Size(211, 87);
            this.show_clients.TabIndex = 2;
            this.show_clients.Text = "Klientų sąrašas";
            this.show_clients.UseVisualStyleBackColor = true;
            this.show_clients.Click += new System.EventHandler(this.clients_button_Click);
            // 
            // Configuration
            // 
            this.Configuration.AccessibleName = "config";
            this.Configuration.Location = new System.Drawing.Point(1129, 271);
            this.Configuration.Name = "Configuration";
            this.Configuration.Size = new System.Drawing.Size(179, 87);
            this.Configuration.TabIndex = 3;
            this.Configuration.Text = "Configuration";
            this.Configuration.UseVisualStyleBackColor = true;
            this.Configuration.Click += new System.EventHandler(this.Configuration_Click);
            // 
            // ClientWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1663, 873);
            this.Controls.Add(this.Configuration);
            this.Controls.Add(this.show_clients);
            this.Controls.Add(this.update_post_codes);
            this.Controls.Add(this.import_clients);
            this.Name = "ClientWindow";
            this.Text = "ClientWindow";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button import_clients;
        private System.Windows.Forms.Button update_post_codes;
        private System.Windows.Forms.Button show_clients;
        private System.Windows.Forms.Button Configuration;
    }
}

