using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace LabResultBackEnd.Controllers;

[ApiController]
[Route("[controller]")]
public class LabInterFaceController : ControllerBase
{
   
    private readonly ILogger<LabInterFaceController> _logger;
    private readonly IHttpClientFactory _httpClientFactory;


    public LabInterFaceController(ILogger<LabInterFaceController> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }


     // For loading Lab Procedures to the  Analyzer
    [HttpPost("GetLabProcedures")]
    public async Task<string> GetLabProcedures([FromBody] ProcedureListRequest procedureListRequest)
    {
        var _httpClient = _httpClientFactory.CreateClient();
        Dictionary<string, string> requestHeaders =
     new Dictionary<string, string>();
        foreach (var header in Request.Headers)
        {
            requestHeaders.Add(header.Key, header.Value);
        }



        var url = "http://localhost/ellider/api/labresultApi.svc/v1/getLabProcedures";
        var json = JsonConvert.SerializeObject(procedureListRequest);
        HttpContent inputContent = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = _httpClient.PostAsync(url, inputContent).Result;
        return await response.Content.ReadAsStringAsync();

    }

    // For updating Result from the analyzer
    [HttpPost("UpdateResult")]
    public async Task<string> UpdateResult([FromBody] UpdateResultListRequest updateResultListRequest)
    {
        var _httpClient = _httpClientFactory.CreateClient();
        var url = "http://localhost/ellider/api/labresultApi.svc/v1/updateResults";
        var json = JsonConvert.SerializeObject(updateResultListRequest);

        _httpClient.DefaultRequestHeaders.Add("AuthUser", "DMLAB");
        _httpClient.DefaultRequestHeaders.Add("AuthKey", "896D4504870F7596EFA815891F23D32A");


        HttpContent inputContent = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = _httpClient.PostAsync(url, inputContent).Result;
        return await response.Content.ReadAsStringAsync();

    }



     public class ProcedureListRequest
    {
        public string? sampleId { get; set; }
        public string? analyzerId { get; set; }
        public string? facilityId { get; set; }
    }


    public class Result
    {
        public string? testCode { get; set; }
        public int resultNumeric { get; set; }
        public string? resultCharacter { get; set; }
        public string? billSerial { get; set; }
        public string? billItemSerial { get; set; }
        public string? testInitiatedTime { get; set; }
        public string? testCompletedTime { get; set; }
        public string? remarks { get; set; }
    }

    public class UpdateResultListRequest
    {
        public string? facilityId { get; set; }
        public string? analyzerId { get; set; }
        public string? sampleId { get; set; }
        public string? patientId { get; set; }
        public List<Result>? results { get; set; }
    }






   
}
