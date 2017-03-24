using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public abstract class RegionSector
{
    public virtual SectorStatistics statistics { get; protected set; }

<<<<<<< HEAD
    public abstract class RegionSector
{
    public virtual string sectorName { get; protected set; }
    public virtual SectorStatistics statistics { get; protected set; }

        public RegionSector()
        {
        }
=======
    public RegionSector()
    {
>>>>>>> 1b894b7f528e47001249c96fb9448c6ed9da6861
    }
}
