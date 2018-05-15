using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reconocimiento_de_voz
{
    class bombilla
    {
        //Aqui diremos si la bombilla está encendida o no
        bool On;

        //Aqui pondremos el brillo que tendra en este efecto la bombilla con minimo 1 y maximo 254
        int Bri;

        //En funcion de lo que pongamos "none" "select" o "Iselect" cambiara de color
        string Alert;

        //Aqui ponemos el color que queramos poner en nuestra bombilla en formato CIE        
        List<string> Xy;

        //Esto es si la bombilla será accesible por el controlador HUE
        bool Reachable;
        
        //Indicaremos la saturacion del color desde 254 hasta 0 que es blanco
        int Sat;

        //El efecto que queremos que tenga la bombilla o "none" o "colorloop" para que cambie siempre de color
        string Effect;

        public bool on
        {
            get
            {
                return On;
            }

            set
            {
                On = value;
            }
        }

        public int bri
        {
            get
            {
                return Bri;
            }

            set
            {
                Bri = value;
            }
        }

        public string alert
        {
            get
            {
                return Alert;
            }

            set
            {
                Alert = value;
            }
        }

        public List<string> xy
        {
            get
            {
                return Xy;
            }

            set
            {
                Xy = value;
            }
        }

        public bool reachable
        {
            get
            {
                return Reachable;
            }

            set
            {
                Reachable = value;
            }
        }

        public int sat
        {
            get
            {
                return Sat;
            }

            set
            {
                Sat = value;
            }
        }

        public string effect
        {
            get
            {
                return Effect;
            }

            set
            {
                Effect = value;
            }
        }

        public bombilla()
        {
        }

        public bombilla(bool on, int bri, string alert, List<string> xy, bool reachable, int sat, string effect)
        {
            On = on;
            Bri = bri;
            Alert = alert;
            Xy = xy;
            Reachable = reachable;
            Sat = sat;
            Effect = effect;
        }

        //public bombilla(bool on, int bri, string alert, List<string> xy, bool reachable, int sat, string effect)
        //{
        //    this.on = on;
        //    this.bri = bri;
        //    this.alert = alert;
        //    this.xy = xy;
        //    this.reachable = reachable;
        //    this.sat = sat;
        //    this.effect = effect;
        //}

        //public bool on { get => On; set => On = value; }
        //public int bri { get => Bri; set => Bri = value; }
        //public string alert { get => Alert; set => Alert = value; }
        //public List<string> xy { get => Xy; set => Xy = value; }
        //public bool reachable { get => Reachable; set => Reachable = value; }
        //public int sat { get => Sat; set => Sat = value; }
        //public string effect { get => Effect; set => Effect = value; }        
    }
}
