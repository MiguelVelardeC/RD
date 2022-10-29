
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using System.Threading;
using Artexacta.App.ViewStateSql;
using Artexacta.App.WebServices.alianza;
using Artexacta.App.Utilities.Email;
using Artexacta.App.WebServices.alianza.BLL;
using Artexacta.App.Poliza.BLL;
using Artexacta.App.Utilities.Import;
using Artexacta.App.Paciente.BLL;
using System.Data;

namespace Artexacta.App.Utilities.TaskManager
{
    /// <summary>
    /// This class must have one method for every Task, being called: Task_[TaskId], without parameters
    /// </summary>
    public class TaskMethods
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        private static int Task_ViewState_Fails = 0;


        /// <summary>
        /// This task deletes all viewstate that are outdated. Just calls the BLL
        /// </summary>
        public static void Task_ExpireViewState()
        {
            log.Debug("BEGIN Task Expire ViewState");

            try
            {
                Artexacta.App.ViewStateSql.BLL.ViewStateSqlBLL.ExpireViewState();
                log.Info("Expired view state has been deleted from DB");
                Task_ViewState_Fails = 0;
            }
            catch(Exception q)
            {
                Task_ViewState_Fails++;
                if (Task_ViewState_Fails > 10)
                {
                    log.Error("Could not expire the view state, something wrong with database", q);
                }
                else
                {
                    log.Warn("Could not expire the view state, fail nb: " + Task_ViewState_Fails, q);
                }
            }

            log.Debug("END Task Expire ViewState");
        }

        public static void Task_AlianzaUpdate()
        {
            log.Info("BEGIN Task Alianza Update");

            DataTable asegurados = null;
            string errors = "";

            try
            {
                int validos = 0;
                asegurados = Artexacta.App.WebServices.alianza.BLL.EstadoAseguradoBLL.GetDataTableFromWebService(ref validos, ref errors);
                log.Info("VA a actualizar: " + asegurados.Rows.Count.ToString() + " registros");
            }
            catch (Exception q)
            {
                log.Error("Error al llamar el webservice, ver log", q);
                EmailUtilities.SendErrorWebServiceUpdate(q.StackTrace);

                log.Info("END Task Alianza Update");
                return;
            }

            int nbEnWebService = (asegurados != null ? asegurados.Rows.Count : 0);
            int clienteId = Artexacta.App.Configuration.Configuration.GetAlianzaClienteId();
            int nbActualizadosAActivo = 0;
            int nbActualizadosAInactivo = 0;
            int nbInsertados = 0;
            int nbNoValidos = 0;
            int nbErrores = 0;
            string errores = "";

            try
            {

                int totalAsegurados = asegurados.Rows.Count;

                ImportElement.ValidateNulls(ref asegurados, 0, true, ref errores);
                nbNoValidos = totalAsegurados - asegurados.Rows.Count;
                totalAsegurados = asegurados.Rows.Count;

                //errores += PacienteBLL.ValidateImportarPaciente(ref asegurados, clienteId, true);
                //nbNoValidos = totalAsegurados - asegurados.Rows.Count;
                //totalAsegurados = asegurados.Rows.Count;
                //PacienteBLL.ImportarPaciente(asegurados, clienteId, "SCZ", true, false, ref errores, ref nbActualizadosAActivo,
                //    ref nbActualizadosAInactivo, ref nbInsertados, ref nbErrores);

                int total = asegurados.Rows.Count;

                PacienteBLL.InactivarPolizas(clienteId);

                for (int i = 0; i < total; i++)
                {
                    DataTable tbl = new DataTable();
                    ImportElement.AddColumns(tbl);
                    tbl.Rows.Add(asegurados.Rows[i].ItemArray);
                    string rowError = "";
                    int actualizadosAActivo = 0;
                    int actualizadosAInactivo = 0;
                    int insertados = 0;
                    int intErrores = 0;
                    try
                    {
                        //PacienteBLL.ImportarPaciente(tbl, clienteId, "SCZ", i == 0, true, ref rowError, ref actualizadosAActivo
                        //    , ref actualizadosAInactivo, ref insertados, ref intErrores);
                        bool Insertado = false;
                        PacienteBLL.ImportarPacienteV2(tbl, clienteId, "SCZ", ref rowError, ref Insertado);
                        if ((tbl.Rows[0][ImportElement.EstadoPoliza]).ToString() == "ACTIVO")
                        {
                            actualizadosAActivo++;
                        }
                        else
                        {
                            actualizadosAInactivo++;
                        }
                        if (Insertado)
                        {
                            insertados++;
                        }
                    }
                    catch (Exception q)
                    {
                        log.Warn("Error al Insertar el Asegurado " + asegurados.Rows[i].ItemArray[0], q);
                        rowError += "Error al Insertar el Asegurado " + asegurados.Rows[i].ItemArray[0];
                        intErrores = 1;
                    }
                    if (!string.IsNullOrWhiteSpace(rowError))
                    {
                        errores += rowError;
                        nbErrores += intErrores;
                    }
                    else
                    {
                        nbActualizadosAActivo += actualizadosAActivo;
                        nbActualizadosAInactivo += actualizadosAInactivo;
                        nbInsertados += insertados;
                    }
                }
            }
            catch (Exception q)
            {
                log.Error("Error al actualizar la informacion del web service, ver log", q);
                EmailUtilities.SendErrorWebServiceUpdate(q.StackTrace);
            }

            string resumen = "Desde Alianza: " + nbEnWebService.ToString() +
                ", No Validos (error en entrada): " + nbNoValidos.ToString() +
                ", Errores: " + nbErrores.ToString() +
                ", Insertados: " + nbInsertados.ToString() +
                ", Convertidos a Activo: " + nbActualizadosAActivo.ToString() +
                ", Convertidos a Inactivo: " + nbActualizadosAInactivo.ToString();

            log.Info(resumen);

            EmailUtilities.SendEmailWebServiceUpdate(DateTime.Now, resumen + "<br />" + errores);
            log.Info("END Task Alianza Update");
        }
    }
}