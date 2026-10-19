using System;
using System.Collections.Generic;

namespace Project_Final
{
public abstract class Room
{
    public string RoomNumber { get; private set; }
    public double BasePrice { get; protected set; }
    public List<string> Amenities { get; private set; }

    public Room(string roomNumber, double basePrice, List<string> amenities = null)
    {
        RoomNumber = roomNumber;
        BasePrice = basePrice;
        Amenities = amenities ?? new List<string>();
    }

    public abstract double CalculateNightlyRate();
    public abstract string GetRoomType();
}
public class StandardRoom : Room
{
    public StandardRoom(string roomNumber, double basePrice, List<string> amenities = null)
        : base(roomNumber, basePrice, amenities ?? new List<string> { "WiFi", "TV", "Air Conditioner" }){}

    public override double CalculateNightlyRate()
    {
        return BasePrice;
    }

    public override string GetRoomType()
    {
        return "Standard";
    }
}

public class VipRoom : Room
{
    public double VipServiceFeeRatio { get; private set; }

    public VipRoom(string roomNumber, decimal basePrice, double vipServiceFeeRatio = 0.2m, List<string> amenities = null)
        : base(roomNumber, basePrice, amenities ?? new List<string> { "High-speed WiFi", "Smart TV 4K", "Air Conditioner", "Bathtub", "Mini Bar", "Ocean View" })
    {
        VipServiceFeeRatio = vipServiceFeeRatio;
    }

    public override double CalculateNightlyRate()
    {
        return BasePrice * (1 + VipServiceFeeRatio);
    }

    public override string GetRoomType()
    {
        return "VIP";
    }
}
}