using System.Net.Sockets;
using System.Net;
using WinFormServer.CustomInvoke;
using System.Globalization;
using System.Windows.Forms;
using System.Reflection.Metadata.Ecma335;
using Npgsql;
using System.Data;
using System.Security.Cryptography;
using Npgsql.Replication.PgOutput.Messages;

namespace Sentral
{
    public partial class SentralGUI : Form
    {

        // General variable setup
        // In the database connection under, please change the connection settings to match your database.
        private const string cs = "Host=localhost;Username=postgres;Password=Kiwivin123!;Database=postgres"; // Setting up database connection parameters
        private const string CRLF = "\r\n";     // New line for writing to a textbox
        private bool onGoing;                   // Variable for closing threads when stopping Sentral server
        private int portNumber = 9050;          // Standard portnumber
        private TcpListener tcpListener;        // TCP socket listener for finding clients on network
        private List<TcpClient> cardreaderList; // List of all clients (easyer to close threads when they are collected)
        

        public SentralGUI()
        {
            InitializeComponent();
            //Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("no-NO"); // Setting a standard language, in case of conflict
            cardreaderList = new List<TcpClient>();
            button_Start.Enabled = true;
            button_Stop.Enabled = false;
            textBox_Status.Text = string.Empty;
        }

        // Sentral is split up in regions (expand them to read the code)

        // This region contains all buttonpress actions
        #region Event Handlers
        //--------------------------------------Event Handelers--------------------------------------------
        private void button_Start_Click(object sender, EventArgs e)
        {
            try
            {
                cardreaderList.Clear();

                if (!Int32.TryParse(textBox_PortNumber.Text, out portNumber)) // checing if textbox string is a number
                {
                    portNumber = 9050;
                    textBox_Status.Text = "Feil portnummer format. La inn standard port: " + portNumber;
                }

                Thread conThread = new Thread(ListenForIncomingConnection); // Make a thread that listens for incoming messages from multiple "Kortleser" programs
                conThread.Name = "Server LytteTråd"; // Defining name for thread
                conThread.IsBackground = true; // Defining that this thread is a backgorund thread
                conThread.Start();
                button_Start.Enabled = false;
                button_Stop.Enabled = true;
                //_sendCommandButton.Enabled = true;
            }
            catch (Exception ex)
            {
                textBox_Status.Text += CRLF + "Problem med oppstart av server";
                textBox_Status.Text += CRLF + ex.ToString();
            }
        }
        private void button_Stop_Click(object sender, EventArgs e)
        {
            onGoing = false;
            textBox_Status.Text = string.Empty;
            textBox_Status.Text = "Skrur av server, og kopla frå kortleserane";

            try
            {
                foreach (TcpClient cardreader in cardreaderList) //Closing all tcp-client threads
                {
                    cardreader.Close();
                }
                cardreaderList.Clear();
                tcpListener.Stop();
            }
            catch (Exception ex)
            {
                textBox_Status.InvokeCu(stb => stb.Text += CRLF + "Problem med stopping av server, eller kortleser");
            }

            button_Start.Enabled = true;
            button_Stop.Enabled = false;
            //textBox_Status.Text = string.Empty;
        }

        private void button_ChangeDatabase_Click(object sender, EventArgs e)
        {
            // Opening a second form for easy editing and overview of database
            Databaseendringar frm2 = new Databaseendringar();
            frm2.Show();
        }
        //--------------------------------------End of event Handlers----------------------------------------------------------
        #endregion Event Handlers

        // This region contains everything that handles TCP connections and starting of threads for each cardreader
        #region Main and TCP-Network Handling
        //--------------------------------------Main TCP Network handling---------------------------------------
        private void ListenForIncomingConnection() // We start a thread that listens for incoming connections and creates threads for each connection
        {
            try
            {
                onGoing = true;
                tcpListener = new TcpListener(IPAddress.Any, portNumber); // Set what IP and port we should listen for connections to
                tcpListener.Start();
                textBox_Status.InvokeCu(stb => stb.Text += CRLF + "Sentral er starta. Lytter på port: " + portNumber); 

                while (onGoing)
                {
                    textBox_Status.InvokeCu(stb => stb.Text += CRLF + "Venter på ny tilkopling...");
                    TcpClient client = tcpListener.AcceptTcpClient();   // Waits here until client connects
                    
                    textBox_Status.InvokeCu(stb => stb.Text += CRLF + "Tilkopling godtatt frå " + client.GetHashCode());
                    Thread t = new Thread(ProcessClientRequests); // Creating a new thread for each connection
                    t.IsBackground = true;
                    t.Start(client);
                }
            }
            catch (SocketException se) // If we have a problem with the socket
            {

                textBox_Status.InvokeCu(stb => stb.Text += CRLF + "Problem starting Sentral socket.");
                textBox_Status.InvokeCu(stb => stb.Text += CRLF + se.ToString());
            }
            catch (Exception ex) // If we have a problem with GUI 
            {
                textBox_Status.InvokeCu(stb => stb.Text += CRLF + "Problem med oppstart av Sentral");
                textBox_Status.InvokeCu(stb => stb.Text += CRLF + ex.ToString());
            }

            //textBox_Status.InvokeCu(stb => stb.Text += CRLF + "Lukker lyttetråd");
            textBox_Status.InvokeCu(stb => stb.Text = String.Empty);
        }

        private void ProcessClientRequests(object o) // When a client is connected, we open a thread that runs this function
        {
            TcpClient client = (TcpClient)o;     // TCP Connection thread
            cardreaderList.Add(client);          // Adding this thread to list, for safe shutdown later

            string alarmtype = "";               // Alarmtype in string format
            int alarmnumb = 0;                   // Variable for alarm in numberform
            string aMessage = "";                // Variable for a complete message
            string data = string.Empty;          // Variable for storing incomplete messages
            bool wholeMessageRecieved = false;   // Variable for confirming if a whole message is recieved
            bool alarmrecieved = false;          // Variable for confirming if a alarm recieved
            bool cardscan = false;               // Variable for waiting for a card scan to be done, before new card can be scanned

            try
            {
                NpgsqlConnection databaseConnection = new NpgsqlConnection(cs); 
                StreamReader reader = new StreamReader(client.GetStream());
                StreamWriter writer = new StreamWriter(client.GetStream());

                while (client.Connected) // Main program that runs when a client is connected
                {
                    wholeMessageRecieved = false;
                    if (!wholeMessageRecieved)
                    {
                        data = data + reader.ReadLine(); // Blocks here until something is received from client

                        if (DataContainsAMessage(data)) // If a whole message is recieved, store the message
                        {
                            data = GetAMessage(data, ref aMessage);
                            wholeMessageRecieved = true;
                        }
                    }
                    if (wholeMessageRecieved) // Do these checks if we recieve a message
                    {
                        // Decoding important info from message
                        ShowAlarm(aMessage, ref alarmtype, ref alarmnumb);
                        DateTime timeOfMessage = DateAndClock(aMessage);
                        string cardReaderNumb = ShowCardReaderNumber(aMessage);
                        string cardNumber = ShowCardNumber(aMessage);
                        string doorNumb = ShowNodenumber(aMessage);

                        //$A001B20220804C234423D0E1F0000G0000H1001#


                        //---------Alarm check-----------------
                        //(D == 00 -> ingen alarm), (D == 01 -> Dør open for lenge), (D == 02 -> innbrudd)
                        // Dersom alarm er oppdaga
                        // --> lagre typen alarm, nodemummer, tidspunkt og sist brukt kortID til database
                        // --> Skriv ut typen alarm, nodemummer, tidspunkt og sist brukt kortID til GUI
                        if ((alarmnumb == 1 || alarmnumb == 2) && !alarmrecieved) //Only save one alarm before reset.
                        {
                            alarmrecieved = true;
                            string dateString = DateAndClock(aMessage).ToString("d"); // Only get date
                            string clockString = DateAndClock(aMessage).ToString("T"); // Only get time

                            // Add alarm to Alarm textbox
                            textBox_Alarms.InvokeCu(alm => alm.Text += CRLF + "Dørnummer: " + doorNumb + " Dato: " 
                            + dateString + " Kl: " + clockString + " Alarmtype: " + alarmtype + 
                            " Kortnummer: " + cardNumber);
                            
                            // Write alarm to database
                            try
                            {
                                DatabaseAlarmAdd(databaseConnection, alarmtype, timeOfMessage, cardNumber, cardReaderNumb);
                            }
                            catch (Exception ex)
                            {
                                textBox_Status.InvokeCu(stb => stb.Text += CRLF + "Database Alarm Error: " + ex.Message);
                            }
                        }
                        else if (alarmnumb == 0) // Waiting for alarm to reset
                        {
                            alarmrecieved = false;
                        }


                        //---------Cardscanning----------------
                        // Om vi mottar at kort er scanna (E == 1)
                        // Få tak i kortNr, PIN, dato, nodenummer
                        // Sjekk dette opp imot database(sjekke gyldighetsperiode, romnummer, PIN, kortid)
                        // returner 01 gyldig info(true) eller 02 avvist(false)
                        // dersom true eller false lagre dette inntastingsforsøket til database
                        if (Cardscanned(aMessage) && !cardscan && alarmnumb == 0)
                        {
                            try
                            {
                                cardscan = true;
                                string cardpin = ShowCardPIN(aMessage);
                                if (DatabaseCheck(databaseConnection, timeOfMessage, doorNumb, cardpin, cardNumber,cardReaderNumb))
                                    writer.WriteLine("$A01#"); // If card-ID and PIN match
                                else
                                    writer.WriteLine("$A00#");// If card-ID and PIN does not match
                                writer.Flush();
                            }
                            catch (Exception ex)
                            {
                                textBox_Status.InvokeCu(stb => stb.Text += CRLF + "Database user-check error: " + ex.Message);
                                writer.WriteLine("$A00#");
                            }
                        }
                        else if (!Cardscanned(aMessage)) // Waiting for a cardscan to be done
                        {
                            cardscan = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                textBox_Status.InvokeCu(stb => stb.Text += CRLF + "Problem processing Cardreader requests. ");
            }

            cardreaderList.Remove(client); // If client is disconnected, then remove the connection
            textBox_Status.InvokeCu(stb => stb.Text += CRLF + "Finished processing client requests for client: " + client.GetHashCode());
        } // end ProcessClientRequests() method

        //---------------------------------------End of TCP Network Handling---------------------------------------
        #endregion Main and TCP-Network Handling

        // This region is for decoding messages from the cardreaderprogram
        #region Message Handling
        //--------------------------------------------Message handling---------------------------------------------
        static bool DataContainsAMessage(string data) // Checking if packet contains a whole message
        {
            bool svar = false;

            int posStart = data.IndexOf('$');
            int posSlutt = data.IndexOf('#');
            if (posStart != -1 && posSlutt != -1)
            {
                if (posStart < posSlutt)
                {
                    svar = true;
                }
            }
            return svar;
        }
        static string GetAMessage(string data, ref string aMessage) // Gets message from packet
        {
            int posStart = data.IndexOf('$');
            int posSlutt = data.IndexOf('#');

            aMessage = data.Substring(posStart, (posSlutt - posStart) + 1);

            // Removes chars behind $
            if (posStart > 0) data = data.Substring(posStart);
            // saves potential chars before #
            if (aMessage.Length < data.Length) data = data.Substring(posSlutt + 1);
            else data = "";
            return data;
        }

        private string ShowNodenumber(string message) // Gets doornumber/nodenumber
        {
            
            int posA = message.IndexOf('A');
            if (posA >= 0)
            {
                string node = message.Substring(posA + 1, 3);
                //textBox_Status.InvokeCu(stb => stb.Text += CRLF + "Mottatt melding frå kortlesernode: " + node);

                return node;
            }
            return "";
        }
        //$A001B20220804C234423D1E0F3333G0011#
        private DateTime DateAndClock(string message) // Get date and clock in correct format 
        {
            DateTime date = DateTime.Now; // Dette kan bli eit problem om info mangla frå kortleser --- sjekk opp i det.
            int posB = message.IndexOf('B');
            int posC = message.IndexOf('C');
            if (posB >= 0 && posC >= 0)
            {
                // Formating date and clock
                string dateo = Convert.ToString(message.Substring(posB + 1, 8)) + Convert.ToString(message.Substring(posC + 1, 6));
                date = DateTime.ParseExact(dateo, "yyyyMMddHHmmss", new CultureInfo("no-NO")); // Getting the right format, just in case.
            }
            return date;
        }

        private void ShowAlarm(string message, ref string alarmtype, ref int alarmnumb) // Defining alarmtypes ("dør open for lenge" and "innbrudd") 
        {
            int posD = message.IndexOf('D');
            if (posD >= 0)
            {
                int alarm = Convert.ToInt32(message.Substring(posD + 1, 1));
                if (alarm == 1)
                {
                    alarmtype = "Open for lenge";
                }
                else if (alarm == 2)
                {
                    alarmtype = "Innbrudd";
                }
                alarmnumb = alarm;
            }
        }

        private bool Cardscanned(string message) // Card scanned or not?
        {
            int scanned = 0;
            int posE = message.IndexOf('E');
            if (posE >= 0)
            {
                scanned = Convert.ToInt32(message.Substring(posE + 1, 1));
            }
            if (scanned == 1)
                return true;
            else
                return false;
        }

        private string ShowCardNumber(string message) // Gets latest scanned cardnumber
        {
                string cardNumb = "";
                int posF = message.IndexOf('F');
                if (posF >= 0)
                {
                    cardNumb = Convert.ToString(message.Substring(posF + 1, 4));
                }
                return cardNumb;
        }
        private string ShowCardPIN(string message) // Gets the PIN that the user typed
        {
            string cpin = "";
            int posG = message.IndexOf('G');
            if (posG >= 0)
            {
                cpin = Convert.ToString(message.Substring(posG + 1, 4));
            }
            return cpin;
        }
        private string ShowCardReaderNumber(string message) // Gets the Card Reader number ID
        {
            string cardReadNumb = "";
            int posH = message.IndexOf('H');
            if (posH >= 0)
            {
                cardReadNumb = Convert.ToString(message.Substring(posH + 1, 4));
            }
            return cardReadNumb;
        }

        //------------------------------------------------- End of Message Handling --------------------------------------
        #endregion Message Handling

        // This region handles handles every query sent and recieved from the database
        #region Database Checks
        //---------------------------------------------------- Database checks -------------------------------------------
        private void DatabaseAlarmAdd(NpgsqlConnection dc, string at, DateTime date, string cn, string crn) // This method adds alarms to the database
        {
            dc.Open();
            var sql = "INSERT INTO alarm(alarmtype, dato, kortid, nummerid) VALUES(@alarmtype, @dato, @kortid, @nummerid);";
            using var cmd = new NpgsqlCommand(sql, dc);
            cmd.Parameters.AddWithValue("alarmtype", at);
            cmd.Parameters.AddWithValue("dato", date);
            cmd.Parameters.AddWithValue("kortid", cn);
            cmd.Parameters.AddWithValue("nummerid",crn);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            dc.Close();
        }
        private bool DatabaseCheck(NpgsqlConnection dc, DateTime date, string room, string pin, string cardid, string cardreaderid) // method for checking CardID and PIN in database, also saving the result
        {
            //Sjekk dette opp imot database(sjekke gyldighetsperiode, romnummer, PIN og kortid
            //$A001B20220804C234423D0E1F0000G0000H1001#
            bool state= false;
            string stateString = "";
            dc.Open();
            // This query is set up so that we either get 1 potential match or 0. This is the method we use to check if Card ID and PIN matches the date and door.
            NpgsqlDataAdapter adp = new NpgsqlDataAdapter("SELECT * FROM adgangview WHERE '" + date + "' > gyldighetfra AND '"
                 + date + "' < gyldighettil" + " AND " + "pin = '" + pin + "' AND kortid = '" + cardid + "' AND romnummer = '"+ room +"' AND nummerid = '" + cardreaderid + "'",dc);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                state = true;
                stateString = "Godkjent";
            }
            else
            {
                state = false;
                stateString = "Avvist";
            }
            var sql = "INSERT INTO forespørsler(status,dato,kortid,nummerid) VALUES(@status, @dato, @kortid, @nummerid);";
            using var cmd = new NpgsqlCommand(sql, dc);
            cmd.Parameters.AddWithValue("status", stateString);
            cmd.Parameters.AddWithValue("dato", date);
            cmd.Parameters.AddWithValue("kortid", cardid);
            cmd.Parameters.AddWithValue("nummerid", cardreaderid);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            dc.Close();
            return state;
        }
        //---------------------------------------------------- End of Database checks -------------------------------------------
        #endregion Database Checks
    }
}
