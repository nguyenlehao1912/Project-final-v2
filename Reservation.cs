// File reservation.cs

using System;

namespace Project_final
{
    public enum ReservationStatus
    {
        Booked,
        CheckedIn,
        CheckedOut,
        Cancelled
    }

    public class Reservation
    {
        public string ReservationId { get; private set; }
        public Guest Guest { get; private set; } 
        public Room Room { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public ReservationStatus Status { get; private set; }
        public decimal TotalBill { get; private set; } // Sẽ được tính khi CheckOut

        public Reservation(string id, Guest guest, Room room, DateTime startDate, DateTime endDate)
        {
            ReservationId = id;
            Guest = guest;
            Room = room;
            StartDate = startDate;
            EndDate = endDate;
            Status = ReservationStatus.Booked; 
        }

        // Kiểm tra và thực hiện Check In
        public void CheckIn()
        {
            if (Status != ReservationStatus.Booked)
            {
                throw new InvalidOperationException("Lỗi: Chỉ có thể Check-in khi trạng thái đang là Booked.");
            }
            Status = ReservationStatus.CheckedIn;
            Console.WriteLine($"Reservation {ReservationId}: Đã Check-in thành công.");
        }

        // Kiểm tra và thực hiện Check Out, gọi hàm tính tiền của Lộc
        public void CheckOut()
        {
            if (Status != ReservationStatus.CheckedIn)
            {
                throw new InvalidOperationException("Lỗi: Không thể Check-out vì khách chưa Check-in.");
            }
            Status = ReservationStatus.CheckedOut;
            
            // TÍNH TIỀN: Hảo tính số đêm và gọi hàm CalculatePrice của class Room (do Lộc viết)
            int nights = (EndDate - StartDate).Days;
            TotalBill = Room.CalculatePrice(nights); 
            
            Console.WriteLine($"Reservation {ReservationId}: Đã Check-out. Tổng tiền: {TotalBill}");
        }

        public void Cancel()
        {
            if (Status != ReservationStatus.Booked)
            {
                 throw new InvalidOperationException("Lỗi: Chỉ có thể Cancel khi phòng chưa được Check-in.");
            }
            Status = ReservationStatus.Cancelled;
            Console.WriteLine($"Reservation {ReservationId}: Đã hủy đơn đặt phòng.");
        }

        // Thuật toán kiểm tra trùng lịch (Double-booking prevention)
        public bool IsOverlapping(DateTime newStartDate, DateTime newEndDate)
        {
            if (Status == ReservationStatus.Cancelled)
            {
                return false;
            }
            return (newStartDate < EndDate) && (newEndDate > StartDate);
        }
    }
}