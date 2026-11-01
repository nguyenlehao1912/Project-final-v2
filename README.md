# 🏨 Hotel Reservation System - Đồ án Lập trình Hướng đối tượng (OOP)

Hệ thống Quản lý Đặt phòng Khách sạn được xây dựng bằng ngôn ngữ **C# (.NET)** chạy trên môi trường Console. Đồ án tập trung vào việc áp dụng và thể hiện rõ nét 4 nguyên lý cốt lõi của Lập trình Hướng đối tượng (OOP): Đóng gói, Trừu tượng, Kế thừa và Đa hình.

## 👥 Thành viên nhóm
1. **Nguyễn Lê Hảo** (MSSV: 25110019)
2. **Bùi Tấn Lộc** (MSSV: 25110037)

---

## 🚀 Các tính năng chính (Features)

*   **Quản lý Phòng (Room Management):** Hỗ trợ nhiều loại phòng (Standard, VIP) với công thức tính giá và tiện ích (Amenities) khác nhau.
*   **Quản lý Đặt phòng (Reservation State Machine):** Quản lý trạng thái vòng đời của một đơn đặt phòng chặt chẽ: `Booked` ➡️ `CheckedIn` ➡️ `CheckedOut` hoặc `Cancelled`. Hệ thống tự động chặn các thao tác sai luồng trạng thái.
*   **Chống trùng lịch (Overlapping Prevention):** Thuật toán tự động lọc và tìm kiếm phòng trống dựa trên khoảng thời gian Check-in và Check-out mà khách yêu cầu.
*   **Dịch vụ đi kèm (Add-on Services):** Cho phép thêm các dịch vụ như Ăn sáng, Giặt ủi, Spa. Tự động miễn phí một số dịch vụ cao cấp nếu khách đặt phòng VIP (áp dụng Đa hình).
*   **Hóa đơn tự động (Billing System):** Tự động tính toán tổng chi phí (tiền phòng + tiền dịch vụ) và xuất hóa đơn chi tiết khi khách Check-out.

---

## 💻 Kiến trúc và Thiết kế OOP

Dự án tuân thủ nghiêm ngặt nguyên lý **SOLID** (đặc biệt là Single Responsibility và Open/Closed) và 4 nguyên lý OOP:
*   **Đóng gói (Encapsulation):** Bảo vệ dữ liệu danh sách phòng và đơn đặt phòng bằng kỹ thuật Backing Field (`IReadOnlyList`), chỉ cho phép cập nhật thông qua các phương thức được kiểm soát trong `HotelManager`.
*   **Trừu tượng (Abstraction):** Ẩn chi tiết triển khai thông qua `abstract class Room` và `interface IAddOnService`.
*   **Kế thừa (Inheritance):** Các lớp `StandardRoom` và `VipRoom` kế thừa thuộc tính và phương thức từ lớp cha `Room`, tái sử dụng tối đa mã nguồn.
*   **Đa hình (Polymorphism):** Cùng một lệnh gọi tính tiền `CalculateNightlyRate()` hoặc `CalculateCost()`, hệ thống tự động đa hình ra các công thức tính toán khác nhau tùy thuộc vào đối tượng thực tế (Phòng thường hay VIP).

---

## ⚙️ Hướng dẫn cài đặt và Chạy chương trình

### Yêu cầu hệ thống:
*   Cài đặt **.NET SDK** (phiên bản 6.0 trở lên).
*   Công cụ lập trình (Visual Studio Code, Visual Studio 2022,...).

### Các bước chạy:
1. **Clone repository về máy:**
   ```bash
   git clone https://github.com/nguyenlehao1912/Project-final-v2.git
   cd Project-final-v2
   ```

2. **Khởi chạy ứng dụng (Console Menu):**
   ```bash
   dotnet run
   ```
   *Hệ thống sẽ tự động nạp dữ liệu mẫu (Seed Data) với 4 phòng (2 Standard, 2 VIP) và 1 đơn đặt phòng mẫu (R001) để bạn trải nghiệm ngay lập tức.*

---

## 🧪 Chạy bộ Kiểm thử tự động (Unit Testing)

Dự án được tích hợp sẵn một bộ kiểm thử (`TestRunner.cs`) tự động chạy qua 3 kịch bản: **Happy path** (Luồng chuẩn), **Validation / Error** (Bắt lỗi nghiệp vụ) và **Polymorphism** (Kiểm tra tính Đa hình).

Để chạy bộ Test, bạn gõ lệnh sau vào Terminal:
```bash
dotnet run -- test
```
*Nếu tất cả các test case đều in ra chữ `[PASS]`, hệ thống đang hoạt động hoàn hảo!*