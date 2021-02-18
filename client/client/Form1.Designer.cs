namespace client
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_ip = new System.Windows.Forms.TextBox();
            this.textBox_port = new System.Windows.Forms.TextBox();
            this.button_connect = new System.Windows.Forms.Button();
            this.logs = new System.Windows.Forms.RichTextBox();
            this.textBox_location = new System.Windows.Forms.TextBox();
            this.button_send = new System.Windows.Forms.Button();
            this.button_choosefile = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_username = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button_request = new System.Windows.Forms.Button();
            this.text_filename = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button_download = new System.Windows.Forms.Button();
            this.button_copy = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.button_delete = new System.Windows.Forms.Button();
            this.button_disconnect = new System.Windows.Forms.Button();
            this.button_public = new System.Windows.Forms.Button();
            this.make_p = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(62, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(54, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Port:";
            // 
            // textBox_ip
            // 
            this.textBox_ip.Location = new System.Drawing.Point(104, 31);
            this.textBox_ip.Name = "textBox_ip";
            this.textBox_ip.Size = new System.Drawing.Size(130, 26);
            this.textBox_ip.TabIndex = 2;
            // 
            // textBox_port
            // 
            this.textBox_port.Location = new System.Drawing.Point(104, 75);
            this.textBox_port.Name = "textBox_port";
            this.textBox_port.Size = new System.Drawing.Size(130, 26);
            this.textBox_port.TabIndex = 3;
            // 
            // button_connect
            // 
            this.button_connect.Location = new System.Drawing.Point(104, 152);
            this.button_connect.Name = "button_connect";
            this.button_connect.Size = new System.Drawing.Size(132, 34);
            this.button_connect.TabIndex = 4;
            this.button_connect.Text = "connect";
            this.button_connect.UseVisualStyleBackColor = true;
            this.button_connect.Click += new System.EventHandler(this.button_connect_Click);
            // 
            // logs
            // 
            this.logs.Location = new System.Drawing.Point(267, 31);
            this.logs.Name = "logs";
            this.logs.Size = new System.Drawing.Size(390, 456);
            this.logs.TabIndex = 5;
            this.logs.Text = "";
            // 
            // textBox_location
            // 
            this.textBox_location.Enabled = false;
            this.textBox_location.Location = new System.Drawing.Point(8, 209);
            this.textBox_location.Name = "textBox_location";
            this.textBox_location.Size = new System.Drawing.Size(226, 26);
            this.textBox_location.TabIndex = 6;
            // 
            // button_send
            // 
            this.button_send.Enabled = false;
            this.button_send.Location = new System.Drawing.Point(134, 263);
            this.button_send.Name = "button_send";
            this.button_send.Size = new System.Drawing.Size(100, 29);
            this.button_send.TabIndex = 8;
            this.button_send.Text = "Send";
            this.button_send.UseVisualStyleBackColor = true;
            this.button_send.Click += new System.EventHandler(this.button_send_Click);
            // 
            // button_choosefile
            // 
            this.button_choosefile.Enabled = false;
            this.button_choosefile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button_choosefile.Location = new System.Drawing.Point(9, 263);
            this.button_choosefile.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_choosefile.Name = "button_choosefile";
            this.button_choosefile.Size = new System.Drawing.Size(117, 29);
            this.button_choosefile.TabIndex = 9;
            this.button_choosefile.Text = "Choose File";
            this.button_choosefile.UseVisualStyleBackColor = true;
            this.button_choosefile.Click += new System.EventHandler(this.button_choosefile_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 118);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 20);
            this.label3.TabIndex = 10;
            this.label3.Text = "Username:";
            // 
            // textBox_username
            // 
            this.textBox_username.Location = new System.Drawing.Point(104, 114);
            this.textBox_username.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox_username.Name = "textBox_username";
            this.textBox_username.Size = new System.Drawing.Size(130, 26);
            this.textBox_username.TabIndex = 11;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // button_request
            // 
            this.button_request.Enabled = false;
            this.button_request.Location = new System.Drawing.Point(9, 302);
            this.button_request.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_request.Name = "button_request";
            this.button_request.Size = new System.Drawing.Size(117, 60);
            this.button_request.TabIndex = 12;
            this.button_request.Text = "Request My Files";
            this.button_request.UseVisualStyleBackColor = true;
            this.button_request.Click += new System.EventHandler(this.button_request_Click);
            // 
            // text_filename
            // 
            this.text_filename.Enabled = false;
            this.text_filename.Location = new System.Drawing.Point(8, 408);
            this.text_filename.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.text_filename.Name = "text_filename";
            this.text_filename.Size = new System.Drawing.Size(226, 26);
            this.text_filename.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.Location = new System.Drawing.Point(4, 380);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 20);
            this.label4.TabIndex = 14;
            this.label4.Text = "Filename:";
            // 
            // button_download
            // 
            this.button_download.Enabled = false;
            this.button_download.Location = new System.Drawing.Point(8, 454);
            this.button_download.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_download.Name = "button_download";
            this.button_download.Size = new System.Drawing.Size(112, 35);
            this.button_download.TabIndex = 15;
            this.button_download.Text = "Download";
            this.button_download.UseVisualStyleBackColor = true;
            this.button_download.Click += new System.EventHandler(this.button_download_Click);
            // 
            // button_copy
            // 
            this.button_copy.Enabled = false;
            this.button_copy.Location = new System.Drawing.Point(122, 454);
            this.button_copy.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_copy.Name = "button_copy";
            this.button_copy.Size = new System.Drawing.Size(112, 35);
            this.button_copy.TabIndex = 16;
            this.button_copy.Text = "Make Copy";
            this.button_copy.UseVisualStyleBackColor = true;
            this.button_copy.Click += new System.EventHandler(this.button_copy_Click);
            // 
            // button_delete
            // 
            this.button_delete.Enabled = false;
            this.button_delete.Location = new System.Drawing.Point(122, 497);
            this.button_delete.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_delete.Name = "button_delete";
            this.button_delete.Size = new System.Drawing.Size(112, 35);
            this.button_delete.TabIndex = 17;
            this.button_delete.Text = "Delete";
            this.button_delete.UseVisualStyleBackColor = true;
            this.button_delete.Click += new System.EventHandler(this.button_delete_Click);
            // 
            // button_disconnect
            // 
            this.button_disconnect.Location = new System.Drawing.Point(544, 500);
            this.button_disconnect.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button_disconnect.Name = "button_disconnect";
            this.button_disconnect.Size = new System.Drawing.Size(112, 35);
            this.button_disconnect.TabIndex = 18;
            this.button_disconnect.Text = "Disconnect";
            this.button_disconnect.UseVisualStyleBackColor = true;
            this.button_disconnect.Click += new System.EventHandler(this.button_disconnect_Click);
            // 
            // button_public
            // 
            this.button_public.Location = new System.Drawing.Point(134, 302);
            this.button_public.Name = "button_public";
            this.button_public.Size = new System.Drawing.Size(102, 60);
            this.button_public.TabIndex = 19;
            this.button_public.Text = "Request Public Files";
            this.button_public.UseVisualStyleBackColor = true;
            this.button_public.Click += new System.EventHandler(this.button_public_Click);
            // 
            // make_p
            // 
            this.make_p.Location = new System.Drawing.Point(8, 497);
            this.make_p.Name = "make_p";
            this.make_p.Size = new System.Drawing.Size(112, 35);
            this.make_p.TabIndex = 20;
            this.make_p.Text = "Make Public";
            this.make_p.UseVisualStyleBackColor = true;
            this.make_p.Click += new System.EventHandler(this.make_p_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(675, 551);
            this.Controls.Add(this.make_p);
            this.Controls.Add(this.button_public);
            this.Controls.Add(this.button_disconnect);
            this.Controls.Add(this.button_delete);
            this.Controls.Add(this.button_copy);
            this.Controls.Add(this.button_download);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.text_filename);
            this.Controls.Add(this.button_request);
            this.Controls.Add(this.textBox_username);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button_choosefile);
            this.Controls.Add(this.button_send);
            this.Controls.Add(this.textBox_location);
            this.Controls.Add(this.logs);
            this.Controls.Add(this.button_connect);
            this.Controls.Add(this.textBox_port);
            this.Controls.Add(this.textBox_ip);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_ip;
        private System.Windows.Forms.TextBox textBox_port;
        private System.Windows.Forms.Button button_connect;
        private System.Windows.Forms.RichTextBox logs;
        private System.Windows.Forms.TextBox textBox_location;
        private System.Windows.Forms.Button button_send;
        private System.Windows.Forms.Button button_choosefile;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_username;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button_request;
        private System.Windows.Forms.TextBox text_filename;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button_download;
        private System.Windows.Forms.Button button_copy;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button button_delete;
        private System.Windows.Forms.Button button_disconnect;
        private System.Windows.Forms.Button button_public;
        private System.Windows.Forms.Button make_p;
    }
}

