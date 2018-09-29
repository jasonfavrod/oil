using System;
using System.Threading.Tasks;
using System.Linq;


namespace Drill
{
    class Drill
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Going to the Web for the oil price...");

            try {
                Rig rig = new Rig();
                Decimal oilPrice = rig.GetPrice().Result;

                using (var context = new econContext())
                {
                    Oil oilPriceSample = new Oil();

                    oilPriceSample.SampleDate = rig.GetSampleDate();
                    oilPriceSample.SampleTime = rig.GetSampleTime();
                    oilPriceSample.Price = oilPrice;
                    oilPriceSample.Uom = rig.Uom;
                    oilPriceSample.Source = rig.Source;

                    context.Oil.Add(oilPriceSample);
                    context.SaveChanges();
                    Console.WriteLine("done");
                }

            }
            catch(Exception ex) {
                Console.WriteLine(ex);
            }
        }
    }
}
