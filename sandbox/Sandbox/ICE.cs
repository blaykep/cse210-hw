using System;
using System.Collections.Generic;

abstract class SmartDevice
{
    public string Name { get; }
    public bool IsOn { get; protected set; }
    private DateTime StartTime;

    public SmartDevice(string name)
    {
        Name = name;
        IsOn = false;
        StartTime = DateTime.MinValue;
    }

    public void TurnOn()
    {
        if (!IsOn)
        {
            IsOn = true;
            StartTime = DateTime.Now;
        }
    }

    public void TurnOff()
    {
        if (IsOn)
            IsOn = false;
    }

    public string Status()
    {
        return IsOn ? "ON" : "OFF";
    }

    public string Uptime()
    {
        if (StartTime != DateTime.MinValue)
            return (DateTime.Now - StartTime).ToString();
        return "N/A";
    }

    public abstract override string ToString();
}

class SmartLight : SmartDevice
{
    public SmartLight(string name) : base(name) { }

    public override string ToString()
    {
        return $"Smart Light '{Name}' is {Status()} for {Uptime()}";
    }
}

class SmartHeater : SmartDevice
{
    public SmartHeater(string name) : base(name) { }

    public override string ToString()
    {
        return $"Smart Heater '{Name}' is {Status()} for {Uptime()}";
    }
}

class SmartTV : SmartDevice
{
    public SmartTV(string name) : base(name) { }

    public override string ToString()
    {
        return $"Smart TV '{Name}' is {Status()} for {Uptime()}";
    }
}

class Room
{
    public string Name { get; }
    private List<SmartDevice> devices;

    public Room(string name)
    {
        Name = name;
        devices = new List<SmartDevice>();
    }

    public void AddDevice(SmartDevice device)
    {
        devices.Add(device);
    }

    public void TurnOnAllDevices()
    {
        foreach (SmartDevice device in devices)
            device.TurnOn();
    }

    public void TurnOffAllDevices()
    {
        foreach (SmartDevice device in devices)
            device.TurnOff();
    }

    public string ReportAllItems()
    {
        string report = $"Items in {Name}:\n";
        foreach (SmartDevice device in devices)
            report += device.ToString() + "\n";
        return report;
    }

    public string ReportAllItemsOn()
    {
        string report = $"Items ON in {Name}:\n";
        foreach (SmartDevice device in devices)
        {
            if (device.IsOn)
                report += device.ToString() + "\n";
        }
        return report;
    }

    public string ReportLongestOnItem()
    {
        TimeSpan longestUptime = TimeSpan.MinValue;
        SmartDevice longestDevice = null;

        foreach (SmartDevice device in devices)
        {
            if (device.IsOn && (DateTime.Now - device.StartTime) > longestUptime)
            {
                longestUptime = DateTime.Now - device.StartTime;
                longestDevice = device;
            }
        }

        if (longestDevice != null)
            return $"Item with longest uptime in {Name} is '{longestDevice.Name}' for {longestUptime}";
        else
            return $"No item is currently on in {Name}";
    }
}

class House
{
    private List<Room> rooms;

    public House()
    {
        rooms = new List<Room>();
    }

    public void AddRoom(Room room)
    {
        rooms.Add(room);
    }

    public void TurnOnAllLights()
    {
        foreach (Room room in rooms)
        {
            foreach (SmartDevice device in room.devices)
            {
                if (device is SmartLight)
                    device.TurnOn();
            }
        }
    }

    public void TurnOffAllLights()
    {
        foreach (Room room in rooms)
        {
            foreach (SmartDevice device in room.devices)
            {
                if (device is SmartLight)
                    device.TurnOff();
            }
        }
    }

    public string ReportAllRooms()
    {
        string report = "House rooms and their items:\n";
        foreach (Room room in rooms)
        {
            report += $"{room.Name}:\n";
            report += room.ReportAllItems();
        }
        return report;
    }
}

class aProgram
{
    static void Main(string[] args)
    {
        SmartLight livingRoomLight = new SmartLight("Living Room Light");
        SmartHeater livingRoomHeater = new SmartHeater("Living Room Heater");
        SmartTV livingRoomTV = new SmartTV("Living Room TV");

        Room livingRoom = new Room("Living Room");
        livingRoom.AddDevice(livingRoomLight);
        livingRoom.AddDevice(livingRoomHeater);
        livingRoom.AddDevice(livingRoomTV);

        SmartLight bedroomLight = new SmartLight("Bedroom Light");
        SmartHeater bedroomHeater = new SmartHeater("Bedroom Heater");

        Room bedroom = new Room("Bedroom");
        bedroom.AddDevice(bedroomLight);
        bedroom.AddDevice(bedroomHeater);

        House myHouse = new House();
        myHouse.AddRoom(livingRoom);
        myHouse.AddRoom(bedroom);

        myHouse.TurnOnAllLights();

        Console.WriteLine(myHouse.ReportAllRooms());
        Console.WriteLine(livingRoom.ReportAllItemsOn());
        Console.WriteLine(livingRoom.ReportLongestOnItem());
    }
}
