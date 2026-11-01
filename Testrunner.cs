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
        var guest = new Guest("Nguyen Van A", 1, "0901234567");
        var from = new DateTime(2026, 12, 1);
        var to = new DateTime(2026, 12, 3); // 2 đêm

        Console.WriteLine("--- 1. Happy path ---");
        var hotel = new HotelManager();
        var std = new StandardRoom("101", 500000m);
        var vip = new VipRoom("201", 1200000m);
        hotel.AddRoom(std);
        hotel.AddRoom(vip);

        var res = WithServices(new Reservation("R001", guest, std, from, to));
        hotel.AddReservation(res);
        Check("Đặt xong, phòng 101 không còn trống", hotel.GetAvailableRooms(from, to).Count == 1);
        res.CheckIn();
        res.CheckOut();
        Check("Hóa đơn Standard = 1,750,000", new BillingService().Generate(res).Total == 1750000m);

        Console.WriteLine("--- 2. Validation / error ---");
        Throws<ArgumentException>("Guest tên rỗng", () => new Guest("", 1, "090"));
        Throws<ArgumentException>("Check-out <= check-in", () => new Reservation("R", guest, std, to, from));
        Throws<InvalidOperationException>("Đặt trùng phòng", () =>
            hotel.AddReservation(new Reservation("R002", guest, std, from, to)));
        Throws<InvalidOperationException>("Check-out khi chưa check-in", () =>
            new Reservation("R3", guest, std, from, to).CheckOut());

        Console.WriteLine("--- 3. Polymorphism ---");
        List<Room> rooms = new() { std, vip };
        Check("Giá đêm: Standard 500,000 / VIP 1,440,000",
            rooms[0].CalculateNightlyRate() == 500000m && rooms[1].CalculateNightlyRate() == 1440000m);
        var vipBill = new BillingService().Generate(WithServices(new Reservation("R4", guest, vip, from, to)));
        Check("Cùng dịch vụ, phòng VIP = 2,880,000 (3 dịch vụ free)",
            vipBill.Total == 2880000m && vipBill.Lines.Count(l => l.Description.Contains("free for VIP")) == 3);

        Console.WriteLine(_fail == 0 ? "ALL PASSED" : $"{_fail} FAILED");
        return _fail == 0 ? 0 : 1;
    }

    private static Reservation WithServices(Reservation r)
    {
        r.AddAddOn(new BreakfastService(2));
        r.AddAddOn(new LaundryService());
        r.AddAddOn(new SpaService(2));
        r.AddAddOn(new EcoParkService());
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
}