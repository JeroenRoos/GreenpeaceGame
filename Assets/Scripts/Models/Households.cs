using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class Households : RegionSector
{
    public Households(string sectorName, SectorStatistics statistics)
    {
        this.sectorName = sectorName;
        this.statistics = statistics;
    }
}

