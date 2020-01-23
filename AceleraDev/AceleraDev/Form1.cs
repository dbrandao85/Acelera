using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AceleraDev
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Dados obj = new Dados();
        
        private void button1_Click(object sender, EventArgs e)
        {
            ReceberJson();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EnviarJson();
        }

        private void ReceberJson()
        {
            string responseString;

            var webClient = new WebClient();
            
            responseString = webClient.DownloadString("https://api.codenation.dev/v1/challenge/dev-ps/generate-data?token=1adcc80b66da14db6aaee5ee7f388e95e5d7827a");

            var obj2= JsonSerializer.Deserialize<Dados>(responseString);

            Console.WriteLine(responseString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            }); 
            textBox1.Text = obj2.Cifrado;
        }

        private void EnviarJson()
        {

        }
    }
}
