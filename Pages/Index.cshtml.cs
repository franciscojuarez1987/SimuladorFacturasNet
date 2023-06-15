using System.Text;
using System.IO.Compression;
using WebAppSimulador.Common;
using WebAppSimulador.Entities;
using Microsoft.AspNetCore.Mvc;
using WebAppSimulador.Common.Service;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppSimulador.Common.Components;

namespace WebAppSimulador.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public bool IsLoading { get; set; } = false;

        [BindProperty]
        public PopertyInvoice InvoiceModel { get; set; } = new PopertyInvoice();


        private readonly ILogger<IndexModel> _logger;
        private readonly IRazorRendererServices _razorRendererServices;
        public IndexModel(ILogger<IndexModel> logger, IRazorRendererServices razorRendererPage)
        {
            _logger = logger;
            _razorRendererServices = razorRendererPage;

            InvoiceModel!.Elementos = new List<Productos>();

            if (InvoiceModel.Elementos.Count == 0)
            {
                InvoiceModel.Elementos.Add(new Productos
                {
                    Cantidad = null,
                    ClaveProducto = null,
                    ClaveUnidad = null,
                    Descripcion = null,
                    Precio = null,
                    Total = 0
                });
            }
        }

        public void OnGet()
        {

        }
        /// <summary>
        /// Agrega Producto Nuevo
        /// </summary>
        public IActionResult OnPostAddProduct()
        {
            InvoiceModel.Elementos.Add(new Productos
            {
                Descripcion = InvoiceModel.NuevoElemento.Descripcion,
                Cantidad = InvoiceModel.NuevoElemento.Cantidad,
                Precio = InvoiceModel.NuevoElemento.Precio,
                ClaveProducto = InvoiceModel.NuevoElemento.ClaveProducto,
                ClaveUnidad = InvoiceModel.NuevoElemento.ClaveUnidad,
                Total = InvoiceModel.NuevoElemento.Cantidad * InvoiceModel.NuevoElemento.Precio
            });
            return Page();
        }

        public IActionResult OnPostCreateInvoice()
        {
            // Si el modelo no es válido, se redirige a la página actual
            if (!ModelState.IsValid) return Page();

            string contentType = "application/zip";
            string xmlFileName = $"XML_{DateTime.Now.ToShortDateString()}.xml";
            string pdfFileName = $"PDF_{DateTime.Now.ToShortDateString()}.pdf";
            string downloadFileName = $"facturas_{DateTime.Now.ToShortDateString()}.zip";

            try
            {
                var modelo = GetInvoiceModel(InvoiceModel);

                //XML Factura
                var xmlBytes = GetFactura(modelo);

                //PDF
                var pdfBytes = GetPdf(modelo);

                // Lógica para generar el xml y pdf de la factura
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (ZipArchive zipArchive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                    {
                        for (int doc = 1; doc < 3; doc++)
                        {
                            ZipArchiveEntry entry = zipArchive.CreateEntry(doc == 1 ? xmlFileName : pdfFileName);
                            using (Stream entryStream = entry.Open())
                            {
                                if (doc == 1)
                                    entryStream.Write(xmlBytes, 0, xmlBytes.Length);
                                else if (doc == 2)
                                    entryStream.Write(pdfBytes, 0, pdfBytes.Length);
                            }
                        }
                    }

                    memoryStream.Seek(0, SeekOrigin.Begin);

                    return File(memoryStream.ToArray(), contentType, downloadFileName);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error WebAppSimulador.Pages.IndexModel.OnPost");
            }
            return Page();
        }
        /// <summary>
        /// Obtenemos el mapeo de entidades que se utilizaran para crear los diferentes documentos
        /// </summary>
        /// <param name="invoice">PopertyInvoice</param>
        /// <returns>Invoice</returns>
        private protected Invoice GetInvoiceModel(PopertyInvoice invoice)
        {
            var model = new Invoice();

            //Se eliminan registros  nulls
            invoice.Elementos.RemoveAll(item => item.ClaveUnidad == null &&
                                                item.ClaveProducto == null &&
                                                item.Descripcion == null &&
                                                item.Cantidad == null &&
                                                item.Precio == null &&
                                                item.Total == null);

            #region CLIENTE

            model.User.RFC = invoice.ClienteRFC;
            model.User.CorreoElectronico = invoice.ClienteCorreo;
            model.User.Nombre = $"{invoice.ClienteNombre} ${invoice.ClienteApellidos}";

            model.User.CFDITipo = invoice.TipoUsoCFDI;
            model.User.Direccion = invoice.ClienteDireccion;
            model.User.TipoFormaPago = invoice.TipoFormaPago;
            model.User.TipoMetodoPago = invoice.TipoMetodoPago;

            model.User.CFDI = HtmlElements.ListCFDI.Where(item => item.Value.Contains(invoice.TipoUsoCFDI)).FirstOrDefault()?.Text;
            model.User.MetodoPago = HtmlElements.ListMetodoPago.Where(item => item.Value.Contains(invoice.TipoMetodoPago)).FirstOrDefault()?.Text;
            model.User.FormaPago = HtmlElements.ListFormaDePago.Where(item => item.Value.Contains(invoice.TipoFormaPago)).FirstOrDefault()?.Text;

            #endregion


            #region COMPAÑIA

            model.Company.RFC = invoice.CompaniaRFC;
            model.Company.Direccion = invoice.Direccion;
            model.Company.NoFactura = invoice.NoFactura;
            model.Company.NombreCompania = invoice.Compania;
            model.Company.RazonSocial = invoice.RazonSocial;
            model.Company.FachaFactura = invoice.FachaFactura;
            model.Company.TipoDocumento = invoice.TipoDocumento;
            model.Company.LugarExpedicion = invoice.CpLugarExpedicion;
            model.Company.TipoRegimenFiscal = invoice.TipoRegimenFiscal;

            model.Company.Documento = HtmlElements.ListTypeDocuments.Where(item => item.Value.Contains(invoice.TipoDocumento)).FirstOrDefault()?.Text;
            model.Company.RegimenFiscal = HtmlElements.ListRegimenFiscal.Where(item => item.Value.Contains(invoice.TipoRegimenFiscal)).FirstOrDefault()?.Text;

            #endregion

            #region PRODUCTOS

            model.Compra.Products = invoice.Elementos.ToArray();

            #endregion

            #region DESGLOSE CONCEPTOS

            model.Conceptos.IVA = invoice.Inpuesto;
            model.Conceptos.Subtotal =Convert.ToDouble(invoice.Elementos.Sum(item => item.Total).Value.ToString("N2"));
            
            #endregion

            return model;
        }

        /// <summary>
        /// Obtenemos los bytes del xml generado 
        /// </summary>
        /// <param name="model">EntityFactura</param>
        /// <returns>Documento en byte[]</returns>
        private protected byte[] GetFactura<T>(T model) => Encoding.UTF8.GetBytes(model.XmlSerializeObject());
        /// <summary>
        /// Obtenemos los bytes del pdf generado
        /// </summary>
        /// <param name="model">EntityFactura</param>
        /// <returns>Documento en byte[]</returns>
        private protected byte[] GetPdf<T>(T model) => _razorRendererServices.RenderPartialToString("/Pages/Shared/TemplateFactura.cshtml", model).CreateToPDF();
    }
}