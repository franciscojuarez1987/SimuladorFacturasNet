using System.Xml.Serialization;

namespace WebAppSimulador.Entities
{
    public class Cliente
    {
        [XmlElement("Nombre")]
        public string? Nombre { get; set; }

        [XmlElement("CorreoElectronico")]
        public string? CorreoElectronico { get; set; }

        [XmlElement("RFC")]
        public string? RFC { get; set; }

        [XmlElement("Direccion")]
        public string? Direccion { get; set; }

        [XmlElement("CFDI")]
        public string? CFDI { get; set; }

        [XmlElement("CFDITipo")]
        public string? CFDITipo { get; set; }

        [XmlElement("MetodoPago")]
        public string? MetodoPago { get; set; }

        [XmlElement("TipoMetodoPago")]
        public string? TipoMetodoPago { get; set; }

        [XmlElement("FormaPago")]
        public string? FormaPago { get; set; }

        [XmlElement("TipoFormaPago")]
        public string? TipoFormaPago { get; set; }


    }
}
