using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


//UNFINISHED
public class GameEvent
{
    public string name { get; private set; } //id
    public string[] description { get; private set; }
    public int[] eventDuration { get; private set; } //in months
    public string[,] choices { get; private set; }
    public RegionStatistics[] consequences { get; private set; }
    public double[] eventChoiceMoneyCost { get; private set; }
    public int eventCooldown { get; private set; }
    public RegionStatistics pickedChoice { get; private set; }
    public int pickedChoiceNumber { get; private set; }
    public int startYear { get; private set; }
    public int startMonth { get; private set; }

    public bool isActive { get; private set; }

    public Region region { get; private set; }

    public GameEvent(string name, string[] description, int[] eventDuration, string[,] choices,
                    RegionStatistics[] consequences, double[] eventChoiceMoneyCost, int eventCooldown)
    {
        this.name = name;
        this.description = description;
        this.eventDuration = eventDuration;
        this.choices = choices;
        this.consequences = consequences;
        this.eventChoiceMoneyCost = eventChoiceMoneyCost;
        this.eventCooldown = eventCooldown;
    }

    public void ActivateEvent(int startYear, int startMonth, Region region)
    {
        this.startYear = startYear;
        this.startMonth = startMonth;
        this.region = region;

        isActive = true;
        //DisplayEvent();
    }

    public void CompleteEvent()
    {
        region.ImplementStatisticValues(pickedChoice, true);
        isActive = false;
    }

    public void SetPickedChoice(int i) //string = Choice (class)
    {
        this.pickedChoice = consequences[i];
        this.pickedChoiceNumber = i;
    }
}

