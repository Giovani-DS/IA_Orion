using System.Collections.Generic;

namespace Assistente_Virtual.Gramaticas
{
    class Gramatica
    {
        public static List<string> WhatHour = new List<string>()
        {
            "Orion Que horas são",
            "Que horas são",
            "Me diga o horario",
            "Qual sao as horas"
        };

        public static List<string> WhatDay = new List<string>()
        {
            "Que dia é hoje",
            "Qual dia é hoje",
            "Orion que dia é hoje",
            "Orion qual dia é hoje"
        };


        public static List<string> StopListening = new List<string>()
        {
            "Orion silencio",
            "Orion ativar modo silencioso",
            "Orion cala a boca"
        };

        public static List<string> ReturnListening = new List<string>()
        {
            "Assistente",
            "Orion",
            "Orion silencio",
            "Orion desativar modo silencioso",
            "Orion volte"
        };

        public static List<string> StopProgram = new List<string>()
        {
            "Orion fechar programa",
            "Orion parar programa",
            "Orion tchau"
        };

        public static List<string> Temperatura = new List<string>()
        {
            "Orion qual é a temperatura",
            "Orion qual é a temperatura de hoje",
            "Qual é a temperatura de hoje"
        };

        public static List<string> MoreInfos = new List<string>()
        {
            "Me de informações da cidade",
            "Informações da cidade"
        };

        public static List <string> Pergunta_Maq = new List<string>()
        {
             "Qual a minha profissão",
             "Orion qual a minha profissão",
             "Orion não sei o que escolher como profissão"//ass Giovani
        };

        public static List <string> resposta_1 = new List<string>() //ass Giovani
        {
            "Animais",
            "Natureza",
            "Cachorro",
            "Gato"
            
        };
        public static List <string> resultadoRes_1 = new List<string>()//animais
        {
            "Cuidaria",
            "Sim",
            "Concerteza",
            "Claro"
        };
        public static List <string> resultadoRes_1No = new List<string>()//animais
        {
            "Não",
            "Não gostaria",
            "Melhor não"
        };

        public static List <string> resposta_2 = new List<string>()//tecnologia
        {
            "Tecnologia",
            "Computador",
            "Programar"
        };
        public static List<string> respostaTec_2Games = new List<string>()// tecnologia = games
        {
            "Games",
            "Jogos",
            "Jogar"
        };
        public static List<string> respostaTec_2Web = new List<string>()// tecnologia = website
        {
            "Web",
            "Websites",
            "Blog",
            "Site"
        };
        public static List<string> respostaTec_2dados = new List<string>()//Tecnologia = dados
        {
            "Dados",
            "Banco de dados",
            "Informações"
        };
        public static List<string> respostaTec_2Animacoes = new List<string>()//Tecnologia = animações
        {
            "Animar",
            "Animações",
            "Movimentos"
        };

        public static List <string> resposta_3 = new List<string>()
        {
            "Ler",
            "Leitura",
            "Livros"
        };

        public static List<string> resposta_4 = new List<string>()
        {
            "Cantar",
            "Musica",
            "Som"
            
        };

        public static List<string> piada1 = new List<string>()
        {
            "Orion piada",
            "Orion conta uma piada"
        };
        public static List<string> respostaPiadaSoli = new List<string>()
        {
            "Não sei",
            "Não faço ideia"
        };
        public static List<string> criação = new List<string>()
        {
            "Orion como você foi construido",
            "Orion, origem"
        };
        public static List<string> motivo = new List<string>()
        {
            "Orion porque você foi criado",
            "porque você existe"
        };
        public static List<string> agradecimentoFinal = new List<string>()
        {
            "Agradecimento",
            "Final"
        };
    }
}
