using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

<<<<<<< HEAD

public class Companies : RegionSector
{
    public Companies(string sectorName, SectorStatistics statistics)
    {
        this.sectorName = sectorName;
=======
public class Companies : RegionSector
{
    public Companies(SectorStatistics statistics)
    {
>>>>>>> 1b894b7f528e47001249c96fb9448c6ed9da6861
        this.statistics = statistics;
    }
}
