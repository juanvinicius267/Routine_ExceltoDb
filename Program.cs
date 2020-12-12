using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace TesteEDMX
{
    class Program
    {
        static void Main(string[] args)
        {
            //Lê a hora do sistema
            DateTime Time = DateTime.Now;
            //Coloca a hora lida em formato de TimeOfDay
            TimeSpan horaAtual = Time.TimeOfDay;
            //Recebe o valor da hora de leitura
            TimeSpan horaDeLeituraMin = TimeSpan.Parse("4:31:00.1");
            TimeSpan horaDeLeituraMax = TimeSpan.Parse("6:33:00.1");
            TimeSpan tempoFaltante = horaDeLeituraMin - horaAtual;
            Console.WriteLine("Aguardando o horario de inicio de leitura, que é as: " + horaDeLeituraMin + " A hora atual é: " + horaAtual + " Faltam: " + tempoFaltante);
            if(true) //(horaAtual >= horaDeLeituraMin && horaAtual <= horaDeLeituraMax)
            {
                //Roda toda a rotina ciclicamento
                //while (true)
                //{
                    //Chama rotina para a africa
                    SouthAfricaRoutine southAfricaRoutine = new SouthAfricaRoutine();
                    southAfricaRoutine.Routine();                    

                //}


            }



        }


    }
}
