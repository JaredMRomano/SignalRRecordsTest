using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SignalRRecordsTest.Requests
{
    public class ImmutableClassRequest
    {
        [JsonConstructor]
        public ImmutableClassRequest(int intProp, string stringProp)
        {
            IntProp = intProp;
            StringProp = stringProp;
        }

        [JsonInclude]
        public int IntProp { get; }

        [JsonInclude]

        public string StringProp { get; }
    }
}
