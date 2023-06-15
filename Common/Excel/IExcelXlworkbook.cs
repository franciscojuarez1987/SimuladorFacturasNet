using System.Data;

namespace WebAppSimulador.Common.Excel
{
    public interface IExcelXlworkbook
    {
        /// <summary>
        /// Crea un documento de excel a traves de un DataTable
        /// </summary>
        /// <param name="table">DataTable</param>
        /// <returns>Stream</returns>
        Stream GetStreamWorkbook(DataTable table);
        /// <summary>
        /// Crea un documento de excel a traves de un DataTable
        /// </summary>
        /// <param name="table">DataTable</param>
        /// <returns>Stream</returns>
        Stream GetStreamWorkbook(DataSet table);
    }
}
