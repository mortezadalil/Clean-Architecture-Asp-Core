using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cms.Api.Presenter
{
  public sealed class JsonContentResult : ContentResult
  {
    public JsonContentResult()
    {
      ContentType = "application/json";

    }

    public string Serialize(object data)
    {
      return JsonConvert.SerializeObject(data,
          Newtonsoft.Json.Formatting.None, new JsonSerializerSettings
          {
            NullValueHandling = NullValueHandling.Include,
            ContractResolver = new CamelCasePropertyNamesContractResolver()
          });
    }
  }
}
