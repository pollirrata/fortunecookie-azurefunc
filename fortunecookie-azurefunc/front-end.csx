using System.Configuration;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

public static HttpResponseMessage Run(HttpRequestMessage req, TraceWriter log)
{
    log.Info("C# HTTP trigger function processed a request."); 

    HttpResponseMessage m = req.CreateResponse(HttpStatusCode.OK);

    StringBuilder sb = new StringBuilder();

    string httptriggerurl = ConfigurationManager.AppSettings["httptriggerurl"];

    sb.AppendLine("<!DOCTYPE html>");
    sb.AppendLine("<html>");
    sb.AppendLine("<head>");
    sb.AppendLine("  <title>Galleta de la suerte</title>");
    sb.AppendLine("  <meta charset=\"utf-8\" />");
    sb.Append("   <script src=\"https://code.jquery.com/jquery-3.1.1.min.js\"");
    sb.Append(" integrity=\"sha256-hVVnYaiADRTO2PzUGmuLJr8BLUSjGIZsDYGmIJLv2b8=\"");
    sb.AppendLine(" crossorigin=\"anonymous\"></script>");
    sb.AppendLine("</head>");
    sb.AppendLine("<body>");
    sb.AppendLine("    Recibe tu fortuna");
    sb.AppendLine("    <form>"); 
    sb.AppendLine("        <label for='phone'>Phone</label>");
    sb.Append("        <input type='tel' name='phone' id='phone'");
    sb.AppendLine(" required='required' maxlength='10' pattern='[0-9]{10}' />");
    sb.AppendLine("        <label for='phone'>E-mail</label>");
    sb.Append("        <input type='email' name='email' id='email'");
    sb.AppendLine(" required='required'  />");
    sb.AppendLine("        <input type='submit' value='Submit' />");
    sb.AppendLine("    </form>");
    sb.AppendLine("    <script>");
    sb.AppendLine("        $(document).ready(function () {");
    sb.AppendLine("            $('form').submit(function (event) {");
    sb.AppendLine("                alert('Recibirás tu fortuna en un momento')");
    sb.Append($"                $.ajax({{ type:'POST', url:'{httptriggerurl}', "); 
    sb.Append("data: JSON.stringify({ 'phone': $('#phone').val(), 'email' : $('#email').val() }), ");
    sb.AppendLine("contentType: 'application/json'});");
    sb.AppendLine("                event.preventDefault();");
    sb.AppendLine("            });");
    sb.AppendLine("        });");
    sb.AppendLine("    </script>");
    sb.AppendLine("</body>");
    sb.AppendLine("</html>");

    m.Content = new StringContent(sb.ToString(), Encoding.UTF8, "text/html");

    return m;
}