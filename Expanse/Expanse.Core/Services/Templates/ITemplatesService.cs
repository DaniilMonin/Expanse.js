namespace Expanse.Core.Services.Templates
{
    public interface ITemplatesService
    {
        string Compile<TModel>(string fileName, TModel model = null)
            where TModel : class;
    }
}