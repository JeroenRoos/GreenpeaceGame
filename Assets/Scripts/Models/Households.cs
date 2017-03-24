using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

<<<<<<< HEAD

public class Households : RegionSector
{
    public Households(string sectorName, SectorStatistics statistics)
    {
        this.sectorName = sectorName;
        this.statistics = statistics;
    }
}

=======
public class Households : RegionSector
{
    public Households(SectorStatistics statistics)
    {
        this.statistics = statistics;
    }
}
>>>>>>> 1b894b7f528e47001249c96fb9448c6ed9da6861
