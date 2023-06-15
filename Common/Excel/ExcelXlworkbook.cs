using System.Data;
using ClosedXML.Excel;

namespace WebAppSimulador.Common.Excel
{
    public class ExcelXlworkbook : IExcelXlworkbook
    {
        /// <summary>
        /// Metodo para crear un documento de excel a traves de un DataTable
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Stream GetStreamWorkbook(DataTable table)
        {
            try
            {
                Stream stream = new MemoryStream();
                using (var workbook = new XLWorkbook())
                {
                    workbook.Worksheets.Add(table);
                    workbook.SaveAs(stream);
                }
                stream.Position = 0;
                return stream;
            }
            catch (Exception ex)
            {
                throw new Exception("Error WebAppSimulador.Common.Excel-GetStreamWorkbook", ex);
            }
        }

        /// <summary>
        /// Metodo para crear un documento de excel a traves de un DataTable
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Stream GetStreamWorkbook(DataSet table)
        {
            try
            {
                Stream stream = new MemoryStream();
                using (var workbook = new XLWorkbook())
                {
                    workbook.Worksheets.Add(table);
                    workbook.SaveAs(stream);
                }
                stream.Position = 0;
                return stream;
            }
            catch (Exception ex)
            {
                throw new Exception("Error WebAppSimulador.Common.Excel-GetStreamWorkbook", ex);
            }
        }
    }
}