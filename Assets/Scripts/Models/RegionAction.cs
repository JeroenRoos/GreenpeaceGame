using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

//namespace Assets.Scripts.Models
[Serializable]
public class RegionAction //: MonoBehaviour
{
    public string[] name { get; private set; }
    public string[] description { get; private set; }
    public double actionMoneyCost { get; private set; }
    public double actionMoneyReward { get; private set; }
    public int actionDuration { get; private set; } //in months
    public string[] possibleSectors { get; private set; }
    public bool[] pickedSectors { get; private set; }
    public int actionCooldown { get; private set; } //in months
    public bool isUnique { get; private set; }
    public int temporaryConsequencesDuration { get; private set; }

    public SectorStatistics actionCosts { get; private set; }
    public SectorStatistics consequences { get; private set; }
    public SectorStatistics temporaryConsequences { get; private set; }
    public SectorStatistics duringActionConsequences { get; private set; }

    public int startYear { get; private set; }
    public int startMonth { get; private set; }
    public int lastCompleted { get; private set; } //in months
    public bool isActive { get; private set; }
    public int endTemporaryConsequencesMonth { get; private set; }

    private RegionAction() { }

    public void ActivateAction(int startYear, int startMonth, bool[] pickedSectors)
    {
        this.pickedSectors = pickedSectors;
        this.startYear = startYear;
        this.startMonth = startMonth;
        isActive = true;
    }

    public void CompleteAction()
    {
        lastCompleted = startYear * 12 + startMonth + actionDuration;
        endTemporaryConsequencesMonth = lastCompleted + +temporaryConsequencesDuration; 
        startYear = 0;
        startMonth = 0;
        isActive = false;
    }
}