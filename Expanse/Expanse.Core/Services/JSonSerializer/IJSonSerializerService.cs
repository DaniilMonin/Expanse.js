namespace Expanse.Core.Services.JSonSerializer
{
    public interface IJSonSerializerService
    {
        string Serialize<TData>(TData data);

        TData DeSerialize<TData>(string json);
    }
}