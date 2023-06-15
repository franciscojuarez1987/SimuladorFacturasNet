using System.Xml.Serialization;

namespace WebAppSimulador.Entities
{
    public class Costos
    {
        private double? _iva;
        private double? _subtotal;

        [XmlElement("Inpuesto")]
        public double? IVA
        {
            get
            {
                return _iva;
            }
            set
            {
                _iva = value;
                CalcularTotal();
            }
        }

        [XmlElement("Subtotal")]
        public double? Subtotal
        {
            get { return _subtotal; }
            set
            {
                _subtotal = value;
                CalcularTotal();
            }
        }

        [XmlElement("Total")]
        public double? Total { get; set; }


        private void CalcularTotal()
        {
            Total = (Subtotal.HasValue && IVA.HasValue) ? Math.Round((Subtotal + (Subtotal * (IVA / 100))).Value, 2) : null;
        }
    }
}
