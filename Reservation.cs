using System;
namespace Project_Final
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
        public string ReservationID { get; private set; }
        public Guest Guest { get; private set; }
        public Room Room { get; private set; }
        public DateTime CheckInDate { get; private set; }
        public DateTime CheckOutDate { get; private set; }
        public ReservationStatus Status { get; private set; }
        public decimal TotalBill { get; private set; }

        public Reservation(string id, Guest guest, Room room, DateTime checkInDate, DateTime checkOutDate)
        {
            ReservationID = id;
            Guest = guest;
            Room = room;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            Status = ReservationStatus.Booked;
        }
        public void CheckIn() 
        {
            if (Status != ReservationStatus.Booked)
                throw new InvalidOperationException("Cannot check in. Reservation is not booked.");
            Status = ReservationStatus.CheckedIn;
            Console.WriteLine($"Reservation {ReservationID} has successfully been checked in.");
        }
        public void CheckOut()
        {
            if (Status != ReservationStatus.CheckedIn)
                throw new InvalidOperationException("Cannot check out. Reservation is not checked in.");
            Status = ReservationStatus.CheckedOut;
            int nights = (CheckOutDate - CheckInDate).Days;
            TotalBill = Room.CalculateNightlyRate() * nights;
            Console.WriteLine($"Reservation {ReservationID} has successfully been checked out. Total bill: {TotalBill:C}");
        }
        public void Cancel()
        {
            if (Status == ReservationStatus.CheckedIn)
                throw new InvalidOperationException("Cannot cancel. Reservation is already checked in.");
            Status = ReservationStatus.Cancelled;
            Console.WriteLine($"Reservation {ReservationID} has been cancelled.");
        }
        public bool IsOverlapping(DateTime newCheckIn, DateTime newCheckOut)
        {
            if(Status == ReservationStatus.Cancelled)
                return false; 
            return newCheckIn < CheckOutDate && newCheckOut > CheckInDate;
        }
    }
}
