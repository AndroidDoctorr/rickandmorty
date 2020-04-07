using RickAndMortyGame.Mortys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RickAndMortyGame.Event;

namespace RickAndMortyGame
{
    public class ProgramUI
    {
        public enum Item { plumbus, meeseeks, portalgun, beer, tinyRick, blimBlam };
        public List<Item> inventory = new List<Item>();

        public static Room garage = new Room(
            Texts.Splashes["garage"],
            new List<string> { "driveway", "house" },
            new List<Item> { Item.meeseeks, Item.plumbus },
            new List<Event> {
                new Event(
                    EventType.newExit,
                    "secret scanner",
                    new Result("laboratory", Texts.EventMessages["openLab"])
                )
            }
        );
        public static Room driveway = new Room(
            Texts.Splashes["driveway"],
            new List<string> { "garage" },
            new List<Item> { },
            new List<Event> { }
        );
        public static Room house = new Room(
            Texts.Splashes["house"],
            new List<string> { "garage" },
            new List<Item> { Item.portalgun, Item.beer },
            new List<Event> { }
        );
        public static Room upstairs = new Room(
            Texts.Splashes["upstairs"],
            new List<string> { "house", "attic", "mortysRoom" },
            new List<Item> { },
            new List<Event> { }
        );
        public static Room attic = new Room(
            Texts.Splashes["attic"],
            new List<string> { "upstairs" },
            new List<Item> { },
            new List<Event> { }
        );
        public static Room mortysRoom = new Room(
            Texts.Splashes["mortysRoom"],
            new List<string> { "upstairs" },
            new List<Item> { },
            new List<Event> { }
        );
        public static Room laboratory = new Room(
            Texts.Splashes["laboratory"],
            new List<string> { "garage" },
            new List<Item> { Item.tinyRick, Item.blimBlam },
            new List<Event> { }
        );

        public Dictionary<string, Room> RoomDictionary = new Dictionary<string, Room>
        {
            { "garage", garage },
            { "driveway", driveway },
            { "house", house },
            { "upstairs", upstairs },
            { "attic", attic },
            { "mortysRoom", mortysRoom },
            { "laboratory", laboratory }
        };

        public void Run()
        {
            Room room = garage;
            bool alive = true;
            Console.Clear();
            Console.WriteLine("You accidentally killed Morty.\n" +
                    "In order to construct a passable facsimile, you must collect " +
                    "enough Mortys from other dimensions to assemble from them " +
                    "Morty's genetic structure and connectome.");
            while (alive)
            {
                Console.WriteLine(room.Splash);
                string command = Console.ReadLine().ToLower();
                // I clear the console before parsing the command, because
                //   I want the response at the top of the screen.
                // I also don't want to clear the screen until after the
                //   Console.ReadLine statement, so the user can still see the text
                Console.Clear();
                if (command.StartsWith("go ") || command.StartsWith("exit"))
                {
                    // Here is some pseudocode to describe what's happening here:

                    // Loop through the available exits
                    //   If the room has an exit that matches the command
                    //     Then change the current room to that room
                    // (after the loop)
                    // If no valid room name can be found in their command
                    //   Then say "Go where??"
                    // (that last part is what the "foundExit" variable is for)

                    bool foundExit = false;
                    foreach (string exit in room.Exits)
                    {
                        if (!foundExit &&
                            command.Contains(exit) &&
                            RoomDictionary.ContainsKey(exit))
                        {
                            room = RoomDictionary[exit];
                            foundExit = true;
                            break;
                        }
                    }
                    if (!foundExit)
                    {
                        Console.WriteLine("Uh... Go where?");
                    }
                }
                else if (command.StartsWith("get ") || command.StartsWith("take ") || command.StartsWith("grab "))
                {
                    // I use similar logic here
                    bool foundItem = false;
                    foreach (Item item in room.Items)
                    {
                        if (!foundItem && command.Contains(item.ToString()))
                        {
                            Random rand = new Random();
                            int flavorTextChoice = rand.Next(0, 3);
                            string flavorText;
                            switch (flavorTextChoice)
                            {
                                case 0:
                                    flavorText = ". Don't break it.";
                                    break;
                                case 1:
                                    flavorText = ". Good for you.";
                                    break;
                                case 2:
                                default:
                                    flavorText = ". Fantastic.";
                                    break;
                            }
                            Console.WriteLine("You found a(n) " + item.ToString() + flavorText);
                            room.RemoveItem(item);
                            inventory.Add(item);
                            foundItem = true;
                            break;
                        }
                    }
                    if (!foundItem)
                    {
                        Console.WriteLine("I don't know what you're talking about.");
                    }
                }
                else if (command.StartsWith("look ") || command.StartsWith("check "))
                {
                    // I haven't used this yet. Maybe the player can examine things in
                    //   the room to unlock secrets. I don't know yet...
                    Console.WriteLine("It looks fine.");
                }
                else if (command.StartsWith("use ") || command.StartsWith("activate "))
                {
                    bool eventTriggered = false;
                    string eventMessage = "(Error: Event message not found)";
                    if (room.Events.Count > 0)
                    {
                        foreach(Event roomEvent in room.Events)
                        {
                            if (!command.Contains(roomEvent.useTrigger))
                            {
                                continue;
                            }
                            if (roomEvent.Type == EventType.newExit)
                            {
                                room.AddExit(roomEvent.EventResult.ResultDestination);
                                eventTriggered = true;
                                eventMessage = roomEvent.EventResult.ResultMessage;
                            } else
                            {
                                inventory.Add(roomEvent.EventResult.ResultItem);
                                eventTriggered = true;
                                eventMessage = roomEvent.EventResult.ResultMessage;
                            }
                        }
                    }
                    if (!eventTriggered)
                    {
                        Console.WriteLine("I doubt you know how.");
                    } else
                    {
                        Console.WriteLine(eventMessage);
                    }
                }
                else
                {
                    Console.WriteLine("*BUUUUURP* What?");
                }
            }
        }
    }
}
