using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace server
{
    public partial class Form1 : Form
    {
        Socket serverSocket;
        string db_path;
        Dictionary<Socket, string> clientSockets = new Dictionary<Socket, string>();
    

        bool terminating = false;
        bool listening = false;
        FileStream dbstream;

        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            InitializeComponent();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            listening = false;
            terminating = true;
            Environment.Exit(0);
        }

        private void button_connect_Click(object sender, EventArgs e)
        {
            int serverPort;

            if (Int32.TryParse(textBox_port.Text, out serverPort))
            {
                dbstream = new FileStream(db_path + "\\db.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, serverPort);
                serverSocket.Bind(endPoint);
                serverSocket.Listen(3);

                listening = true;
                button_connect.Enabled = false;
                logs.Enabled = true;
                button_disconnect.Enabled = true;
                button_browse.Enabled = false;

                Thread acceptThread = new Thread(Accept);
                acceptThread.Start();

                logs.AppendText("Started listening on port: " + serverPort + "\n");

            }
            else
            {
                logs.AppendText("Please check port number \n");
            }
        }
        private void addToDatabase(string s)
        {
            
            StreamWriter sw = new StreamWriter(dbstream);
            
            sw.BaseStream.Seek(0, SeekOrigin.End);
            sw.WriteLine(s);
            sw.Flush();
            
        }

        private int countSameFiles(string myUsername, string myFilename)
        {
           
            int count = 0;
            string s = myUsername + " | " + myFilename;
            StreamReader sr2 = new StreamReader(dbstream);
            int length = s.Length;
            sr2.BaseStream.Position = 0;
            sr2.DiscardBufferedData();
            string line;
            while ((line = sr2.ReadLine()) != null)
            {
                if (line.Length >= s.Length && line.Substring(0,length) == s)
                {
                    count++;
                }
            }
            return count;
        }

        private bool CheckFile(string message, Socket Client)
        {
            string line1;
            string clientName = clientSockets[Client];
            int clientLen = clientName.Length;
            int fileLen = message.Length;

            StreamReader sr_d = new StreamReader(dbstream);
            sr_d.BaseStream.Position = 0;
            sr_d.DiscardBufferedData();

            while ((line1 = sr_d.ReadLine()) != null)
            {
                string us = line1.Substring(0, clientLen);
                if (us == clientName)
                {
                    if(line1.Length > fileLen + clientLen)
                    {
                        string nm = line1.Substring(line1.IndexOf("|") + 2, fileLen);
                        if (nm == message)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private char CheckPermission(string message, string username)
        {
            StreamReader sr_d = new StreamReader(dbstream);
            sr_d.BaseStream.Position = 0;
            sr_d.DiscardBufferedData();
            string line;
            string clientName = username;
            int clientLen = clientName.Length;
            int fileLen = message.Length;

            while ((line = sr_d.ReadLine()) != null)
            {
                string us = line.Substring(0, clientLen);
                if (us == clientName)
                {
                    if (line.Length > fileLen + clientLen)
                    {
                        string nm = line.Substring(line.IndexOf("|") + 2, fileLen);
                        if (nm == message)
                        {
                            return line[line.Length - 1];
                        }
                    }
                }
            }
            return 'a';
        }

        private int CheckLine(string message, Socket Client)
        {
            string line1;
            int line = 0;
            string clientName = clientSockets[Client];
            int clientLen = clientName.Length;
            int fileLen = message.Length;

            StreamReader sr_d = new StreamReader(dbstream);
            sr_d.BaseStream.Position = 0;
            sr_d.DiscardBufferedData();

            while ((line1 = sr_d.ReadLine()) != null)
            {
                line++;
                string us = line1.Substring(0, clientLen);
                if (us == clientName)
                {
                    if (line1.Length > fileLen + clientLen)
                    {
                        string nm = line1.Substring(line1.IndexOf("|") + 2, fileLen);
                        if (nm == message)
                        {
                            return line;
                        }
                    }
                }
            
            }
            return 0;
        }

        private void File_DeleteLine(int Line)
        {
            string sb = "";
            StreamReader sr = new StreamReader(dbstream);
            sr.BaseStream.Position = 0;
            sr.DiscardBufferedData();
            int Countup = 0;
            while (!sr.EndOfStream)
            {
                Countup++;
                if (Countup != Line)
                {
                           
                    sb += sr.ReadLine() + "\n";
                    
                }
                else
                {
                    sr.ReadLine();
                }
            }
            dbstream.SetLength(0);
            dbstream.Flush();
            StreamWriter sw2 = new StreamWriter(dbstream);
            sw2.Write(sb);
            sw2.Flush();
        }

        private void File_ChangeLinePublic(int Line)
        {
            string sb = "";
            StreamReader sr = new StreamReader(dbstream);
            sr.BaseStream.Position = 0;
            sr.DiscardBufferedData();
            int Countup = 0;
            while (!sr.EndOfStream)
            {
                Countup++;
                if (Countup != Line)
                {

                    sb += sr.ReadLine() + "\n";

                }
                else
                {
                    string temp = sr.ReadLine();
                    temp = temp.Substring(0, temp.Length - 1) + "p\n";
                    sb += temp;
                
                }
            }
            dbstream.SetLength(0);
            dbstream.Flush();
            StreamWriter sw2 = new StreamWriter(dbstream);
            sw2.Write(sb);
            sw2.Flush();
        }


        private void Accept()
        {
            while (listening)
            {
                try
                {
                    Socket newClient = serverSocket.Accept();
                    Thread usernameThread = new Thread(() => getUsername(newClient)); // updated
                    usernameThread.Start();

                    Thread receiveThread = new Thread(() => Receive(newClient)); // updated
                    receiveThread.Start();
                }
                catch
                {
                    if (terminating)
                    {
                        listening = false;
                    }
                    else
                    {
                        logs.AppendText("The socket stopped working.\n");
                    }

                }
            }
        }

        private void Receive(Socket thisClient) // updated
        {
            bool connected = true;

            while (connected && !terminating)
            {
                try
                {
                    Byte[] buffer = new Byte[1024];
                    thisClient.Receive(buffer);

                    string incomingMessage = Encoding.Default.GetString(buffer);
                    incomingMessage = incomingMessage.Substring(0, incomingMessage.IndexOf("\0"));
                    //logs.AppendText(incomingMessage + "\n");
                    if (incomingMessage != "")
                    {
                        if (incomingMessage.Length >= 8 && incomingMessage.Substring(0, 8) == "request@")
                        {
                            string req_user = incomingMessage.Substring(9);
                            StreamReader sr_req = new StreamReader(dbstream);
                            sr_req.BaseStream.Position = 0;
                            sr_req.DiscardBufferedData();

                            string line, message = "Your uploaded files are:\n";
                            int len = req_user.Length;
                            int count = 0;
                            while ((line = sr_req.ReadLine()) != null)
                            {
                                if (line.Substring(0, len) == req_user)
                                {
                                    string req_filename = line.Substring(incomingMessage.IndexOf("|") + 1);
                                    req_filename = req_filename.Substring(0, req_filename.Length - 1);
                                    message += req_filename + "\n";
                                    count++;
                                }
                            }
                            if (count == 0)
                            {
                                message = "You don't have any files uploaded!\n";
                            }
                            sendMessage(message, thisClient);
                        }
                        else if (incomingMessage == "requestp@")
                        {
                            StreamReader sr_req = new StreamReader(dbstream);
                            sr_req.BaseStream.Position = 0;
                            sr_req.DiscardBufferedData();

                            string line, message = "Public files on the server are:\n";
                            int count = 0;
                            while ((line = sr_req.ReadLine()) != null)
                            {
                                if (line[line.Length - 1] == 'p')
                                {
                                    string req_filename = line.Substring(0, line.Length - 1);
                                    message += req_filename + "\n";
                                    count++;
                                }
                            }
                            if (count == 0)
                            {
                                message = "There are no public files on the server!\n";
                            }
                            sendMessage(message, thisClient);
                        }
                        else if (incomingMessage.Length >= 8 && incomingMessage.Substring(0, 8) == "download")
                        {
                            int size_d;
                            int firstPlus = incomingMessage.IndexOf("+");
                            string filename = incomingMessage.Substring(firstPlus + 1);
                            string filename2 = filename.Substring(0, filename.IndexOf("."));

                            string text;
                            if (CheckFile(filename2, thisClient))
                            {
                                text = "download~" + filename + "|";
                                text += File.ReadAllText(db_path + "\\" + clientSockets[thisClient] + "_" + filename);

                            }
                            else
                            {
                                text = "You don't have such a file in the database or you don't have permission!\n";
                            }
                            size_d = text.Length;
                            Byte[] buffer_d = new Byte[size_d];
                            buffer_d = Encoding.Default.GetBytes(text);
                            thisClient.Send(buffer_d);
                        }
                        else if (incomingMessage.Length >= 7 && incomingMessage.Substring(0, 7) == "dlother")
                        {
                            int size_d;
                            int firstPlus = incomingMessage.IndexOf("+");
                            int dalga = incomingMessage.IndexOf("~");
                            string username = incomingMessage.Substring(firstPlus + 1, dalga - firstPlus - 1);
                            string filename = incomingMessage.Substring(dalga + 1);

                            string text;
                            if (CheckPermission(filename, username) == 'p')
                            {
                                text = "download~" + filename + "|";
                                text += File.ReadAllText(db_path + "\\" + username + "_" + filename);

                            }
                            else
                            {
                                text = "There is no file in the database or the file is not public!\n";
                            }
                            size_d = text.Length;
                            Byte[] buffer_d = new Byte[size_d];
                            buffer_d = Encoding.Default.GetBytes(text);
                            thisClient.Send(buffer_d);
                        }
                        else if (incomingMessage.Length >= 4 && incomingMessage.Substring(0, 4) == "copy")
                        {
                            int size_d;
                            int firstPlus = incomingMessage.IndexOf("+");
                            string filename = incomingMessage.Substring(firstPlus + 1);
                            string filename2 = filename.Substring(0, filename.IndexOf("."));

                            string text;
                            if (CheckFile(filename2, thisClient))
                            {
                                text = File.ReadAllText(db_path + "\\" + clientSockets[thisClient] + "_" + filename);

                                int count = countSameFiles(clientSockets[thisClient], filename2);

                                string filename3 = filename.Substring(0, filename.IndexOf(".")) + "-" + count.ToString() + filename.Substring(filename.IndexOf("."));
                                string path2 = db_path + "\\" + clientSockets[thisClient] + "_" + filename3;
                                StreamWriter sr = File.CreateText(path2);

                                sr.Write(incomingMessage.Substring(incomingMessage.IndexOf("~") + 1));
                                sr.Close();

                                logs.AppendText(filename2 + " is copied to database by " + clientSockets[thisClient] + "!\n");
                                string time = DateTime.Now.ToString("HH:mm:ss");
                                string size = text.Length.ToString();
                                char permission = CheckPermission(filename2, clientSockets[thisClient]);

                                if (permission == 'a')
                                {
                                    sendMessage("The file you wanted to copy does not exist or you don't have permission!\n", thisClient);
                                }
                                else
                                {
                                    string toAdd = clientSockets[thisClient] + " | " + filename3 + " | " + time + " | " + size + " " + permission;
                                    addToDatabase(toAdd);

                                    sendMessage("Your file has been copied as " + filename3 + "\n", thisClient);
                                }
                            }
                            else
                            {
                                sendMessage("The file you wanted to copy does not exist or you don't have permission!\n", thisClient);
                            }
                        }
                        else if (incomingMessage.Length >= 6 && incomingMessage.Substring(0, 6) == "makep@")
                        {
                            string filename = incomingMessage.Substring(6);
                            string username = clientSockets[thisClient];
                            if (CheckFile(filename, thisClient))
                            {
                                if (CheckPermission(filename, username) == 'p')
                                {
                                    sendMessage("The file " + filename + " is already public!\n", thisClient);
                                }
                                else
                                {
                                    int deleteLine = CheckLine(filename, thisClient);
                                    File_ChangeLinePublic(deleteLine);
                                    logs.AppendText("The file " + filename + " was made public by " + clientSockets[thisClient] + "\n");
                                    sendMessage("Your file " + filename + " is made public now!\n", thisClient);
                                }
                            }
                            else
                                sendMessage("The file you wanted to make public does not exist or you don't have permission!\n", thisClient);

                        }
                        else if (incomingMessage.Length >= 6 && incomingMessage.Substring(0, 6) == "delete")
                        {

                            int firstPlus = incomingMessage.IndexOf("+");
                            string filename = incomingMessage.Substring(firstPlus + 1);

                            string path_delete = db_path + "\\" + clientSockets[thisClient] + "_" + filename;
                            if (CheckFile(filename, thisClient))
                            {
                                int deleteLine = CheckLine(filename, thisClient);
                              //  logs.AppendText(deleteLine.ToString());
                                File_DeleteLine(deleteLine);
                                File.Delete(path_delete);
                                logs.AppendText("The file " + filename + " was deleted from the database by " + clientSockets[thisClient] + "\n");
                                sendMessage("Your file " + filename + " is deleted from the database!\n", thisClient);
                            }
                            else
                            {
                                sendMessage("The file you wanted to delete does not exist or you don't have permission!\n", thisClient);
                            }
                        }
                        else
                        {
                            int idx = incomingMessage.IndexOf("~");
                            string userName = clientSockets[thisClient];

                            string filename = incomingMessage.Substring(0, idx);
                            int fileDot = filename.IndexOf(".");
                            string path = db_path + "\\" + userName + "_" + filename;
                            if (!File.Exists(path))
                            {
                                StreamWriter sr = File.CreateText(path);

                                sr.Write(incomingMessage.Substring(incomingMessage.IndexOf("~") + 1));
                                sr.Close();

                                logs.AppendText(filename + " is added to database!\n");
                                string time = DateTime.Now.ToString("HH:mm:ss");
                                string size = incomingMessage.Substring(incomingMessage.IndexOf("~") + 1).Length.ToString();
                                string toAdd = userName + " | " + filename + " | " + time + " | " + size + " r";
                                addToDatabase(toAdd);
                            }
                            else
                            {

                                int count = countSameFiles(userName, filename.Substring(0, fileDot));

                                string filename2 = filename.Substring(0, filename.IndexOf(".")) + "-" + count.ToString() + filename.Substring(filename.IndexOf("."));
                                string path2 = db_path + "\\" + userName + "_" + filename2;
                                StreamWriter sr = File.CreateText(path2);

                                sr.Write(incomingMessage.Substring(incomingMessage.IndexOf("~") + 1));
                                sr.Close();


                                logs.AppendText(filename2 + " is added to database!\n");
                                string time = DateTime.Now.ToString("HH:mm:ss");
                                string size = incomingMessage.Substring(incomingMessage.IndexOf("~") + 1).Length.ToString();
                                string toAdd = userName + " | " + filename2 + " | " + time + " | " + size + " r";
                                addToDatabase(toAdd);
                            }
                        }
                    }
                }
                catch(Exception i)
                {
                    //logs.AppendText(i.ToString());
                    if (!terminating)
                    {
                        logs.AppendText(clientSockets[thisClient] + " has disconnected\n");
                    }
                    thisClient.Close();
                    clientSockets.Remove(thisClient);
                    connected = false;
                }
            }
        }

        private void getUsername(Socket thisClient) { 
            bool connected = true;
            string username = " ";
            
            while (connected && !terminating)
            {
                try
                {
                    Byte[] buffer2 = new Byte[1024];
                    thisClient.Receive(buffer2);

                    string incomingMessage = Encoding.Default.GetString(buffer2);
                    bool userIn = false;
                    if (incomingMessage != "") 
                    {

                        username = incomingMessage;
                        username = username.Substring(0, username.IndexOf("\0"));
                        foreach(string u in clientSockets.Values)
                        {
                            if (u == username) {
                                userIn = true;
                            }
                        }
                        if (!userIn)
                        {
                            clientSockets.Add(thisClient, username);
                            logs.AppendText(username);
                            logs.AppendText(" is connected.\n");
                        }
                        else
                        {
                            logs.AppendText("There is already someone connected with username: ");
                            logs.AppendText(username);
                            logs.AppendText("\n");

                            string message = "There is already someone connected with username: " + username + "\n";
                            sendMessage(message, thisClient);

                            thisClient.Close();
                        }
                        connected = false;
                    }
                }
                catch
                {}
            }
        }

        private void sendMessage(string str, Socket thisClient)
        {
            int size = str.Length;
            Byte[] buffer = new Byte[size];
            buffer = Encoding.Default.GetBytes(str);
            thisClient.Send(buffer);
        }

        private void button_disconnect_Click(object sender, EventArgs e)
        {
            terminating = true;
            button_connect.Enabled = false;
            button_browse.Enabled = true;
            button_disconnect.Enabled = false;
            textBox_port.Enabled = false;

            serverSocket.Close();
        }

        private void button_browse_Click(object sender, EventArgs e)
        {
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            terminating = false;
            DialogResult db = folderBrowser.ShowDialog();
            button_connect.Enabled = true;
            textBox_port.Enabled = true;

            if (db == DialogResult.OK) {
                db_path = folderBrowser.SelectedPath;
                logs.AppendText("Database is at " + db_path + "\n");
            }
        }

      
    }
}
