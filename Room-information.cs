using System;
using System.Collections.Generic;

namespace Project_Final
{

public class Guest
    {
        public string Name { get; private set;}
        public int ID { get; private set;}
        public string PhoneNumber { get; private set;}

        public Guest(string name, int id, string phone)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is not empty");
            if (string.IsNullOrWhiteSpace(phone)) throw new ArgumentException("Phone number is not empty");
            Name = name;
            ID = id;
            PhoneNumber = phone;
        }
    }
public abstract class Room
{
    public string RoomNumber { get; private set; }
    public decimal BasePrice { get; protected set; }
    public List<string> Amenities { get; private set; }

    public Room(string roomNumber, decimal basePrice, List<string> amenities = null)
    {
        RoomNumber = roomNumber;
        BasePrice = basePrice;
        Amenities = amenities ?? new List<string>();
    }

    public abstract decimal CalculateNightlyRate();
    public abstract string GetRoomType();
}

public class StandardRoom : Room
{
    public StandardRoom(string roomNumber, decimal basePrice, List<string> amenities = null)
        : base(roomNumber, basePrice, amenities ?? new List<string> { "WiFi", "TV", "Air Conditioner" })
    {
    }

    public override decimal CalculateNightlyRate()
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
    public decimal VipServiceFeeRatio { get; private set; }

    public VipRoom(string roomNumber, decimal basePrice, double vipServiceFeeRatio = 0.2, List<string> amenities = null)
        : base(roomNumber, basePrice, amenities ?? new List<string> { "High-speed WiFi", "Smart TV 4K", "Air Conditioner", "Air Conditioner", "Bathtub", "Mini Bar", "Ocean View" })
    {
        VipServiceFeeRatio = vipServiceFeeRatio;
    }

    public override decimal CalculateNightlyRate()
    {
        return BasePrice * (1 + VipServiceFeeRatio);
    }

    public override string GetRoomType()
    {
        return "VIP";
    }
}


}
