using System;
using System.Collections.Generic;
using System.Linq;
namespace Project_Final
{
    public class HotelManager
    {
        //Tạo ra 2 danh sách gốc để lưu trữ các phòng và các đặt lịch
        private List<Room> _rooms;
        private List<Reservation> _reservations;

        //TẠo ra 2 danh sách chỉ đọc để truy cập các phòng và các đặt lịch + trưng bày dữ liệu
        public IReadOnlyList<Room> Rooms => _rooms;
        public IReadOnlyList<Reservation> Reservations => _reservations;
        public HotelManager()
        {
            _rooms = new List<Room>();
            _reservations = new List<Reservation>();
        }
        public void AddRoom(Room room)
        {
            if(room == null)
                throw new ArgumentNullException(nameof(room), "Room cannot be null.");
            if(_rooms.Exists(r => r.RoomNumber == room.RoomNumber))
                throw new ArgumentException($"Room with number {room.RoomNumber} already exists.");
            _rooms.Add(room);
        }


    // Hàm tìm kiếm các phòng trống trong khoảng thời gian check-in và check-out
        public List<Room> GetAvailableRooms(DateTime checkInDate, DateTime checkOutDate)
        {
            if (checkOutDate.Date <= checkInDate.Date)
                throw new ArgumentException("Check-out date must be after check-in date");

            return _rooms
                .Where(room => IsRoomAvailable(room, checkInDate.Date, checkOutDate.Date))
                .ToList();
        }

        // Kiểm tra một phòng có trống trong khoảng ngày hay không
        private bool IsRoomAvailable(Room room, DateTime checkIn, DateTime checkOut)
        {
            return !_reservations.Any (res =>res.Room.RoomNumber == room.RoomNumber && res.IsOverlapping(checkIn, checkOut));
        }

        // Tạo reservation: kiểm tra phòng tồn tại, ID chưa trùng, phòng còn trống
        public Reservation MakeReservation(string id, Guest guest, string roomNumber,DateTime checkIn, DateTime checkOut)
        {
            Room room = FindRoom(roomNumber)
                ?? throw new InvalidOperationException($"Room {roomNumber} does not exist.");

            if (FindReservation(id) != null)
                throw new InvalidOperationException($"Reservation ID {id} already exists.");

            // Constructor của Reservation tự validate ngày, guest, room
            Reservation reservation = new Reservation(id, guest, room, checkIn, checkOut);

            if (!IsRoomAvailable(room, reservation.CheckInDate, reservation.CheckOutDate))
                throw new InvalidOperationException(
                    $"Room {roomNumber} is not available for the selected dates.");

            _reservations.Add(reservation);
            return reservation;
        }

        public Room FindRoom(string roomNumber)
        {
            return _rooms.FirstOrDefault(r => r.RoomNumber == roomNumber);
        }

        public Reservation FindReservation(string id)
        {
            return _reservations.FirstOrDefault(r => r.ReservationID == id);
        }

        // Các hàm bên dưới chỉ tìm reservation rồi giao việc cho chính nó
        public void CheckIn(string id)
        {
            GetReservationOrThrow(id).CheckIn();
        }

        public Invoice CheckOut(string id)
        {
            return GetReservationOrThrow(id).CheckOut();
        }

        public void Cancel(string id)
        {
            GetReservationOrThrow(id).Cancel();
        }

        private Reservation GetReservationOrThrow(string id)
        {
            return FindReservation(id)
                ?? throw new InvalidOperationException($"Reservation {id} not found.");
        }
    }
}