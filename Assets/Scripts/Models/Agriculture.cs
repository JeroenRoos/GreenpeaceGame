using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Agriculture : RegionSector
{
    public Agriculture(string sectorName, SectorStatistics statistics)
    {
        this.sectorName = sectorName;
        this.statistics = statistics;
    }
}

