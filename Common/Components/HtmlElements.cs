using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebAppSimulador.Common.Components
{
    public class HtmlElements
    {
        public static List<SelectListItem> ListTypeDocuments = new List<SelectListItem>
        {
            new SelectListItem { Value = "CFDI", Text = "Factura Electrónica" },
            new SelectListItem { Value = "NC", Text = "Nota de Crédito Electrónica" },
            new SelectListItem { Value = "ND", Text = "Nota de Débito Electrónica" },
            new SelectListItem { Value = "Recibo", Text = "Comprobante de Pago" }
        };

        public static List<SelectListItem> ListRegimenFiscal = new List<SelectListItem>
        {
            new SelectListItem { Value = "601", Text = "General de Ley Personas Morales" },
            new SelectListItem { Value = "605", Text = "Incorporación Fisca" },
            new SelectListItem { Value = "606", Text = "Sueldos y Salarios" },
            new SelectListItem { Value = "608", Text = "Actividades Profesionales" },
            new SelectListItem { Value = "609", Text = "Arrendamiento" },
            new SelectListItem { Value = "610", Text = "Pequeños Contribuyentes" },
            new SelectListItem { Value = "620", Text = "Intermediación de Actividades" },
        };

        public static List<SelectListItem> ListCFDI = new List<SelectListItem>
        {
            new SelectListItem { Value = "G01", Text = "Gastos en general" },
            new SelectListItem { Value = "H1", Text = "Honorarios" },
            new SelectListItem { Value = "I01", Text = "Arrendamiento" },
            new SelectListItem { Value = "P01", Text = "Servicios profesionales" },
            new SelectListItem { Value = "C01", Text = "Construcción" },
            new SelectListItem { Value = "E01", Text = "Enajenación de acciones" },
            new SelectListItem { Value = "Nómina", Text = "Pago de nómina" },
            new SelectListItem { Value = "D01", Text = "Dividendos" },
            new SelectListItem { Value = "I02", Text = "Ingresos por intereses" },
            new SelectListItem { Value = "C02", Text = "Comisiones" },
            new SelectListItem { Value = "V01", Text = "Compra-venta de bienes" },
            new SelectListItem { Value = "T01", Text = "Traslado de mercancías" },
        };

        public static List<SelectListItem> ListMetodoPago = new List<SelectListItem>
        {
             new SelectListItem { Value = "01", Text = "Efectivo" },
             new SelectListItem { Value = "02", Text = "Cheque" },
             new SelectListItem { Value = "03", Text = "Transferencia electrónica de fondos" },
             new SelectListItem { Value = "04", Text = "Tarjeta de crédito" },
             new SelectListItem { Value = "05", Text = "Monedero electrónico" },
             new SelectListItem { Value = "06", Text = "Dinero electrónico" },
             new SelectListItem { Value = "08", Text = "Vales de despensa" },
             new SelectListItem { Value = "28", Text = "Tarjeta de débito" },
             new SelectListItem { Value = "29", Text = "Tarjeta de servicios" },
             new SelectListItem { Value = "99", Text = "Otros métodos de pago" }
        };

        public static List<SelectListItem> ListFormaDePago = new List<SelectListItem>
        {
            new SelectListItem { Value = "01", Text ="Pago en una sola exhibición"},
            new SelectListItem { Value = "02", Text ="Pago en parcialidades o diferido"},
            new SelectListItem { Value = "03", Text ="Pago con tarjeta de crédito"},
            new SelectListItem { Value = "04", Text ="Pago con tarjeta de débito"},
            new SelectListItem { Value = "05", Text ="Pago en transferencia electrónica de fondos"},
            new SelectListItem { Value = "06", Text ="Pago con cheque nominativo"},
            new SelectListItem { Value = "08", Text ="Pago en efectivo"},
            new SelectListItem { Value = "12", Text ="Pago con vales de despensa"},
            new SelectListItem { Value = "14", Text ="Pago por Internet"},
            new SelectListItem { Value = "13", Text ="Pago con tarjeta de servicios"},
            new SelectListItem { Value = "15", Text ="Pago con dinero electrónico"},
            new SelectListItem { Value = "17", Text ="Pago por teléfono móvil"},
            new SelectListItem { Value = "18", Text ="Pago con tarjeta de crédito emitida en el extranjero"},
            new SelectListItem { Value = "19", Text ="Pago con tarjeta de débito emitida en el extranjero"},
            new SelectListItem { Value = "26", Text ="Pago con crédito del importe"},
            new SelectListItem { Value = "23", Text ="Pago por subrogación"},
            new SelectListItem { Value = "24", Text ="Pago por consignación"},
            new SelectListItem { Value = "25", Text ="Pago por compensación"}
        };
    }
}
