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
            if (e.Result.Text == "negro")
            {
                negro.Visible = true;
            }
            else if (e.Result.Text == "azul")
            {
                azul.Visible = true;
            }
            else if (e.Result.Text == "rojo")
            {
                rojo.Visible = true;
            }
            else if (e.Result.Text == "verde")
            {
                verde.Visible = true;
            }
            else if (e.Result.Text == "amarillo")
            {
                amarillo.Visible = true;
            }
            else if (e.Result.Text == "blanco")
            {
                blanco.Visible = true;
            }
            else if (e.Result.Text == "marron")
            {
                marron.Visible = true;
            }
            else if (e.Result.Text == "rosa")
            {
                rosa.Visible = true;
            }
            else if (e.Result.Text == "morado")
            {
                morado.Visible = true;
            }
            else if (e.Result.Text == "todos")
            {
                negro.Visible = true;
                azul.Visible = true;
                rojo.Visible = true;
                verde.Visible = true;
                amarillo.Visible = true;
                blanco.Visible = true;
                marron.Visible = true;
                rosa.Visible = true;
                morado.Visible = true;

            }
            else if (e.Result.Text == "ninguno")
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
            }
            else if(e.Result.Text == "Salir")
            {
                Application.Exit();
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

            Choices lista = new Choices();
            SpeechRecognitionEngine grabar = new SpeechRecognitionEngine();

            lista.Add(new string[] {"negro", "azul", "rojo", "verde", "amarillo", "blanco",
        "marron", "rosado", "morado", "todos", "ninguno", "salir"});

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




        }
    }
}

