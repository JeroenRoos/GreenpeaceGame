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
    public SectorStatistics consequences { get; private set; }
    public SectorStatistics actionCosts { get; private set; }
    public double actionMoneyCost { get; private set; }
    public double actionMoneyReward { get; private set; }
    public int actionDuration { get; private set; } //in months
    public int startYear { get; private set; }
    public int startMonth { get; private set; }
    public int lastCompleted { get; private set; } //in months
    public int actionCooldown { get; private set; } //in months
    public bool isActive { get; private set; }

    public string[] possibleSectors { get; private set; }
    public bool[] pickedSectors { get; private set; }

    private RegionAction()
    {
        actionMoneyReward = 0;
        possibleSectors = new string[] { "Huishoudens", "Bedrijven", "Landbouw" };
        pickedSectors = new bool[] { false, false, false };
    }

    public RegionAction(string[] name, string[] description, SectorStatistics consequences, SectorStatistics actionCosts, int actionDuration, int actionCooldown,
                        double actionMoneyCost)
    {
        this.name = name;
        this.description = description;
        this.consequences = consequences;
        this.actionDuration = actionDuration;
        this.actionCooldown = actionCooldown;
        this.actionCosts = actionCosts;
        this.actionMoneyCost = actionMoneyCost;
        isActive = false;

        startYear = 0;
        startMonth = 0;
        lastCompleted = 0;
    }

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
        this.startYear = 0;
        this.startMonth = 0;
        isActive = false;
    }
}