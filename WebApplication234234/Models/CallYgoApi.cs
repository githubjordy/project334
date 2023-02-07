


using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Dynamic;
using System.Net.Http;
using System.Security.Principal;

namespace WebApplication234234.Models
{
    public class CallYgoApi
    {

        public static async Task GetYgoData()
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(" https://db.ygoprodeck.com/api/v7/cardinfo.php"))
                {
                    
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    var cardinfo = JsonConvert.DeserializeObject<ExpandoObject>(apiResponse, new ExpandoObjectConverter());
                    var firstobject = cardinfo?.First();
                    var cardarray = firstobject.Value.Value;
                    var cardcast = (List<dynamic>)cardarray;
                   
                    var CardNameAndId = cardcast.Select(x => new { x.id, x.name });

            
                    var jsontext = JsonConvert.SerializeObject(CardNameAndId, Formatting.Indented);
                   
                   

                    File.WriteAllText(@"C:\duelingnexusdeckcreatorygo\WebApplication234234\WebApplication234234\wwwroot\textfilesforcards\ygocards.json", jsontext);
                   
                   // reservationList = JsonConvert.DeserializeObject<List<Reservation>>(apiResponse);
                }
            }
           
        }



    }
}
