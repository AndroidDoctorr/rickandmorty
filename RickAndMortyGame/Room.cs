using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RickAndMortyGame.ProgramUI;

namespace RickAndMortyGame
{
    public class Room
    {
        public string Splash { get; }
        public List<string> Exits { get; }
        public List<Item> Items { get; }
        public List<Event> Events { get; }

        public Room(string splash, List<string> exits, List<Item> items, List<Event> events)
        {
            Splash = splash;
            Items = items;
            Exits = exits;
            Events = events;
        }
        // These methods are similar to the "repo pattern", which could also be called
        //    "list control":
        public void AddExit(string newExit)
        {
            Exits.Add(newExit);
        }

        public void RemoveItem(Item item)
        {
            if (Items.Contains(item))
            {
                Items.Remove(item);
            }
        }
        public void ResolveEvent(Event resolvedEvent)
        {
            if (Events.Contains(resolvedEvent))
            {
                Events.Remove(resolvedEvent);
            }
        }
    }
}
