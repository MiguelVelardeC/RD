using Artexacta.App.Desgravamen;
using Artexacta.App.Desgravamen.BLL;
using Artexacta.App.Documents;
using EvoPdf;
using EvoPdf.PdfMerge;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Test_TestPdf : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnHacerPDf_Click(object sender, EventArgs e)
    {
        int citaId = 46;

        CitaDesgravamen objCita = CitaDesgravamenBLL.GetCitaDesgravamenById(citaId);
        byte[] pdfBytesExamenMedico = null;
        if (objCita.NecesitaExamen)
        {
            pdfBytesExamenMedico = CitaMedica.GetExamenMedicoEnPdf(citaId,"");
        }

        List<byte[]> pdfBytesLabos = new List<byte[]>();
        if (pdfBytesExamenMedico != null)
            pdfBytesLabos.Add(pdfBytesExamenMedico);

        List<ProgramacionCitaLabo> listaLabos = ProgramacionCitaLaboBLL.GetProgramacionCitaLabo(citaId);
        foreach (ProgramacionCitaLabo progLabo in listaLabos)
        {
            foreach (DocumentFile objDoc in progLabo.LaboratorioFiles)
            {
                pdfBytesLabos.Add(objDoc.Bytes);
            }
        }

        EvoPdf.PdfMerge.PdfDocumentOptions pdfDocumentOptions = new EvoPdf.PdfMerge.PdfDocumentOptions();
        pdfDocumentOptions.PdfCompressionLevel = EvoPdf.PdfMerge.PDFCompressionLevel.Normal;
        pdfDocumentOptions.PdfPageSize = EvoPdf.PdfMerge.PdfPageSize.Letter;
        pdfDocumentOptions.PdfPageOrientation = EvoPdf.PdfMerge.PDFPageOrientation.Portrait;
        EvoPdf.PdfMerge.PDFMerge objMerger = new EvoPdf.PdfMerge.PDFMerge(pdfDocumentOptions);

        foreach (byte[] pdfBytes in pdfBytesLabos)
        {
            MemoryStream inputStream = new MemoryStream(pdfBytes);
            objMerger.AppendPDFStream(inputStream, 0);
            inputStream.Close();
        }

        byte[] fullOutput = objMerger.RenderMergedPDFDocument();

        // send the PDF document as a response to the browser for download
        System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
        response.Clear();
        response.AddHeader("Content-Type", "application/pdf");

        response.AddHeader("Content-Disposition", String.Format("attachment; filename=InformeCaso45.pdf; size={0}", fullOutput.Length.ToString()));
        response.BinaryWrite(fullOutput);
        // Note: it is important to end the response, otherwise the ASP.NET
        // web page will render its content to PDF document stream
        response.End();
    }
}