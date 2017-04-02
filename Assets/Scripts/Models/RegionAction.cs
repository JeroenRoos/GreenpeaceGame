using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//namespace Assets.Scripts.Models
public class RegionAction
{
    public string[] description { get; private set; }
    public RegionStatistics consequences { get; private set; }
    public RegionStatistics actionCosts { get; private set; }
    public double actionMoneyCost { get; private set; }
    public int actionDuration { get; private set; } //in months
    public int? startYear { get; private set; }
    public int? startMonth { get; private set; }
    public int? lastCompleted { get; private set; } //in months
    public int actionCooldown { get; private set; } //in months
    public bool isActive { get; private set; }

    public RegionAction(string[] description, RegionStatistics consequences, RegionStatistics actionCosts, int actionDuration, int actionCooldown,
                        double actionMoneyCost)
    {
        this.description = description;
        this.consequences = consequences;
        this.actionDuration = actionDuration;
        this.actionCooldown = actionCooldown;
        this.actionCosts = actionCosts;
        this.actionMoneyCost = actionMoneyCost;
        isActive = false;
    }

    public void ActivateAction(int startYear, int startMonth)
    {
        if (lastCompleted == null || (lastCompleted != null && !(startYear * 12 + startMonth >= lastCompleted + actionCooldown)))
        {
            this.startYear = startYear;
            this.startMonth = startMonth;
            isActive = true;
        }

        else
        {
            //toon dat action op cooldown is
        }
    }

    public void CompleteAction()
    {
        lastCompleted = startYear * 12 + startMonth + actionDuration;
        this.startYear = null;
        this.startMonth = null;
        isActive = false;
    }
}