using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class Tutorial
{
    /*
    public bool tutorialStep2;
    public bool tutorialStep3;
    public bool tutorialStep4;
    public bool tutorialStep5;
    public bool tutorialStep6;
    public bool tutorialStep7;
    public bool tutorialStep8;
    public bool tutorialStep9;
    public bool tutorialStep10;
    public bool tutorialstep11;
    public bool tutorialstep12;
    public bool tutorialStep13;
    public bool tutorialStep14;
    public bool tutorialStep15;
    public bool tutorialStep16;
    public bool tutorialStep17;
    public bool tutorialStep18;
    public bool tutorialstep19;
    public bool tutorialStep20;
    public bool tutorialStep21;
    */

    public int tutorialIndex;
    public bool tutorialActive;
    public bool tutorialNoTooltip;
    public bool tutorialCheckActionDone;
    public bool regionWestActivated;
    public bool tutorialOrganizationDone;
    public bool tutorialNextTurnDone;
    public bool tutorialEventsDone;
    public bool tutorialMonthlyReportDone;
    public bool tutorialNexTurnPossibe;
    public bool tutorialQuestsActive;
    public bool tutorialOrganizationActive;
    public bool tutorialeventsClickable;
    public bool tutorialBuildingsClickable;
    public bool tutorialOnlyWestNL;
    public bool tutorialRegionActive;
    public bool tutorialEventsActive;
    public bool tutorialMonthlyReportActive;
    public bool doTuto;
    public bool tutorialCardsActive;
    public bool tutorialCardsDone;
    public bool tutorialInvestementsActive;
    public bool tutorialBuildingsActive;
    public bool tutorialRegionsClickable;

    public bool[] tutorialChecks;

    public Tutorial()
    {
        tutorialIndex = 0;
        tutorialNexTurnPossibe = false;
        tutorialNextTurnDone = false;
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
        tutorialCheckActionDone = false;
        tutorialChecks = new bool[21];
        tutorialActive = true;


    }
}
