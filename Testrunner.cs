using System;
using System.Collections.Generic;
using System.Linq;

namespace Project_Final;

// Chạy bằng: dotnet run -- test
public static class TestRunner
{
    private static int _fail;

    public static int Run()
    {
        _fail = 0;
        var guest = new Guest("Nguyen Van A", 1, "0901234567");
        var from = new DateTime(2026, 12, 1);
        var to = new DateTime(2026, 12, 3); // 2 đêm

        var hotel = new HotelManager();
        var std = new StandardRoom("101", 500000m);
        var vip = new VipRoom("201", 1200000m);
        hotel.AddRoom(std);
        hotel.AddRoom(new StandardRoom("102", 500000m));
        hotel.AddRoom(vip);

        Console.WriteLine("--- 1. Happy path ---");
        Reservation res = hotel.MakeReservation("R001", guest, "101", from, to);
        WithServices(res);
        Check("Đặt xong, phòng 101 không còn trống", hotel.GetAvailableRooms(from, to).Count == 2);
        hotel.CheckIn("R001");
        Check("Sau CheckIn trạng thái là CheckedIn", res.Status == ReservationStatus.CheckedIn);
        Invoice invoice = hotel.CheckOut("R001");
        Check("Sau CheckOut trạng thái là CheckedOut", res.Status == ReservationStatus.CheckedOut);
        Check("Hóa đơn Standard = 1,750,000", invoice.Total == 1750000m && res.TotalBill == 1750000m);

        Console.WriteLine("--- 2. Validation / error ---");
        Throws<ArgumentException>("Guest tên rỗng", () => new Guest("", 1, "090"));
        Throws<ArgumentException>("Check-out <= check-in", () => new Reservation("R", guest, std, to, from));
        Throws<InvalidOperationException>("Đặt trùng phòng 101", () =>
            hotel.MakeReservation("R002", guest, "101", from, to));
        Throws<InvalidOperationException>("Đặt phòng không tồn tại", () =>
            hotel.MakeReservation("R002", guest, "999", from, to));

        hotel.MakeReservation("R003", guest, "102", from, to);
        Throws<InvalidOperationException>("CheckOut khi chưa CheckIn", () => hotel.CheckOut("R003"));
        hotel.CheckIn("R003");
        Throws<InvalidOperationException>("CheckIn lần hai", () => hotel.CheckIn("R003"));
        Throws<InvalidOperationException>("Cancel khi đã CheckIn", () => hotel.Cancel("R003"));

        hotel.MakeReservation("R004", guest, "201", from, to);
        NoThrow("Cancel đơn Booked", () => hotel.Cancel("R004"));
        Check("Đơn đã hủy thì phòng 201 trống lại", hotel.GetAvailableRooms(from, to).Count == 1);

        Console.WriteLine("--- 3. Polymorphism ---");
        List<Room> rooms = new() { std, vip };
        Check("Giá đêm: Standard 500,000 / VIP 1,440,000",
            rooms[0].CalculateNightlyRate() == 500000m && rooms[1].CalculateNightlyRate() == 1440000m);
        var vipBill = new BillingService().Generate(WithServices(new Reservation("R9", guest, vip, from, to)));
        Check("Cùng dịch vụ, phòng VIP = 2,880,000 (3 dịch vụ free)",
            vipBill.Total == 2880000m && vipBill.Lines.Count(l => l.Description.Contains("free for VIP")) == 3);

        Console.WriteLine(_fail == 0 ? "ALL PASSED" : $"{_fail} FAILED");
        return _fail == 0 ? 0 : 1;
    }

    private static Reservation WithServices(Reservation r)
    {
        r.AddService(new BreakfastService(2));
        r.AddService(new LaundryService());
        r.AddService(new SpaService(2));
        r.AddService(new EcoParkService());
        return r;
    }

    private static void Check(string name, bool ok)
    {
        if (!ok) _fail++;
        Console.WriteLine($"  [{(ok ? "PASS" : "FAIL")}] {name}");
    }

    private static void Throws<T>(string name, Action action) where T : Exception
    {
        try { action(); Check(name, false); }
        catch (T) { Check(name, true); }
        catch (Exception) { Check(name, false); }
    }

    private static void NoThrow(string name, Action action)
    {
        try { action(); Check(name, true); }
        catch (Exception) { Check(name, false); }
    }
}