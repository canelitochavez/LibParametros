using System;
using System.Xml;
using System.Windows.Forms;


namespace LibParametros
{
    public class Parametros
    {
        #region "Atributos"
        private string servidor;
        private string baseDatos;
        private string usuario;
        private string clave;
        private bool seguridadIntegrada;
        private string archivoParametros;
        private string cadenaConexion;
        private string error;
        private XmlDocument xml = new XmlDocument();
        private XmlNode nodo;
        #endregion

        #region "Constructor"
        public Parametros()
        {
            servidor = "";
            baseDatos = "";
            usuario = "";
            clave = "";
            seguridadIntegrada = true;
            archivoParametros = "";
            cadenaConexion = "";
            error = "";
        }
        #endregion

        #region "Propiedades"
        public string CadenaConexion
        {
            get { return cadenaConexion; }
        }
        public string Error
        {
            get { return error; }
        }
        #endregion

        #region "Métodos Públicos"
        public bool GenerarCadenaConexion(string nombreArchivoParametros)
        {
            archivoParametros = Application.StartupPath + "\\" + nombreArchivoParametros;
            try
            {
                xml.Load(archivoParametros);
                nodo = xml.SelectSingleNode("//Servidor");
                servidor = nodo.InnerText;
                nodo = xml.SelectSingleNode("//BaseDatos");
                baseDatos = nodo.InnerText;
                nodo = xml.SelectSingleNode("//Usuario");
                usuario = nodo.InnerText;
                nodo = xml.SelectSingleNode("//Clave");
                clave = nodo.InnerText;
                nodo = xml.SelectSingleNode("//SeguridadIntegrada");
                seguridadIntegrada = Convert.ToBoolean(nodo.InnerText);

                if (seguridadIntegrada) //Autenticacion Windows
                {
                    cadenaConexion = "Data Source=" + servidor
                        + ";Initial Catalog=" + baseDatos
                        + ";Integrated Security=True";
                }
                else //Autenticacion SQL
                {
                    cadenaConexion = "Data Source=" + servidor
                        + ";Initial Catalog=" + baseDatos
                        + ";User Id=" + usuario
                        + ";Password=" + clave
                        + ";Integrated Security=True";
                }
                xml = null;
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                xml = null;
                return false;
            }
        }
        #endregion

    }
}

