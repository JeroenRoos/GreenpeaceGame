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
    public bool isGlobal { get; private set; }
    public bool isRegional { get; private set; }

    public RegionStatistics questCompleteConditions { get; private set; }
    public double questMoneyReward { get; private set; }

    public bool isActive { get; private set; }

    public Quest()
    {
        questID = "placeholderName";
        name = new string[] { "dutchname", "englishname" };
        description = new string[] { "dutchdescription", "englishdescription" };
        startYear = 0;
        startMonth = 0;
        isGlobal = false;
        isRegional = false;
        questCompleteConditions = new RegionStatistics();
        questMoneyReward = 0;
        isActive = false;
    }

    public void StartQuest()
    {
        isActive = true;
    }

    public void CompleteQuest()
    {
        isActive = false;
    }
}
