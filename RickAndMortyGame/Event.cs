using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RickAndMortyGame.Event;
using static RickAndMortyGame.ProgramUI;

namespace RickAndMortyGame
{
    public class Event
    {
        public enum EventType { newExit, getItem }
        public string useTrigger;
        public EventType Type;
        public Result EventResult;

        public Event(EventType eventType, string trigger, Result eventResult)
        {
            Type = eventType;
            useTrigger = trigger;
            EventResult = eventResult;
        }
    }

    public class Result
    {
        public EventType Type { get; }
        public string ResultDestination { get; }
        public Item ResultItem { get; }
        public string ResultMessage { get; }

        public Result(string resultDestination, string resultMessage)
        {
            Type = EventType.newExit;
            ResultDestination = resultDestination;
            ResultMessage = resultMessage;
        }
        public Result(Item resultItem, string resultMessage)
        {
            Type = EventType.newExit;
            ResultItem = resultItem;
            ResultMessage = resultMessage;
        }
    }
}
