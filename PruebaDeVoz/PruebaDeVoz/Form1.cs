using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition; //Reconocimiento de voz de Windows

namespace PruebaDeVoz
{
    public partial class Form1 : Form
    {
        //Iniciamos el motor de reconocimiento de voz
        SpeechRecognitionEngine Grabar = new SpeechRecognitionEngine();
        string palabras;
        public Form1()
        {
            InitializeComponent();
        }

        private void grabar_Click(object sender, EventArgs e)
        {
            //Inicia la escucha por el microfono predeterminado de windows
            Grabar.SetInputToDefaultAudioDevice();
            Grabar.LoadGrammar(new DictationGrammar()); //Carga la gramatica de Windows
            Grabar.SpeechRecognized += escuchando; //Controlador de eventos
            Grabar.RecognizeAsync(RecognizeMode.Multiple); //Inicia el reconocimiento
            MessageBox.Show("Te escucho");
        }

        void escuchando(object sender, SpeechRecognizedEventArgs e)
        {
            palabras = e.Result.Text; //Aqui va a ir cogiendo las palabras que son reconocidas
            textBox1.Text = palabras; //Pones las palabras reconocidas en el textbox
        }

        private void stop_Click(object sender, EventArgs e)
        {
            Grabar.RecognizeAsyncStop(); //Paramos la escucha
            MessageBox.Show("Escucha parada");
            textBox1.Clear(); // limpia el textbox
        }

        private void salir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
