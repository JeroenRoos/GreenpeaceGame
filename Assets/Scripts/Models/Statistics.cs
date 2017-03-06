using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEvent
{
    //class to store all the relevant statistics of a region
    public class Statistics
    {
        public int income { get; private set; }
        public int happiness { get; private set; }
        public double pollution { get; private set; }
        public double oxygen { get; private set; }

        public double pollutionIncrease { get; private set; } //the increase of pollution during a certain timeframe
        public double oxygenIncrease { get; private set; } //the increase of  oxygen during a certain timeframe

        public Statistics(int income, int happiness, double pollution, double oxygen)
        {
            this.income = income;
            this.happiness = happiness;
            this.pollution = pollution;
            this.oxygen = oxygen;

            //dummy values for testing
            pollutionIncrease = 5;
            oxygenIncrease = -5;
        }

        public void ChangeIncome(int changeValue)
        {
            income = income + changeValue;
        }

        public void changeHappiness(int changeValue)
        {
            happiness = happiness + changeValue;
        }

        public void ChangePollutionMutation(double changeValue)
        {
            pollutionIncrease = pollutionIncrease + changeValue;
        }

        public void ChangeOxygenMutation(double changeValue)
        {
            oxygenIncrease = oxygenIncrease + changeValue;
        }

        public void mutateTimeBasedStatistics()
        {
            if (oxygen < 100)
                oxygen = oxygen + ((100 - oxygen) / 100 * oxygenIncrease);
            else
                oxygen = oxygen = oxygen + ((120 - oxygen) / 100 * oxygenIncrease);

            if (pollution > 0)
                pollution = pollution + (pollution / 100 * pollutionIncrease);
            else
                pollution = pollution + ((pollution + 20) / 100 * pollutionIncrease);
        }
    }
}
