using System;
using System.Collections.Generic;
using System.Linq;

public class Event
{
    public int EventId { get; set; }
    public int Timestamp { get; set; }
    public int NodeId { get; set; }

    public Event(int eventId, int timestamp, int nodeId)
    {
        EventId = eventId;
        Timestamp = timestamp;
        NodeId = nodeId;
    }

    public override string ToString()
    {
        return $"Event {EventId} from Node {NodeId} at {Timestamp}";
    }
}

