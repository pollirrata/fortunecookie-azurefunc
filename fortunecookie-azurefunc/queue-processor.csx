#r "Twilio.Api"
#r "Newtonsoft.Json"
#r "SendGrid"

using System;
using System.Configuration;
using Newtonsoft.Json;
using SendGrid.Helpers.Mail;
using Twilio; 


public static void Run(string myQueueItem, TraceWriter log, ICollector<SMSMessage> message, ICollector<Mail> email)
{
    log.Info($"C# Queue trigger function processed: {myQueueItem}");

    FortuneCookieEngine f = new FortuneCookieEngine();
    string fortune = f.Fortunes[new Random().Next(0, f.Fortunes.Length - 1)];

    dynamic queueItem = JsonConvert.DeserializeObject(myQueueItem);
    string to = queueItem?.value;

    if(queueItem?.type == "phone"){
        log.Info($"Sending SMS to {to}");
        SMSMessage sms = new SMSMessage();
        sms.Body = fortune;
        sms.To = $"+52{to}";
        sms.From = ConfigurationManager.AppSettings["TwilioFrom"];
        message.Add(sms);
    }

    if(queueItem?.type == "email"){ 
        log.Info($"Sending Email to {to}");
        Mail mail = new Mail();
        Personalization personalization = new Personalization();
        personalization.AddTo(new Email(to));
        mail.AddPersonalization(personalization);
        mail.AddContent(new Content("text/plain", fortune));
        email.Add(mail);
    }

}

public class FortuneCookieEngine
{
    public string[] Fortunes;
    public FortuneCookieEngine()
    {
        Fortunes = new[]{ 
            "Eso no era pollo",
            "Este mensaje se auto-destruirá en cinco segundos",
            "Todo lo que sabes es una mentira",
            "Ignora el mensaje de la siguiente galleta de la suerte",
            "El fin se acerca, y es TU culpa",
            "!Auxilio, soy rehen de una panadería de galletas de la fortuna!",
            "La suerte que buscas está en otra galleta",
            "Boleto no premiado !sigue participando!",
            "Garantía inválida una vez abierto el empaque",
            "Coloca pierna derecha sobre el rojo",
            "Vote por el PAN/PRI/PRD/Partido favorito",
            "CocaCola, siempre en los grandes eventos",
            "!Lotería!",
            "Sé lo que hiciste el almuerzo pasado",
            "Agítese antes de usar",
            "Te dará hambre otra vez en una hora",
            "No se deje al alcance de los niños",
            "Cuidado con la comida China hoy, puede que te enfermes",
            "Espacio en renta",
            "404 fortuna no encontrada",
            "Si puedes leer esto, no necesitas lentes",
            "Rómpase en caso de incendio",
            "Este mensaje miente",
            "!Ella lo sabe todo!",
            "Baterías no incluidas",
            "Hagas lo que hagas no te comas esta gall... oh, demasiado tarde",
            "Para continuar, inserte una moneda",
            "Deberás llevar el Halcón de Jade al aeropuerto a media noche",
            "La verdad está allá afuera",
            "Sigue al conejo blanco",
            "No confíes en nadie",
            "Si yo fuera tú, dejaría de leer ahora mismo",
            "Lo sentimos, su fortuna ha sido rechazada, por favor vuelva a intentar",
            "Si estás leyendo este mensaje, ya es demasiado tarde para mí...",
            "La policía está en camino !huye!",
        };
    }
}