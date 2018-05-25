using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Speech.Recognition;
using Newtonsoft.Json;
using Q42.HueApi.Interfaces;
using Q42.HueApi;
using Q42.HueApi.ColorConverters;
using Q42.HueApi.ColorConverters.HSB;
using Q42.HueApi.ColorConverters.Original;
using Q42.HueApi.ColorConverters.OriginalWithModel;

namespace FormFinal
{
    public partial class Form1 : Form
    {
        bombilla bombilla1 = new bombilla(true, 254, "none", true, 254, new List<string>() { "0.139", "0.081" }, "none");
        List<bombilla> bombillas = new List<bombilla>();
        WebResponse response = null;

        #region Conexion con la API
        public void conexion()
        {

            try
            {
                //Indicamos la URL de nuestro controlador Philips HUE
                string usuario = "b7KL4u8mYpzYwG7q14D4zr1zVG63-kor6ncAkXAj";
                string URL = "http://" + txtURL1.Text + "." + txtURL2.Text + "." + txtURL3.Text + "." + txtURL4.Text /*192.168.1.120*/ + "/api";
                string uri = string.Format("{0}/{1}/lights/1/state", URL, usuario);

                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);

                httpWebRequest.ContentType = "application/JSON";

                //Aqui ponemos el metodo de acceso a la API
                httpWebRequest.Method = "PUT"; //POST

                string requestBody = string.Empty;
                int contador = 1;

                if (bombillas.Count > 1)
                {
                    foreach (var bombilla in bombillas)
                    {
                        //Para añadir el nombre del dispositivo que queremos modificar
                        //en este caso las bombillas empezaran desde el 1 en adelante
                        requestBody += "\"" + contador.ToString() + "\":";
                        //en nuestro body añade los datos de la clase bombilla en formato JSON
                        requestBody += JsonConvert.SerializeObject(bombilla);
                        requestBody += ",";
                        contador = contador + 1;
                    }
                }
                else
                {
                    foreach (var bombilla in bombillas)
                    {
                        requestBody += JsonConvert.SerializeObject(bombilla);
                        requestBody += ",";
                    }
                }

                //Para quitar la ultima , del JSON
                if (!string.IsNullOrEmpty(requestBody))
                    requestBody = requestBody.Remove(requestBody.Length - 1);

                httpWebRequest.AllowAutoRedirect = true;

                //transformo el string en un array de bytes que mandare al request para enviar a la API
                byte[] bytes = Encoding.UTF8.GetBytes(requestBody);

                httpWebRequest.ContentLength = bytes.Length;

                using (Stream outputStream = httpWebRequest.GetRequestStream())
                {
                    outputStream.Write(bytes, 0, bytes.Length);
                }

                //Aqui recibo la respuesta y la trato como una cadena de texto
                string strResult;

                response = httpWebRequest.GetResponse();

                using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                {

                    strResult = stream.ReadToEnd();

                    //Aqui tenemos que transformar el JSON que nos llega en un string
                    string reports = JsonConvert.DeserializeObject<string>(strResult);

                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                log.guardar(this, ex);
            }
            finally
            {

                if (response != null)
                {
                    response.Close();
                    response = null;
                }
            }
        }
        #endregion

        #region Validar numeros
        //Funcion que comprueba que no sea diferente a un numero
        public bool esValido(char c)
        {
            if ((c >= '0' && c <= '9'))
                return true;
            else
                return false;
        }

        //Funcion para obligar a meter solo numeros en los textBox
        public void soloNumeros(TextBox txtbox)
        {
            string ca = txtbox.Text;
            char[] c = ca.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (esValido(c[i]) == false)
                {
                    string mensaje = "Introduce solo numeros";
                    string titulo = "Error en datos";
                    MessageBoxButtons opciones = MessageBoxButtons.OK;
                    DialogResult result = MessageBox.Show(mensaje, titulo, opciones, MessageBoxIcon.Error);
                    txtbox.Text = "";
                }
            }

        }
        #endregion

        #region Limitar Textbox

        //Creamos un evento para los textbox donde el usuario meterá la direccion IP
        //Aunque tengamos 4 textbox con 1 evento nos sirve porque los 4 van a tirar 
        //del mismo evento
        private void txtURL_TextChanged(object sender, EventArgs e)
        {
            soloNumeros((TextBox)sender);
        }
        #endregion        

        public Form1()
        {
            InitializeComponent();
        }

        void reconocimiento(object sender, SpeechRecognizedEventArgs e)
        {
            txtTexto.Text = e.Result.Text;

            //Pasamos la direccion IP de nuestro controlador que nos está dando el usuario.
            ILocalHueClient controlador = new LocalHueClient(txtURL1.Text + "." + txtURL2.Text + "." + txtURL3.Text + "." + txtURL4.Text);

            //Aqui le damos el usuario que nos a proporcionado la API de Philips al registrarnos
            controlador.Initialize("b7KL4u8mYpzYwG7q14D4zr1zVG63-kor6ncAkXAj");

            //Creamos una variable para pasarle el comando que nosotros queramos a la bombilla
            var comando = new LightCommand();

            #region Casos de reconocimiento de voz
            switch (e.Result.Text)
            {

                case "enciendete":
                    try
                    {
                        comando.On = true;
                    }
                    catch (Exception ex)
                    {
                        log.guardar(this, ex);
                    }
                    break;

                case "apagate":

                    try
                    {
                        try
                        {
                            //comando.On = false;
                            foreach (var bombilla in bombillas)
                                bombilla.on = false;

                            //Indicamos la URL de nuestro controlador Philips HUE
                            string usuario = "b7KL4u8mYpzYwG7q14D4zr1zVG63-kor6ncAkXAj"; /*josemanuelDAM*/
                            string URL = "http://192.168.1.120/api";
                            string uri = String.Format("{0}/{1}/lights/1/state", URL, usuario);


                            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);

                            httpWebRequest.ContentType = "application/JSON";

                            //Aqui ponemos el metodo de acceso a la API
                            httpWebRequest.Method = "PUT"; //POST

                            string requestBody = string.Empty;
                            int contador = 1;

                            //if (bombillas.Count > 1)
                            //{
                            foreach (var bombilla in bombillas)
                            {
                                //Para añadir el nombre del dispositivo que queremos modificar
                                //en este caso las bombillas empezaran desde el 1 en adelante
                                /*requestBody += "\"" + contador.ToString() + "\":";*/
                                //en nuestro body añade los datos de la clase bombilla en formato JSON
                                requestBody += JsonConvert.SerializeObject(bombilla);
                                requestBody += ",";
                                //contador = contador + 1;
                            }
                            //}

                            //Para quitar la ultima , del JSON
                            if (!string.IsNullOrEmpty(requestBody))
                                requestBody = requestBody.Remove(requestBody.Length - 1);

                            httpWebRequest.AllowAutoRedirect = true;

                            //transformo el string en un array de bytes que mandare al request para enviar a la API
                            byte[] bytes = Encoding.UTF8.GetBytes(requestBody);

                            httpWebRequest.ContentLength = bytes.Length;

                            using (Stream outputStream = httpWebRequest.GetRequestStream())
                            {
                                outputStream.Write(bytes, 0, bytes.Length);
                            }

                            //Aqui recibo la respuesta y la trato como una cadena de texto
                            string strResult;

                            response = httpWebRequest.GetResponse();

                            using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                            {

                                strResult = stream.ReadToEnd();

                                //Aqui tenemos que transformar el JSON que nos llega en un string
                                string reports = JsonConvert.DeserializeObject<string>(strResult);

                                stream.Close();
                            }

                        }
                        catch (Exception ex)
                        {
                            string mensaje = ex.Message;
                        }
                        finally
                        {

                            if (response != null)
                            {
                                response.Close();
                                response = null;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        log.guardar(this, ex);
                    }
                    break;

                case "fiesta":
                    try
                    {
                        comando.Effect = Effect.ColorLoop;
                    }
                    catch (Exception ex)
                    {
                        log.guardar(this, ex);
                    }
                    break;

                case "bombilla azul":
                    try
                    {
                        comando.SetColor(new RGBColor("0000ff"));
                        controlador.SendCommandAsync(comando);
                    }
                    catch (Exception ex)
                    {
                        log.guardar(this, ex);
                    }
                    break;

                case "bombilla roja":
                    try
                    {
                        comando.SetColor(new RGBColor("FF0000"));
                        controlador.SendCommandAsync(comando);
                    }
                    catch (Exception ex)
                    {
                        log.guardar(this, ex);
                    }
                    break;

                case "bombilla verde":
                    try
                    {
                        comando.SetColor(new RGBColor("00FF00"));
                        controlador.SendCommandAsync(comando);
                    }
                    catch (Exception ex)
                    {
                        log.guardar(this, ex);
                    }
                    break;

                case "bombilla amarilla":
                    try
                    {
                        comando.SetColor(new RGBColor("ffff00"));
                        controlador.SendCommandAsync(comando);
                    }
                    catch (Exception ex)
                    {
                        log.guardar(this, ex);
                    }
                    break;

                case "bombilla blanca":
                    try
                    {
                        comando.SetColor(new RGBColor("ffffff"));
                        controlador.SendCommandAsync(comando);
                    }
                    catch (Exception ex)
                    {
                        log.guardar(this, ex);
                    }
                    break;

                case "bombilla rosa":
                    try
                    {
                        comando.SetColor(new RGBColor("ff00ff"));
                        controlador.SendCommandAsync(comando);
                    }
                    catch (Exception ex)
                    {
                        log.guardar(this, ex);
                    }
                    break;

                case "bombilla morada":
                    try
                    {
                        comando.SetColor(new RGBColor("bf00ff"));
                        controlador.SendCommandAsync(comando);
                    }
                    catch (Exception ex)
                    {
                        log.guardar(this, ex);
                    }
                    break;

                case "menos luz":
                    try
                    {
                        comando.Brightness = 100;
                        controlador.SendCommandAsync(comando);
                    }
                    catch (Exception ex)
                    {
                        log.guardar(this, ex);
                    }
                    break;

                case "mas luz":
                    try
                    {
                        comando.BrightnessIncrement = 254;
                        controlador.SendCommandAsync(comando);
                    }
                    catch (Exception ex)
                    {
                        log.guardar(this, ex);
                    }
                    break;

                case "cerrar programa":
                    Application.Exit();
                    break;

            }
            #endregion
        }


        #region Pone en funcionamiento el motor de reconocimiento de voz
        private void btnEmpezar_Click(object sender, EventArgs e)
        {
            if (txtURL1.Text == "" || txtURL2.Text == "" || txtURL3.Text == "" || txtURL4.Text == "")
            {
                MessageBox.Show("Introduce alguna direccion IP", "Error");
            }
            else
            {

                Choices lista = new Choices();
                SpeechRecognitionEngine grabar = new SpeechRecognitionEngine();

                lista.Add(new string[] {"bombilla azul", "bombilla roja", "bombilla verde", "bombilla amarilla", "bombilla blanca",
            "bombilla rosa", "bombilla morada", "fiesta"/*multicolor*/, "apagate", "enciendete", "mas luz", "menos luz", "cerrar programa"});

                Grammar gramatica = new Grammar(new GrammarBuilder(lista));

                try
                {
                    grabar.SetInputToDefaultAudioDevice();
                    grabar.LoadGrammar(gramatica);
                    grabar.SpeechRecognized += reconocimiento;
                    grabar.RecognizeAsync(RecognizeMode.Multiple);

                }
                catch (Exception ex)
                {
                    log.guardar(this, ex);
                }
            }

        }
        #endregion      

    }
}
