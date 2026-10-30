using System.Collections.Generic;
using System.Linq;

namespace Project_final;

// Một dòng trong hóa đơn
public record InvoiceLine(string Description, decimal Amount);

// Hóa đơn: chỉ chứa dữ liệu và tự cộng tổng
public class Invoice
{
    public List<InvoiceLine> Lines { get; } = new();
    public decimal Total => Lines.Sum(l => l.Amount);
}

// Tính hóa đơn cho một Reservation
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

        // TODO: add-on fees and VIP exemption

        return invoice;
    }
}