using System;
using System.Collections.Generic;

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
            _rooms.Add(room);
        }
        public void AddReservation(Reservation reservation)
        {
            _reservations.Add(reservation);
        }

    // Hàm tìm kiếm các phòng trống trong khoảng thời gian check-in và check-out
        public List<Room> GetAvailableRooms(DateTime checkInDate, DateTime checkOutDate)
        {
            List<Room> availableRooms = new List<Room>();

            foreach (var room in Rooms)
            {
                bool isRoomAvailable = true;

                
                foreach (var res in Reservations)
                {
                    // Nếu đúng là phòng này VÀ khoảng thời gian bị trùng (gọi hàm IsOverlapping của bạn)
                    if (res.Room.RoomNumber == room.RoomNumber && res.IsOverlapping(checkInDate, checkOutDate))
                    {
                        isRoomAvailable = false; 
                        break; 
                    }
                }
                
                //Đúng thì thêm phòng vào danh sách phòng trống
                if (isRoomAvailable)
                {
                    availableRooms.Add(room);
                }
            }

            return availableRooms; 
        }
        
    }
}