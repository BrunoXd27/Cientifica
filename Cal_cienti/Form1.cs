using System;
using System.Data;
using System.Globalization;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using NCalc;
using static System.Formats.Asn1.AsnWriter;

namespace Cal_cienti
{
    public partial class Cal : Form
    {
        public Cal()
        {
            InitializeComponent();
            CheckForUpdates();
        }

        public double EvaluateExpression(string expression)
        {
            expression = Exponente(expression);
            expression = ReplaceSpecialSymbols(expression);

            var e = new NCalc.Expression(expression);
            return Convert.ToDouble(e.Evaluate());
        }

        private string ReplaceSpecialSymbols(string expression)
        {
            expression = Regex.Replace(expression, @"\s*√\s*(\d+)", "Sqrt($1)");
            expression = Regex.Replace(expression, @"√\s*(\d+)", "Sqrt($1)");
            return expression;
        }

        private string Exponente(string expression)
        {
            expression = ope.Text;

            if (expression.Contains('^'))
            {
                string[] partes = expression.Split('^');
                if (partes.Length == 2 && double.TryParse(partes[0], out double baseNumero) && double.TryParse(partes[1], out double exponente))
                {
                    double resultado = Math.Pow(baseNumero, exponente);
                    return resultado.ToString();
                }
            }

            if (expression.Contains('√'))
            {
                string[] partes = expression.Split('√');
                if (partes.Length == 2 && double.TryParse(partes[0], out double exponente) && double.TryParse(partes[1], out double baseNumero))
                {
                    double resultado = Math.Pow(baseNumero, 1.0 / exponente);
                    return resultado.ToString();
                }
            }
            if (expression.Contains('π'))
            {
                // Reemplaza π por el valor de Pi
                expression = expression.Replace("π", Math.PI.ToString(CultureInfo.InvariantCulture));
                return expression;
            }
            return expression;
        }

        private async void CheckForUpdates()
        {
            string currentVersion = "1.5.0"; // Versión actual de tu aplicación
            string repoOwner = "BrunoXd27";
            string repoName = "Cientifica";
            string apiUrl = $"https://api.github.com/repos/{repoOwner}/{repoName}/releases/latest";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.UserAgent.TryParseAdd("request");
                var response = await client.GetStringAsync(apiUrl);
                dynamic release = Newtonsoft.Json.JsonConvert.DeserializeObject(response);
                string latestVersion = release.tag_name;

                if (latestVersion != currentVersion)
                {
                    Console.WriteLine("Nueva versión disponible: " + latestVersion);
                    string downloadUrl = release.assets.browser_download_url;
                    string tempFilePath = Path.Combine(Path.GetTempPath(), "Form1.cs");

                    using (var downloadClient = new HttpClient())
                    {
                        var downloadResponse = await downloadClient.GetAsync(downloadUrl);
                        using (var fs = new FileStream(tempFilePath, FileMode.Create))
                        {
                            await downloadResponse.Content.CopyToAsync(fs);
                        }
                    }

                    // Cargar el nuevo ensamblado dinámicamente
                    Assembly newAssembly = Assembly.LoadFile(tempFilePath);
                    Type type = newAssembly.GetType("Cal_cienti.Cal"); // Reemplaza con el nombre del tipo que necesitas
                    MethodInfo method = type.GetMethod("InitializeComponent"); // Reemplaza con el nombre del método que necesitas
                    object instance = Activator.CreateInstance(type);
                    method.Invoke(instance, null);

                    RefreshUI();

                    Console.WriteLine("Actualización aplicada sin reiniciar.");
                }
                else
                {
                    Console.WriteLine("Ya tienes la última versión.");
                }
            }
        }

        private void RefreshUI()
        {
            this.Controls.Clear();
            InitializeComponent();
        }

        private void No_0_Click(object sender, EventArgs e)
        {
            if (ope.Text == "0") { ope.Text = ""; }
            ope.Text = ope.Text + "0";
        }

        private void No_1_Click(object sender, EventArgs e)
        {
            if (ope.Text == "0") { ope.Text = ""; }
            ope.Text = ope.Text + "1";
        }

        private void No_2_Click(object sender, EventArgs e)
        {
            if (ope.Text == "0") { ope.Text = ""; }
            ope.Text = ope.Text + "2";
        }

        private void No_3_Click(object sender, EventArgs e)
        {
            if (ope.Text == "0") { ope.Text = ""; }
            ope.Text = ope.Text + "3";
        }

        private void No_4_Click(object sender, EventArgs e)
        {
            if (ope.Text == "0") { ope.Text = ""; }
            ope.Text = ope.Text + "4";
        }

        private void No_5_Click(object sender, EventArgs e)
        {
            if (ope.Text == "0") { ope.Text = ""; }
            ope.Text = ope.Text + "5";
        }

        private void No_6_Click(object sender, EventArgs e)
        {
            if (ope.Text == "0") { ope.Text = ""; }
            ope.Text = ope.Text + "6";
        }

        private void No_7_Click(object sender, EventArgs e)
        {
            if (ope.Text == "0") { ope.Text = ""; }
            ope.Text = ope.Text + "7";
        }

        private void No_8_Click(object sender, EventArgs e)
        {
            if (ope.Text == "0") { ope.Text = ""; }
            ope.Text = ope.Text + "8";
        }

        private void No_9_Click(object sender, EventArgs e)
        {
            if (ope.Text == "0") { ope.Text = ""; }
            ope.Text = ope.Text + "9";
        }

        private void punto_Click(object sender, EventArgs e)
        {
            ope.Text = ope.Text + ".";
        }

        private void mas_Click(object sender, EventArgs e)
        {
            if (ope.Text != "0") { ope.Text = ope.Text + "+"; }
        }

        private void menos_Click(object sender, EventArgs e)
        {
            if (ope.Text != "0") { ope.Text = ope.Text + "-"; }
        }

        private void por_Click(object sender, EventArgs e)
        {
            if (ope.Text != "0") { ope.Text = ope.Text + "*"; }
        }

        private void div_Click(object sender, EventArgs e)
        {
            if (ope.Text != "0") { ope.Text = ope.Text + "/"; }
        }

        private void root_Click(object sender, EventArgs e)
        {
            if (ope.Text == "0") { ope.Text = ""; }
            ope.Text = ope.Text + "√";
        }

        private void porce_Click(object sender, EventArgs e)
        {
            if (ope.Text != "0")
            {
                reg.Text = ope.Text + "%";
                string expression = ope.Text;
                double result = EvaluateExpression(expression) / 100;
                ope.Text = result.ToString();
            }
        }

        private void igual_Click(object sender, EventArgs e)
        {
            reg.Text = ope.Text;
            reg.Text = reg.Text + "=";
            string expression = ope.Text;
            var result = EvaluateExpression(expression);
            ope.Text = result.ToString();
        }

        private void todo_Click(object sender, EventArgs e)
        {
            ope.Text = "0";
            reg.Text = "";
        }

        private void borrar_Click(object sender, EventArgs e)
        {
            ope.Text = "0";
        }

        private void elin_Click(object sender, EventArgs e)
        {
            if (ope.TextLength == 1) { ope.Text = "0"; }
            else { ope.Text = ope.Text.Substring(0, ope.Text.Length - 1); }
        }

        private void ope_TextChanged(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && "+-*/%^√π±".IndexOf(e.KeyChar) == -1)
            {
                e.Handled = true;
            }
        }

        private void masmenos_Click(object sender, EventArgs e)
        {
            char primerCaracter = ope.Text[0];
            string texto = ope.Text;
            if (primerCaracter == '-')
            {
                ope.Text = texto.Substring(1);
            }
            else
            {
                ope.Text = "-" + ope.Text;
            }
        }

        private void pi_Click(object sender, EventArgs e)
        {
            if (ope.Text == "0") { ope.Text = ""; }
            ope.Text = ope.Text + "π";
        }

        private void cuadrado_Click(object sender, EventArgs e)
        {
            ope.Text = ope.Text + "^2";
        }

        private void cubo_Click(object sender, EventArgs e)
        {
            ope.Text = ope.Text + "^3";
        }

        private void exponente_Click(object sender, EventArgs e)
        {
            ope.Text = ope.Text + "^";
        }

        private void expo10_Click(object sender, EventArgs e)
        {
            if (ope.Text == "0") { ope.Text = ""; }
            ope.Text = ope.Text + "10^";
        }

        private void exp_Click(object sender, EventArgs e)
        {
            if (double.TryParse(ope.Text, out double numero))
            {
                double exponente = Math.Exp(numero);
                reg.Text = ope.Text;
                ope.Text = exponente.ToString();
            }
        }

        private void log_Click(object sender, EventArgs e)
        {
            if (double.TryParse(ope.Text, out double numero))
            {
                double logaritmo = Math.Log10(numero);
                ope.Text = logaritmo.ToString();
            }
        }

        private void cubo_raiz_Click(object sender, EventArgs e)
        {
            if (double.TryParse(ope.Text, out double numero))
            {
                double raizCubica = Math.Pow(numero, 1.0 / 3.0);
                reg.Text = ope.Text;
                ope.Text = raizCubica.ToString();
            }
        }

        private void raiz_Click(object sender, EventArgs e)
        {
            //Si se quiere usar basta con poner "7√50"
            if (ope.Text == "0") { ope.Text = ""; }
            ope.Text = ope.Text + "√";
        }

        private void sin_Click(object sender, EventArgs e)
        {
            if (double.TryParse(ope.Text, out double angulo))
            {
                double seno = Math.Sin(angulo * Math.PI / 180);
                ope.Text = seno.ToString();
            }
        }

        private void cos_Click(object sender, EventArgs e)
        {
            if (double.TryParse(ope.Text, out double angulo))
            {
                double coseno = Math.Cos(angulo * Math.PI / 180);
                ope.Text = coseno.ToString();
            }
        }

        private void tan_Click(object sender, EventArgs e)
        {
            if (double.TryParse(ope.Text, out double angulo))
            {
                double tangente = Math.Tan(angulo * Math.PI / 180);
                ope.Text = tangente.ToString();
            }
        }
    }
}
