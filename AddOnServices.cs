namespace Project_final;

// Hợp đồng: dịch vụ nào cũng phải có tên và tự tính được chi phí
public interface IAddOnService
{
    string Name { get; }
    decimal CalculateCost(int nights);
}

// Ăn sáng: giá mỗi người × số người × số đêm
public class BreakfastService : IAddOnService
{
    private readonly int _persons;
    private readonly decimal _pricePerPerson;

    public string Name => "Breakfast";

    public BreakfastService(int persons, decimal pricePerPerson = 100000m)
    {
        _persons = persons;
        _pricePerPerson = pricePerPerson;
    }

    public decimal CalculateCost(int nights)
    {
        return _pricePerPerson * _persons * nights;
    }
}

// Giặt ủi: phí cố định, không phụ thuộc số đêm
public class LaundryService : IAddOnService
{
    private readonly decimal _fee;

    public string Name => "Laundry";

    public LaundryService(decimal fee = 150000m)
    {
        _fee = fee;
    }

    public decimal CalculateCost(int nights)
    {
        return _fee;
    }
}

