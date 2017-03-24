using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class SectorStatistics
{
    public double happiness { get; private set; }
    public double ecoAwareness { get; private set; }
    public double prosperity { get; private set; }

<<<<<<< HEAD
public class SectorStatistics
{
    public double happiness { get; private set; }
    public double ecoAwareness { get; private set; }
    public double prosperity { get; private set; }
    public Pollution pollution { get; private set; }

    public SectorStatistics(double happiness, double ecoAwareness, double prosperity, Pollution pollution)
=======
    public SectorStatistics(double happiness, double ecoAwareness, double prosperity)
>>>>>>> 1b894b7f528e47001249c96fb9448c6ed9da6861
    {
        this.happiness = happiness;
        this.ecoAwareness = ecoAwareness;
        this.prosperity = prosperity;
<<<<<<< HEAD
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
=======
>>>>>>> 1b894b7f528e47001249c96fb9448c6ed9da6861
    }
}
