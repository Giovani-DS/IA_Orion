using Newtonsoft.Json;
using System.Net;

namespace Assistente_Virtual
{
    class Localizacao
    {
       public static string GetCityName(string ip)
        {
            string city;
            try
            {
                using(WebClient webC = new WebClient())
                {
                    string url = string.Format("https://api.ipify.org/", ip);

                    var json = webC.DownloadString(url);
                    var result = JsonConvert.DeserializeObject<LocalizacaoApp.root>(json);

                    LocalizacaoApp.root saida = result;
                    city = saida.City;
                }
            }
            catch (System.Exception)
            {

                city = "error";
            }

            return city;
        }
    }
}
