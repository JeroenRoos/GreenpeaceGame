using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class Quest
{
    public string questID { get; private set; }
    public string[] name { get; private set; }
    public string[] description { get; private set; }
    public int startYear { get; private set; }
    public int startMonth { get; private set; }
    public string questLocation { get; private set; }

    public RegionStatistics questCompleteConditions { get; private set; }
    public double questMoneyReward { get; private set; }

    public bool isActive { get; private set; }
    public bool isCompleted { get; private set; }

    public Quest() { }

    #region UpdateQuestStatusMethods
    public void StartQuest()
    {
        isActive = true;
    }

    public void CompleteQuest()
    {
        isActive = false;
        isCompleted = true;
    }
    #endregion

    #region CompletionCheckMethods
    public bool RegionalCompleteConditionsMet(RegionStatistics s)
    {
        bool conditionsMet = true;

        if (!(s.income >= questCompleteConditions.income || questCompleteConditions.income == 0))
            conditionsMet = false;
        if (!(s.prosperity >= questCompleteConditions.prosperity || questCompleteConditions.prosperity == 0))
            conditionsMet = false;
        if (!(s.ecoAwareness >= questCompleteConditions.ecoAwareness || questCompleteConditions.ecoAwareness == 0))
            conditionsMet = false;
        if (!(s.happiness >= questCompleteConditions.happiness || questCompleteConditions.happiness == 0))
            conditionsMet = false;
        if (!(s.avgPollution <= questCompleteConditions.avgPollution || questCompleteConditions.avgPollution == 0))
            conditionsMet = false;

        return conditionsMet;
    }

    public bool NationalCompleteConditionsMet(GameStatistics s)
    {
        bool conditionsMet = true;

        if (!(s.income >= questCompleteConditions.income || questCompleteConditions.income == 0))
            conditionsMet = false;
        if (!(s.prosperity >= questCompleteConditions.prosperity || questCompleteConditions.prosperity == 0))
            conditionsMet = false;
        if (!(s.ecoAwareness >= questCompleteConditions.ecoAwareness || questCompleteConditions.ecoAwareness == 0))
            conditionsMet = false;
        if (!(s.happiness >= questCompleteConditions.happiness || questCompleteConditions.happiness == 0))
            conditionsMet = false;
        if (!(s.pollution <= questCompleteConditions.avgPollution || questCompleteConditions.avgPollution == 0))
            conditionsMet = false;

        return conditionsMet;
    }
    #endregion
}
