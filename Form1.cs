using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Assistente_Virtual;
using Assistente_Virtual.Gramaticas;
using Microsoft.Speech.Recognition;

namespace Assistente_Virtual___Orion
{
    public partial class Form1 : Form
    {
        private SpeechRecognitionEngine engine;
        private CultureInfo ci;
        private bool Ouvindo = true;
        public static string Nome_Assistente = "Orion";
        //private string city;

        public Form1()
        {
            InitializeComponent();
            Init();
        }

        private void Init()

        {
            try
            {
                ci = new CultureInfo("pt-BR");
                engine = new SpeechRecognitionEngine(ci);
                saidaSom.Speak("Carregando");
                //city = Localizacao.GetCityName(Executer.GetIp()).ToLower();
                SpeechRec();
                saidaSom.Speak("Sistema Carregado");
            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Erro em Init()");
            }

        }

        private void setText(string text)
        {
            this.labelText.Text = "Você: " + text;
        }

        private void setColor(string cor)
        {
            switch (cor)
            {
                case "green":
                    this.labelStatus.BackColor = Color.Green;
                    break;
                case "red":
                    this.labelStatus.BackColor = Color.Red;
                    break;
                case "gray":
                    this.labelStatus.BackColor = Color.Gray;
                    break;
                default:
                    this.labelStatus.BackColor = Color.Black;
                    break;
            }
        }

        private List<Grammar> Load_Grammar()
        {
            List<Grammar> GramaticaParaFala = new List<Grammar>();


            #region Choices

            Choices comandosDoSistema = new Choices();
            comandosDoSistema.Add(Gramatica.WhatHour.ToArray());
            comandosDoSistema.Add(Gramatica.WhatDay.ToArray());
            comandosDoSistema.Add(Gramatica.StopListening.ToArray());
            comandosDoSistema.Add(Gramatica.ReturnListening.ToArray());
            comandosDoSistema.Add(Gramatica.StopProgram.ToArray());
            comandosDoSistema.Add(Gramatica.Temperatura.ToArray());
            comandosDoSistema.Add(Gramatica.MoreInfos.ToArray());
            comandosDoSistema.Add(Gramatica.Pergunta_Maq.ToArray());//ass Giovani
            comandosDoSistema.Add(Gramatica.resposta_1.ToArray());// animais
            comandosDoSistema.Add(Gramatica.resultadoRes_1.ToArray());//animais = sim
            comandosDoSistema.Add(Gramatica.resultadoRes_1No.ToArray());// animais em negativo
            comandosDoSistema.Add(Gramatica.resposta_2.ToArray());// tecnologia
            comandosDoSistema.Add(Gramatica.respostaTec_2Games.ToArray());// tecnologia - jogos
            comandosDoSistema.Add(Gramatica.respostaTec_2Web.ToArray());// tecnologia - web
            comandosDoSistema.Add(Gramatica.respostaTec_2dados.ToArray());// tecnologia - dados
            comandosDoSistema.Add(Gramatica.respostaTec_2Animacoes.ToArray());// tecnologia - animações
            comandosDoSistema.Add(Gramatica.resposta_3.ToArray());//ler
            comandosDoSistema.Add(Gramatica.resposta_4.ToArray());//cantar
            comandosDoSistema.Add(Gramatica.piada1.ToArray());
            comandosDoSistema.Add(Gramatica.respostaPiadaSoli.ToArray());
            comandosDoSistema.Add(Gramatica.criação.ToArray());
            comandosDoSistema.Add(Gramatica.motivo.ToArray());
            comandosDoSistema.Add(Gramatica.agradecimentoFinal.ToArray());
           
            //comandosDoSistema.Add(Gramaticas.Comprimento.ToArray());
            //comandosDoSistema.Add(Gramaticas.Bem.ToArray());
            
            
            //comandosDoSistema.Add(Gramaticas.Fechar.ToArray());




            Choices NValues = new Choices();
            // NValues.Add(Gramaticas.Numeros.ToArray());


            #endregion

            #region GrammarBuilder

            GrammarBuilder comandosDoSistema_gb = new GrammarBuilder();
            comandosDoSistema_gb.Append(comandosDoSistema);

            #endregion

            #region Grammar
            Grammar gramaticaSistema = new Grammar(comandosDoSistema_gb);
            gramaticaSistema.Name = "system";



            #endregion

            GramaticaParaFala.Add(gramaticaSistema);


            return GramaticaParaFala;
        }

        private void SpeechRec()
        {
            try
            {
                List<Grammar> g = Load_Grammar();

                #region Speech Recognition (Eventos)
                engine.SetInputToDefaultAudioDevice();

                foreach (Grammar gr in g)
                {
                    engine.LoadGrammar(gr);
                }

                engine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(Rec);
                engine.AudioLevelUpdated += new EventHandler<AudioLevelUpdatedEventArgs>(AudioLevel);
                engine.SpeechRecognitionRejected += new EventHandler<SpeechRecognitionRejectedEventArgs>(Rejected);

                #endregion

                engine.RecognizeAsync(RecognizeMode.Multiple); // Inicia o reconhecimento


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Erro em SpeechRec()");

            }

        }


        #region Eventos do Reconhecimento

        private void Rec(object s, SpeechRecognizedEventArgs e) // Aqui entra se for reconhecido  
        {
            string fala = e.Result.Text;



            if (Ouvindo)
            {
                setText(fala);
                setColor("green");

                switch (e.Result.Grammar.Name)
                {
                    case "system":
                        // Tudo aqui dentro corresponde a gramatica do sistema 

                        if (Gramatica.WhatHour.Any(f => f == fala))
                        {
                            Executer.GetHoras();
                        }
                        else if (Gramatica.WhatDay.Any(f => f == fala))
                        {
                            Executer.GetData();
                        }
                        else if (Gramatica.StopListening.Any(f => f == fala))
                        {
                            Ouvindo = false;
                            setColor("gray");
                            Executer.ModeMute();
                        }

                        else if (Gramatica.StopProgram.Any(f => f == fala))
                        {
                            Ouvindo = false;
                            setColor("gray");
                            Executer.ModeMute();
                        }
                        #region temperatura 
                        /*else if (Gramatica.Temperatura.Any(f => f == fala))
                        {
                            Executer.GetTemperatura(city);
                        }
                        else if (Gramatica.MoreInfos.Any(f => f == fala))
                        {
                            Executer.GetMainInfos(city);
                        }*/
                        #endregion

                        else if (Gramatica.Pergunta_Maq.Any(f => f == fala))// ass Giovani
                        {
                            Ouvindo = false;
                            setColor("gray");
                            Executer.Pergunta();
                            
                        }
                        else if (Gramatica.resposta_1.Any(f => f == fala))//animais
                        {
                            Ouvindo = false;
                            setColor("gray");
                            Executer.RespostaVoltaAnimais_1();

                        }
                        else if (Gramatica.resultadoRes_1.Any(f => f == fala))//animais - sim
                        {
                            Ouvindo = false;
                            setColor("gray");
                            Executer.RespostaVoltaAnimais_sim();
                        }
                        else if (Gramatica.resultadoRes_1No.Any(f => f == fala))//animais - não
                        {
                            Ouvindo = false;
                            setColor("gray");
                            Executer.RespostaVoltaAnimais_Não();
                        }
                        else if (Gramatica.resposta_2.Any(f => f == fala))// tecnologia
                        {
                            Ouvindo = false;
                            setColor("gray");
                            Executer.RespostaVoltaTecno_2();
                        }
                        else if (Gramatica.respostaTec_2Games.Any(f => f == fala))//tec - jogos
                        {
                            Ouvindo = false;
                            setColor("gray");
                            Executer.RespostaVoltaTecno_jogos();
                        }
                        else if (Gramatica.respostaTec_2Web.Any(f => f == fala))//tec - websites
                        {
                            Ouvindo = false;
                            setColor("gray");
                            Executer.RespostaVoltaTecno_Web();
                        }
                        else if (Gramatica.respostaTec_2dados.Any(f => f == fala))//tec - dados
                        {
                            Ouvindo = false;
                            setColor("gray");
                            Executer.RespostaVoltaTecno_dados();
                        }
                        else if(Gramatica.respostaTec_2Animacoes.Any(f => f == fala))// tec - animações
                        {
                            Ouvindo = false;
                            setColor("gray");
                            Executer.RespostaVoltaTecno_animacoes();
                        }
                        else if (Gramatica.resposta_3.Any(f => f == fala))// Ler
                        {
                            Ouvindo = false;
                            setColor("gray");
                            Executer.RespostaVolta_3Ler();
                        }
                        else if (Gramatica.resposta_4.Any(f => f == fala))//cantar
                        {
                            Ouvindo = false;
                            setColor("gray");
                            Executer.RespostaVolta_4Cantar();
                        }
                        else if (Gramatica.piada1.Any(f => f == fala))
                        {
                            Ouvindo = false;
                            setColor("gray");
                            Executer.PgtaPiada1();
                        }
                        else if (Gramatica.respostaPiadaSoli.Any(f => f == fala))
                        {
                            Ouvindo = false;
                            setColor("gray");
                            Executer.Respostapiada1();
                        }
                        else if (Gramatica.criação.Any(f => f == fala))
                        {
                            Ouvindo = false;
                            setColor("gray");
                            Executer.RespostaOrigem();
                        }
                        else if (Gramatica.agradecimentoFinal.Any(f => f == fala))
                        {
                            Ouvindo = false;
                            setColor("gray");
                            Executer.Agradecimento();
                        }
                        else if (Gramatica.motivo.Any(f => f == fala))
                        {
                            Ouvindo = false;
                            setColor("gray");
                            Executer.RespostaMotivo();
                        }
                        break;
                }
            }
            else
            {
                if (Gramatica.ReturnListening.Any(f => f == fala))
                {
                    Ouvindo = true;
                    setColor("green");
                    Executer.ReturnMute();

                }
            }

        }
        private void AudioLevel(object s, AudioLevelUpdatedEventArgs e)
        {
            if (e.AudioLevel > barraDeAudio.Maximum)
            {
                barraDeAudio.Value = barraDeAudio.Maximum;
            }
            else if (e.AudioLevel < barraDeAudio.Minimum)
            {
                barraDeAudio.Value = barraDeAudio.Minimum;
            }
            else
            {
                barraDeAudio.Value = e.AudioLevel;
            }
        }
        private void Rejected(object s, SpeechRecognitionRejectedEventArgs e) // Aqui fica o que ela nao entender
        {

            // saidaSom.Speak("Fala não reconhecida");
            setText("Fala não reconhecida");
            setColor("red");

        }


        #endregion


    }
}


