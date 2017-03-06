using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace GameEvent
{
    //calling this class will start a new game. Everything within the game is called from this class
    public class Game
    {
        //All Regions and region types planned for complete game
        private string[] regionNames = { "North America", "South America", "Europe", "Africa", "Asia", "Australia",
                                   "Atlantic Ocean", "Pacific Ocean", "Indian Ocean", "Arctic Ocean"};
        private string[] regionTypes = { "Continent", "Ocean" };

        public int currentYear { get; private set; }
        public int currentMonth { get; private set; }

        public Timer timeflowTimer { get; private set; }

        public List<Region> regions { get; private set; }
        public List<GameEvent> events { get; private set; }

        public List<GameEvent> activeEvents { get; private set; }

        public Game()
        {
            currentYear = 1;
            currentMonth = 1;

            StartTimeflowTimer();
            GenerateRegions();
            GenerateGameEvents();

            Console.WriteLine("Generating complete");

            //ActivateEvent(events[0]);
        }

        private void StartTimeflowTimer()
        {
            timeflowTimer = new Timer();
            timeflowTimer.Elapsed += new ElapsedEventHandler(UpdateGameTime);
            timeflowTimer.Interval = 2000;
            timeflowTimer.Enabled = true;
        }

        private void UpdateGameTime(object source, ElapsedEventArgs e)
        {
            currentMonth++;

            if (currentMonth > 12)
            {
                currentMonth = currentMonth - 12;
                currentYear++;
                ExecuteNewYearMethods();
            }

            ExecuteNewMonthMethods();
        }

        private void ExecuteNewMonthMethods()
        {
            foreach (GameEvent activeEvent in activeEvents)
            {
                if (activeEvent.startMonth + activeEvent.eventDuration == currentYear * 12 + currentMonth)
                {
                    FinishEvent(activeEvent);
                    Console.WriteLine("event finished");
                }
            }

            foreach (GameEvent gameEvent in events)
            {
                if (gameEvent.IsAvailable() && !activeEvents.Contains(gameEvent) && !gameEvent.isFinished)
                {
                    ActivateEvent(gameEvent);
                    Console.WriteLine("event activated");
                }
            }
        }

        private void ExecuteNewYearMethods()
        {
            foreach (Region region in regions)
            {
                region.statistics.mutateTimeBasedStatistics();
            }
        }

        private void GenerateRegions()
        {
            regions = new List<Region>();
            GenerateNorthAmerica();
        }

        private void GenerateNorthAmerica()
        {
            Random rnd = new Random();
            Statistics statistics = new Statistics(5000, 10, 10, 95);
            Council council = new Council("President of America");
            Animal[] animals = { new Animal("Bald Eagle", rnd), new Animal("Bison", rnd), new Animal("Brown Bear", rnd),
                                   new Animal("Elk", rnd), new Animal("Turkey", rnd), new Animal("Beaver", rnd) };

            Region north_America = new Region(regionNames[0], regionTypes[0], statistics, animals, council);

            Building building = new Building("Coal factory");
            north_America.CreateBuilding(building);

            regions.Add(north_America);
        }

        public void DisplayRegion(Region currentRegion)
        {
            foreach (Region region in regions)
            {
                if (currentRegion.name == region.name)
                {
                    region.DisplayRegionValues();
                    break;
                }
            }
        }

        private void GenerateGameEvents()
        {
            events = new List<GameEvent>();
            activeEvents = new List<GameEvent>();

            GenerateFirstEvent();
        }

        private void GenerateFirstEvent()
        {
            string description = "A Coal factory is causing pollution!";
            string[] choices = { "Close the factory", "Negotiate with the Region Council", "Do nothing", "Research an alternative solution" };
            List<GameEvent> previousEvents = new List<GameEvent>();
            GameEvent firstEvent = new GameEvent(description, regions[0], true, 2, choices, previousEvents, false);

            events.Add(firstEvent);
        }

        private void ActivateEvent(GameEvent gameEvent)
        {
            gameEvent.ActivateEvent(12 * currentYear + currentMonth);
            activeEvents.Add(gameEvent);
        }

        private void FinishEvent(GameEvent gameEvent)
        {
            gameEvent.ActivatePickedChoice();
            activeEvents.Remove(gameEvent);
        }
    }
}
