<<<<<<< HEAD
=======
﻿using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Project_final;

public class Program
{
    public static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        Hotel hotel = SeedHotel();
        bool running = true;

        while (running)
        {
            PrintMenu();
            switch (Console.ReadLine()?.Trim())
            {
                case "1": SearchRooms(hotel); break;
                case "2":
                case "3":
                case "4":
                    Console.WriteLine("Not implemented yet."); break;
                case "0": running = false; break;
                default: Console.WriteLine("Invalid choice."); break;
            }
        }

        Console.WriteLine("Goodbye!");
    }

    private static void PrintMenu()
    {
        Console.WriteLine();
        Console.WriteLine("=== HOTEL RESERVATION SYSTEM ===");
        Console.WriteLine("1. Search available rooms");
        Console.WriteLine("2. Make a reservation");
        Console.WriteLine("3. Check-in");
        Console.WriteLine("4. Check-out (print invoice)");
        Console.WriteLine("0. Exit");
        Console.Write("Choose: ");
    }

    private static void SearchRooms(Hotel hotel)
    {
        DateTime from = ReadDate("Check-in date (dd/MM/yyyy): ");
        DateTime to = ReadDate("Check-out date (dd/MM/yyyy): ");

        try
        {
            var rooms = hotel.FindAvailableRooms(from, to, null).ToList();
            if (rooms.Count == 0)
            {
                Console.WriteLine("No rooms available.");
                return;
            }
            foreach (Room room in rooms)
                Console.WriteLine($"  Room {room.Number} - {room.Type.Name} - {room.Type.GetNightlyRate():N0} VND/night");
        }
        catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

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

    // Dữ liệu mẫu để demo
    private static Hotel SeedHotel()
    {
        var hotel = new Hotel();
        hotel.Rooms.Add(new Room(101, new StandardRoomType()));
        hotel.Rooms.Add(new Room(102, new StandardRoomType()));
        hotel.Rooms.Add(new Room(201, new VipRoomType()));
        hotel.Rooms.Add(new Room(202, new VipRoomType()));
        return hotel;
    }
}
>>>>>>> a857f5f (AI sinh code thêm vòng lặp menu console và chức năng tìm phòng trống trong main)
