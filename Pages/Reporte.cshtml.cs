using System.Text.Json;
using WebAppSimulador.Common;
using WebAppSimulador.Entities;
using Microsoft.AspNetCore.Mvc;
using WebAppSimulador.Common.Excel;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace WebAppSimulador.Pages
{
    public class ReporteModel : PageModel
    {
        [BindProperty]
        public List<Invoice>? EntityList { get; set; }

        [BindProperty]
        public List<IFormFile>? XmlFiles { get; set; }

        private readonly ILogger<ReporteModel> _logger;
        private readonly IExcelXlworkbook _excelXlworkbook;
        public ReporteModel(ILogger<ReporteModel> logger)
        {
            _logger = logger;
            _excelXlworkbook = new ExcelXlworkbook();
        }
        /// <summary>
        /// Recuperar la lista de entidades desde la Session y se guarda en List<Invoice>
        /// </summary>
        public void OnGet()
        {
            // Recuperar la lista de entidades desde la Session
            var serializedEntityList = HttpContext.Session.GetString("ListaFacturas");
            if (!string.IsNullOrEmpty(serializedEntityList))
                EntityList = JsonSerializer.Deserialize<List<Invoice>>(serializedEntityList);
        }
        /// <summary>
        /// Acción que se encargará en extraer el contenido del xml y Crear la transformación en un objeto de tipo EntityFactura
        /// </summary>
        /// <returns>ViewPage</returns>
        public IActionResult OnPost()
        {
            HttpContext.Session.SetString("ListaFacturas", string.Empty);
            if (XmlFiles != null && XmlFiles.Count > 0)
            {
                List<Invoice> listaFacturas = new();

                foreach (var file in XmlFiles)
                {
                    if (file.Length > 0)
                    {
                        // Leer el contenido del archivo XML como una cadena de texto
                        using (var reader = new StreamReader(file.OpenReadStream()))
                        {
                            //Obtenemos el contenido en un string
                            var xmlContent = reader.ReadToEnd();

                            //Deserializa la cadena en la entidad facturas
                            var factura = xmlContent is not null ? xmlContent.XmlDeserializeObject<Invoice>() : null;

                            //se valida que la factura no este en null
                            if (factura is not null)
                                //Se agrega a la lista de facturas
                                listaFacturas.Add(factura);
                        }

                    }
                }

                // Serializar la lista de entidades en formato JSON
                var modelo = JsonSerializer.Serialize(listaFacturas);

                // Almacena la lista en la session
                HttpContext.Session.SetString("ListaFacturas", modelo);
            }
            // Redireccionar a la misma página
            return RedirectToPage();
        }

        /// <summary>
        /// Accion que se utiliza para generar libro de Excel
        /// </summary>
        /// <returns>File | Page</returns>
        public IActionResult OnPostGeneraReporte()
        {
            // Recuperar la lista de entidades desde la sesión
            var serializedEntityList = HttpContext.Session.GetString("ListaFacturas");
            if (!string.IsNullOrEmpty(serializedEntityList))
            {
                try
                {
                    List<Productos> ListProducts = new();
                    DataSet dataSet = new DataSet("ReporteFacturas");

                    //Nombre del archivo
                    string fileName = $"Reporte_Facturas_{DateTime.Now.ToShortDateString()}.xlsx";

                    //Transofomar la sesión en una lista de entidades
                    var model = JsonSerializer.Deserialize<List<Invoice>>(serializedEntityList);
                    
                    if (model!.Any())
                    {
                        //Transforma cada entidad en un Objeto DataTable
                        var dtReceptor = model!.Select(receptor => receptor.User).ToList()!.ToDataTable("RECEPTOR");
                        var dtEmisor = model!.Select(emisor => emisor.Company).ToList()!.ToDataTable("EMISOR");
                        var dtConceptos = model!.Select(emisor => emisor.Conceptos).ToList()!.ToDataTable("DESGLOCE");
                       
                        //Agregamos cada arreglo en una lista de tipo producto
                        model!.Select(producto => producto.Compra.Products).ToList().ForEach(producto =>
                        {
                            ListProducts.AddRange(producto.ToList());
                        });

                        var dtCompra = ListProducts.ToDataTable("COMPRAS");

                        //Almacenamos los DataTables en un DataSet que serian las hojas del libro
                        dataSet.Tables.Add(dtReceptor);
                        dataSet.Tables.Add(dtEmisor);
                        dataSet.Tables.Add(dtCompra);
                        dataSet.Tables.Add(dtConceptos);
                    }

                    // Obtener un flujo de memoria (MemoryStream) que contiene el libro de Excel generado a partir de la tabla (_table).
                    // El método GetStreamWorkbook es invocado en el objeto _excelXlworkbook, que representa un generador de archivos de Excel.
                    // El resultado se almacena en la variable 'response' como un MemoryStream.
                    //var response = (MemoryStream)_excelXlworkbook.GetStreamWorkbook(_table);
                    var response = (MemoryStream)_excelXlworkbook.GetStreamWorkbook(dataSet);

                    //Descarga el libro de Excel
                    return File(response.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                }
                catch (Exception ex)
                {

                }
            }
            return RedirectToPage();
        }
    }
}