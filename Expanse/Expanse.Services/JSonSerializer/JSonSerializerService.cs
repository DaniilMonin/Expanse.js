#region Using namespaces..

using Expanse.Core.Services.JSonSerializer;
using Newtonsoft.Json;

#endregion


namespace Expanse.Services.JSonSerializer
{
    internal sealed class JSonSerializerService : IJSonSerializerService
    {
        #region Public Methods

        public string Serialize<TData>(TData data) => JsonConvert.SerializeObject(data);

        public TData DeSerialize<TData>(string json) => JsonConvert.DeserializeObject<TData>(json);

        #endregion

    }
}