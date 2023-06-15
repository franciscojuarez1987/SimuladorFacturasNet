using SelectPdf;
using System.Xml;
using System.Text;
using System.Data;
using System.Reflection;
using System.Xml.Serialization;

namespace WebAppSimulador.Common
{
    /// <summary>
    /// Clase que se usa como una extencion de metodos
    /// </summary>
    public static class ExtensionMethods
    {
        

        /// <summary>
        /// Deserialisa un string en un tipo T
        /// </summary>
        /// <typeparam name="T">T: Modelo</typeparam>
        /// <param name="xmlData">string: xml</param>
        /// <returns>T?</returns>
        public static T? XmlDeserializeObject<T>(this string xmlData)
        {
            XmlSerializer xmlModel = new(typeof(T));
            using StringReader sr = new(xmlData);
            T? model = (T?)xmlModel.Deserialize(sr);
            sr.Close();
            return model;
        }
        /// <summary>
        /// Transforma un tipo T en formato xml
        /// </summary>
        /// <typeparam name="T">T: Modelo</typeparam>
        /// <param name="model">T: Model</param>
        /// <returns>string</returns>
        public static string XmlSerializeObject<T>(this T model)
        {
            XmlSerializer xml = new(typeof(T));
            using MemoryStream ms = new();
            using var xw = XmlWriter.Create(ms);
            xml.Serialize(xw, model);
            byte[] utf8 = ms.ToArray();
            string xmlData = Encoding.UTF8.GetString(utf8);
            return xmlData.Replace("<?xml version=\"1.0\" encoding=\"utf-8\"?>", "");
        }

        /// <summary>
        /// Crea una Lista en un DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lista"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(this List<T> lista,string NameTable)
        {
            DataTable dataTable = new DataTable(NameTable);

            // Obtener el tipo de objeto de la lista
            Type tipoObjeto = typeof(T);

            // Obtener todas las propiedades públicas del tipo de objeto
            PropertyInfo[] propiedades = tipoObjeto.GetProperties();

            // Agregar las columnas al DataTable basado en las propiedades del objeto
            foreach (PropertyInfo propiedad in propiedades)
            {
                dataTable.Columns.Add(propiedad.Name, Nullable.GetUnderlyingType(propiedad.PropertyType) ?? propiedad.PropertyType);
            }

            // Agregar las filas al DataTable basado en los valores de las propiedades en cada objeto de la lista
            lista.ForEach(objeto => {
                DataRow fila = dataTable.NewRow();
                propiedades.ToList().ForEach(propiedad =>
                {
                    fila[propiedad.Name] = propiedad.GetValue(objeto) ?? DBNull.Value;
                });
                dataTable.Rows.Add(fila);
            });
            return dataTable;
        }
        /// <summary>
        /// Crea pdf en base al contenido html
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static byte[] CreateToPDF(this string html)
        {
            HtmlToPdf htmlToPdf = new HtmlToPdf();
            var pdf = htmlToPdf.ConvertHtmlString(html);

            byte[] pdfBytes = pdf.Save();

            pdf.Close();

            //Crea el pdf en el scritorio
            //var testFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "test.pdf");
            //System.IO.File.WriteAllBytes(testFile, pdfBytes);

            return pdfBytes;
        }

        public static long NoFactura() => new Random().NextInt64(10000000000000, 99999999999999);

        /// <summary>
        /// Verifica si una cadena no esta en: nul, vacío, espacio en blanco
        /// </summary>
        /// <param name="input">string</param>
        /// <returns>True|False</returns>
        public static bool IsEmpty(this string input) => !(string.IsNullOrEmpty(input) && string.IsNullOrWhiteSpace(input)!);
    }
}
