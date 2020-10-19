using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliente
{
    public class DatosServidor
    {

        public string Accion { get; set; }
        public string Datos { get; set; }

        public DatosServidor()
        {

        }

        public DatosServidor(string accion, string datos)
        {
            Accion = accion;
            Datos = datos;
        }

        public DatosServidor(string datos)
        {
            int sepIndex = datos.IndexOf(":", StringComparison.Ordinal);
            Accion = datos.Substring(0, sepIndex);
            Datos = datos.Substring(Accion.Length + 1);
        }

        public string Serializar()
        {
            return string.Format("{0}:{1}", Accion, Datos);
        }

        public static implicit operator string(DatosServidor datos)
        {
            return datos.Serializar();
        }

        //Acciones a realizar con la lista

        public static string SerializarLista(List<string> lista)
        {
            if (lista.Count == 0)
            {
                return null;
            }

            bool esElPrimero = true;
            var salida = new StringBuilder();

            foreach (var dato in lista)
            {
                if (esElPrimero)
                {
                    salida.Append(dato);
                    esElPrimero = false;
                }
                else
                {
                    salida.Append(string.Format(",{0}", dato));
                }
            }
            return salida.ToString();
        }

        public static List<string> DeserializarLista(string entrada)
        {
            string str = entrada;
            var lista = new List<string>();

            if (string.IsNullOrEmpty(str))
            {
                return lista;
            }

            try
            {
                foreach (string dato in entrada.Split(','))
                {
                    lista.Add(dato);
                }
            }
            catch (Exception)
            {
                return null;
            }

            return lista;
        }


    }
}
