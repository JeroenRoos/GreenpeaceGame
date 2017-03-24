using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectGreanLeader
{
    public class BuildingStatistics
    {
        public double income { get; private set; }
        public Pollution pollution { get; private set; }
        public double prosperity { get; private set; }

        public BuildingStatistics(double income, Pollution pollutionInfluence, double prosperityInfluence)
        {
            this.income = income;
            this.pollution = pollutionInfluence;
            this.prosperity = prosperityInfluence;
        }
    }
}
