using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class Companies : RegionSector
{
    public Companies(string sectorName, SectorStatistics statistics)
    {
        this.sectorName = sectorName;
        this.statistics = statistics;
    }
}
