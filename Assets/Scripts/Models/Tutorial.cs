using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class Tutorial
{
    public int tutorialIndex;
    public bool tutorialNoTooltip;
    public bool regionWestActivated;

    public bool tutorialOrganizationDone;
    public bool tutorialRegionDone;
    public bool tutorialNextTurnDone;
    public bool tutorialEventsDone;
    public bool tutorialMonthlyReportDone;
    public bool tutorialCheckActionDone;
    public bool tutorialCardsDone;
    public bool tutorialQuestsDone;
    public bool tutorialInvestementsDone;
    public bool tutorialBuildingsDone;

    public bool tutorialNexTurnPossibe;
    public bool tutorialQuestsActive;
    public bool tutorialeventsClickable;

    public bool tutorialRegionsClickable;
    public bool tutorialBuildingsClickable;

    public bool tutorialOnlyWestNL;
    public bool doTuto;

    public bool tutorialActive;
    public bool tutorialOrganizationActive;
    public bool tutorialCardsActive;
    public bool tutorialInvestementsActive;
    public bool tutorialBuildingsActive;
    public bool tutorialRegionActive;
    public bool tutorialEventsActive;
    public bool tutorialMonthlyReportActive;

    public Tutorial()
    {
        tutorialActive = true;





        tutorialIndex = 0;
        tutorialNexTurnPossibe = false;
        tutorialQuestsActive = false;
        tutorialOrganizationActive = false;
        tutorialeventsClickable = false;
        tutorialBuildingsClickable = false;
        tutorialOnlyWestNL = false;
        tutorialRegionActive = false;
        tutorialEventsActive = false;
        tutorialMonthlyReportActive = false;
        tutorialCardsActive = false;
        tutorialInvestementsActive = false;
        tutorialBuildingsActive = false;
        tutorialRegionsClickable = false;
        tutorialNoTooltip = true;
        regionWestActivated = false;

        tutorialOrganizationDone = false;
        tutorialRegionDone = false;
        tutorialNextTurnDone = false;
        tutorialEventsDone = false;
        tutorialMonthlyReportDone = false;
        tutorialCheckActionDone = false;
        tutorialCardsDone = false;
        tutorialQuestsDone = false;
        tutorialInvestementsDone = false;
        tutorialBuildingsDone = false;
    }
}
