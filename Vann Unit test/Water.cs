using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vann_Unit_test
{
    public enum WaterState
    {
        Fluid,
        Gas,
        Ice,
        FluidAndGas,
        IceAndFluid
    }

    public class Water
    {
        public double Temperature;
        public double Amount;
        public WaterState State;
        public double ProportionFirstState { get; set; }

        public Water(double amount, double temperature, double proportionFirstState = 0)
        {
            Temperature = temperature;
            Amount = amount;
            ProportionFirstState = proportionFirstState;
            CheckState();
        }

        private void CheckState()
        {
            if (Temperature >= 100 && ProportionFirstState > 0) State = WaterState.FluidAndGas;
            else if (Temperature == 0 && ProportionFirstState > 0) State = WaterState.IceAndFluid;
            else if (Temperature >= 100 && ProportionFirstState == 0) State = WaterState.Gas;
            else if (Temperature >= 0 && ProportionFirstState == 0) State = WaterState.Fluid;
            else if (Temperature <= 0 && ProportionFirstState == 0) State = WaterState.Ice;

        }



        public void AddEnergy(double energy)
        {

            if (Temperature < 0 && energy > 0)
            {
                double temperatureRise = energy / Amount;
                var temperatureNeeded = temperatureRise - (Temperature + temperatureRise);
                var tempRiseLeftover = temperatureRise - temperatureNeeded;

                if (temperatureRise > temperatureNeeded)
                {
                    Temperature += temperatureNeeded;
                    energy = tempRiseLeftover * Amount;
                }
                else Temperature += temperatureRise;

            }

            if (Temperature == 0)
            {
                var smeltFactor = Amount * 80;
                if (energy >= smeltFactor)
                {
                    energy -= smeltFactor;
                    //State = WaterState.Fluid;
                    CheckState();
                }
                else
                {
                    var waterAmount = energy / 80;
                    energy -= 80 * waterAmount;
                    var IceAmount = Amount - waterAmount;
                    ProportionFirstState = IceAmount / Amount;
                    //State = WaterState.IceAndFluid; //<-------------- SKAL HÅNDTERES AV CHECKSTATE() ??
                    CheckState();
                }
            }

            if (Temperature >= 0 && energy > 0)
            {
                // Deler:10
                // Temp:70
                // Energi:900

                double temperatureRise = energy / Amount;
                var leftI = temperatureRise * Amount;
                var tempCheck = Temperature + temperatureRise;
                var energyNeeded = (100 - Temperature) * Amount;


                if (Temperature < 100)
                {
                    if (Temperature + temperatureRise > 100)
                    {
                        Temperature = 100;
                        energy -= energyNeeded; // 600
                    }
                    else
                    {
                        Temperature += temperatureRise;
                        energy -= leftI;
                    }
                }

                if (Temperature == 100)
                {
                    var TempgassAmount = energy / 600; // 1 
                    var gassAmount = TempgassAmount > Amount ? Amount : TempgassAmount; // 1
                    var excessAmount = TempgassAmount - gassAmount; 

                    var toGassFactor = Amount * 600; //6000

                    if (energy > toGassFactor) 
                    {
                        energy -= toGassFactor;
                        //State = WaterState.Gas;
                        CheckState();
                    }

                    var waterAmount = excessAmount > 0 ? 0 : Amount - gassAmount; // 9

                    if (excessAmount <= 0)
                    {
                        ProportionFirstState = waterAmount / Amount; //0,9
                        //State = WaterState.FluidAndGas;
                        CheckState();
                    }
                    if (energy > 0 && gassAmount == Amount)
                    {
                        temperatureRise = energy / Amount;
                        Temperature += temperatureRise;
                        energy -= temperatureRise * Amount;
                        //State = WaterState.Gas;
                        ProportionFirstState = 0;
                        CheckState();
                    }

                }
                if (energyNeeded <= tempCheck && energy > 0)
                {
                    Temperature += temperatureRise;
                    //State = WaterState.Gas;
                    CheckState();
                }
            }
        }

    }
}
