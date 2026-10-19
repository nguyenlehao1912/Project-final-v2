using System;
using System.Collections.Generic;
using System.Linq;

namespace Project_Final
{

public class Guest
    {
        public string Name { get; private set;}
        public int ID { get; private set;}
        public string PhoneNumber { get; private set;}

        public Guest(string name, int id, string phone)
        {
            Name = name;
            ID = id;
            PhoneNumber = phone;
        }
    }
public abstract class Room
{
    public string RoomNumber { get; private set; }
    public decimal BasePrice { get; protected set; }
    private readonly List<string> _amenities;
    public IReadOnlyList<string> Amenities => _amenities.AsReadOnly();


    protected Room(string roomNumber, decimal basePrice, List<string> amenities)
    {
        if (string.IsNullOrWhiteSpace(roomNumber)) throw new ArgumentException("Room number is not empty");
        if (basePrice <= 0) throw new ArgumentException("Base price must be greater than 0");
        RoomNumber = roomNumber;
        BasePrice = basePrice;
        _amenities =  amenities.Distinct().ToList();
    }

    public abstract decimal CalculateNightlyRate();
    public decimal CalculateRoomCharge(DateTime checkIn, DateTime checkOut)
        {
            if (checkOut.Date <= checkIn.Date)
            {
                throw new ArgumentException("Ngay tra phong phai sau ngay nhan phong");
            }
            int nights = (checkOut.Date - checkIn.Date).Days;
            return CalculateNightlyRate() * nights;
        }
    public abstract string GetRoomType();
}
public class StandardRoom : Room
{
    public StandardRoom(string roomNumber, decimal basePrice, List<string> amenities = null)
        : base(roomNumber, basePrice, amenities ?? new List<string> { "WiFi", "TV", "Air Conditioner" }){}

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

    public VipRoom(string roomNumber, decimal basePrice, decimal vipServiceFeeRatio = 0.2m, List<string> amenities = null)
        : base(roomNumber, basePrice, amenities ?? new List<string> { "High-speed WiFi", "Smart TV 4K", "Air Conditioner", "Bathtub", "Mini Bar", "Ocean View" })
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