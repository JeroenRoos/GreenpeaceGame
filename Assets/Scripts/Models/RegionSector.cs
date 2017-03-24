using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    public class RegionSector
{
    public string sectorName { get; protected set; }
    public SectorStatistics statistics { get; protected set; }

    public RegionSector(string sectorName, SectorStatistics statistics)
    {
        this.sectorName = sectorName;
        this.statistics = statistics;
    }
}
