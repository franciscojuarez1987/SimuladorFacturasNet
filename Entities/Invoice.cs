using System.Xml.Serialization;

namespace WebAppSimulador.Entities
{
    [XmlRoot("Comprobante")]
    public class Invoice
    {
        [XmlElement("Receptor")]
        public Cliente User { get; set; } = new Cliente();
        [XmlElement("Emisor")]
        public Compania Company { get; set; }= new Compania();

        [XmlElement("Productos")]
        public Compra Compra { get; set; } = new Compra();

        [XmlElement("DesgloseConceptos")]
        public Costos Conceptos { get; set; }=new Costos();
    }

    public class Productos
    {
        public string? ClaveUnidad { get; set; } = string.Empty;
        public string? ClaveProducto { get; set; } = string.Empty;
        public string? Descripcion { get; set; } = string.Empty;
        public int? Cantidad { get; set; }
        public double? Precio { get; set; }
        public double? Total { get; set; }
    }

    public class Compra
    {
        [XmlElement("Producto")]
        public Productos[] Products { get; set; }
    }
}
