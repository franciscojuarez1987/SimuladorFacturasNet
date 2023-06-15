using Microsoft.AspNetCore.Mvc;
using WebAppSimulador.Entities;

namespace WebAppSimulador.Common.Components
{
    public class PopertyInvoice
    {

        [BindProperty]
        public string? Compania { get; set; }



        [BindProperty]
        public string? NoFactura { get; set; } = $"{ExtensionMethods.NoFactura()}";

        [BindProperty]
        public string? FachaFactura { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");



        [BindProperty]
        public string TipoRegimenFiscal { get; set; }
        [BindProperty]
        public string CpLugarExpedicion { get; set; }

        [BindProperty]
        public string RazonSocial { get; set; }
        [BindProperty]
        public string Direccion { get; set; }
        [BindProperty]
        public string CompaniaRFC { get; set; }

        [BindProperty]
        public string ClienteNombre { get; set; }
        [BindProperty]
        public string ClienteApellidos { get; set; }
        [BindProperty]
        public string ClienteCorreo { get; set; }
        [BindProperty]
        public string ClienteRFC { get; set; }
        [BindProperty]
        public string ClienteDireccion { get; set; }

        [BindProperty]
        public string TipoDocumento { get; set; }

        [BindProperty]
        public string TipoUsoCFDI { get; set; }

        [BindProperty]
        public string TipoMetodoPago { get; set; }
        [BindProperty]
        public string TipoFormaPago { get; set; }

        [BindProperty]
        public double? Inpuesto { get; set; }

        [BindProperty]
        public List<Productos> Elementos { get; set; }

        [BindProperty]
        public Productos NuevoElemento { get; set; } = new Productos();
    }
}
