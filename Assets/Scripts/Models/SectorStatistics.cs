using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class SectorStatistics
{
    public double happiness { get; private set; }
    public double ecoAwareness { get; private set; }
    public double prosperity { get; private set; }
    public Pollution pollution { get; private set; }

    public SectorStatistics(double happiness, double ecoAwareness, double prosperity, Pollution pollution)
    {
        this.happiness = happiness;
        this.ecoAwareness = ecoAwareness;
        this.prosperity = prosperity;
        this.pollution = pollution;
    }

    //happiness = prosperity - pollution *formule*
    public void ModifyHappiness(double changeValue)
    {
        happiness += changeValue;
    }

    public void ModifyEcoAwareness(double changeValue)
    {
        ecoAwareness += changeValue;
    }

    public void ModifyProsperity(double changeValue)
    {
        prosperity += changeValue;
    }
}
