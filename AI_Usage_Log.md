# AI Usage Log: Hotel Reservation System
## Tuần 1: chuẩn bị báo cáo
**Công cụ AI:** Claude (Anthropic) | **Thành viên:** Bùi Tấn Lộc (25110037)
### 1.1. Hỏi: Class `Room` cần gồm những gì?

**AI trả lời:** Khung `Room` làm khá ổn. `Room` nên là abstract class có số phòng, giá cơ bản và danh sách tiện ích, kèm hai phương thức abstract `CalculateNightlyRate()` và `GetRoomType()`. Hai lớp con `StandardRoom` và `VipRoom` kế thừa và tự quyết định cách tính giá, nhờ vậy các class khác không cần biết phòng thuộc loại nào. Nên đổi `double` sang `decimal` cho tiền, và dùng `enum RoomType` thay cho chuỗi để dễ tìm phòng theo loại.

**Nhóm làm:** Viết khung `Room`, `StandardRoom`, `VipRoom`.

### 1.2. Hỏi: Cần thêm gì nữa không, tiện ích của phòng VIP

**AI trả lời:** Phòng VIP nên có danh sách tiện ích cao cấp hơn phòng Standard (WiFi tốc độ cao, Smart TV, bồn tắm, minibar, view biển), và không nên để trùng lặp tiện ích. Ngoài `Room`, hệ thống cần thêm `Guest`, `DateRange` (khoảng ngày ở), `Reservation`, `HotelService` và các exception cho lỗi nghiệp vụ. Mỗi class chỉ làm một việc để tránh God class.

**Nhóm làm:** Thêm tiện ích cho phòng VIP, viết `Guest`.

-----------
**Công cụ AI:** Gemini (3.1 Pro) | **Thành viên:** Nguyễn Lê Hảo (25110019)
### 1.1. Hỏi: Phân tích yêu cầu đồ án OOP nhóm 2 người và gợi ý chia việc?

**AI trả lời:** Cần tập trung thể hiện 3 nguyên lý OOP (Đóng gói, Đa hình, Trừu tượng). Gợi ý chia việc theo Domain: Thành viên A lo Core Models (`Guest`, `Room`), đa hình tính tiền và UI; Thành viên B lo luồng đặt phòng (`Reservation`), State Machine và thuật toán chặn trùng lịch.

**Nhóm làm:** Lên danh sách phân công nhiệm vụ thực tế theo gợi ý: Lộc xử lý `Guest`, `Room`, tính tiền hóa đơn; Hảo lo lớp `Reservation`, trạng thái phòng và chống trùng lịch.

### 1.2. Hỏi: Code xong ở nhánh phụ, push lên GitHub thì nhánh main có tự nhận code không?

**AI trả lời:** Không. Nhánh làm việc là độc lập. Để đưa code vào nhánh `main`, cần dùng tính năng Pull Request (PR) trên GitHub và Merge. Khuyên nhóm thiết lập Branch protection rule để bắt buộc review chéo code trước khi gộp.

**Nhóm làm:** Thiết lập Branch protection, làm quen với quy trình push code lên nhánh phụ, tạo PR và nhờ thành viên còn lại duyệt (Approve) rồi mới Merge.

### 1.3. Hỏi: Có cách nào hủy thao tác merge nhầm của người kia không?

**AI trả lời:** Dùng nút Revert ngay trong Pull Request đã đóng trên GitHub để tạo một PR đảo ngược. Sau khi merge bản Revert, nhánh cũ bị dính lịch sử "vứt bỏ", do đó phải chuyển về nhánh `main` dưới máy tính (`git pull`), xóa nhánh cũ và tạo nhánh hoàn toàn mới (ví dụ: `v2`) để code lại.

**Nhóm làm:** Bấm Revert trên GitHub, gộp vào `main`. Mở VSC dọn dẹp nhánh cũ, pull nhánh `main` sạch về và tạo nhánh mới `Room-information-v2` để tiếp tục làm việc.

-----------------------------------------------------------
## Tuần 2: lõi hệ thống 
**Công cụ AI:** Claude (Anthropic) | **Thành viên:** Bùi Tấn Lộc (25110037)
### 2.1. Hỏi: Rà code tuần 1, cần sửa gì?

**AI trả lời:** Có hai class `Program` trùng nhau nên build sẽ lỗi, cần xóa cái trong file Room. Namespace chưa thống nhất giữa các file (`Project_Final` và `Project_final`), mà C# phân biệt hoa thường nên đây là hai namespace khác nhau. Ngoài ra `PhoneNumber` kiểu `int` sẽ tràn số và mất số 0 đầu, nên đổi sang `string`; tiền nên dùng `decimal`; tiện ích `"Air Conditioner"` của VIP bị lặp hai lần.

**Nhóm làm:** Sửa lại code theo gợi ý, chạy `dotnet build` để kiểm tra.

### 2.2. Hỏi: Cách clone repo GitHub và cách dùng namespace

**AI trả lời:** Clone bằng `git clone <link repo>`, hai file `_gitignore` và `_gitattributes` phải đổi thành `.gitignore` và `.gitattributes`. Trước khi code nên `git pull`, code xong thì `git add`, `commit`, `push`. Namespace dùng để gom class và tránh trùng tên; với project nhỏ nên dùng một namespace duy nhất cho mọi file và khai báo bằng kiểu file-scoped (`namespace Project_final;`).

**Nhóm làm:** Làm theo hướng dẫn trên máy.

### 2.3. Hỏi: Có đổi cách tính giá phòng VIP được không?

**AI trả lời:** Được, vì `CalculateNightlyRate()` là abstract nên mỗi loại phòng tự quyết định công thức giá. Có ba hướng: chỉ đổi số liệu, tính theo thành phần (giá cơ bản cộng phí dịch vụ cố định cộng phụ thu cuối tuần), hoặc tách chiến lược giá thành interface. Hướng thứ hai vừa đủ để phòng VIP "ra dáng VIP" và thể hiện rõ tính đa hình trong test.

**Nhóm làm:** Tự chọn hướng tính giá cho `VipRoom`.

### 2.4. Hỏi: Vì sao số điện thoại là `string`, tiền là `decimal`?

**AI trả lời:** Số điện thoại là định danh chứ không phải số để tính toán, và kiểu số sẽ làm mất số 0 đầu hoặc tràn giới hạn. `double` lưu theo nhị phân nên các số như 0.1 bị sai số nhỏ, còn `decimal` lưu theo thập phân nên chính xác với số tiền như 0.1 hay 19.99. Khi hỏi thêm về 1/3, AI đính chính rằng `decimal` cũng không lưu chính xác 1/3, nó chỉ chính xác với số thập phân hữu hạn.

**Nhóm làm:** Đổi kiểu dữ liệu trong code, tự chạy thử `0.1 + 0.2` để kiểm chứng.

### 2.5. Hỏi: Mẫu code `HotelService` và dịch vụ đi kèm

**AI trả lời:** `IAddOnService` có `Name` và `CalculateCost(int nights)`, hai dịch vụ `BreakfastService` (tính theo người nhân số đêm) và `LaundryService` (phí cố định) cùng cài đặt interface này. `HotelService` có `AddRoom`, `SearchAvailable` và `Book`, trong đó `Book` ném `RoomNotAvailableException` nếu phòng đã có người đặt trùng ngày, bỏ qua các reservation đã hủy. Kèm ba test: đặt trùng bị chặn, đặt liền kề được, hủy rồi đặt lại được.

**Nhóm làm:** Tham khảo, tự đọc hiểu và chỉnh lại trước khi dùng.

----------------------------------------------------------------------------
**Công cụ AI:** Gemini (3.1 Pro) | **Thành viên:** Nguyễn Lê Hảo (25110019)
### 2.1. Hỏi: Xây dựng lớp Reservation và xử lý trạng thái đặt phòng như thế nào?

**AI trả lời:** Cung cấp khung code class `Reservation`, dùng `enum ReservationStatus` và `private set` để đảm bảo tính Đóng gói (Encapsulation). Đưa ra thuật toán chống trùng lịch `(newStartDate < EndDate) && (newEndDate > StartDate)`. Phát hiện lỗi khai báo `internal class` trong code của nhóm và khuyên sửa thành `public class`.

**Nhóm làm:** Đổi thành `public class`, copy thuật toán trùng lịch và triển khai các hàm `CheckIn`, `CheckOut`, `Cancel` có chặn lỗi ngoại lệ (`InvalidOperationException`) khi sai luồng trạng thái.

### 2.2. Hỏi: Tại sao gọi biến Guest/Room trong file Reservation lại bị báo lỗi gạch đỏ?

**AI trả lời:** Do file `Guest.cs` của Lộc chưa được tạo hoặc chưa gộp chung vào nhánh. Khuyên áp dụng chiến thuật "Thống nhất bộ khung": Cả hai cùng tạo khung file class trống, chung `namespace Project_final`, push lên `main` và pull về VSC để trình biên dịch tự động liên kết các file, giúp hết lỗi đỏ trước khi viết logic chi tiết.

**Nhóm làm:** Tạo các file khung `program.cs`, `reservation.cs`, `room-information.cs` chung một namespace, đồng bộ xuống VSC của cả 2 người, sau đó mạnh ai nấy code file của mình không sợ conflict.

### 2.3. Hỏi: Ý nghĩa của đoạn code public enum ReservationStatus là gì?

**AI trả lời:** Đây là kiểu liệt kê (enum) giúp quy định các trạng thái cố định (Booked, CheckedIn, CheckedOut, Cancelled). Nó giúp chống gõ sai chính tả, giới hạn quy tắc chặt chẽ cho State Machine và làm code dễ đọc hơn so với dùng số hoặc chuỗi.

**Nhóm làm:** Áp dụng `enum` để kiểm tra điều kiện (if) trong các hàm chuyển đổi trạng thái của đơn đặt phòng.

-----------------------------------------------------------
## Tuần 3: Xây dựng bộ quản lý (Manager), Giao diện Console và Tích hợp Hóa đơn
**Công cụ AI:** Claude (Anthropic) | **Thành viên:** Bùi Tấn Lộc (25110037)
### 3.1. Hỏi: Tuần 3 mình cần làm gì tiếp theo?

**AI trả lời:** Phần việc của Lộc gồm thanh toán và hóa đơn, Console Menu, và viết test cùng Hảo. AI chỉ ra `AddOnServices.cs` bị lệch: comment của `BreakfastService` và `LaundryService` ghi miễn phí cho VIP, nhưng chỉ `SpaService` có `FreeForVip => true`. Thứ tự gợi ý: chốt giao diện với Hảo, viết `Invoice` và `BillingService` tách riêng khỏi `Reservation` để tránh God class, viết menu bằng `while` và `switch-case`, cuối cùng viết test cho ba loại kịch bản (happy path, lỗi, đa hình).

**Nhóm làm:** Sửa `AddOnServices.cs` cho khớp comment (thêm `FreeForVip => true` cho `BreakfastService` và `LaundryService`), giữ nguyên các comment cũ.

### 3.2. Hỏi: Viết code cho Billing, Console Menu và Test Cases

**AI trả lời:** Cung cấp `Billing.cs` (`InvoiceLine`, `Invoice`, `BillingService`), `Program.cs` (menu tìm phòng, đặt phòng, check-in, check-out, hủy, xem danh sách) và `TestRunner.cs` (chạy bằng `dotnet run -- test`). Lúc đó `Reservation.cs` và `Room-information.cs` còn trống nên AI viết theo giao diện tự giả định (`RoomType`, `Hotel`, `FindAvailableRooms`...). AI nói rõ môi trường của nó không có trình biên dịch C# nên chưa chạy thử được code.

**Nhóm làm:** Dùng làm khung ban đầu, chưa dùng nguyên văn vì tên chưa khớp với code thật (xem mục 3.6).

### 3.3. Hỏi: Giải thích Program, TestRunner và toàn bộ đoạn code Billing

**AI trả lời:** `Program.cs` là giao diện console và điểm khởi chạy, `TestRunner.cs` là bộ kiểm thử tự động in PASS hoặc FAIL để làm bằng chứng test. Với `Billing.cs`: `Invoice.Total` tính lại từ các dòng hóa đơn, tiền dùng decimal, `BillingService` hỏi từng loại phòng và từng dịch vụ tự tính giá nên thêm loại phòng hay dịch vụ mới không phải sửa `BillingService` (đa hình, Open/Closed).

**Nhóm làm:** Đọc lại và ghi chú lý do thiết kế để chuẩn bị phần Tự giải trình và buổi bảo vệ.

### 3.4. Hỏi: Chia code thành nhiều commit và đặt tên commit

**AI trả lời:** Chia `Billing.cs`, `Program.cs`, `TestRunner.cs` thành bản chưa đầy đủ rồi bản hoàn chỉnh (tổng 8 commit), tên commit theo kiểu feat, fix, test viết bằng tiếng Việt. AI lưu ý nên commit đúng theo từng bước làm thật và đọc hiểu từng commit trước khi nộp.

**Nhóm làm:** Commit phần dịch vụ lên nhánh billing-addons, các phần còn lại commit dần theo tiến độ.

### 3.5. Hỏi: Vì sao không push được lên nhánh main?

**AI trả lời:** `main` trên máy và trên GitHub đã rẽ nhánh (máy có 3 commit khác với 11 commit trên GitHub). AI hướng dẫn `git fetch`, `git pull --rebase origin main`, sửa conflict ở `Program.cs`, `git add`, `git rebase --continue` rồi mới push, và cảnh báo không dùng `--force` lên `main`. AI cũng phát hiện `AddOnServices.cs` đã commit nhưng là file rỗng. Phần `Program.cs` của Hảo trong conflict trống nên giữ code của Lộc. Gợi ý làm trên nhánh riêng rồi tạo Pull Request.

**Nhóm làm:** Chạy `git fetch` và `git pull --rebase`, xử lý conflict trong `Program.cs`.

### 3.6. Hỏi: Code mới không ăn nhập với các hàm trong code cũ

**AI trả lời:** Nguyên nhân là AI tự đoán tên khi chưa thấy code thật. Sau khi đối chiếu với code thật (`Room` abstract với `RoomNumber`, `CalculateNightlyRate()`, `Type` kiểu enum; `HotelManager` với `GetAvailableRooms`, `AddReservation`; `Reservation` có `ReservationID` và chưa có danh sách dịch vụ), AI viết lại `Billing.cs`, `Program.cs`, `TestRunner.cs` cho khớp, cho `BillingService.Generate` nhận danh sách dịch vụ qua tham số và đổi namespace sang `Project_Final`. AI cũng chỉ ra lỗi trong code của Hảo: `IReadOnlyList` viết sai, `AddReservation` chưa chặn đặt trùng, `Cancel()` chưa chặn đúng trạng thái, `TotalBill` trùng việc với `BillingService`.

**Nhóm làm:** Dùng bản đã sửa của `Billing.cs`, `Program.cs`, `TestRunner.cs`, gửi danh sách lỗi cho Hảo để sửa trong file của Hảo.

----------------------------------------------------------------------------
**Công cụ AI:** Gemini (3.1 Pro) | **Thành viên:** Nguyễn Lê Hảo (25110019)

### 3.1. Hỏi: Xây dựng tính năng tìm kiếm phòng nên viết chung trong file Reservation.cs hay tạo file mới?

**AI trả lời:** Khuyên nhóm tạo file `HotelManager.cs` riêng biệt để tuân thủ Nguyên lý Đơn nhiệm (SRP). Lớp `Reservation` chỉ nên quản lý 1 đơn đặt phòng, còn `HotelManager` sẽ đứng ra cầm danh sách tổng (`Rooms`, `Reservations`) để duyệt và tìm kiếm. Cung cấp khung code thuật toán duyệt phòng kết hợp hàm `IsOverlapping`.

**Nhóm làm:** Tạo file `HotelManager.cs`, triển khai hàm `GetAvailableRooms(checkInDate, checkOutDate)` để lọc danh sách các phòng còn trống dựa trên lịch khách yêu cầu.

### 3.2. Hỏi: Tại sao danh sách phòng phải dùng private set / IReadOnlyList mà không dùng public List hoặc readonly?

**AI trả lời:** Giải thích kỹ thuật Backing Field (trường dữ liệu chống lưng) trong OOP để đạt điểm tối đa tính Đóng gói (Encapsulation). Từ khóa `readonly` chỉ khóa vỏ hộp (không đổi được list khác) nhưng vẫn cho phép sửa ruột (gọi hàm `.Clear()`, `.Add()`). Việc dùng `IReadOnlyList` kết hợp hàm `AddRoom()` giúp khóa chặt dữ liệu, bắt buộc Client Code phải đi qua trạm kiểm duyệt.

**Nhóm làm:** Áp dụng thiết kế Backing Field (tạo biến `private List<Room> _rooms` và `public IReadOnlyList<Room> Rooms`), bắt buộc khởi tạo dữ liệu qua cửa ngõ `AddRoom()` để bảo vệ an toàn dữ liệu khách sạn.

### 3.3. Hỏi: Sửa lỗi code khi ráp nối file Program.cs (Menu Console) và Billing.cs vào hệ thống Room của Lộc như thế nào?

**AI trả lời:** Chỉ ra các điểm "vênh" do 2 thành viên code độc lập: Sai tên thuộc tính (`Number` thành `RoomNumber`), sai cách gọi đa hình khi khởi tạo (`new StandardRoom` thay vì truyền class Type), và cách gọi biến VIP. AI cung cấp bản sửa lỗi tích hợp cho `Program.cs` và `BillingService` để khớp với `interface IAddOnService`.

**Nhóm làm:** Điều chỉnh lại các thuộc tính trong `Billing.cs`, sửa logic Menu Console trong vòng lặp `while/switch-case` tại `Program.cs`. Chạy thử nghiệm luồng tích hợp: Khởi tạo phòng -> Tìm kiếm phòng trống -> Xuất thông tin phòng có đa hình giá tiền và các dịch vụ Add-on đi kèm.