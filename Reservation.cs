using System;
using System.Collections.Generic;
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
        private readonly List<IAddOnService> _addOns = new List<IAddOnService>();
        public IReadOnlyList<IAddOnService> AddOns => _addOns;
        public Reservation(string id, Guest guest, Room room, DateTime checkInDate, DateTime checkOutDate)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException("Reservation ID is not empty");
            if (guest == null) throw new ArgumentNullException(nameof(guest));
            if (room == null) throw new ArgumentNullException(nameof(room));
            if (checkOutDate.Date <= checkInDate.Date)
                throw new ArgumentException("Check-out date must be after check-in date");
            ReservationID = id;
            Guest = guest;
            Room = room;
            CheckInDate = checkInDate.Date;
            CheckOutDate = checkOutDate.Date;
            Status = ReservationStatus.Booked;
        }
         public void AddService(IAddOnService service)
        {
            if (service == null)
                throw new ArgumentNullException(nameof(service), "Service cannot be null.");
            if (Status == ReservationStatus.Cancelled || Status == ReservationStatus.CheckedOut)
                throw new InvalidOperationException("Cannot add service. Reservation is either cancelled or checked out.");
            _addOns.Add(service);
            Console.WriteLine($"Service {service.Name} has been added to reservation {ReservationID}.");
        }
        public void CheckIn() 
        {
            if (Status != ReservationStatus.Booked)
                throw new InvalidOperationException("Cannot check in. Reservation is not booked.");
            Status = ReservationStatus.CheckedIn;
            Console.WriteLine($"Reservation {ReservationID} has successfully been checked in.");
        }
        public Invoice CheckOut()
        {
            if (Status != ReservationStatus.CheckedIn)
                throw new InvalidOperationException("Cannot check out. Reservation is not checked in.");
            Invoice invoice = new BiliingService().Generate(this);
            TotalBill = invoice.Total;
            Status = ReservationStatus.CheckedOut;
            return invoice;
        }
        public void Cancel()
        {
            if (Status == ReservationStatus.Booked)
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
