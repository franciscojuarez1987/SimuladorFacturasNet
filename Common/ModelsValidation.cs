using WebAppSimulador.Entities;

namespace WebAppSimulador.Common
{
    internal class ModelsValidation
    {
        /// <summary>
        /// Constructor de tipo ModelsValidation
        /// </summary>
        protected ModelsValidation() { }

        /// <summary>
        /// Predicado de tipo Productos
        /// </summary>
        internal static readonly Predicate<Productos>[] ValidateProduct =
        {
                (model)=>model.ClaveUnidad!.IsEmpty(),
                (model)=>model.ClaveProducto!.IsEmpty(),
                (model)=>model.Descripcion!.IsEmpty(),
                (model)=>model.Precio is not null,
                (model)=>model.Cantidad is not null
        };
        /// <summary>
        /// Valida Modelo Productos
        /// </summary>
        /// <param name="model">Productos</param>
        /// <param name="validations">Predicate<Productos>[]</param>
        /// <returns>True|False</returns>
        internal static bool ValidationInvoice<T>(T model, params Predicate<T>[] validations)
           => !validations!.ToList()!.Any(item => { return !item(model); });

        
    }
}
