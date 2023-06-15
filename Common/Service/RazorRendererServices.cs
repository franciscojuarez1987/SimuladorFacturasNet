using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebAppSimulador.Common.Service
{
    public class RazorRendererServices:IRazorRendererServices
    {
        private IRazorViewEngine _viewEngine;
        private ITempDataProvider _tempDataProvider;
        private IServiceProvider _serviceProvider;

        public RazorRendererServices(IRazorViewEngine viewEngine, ITempDataProvider tempDataProvider, IServiceProvider serviceProvider)
        {
            _viewEngine = viewEngine;
            _tempDataProvider = tempDataProvider;
            _serviceProvider = serviceProvider;
        }

        #region METODOS PRIVADOS
        private ActionContext GetActionContext()
        {
            var httpContext = new DefaultHttpContext
            {
                RequestServices = _serviceProvider
            };
            return new ActionContext(httpContext, new RouteData(), new ActionDescriptor());
        }

        private IView FindView(ActionContext actionContext, string partialName)
        {
            var getPartialResult = _viewEngine.GetView(null, partialName, false);

            if (getPartialResult.Success)
            {
                return getPartialResult.View;
            }

            var findPartialResult = _viewEngine.FindView(actionContext, partialName, false);

            if (findPartialResult.Success)
            {
                return findPartialResult.View;
            }

            var searchedLocations = getPartialResult.SearchedLocations.Concat(findPartialResult.SearchedLocations);

            var errorMessage = string.Join(Environment.NewLine, new[] { $"No se pudo encontrar la vista '{partialName}'. Se buscaron en la siguiente ruta:" }.Concat(searchedLocations));

            throw new InvalidOperationException(errorMessage);
        }
        #endregion

        public string RenderPartialToString<TModel>(string partialName, TModel model)
        {
            var actionContext = GetActionContext();
            var partial = FindView(actionContext, partialName);
            using (var output = new StringWriter())
            {
                var viewContext = new ViewContext(
                    actionContext,
                    partial,
                    new ViewDataDictionary<TModel>(
                        metadataProvider: new EmptyModelMetadataProvider(),
                        modelState: new ModelStateDictionary())
                    {
                        Model = model
                    },
                    new TempDataDictionary(
                        actionContext.HttpContext,
                        _tempDataProvider),
                    output,
                    new HtmlHelperOptions()
                );

                partial.RenderAsync(viewContext).ConfigureAwait(false);
                return output.ToString();
            }
        }
    }
}
