using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    //This class stores the values of the Regions
    public class Region
    {
        public string name { get; private set; }
        public RegionStatistics statistics { get; private set; }
        public List<Building> buildings { get; private set; }

        public Households households { get; private set; }
        public Companies companies { get; set; }
        public Agriculture agriculture { get; set; }

        public Region(string name, RegionStatistics statistics)
        {
            this.name = name;
            this.statistics = statistics;
            buildings = new List<Building>();
        }

        //adds a building to the list of buildings the region has
        public void CreateBuilding(Building newBuilding)
        {
            buildings.Add(newBuilding);
            ImplementBuildingValues(newBuilding.statistics, true);
        }

        public void ImplementBuildingValues(BuildingStatistics statistics, bool isAdded) //if a building is removed for example, isAdded is false
        {
            if (isAdded)
            {
                this.statistics.ChangeIncome(statistics.income);
                this.statistics.ChangeProsperity(statistics.prosperity); //change households and companies instead of region prosperity

                //temporary methods (incomplete)
                this.statistics.pollution.ChangeAirPollutionMutation(statistics.pollution.airPollutionIncrease);
                this.statistics.pollution.ChangeNaturePollutionMutation(statistics.pollution.naturePollutionIncrease);
                this.statistics.pollution.ChangeWaterPollutionMutation(statistics.pollution.waterPollutionIncrease);
            }

            else
            {
                this.statistics.ChangeIncome(0 - statistics.income);
                this.statistics.ChangeProsperity(0 - statistics.prosperity); //change households and companies instead of region prosperity

                //temporary methods (incomplete)
                this.statistics.pollution.ChangeAirPollutionMutation(0 - statistics.pollution.airPollutionIncrease);
                this.statistics.pollution.ChangeNaturePollutionMutation(0 - statistics.pollution.naturePollutionIncrease);
                this.statistics.pollution.ChangeWaterPollutionMutation(0 - statistics.pollution.waterPollutionIncrease);
            }
        }

        public void ImplementStatisticValues(RegionStatistics statistics, bool isAdded) //if a statistic is removed for example, isAdded is false
        {
            if (isAdded)
            {
                this.statistics.ChangeIncome(statistics.income);
                this.statistics.ChangeDonations(statistics.donations);
                this.statistics.changeHappiness(statistics.happiness);
                this.statistics.ChangeEcoAwareness(statistics.ecoAwareness);
                this.statistics.ChangeProsperity(statistics.prosperity);

                //temporary methods (incomplete)
                this.statistics.pollution.ChangeAirPollutionMutation(statistics.pollution.airPollutionIncrease);
                this.statistics.pollution.ChangeNaturePollutionMutation(statistics.pollution.naturePollutionIncrease);
                this.statistics.pollution.ChangeWaterPollutionMutation(statistics.pollution.waterPollutionIncrease);
            }

            else
            {
                this.statistics.ChangeIncome(0 - statistics.income);
                this.statistics.ChangeDonations(0 - statistics.donations);
                this.statistics.changeHappiness(0 - statistics.happiness);
                this.statistics.ChangeEcoAwareness(0 - statistics.ecoAwareness);
                this.statistics.ChangeProsperity(0 - statistics.prosperity);

                //temporary methods (incomplete)
                this.statistics.pollution.ChangeAirPollutionMutation(0 - statistics.pollution.airPollutionIncrease);
                this.statistics.pollution.ChangeNaturePollutionMutation(0 - statistics.pollution.naturePollutionIncrease);
                this.statistics.pollution.ChangeWaterPollutionMutation(0 - statistics.pollution.waterPollutionIncrease);
            }
        }

        //method to show all the values of the Region
        public void DisplayRegionValues(string textDistance)
        {
            ShowRegion(textDistance);
            ShowStatistics(textDistance);
            ShowBuildings(textDistance);
        }

        //shows region name, type, council name and local animals (fixed info)
        private void ShowRegion(string textDistance)
        {
            Console.Write(textDistance, "Region:");
            Console.WriteLine("{0}", name);
            Console.WriteLine("===============================================================");
            Console.WriteLine();
        }
        
        //shows the current statistics of the region (changeable)
        private void ShowStatistics(string textDistance)
        {
            Console.WriteLine("Statistics");
            Console.WriteLine("---------------------------------------------------------------");

            Console.Write(textDistance, "Income: ");
            Console.WriteLine(statistics.income);

            Console.Write(textDistance, "Donations: ");
            Console.WriteLine(statistics.donations);

            Console.Write(textDistance, "Happiness: ");
            Console.WriteLine(statistics.happiness);

            Console.Write(textDistance, "Pollution: ");
            double value = (statistics.pollution.airPollution + statistics.pollution.naturePollution + statistics.pollution.waterPollution) / 3;
            Console.WriteLine("{0:0.0}%", value);

            Console.Write(textDistance, "Eco Awareness: ");
            Console.WriteLine(statistics.ecoAwareness);

            Console.Write(textDistance, "Prosperity: ");
            Console.WriteLine(statistics.prosperity);

            Console.WriteLine();
        }

        //shows the currently present buildings (changeable)
        private void ShowBuildings(string textDistance)
        {
            /*
            string textValue;
            Console.WriteLine("Buildings");
            Console.WriteLine("---------------------------------------------------------------");
            Console.Write(textDistance, "Name");
            Console.Write(textDistance, "Income");
            Console.Write(textDistance, "Donations");
            Console.Write(textDistance, "Happiness");
            Console.Write(textDistance, "Pollution");
            Console.Write(textDistance, "Eco Awareness");
            Console.Write(textDistance, "Prosperity");
            Console.WriteLine();

            foreach (Building building in buildings)
            {
                Console.Write(textDistance, building.name);
                Console.Write(textDistance, building.statistics.income);
                Console.Write(textDistance, building.statistics.donations);
                Console.Write(textDistance, building.statistics.happiness);
                //textValue = (building.statistics.pollution + "% per year");
                //Console.Write(textDistance, textValue);
                Console.Write(textDistance, building.statistics.ecoAwareness);
                Console.Write(textDistance, building.statistics.prosperity);
                
                Console.WriteLine();
            }
            */
        }
    }

