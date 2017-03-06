using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;


namespace GameEvent
{
    //this class stores the values of the Animals within a region
    public class Animal
    {

        public string[] status = { "Healthy", "Threatened", "Endangered", "extinct" };

        public string name { get; private set; }
        public string currentStatus { get; private set; }

        public Animal(string name, Random rnd)
        {
            this.name = name;
            //0,3 to go from Healthy to Endangered, 0,4 to go from Healthy to Extinct (during generation)
            currentStatus = status[rnd.Next(0,3)];
        }

        //Change the status of the animal towards the left of the array (towards healthy)
        public void increaseCurrentStatus()
        {
            for (int i = 0; i < status.Length; i++)
            {
                if (status[i] == currentStatus && currentStatus != status[0])
                {
                    currentStatus = status[i - 1];
                    break;
                }
            }
        }

        //Change the status of the animal towards the right of the array (towards extinct)
        public void decreaseCurrentStatus()
        {
            for (int i = 0; i < status.Length; i++)
            {
                if (status[i] == currentStatus && currentStatus != status[status.Length - 1])
                {
                    currentStatus = status[i + 1];
                    break;
                }
            }
        }
    }
}
