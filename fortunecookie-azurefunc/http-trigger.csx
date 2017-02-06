using System.Net; 

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log, ICollector<string> outputQueueItem)
{
    log.Info("C# HTTP trigger function processed a request.");

    dynamic data = await req.Content.ReadAsAsync<object>();
    
    string phone = data?.phone; 
    string email = data?.email;

    if(!string.IsNullOrEmpty(phone)){
        outputQueueItem.Add($"{{'type': 'phone', 'value':'{phone}'}}");
    }
    if(!string.IsNullOrEmpty(email)){
        outputQueueItem.Add($"{{'type': 'email', 'value':'{email}'}}");
    }

    return string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(email)
        ? req.CreateResponse(HttpStatusCode.BadRequest, "Es necesario pasar teléfono o email")
        : req.CreateResponse(HttpStatusCode.OK);
}