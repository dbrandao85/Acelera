using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
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

        Dados obj = new Dados {
            Numero_casas = 0,
            Token = "",
            Cifrado = "",
            Decifrado = "",
            Resumo_criptografico = ""
        };

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

            obj = JsonSerializer.Deserialize<Dados>(responseString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            textBox1.Text = "Número de casas - " + obj.Numero_casas + Environment.NewLine
                + "Token - " + obj.Token + Environment.NewLine
                + "Cifrado - " + obj.Cifrado + Environment.NewLine
                + "Decifrado - " + obj.Decifrado + Environment.NewLine
                + "Criptografia - " + obj.Resumo_criptografico;

            DecriptarCifraDeCesar();
            CalcularSha1();
        }

        private void EnviarJson()
        {
            textBox2.Text = "Número de casas - " + obj.Numero_casas + Environment.NewLine
                + "Token - " + obj.Token + Environment.NewLine
                + "Cifrado - " + obj.Cifrado + Environment.NewLine
                + "Decifrado - " + obj.Decifrado + Environment.NewLine
                + "Criptografia - " + obj.Resumo_criptografico;

            if (!File.Exists(Directory.GetCurrentDirectory() + @"\answer.json"))
            {
                File.Create(Directory.GetCurrentDirectory() + @"\answer.json");
            } 

            File.WriteAllText(Directory.GetCurrentDirectory() + @"\answer.json",
                JsonSerializer.Serialize(obj)
                );

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"https://api.codenation.dev/v1/challenge/dev-ps/submit-solution?token=1adcc80b66da14db6aaee5ee7f388e95e5d7827a");

        }

        private void DecriptarCifraDeCesar()
        {
            string textoDescriptografado = "";
            for (int i = 0; i < obj.Cifrado.Length; i++)
            {
                char letra = Convert.ToChar(obj.Cifrado.Substring(i, 1));
                if(letra >= 97 && letra <= 122)                    
                {
                    int valor = letra - 5 - 97;
                    if(valor < 0)
                    {
                        textoDescriptografado += Convert.ToChar(26 + valor + 97);
                    }
                    else
                    {
                        textoDescriptografado += Convert.ToChar(((letra - 97 - 5) % 26) + 97);
                    }                    
                }
                else
                {
                    textoDescriptografado += letra;
                }                
            }
            obj.Decifrado = textoDescriptografado;
        }

        private void CalcularSha1()
        {
            var hash = new SHA1Managed().ComputeHash(Encoding.UTF8.GetBytes(obj.Decifrado));
            obj.Resumo_criptografico = string.Concat(hash.Select(b => b.ToString("x2")));
        }
    }
}
