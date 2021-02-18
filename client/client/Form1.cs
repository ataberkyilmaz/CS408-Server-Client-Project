using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client
{
    public partial class Form1 : Form
    {

        bool terminating = false;
        bool connected = false;
        Socket clientSocket;
        string username;
        string dw_path; 

        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            InitializeComponent();
        }

        private void button_connect_Click(object sender, EventArgs e)
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            string IP = textBox_ip.Text;
            string port = textBox_port.Text;
            username = textBox_username.Text;

            int portNum;
            if(Int32.TryParse(textBox_port.Text, out portNum))
            {
                try
                {
                    clientSocket.Connect(IP, portNum);
                    button_connect.Enabled = false;
                    textBox_location.Enabled = true;
                    button_send.Enabled = true;
                    button_disconnect.Enabled = true;
                    button_delete.Enabled = true;
                    button_choosefile.Enabled = true;
                    button_download.Enabled = true;
                    button_copy.Enabled = true;
                    button_request.Enabled = true;
                    text_filename.Enabled = true;
                    make_p.Enabled = true;
                    button_public.Enabled = true;
                    connected = true;
                    logs.AppendText("Connected to the server!\n");

                    Byte[] buffer2 = new Byte[64];
                    buffer2 = Encoding.Default.GetBytes(username);
                    clientSocket.Send(buffer2);

                    Thread receiveThread = new Thread(Receive);
                    receiveThread.Start();

                }
                catch (Exception i)
                {
                    logs.AppendText("Could not connect to the server!\n");
                }
            }
            else
            {
                logs.AppendText("Check the port\n");
            }

        }

        private void Receive()
        {
            while(connected)
            {
                try
                {
                    Byte[] buffer = new Byte[1024];
                    clientSocket.Receive(buffer);

                    string incomingMessage = Encoding.Default.GetString(buffer);
                    incomingMessage = incomingMessage.Substring(0, incomingMessage.IndexOf("\0"));
                    if (incomingMessage.Length != 0)
                    {
                        if (incomingMessage.Length >= 9 && incomingMessage.Substring(0, 9) == "download~")
                        {
                            string download_filename = incomingMessage.Substring(9,incomingMessage.IndexOf("|")-9);
                            string download_path = dw_path + "\\" + download_filename;

                            StreamWriter sr = File.CreateText(download_path);

                            sr.Write(incomingMessage.Substring(incomingMessage.IndexOf("|") + 1));
                            sr.Close();
                            logs.AppendText("Your file, " + download_filename + ", is downloaded!\n");
                        }
                        else 
                        logs.AppendText("Server: " + incomingMessage + "\n");
                    }
                }
                catch
                {
                    if (!terminating)
                    {
                        logs.AppendText("The server has disconnected\n");
                        button_connect.Enabled = true;
                        textBox_location.Enabled = false;
                        button_send.Enabled = false;
                    }

                    clientSocket.Close();
                    connected = false;
                }

            }
        }

        private void Form1_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            connected = false;
            terminating = true;
            Environment.Exit(0);
        }

        private void button_send_Click(object sender, EventArgs e)
        {
            int size = -1;
            string filename = textBox_location.Text.Substring(textBox_location.Text.LastIndexOf('\\')+1);
               /* Byte[] buffer = new Byte[64];
                buffer = Encoding.Default.GetBytes(message);
                clientSocket.Send(buffer); */
            try
            {
                string text = File.ReadAllText(textBox_location.Text);
                string text_final = filename + "~" + text;
                size = text_final.Length;
                Byte[] buffer3 = new Byte[size];
                buffer3 = Encoding.Default.GetBytes(text_final);
                clientSocket.Send(buffer3);
                //logs.AppendText("valla yolladım.\n");
            }
            catch
            {
            }
        }

        private void button_choosefile_Click(object sender, EventArgs e)
        {
            int size = -1;
            textBox_location.Clear();
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string file = openFileDialog1.FileName;
                textBox_location.AppendText(file);
            }
        }

        private void button_request_Click(object sender, EventArgs e)
        {
            try
            {
                int size2 = -1;
                string toSend = "request@ " + username;
                size2 = toSend.Length;
                Byte[] buffer_b = new Byte[size2];
                buffer_b = Encoding.Default.GetBytes(toSend);
                clientSocket.Send(buffer_b);
                //logs.AppendText(toSend);
            }
            catch (Exception i)
            {
                //logs.AppendText(i.ToString());
            }
        }

        private void button_download_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName = text_filename.Text;
                string message = "";
                DialogResult result2 = folderBrowserDialog1.ShowDialog(); // Show the dialog.
                if (result2 == DialogResult.OK) // Test result.
                {
                    dw_path = folderBrowserDialog1.SelectedPath;
                    if (fileName.IndexOf("@") != -1)
                    {
                        message = "dlother+" + fileName.Substring(0, fileName.IndexOf("@")) 
                            + "~" + fileName.Substring(fileName.IndexOf("@") + 1);
                    }
                    else
                        message = "download+" + fileName;
                }
                if (message != "")
                {
                    int size_d = message.Length;
                    Byte[] buffer_d = new Byte[size_d];
                    buffer_d = Encoding.Default.GetBytes(message);
                    clientSocket.Send(buffer_d);
                }
            }
            catch (Exception i)
            {
                //logs.AppendText(i.ToString());
            }
        }

        private void button_copy_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName = text_filename.Text;      
                string message = "copy+" + fileName; 
            
                int size_d = message.Length;
                Byte[] buffer_d = new Byte[size_d];
                buffer_d = Encoding.Default.GetBytes(message);
                clientSocket.Send(buffer_d);
            }
            catch (Exception i)
            {
                //logs.AppendText(i.ToString());
            }
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName = text_filename.Text;
                string message = "delete+" + fileName;

                int size_d = message.Length;
                Byte[] buffer_d = new Byte[size_d];
                buffer_d = Encoding.Default.GetBytes(message);
                clientSocket.Send(buffer_d);
            }
            catch (Exception i)
            {
                //logs.AppendText(i.ToString());
            }
        }

        private void button_disconnect_Click(object sender, EventArgs e)
        {
            button_disconnect.Enabled = false;
            button_delete.Enabled = false;
            button_copy.Enabled = false;
            button_download.Enabled = false;
            button_request.Enabled = false;
            button_choosefile.Enabled = false;
            text_filename.Enabled = false;
            make_p.Enabled = false;
            button_public.Enabled = false;
            logs.AppendText("Disconnecting...\n");
            connected = false;
            button_connect.Enabled = true;
            clientSocket.Close();
        }

        private void button_public_Click(object sender, EventArgs e)
        {
            try
            {
                int size2 = -1;
                string toSend = "requestp@";
                size2 = toSend.Length;
                Byte[] buffer_b = new Byte[size2];
                buffer_b = Encoding.Default.GetBytes(toSend);
                clientSocket.Send(buffer_b);
                //logs.AppendText(toSend);
            }
            catch (Exception i)
            {
                //logs.AppendText(i.ToString());
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void make_p_Click(object sender, EventArgs e)
        {
            try
            {
                int size2 = -1;
                string fileName = text_filename.Text;
                string toSend = "makep@" + fileName;
                size2 = toSend.Length;
                Byte[] buffer_b = new Byte[size2];
                buffer_b = Encoding.Default.GetBytes(toSend);
                clientSocket.Send(buffer_b);
                //logs.AppendText(toSend);
            }
            catch (Exception i)
            {
                //logs.AppendText(i.ToString());
            }
        }
    }
}
