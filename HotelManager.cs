using System;
using System.Collections.Generic;

namespace Project_Final
{
    public class HotelManager
    {
        // 1. Chứa danh sách tổng
        public List<Room> Rooms { get; private set; }
        public List<Reservation> Reservations { get; private set; }

        public HotelManager()
        {
            Rooms = new List<Room>();
            Reservations = new List<Reservation>();
        }

        // 2. Hàm TÌM KIẾM PHÒNG TRỐNG (Nhiệm vụ tuần 3 của bạn)
        public List<Room> GetAvailableRooms(DateTime checkInDate, DateTime checkOutDate)
        {
            List<Room> availableRooms = new List<Room>();

            // Duyệt qua từng phòng có trong khách sạn
            foreach (var room in Rooms)
            {
                bool isRoomAvailable = true;

                // Kiểm tra phòng này có vướng lịch đặt nào trong hệ thống không
                foreach (var res in Reservations)
                {
                    // Nếu đúng là phòng này VÀ khoảng thời gian bị trùng (gọi hàm IsOverlapping của bạn)
                    if (res.Room.RoomNumber == room.RoomNumber && res.IsOverlapping(checkInDate, checkOutDate))
                    {
                        isRoomAvailable = false; // Bị trùng rồi, đánh dấu là không trống
                        break; // Thoát khỏi vòng lặp kiểm tra reservation, chuyển sang phòng khác
                    }
                }

                // Nếu kiểm tra xong xuôi mà vẫn available thì thêm vào danh sách kết quả
                if (isRoomAvailable)
                {
                    availableRooms.Add(room);
                }
            }

            return availableRooms; // Trả về danh sách các phòng trống
        }
        
        // Bạn có thể viết thêm các hàm như AddRoom, AddReservation ở đây để Lộc gọi từ Program.cs
    }
}