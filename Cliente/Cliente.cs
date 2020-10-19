using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliente
{
    public partial class Cliente : Form
    {
        public static ConexionServidor conexionServidor = new ConexionServidor();
        public static String IP = "192.168.0.9";
        public const int puerto = 25565;
        public string usuario;
        public Cliente()
        {
            InitializeComponent();
            this.CenterToScreen();
        }


        #region Eventos
        //Botón que inicia sesión
        private void btnInicioSesion_Click(object sender, EventArgs e)
        {

            if (!conexionServidor.Conectar(IP, puerto))
            {
                MessageBox.Show("El servidor no se encuentra disponible, verifique que se encuentre abierto.", "Se ha presentado un error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                usuario = txtUsuario.Text;
                var mensaje = new DatosServidor("login", txtUsuario.Text + "," + txtPsswd.Text);
                conexionServidor.EnviarPaquete(mensaje);
            }
        }
        //Botón salir
        private void btnSalir_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void Cliente_Load(object sender, EventArgs e)
        {
            conexionServidor.OnDataRecieved += MensajeRecibido;
        }

        private void Cliente_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        #endregion

        #region Procedimientos
        //Método que recibe los mensajes del servidor
        private void MensajeRecibido(string datos)
        {
            var datosServidor = new DatosServidor(datos);
            string comando = datosServidor.Accion;
            ;
            string contenido = datosServidor.Datos;
            List<string> valores = DatosServidor.DeserializarLista(contenido);

            //Mensaje para permitir entrar al usuario
            if (comando.Equals("Ingresar"))
            {
                MessageBox.Show("Se ha iniciado sesión en el servidor", "Inicio de Sesion Correcto");
                //btnInicioSesion.Enabled = false;
            }

            if (comando.Equals("No Ingresar"))
            {
                MessageBox.Show("El servidor ha rechazado su sesion", "Inicio de Sesion Prohibido");
            }
            //Mensaje que contiene las notas del textBox del servidor
            if (comando.Equals("Nueva Notificacion"))
            {
                String notasServidor = string.Join(",", valores);
                MessageBox.Show(notasServidor, "Notif. del servidor");
            }
        }
        #endregion
    }
}
