using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
namespace Alarm
{
    public partial class Port : Form
    {
        String[] listPort;
        StreamReader sr;
        public Port()
        {
            InitializeComponent();
        }

        private void Port_Load(object sender, EventArgs e)
        {
            listPort = SerialPort.GetPortNames();
            Array.Sort(listPort);
            cboList.Items.AddRange(listPort);
            sr = new StreamReader("C:/Users/51915/Desktop/Teccart/5eme session/objet conecte/Alarm/Alarm/Fichier/port.txt");
            string line;
            if ((line = sr.ReadLine()) != null)
            {
                lblPort.Text = "Port stocké " + line;

            }
            sr.Close();
        }

        private void btnChoix_Click(object sender, EventArgs e)
        {
            if(cboList.SelectedIndex!=-1)
            {
                serialPort1.PortName = cboList.SelectedItem.ToString();
                if (serialPort1.IsOpen)
                {
                    MessageBox.Show("Le port est ocupe!");
                }
                else
                {
                    MessageBox.Show("Le port est livre!");
                    // serialPort1.Open();
                    StreamWriter sw = new StreamWriter("C:/Users/51915/Desktop/Teccart/5eme session/objet conecte/Alarm/Alarm/Fichier/port.txt", false);
                    sw.WriteLine(cboList.SelectedItem.ToString());
                    sw.Close();
                }
            }
            else
            {
                MessageBox.Show("Vous devez choissir un port");
            }
            
        }
    }
}
