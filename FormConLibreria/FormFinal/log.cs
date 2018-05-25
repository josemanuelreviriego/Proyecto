using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormFinal
{
    class log
    {
        public static void guardar(object obj, Exception e)
        {
            string fecha = DateTime.Now.ToString("yyyyMMdd");
            string hora = DateTime.Now.ToString("HH:mm:ss");
            //Aqui tenemos una coleccion 
            char[] letras = {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L',
            'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'};
            StreamWriter sw = null;
            bool aux = false;

            for (int i = 0; i < letras.Length || aux == true; i++)
            {
                try
                {
                    string path = letras[i] + ":\\FormFinal - copia\\log\\log" + fecha + ".txt";
                    sw = new StreamWriter(path, true);
                    aux = true;
                }
                catch (Exception a)
                {
                    aux = false;
                }
            }

            StackTrace stacktrace = new StackTrace();

            sw.WriteLine(obj.GetType().FullName + " " + fecha + " " + hora);
            sw.WriteLine(stacktrace.GetFrame(1).GetMethod().Name + " - " + e.Message);
            sw.WriteLine("");

            sw.Flush();
            sw.Close();
        }
    }
}
