using Kortleser.Properties;
using System.IO.Ports;
using Kortleser.Utils;
using System.Net.Sockets;
using System.Net;
using System.Globalization;

namespace Kortleser
{
    public partial class Kortleser_GUI : Form
    {   //CardReaderProgram//
        #region VARIABLES
        //************* General Variables***********************//
        bool alarm,accessGranted;
        bool communicate;
        SerialPort sp;
        string node, appID, date, time, dInput,dOutput, termistor, pot1, pot2, temp1, temp2; //SIM-SIM OUTPUTS
        string message, data;
        string toSentral;
        string PIN = "xxxx";
        string cardID = "yyyy";
        string aType = "0";
        string cardScanned = "0";
        int timeLeft = 45;
        int timeLeftDoor = 45;

        //*************TCP-IP related variables***********************//

        private const string CRLF = "\r\n";
        private const string LOCALHOST = "127.0.0.1";
        private const int DEFAULT_PORT = 9050;

        private IPAddress _serverIpAddress;
        private int _port;
        private TcpClient _client;

        #endregion

        //**************Form initializing***************//
        private void Kortleser_GUI_Load(object sender, EventArgs e)
        {
            //Initializes the GUI pictures
            pb_Alarm.Image = Resources.LED_Tom_V2;
            pb_Open.Image = Resources.LED_Tom_V2;
            pb_Closed.Image = Resources.LED_Tom_V2;
            pb_DoorState.Image = Resources.Door_Closed;

        }

        public Kortleser_GUI()
        {
            InitializeComponent();
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("no-NO");
            InitializeNode();
            UpdateSerialPorts();
            communicate = false;

            _serverIpAddress = GetIpAddress(_ipAddressTextBox.Text);
            _port = GetPort(_portTextBox.Text);
            _connectButton.Enabled = true;
            _disconnectButton.Enabled = false;
        }

        //***************Main Function**********************//
        //As Long as the background worker is running the main function will continue to loop
        private void MainFnc(string message)
        {
            lbSimSim.Items.Insert(0, message.ToString()); 
            DecodeMessage(message);
            SendToSentral();
            //FromSentral();
            if (dOutput.Substring(4, 1) == "1" && !alarm)
            {
                //sp.Write("$O40");
                AccessAttempt(); //open door
                timer1.Start(); //Starts timer for access attempt
            }
            else
            {
                tbPin.Enabled = false; //disables pin and id textboxes
                tbID.Enabled = false;
            }

            if (Convert.ToInt32(pot1) > 500) //Checks for broken in door
            {
                TriggerAlarm("2"); //triggers door broken in alarm
                pb_DoorState.Image = Resources.Door_Broken;
                pb_DoorState.Refresh();
            }

            if (dOutput.Substring(6, 1) == "1" && dOutput.Substring(5,1) == "0") // Update gui if door is open
            {
                pb_DoorState.Image = Resources.Door_Open;
                pb_DoorState.Refresh();
                timerDoorOpeningTimer.Start();
            }
            else if(dOutput.Substring(6,1) == "0" && !alarm) // Update gui if door is closed
            {
                pb_DoorState.Image = Resources.Door_Closed;
                pb_DoorState.Refresh();
            }

            if (cbResetAlarm.Checked && dOutput.Substring(6,1) == "0") // door has to be closed in order to reset the alarm.
            {
                aType = "0";
                alarm = false;
                SendMessage(sp, "$O70");
                pb_Alarm.Image = Resources.LED_Tom_V2;
                pb_Alarm.Refresh();
                pb_Open.Image = Resources.LED_Tom_V2;
                pb_Open.Refresh();
                SendMessage(sp, "$O51"); //lock the door
                pb_Closed.Image = Resources.LED_Rød_V2;
                pb_Closed.Refresh();
                cbResetAlarm.Checked = false;
            }                         
        }

        #region METHODS
        //******************Methods*************************//
        //***These methods are used by the main function***///
        //*************************************************//

        private void SendToSentral()
        {
            //sende info to sentral: NODE/DATE/Time/ALARM/CardScanned/CardID/PIN/App Id
            // "$AxxxBYYYYMMDDCHHMMSSDxExFxxxxGxxxxHxxxx#"
            toSentral = "$"+"A" + node + "B" + date + "C" + time + "D" + aType + "E" + cardScanned + "F" + cardID + "G" + PIN + "H" + appID + "#";
            tbSentralString.Text = toSentral;
            string txtSentral = "Dørnummer: "+node + " Alarmtype: " + aType + " Kortnummer: " + cardID + " PIN-Kode: " +PIN;
            //lbSentral.Items.Add(toSentral);
            if(cardScanned == "1" || aType != "0")
                lbSentral.Items.Insert(0, txtSentral);

            //Send sentral message to client
            try
            {
                if (_client.Connected)
                {
                    StreamWriter writer = new StreamWriter(_client.GetStream());
                    writer.WriteLine(toSentral);
                    writer.Flush();
                }

            }
            catch (Exception ex)
            {
                _statusTextBox.Text += CRLF + "problem med sending..";
                _statusTextBox.Text += CRLF + ex.ToString();
            }

        }

        private void DecodeMessage(string message)
        {
            //This function takes the output from sim-sim, splits it and stores them in seperate variables
            if (communicate)
            {
                node = message.Substring(message.IndexOf("A") + 1, 3);
                date = message.Substring(message.IndexOf("B") + 1, 8);
                time = message.Substring(message.IndexOf("C") + 1, 6);
                dInput = message.Substring(message.IndexOf("D") + 1, 8);
                dOutput = message.Substring(message.IndexOf("E") + 1, 8);
                termistor = message.Substring(message.IndexOf("F") + 1, 4);
                pot1 = message.Substring(message.IndexOf("G") + 1, 4);
                pot2 = message.Substring(message.IndexOf("H") + 1, 4);
                temp1 = message.Substring(message.IndexOf("I") + 1, 3);
                temp2 = message.Substring(message.IndexOf("J") + 1, 3);
            }
        }

        private void TriggerAlarm(string alarmtype)
        {
            //Sends message to SIM-SIM that there is an alarm, and sets alarmvariables true;
            alarm = true;
            aType = alarmtype;
            SendMessage(sp, "$O71");
            pb_Alarm.Image = Resources.LED_Oransje_V2;
            pb_Alarm.Refresh();


        }

        private void InitializeDoor()
        {
            DateTime datonow = DateTime.Now; // gets date time
            node = String.Format("$N00" + cbNode.SelectedIndex.ToString());
            appID = String.Format((1000 + (int)cbAppID.SelectedIndex).ToString());
            SendMessage(sp, "$S001"); //new message every second
            SendMessage(sp, node);
            SendMessage(sp, "$D" + datonow.ToString("yyyyMMdd", new CultureInfo("no-NO")));
            SendMessage(sp, "$T" + datonow.ToString("HHmmss", new CultureInfo("no-NO")));
            SendMessage(sp, "$O51"); // door is locked

            SendMessage(sp, "$O60"); // door is closed
            alarm = false;
            pb_Alarm.Image = Resources.LED_Tom_V2;
            pb_Alarm.Refresh();
            pb_Closed.Image = Resources.LED_Rød_V2;
            pb_Closed.Refresh();
            pb_Open.Image = Resources.LED_Tom_V2;
            pb_Open.Refresh();
            pb_DoorState.Image = Resources.Door_Closed;
        }

        private void InitializeNode()
        {
            //Adds selectable doornumbers and appID in comboboxes
            int doorNumbers = 5;
            for (int i = 0; i < doorNumbers; i++)   
                cbNode.Items.Add((i).ToString()); 
            if (cbNode.Items.Count > 0) cbNode.SelectedIndex = 0;

            for (int i = 0; i < doorNumbers; i++)
                cbAppID.Items.Add("100" + i);            
            if(cbAppID.Items.Count > 0) cbAppID.SelectedIndex = 0;

            
        }

        private void AccessAttempt()
        {

            tbID.Enabled = true;
            tbPin.Enabled = true;

            if (tbID.Text.Length == 4 && tbPin.Text.Length == 4) // wait until both ID and PIN is written
            {
                cardScanned = "1";
                cardID = tbID.Text;
                PIN = tbPin.Text;
            }
            else
            {
                cardScanned = "0";
            }
        }
        #endregion

        #region GUI
        //****************GUI-Components*******************//


        private void bwCom_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            bool msgRecieved = false ;
            message = "";
            //if there is a new message continue
            while (communicate && !msgRecieved)
            {
                data = data + RecieveData(sp);

                if (DataContainsFullMessage(data))
                {
                    data = RetrieveMessage(data, ref message);
                    msgRecieved = true;
                }

            }
        }

        private void bwCom_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {

            MainFnc(message); // calls main function
            if (communicate) bwCom.RunWorkerAsync(); // if start is still enabled 
        }

        private void timer1_Tick(object sender, EventArgs e) // Lock and unlock door function
        {
            if(accessGranted)
            {
                SendMessage(sp,"$O50"); //e5 = 0 = dør unlocked
                SendMessage(sp, "$O40");  //reset the access command
                pb_Open.Image = Resources.LED_Grønn_V2;
                pb_Open.Refresh();
                pb_Closed.Image = Resources.LED_Tom_V2;
                pb_Closed.Refresh();
                tbID.Text = String.Empty;
                tbPin.Text = String.Empty;
                tbID.Enabled = false;
                tbPin.Enabled = false;
                cardScanned = "0";
                accessGranted = false; //resets
                timer1.Stop();
                timeLeft = 45;
            }else if(timeLeft > 0)
            {
                timeLeft--;
            }
            else
            {
                SendMessage(sp,"$O40");  //reset the access command after 45 secs
                tbID.Text = String.Empty;
                tbPin.Text = String.Empty;
                tbID.Enabled = false;
                tbPin.Enabled = false;
                timer1.Stop();
                timeLeft = 45;
                //reset accessattempt
            }
        }

        private void timerDoorOpeningTimer_Tick(object sender, EventArgs e)
        {
            if(dOutput.Substring(6,1)== "0") //
            {
                SendMessage(sp,"$O51"); // dør = locked
                pb_Closed.Image = Resources.LED_Rød_V2;
                pb_Closed.Refresh();
                pb_Open.Image = Resources.LED_Tom_V2;
                pb_Open.Refresh();
                timerDoorOpeningTimer.Stop();
                timeLeftDoor = 45;
            }
            else if (timeLeftDoor > 0)
            {
                timeLeftDoor--;
            }
            else
            {
                TriggerAlarm("1"); // trigger door open too long alarm
                timerDoorOpeningTimer.Stop();
                timeLeftDoor = 45;
            }
        } 

        private void btnStart_Click(object sender, EventArgs e)
        {
            // checks if comboports are selected in order to start communicating with SIM SIM.
            if(cbPorts.SelectedIndex >= 0 && cbNode.SelectedIndex >= 0 && cbAppID.SelectedIndex >= 0)
            {
                string comPort = cbPorts.SelectedItem.ToString();
                btnStop.Enabled = true;
                btnStart.Enabled = false;
                communicate = true;

                sp = new SerialPort(comPort, 9600);

                try
                {
                    sp.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,"Error! test",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    btnStop.Enabled = false;
                    btnStart.Enabled = true;
                }

                if (sp.IsOpen)
                {
                    InitializeDoor();
                    bwCom.RunWorkerAsync();
                } 

            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            // Stops communication with SIM SIM
            btnStop.Enabled = false;
            communicate = false;
            sp.Close();
            btnStart.Enabled = true;
            timeLeft = 45;
            timeLeftDoor = 45;
            timer1.Stop();
            timerDoorOpeningTimer.Stop();

        }
        #endregion

        #region SIMSIM
        //*****************SIM-SIM Methods*****************//
        //Primarily used for connecting to SIM-SIM(read/write)




        private void SendMessage(SerialPort sp, string message)
        {
            // Sends message to SIM-SIM
            try
            {
                sp.Write(message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateSerialPorts()
        {
            string[] ports = SerialPort.GetPortNames();
            for (int i = 0; i < ports.Length; i++)
                cbPorts.Items.Add(ports[i]);

            if (cbPorts.Items.Count > 0) cbPorts.SelectedIndex = 0;
        }

        private string RetrieveMessage(string data, ref string message)
        {
            // gets messages from SIM-SIM
            int posStart = data.IndexOf('$');
            int posStop = data.IndexOf('#');
            message = data.Substring(posStart, (posStop - posStart) + 1);

            if (posStart > 0) data = data.Substring(posStart);
            if (message.Length < data.Length) data = data.Substring(posStop + 1);
            else data = "";
            return data;
        }

        private bool DataContainsFullMessage(string data)
        {
            bool ans = false;
            int posStart = data.IndexOf('$');
            int posStop = data.IndexOf('#');

            if (posStart != -1 && posStop != -1)
            {
                if (posStart < posStop)
                {
                    ans = true;
                }
            }
            return ans;
        }

        private string RecieveData(SerialPort sp)
        {
            string ans = "";
            try
            {
                ans = sp.ReadExisting();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            return ans;
           
        }
        #endregion

        #region TCPIP
        //*******************TCP-IP***********************//



        private void _connectButton_Click(object sender, EventArgs e)
        {
            try
            {
                _serverIpAddress = GetIpAddress(_ipAddressTextBox.Text);
                _port = GetPort(_portTextBox.Text);

                _client = new TcpClient(_serverIpAddress.ToString(), _port);
                Thread t = new Thread(ProcessClientTransactions);
                t.IsBackground = true;
                t.Start(_client);

                _connectButton.Enabled = false;
                _disconnectButton.Enabled = true;
                btnStart.Enabled = true;

            }
            catch (Exception ex)
            {
                _statusTextBox.Text += CRLF + "Problem med tilkobling";
                _statusTextBox.Text += CRLF + ex.ToString();
            }

        }

        private void _disconnectButton_Click(object sender, EventArgs e)
        {
            DisconnectFromServer();
        }

        private void ProcessClientTransactions(object tcpClient)
        {
            TcpClient client = (TcpClient)tcpClient;
            string input = string.Empty;
            StreamReader reader = null;
            StreamWriter writer = null;


            try
            {
                reader = new StreamReader(client.GetStream());
                writer = new StreamWriter(client.GetStream());

                // Tell the server we've connected
                writer.WriteLine("Tilkoblet!");
                writer.Flush();

                while (client.Connected)
                {
                    input = reader.ReadLine(); // block here until we receive something from the server.
                    if (input == null)
                    {
                        DisconnectFromServer();
                    }
                    else
                    {
                        switch (input)
                        {

                            default:
                                {
                                    
                                    if(input == "$A01#")
                                    {
                                        lbFromSentral.InvokeEx(x => x.Items.Add(input +"Kort: "+cardID + " Godkjent adgang!"));
                                        accessGranted = true;


                                    }
                                    else if (input == "$A00#")
                                    {
                                        lbFromSentral.InvokeEx(x => x.Items.Add(input + "Kort: " + cardID + " Ikke Godkjent adgang!"));
                                        timer1.Stop();
                                        timeLeft = 45;
                                        SendMessage(sp, "$O40");
                                        accessGranted = false;
                                        tbID.InvokeEx(x => x.Text = String.Empty);
                                        tbPin.InvokeEx(x => x.Text = String.Empty);
                                        cardScanned = "0";
                                       
                                    }

                                    break;
                                }
                        } // end switch
                    } // end if/else


                }
            }
            catch (Exception ex)
            {
                _statusTextBox.InvokeEx(stb => stb.Text += CRLF + "Problemer med tilkobling til server");
                // _statusTextBox.InvokeEx(stb => stb.Text += CRLF + ex.ToString());
            }

            _disconnectButton.InvokeEx(dcb => dcb.Enabled = false);
            _connectButton.InvokeEx(cb => cb.Enabled = true);
            _statusTextBox.InvokeEx(stb => stb.Text = string.Empty);


        }

        private void DisconnectFromServer()
        {

            try
            {
                _client.Close();
                _statusTextBox.InvokeEx(stb => stb.Text += CRLF + "Frakoblet!");
                _disconnectButton.InvokeEx(db => db.Enabled = false);
                _connectButton.InvokeEx(cb => cb.Enabled = true);
                _statusTextBox.InvokeEx(stb => stb.Text = string.Empty);
                btnStart.InvokeEx(cb => cb.Enabled = true);
                btnStop.InvokeEx(cb => cb.Enabled = false);
            }
            catch (Exception ex)
            {
                _statusTextBox.InvokeEx(stb => stb.Text += CRLF + "Feil i frakobling");
                _statusTextBox.InvokeEx(stb => stb.Text += CRLF + ex.ToString());
            }

            _statusTextBox.InvokeEx(stb => stb.Text = string.Empty);

        }

        private IPAddress GetIpAddress(string ipAddress)
        {
            IPAddress address = IPAddress.Parse(LOCALHOST);
            try
            {
                if (!IPAddress.TryParse(ipAddress, out address))
                {
                    address = IPAddress.Parse(LOCALHOST);
                }
            }
            catch (Exception ex)
            {
                _statusTextBox.Text += CRLF + "Invalid IP address - Client will connect to: " + _serverIpAddress.ToString();
                _statusTextBox.Text += CRLF + ex.ToString();
            }

            return address;

        }

        private int GetPort(string serverPort)
        {

            int port = DEFAULT_PORT;

            try
            {
                if (!Int32.TryParse(serverPort, out port))
                {
                    port = DEFAULT_PORT;
                }
            }
            catch (Exception ex)
            {
                _statusTextBox.Text += CRLF + "Invalid port value - Client will connect to port: " + port.ToString();
                _statusTextBox.Text += CRLF + ex.ToString();
            }

            return port;

        }

        #endregion





    }
}