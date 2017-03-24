using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    public abstract class RegionSector
{
    public virtual string sectorName { get; protected set; }
    public virtual SectorStatistics statistics { get; protected set; }

        public RegionSector()
        {
        }
    }
