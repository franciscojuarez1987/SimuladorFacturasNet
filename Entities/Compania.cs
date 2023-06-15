using System.Xml.Serialization;

namespace WebAppSimulador.Entities
{
    public class Compania
    {
        [XmlElement("Nombre")]
        public string? NombreCompania { get; set; }
  
        [XmlElement("RazonSocial")]
        public string? RazonSocial { get; set; }
        
        [XmlElement("RFC")]
        public string? RFC { get; set; }

        [XmlElement("Direccion")]
        public string? Direccion { get; set; }

        [XmlElement("NumeroFactura")]
        public string? NoFactura { get; set; }

        [XmlElement("Fecha")]
        public string? FachaFactura { get; set; }

        [XmlElement("Documento")]
        public string? Documento { get; set; }

        [XmlElement("TipoDocumento")]
        public string? TipoDocumento { get; set; }

        [XmlElement("RegimenFiscal")]
        public string? RegimenFiscal { get; set; }

        [XmlElement("TipoRegimenFiscal")]
        public string? TipoRegimenFiscal { get; set; }

        [XmlElement("LugarExpedicion")]
        public string LugarExpedicion { get; set; }
    }
}
