using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de VideoLlamadaDto
/// </summary>
public class VideoLlamadaDto
{
    public VideoLlamadaDto()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    public string RoomName { get; set; }
    public string Token { get; set; }

    public string appid { get; set; }
    public string appCertificate { get; set; }

}