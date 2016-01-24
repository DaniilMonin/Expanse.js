namespace Expanse.Core.Services.Templates
{
    public interface ITemplatesService
    {
        string Compile(string fileName, dynamic model = null);
    }
}