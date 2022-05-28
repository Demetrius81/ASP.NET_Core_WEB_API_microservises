using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsManager.Client
{

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "13.16.0.0 (NJsonSchema v10.7.1.0 (Newtonsoft.Json v11.0.0.0))")]
    public partial class AgentInfo
    {
        [Newtonsoft.Json.JsonProperty("agentId", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int AgentId { get; set; }

        [Newtonsoft.Json.JsonProperty("agentAddress", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Uri AgentAddress { get; set; }

        [Newtonsoft.Json.JsonProperty("enable", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool Enable { get; set; }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "13.16.0.0 (NJsonSchema v10.7.1.0 (Newtonsoft.Json v11.0.0.0))")]
    public partial class CpuAllMetricsResponse
    {
        [Newtonsoft.Json.JsonProperty("metrics", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.Generic.ICollection<CpuMetric> Metrics { get; set; }

        [Newtonsoft.Json.JsonProperty("agentID", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int AgentID { get; set; }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "13.16.0.0 (NJsonSchema v10.7.1.0 (Newtonsoft.Json v11.0.0.0))")]
    public partial class CpuMetric
    {
        [Newtonsoft.Json.JsonProperty("id", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int Id { get; set; }

        [Newtonsoft.Json.JsonProperty("value", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int Value { get; set; }

        [Newtonsoft.Json.JsonProperty("time", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Time { get; set; }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "13.16.0.0 (NJsonSchema v10.7.1.0 (Newtonsoft.Json v11.0.0.0))")]
    public partial class CpuMetricCreateRequest
    {
        [Newtonsoft.Json.JsonProperty("agentId", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int AgentId { get; set; }

        [Newtonsoft.Json.JsonProperty("fromTime", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string FromTime { get; set; }

        [Newtonsoft.Json.JsonProperty("toTime", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string ToTime { get; set; }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "13.16.0.0 (NJsonSchema v10.7.1.0 (Newtonsoft.Json v11.0.0.0))")]
    public partial class DotNetAllMetricsResponse
    {
        [Newtonsoft.Json.JsonProperty("metrics", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.Generic.ICollection<DotNetMetric> Metrics { get; set; }

        [Newtonsoft.Json.JsonProperty("agentID", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int AgentID { get; set; }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "13.16.0.0 (NJsonSchema v10.7.1.0 (Newtonsoft.Json v11.0.0.0))")]
    public partial class DotNetMetric
    {
        [Newtonsoft.Json.JsonProperty("id", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int Id { get; set; }

        [Newtonsoft.Json.JsonProperty("value", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int Value { get; set; }

        [Newtonsoft.Json.JsonProperty("time", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Time { get; set; }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "13.16.0.0 (NJsonSchema v10.7.1.0 (Newtonsoft.Json v11.0.0.0))")]
    public partial class DotNetMetricCreateRequest
    {
        [Newtonsoft.Json.JsonProperty("agentId", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int AgentId { get; set; }

        [Newtonsoft.Json.JsonProperty("fromTime", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string FromTime { get; set; }

        [Newtonsoft.Json.JsonProperty("toTime", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string ToTime { get; set; }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "13.16.0.0 (NJsonSchema v10.7.1.0 (Newtonsoft.Json v11.0.0.0))")]
    public partial class HddAllMetricsResponse
    {
        [Newtonsoft.Json.JsonProperty("metrics", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.Generic.ICollection<HddMetric> Metrics { get; set; }

        [Newtonsoft.Json.JsonProperty("agentID", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int AgentID { get; set; }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "13.16.0.0 (NJsonSchema v10.7.1.0 (Newtonsoft.Json v11.0.0.0))")]
    public partial class HddMetric
    {
        [Newtonsoft.Json.JsonProperty("id", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int Id { get; set; }

        [Newtonsoft.Json.JsonProperty("value", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int Value { get; set; }

        [Newtonsoft.Json.JsonProperty("time", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Time { get; set; }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "13.16.0.0 (NJsonSchema v10.7.1.0 (Newtonsoft.Json v11.0.0.0))")]
    public partial class HddMetricCreateRequest
    {
        [Newtonsoft.Json.JsonProperty("agentId", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int AgentId { get; set; }

        [Newtonsoft.Json.JsonProperty("fromTime", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string FromTime { get; set; }

        [Newtonsoft.Json.JsonProperty("toTime", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string ToTime { get; set; }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "13.16.0.0 (NJsonSchema v10.7.1.0 (Newtonsoft.Json v11.0.0.0))")]
    public partial class NetworkAllMetricsResponse
    {
        [Newtonsoft.Json.JsonProperty("metrics", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.Generic.ICollection<NetworkMetric> Metrics { get; set; }

        [Newtonsoft.Json.JsonProperty("agentID", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int AgentID { get; set; }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "13.16.0.0 (NJsonSchema v10.7.1.0 (Newtonsoft.Json v11.0.0.0))")]
    public partial class NetworkMetric
    {
        [Newtonsoft.Json.JsonProperty("id", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int Id { get; set; }

        [Newtonsoft.Json.JsonProperty("value", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int Value { get; set; }

        [Newtonsoft.Json.JsonProperty("time", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Time { get; set; }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "13.16.0.0 (NJsonSchema v10.7.1.0 (Newtonsoft.Json v11.0.0.0))")]
    public partial class NetworkMetricCreateRequest
    {
        [Newtonsoft.Json.JsonProperty("agentId", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int AgentId { get; set; }

        [Newtonsoft.Json.JsonProperty("fromTime", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string FromTime { get; set; }

        [Newtonsoft.Json.JsonProperty("toTime", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string ToTime { get; set; }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "13.16.0.0 (NJsonSchema v10.7.1.0 (Newtonsoft.Json v11.0.0.0))")]
    public partial class RamAllMetricsResponse
    {
        [Newtonsoft.Json.JsonProperty("metrics", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.Generic.ICollection<RamMetric> Metrics { get; set; }

        [Newtonsoft.Json.JsonProperty("agentID", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int AgentID { get; set; }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "13.16.0.0 (NJsonSchema v10.7.1.0 (Newtonsoft.Json v11.0.0.0))")]
    public partial class RamMetric
    {
        [Newtonsoft.Json.JsonProperty("id", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int Id { get; set; }

        [Newtonsoft.Json.JsonProperty("value", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int Value { get; set; }

        [Newtonsoft.Json.JsonProperty("time", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Time { get; set; }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "13.16.0.0 (NJsonSchema v10.7.1.0 (Newtonsoft.Json v11.0.0.0))")]
    public partial class RamMetricCreateRequest
    {
        [Newtonsoft.Json.JsonProperty("agentId", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int AgentId { get; set; }

        [Newtonsoft.Json.JsonProperty("fromTime", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string FromTime { get; set; }

        [Newtonsoft.Json.JsonProperty("toTime", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string ToTime { get; set; }

    }



    [System.CodeDom.Compiler.GeneratedCode("NSwag", "13.16.0.0 (NJsonSchema v10.7.1.0 (Newtonsoft.Json v11.0.0.0))")]
    public partial class ApiException : System.Exception
    {
        public int StatusCode { get; private set; }

        public string Response { get; private set; }

        public System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> Headers { get; private set; }

        public ApiException(string message, int statusCode, string response, System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> headers, System.Exception innerException)
            : base(message + "\n\nStatus: " + statusCode + "\nResponse: \n" + ((response == null) ? "(null)" : response.Substring(0, response.Length >= 512 ? 512 : response.Length)), innerException)
        {
            StatusCode = statusCode;
            Response = response;
            Headers = headers;
        }

        public override string ToString()
        {
            return string.Format("HTTP Response: \n\n{0}\n\n{1}", Response, base.ToString());
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("NSwag", "13.16.0.0 (NJsonSchema v10.7.1.0 (Newtonsoft.Json v11.0.0.0))")]
    public partial class ApiException<TResult> : ApiException
    {
        public TResult Result { get; private set; }

        public ApiException(string message, int statusCode, string response, System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> headers, TResult result, System.Exception innerException)
            : base(message, statusCode, response, headers, innerException)
        {
            Result = result;
        }
    }

}
