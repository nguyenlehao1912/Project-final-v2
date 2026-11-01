using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Project_Final;

public class Program
{
    public static int Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        if(args.Length > 0 && args[0] == "test")
        {
            return TestRunner.Run();
        }
        HotelManager hotel = SeedHotel();
        bool running = true;

        while (running)
        {
            PrintMenu();
            switch (Console.ReadLine()?.Trim())
            {
                case "1": Run(() => SearchRooms(hotel)); break;
                case "2": Run(() => MakeReservation(hotel)); break;
                case "3": Run(() => CheckIn(hotel)); break;
                case "4": Run(() => CheckOut(hotel)); break;
                case "5": Run(() => AddServiceToReservation(hotel)); break;
                case "6": Run(() => CancelReservation(hotel)); break;
                case "7": ListReservations(hotel); break;
                case "0": running = false; break;
                default: Console.WriteLine("Invalid choice."); break;
            }
        }

        Console.WriteLine("Goodbye!");
        return 0;
    }

    // Bắt lỗi nghiệp vụ ở một chỗ duy nhất, chương trình không bị sập
    private static void Run(Action action)
    {
        try
        {
            action();
        }
        catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    private static void PrintMenu()
    {
        Console.WriteLine();
        Console.WriteLine("=== HOTEL RESERVATION SYSTEM ===");
        Console.WriteLine("1. Search available rooms");
        Console.WriteLine("2. Make a reservation");
        Console.WriteLine("3. Check-in");
        Console.WriteLine("4. Check-out (print invoice)");
        Console.WriteLine("5. Add service to a reservation");
        Console.WriteLine("6. Cancel a reservation");
        Console.WriteLine("7. List reservations");
        Console.WriteLine("0. Exit");
        Console.Write("Choose: ");
    }

    // ---------- Chức năng 1: tìm phòng trống ----------
    private static void SearchRooms(HotelManager hotel)
    {
        DateTime from = ReadDate("Check-in date (dd/MM/yyyy): ");
        DateTime to = ReadDate("Check-out date (dd/MM/yyyy): ");

        List<Room> rooms = hotel.GetAvailableRooms(from, to);
        if (rooms.Count == 0)
        {
            Console.WriteLine("No rooms available.");
            return;
        }
        PrintRooms(rooms);
    }

    private static void PrintRooms(IEnumerable<Room> rooms)
    {
        foreach (Room room in rooms)
        {
            string price = room.CalculateNightlyRate().ToString("N0", CultureInfo.InvariantCulture);
            Console.WriteLine($"Room {room.RoomNumber} - {room.Type} - {price} VND/night");
        }
    }

    // ---------- Chức năng 2: đặt phòng ----------
    private static void MakeReservation(HotelManager hotel)
    {
        Console.WriteLine("--- Guest information ---");
        string name = ReadText("Full name: ");
        int idNumber = ReadInt("ID number: ");
        string phone = ReadPhone("Phone: ");
        Guest guest = new Guest(name, idNumber, phone);

        DateTime checkIn = ReadDate("Check-in date (dd/MM/yyyy): ");
        DateTime checkOut = ReadDate("Check-out date (dd/MM/yyyy): ");

        List<Room> available = hotel.GetAvailableRooms(checkIn, checkOut);
        if (available.Count == 0)
        {
            Console.WriteLine("No rooms available for these dates.");
            return;
        }
        PrintRooms(available);

        string roomNumber = ReadText("Room number to book: ");
        int next = hotel.Reservations.Count + 1;
        string id = $"R{next:000}";

        Reservation reservation = hotel.MakeReservation(id, guest, roomNumber, checkIn, checkOut);
        Console.WriteLine($"Reservation {reservation.ReservationID} created. Please remember this ID.");

        Console.Write("Add services now? (y/n): ");
        if (Console.ReadLine()?.Trim().ToLower() == "y")
            ChooseServices(reservation);
    }

    // ---------- Chức năng 3: check-in ----------
    private static void CheckIn(HotelManager hotel)
    {
        string id = ReadReservationId();
        hotel.CheckIn(id);
        Console.WriteLine($"Reservation {id} has been checked in.");
    }

    // ---------- Chức năng 4: check-out, in hóa đơn ----------
    private static void CheckOut(HotelManager hotel)
    {
        string id = ReadReservationId();
        Invoice invoice = hotel.CheckOut(id);
        Console.WriteLine(invoice);
    }

    // ---------- Chức năng 5: thêm dịch vụ ----------
    private static void AddServiceToReservation(HotelManager hotel)
    {
        Reservation reservation = hotel.FindReservation(ReadReservationId());
        if (reservation == null)
        {
            Console.WriteLine("Reservation not found.");
            return;
        }
        ChooseServices(reservation);
    }

    private static void ChooseServices(Reservation reservation)
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("--- Add-on services ---");
            Console.WriteLine("1. Breakfast");
            Console.WriteLine("2. Laundry");
            Console.WriteLine("3. Hot Spring & Sauna");
            Console.WriteLine("4. Eco Park (free)");
            Console.WriteLine("0. Done");
            Console.Write("Choose: ");

            try
            {
                IAddOnService service;
                switch (Console.ReadLine()?.Trim())
                {
                    case "1": service = new BreakfastService(ReadInt("Number of persons: ")); break;
                    case "2": service = new LaundryService(); break;
                    case "3": service = new SpaService(ReadInt("Number of persons: ")); break;
                    case "4": service = new EcoParkService(); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid choice."); continue;
                }

                reservation.AddService(service);
                Console.WriteLine($"Added: {service.Name}");
            }
            catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
            {
                // Lỗi một dịch vụ không làm mất cả vòng chọn
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }

    // ---------- Chức năng 6: hủy ----------
    private static void CancelReservation(HotelManager hotel)
    {
        string id = ReadReservationId();
        hotel.Cancel(id);
        Console.WriteLine($"Reservation {id} has been cancelled.");
    }

    // ---------- Chức năng 7: xem danh sách ----------
    private static void ListReservations(HotelManager hotel)
    {
        if (hotel.Reservations.Count == 0)
        {
            Console.WriteLine("No reservations yet.");
            return;
        }
        foreach (Reservation r in hotel.Reservations)
        {
            Console.WriteLine($"{r.ReservationID} | {r.Guest.Name} | Room {r.Room.RoomNumber} | " +
                              $"{r.CheckInDate:dd/MM/yyyy} - {r.CheckOutDate:dd/MM/yyyy} | {r.Status}");
        }
    }

    // ---------- Hàm đọc dữ liệu có kiểm tra ----------
    private static DateTime ReadDate(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            if (DateTime.TryParseExact(Console.ReadLine()?.Trim(), "dd/MM/yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
                return date;
            Console.WriteLine("Invalid format, please use dd/MM/yyyy.");
        }
    }

    private static string ReadText(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine()?.Trim();
            if (!string.IsNullOrEmpty(input)) return input;
            Console.WriteLine("This field cannot be empty.");
        }
    }

    private static int ReadInt(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine()?.Trim(), out int value)) return value;
            Console.WriteLine("Please enter a whole number.");
        }
    }
    // Hỏi lại cho đến khi SĐT hợp lệ. Luật kiểm tra nằm trong Guest, không lặp lại ở đây.
    private static string ReadPhone(string prompt)
    {
        while (true)
        {
            string phone = ReadText(prompt);
            try
            {
                _ = new Guest("check", 0, phone);   
                return phone;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Error: " + ex.Message + ". Please re-enter.");
            }
        }
    }
    // Đổi sang chữ hoa để gõ "r001" vẫn tìm được "R001"
    private static string ReadReservationId()
    {
        return ReadText("Reservation ID: ").ToUpperInvariant();
    }

    // ---------- Dữ liệu mẫu để demo ----------
    private static HotelManager SeedHotel()
    {
        var manager = new HotelManager();
        manager.AddRoom(new StandardRoom("101", 500000m));
        manager.AddRoom(new StandardRoom("102", 500000m));
        manager.AddRoom(new VipRoom("201", 1200000m));
        manager.AddRoom(new VipRoom("202", 1200000m));

        // Một đặt phòng mẫu (R001) để thử ngay check-in / check-out
        var guest = new Guest("Nguyen Van A", 1, "0901234567");
        Reservation sample = manager.MakeReservation(
            "R001", guest, "201", DateTime.Today.AddDays(1), DateTime.Today.AddDays(3));
        sample.AddService(new BreakfastService(2));
        sample.AddService(new LaundryService());

        return manager;
    }
}