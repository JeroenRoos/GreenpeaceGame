using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEvent
{
    //UNFINISHED
    public class GameEvent
    {
        public string description { get; private set; }
        public Region region { get; private set; }
        public bool isUnique { get; private set; }
        public string[] choices { get; private set; }
        public string pickedChoice { get; private set; }
        public List<GameEvent> previousEvents { get; private set; }
        public int eventDuration { get; private set; } //in months
        public int startMonth { get; private set; }
        public bool isPositive { get; private set; } //if true, the event is a "good" event

        public bool isFinished { get; private set; }

        public GameEvent(string description, Region region, bool isUnique, int eventDuration, string[] choices, List<GameEvent> previousEvents, bool isPositive)
        {
            this.description = description;
            this.region = region;
            this.isUnique = isUnique;
            this.eventDuration = eventDuration;
            this.choices = choices;
            this.previousEvents = previousEvents;
            this.isPositive = isPositive;
        }

        public void ActivateEvent(int startMonth)
        {
            this.startMonth = startMonth;

            DisplayEvent();
        }

        public void DisplayEvent()
        {
            Console.WriteLine("Region: {0}", region.name);
            Console.WriteLine(description);
            Console.WriteLine();
            Console.WriteLine("What will you do?");
            int i = 1;
            foreach (string choice in choices)
            {
                Console.WriteLine("{0}: {1}", i, choice);
                i++;
            }
        }

        public bool IsAvailable()
        {
            bool isAvailable = true;
            foreach (GameEvent previousEvent in previousEvents)
            {
                if ((isFinished && isUnique) || previousEvent.isFinished == false)
                {
                    isAvailable = false;
                    break;
                }
            }

            return isAvailable;
        }

        public void SetPickedChoice(string pickedChoice) //string = Choice (class)
        {
            this.pickedChoice = pickedChoice;
        }

        /*structure of choices:
         * choices[0] = Extreme green solution
         * choices[1] = negotiating with the region
         * choices[2] = researching
         * choices[4] = do nothing
         */
        public void ActivatePickedChoice()
        {
            if (pickedChoice == choices[0])
            {
                CompleteEvent();
            }
            else if (pickedChoice == choices[1])
            {
                CompleteEvent();
            }
            else if (pickedChoice == choices[2])
            {
                CompleteEvent();
            }
            else if (pickedChoice == choices[3])
            {
                CompleteEvent();
            }
        }
        
        private void CompleteEvent()
        {
            isFinished = true;
        }
    }
}
