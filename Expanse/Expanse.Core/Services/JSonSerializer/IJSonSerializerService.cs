namespace Expanse.Core.Services.JSonSerializer
{
    public interface IJSonSerializerService
    {
        string SerializeObject<TData>(TData data);

        TData DeserializeObject<TData>(string json);

        dynamic DeserializeObject(string json);
    }
}