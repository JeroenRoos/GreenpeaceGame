using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEvent
{
    //this class stores the values of the Council in a region
    public class Council
    {
        public string[] mood = { "Satisfied", "Neutral", "Annoyed", "Angry" };

        public string name { get; private set; }
        public string currentMood { get; private set; }

        public Council(string name)
        {
            this.name = name;
            currentMood = mood[1];
        }

        //Change the mood of the council towards the left of the array (towards satisfied)
        public void increasecurrentMood()
        {
            for (int i = 0; i < mood.Length; i++)
            {
                if (mood[i] == currentMood && currentMood != mood[0])
                {
                    currentMood = mood[i - 1];
                    break;
                }
            }
        }

        //Change the mood of the council towards the right of the array (towards angry)
        public void decreasecurrentMood()
        {
            for (int i = 0; i < mood.Length; i++)
            {
                if (mood[i] == currentMood && currentMood != mood[mood.Length - 1])
                {
                    currentMood = mood[i + 1];
                    break;
                }
            }
        }
    }
}
