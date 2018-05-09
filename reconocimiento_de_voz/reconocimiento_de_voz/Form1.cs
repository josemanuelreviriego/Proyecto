using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Collections;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Net;
using Newtonsoft.Json;
using System.IO;

namespace reconocimiento_de_voz
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }
        void reconocimiento(object sender, SpeechRecognizedEventArgs e)
        {
            switch (e.Result.Text)
            {
                case "negro":
                    negro.Visible = true;
                    break;

                case "azul":
                    azul.Visible = true;
                    break;

                case "rojo":
                    rojo.Visible = true;
                    break;

                case "verde":
                    verde.Visible = true;
                    break;

                case "amarillo":
                    amarillo.Visible = true;
                    break;

                case "blanco":
                    blanco.Visible = true;
                    break;

                case "marron":
                    marron.Visible = true;
                    break;

                case "rosa":
                    rosa.Visible = true;
                    break;

                case "morado":
                    morado.Visible = true;
                    break;

                case "todos":
                    negro.Visible = true;
                    azul.Visible = true;
                    rojo.Visible = true;
                    verde.Visible = true;
                    amarillo.Visible = true;
                    blanco.Visible = true;
                    marron.Visible = true;
                    rosa.Visible = true;
                    morado.Visible = true;
                    break;

                case "ninguno":
                    negro.Visible = false;
                    azul.Visible = false;
                    rojo.Visible = false;
                    verde.Visible = false;
                    amarillo.Visible = false;
                    blanco.Visible = false;
                    marron.Visible = false;
                    rosa.Visible = false;
                    morado.Visible = false;
                    break;

                case "salir":
                    Application.Exit();
                    break;

                default:
                    negro.Visible = false;
                    azul.Visible = false;
                    rojo.Visible = false;
                    verde.Visible = false;
                    amarillo.Visible = false;
                    blanco.Visible = false;
                    marron.Visible = false;
                    rosa.Visible = false;
                    morado.Visible = false;
                    break;

            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            negro.Visible = false;
            azul.Visible = false;
            rojo.Visible = false;
            verde.Visible = false;
            amarillo.Visible = false;
            blanco.Visible = false;
            marron.Visible = false;
            rosa.Visible = false;
            morado.Visible = false;

            //Creamos la lista que va a guardar el texto a reconocer y creamos tambien un objeto que será el motor de
            //nuestro reconocimiento de voz, despues añadimos las palabras permitidas a la lista

            Choices lista = new Choices();
            SpeechRecognitionEngine grabar = new SpeechRecognitionEngine();

            lista.Add(new string[] {"negro", "azul", "rojo", "verde", "amarillo", "blanco",
            "marron", "rosa", "morado", "todos", "ninguno", "salir"});

            Grammar gramatica = new Grammar(new GrammarBuilder(lista));

            try
            {
                grabar.SetInputToDefaultAudioDevice();
                grabar.LoadGrammar(gramatica);
                grabar.SpeechRecognized += reconocimiento;
                grabar.RecognizeAsync(RecognizeMode.Multiple);
            }
            catch (Exception)
            {
                throw;
            }

            //Creamos una variable para la respuesta de la web
            WebResponse response = null;

            try
            {
                //creamos una lista de bombillas en funcion de los dispositivos que tengamos               
                bombilla bombilla1 = new bombilla(true, 200, "none", new List<string>() { "0.4043", "0.4368" }, true, 254, "colorloop");
                bombilla bombilla2 = new bombilla(true, 200, "none", new List<string>() { "0.4043", "0.4368" }, true, 254, "colorloop");


                List<bombilla> bombillas = new List<bombilla>
                {
                    bombilla1,
                    bombilla2
                };

                //Indicamos la URL de nuestro controlador Philips HUE
                string usuario = "josemanuelDAM";
                string URL = "http://192.168.1.158/api";
                string uri = String.Format("{0}/{1}/lights", URL, usuario);

                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);

                httpWebRequest.ContentType = "application/JSON";

                //Aqui ponemos el metodo de acceso a la API
                httpWebRequest.Method = "POST";

                string requestBody = string.Empty;
                int contador = 1;

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

                //Para quitar la ultima , del JSON
                if (!String.IsNullOrEmpty(requestBody))
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
    }


}



