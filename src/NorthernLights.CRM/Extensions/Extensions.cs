using Newtonsoft.Json;
using NorthernLights.CRM.Models;
using System.Collections.Generic;

namespace NorthernLights.CRM.Extensions
{
    public static partial class Extensions
    {
        public static void AppendActivity(this Project record, ActivityViewModel model)
        {
            var data = new List<dynamic>();
            if (!string.IsNullOrWhiteSpace(record.Activities))
            {
                data.AddRange(JsonConvert.DeserializeObject<IEnumerable<dynamic>>(record.Activities));
            }

            data.Add(model);
            record.Activities = JsonConvert.SerializeObject(data);
        }

        public static void AppendActivity(this ToDo record, ActivityViewModel model)
        {
            var data = new List<dynamic>();
            if (!string.IsNullOrWhiteSpace(record.Activities))
            {
                data.AddRange(JsonConvert.DeserializeObject<IEnumerable<dynamic>>(record.Activities));
            }

            data.Add(model);
            record.Activities = JsonConvert.SerializeObject(data);
        }
    }
}