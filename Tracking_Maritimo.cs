using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteEDMX
{
    public class SouthAfricaRoutine
    {
        public void Routine()
        {
            // Declaração das variaveis utilizadas no sistema
            var excelData = new Maritimo();
            var dao = new DAO();
            int retornoDaConsulta;
            string[] vs = new string[1];
            //Declaração dos links das planilhas que o sistema deve acessar
            vs[0] = " G:/15_TEMPORARY/Juan/Tracking Maritimo.xlsx";



            //Intervalo de tempo entre uma leitura e outra

            //Lê a hora do sistema
            DateTime Time = DateTime.Now;
            //Coloca a hora lida em formato de TimeOfDay
            TimeSpan horaAtual = Time.TimeOfDay;
            //Recebe o valor da hora de leitura
            TimeSpan horaDeLeituraMin = TimeSpan.Parse("7:31:00.1");
            TimeSpan horaDeLeituraMax = TimeSpan.Parse("7:33:00.1");
            TimeSpan tempoFaltante = horaDeLeituraMin - horaAtual;
            Console.WriteLine("Aguardando o horario de inicio de leitura, que é as: " + horaDeLeituraMin + " A hora atual é: " + horaAtual + " Faltam: " + tempoFaltante);
            //Se a hora atual for igual a hora de leitura, a rotina roda. Senão ela espera
            if (true)//horaAtual >= horaDeLeituraMin && horaAtual <= horaDeLeituraMax)
            {
                Console.WriteLine(" O sistema iniciou a leitura");
                dao.DeleteAll();
                //Rotina de leitura das planilhas
                for (int i = 0; i < vs.Length; i++)
                {
                    //Tenta realizar a leitura da planilha
                    try
                    {
                        //Abre o arquivo no link setado 
                        using (var stream = File.Open(vs[i], FileMode.Open, FileAccess.Read))
                        {
                            //Abre a planilha no link setado 
                            using (var reader = ExcelReaderFactory.CreateReader(stream))
                            {
                                do
                                {
                                    
                                    //Executa a leitura enquato existir linha e colunas a serem lidas
                                    while (reader.Read())
                                    {
                                        
                                        // Lê o valor de Chassi e verifica se ja existe no banco de dados 
                                        excelData.Chassis = Convert.ToString(reader.GetValue(0));
                                        // Se ja existir retorna 1 e senão existir retorna 0
                                        retornoDaConsulta = 0;//dao.Consulta2(excelData.Chassis);
                                        //Senão existir o numero de Chassi no banco de dados realiza a leitura da linha e das colunas
                                        if (retornoDaConsulta == 0)
                                        {
                                            //Converte o valor lido (que vem em Object) de object para string e coloca na variavel especifica do sistema 
                                            excelData.BatchId = Convert.ToString(reader.GetValue(8));

                                            excelData.PopId = Convert.ToString(reader.GetValue(1));

                                           // excelData.CustomerOrder = Convert.ToString(reader.GetValue(3));

                                           // excelData.PartPeriod = Convert.ToString(reader.GetValue(4));

                                            excelData.Type = Convert.ToString(reader.GetValue(2));

                                            excelData.Market = Convert.ToString(reader.GetValue(5));

                                            excelData.Model = Convert.ToString(reader.GetValue(2));

                                           // excelData.PDD = Convert.ToString(reader.GetValue(11));

                                           // excelData.PlanPacking = Convert.ToString(reader.GetValue(12));

                                           // excelData.PlanDelivery = Convert.ToString(reader.GetValue(13));

                                            excelData.Liner = Convert.ToString(reader.GetValue(4));

                                            excelData.PortDestination = Convert.ToString(reader.GetValue(5));

                                            //excelData.InttraNumber = Convert.ToString(reader.GetValue(16));

                                            //excelData.Booking = Convert.ToString(reader.GetValue(17));

                                            //excelData.Terminal = Convert.ToString(reader.GetValue(18));

                                           // excelData.Container40 = Convert.ToString(reader.GetValue(19));

                                           // excelData.Container20 = Convert.ToString(reader.GetValue(20));

                                            excelData.Vessel = Convert.ToString(reader.GetValue(11));

                                           // excelData.LastDateOutSLA = Convert.ToString(reader.GetValue(9));

                                            excelData.ETDSantos = Convert.ToString(reader.GetValue(9));

                                            excelData.ETD2Santos = Convert.ToString(reader.GetValue(10));

                                           // excelData.ATDSantos = Convert.ToString(reader.GetValue(10));

                                            excelData.ETADestination = Convert.ToString(reader.GetValue(12));

                                            excelData.ETA2Destination = Convert.ToString(reader.GetValue(13));

                                          //  excelData.ATADestination = Convert.ToString(reader.GetValue(13));
                                            //Grava tudo que foi lido na linha e nas colunas no banco de dados 
                                            dao.Adiciona(excelData);

                                            //Informa a gravação no banco de dados 
                                            Console.WriteLine("Gravou no banco");
                                        }
                                        // Se ja existir o Chassi no banco retorna 1 e não grava os dados no banco
                                        else
                                        {
                                            //Informa que não foi possivel a gravação no banco de dados 
                                            Console.WriteLine("Não foi possivel a gravação no banco de dados");
                                        }

                                    }
                                    //Retorna a leitura caso exista alguma outra aba na planilha
                                } while (reader.NextResult());
                            }
                        }
                    }
                    //Pega alguma erro na tentativa de leitura
                    catch (Exception)
                    {
                        Console.WriteLine("Impossivel ler a planilha, planilha com problemas");
                    }
                }
                //Informa que finalizou o programa
                Console.WriteLine("Programa Finalizado");

            }



        }
    }
}
