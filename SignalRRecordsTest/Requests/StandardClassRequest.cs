using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SignalRRecordsTest.Requests
{
    public class StandardClassRequest
    {
        public StandardClassRequest()
        {

        }
        public StandardClassRequest(int intProp, string stringProp)
        {
            IntProp = intProp;
            StringProp = stringProp;
        }

        [JsonInclude]
        public int IntProp { get; private set; }

        [JsonInclude]

        public string StringProp { get; private set; }
    }
}
