using Artexacta.App.User;
using Artexacta.App.User.BLL;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Services;
using Telerik.Web.UI;
using Artexacta.App.Especialidad.BLL;
using Artexacta.App.Especialidad;
using Artexacta.App.Proveedor;
using Artexacta.App.Proveedor.BLL;
using Artexacta.App.Enfermedad.BLL;
using Artexacta.App.Enfermedad;
using Artexacta.App.CLAMedicamento.BLL;
using Artexacta.App.CLAMedicamento;
using Artexacta.App.MedicamentoLINAME;
using Artexacta.App.MedicamentoLINAME.BLL;
using Artexacta.App.TipoMedicamento;
using Artexacta.App.TipoMedicamento.BLL;
using Artexacta.App.TipoConcentracion;
using Artexacta.App.TipoConcentracion.BLL;
using Artexacta.App.Medico.BLL;
using Artexacta.App.Medico;
using Artexacta.App.Paciente;
using Artexacta.App.Paciente.BLL;
using Artexacta.App.CodigoArancelario.BLL;
using Artexacta.App.CodigoArancelario;
using Artexacta.App.EnfermedadCronica;
using Artexacta.App.EnfermedadCronica.BLL;
using Artexacta.App.Desgravamen;
using Artexacta.App.Desgravamen.BLL;
using Artexacta.App.Ciudad.BLL;
using Artexacta.App.Ciudad;
using Artexacta.App.CLAPrestacionOdontologica.BLL;
using Artexacta.App.CLAPrestacionOdontologica;
using System.Text.RegularExpressions;
using Artexacta.App.Poliza.BLL;
using Artexacta.App.Poliza;
/// <summary>
/// Summary description for ComboBoxWebServices
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class ComboBoxWebServices : System.Web.Services.WebService
{

    private static readonly ILog log = LogManager.GetLogger("Standard");

    public ComboBoxWebServices()
    {
        //InitializeComponent(); 
    }

    [WebMethod]
    public static PropuestoAsegurado CargarPAConCI(string ci, string clienteId)
    {
        PropuestoAsegurado result = null;
        try
        {
            int intClienteId = Convert.ToInt32(clienteId);
            result = PropuestoAseguradoBLL.GetPropuestoAseguradoByCI(ci, intClienteId);
        }
        catch
        {
            result = new PropuestoAsegurado();
        }

        return result;
    }

    [WebMethod]
    public RadComboBoxData GetUsuarios(RadComboBoxContext context)
    {
        string filter = context.Text;
        int? start = context.NumberOfItems;
        int? numItems = Convert.ToInt32(ConfigurationManager.AppSettings["radComboPageSize"]);

        RadComboBoxData returnData = new RadComboBoxData();

        List<RadComboBoxItemData> result = new List<RadComboBoxItemData>();
        RadComboBoxData comboData = new RadComboBoxData();

        try
        {
            int? totalRows = 0;
            int? itemsPerRequest = numItems;
            int? itemOffset = start;
            int? endOffset = itemOffset + itemsPerRequest;

            List<User> lista = null;
            lista = UserBLL.GetUsersForAutoComplete(start, numItems, filter, ref totalRows);

            if (endOffset > totalRows)
            {
                endOffset = totalRows;
            }

            result = new List<RadComboBoxItemData>((int)(endOffset - itemOffset));

            foreach (User theUser in lista)
            {
                RadComboBoxItemData itemData = new RadComboBoxItemData();
                itemData.Text = theUser.FullName;
                itemData.Value = theUser.UserId.ToString();
                result.Add(itemData);
            }

            if (lista.Count == 0)
            {
                comboData.Message = Resources.Glossary.ComboBoxNoMatchesText;
            }
            else
            {
                string format = "";

                format = Resources.Glossary.ItemsLoadedFromWebServiceMessage;

                comboData.Message = String.Format(format, endOffset.ToString(), totalRows.ToString());
            }
        }
        catch (Exception q)
        {
            log.Error("Failed to load data for auto complete", q);

            comboData.Message = Resources.Glossary.ErrorLoadingDataFromWebServiceMessage;
        }

        comboData.Items = result.ToArray();
        return comboData;
    }
    //

    [WebMethod]
    public RadComboBoxData GetUsuariosActivo(RadComboBoxContext context)
    {
        string filter = context.Text;
        int? start = context.NumberOfItems;
        int? numItems = Convert.ToInt32(ConfigurationManager.AppSettings["radComboPageSize"]);

        RadComboBoxData returnData = new RadComboBoxData();

        List<RadComboBoxItemData> result = new List<RadComboBoxItemData>();
        RadComboBoxData comboData = new RadComboBoxData();

        try
        {
            int? totalRows = 0;
            int? itemsPerRequest = numItems;
            int? itemOffset = start;
            int? endOffset = itemOffset + itemsPerRequest;

            List<User> lista = null;

            lista = UserBLL.GetUsersActiveForAutoComplete(start, numItems, filter, ref totalRows);

            if (endOffset > totalRows)
            {
                endOffset = totalRows;
            }

            result = new List<RadComboBoxItemData>((int)(endOffset - itemOffset));

            foreach (User theUser in lista)
            {
                RadComboBoxItemData itemData = new RadComboBoxItemData();
                itemData.Text = theUser.FullName;
                itemData.Value = theUser.UserId.ToString();
                result.Add(itemData);
            }

            if (lista.Count == 0)
            {
                comboData.Message = Resources.Glossary.ComboBoxNoMatchesText;
            }
            else
            {
                string format = "";

                format = Resources.Glossary.ItemsLoadedFromWebServiceMessage;

                comboData.Message = String.Format(format, endOffset.ToString(), totalRows.ToString());
            }
        }
        catch (Exception q)
        {
            log.Error("Failed to load data for auto complete", q);

            comboData.Message = Resources.Glossary.ErrorLoadingDataFromWebServiceMessage;
        }

        comboData.Items = result.ToArray();
        return comboData;
    }

    [WebMethod]
    public RadComboBoxData GetMedicos(RadComboBoxContext context)
    {
        string filter = context.Text;
        int? start = context.NumberOfItems;
        int? numItems = Convert.ToInt32(ConfigurationManager.AppSettings["radComboPageSize"]);

        RadComboBoxData returnData = new RadComboBoxData();

        List<RadComboBoxItemData> result = new List<RadComboBoxItemData>();
        RadComboBoxData comboData = new RadComboBoxData();

        try
        {
            int? totalRows = 0;
            int? itemsPerRequest = numItems;
            int? itemOffset = start;
            int? endOffset = itemOffset + itemsPerRequest;

            List<Medico> lista = new List<Medico>();
            totalRows = MedicoBLL.SearchMedico(ref lista, "[USER].[fullname] LIKE '%" + filter + "%' AND [RED].[ClienteId] = 4 AND [MED].[IsCallCenter] = 1", (int)start, (int)numItems);

            if (endOffset > totalRows)
            {
                endOffset = totalRows;
            }

            result = new List<RadComboBoxItemData>((int)(endOffset - itemOffset));

            foreach (Medico theUser in lista)
            {
                RadComboBoxItemData itemData = new RadComboBoxItemData();
                User user = UserBLL.GetUserById(theUser.UserId);
                Ciudad ciudad = CiudadBLL.GetCiudadDetails(user.CiudadId);
                itemData.Text = theUser.NombreForDisplay + " - " + theUser.Especialidad + " - " + ciudad.Nombre;
                itemData.Value = theUser.MedicoId.ToString();
                result.Add(itemData);
            }

            if (lista.Count == 0)
            {
                comboData.Message = Resources.Glossary.ComboBoxNoMatchesText;
            }
            else
            {
                string format = "";

                format = Resources.Glossary.ItemsLoadedFromWebServiceMessage;

                comboData.Message = String.Format(format, endOffset.ToString(), totalRows.ToString());
            }
        }
        catch (Exception q)
        {
            log.Error("Failed to load data for auto complete", q);

            comboData.Message = Resources.Glossary.ErrorLoadingDataFromWebServiceMessage;
        }

        comboData.Items = result.ToArray();
        return comboData;
    }
    [WebMethod]
    public RadComboBoxData GetUsuariosMedicos(RadComboBoxContext context)
    {
        string filter = context.Text;
        int? start = context.NumberOfItems;
        int? numItems = Convert.ToInt32(ConfigurationManager.AppSettings["radComboPageSize"]);
        int ClientId = Convert.ToInt32("0" + context["ClientId"]);

        RadComboBoxData returnData = new RadComboBoxData();

        List<RadComboBoxItemData> result = new List<RadComboBoxItemData>();
        RadComboBoxData comboData = new RadComboBoxData();

        try
        {
            int? totalRows = 0;
            int? itemsPerRequest = numItems;
            int? itemOffset = start;
            int? endOffset = itemOffset + itemsPerRequest;
            string filterCliente = ClientId > 0 ? " AND [RED].[ClienteId] = " + ClientId : "";

            List<Medico> lista = new List<Medico>();
            totalRows = MedicoBLL.SearchMedico(ref lista, "[USER].[fullname] LIKE '%" + filter + "%'" + filterCliente, (int)start, (int)numItems);

            if (endOffset > totalRows)
            {
                endOffset = totalRows;
            }

            result = new List<RadComboBoxItemData>((int)(endOffset - itemOffset));

            foreach (Medico theUser in lista)
            {
                RadComboBoxItemData itemData = new RadComboBoxItemData();
                itemData.Text = theUser.NombreForDisplay + " - " + theUser.Especialidad;
                itemData.Value = theUser.UserId.ToString();
                result.Add(itemData);
            }

            if (lista.Count == 0)
            {
                comboData.Message = Resources.Glossary.ComboBoxNoMatchesText;
            }
            else
            {
                string format = "";

                format = Resources.Glossary.ItemsLoadedFromWebServiceMessage;

                comboData.Message = String.Format(format, endOffset.ToString(), totalRows.ToString());
            }
        }
        catch (Exception q)
        {
            log.Error("Failed to load data for auto complete", q);

            comboData.Message = Resources.Glossary.ErrorLoadingDataFromWebServiceMessage;
        }

        comboData.Items = result.ToArray();
        return comboData;
    }

    [WebMethod]
    public RadComboBoxData GetPacientes(RadComboBoxContext context)
    {
        string filter = context.Text;
        int? start = context.NumberOfItems;
        int? numItems = Convert.ToInt32(ConfigurationManager.AppSettings["radComboPageSize"]);
        int ClientId = Convert.ToInt32("0" + context["ClientId"]);
        bool useAsegurado = (context["UseAsegurado"] == null ? "0" : context["UseAsegurado"].ToString()) == "1";

        RadComboBoxData returnData = new RadComboBoxData();

        List<RadComboBoxItemData> result = new List<RadComboBoxItemData>();
        RadComboBoxData comboData = new RadComboBoxData();

        try
        {
            int? totalRows = 0;
            int? itemsPerRequest = numItems;
            int? itemOffset = start;
            int? endOffset = itemOffset + itemsPerRequest;

            List<Paciente> lista = new List<Paciente>();
            totalRows = PacienteBLL.SearchPaciente(ref lista, ClientId, filter, (int)numItems, (int)start, true);

            if (endOffset > totalRows)
            {
                endOffset = totalRows;
            }

            result = new List<RadComboBoxItemData>((int)(endOffset - itemOffset));

            foreach (Paciente theUser in lista)
            {
                RadComboBoxItemData itemData = new RadComboBoxItemData();
                itemData.Text = theUser.Nombre + " [" + theUser.CodigoAsegurado + "]";
                itemData.Value = (useAsegurado ? theUser.AseguradoId : theUser.PacienteId).ToString();
                result.Add(itemData);
            }

            if (lista.Count == 0)
            {
                comboData.Message = Resources.Glossary.ComboBoxNoMatchesText;
            }
            else
            {
                string format = "";

                format = Resources.Glossary.ItemsLoadedFromWebServiceMessage;

                comboData.Message = String.Format(format, endOffset.ToString(), totalRows.ToString());
            }
        }
        catch (Exception q)
        {
            log.Error("Failed to load data for auto complete", q);

            comboData.Message = Resources.Glossary.ErrorLoadingDataFromWebServiceMessage;
        }

        comboData.Items = result.ToArray();
        return comboData;
    }

    [WebMethod]
    public RadComboBoxData GetTipoMedicamentoByMedicamentoClaId(RadComboBoxContext context)
    {
        int MedicamentoClaId = int.Parse("0" + context["MedicamentoClaId"].ToString());

        RadComboBoxData returnData = new RadComboBoxData();

        List<RadComboBoxItemData> result = new List<RadComboBoxItemData>();
        RadComboBoxData comboData = new RadComboBoxData();

        try
        {

            List<TipoMedicamento> lista = null;
            lista = TipoMedicamentoBLL.GetTipoMedicamentoByMedicamentoClaId(MedicamentoClaId);

            result = new List<RadComboBoxItemData>(lista.Count);

            foreach (TipoMedicamento theTipoMedicamento in lista)
            {
                RadComboBoxItemData itemData = new RadComboBoxItemData();
                itemData.Text = theTipoMedicamento.Nombre;
                itemData.Value = theTipoMedicamento.TipoMedicamentoId.ToString();
                result.Add(itemData);
            }

            if (lista.Count == 0)
            {
                comboData.Message = Resources.Glossary.ComboBoxNoMatchesText;
            }
            else
            {
                string format = "";

                format = Resources.Glossary.ItemsLoadedFromWebServiceMessage;
            }
        }
        catch (Exception q)
        {
            log.Error("Failed to load data for auto complete", q);

            comboData.Message = Resources.Glossary.ErrorLoadingDataFromWebServiceMessage;
        }

        comboData.Items = result.ToArray();
        return comboData;
    }

    [WebMethod]
    public RadComboBoxData GetTipoConcentracionByMedicamentoClaId(RadComboBoxContext context)
    {
        int MedicamentoClaId = int.Parse("0" + context["MedicamentoClaId"].ToString());
        string TipoMedicamentoId = context["TipoMedicamentoId"].ToString();

        RadComboBoxData returnData = new RadComboBoxData();

        List<RadComboBoxItemData> result = new List<RadComboBoxItemData>();
        RadComboBoxData comboData = new RadComboBoxData();

        try
        {

            List<TipoConcentracion> lista = null;
            lista = TipoConcentracionBLL.GetTipoConcentracionByMedicamentoClaId(MedicamentoClaId, TipoMedicamentoId);

            result = new List<RadComboBoxItemData>(lista.Count);

            foreach (TipoConcentracion theTipoConcentracion in lista)
            {
                RadComboBoxItemData itemData = new RadComboBoxItemData();
                itemData.Text = theTipoConcentracion.Nombre;
                itemData.Value = theTipoConcentracion.TipoConcentracionId.ToString();
                result.Add(itemData);
            }

            if (lista.Count == 0)
            {
                comboData.Message = Resources.Glossary.ComboBoxNoMatchesText;
            }
            else
            {
                string format = "";

                format = Resources.Glossary.ItemsLoadedFromWebServiceMessage;
            }
        }
        catch (Exception q)
        {
            log.Error("Failed to load data for auto complete", q);

            comboData.Message = Resources.Glossary.ErrorLoadingDataFromWebServiceMessage;
        }

        comboData.Items = result.ToArray();
        return comboData;
    }

    [WebMethod]
    public RadComboBoxData GetEnfermedades(RadComboBoxContext context)
    {
        string filter = context.Text;
        int? start = context.NumberOfItems;
        int? numItems = Convert.ToInt32(ConfigurationManager.AppSettings["radComboPageSize"]);

        RadComboBoxData returnData = new RadComboBoxData();

        List<RadComboBoxItemData> result = new List<RadComboBoxItemData>();
        RadComboBoxData comboData = new RadComboBoxData();

        try
        {
            int? totalRows = 0;
            int? itemsPerRequest = numItems;
            int? itemOffset = start;
            int? endOffset = itemOffset + itemsPerRequest;

            List<Enfermedad> lista = new List<Enfermedad>();
            totalRows = EnfermedadBLL.SearchEnfermedad(ref lista, (int)numItems, (int)start, filter);

            if (endOffset > totalRows)
            {
                endOffset = totalRows;
            }

            result = new List<RadComboBoxItemData>((int)(endOffset - itemOffset));

            foreach (Enfermedad theUser in lista)
            {
                RadComboBoxItemData itemData = new RadComboBoxItemData();
                itemData.Text = "[" + theUser.EnfermedadId + "] " + theUser.Nombre;
                itemData.Value = theUser.EnfermedadId;
                result.Add(itemData);
            }

            if (lista.Count == 0)
            {
                comboData.Message = Resources.Glossary.ComboBoxNoMatchesText;
            }
            else
            {
                string format = "";

                format = Resources.Glossary.ItemsLoadedFromWebServiceMessage;

                comboData.Message = String.Format(format, endOffset.ToString(), totalRows.ToString());
            }
        }
        catch (Exception q)
        {
            log.Error("Failed to load data for auto complete", q);

            comboData.Message = Resources.Glossary.ErrorLoadingDataFromWebServiceMessage;
        }

        comboData.Items = result.ToArray();
        return comboData;
    }

    [WebMethod]
    public RadComboBoxData GetMedicamentoLINAME(RadComboBoxContext context)
    {
        string filter = context.Text;
        int? start = context.NumberOfItems;
        int? numItems = Convert.ToInt32(ConfigurationManager.AppSettings["radComboPageSize"]);

        RadComboBoxData returnData = new RadComboBoxData();

        List<RadComboBoxItemData> result = new List<RadComboBoxItemData>();
        RadComboBoxData comboData = new RadComboBoxData();

        try
        {
            int? totalRows = 0;
            int? itemsPerRequest = numItems;
            int? itemOffset = start;
            int? endOffset = itemOffset + itemsPerRequest;

            List<MedicamentoLINAME> lista = new List<MedicamentoLINAME>();
            totalRows = MedicamentoLINAMEBLL.SearchMedicamentoLINAME(ref lista, (int)numItems, (int)start, filter);

            if (endOffset > totalRows)
            {
                endOffset = totalRows;
            }

            result = new List<RadComboBoxItemData>((int)(endOffset - itemOffset));

            foreach (MedicamentoLINAME theUser in lista)
            {
                RadComboBoxItemData itemData = new RadComboBoxItemData();
                itemData.Text = theUser.Nombre;
                itemData.Value = theUser.MedicamentoLINAMEId.ToString();
                itemData.Attributes.Add("Grupo", theUser.Grupo);
                itemData.Attributes.Add("Subgrupo", theUser.Subgrupo);
                result.Add(itemData);
            }

            if (lista.Count == 0)
            {
                comboData.Message = Resources.Glossary.ComboBoxNoMatchesText;
            }
            else
            {
                string format = "";

                format = Resources.Glossary.ItemsLoadedFromWebServiceMessage;

                comboData.Message = String.Format(format, endOffset.ToString(), totalRows.ToString());
            }
        }
        catch (Exception q)
        {
            log.Error("Failed to load data for auto complete", q);

            comboData.Message = Resources.Glossary.ErrorLoadingDataFromWebServiceMessage;
        }

        comboData.Items = result.ToArray();
        return comboData;
    }

    [WebMethod]
    public RadComboBoxData GetEspecialidad(RadComboBoxContext context)
    {
        string filter = context.Text;
        int? start = context.NumberOfItems;
        int? numItems = Convert.ToInt32(ConfigurationManager.AppSettings["radComboPageSize"]);

        RadComboBoxData returnData = new RadComboBoxData();

        List<RadComboBoxItemData> result = new List<RadComboBoxItemData>();
        RadComboBoxData comboData = new RadComboBoxData();

        try
        {
            int? totalRows = 0;
            int? itemsPerRequest = numItems;
            int? itemOffset = start;
            int? endOffset = itemOffset + itemsPerRequest;

            List<Especialidad> lista = EspecialidadBLL.GetEspecialidadesForAutoComplete(start, numItems, filter, ref totalRows);

            if (endOffset > totalRows)
            {
                endOffset = totalRows;
            }

            result = new List<RadComboBoxItemData>((int)(endOffset - itemOffset));

            foreach (Especialidad theEspecialidad in lista)
            {
                RadComboBoxItemData itemData = new RadComboBoxItemData();
                itemData.Text = theEspecialidad.Nombre;
                itemData.Value = theEspecialidad.EspecialidadId.ToString();
                result.Add(itemData);
            }

            if (lista.Count == 0)
            {
                comboData.Message = Resources.Glossary.ComboBoxNoMatchesText;
            }
            else
            {
                string format = "";

                format = Resources.Glossary.ItemsLoadedFromWebServiceMessage;

                comboData.Message = String.Format(format, endOffset.ToString(), totalRows.ToString());
            }
        }
        catch (Exception q)
        {
            log.Error("Failed to load data for auto complete", q);

            comboData.Message = Resources.Glossary.ErrorLoadingDataFromWebServiceMessage;
        }

        comboData.Items = result.ToArray();
        return comboData;
    }

    [WebMethod]
    public RadComboBoxData GetProveedorPorCiudad(RadComboBoxContext context)
    {
        return GetProveedorAutocompletePorCiudad(context);
        //int? start = context.NumberOfItems;
        //int? numItems = Convert.ToInt32(ConfigurationManager.AppSettings["radComboPageSize"]);
        //string ciudadId = context["ciudadId"].ToString();
        //string tipoPriveedor = context["tipoPriveedor"].ToString();
        //int redMedicaPaciente = int.Parse("0" + context["redMedicaPaciente"]);

        //RadComboBoxData returnData = new RadComboBoxData();

        //List<RadComboBoxItemData> result = new List<RadComboBoxItemData>();
        //RadComboBoxData comboData = new RadComboBoxData();


        //if (string.IsNullOrWhiteSpace(ciudadId))
        //{
        //    comboData.Message = "Debe seleccionar una Ciudad Primero";
        //}
        //else try
        //{
        //    int totalRows = 0;
        //    int? itemsPerRequest = numItems;
        //    int? itemOffset = start;
        //    int? endOffset = itemOffset + itemsPerRequest;

        //    List<Proveedor> lista = ProveedorBLL.getProveedorListPag(tipoPriveedor, ciudadId, redMedicaPaciente, (int)itemOffset, (int)itemsPerRequest, ref totalRows);

        //    if (endOffset > totalRows)
        //    {
        //        endOffset = totalRows;
        //    }

        //    result = new List<RadComboBoxItemData>((int)(endOffset - itemOffset));

        //    foreach (Proveedor theProveedor in lista)
        //    {
        //        RadComboBoxItemData itemData = new RadComboBoxItemData();
        //        itemData.Text = theProveedor.NombreCompletoOrJuridico;
        //        itemData.Value = theProveedor.ProveedorId.ToString();
        //        result.Add(itemData);
        //    }

        //    if (lista.Count == 0)
        //    {
        //        comboData.Message = Resources.Glossary.ComboBoxNoMatchesText;
        //    }
        //    else
        //    {
        //        string format = "";

        //        format = Resources.Glossary.ItemsLoadedFromWebServiceMessage;

        //        comboData.Message = String.Format(format, endOffset.ToString(), totalRows.ToString());
        //    }
        //}
        //catch (Exception q)
        //{
        //    log.Error("Failed to load data for auto complete", q);

        //    comboData.Message = Resources.Glossary.ErrorLoadingDataFromWebServiceMessage;
        //}

        //comboData.Items = result.ToArray();
        //return comboData;
    }

    [WebMethod]
    public RadComboBoxData GetProveedorAutocompletePorCiudad(RadComboBoxContext context)
    {
        string filter = context.Text;
        int? start = context.NumberOfItems;
        int? numItems = Convert.ToInt32(ConfigurationManager.AppSettings["radComboPageSize"]);
        string ciudadId = context["ciudadId"].ToString();
        string tipoPriveedor = context["tipoPriveedor"].ToString();
        int redMedicaPaciente = int.Parse("0" + context["redMedicaPaciente"]);

        RadComboBoxData returnData = new RadComboBoxData();

        List<RadComboBoxItemData> result = new List<RadComboBoxItemData>();
        RadComboBoxData comboData = new RadComboBoxData();


        if (string.IsNullOrWhiteSpace(ciudadId))
        {
            comboData.Message = "Debe seleccionar una Ciudad Primero";
        }
        else try
            {
                int totalRows = 0;
                int? itemsPerRequest = numItems;
                int? itemOffset = start;
                int? endOffset = itemOffset + itemsPerRequest;

                List<Proveedor> lista = ProveedorBLL.getProveedorAutocomplete(tipoPriveedor, ciudadId, redMedicaPaciente, (int)itemOffset, (int)itemsPerRequest, filter, ref totalRows);

                if (endOffset > totalRows)
                {
                    endOffset = totalRows;
                }

                result = new List<RadComboBoxItemData>((int)(endOffset - itemOffset));

                foreach (Proveedor theProveedor in lista)
                {
                    RadComboBoxItemData itemData = new RadComboBoxItemData();
                    itemData.Text = theProveedor.NombreCompletoOrJuridico +
                        (string.IsNullOrWhiteSpace(theProveedor.NombreEspecialidad) ? "" :
                        " [" + theProveedor.NombreEspecialidad + "]");
                    itemData.Value = theProveedor.ProveedorId.ToString();
                    result.Add(itemData);
                }

                if (lista.Count == 0)
                {
                    comboData.Message = Resources.Glossary.ComboBoxNoMatchesText;
                }
                else
                {
                    string format = "";

                    format = Resources.Glossary.ItemsLoadedFromWebServiceMessage;

                    comboData.Message = String.Format(format, endOffset.ToString(), totalRows.ToString());
                }
            }
            catch (Exception q)
            {
                log.Error("Failed to load data for auto complete", q);

                comboData.Message = Resources.Glossary.ErrorLoadingDataFromWebServiceMessage;
            }

        comboData.Items = result.ToArray();
        return comboData;
    }

    [WebMethod]
    public RadComboBoxData GetEspecialistasAutocompletePorCiudadYCliente(RadComboBoxContext context)
    {
        string filter = context.Text;
        int? start = context.NumberOfItems;
        int? numItems = Convert.ToInt32(ConfigurationManager.AppSettings["radComboPageSize"]);
        string ciudadId = context["ciudadId"].ToString();
        string clienteId = context["clienteId"].ToString();
        string especialidadId = context["especialidadId"].ToString();
        RadComboBoxData returnData = new RadComboBoxData();

        List<RadComboBoxItemData> result = new List<RadComboBoxItemData>();
        RadComboBoxData comboData = new RadComboBoxData();


        if (string.IsNullOrWhiteSpace(ciudadId))
        {
            comboData.Message = "Debe seleccionar una Ciudad Primero";
        }

        else if (string.IsNullOrWhiteSpace(especialidadId))
        {
            comboData.Message = "Debe seleccionar una Especialidad Primero";
        }
        else if (string.IsNullOrWhiteSpace(clienteId))
        {
            comboData.Message = "Debe seleccionar una Ciudad o Especialidad Primero";
        }
        else try
            {
                int totalRows = 0;
                int intClienteId = int.Parse(clienteId);
                int intEspecialidadId = int.Parse(especialidadId);
                int? itemsPerRequest = numItems;
                int? itemOffset = start;
                int? endOffset = itemOffset + itemsPerRequest;

                List<Medico> list = MedicoBLL.getEspecialistasAutocomplete(ciudadId, intClienteId, intEspecialidadId, (int)itemOffset, (int)itemsPerRequest, filter, ref totalRows);
                //List<Proveedor> lista = ProveedorBLL.getProveedorAutocomplete(tipoPriveedor, ciudadId, redMedicaPaciente, (int)itemOffset, (int)itemsPerRequest, filter, ref totalRows);

                if (endOffset > totalRows)
                {
                    endOffset = totalRows;
                }

                result = new List<RadComboBoxItemData>((int)(endOffset - itemOffset));

                foreach (Medico theMedico in list)
                {
                    RadComboBoxItemData itemData = new RadComboBoxItemData();
                    itemData.Text = theMedico.Nombre +
                        (string.IsNullOrWhiteSpace(theMedico.Especialidad) ? "" :
                        " [" + theMedico.Especialidad + "]");
                    itemData.Value = theMedico.MedicoId.ToString();
                    result.Add(itemData);
                }

                if (list.Count == 0)
                {
                    comboData.Message = Resources.Glossary.ComboBoxNoMatchesText;
                }
                else
                {
                    string format = "";

                    format = Resources.Glossary.ItemsLoadedFromWebServiceMessage;

                    comboData.Message = String.Format(format, endOffset.ToString(), totalRows.ToString());
                }
            }
            catch (Exception q)
            {
                log.Error("Failed to load data for auto complete", q);

                comboData.Message = Resources.Glossary.ErrorLoadingDataFromWebServiceMessage;
            }

        comboData.Items = result.ToArray();
        return comboData;
    }
    #region "Edwin Suyo"

    [WebMethod]
    public RadComboBoxData GetEspecialistasProveedorAutocompletePorCiudadYCliente(RadComboBoxContext context)
    {
        string filter = context.Text;
        int? start = context.NumberOfItems;
        int? numItems = Convert.ToInt32(ConfigurationManager.AppSettings["radComboPageSize"]);
        string ciudadId = context["ciudadId"].ToString();
        string clienteId = context["clienteId"].ToString();
        string especialidadId = context["especialidadId"].ToString();
        RadComboBoxData returnData = new RadComboBoxData();

        List<RadComboBoxItemData> result = new List<RadComboBoxItemData>();
        RadComboBoxData comboData = new RadComboBoxData();


        if (string.IsNullOrWhiteSpace(ciudadId))
        {
            comboData.Message = "Debe seleccionar una Ciudad Primero";
        }

        else if (string.IsNullOrWhiteSpace(especialidadId))
        {
            comboData.Message = "Debe seleccionar una Especialidad Primero";
        }
        else if (string.IsNullOrWhiteSpace(clienteId))
        {
            comboData.Message = "Debe seleccionar una Ciudad o Especialidad Primero";
        }
        else try
            {
                int totalRows = 0;
                int intClienteId = int.Parse(clienteId);
                int intEspecialidadId = int.Parse(especialidadId);
                int? itemsPerRequest = numItems;
                int? itemOffset = start;
                int? endOffset = itemOffset + itemsPerRequest;

                List<Medico> list = MedicoBLL.getEspecialistasProveedorAutocomplete(ciudadId, intClienteId, intEspecialidadId, (int)itemOffset, (int)itemsPerRequest, filter, ref totalRows);
                //List<Proveedor> lista = ProveedorBLL.getProveedorAutocomplete(tipoPriveedor, ciudadId, redMedicaPaciente, (int)itemOffset, (int)itemsPerRequest, filter, ref totalRows);

                if (endOffset > totalRows)
                {
                    endOffset = totalRows;
                }

                result = new List<RadComboBoxItemData>((int)(endOffset - itemOffset));

                foreach (Medico theMedico in list)
                {
                    RadComboBoxItemData itemData = new RadComboBoxItemData();
                    itemData.Text = theMedico.Nombre +
                        (string.IsNullOrWhiteSpace(theMedico.Especialidad) ? "" :
                        " [" + theMedico.Especialidad + "]");
                    itemData.Value = theMedico.MedicoId.ToString();
                    result.Add(itemData);
                }

                if (list.Count == 0)
                {
                    comboData.Message = Resources.Glossary.ComboBoxNoMatchesText;
                }
                else
                {
                    string format = "";

                    format = Resources.Glossary.ItemsLoadedFromWebServiceMessage;

                    comboData.Message = String.Format(format, endOffset.ToString(), totalRows.ToString());
                }
            }
            catch (Exception q)
            {
                log.Error("Failed to load data for auto complete", q);

                comboData.Message = Resources.Glossary.ErrorLoadingDataFromWebServiceMessage;
            }

        comboData.Items = result.ToArray();
        return comboData;
    }
    //edwin suyo
    [WebMethod]
    public RadComboBoxData GetEspecialistasAutocompletePorCiudadYClienteNew(RadComboBoxContext context)
    {
        string filter = context.Text;
        int? start = context.NumberOfItems;
        int? numItems = Convert.ToInt32(ConfigurationManager.AppSettings["radComboPageSize"]);
        string especialidadId = context["especialidadId"].ToString();
        RadComboBoxData returnData = new RadComboBoxData();

        List<RadComboBoxItemData> result = new List<RadComboBoxItemData>();
        RadComboBoxData comboData = new RadComboBoxData();


        if (string.IsNullOrWhiteSpace(especialidadId))
        {
            comboData.Message = "Debe seleccionar una Especialidad Primero";
        }
        else try
            {
                int totalRows = 0;
                int intEspecialidadId = int.Parse(especialidadId);
                int? itemsPerRequest = numItems;
                int? itemOffset = start;
                int? endOffset = itemOffset + itemsPerRequest;

                List<Medico> list = MedicoBLL.getEspecialistasAutocompleteNew(intEspecialidadId);
                //List<Proveedor> lista = ProveedorBLL.getProveedorAutocomplete(tipoPriveedor, ciudadId, redMedicaPaciente, (int)itemOffset, (int)itemsPerRequest, filter, ref totalRows);

                if (endOffset > totalRows)
                {
                    endOffset = totalRows;
                }

                result = new List<RadComboBoxItemData>((int)(endOffset - itemOffset));

                foreach (Medico theMedico in list)
                {
                    RadComboBoxItemData itemData = new RadComboBoxItemData();
                    itemData.Text = theMedico.Nombre +
                        (string.IsNullOrWhiteSpace(theMedico.Especialidad) ? "" :
                        " [" + theMedico.Especialidad + "]");
                    itemData.Value = theMedico.MedicoId.ToString();
                    result.Add(itemData);
                }

                if (list.Count == 0)
                {
                    comboData.Message = Resources.Glossary.ComboBoxNoMatchesText;
                }
                else
                {
                    string format = "";

                    format = Resources.Glossary.ItemsLoadedFromWebServiceMessage;

                    comboData.Message = String.Format(format, endOffset.ToString(), totalRows.ToString());
                }
            }
            catch (Exception q)
            {
                log.Error("Failed to load data for auto complete", q);

                comboData.Message = Resources.Glossary.ErrorLoadingDataFromWebServiceMessage;
            }

        comboData.Items = result.ToArray();
        return comboData;
    }

    [WebMethod]
    public RadComboBoxData GetProveedorAutocompletePorCiudadAndTipoEspecialidad(RadComboBoxContext context)
    {
        string filter = context.Text;
        int? start = context.NumberOfItems;
        int? numItems = Convert.ToInt32(ConfigurationManager.AppSettings["radComboPageSize"]);
        string ciudadId = context["ciudadId"].ToString();
        string tipoPriveedor = context["tipoPriveedor"].ToString();


        string especialidadId = (context["especialidadId"].ToString());

        string redMedicaPaciente = (context["clienteId"].ToString());
        RadComboBoxData returnData = new RadComboBoxData();

        List<RadComboBoxItemData> result = new List<RadComboBoxItemData>();
        RadComboBoxData comboData = new RadComboBoxData();

        if (string.IsNullOrWhiteSpace(ciudadId))
        {
            comboData.Message = "Debe seleccionar una Ciudad Primero";
        }

        else if (string.IsNullOrWhiteSpace(especialidadId))
        {
            comboData.Message = "Debe seleccionar una Especialidad Primero";
        }
        else if (string.IsNullOrWhiteSpace(redMedicaPaciente))
        {
            comboData.Message = "Debe seleccionar una Ciudad o Especialidad Primero";
        }
        else try
            {
                int totalRows = 0;
                int? itemsPerRequest = numItems;
                int? itemOffset = start;
                int? endOffset = itemOffset + itemsPerRequest;

                List<Proveedor> lista = ProveedorBLL.getProveedorxEspeciliadAutocomplete(tipoPriveedor, ciudadId, int.Parse(redMedicaPaciente), int.Parse(especialidadId), (int)itemOffset, (int)itemsPerRequest, filter, ref totalRows);

                if (endOffset > totalRows)
                {
                    endOffset = totalRows;
                }

                result = new List<RadComboBoxItemData>((int)(endOffset - itemOffset));

                foreach (Proveedor theProveedor in lista)
                {
                    RadComboBoxItemData itemData = new RadComboBoxItemData();
                    itemData.Text = theProveedor.NombreCompletoOrJuridico;
                    itemData.Value = theProveedor.ProveedorId.ToString();
                    result.Add(itemData);
                }

                if (lista.Count == 0)
                {
                    comboData.Message = Resources.Glossary.ComboBoxNoMatchesText;
                }
                else
                {
                    string format = "";

                    format = Resources.Glossary.ItemsLoadedFromWebServiceMessage;

                    comboData.Message = String.Format(format, endOffset.ToString(), totalRows.ToString());
                }
            }
            catch (Exception q)
            {
                log.Error("Failed to load data for auto complete", q);

                comboData.Message = Resources.Glossary.ErrorLoadingDataFromWebServiceMessage;
            }


        comboData.Items = result.ToArray();
        return comboData;
    }

    //Para Mostrar Los Prestaciones Odontologicas Por Proveedor y Ciudad y cliente 
    [WebMethod]
    public RadComboBoxData GetPrestacionesOdontologicasAutocompleteProveedorYCliente(RadComboBoxContext context)
    {

        string ProveedorId = context["ProveedorId"].ToString();
        string clienteId = context["clienteId"].ToString();
        RadComboBoxData returnData = new RadComboBoxData();

        List<RadComboBoxItemData> result = new List<RadComboBoxItemData>();
        RadComboBoxData comboData = new RadComboBoxData();
        RadComboBoxItem comboData2 = new RadComboBoxItem();
        List<RadComboBoxItem> result2 = new List<RadComboBoxItem>();

        if (string.IsNullOrWhiteSpace(ProveedorId))
        {
            comboData.Message = "Debe seleccionar un Proveedor Primero";
        }
        else if (string.IsNullOrWhiteSpace(clienteId))
        {
            comboData.Message = "Debe seleccionar una Ciudad y Proveedor";
        }
        else try
            {

                int intClienteId = int.Parse(clienteId);
                int intProveedorId = int.Parse(ProveedorId);


                List<PrestacionOdontologica> list = PrestacionOdontologicaBLL.getAllPrestacionOdontologicaNew(intClienteId, intProveedorId);
                //List<Proveedor> lista = ProveedorBLL.getProveedorAutocomplete(tipoPriveedor, ciudadId, redMedicaPaciente, (int)itemOffset, (int)itemsPerRequest, filter, ref totalRows);



                foreach (PrestacionOdontologica PrestacionOdontologica in list)
                {
                    RadComboBoxItemData itemData = new RadComboBoxItemData();
                    itemData.Text = PrestacionOdontologica.Nombre;
                    itemData.Value = PrestacionOdontologica.PrestacionOdontologicaId.ToString();
                    itemData.Enabled = !PrestacionOdontologica.Categoria;

                    if (PrestacionOdontologica.Categoria)
                        itemData.Text = SetHighlight(PrestacionOdontologica.Nombre, PrestacionOdontologica.Nombre);

                    result.Add(itemData);

                    RadComboBoxItem items = new RadComboBoxItem();
                    items.Text = PrestacionOdontologica.Nombre;
                    items.Value = PrestacionOdontologica.PrestacionOdontologicaId.ToString();
                    items.IsSeparator = PrestacionOdontologica.Categoria;
                    result2.Add(items);


                }


            }
            catch (Exception q)
            {
                log.Error("Failed to load data for au to complete", q);

                comboData.Message = Resources.Glossary.ErrorLoadingDataFromWebServiceMessage;
            }

        comboData.Items = result.ToArray();

        return comboData;
    }
    public static string SetHighlight(string strOrginal, string strNeedle)
    {
        Regex regex = new Regex(strNeedle, RegexOptions.IgnoreCase);
        //return regex.Replace(strOrginal, "<b>" + strNeedle + "</b>");
        return regex.Replace(strOrginal, "<font color=red>" + strNeedle + "</font>");
    }
    #endregion
    [WebMethod]
    public RadComboBoxData GetSOATProveedorAutocompletePorCiudad(RadComboBoxContext context)
    {
        string filter = context.Text;
        int? start = context.NumberOfItems;
        int? numItems = Convert.ToInt32(ConfigurationManager.AppSettings["radComboPageSize"]);
        string ciudadId = context["ciudadId"].ToString();

        RadComboBoxData returnData = new RadComboBoxData();

        List<RadComboBoxItemData> result = new List<RadComboBoxItemData>();
        RadComboBoxData comboData = new RadComboBoxData();


        if (string.IsNullOrWhiteSpace(ciudadId))
        {
            comboData.Message = "Debe seleccionar una Ciudad Primero";
        }
        else try
            {
                int totalRows = 0;
                int? itemsPerRequest = numItems;
                int? itemOffset = start;
                int? endOffset = itemOffset + itemsPerRequest;

                List<Artexacta.App.SOAT.Proveedor.Proveedor> list = Artexacta.App.SOAT.Proveedor.BLL.ProveedorBLL.GetProveedorAutocomplete(ciudadId, (int)itemOffset, (int)itemsPerRequest, filter, ref totalRows);   //MedicoBLL.getEspecialistasAutocomplete(ciudadId, intClienteId, (int)itemOffset, (int)itemsPerRequest, filter, ref totalRows);
                //List<Proveedor> lista = ProveedorBLL.getProveedorAutocomplete(tipoPriveedor, ciudadId, redMedicaPaciente, (int)itemOffset, (int)itemsPerRequest, filter, ref totalRows);

                if (endOffset > totalRows)
                {
                    endOffset = totalRows;
                }

                result = new List<RadComboBoxItemData>((int)(endOffset - itemOffset));

                foreach (Artexacta.App.SOAT.Proveedor.Proveedor theProveedor in list)
                {
                    RadComboBoxItemData itemData = new RadComboBoxItemData();
                    itemData.Text = theProveedor.Nombre;
                    itemData.Value = theProveedor.ProveedorId.ToString();
                    result.Add(itemData);
                }

                if (list.Count == 0)
                {
                    comboData.Message = Resources.Glossary.ComboBoxNoMatchesText;
                }
                else
                {
                    string format = "";

                    format = Resources.Glossary.ItemsLoadedFromWebServiceMessage;

                    comboData.Message = String.Format(format, endOffset.ToString(), totalRows.ToString());
                }
            }
            catch (Exception q)
            {
                log.Error("Failed to load data for auto complete", q);

                comboData.Message = Resources.Glossary.ErrorLoadingDataFromWebServiceMessage;
            }

        comboData.Items = result.ToArray();
        return comboData;
    }


    [WebMethod]
    public RadComboBoxData GetMedicamentos(RadComboBoxContext context)
    {
        string filter = context.Text;
        int? start = context.NumberOfItems;
        int? numItems = Convert.ToInt32(ConfigurationManager.AppSettings["radComboPageSize"]);

        RadComboBoxData returnData = new RadComboBoxData();

        List<RadComboBoxItemData> result = new List<RadComboBoxItemData>();
        RadComboBoxData comboData = new RadComboBoxData();
        try
        {
            int totalRows = 0;
            int? itemsPerRequest = numItems;
            int? itemOffset = start;
            int? endOffset = itemOffset + itemsPerRequest;

            List<Medicamento> lista = new List<Medicamento>();
            totalRows = CLAMedicamentoBLL.SearchMedicamento(ref lista, (int)itemsPerRequest, (int)itemOffset, filter);

            if (endOffset > totalRows)
            {
                endOffset = totalRows;
            }

            result = new List<RadComboBoxItemData>((int)(endOffset - itemOffset));

            foreach (Medicamento theMedicamento in lista)
            {
                RadComboBoxItemData itemData = new RadComboBoxItemData();
                itemData.Text = theMedicamento.Nombre;
                itemData.Value = theMedicamento.MedicamentoId.ToString();
                result.Add(itemData);
            }

            if (lista.Count == 0)
            {
                comboData.Message = Resources.Glossary.ComboBoxNoMatchesText;
            }
            else
            {
                string format = "";

                format = Resources.Glossary.ItemsLoadedFromWebServiceMessage;

                comboData.Message = String.Format(format, endOffset.ToString(), totalRows.ToString());
            }
        }
        catch (Exception q)
        {
            log.Error("Failed to load data for auto complete", q);

            comboData.Message = Resources.Glossary.ErrorLoadingDataFromWebServiceMessage;
        }

        comboData.Items = result.ToArray();
        return comboData;
    }

    [WebMethod]
    public RadComboBoxData GetCodigoArancelarioAutocomplete(RadComboBoxContext context)
    {
        string filter = context.Text;
        int? start = context.NumberOfItems;
        int? numItems = Convert.ToInt32(ConfigurationManager.AppSettings["radComboPageSize"]);

        RadComboBoxData returnData = new RadComboBoxData();

        List<RadComboBoxItemData> result = new List<RadComboBoxItemData>();
        RadComboBoxData comboData = new RadComboBoxData();
        try
        {
            int totalRows = 0;
            int? itemsPerRequest = numItems;
            int? itemOffset = start;
            int? endOffset = itemOffset + itemsPerRequest;

            List<CodigoArancelario> lista = new List<CodigoArancelario>();
            totalRows = CodigoArancelarioBLL.SearchCodigoArancelario(ref lista, (int)itemsPerRequest, (int)itemOffset, filter);

            if (endOffset > totalRows)
            {
                endOffset = totalRows;
            }

            result = new List<RadComboBoxItemData>((int)(endOffset - itemOffset));

            foreach (CodigoArancelario theCodigoArancelario in lista)
            {
                RadComboBoxItemData itemData = new RadComboBoxItemData();
                itemData.Text = theCodigoArancelario.Nombre;
                itemData.Value = theCodigoArancelario.CodigoArancelarioId;
                result.Add(itemData);
            }

            if (lista.Count == 0)
            {
                comboData.Message = Resources.Glossary.ComboBoxNoMatchesText;
            }
            else
            {
                string format = "";

                format = Resources.Glossary.ItemsLoadedFromWebServiceMessage;

                comboData.Message = String.Format(format, endOffset.ToString(), totalRows.ToString());
            }
        }
        catch (Exception q)
        {
            log.Error("Failed to load data for auto complete", q);

            comboData.Message = Resources.Glossary.ErrorLoadingDataFromWebServiceMessage;
        }

        comboData.Items = result.ToArray();
        return comboData;
    }

    [WebMethod]
    public RadComboBoxData GetEnfermedadCronicaAutocomplete(RadComboBoxContext context)
    {
        string filter = context.Text;
        int? start = context.NumberOfItems;
        int? numItems = Convert.ToInt32(ConfigurationManager.AppSettings["radComboPageSize"]);

        RadComboBoxData returnData = new RadComboBoxData();

        List<RadComboBoxItemData> result = new List<RadComboBoxItemData>();
        RadComboBoxData comboData = new RadComboBoxData();
        try
        {
            int totalRows = 0;
            int? itemsPerRequest = numItems;
            int? itemOffset = start;
            int? endOffset = itemOffset + itemsPerRequest;

            List<EnfermedadCronica> lista = new List<EnfermedadCronica>();
            totalRows = EnfermedadCronicaBLL.SearchEnfermedadCronica(ref lista, (int)itemsPerRequest, (int)itemOffset, filter);

            if (endOffset > totalRows)
            {
                endOffset = totalRows;
            }

            result = new List<RadComboBoxItemData>((int)(endOffset - itemOffset));

            foreach (EnfermedadCronica theEnfermedadCronica in lista)
            {
                RadComboBoxItemData itemData = new RadComboBoxItemData();
                itemData.Text = theEnfermedadCronica.Nombre;
                itemData.Value = theEnfermedadCronica.EnfermedadCronicaId.ToString();
                result.Add(itemData);
            }

            if (lista.Count == 0)
            {
                comboData.Message = Resources.Glossary.ComboBoxNoMatchesText;
            }
            else
            {
                string format = "";

                format = Resources.Glossary.ItemsLoadedFromWebServiceMessage;

                comboData.Message = String.Format(format, endOffset.ToString(), totalRows.ToString());
            }
        }
        catch (Exception q)
        {
            log.Error("Failed to load data for auto complete", q);

            comboData.Message = Resources.Glossary.ErrorLoadingDataFromWebServiceMessage;
        }

        comboData.Items = result.ToArray();
        return comboData;
    }

    [WebMethod]
    public RadComboBoxData GetPolizaxPacienteId(RadComboBoxContext context)
    {
        string filter = context.Text;
        int? start = context.NumberOfItems;
        int? numItems = Convert.ToInt32(ConfigurationManager.AppSettings["radComboPageSize"]);
        string vPacienteId = context["PacienteId"].ToString();
        RadComboBoxData returnData = new RadComboBoxData();

        List<RadComboBoxItemData> result = new List<RadComboBoxItemData>();
        RadComboBoxData comboData = new RadComboBoxData();


        if (string.IsNullOrWhiteSpace(vPacienteId) | vPacienteId == "0" | vPacienteId.Length == 0)
        {
            comboData.Message = "Debe seleccionar un Paciente  Primero";
        }
        else try
            {
                int totalRows = 0;
                int PacienteId = int.Parse(vPacienteId);
                int? itemsPerRequest = numItems;
                int? itemOffset = start;
                int? endOffset = itemOffset + itemsPerRequest;

                List<Poliza> lista = PolizaBLL.GetPolizaByPacienteId(PacienteId);
                totalRows = lista.Count;
                if (lista.Count > start)
                {
                    if (endOffset > totalRows)
                    {
                        endOffset = totalRows;
                    }
                    result = new List<RadComboBoxItemData>((int)(endOffset - itemOffset));


                    foreach (Poliza thePoliza in lista)
                    {
                        RadComboBoxItemData itemData = new RadComboBoxItemData();
                        itemData.Text = thePoliza.NombreJuridicoCliente+" - "+ thePoliza.NumeroPoliza + " - " + thePoliza.Estado;
                        itemData.Value = thePoliza.PolizaId.ToString();
                        result.Add(itemData);
                    }

                    if (lista.Count == start)
                    {
                        comboData.Message = Resources.Glossary.ComboBoxNoMatchesText;
                    }
                    else
                    {
                        string format = "";

                        format = Resources.Glossary.ItemsLoadedFromWebServiceMessage;

                        comboData.Message = String.Format(format, endOffset.ToString(), totalRows.ToString());
                    }
                }
                else
                {
                    comboData.Message = "No Hay MAs Datos";
                }
            }
            catch (Exception q)
            {
                // log.Error("Failed to load data for auto complete", q);

                //comboData.Message = Resources.Glossary.ErrorLoadingDataFromWebServiceMessage;
            }

        comboData.Items = result.ToArray();
        return comboData;
    }


    [WebMethod]
    public RadComboBoxData GetMedicoGeneralxCiudadxCliente(RadComboBoxContext context)
    {
        string filter = context.Text;
        int? start = context.NumberOfItems;
        int? numItems = Convert.ToInt32(ConfigurationManager.AppSettings["radComboPageSize"]);
        string PolizaId = context["PolizaId"].ToString();
        string PacienteId = context["PacienteId"].ToString();
        RadComboBoxData returnData = new RadComboBoxData();

        List<RadComboBoxItemData> result = new List<RadComboBoxItemData>();
        RadComboBoxData comboData = new RadComboBoxData();


        if (string.IsNullOrWhiteSpace(PolizaId) | PolizaId == "0" | PolizaId.Length == 0)
        {
            comboData.Message = "Debe seleccionar una Cliente  Primero";
        }
        else try
            {
                string UserName = HttpContext.Current.User.Identity.Name;
                User objUser = UserBLL.GetUserByUsername(UserName);
                string CiudadId = objUser.CiudadId;
                int clienteId = 0;
                int IdPaciente = Convert.ToInt32(PacienteId);
                List<Poliza> lista = PolizaBLL.GetPolizaByPacienteId(IdPaciente);
                for (int i = 0; i < lista.Count; i++)
                {
                    if (lista[i].PolizaId == Convert.ToInt32(PolizaId))
                    {
                        clienteId = lista[i].ClienteId;
                        break;
                    }
                }
                int totalRows = 0;
                int? itemsPerRequest = numItems;
                int? itemOffset = start;
                int? endOffset = itemOffset + itemsPerRequest;

                string RolAdmin = System.Web.Configuration.WebConfigurationManager.AppSettings["RolAdmin"];
                Artexacta.App.Validacion.BLL.ValidacionBLL obj = new Artexacta.App.Validacion.BLL.ValidacionBLL();
                if (obj.VerificarRol(RolAdmin))
                {
                    CiudadId = null;
                }

                List<Medico> _cache = new List<Medico>();
                string EspecialidadMG = System.Web.Configuration.WebConfigurationManager.AppSettings["EspecialidadMG"];
                int EspecialidadId = EspecialidadBLL.GetEspecialidadxNombre(EspecialidadMG).EspecialidadId;
                int cant = MedicoBLL.GetALLMedico(_cache, CiudadId, clienteId, EspecialidadId);
                totalRows = cant;
                List<Medico> list = new List<Medico>();
                int userId = UserBLL.GetUserIdByUsername(User.Identity.Name);
                //que tipo de Rol
                //string RolAdmin = System.Web.Configuration.WebConfigurationManager.AppSettings["RolAdmin"];
                string RolRecepcionista = System.Web.Configuration.WebConfigurationManager.AppSettings["RolRecepcionista"];
                //Artexacta.App.Validacion.BLL.ValidacionBLL obj = new Artexacta.App.Validacion.BLL.ValidacionBLL();
                if (obj.VerificarRol(RolAdmin) | obj.VerificarRol(RolRecepcionista))
                {
                    list = _cache;
                }
                else
                {
                    Medico medico = null;

                    medico = MedicoBLL.getMedicoByUserId(userId);
                    if (medico != null)
                    {
                        //verificamos si es medico general
                        if (medico.EspecialidadId == EspecialidadId)
                        {
                            for (int i = 0; i < _cache.Count; i++)
                            {
                                if (_cache[i].UserId == userId)
                                {
                                    list.Add(_cache[i]);
                                    totalRows = 1;
                                }
                            }
                        }
                        else
                        {
                            list = _cache;
                        }
                    }
                    else
                    {
                        list = _cache;

                    }
                }

                if (endOffset > totalRows)
                {
                    endOffset = totalRows;
                }

                result = new List<RadComboBoxItemData>((int)(endOffset - itemOffset));
                foreach (Medico theMedico in list)
                {
                    RadComboBoxItemData itemData = new RadComboBoxItemData();
                    itemData.Text = theMedico.Nombre;
                    itemData.Value = theMedico.UserId.ToString();
                    result.Add(itemData);
                }

                if (list.Count == 0)
                {
                    comboData.Message = Resources.Glossary.ComboBoxNoMatchesText;
                }
                else
                {
                    string format = "";

                    format = Resources.Glossary.ItemsLoadedFromWebServiceMessage;

                    comboData.Message = String.Format(format, cant.ToString(), cant.ToString());
                }

            }
            catch (Exception q)
            {
                log.Error("Failed to load data for auto complete", q);

                comboData.Message = Resources.Glossary.ErrorLoadingDataFromWebServiceMessage;
            }

        comboData.Items = result.ToArray();
        return comboData;
    }



}
