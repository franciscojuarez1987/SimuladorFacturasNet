namespace WebAppSimulador.Common.Service
{
    public interface IRazorRendererServices
    {
        string RenderPartialToString<TModel>(string partialView, TModel model);
    }
}
