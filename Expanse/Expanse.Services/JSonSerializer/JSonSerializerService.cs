#region Using namespaces..

using Expanse.Core.Services.JSonSerializer;
using Newtonsoft.Json;

#endregion


namespace Expanse.Services.JSonSerializer
{
    internal sealed class JSonSerializerService : IJSonSerializerService
    {
        #region Public Methods

        public string SerializeObject<TData>(TData data) => JsonConvert.SerializeObject(data);

        public TData DeserializeObject<TData>(string json) => JsonConvert.DeserializeObject<TData>(json);

        public dynamic DeserializeObject(string json) => JsonConvert.DeserializeObject(json);

        #endregion
    }
}