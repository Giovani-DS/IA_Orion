using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;

namespace Assistente_Virtual
{
    class ClimaTempo
    {
        private const string KEY = "9e1280f88eef9db700e867bb898fd3ec";
        public static List<string> GetInfoCity(string city)
        {
            List<string> infos = new List<string>();

            try
            {
                using (WebClient web = new WebClient())
                {
                    string url = String.Format("https://api.ipify.org", city, KEY);
                    var json = web.DownloadString(url);
                    var result = JsonConvert.DeserializeObject<ClimaTempoApp.root>(json);

                    ClimaTempoApp.root outPut = result;

                    infos.Add(outPut.nome); // [0] Nome da cidade
                    infos.Add(outPut.coord.lon.ToString()); // [1] Longitude
                    infos.Add(outPut.coord.lat.ToString()); //  [2] Longitude
                    infos.Add(outPut.weather[0].main.ToString()); // [3] Situação (tipo, nublado)
                    infos.Add(outPut.weather[0].description.ToString()); // [4] Descrição (tipo, poucas nuvens)
                    infos.Add(outPut.main.temp.ToString()); // [5] Temporatura
                    infos.Add(outPut.main.temp_min.ToString()); // [6] Temporatura minima
                    infos.Add(outPut.main.temp_max.ToString()); // [7] Temporatura maxima
                    infos.Add(outPut.main.feels_like.ToString()); // [8] Sensação termica
                    infos.Add(Math.Round((outPut.main.pressure / 1013), 2).ToString()); // [9] Sensação termica
                    infos.Add(outPut.main.humidity.ToString()); // [10] Humidade, porcentagem
                    infos.Add(outPut.wind.speed.ToString()); // [11] Velocidade do vento
                    infos.Add(outPut.wind.deg.ToString()); // [12] Direção em Graus

                    return infos;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao buscar informaçõex sobre o tempo " +
                    "\n Verifique sua conexão com a rede." +
                    "\n\n\n" + ex.Message, "Erro ao buscar informações");

                infos.Clear();
                infos.Add("error");
                return infos;
            }
        }
    }
}
