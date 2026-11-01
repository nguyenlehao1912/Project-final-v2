using System;
using System.Collections.Generic;
using System.Linq;

namespace Project_Final;

// Hợp đồng: dịch vụ nào cũng phải có tên và tự tính được chi phí
public interface IAddOnService
{
    string Name { get; }
    decimal CalculateCost(int nights);
    bool FreeForVip => false;
}
 
// Ăn sáng: giá mỗi người × số người × số đêm, miễn phí cho phòng VIP
public class BreakfastService : IAddOnService
{
    private readonly int _persons;
    private readonly decimal _pricePerPerson;

    public string Name => $"Breakfast x {_persons} person(s)";
    public bool FreeForVip => true;  

    public BreakfastService(int persons, decimal pricePerPerson = 100000m)
    {
        if(persons <= 0)
            throw new ArgumentException("Number of persons must be greater than zero.");
        if(pricePerPerson < 0)
            throw new ArgumentException("Price per person must be a non-negative value.");
        _persons = persons;
        _pricePerPerson = pricePerPerson;
    }

    public decimal CalculateCost(int nights)
    {
        return _pricePerPerson * _persons * nights;
    }
}

// Giặt ủi: phí cố định, không phụ thuộc số đêm, miễn phí cho phòng VIP
public class LaundryService : IAddOnService
{
    private readonly decimal _fee;

    public string Name => "Laundry";
    public bool FreeForVip => true;  

    public LaundryService(decimal fee = 150000m)
    {
        if(fee < 0)
            throw new ArgumentException("Fee must be a non-negative value.");   
        _fee = fee;
    }

    public decimal CalculateCost(int nights)
    {
        return _fee;
    }
}

// Tắm suối nước nóng + xông hơi: gộp 1 gói, miễn phí cho phòng VIP
public class SpaService : IAddOnService
{
    private readonly int _persons;
    private readonly decimal _pricePerPerson;

    public string Name => $"Hot Spring & Sauna x {_persons} person(s)";
    public bool FreeForVip => true;

    public SpaService(int persons, decimal pricePerPerson = 100000m)
    {
        if(persons <= 0)
            throw new ArgumentException("Number of persons must be greater than zero.");
        if(pricePerPerson < 0)
            throw new ArgumentException("Price per person must be a non-negative value.");
        _persons = persons;
        _pricePerPerson = pricePerPerson;
    }

    public decimal CalculateCost(int nights) => _pricePerPerson * _persons;
}

// Khu vui chơi sinh thái: miễn phí cho mọi khách
public class EcoParkService : IAddOnService
{
    public string Name => "Eco Park (free)";

    public decimal CalculateCost(int nights) => 0m;
}