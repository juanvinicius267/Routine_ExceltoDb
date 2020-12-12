using System.Collections.Generic;
using System.Linq;
using TesteEDMX;
namespace TesteEDMX
{
    public class DAO
    {
        public void DeleteAll()
        {
            using (var context = new OutboundTrackNTrace())
            {
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE [Maritimo]");
                
                context.SaveChanges();
            }
        }

        public void Adiciona(Maritimo excelData)
        {
            using (var context = new OutboundTrackNTrace())
            {
               
                context.Maritimo.Add(excelData);
                context.SaveChanges();
            }
        }
        public List<Maritimo> Consulta()
        {

            using (var context = new OutboundTrackNTrace())
            {
                var excelData = context.Maritimo
                    .ToList();

                return excelData;

            }

        }
        public int Consulta2(string chassi)
        {

            using (var context = new OutboundTrackNTrace())
            {
                var excelData = context.Maritimo
                    .Where(e => e.Chassis == chassi).ToList();
                int h = excelData.Count();
                return h;

            }

        }
    }
}
