using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artexacta.App.Notificacion.BLL;
using Artexacta.App.Utilities.SystemMessages;
using log4net;
using System.Data;
using System.Text.RegularExpressions;


public partial class Mantenimiento_Notificacion_Notificacion : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            cargar();
            //Prueba

        }
        
    }

    void cargar() 
    {
        Tiponotificaciondp.DataSource = NotificacionBLL.getTipoNotificacionesListar();
        Tiponotificaciondp.DataValueField = "tiponotificacionid";
        Tiponotificaciondp.DataTextField = "nombre";
        Tiponotificaciondp.DataBind();

        gruponotificaciondp.DataSource = NotificacionBLL.getGrupoNotificacionesListar();
        gruponotificaciondp.DataValueField = "gruponotificacionid";
        gruponotificaciondp.DataTextField = "nombre";
        gruponotificaciondp.DataBind();


        DateTime fec = DateTime.Now;
        Fechainiciotxt.Text= fec.ToString();
        Fechafintxt.Text= fec.ToString();
        Prioridadtxt.Text = "0";
        Tiponotificaciondp.SelectedValue = "1";
        gruponotificaciondp.SelectedValue= "1";
        Titulotxt.Text= "";
        Descripciontxt.Text = "";
        lblfoto.Text = "";
      
    }

    public int  procesocargarfoto() 
    {
        // Validamos si el control tiene archivo
        if (fotoFUP.HasFile)
        {
            //Validamos que sea PNG y JPEG
            if (fotoFUP.PostedFile.ContentType == "image/png" || fotoFUP.PostedFile.ContentType == "image/jpeg")
            {
                //Obtener el tamaño del archivo
                int fileSize = fotoFUP.PostedFile.ContentLength;
                //  2,100,000 bytes (approximately 2 MB).
                if (fileSize < 2100000)
                {
                    //Ruta donde se guardará el archivo
                    string SavePath = System.Configuration.ConfigurationManager.AppSettings["DirectoryNotificaciones"].ToString();
                    //string appPath = Request.PhysicalApplicationPath;
                    //Obtener nombre del archivo
                    //String fileName = fotoFUP.FileName;
                    string fname = Server.HtmlEncode(fotoFUP.FileName);

                    //Obtener la extensión
                    string extension = System.IO.Path.GetExtension(fname);

                    string ruta= SavePath + fname;

                    //string mappath=Server.MapPath(ruta);

                    if ((extension == ".jpg") || (extension == ".jpeg") || (extension == ".png"))
                    {

                        int id =NotificacionBLL.FileNotificacionesInsert(fname, extension, fileSize, ruta);
                        string ide=id.ToString();

                        string imagen = ide  + extension;
                        //Actualizamos la ruta de Fileid agregado
                        NotificacionBLL.FileNotificacionesUpdate(id, SavePath + imagen);
                        //Guardamos el archivo en nuestro repositorio
                        fotoFUP.SaveAs(SavePath + imagen);
                        return id;
                    }
                    else 
                    {
                        lblfoto.Text = "La extensión no es valida";
                        return -1;
                    }
                }
                else 
                {
                    lblfoto.Text = "El archivo supera el tamaño máximo permitido de 2MB";
                    return -1;
                }
            }
            else 
            {
                lblfoto.Text = "Debe seleccionar un archivo JPG,JPEG o PNG.";
                return -1;
            }
        }
        else 
        {
            lblfoto.Text = "Debe seleccionar un archivo de tipo imagen.";
            return -1;
        }
    
    }
  
    protected void NotificacionDataSource_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Ocurrio un error al Obtener la lista de Notificaciones");
            log.Error("Ocurrio un error al Obtener la lista de Notificaciones", e.Exception);
            e.ExceptionHandled = true;
        }
    }

    protected void NotificacionGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteRecord")
        {
            int Notificacionid = Convert.ToInt32(e.CommandArgument);

            //string fecha = lblFecha.Text;
            //string fechacreacion = CitasGridView.Rows[0].Cells[2].Text;
            //string fechaCita = CitasGridView.Rows[0].Cells[3].Text;


            NotificacionBLL.NotificacionUpdateStatus(Notificacionid);
         
            SystemMessages.DisplaySystemMessage("Se inactivo la notificación " + Notificacionid);
            NotificacionGridView.DataBind();
            return;

        }
    }

    public static string validarfecha(string fecha) 
    {
        Regex RX = new Regex(@"^([0-9]{2})\/([0-9]{2})\/([0-9]{4}) (20|21|22|23|[0-1]?\d{1}):([0-5]?\d{1}):([0-5]?\d{1})$"); // aquí colocar la expresión de validación

        if (!RX.IsMatch(fecha)) 
        {
            return "El formato ingresado debe ser: dd/mm/yyyy hh:mm:ss";
        } 
        else 
        {
            return "OK";
        
        }
    
    }
    protected void SaveButton_Click(object sender, EventArgs e)
    {

        int id = procesocargarfoto();
        if (id != 0 && id != -1) 
        {
            string Descripcion = Descripciontxt.Text;
            int tiponotificacionid = Int32.Parse(Tiponotificaciondp.SelectedValue);
            string GrupoNotificacion = gruponotificaciondp.SelectedItem.ToString();
            DateTime fechastart = DateTime.Parse(Fechainiciotxt.Text);
            DateTime fechaend = DateTime.Parse(Fechafintxt.Text);
            int Prioridad = Int32.Parse(Prioridadtxt.Text);
            string titulo = Titulotxt.Text;
            int fileid = id;

            int res = NotificacionBLL.NotificacionInsert(Descripcion, tiponotificacionid,
                GrupoNotificacion, fechastart, fechaend, Prioridad, titulo, fileid);
            NotificacionGridView.DataBind();
            
            SystemMessages.DisplaySystemMessage("Se ha creado "+res+" notificación.");
            cargar();
        }
    }

    protected void btnlimpiar_Click(object sender, EventArgs e)
    {
        cargar();
    }
}

