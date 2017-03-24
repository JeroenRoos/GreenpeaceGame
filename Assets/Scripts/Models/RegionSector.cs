using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectGreanLeader
{
    public abstract class RegionSector
    {
        public virtual SectorStatistics statistics { get; protected set; }

        public RegionSector()
        {
        }
    }
}
