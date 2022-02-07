using System;

namespace Vann_Unit_test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //var water = new Water(4, -10);
            //water.AddEnergy(168);
            //Console.WriteLine(@$"
            //Amount:     {water.Amount}
            //State:      {water.State}
            //Temp:       {water.Temperature}
            //Prop:       {water.ProportionFirstState}");

            //Start her med debug og gå gjennom addenergy-------------------------------------
            var water = new Water(10, 70);
            water.AddEnergy(6400);
            Console.WriteLine(@$"
            Amount:     {water.Amount}
            State:      {water.State}
            Temp:       {water.Temperature}
            Prop:       {water.ProportionFirstState}");

        }
    }
}
