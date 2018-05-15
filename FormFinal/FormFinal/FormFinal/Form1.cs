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

namespace FormFinal
{
    public partial class Form1 : Form
    {
        bombilla bombilla1 = new bombilla(true, 254, "none", new List<string>() { "0.139", "0.081" }, true, 254, "none");
        List<bombilla> bombillas = new List<bombilla>();
        WebResponse response = null;

        public void conexion()
        {
            
            try
            {
                //Indicamos la URL de nuestro controlador Philips HUE
                string usuario = "b7KL4u8mYpzYwG7q14D4zr1zVG63-kor6ncAkXAj"; /*josemanuelDAM*/
                string URL = "http://" + txtURL.Text /*192.168.1.120*/ + "/api";
                string uri = String.Format("{0}/{1}/lights/1/state", URL, usuario);

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

        public Form1()
        {
            InitializeComponent();

            bombilla bombilla1 = new bombilla(true, 254, "none", new List<string>() { "0.139", "0.081" }, true, 254, "none");
            List<bombilla> bombillas = new List<bombilla> { bombilla1 };
        }

        void reconocimiento(object sender, SpeechRecognizedEventArgs e)
        {
            txtTexto.Text = e.Result.Text;

            bombillas.Add(bombilla1);
            switch (e.Result.Text)
            {
                case "enciende":
                    foreach (var bombilla in bombillas)
                        bombilla.on = true;
                    conexion();
                    txtResponse.Text = response.ToString();
                    //try
                    //{
                    //    //Indicamos la URL de nuestro controlador Philips HUE
                    //    string usuario = "b7KL4u8mYpzYwG7q14D4zr1zVG63-kor6ncAkXAj"; /*josemanuelDAM*/
                    //    string URL = "http://192.168.1.120/api";
                    //    string uri = String.Format("{0}/{1}/lights/1/state", URL, usuario);

                    //    HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);

                    //    httpWebRequest.ContentType = "application/JSON";

                    //    //Aqui ponemos el metodo de acceso a la API
                    //    httpWebRequest.Method = "PUT"; //POST

                    //    string requestBody = string.Empty;
                    //    int contador = 1;

                    //    if (bombillas.Count > 1)
                    //    {
                    //        foreach (var bombilla in bombillas)
                    //        {
                    //            //Para añadir el nombre del dispositivo que queremos modificar
                    //            //en este caso las bombillas empezaran desde el 1 en adelante
                    //            requestBody += "\"" + contador.ToString() + "\":";
                    //            //en nuestro body añade los datos de la clase bombilla en formato JSON
                    //            requestBody += JsonConvert.SerializeObject(bombilla);
                    //            requestBody += ",";
                    //            contador = contador + 1;
                    //        }
                    //    }

                    //    //Para quitar la ultima , del JSON
                    //    if (!String.IsNullOrEmpty(requestBody))
                    //        requestBody = requestBody.Remove(requestBody.Length - 1);

                    //    httpWebRequest.AllowAutoRedirect = true;

                    //    //transformo el string en un array de bytes que mandare al request para enviar a la API
                    //    byte[] bytes = Encoding.UTF8.GetBytes(requestBody);

                    //    httpWebRequest.ContentLength = bytes.Length;

                    //    using (Stream outputStream = httpWebRequest.GetRequestStream())
                    //    {
                    //        outputStream.Write(bytes, 0, bytes.Length);
                    //    }

                    //    //Aqui recibo la respuesta y la trato como una cadena de texto
                    //    string strResult;

                    //    response = httpWebRequest.GetResponse();

                    //    using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                    //    {

                    //        strResult = stream.ReadToEnd();

                    //        //Aqui tenemos que transformar el JSON que nos llega en un string
                    //        string reports = JsonConvert.DeserializeObject<string>(strResult);

                    //        stream.Close();
                    //    }
                    //}
                    //catch (Exception ex)
                    //{
                    //    string mensaje = ex.Message;
                    //}
                    //finally
                    //{

                    //    if (response != null)
                    //    {
                    //        response.Close();
                    //        response = null;
                    //    }
                    //}
                    break;

                case "apaga":
                    try
                    {
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
                    break;

                case "fiesta":
                    try
                    {
                        foreach (var bombilla in bombillas)
                            bombilla.effect = "colorloop";

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
                    break;


            }
        }

        private void btnEmpezar_Click(object sender, EventArgs e)
        {
            Choices lista = new Choices();
            SpeechRecognitionEngine grabar = new SpeechRecognitionEngine();

            lista.Add(new string[] {"bombilla azul", "bombilla rojo", "bombilla verde", "bombilla amarillo", "bombilla blanco",
            "bombilla rosa", "bombilla morado", "fiesta"/*multicolor*/, "apaga", "enciende"});

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
                string mensaje = ex.Message;
            }

        }

    }
}
