using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectGreanLeader
{
    public class SectorStatistics
    {
        public double happiness { get; private set; }
        public double ecoAwareness { get; private set; }
        public double prosperity { get; private set; }

        public SectorStatistics(double happiness, double ecoAwareness, double prosperity)
        {
            this.happiness = happiness;
            this.ecoAwareness = ecoAwareness;
            this.prosperity = prosperity;
        }
    }
}
