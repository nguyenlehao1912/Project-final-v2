using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Project_final;

// Một dòng trong hóa đơn
public record InvoiceLine(string Description, decimal Amount);

// Hóa đơn: chỉ chứa dữ liệu và tự cộng tổng
public class Invoice
{
    public List<InvoiceLine> Lines { get; } = new();
    public decimal Total => Lines.Sum(l => l.Amount);

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine("============ INVOICE ============");
        foreach (var line in Lines)
            sb.AppendLine($"{line.Description,-40} {line.Amount.ToString("N0", CultureInfo.InvariantCulture),14} VND");
        sb.AppendLine("-------------------------------------------------------------");
        sb.AppendLine($"{"TOTAL",-40} {Total.ToString("N0", CultureInfo.InvariantCulture),14} VND");
        return sb.ToString();
    }
}

// Tính hóa đơn cho một Reservation.
// Tách riêng khỏi Reservation để tránh God class.
// Thêm dịch vụ mới = thêm 1 class implement IAddOnService, class này không phải sửa.
public class BillingService
{
    public Invoice Generate(Reservation r)
    {
        var invoice = new Invoice();
        int nights = r.Nights;
        RoomType type = r.Room.Type;

        // Tiền phòng: mỗi loại phòng tự quyết giá (đa hình qua GetNightlyRate)
        invoice.Lines.Add(new InvoiceLine(
            $"Room {r.Room.Number} ({type.Name}) x {nights} night(s)",
            type.GetNightlyRate() * nights));

        // Phụ phí dịch vụ: VIP được miễn nếu dịch vụ cho phép                                                                                          
        
        foreach (IAddOnService service in r.AddOns)
        {
            bool free = type.IsVip && service.FreeForVip;
            decimal cost = free ? 0m : service.CalculateCost(nights);
            string label = free ? $"{service.Name} (free for VIP)" : service.Name;
            invoice.Lines.Add(new InvoiceLine(label, cost));
        }

        return invoice;
    }
}