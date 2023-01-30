using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Alarm
{
    public partial class Principal : Form
    {

        Byte F=0;
        Byte P = 1;
        Byte DM = 2;
        Byte DI = 3;

        Byte nb = 0;


        Button[] fene = new Button[5];
        Button[] port = new Button[9];
        Button[] dm = new Button[2];
        Button[] di = new Button[3];

        //Status time

        DateTime[] feneT = new DateTime[5];
        DateTime[] portT = new DateTime[9];
        DateTime[] dmT = new DateTime[2];
        DateTime[] diT = new DateTime[3];


        public Principal()
        {
            InitializeComponent();
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            fene[0] = F1;
            fene[1] = F2;
            fene[2] = F3;
            fene[3] = F4;
            fene[4] = F5;

            port[0] = P1;
            port[1] = P2;
            port[2] = P3;
            port[3] = P4;
            port[4] = P5;
            port[5] = P6;
            port[6] = P7;
            port[7] = P8;
            port[8] = P9;

            dm[0] = DM1;
            dm[1] = DM2;

            di[0] = DI1;
            di[1] = DI2;
            di[2] = DI3;


            CheckForIllegalCrossThreadCalls = false;
            StreamReader sr = new StreamReader("C:/Users/51915/Desktop/Teccart/5eme session/objet conecte/Alarm/Alarm/Fichier/port.txt");
            string line;
            if ((line = sr.ReadLine()) != null)
            {
                try
                {
                    serialPort1.PortName = line;
                    serialPort1.BaudRate = 9800;
                    serialPort1.Open();
                    serialPort1.ReceivedBytesThreshold = 3;
                    statusPort.Text = "Port " + line;
                   // timer1.Enabled = true;
                }
                catch (Exception ex)
                {
                    // MessageBox.Show(ex.ToString());
                    statusPort.Text = "Port non ouvert";
                }
            }
            sr.Close();

        }

        private void portSerialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Port port = new Port();
            port.Show();
        }

        private void F1_Click(object sender, EventArgs e)
        {            
            Envoyer(F1, F, 0);
        }

        private void F2_Click(object sender, EventArgs e)
        {
           Envoyer(F2, F, 1);
        }

        private void F3_Click(object sender, EventArgs e)
        {           
            Envoyer(F3, F, 2);
        }

        private void F4_Click(object sender, EventArgs e)
        {           
            Envoyer(F4, F, 3);
        }

        private void F5_Click(object sender, EventArgs e)
        {
            Envoyer(F5, F, 4);
        }
              

        private void P1_Click(object sender, EventArgs e)
        {
            Envoyer(P1, P, 0);
        }

        private void P2_Click(object sender, EventArgs e)
        {
            Envoyer(P2, P, 1);
        }

        private void P3_Click(object sender, EventArgs e)
        {
            Envoyer(P3, P, 2);
        }

        private void P4_Click(object sender, EventArgs e)
        {
            Envoyer(P4, P, 3);
        }

        private void P5_Click(object sender, EventArgs e)
        {
            Envoyer(P5, P, 4);
        }

        private void P6_Click(object sender, EventArgs e)
        {
            Envoyer(P6, P, 5);
        }

        private void P7_Click(object sender, EventArgs e)
        {
            Envoyer(P7, P, 6);
        }

        private void P8_Click(object sender, EventArgs e)
        {
            Envoyer(P8, P, 7);
        }

        private void P9_Click(object sender, EventArgs e)
        {
            Envoyer(P9, P, 8);
        }
        private void DM1_Click(object sender, EventArgs e)
        {
            Envoyer(DM1, DM, 0);
        }

        private void DM2_Click(object sender, EventArgs e)
        {
            Envoyer(DM2, DM, 1);
        }

        private void DI1_Click(object sender, EventArgs e)
        {
            Envoyer(DI1, DI, 0);
        }

        private void DI2_Click(object sender, EventArgs e)
        {
            Envoyer(DI2, DI, 1);
        }

        private void DI3_Click(object sender, EventArgs e)
        {
            Envoyer(DI3, DI, 2);
        }

        private void statusPort_Click(object sender, EventArgs e)
        {
            StreamReader sr = new StreamReader("C:/Users/51915/Desktop/Teccart/5eme session/objet conecte/Alarm/Alarm/Fichier/port.txt");
            string line;
            if ((line = sr.ReadLine()) != null)
            {
                try
                {
                    if (!serialPort1.IsOpen)
                    {
                        serialPort1.PortName = line;
                        serialPort1.BaudRate = 9800;                   
                        serialPort1.Open();
                    }                    
                    statusPort.Text = "Port " + line;
                }
                catch (Exception ex)                {
                    MessageBox.Show(ex.ToString());
                    statusPort.Text = "Port non ouvert";
                }
            }
            sr.Close();
        }

        private void Envoyer(Button bt,Byte type,Byte nb)
        {
            byte[] send = new byte[3];
            byte[] datSend = new byte[8];
            DateTime dat = DateTime.Now;
            datSend = BitConverter.GetBytes(dat.Ticks);
            send[0] = type;
            send[1] = nb;

            String typeControl = bt.Text.Substring(0,1);

            if (bt.BackColor == Color.Red)
            {
                send[2] = 0;
                bt.BackColor = Color.Green;
                serialPort1.Write(send, 0, 3);
                serialPort1.Write(datSend, 0, 8);            
            }
            else
            {
                send[2] = 1;
                bt.BackColor = Color.Red;
                serialPort1.Write(send, 0, 3);
                serialPort1.Write(datSend, 0, 8);

            }


            if (typeControl == "F")
            {
                feneT[nb] = dat;
            }
            else if (typeControl=="P")
            {
                portT[nb] = dat;
            }


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            byte[] send = new byte[11];
            send[0] = 10;
            send[1] = 0;
            send[2] = 0;
            send[3] = 0;
            send[4] = 0;
            send[5] = 0;
            send[6] = 0;
            send[7] = 0;
            send[8] = 0;
            send[9] = 0;
            send[10] = 0;


            if (serialPort1.IsOpen)
            {
                serialPort1.Write(send, 0, 11);
            }
        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            byte[] msg1 = new byte[3];
            serialPort1.Read(msg1, 0, 3);

 
            byte[] datSend = new byte[8];           
           

            byte type = msg1[0];
            byte nombre = msg1[1];
            byte status = msg1[2];
           
            if (type==11) //demande actualization
            {
               byte[] send = new byte[3];
               for(byte i=0;i<fene.Length;i++)
                {
                    send[0] = F;
                    send[1] = i;
                    datSend = BitConverter.GetBytes(feneT[i].Ticks);
                    if (fene[i].BackColor==Color.Red)
                    {
                        send[2] = 1;
                        serialPort1.Write(send,0,3);
                        serialPort1.Write(datSend, 0, 8);

                    }
                   
                }

                for (byte i = 0; i < port.Length; i++)
                {
                    send[0] = P;
                    send[1] = i;
                    datSend = BitConverter.GetBytes(portT[i].Ticks);
                    if (port[i].BackColor == Color.Red)
                    {
                        send[2] = 1;
                        serialPort1.Write(send, 0, 3);
                        serialPort1.Write(datSend, 0, 8);

                    }

                }

                for (byte i = 0; i < dm.Length; i++)
                {
                    send[0] = DM;
                    send[1] = i;
                    datSend = BitConverter.GetBytes(dmT[i].Ticks);
                    if (dm[i].BackColor == Color.Red)
                    {
                        send[2] = 1;
                        serialPort1.Write(send, 0, 3);
                        serialPort1.Write(datSend, 0, 8);
                    }

                }

                for (byte i = 0; i < di.Length; i++)
                {
                    send[0] = DI;
                    send[1] = i;
                    datSend = BitConverter.GetBytes(diT[i].Ticks);
                    if (di[i].BackColor == Color.Red)
                    {
                        send[2] = 1;
                        serialPort1.Write(send, 0, 3);
                        serialPort1.Write(datSend, 0, 8);
                    }

                }
            }
        }
    }
}
