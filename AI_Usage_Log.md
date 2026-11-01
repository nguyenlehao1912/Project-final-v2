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
-----------------------------------------------------------
## Tuần 4: Hoàn thiện, tích hợp và kiểm thử toàn hệ thống
**Công cụ AI:** Claude (Anthropic) | **Thành viên:** Bùi Tấn Lộc (25110037)
### 4.1. Hỏi: Tuần 4 là tuần cuối, sau nhận xét của thầy mình cần làm gì?

**AI trả lời:** Chỉ ra code lúc đó chưa build được: `Program.cs` dùng `room.Number`, `room.Type.Name`, `GetNightlyRate()` trong khi `Room` có `RoomNumber`, `Type` là enum và hàm là `CalculateNightlyRate()`; `Billing.cs` dùng `r.AddOns` nhưng `Reservation` chưa có. Ngoài ra `AddReservation` chưa chặn đặt trùng, `Cancel()` chưa chặn đúng trạng thái, `Reservation.CheckOut()` tự tính tiền trùng việc với `BillingService`. Thứ tự gợi ý: cho code build được và nối menu 2-4, viết `TestRunner` với 3 scenario (happy path, validation/error, polymorphism), rồi làm tài liệu (Self-Explanation, state machine diagram, README, AI log tuần 4, khớp tên tool trong form).

**Nhóm làm:** Sửa các chỗ lệch tên, thêm `AddOns`/`AddService` vào `Reservation`, chuyển việc tạo đặt phòng và chặn trùng sang `HotelManager.MakeReservation`, cho `CheckOut()` trả về `Invoice` do `BillingService` tạo, nối đủ menu trong `Program.cs`.

### 4.2. Hỏi: Viết `TestRunner.cs` và sửa cho khớp tên hàm đã đổi

**AI trả lời:** Viết `TestRunner.cs` chạy bằng `dotnet run -- test`, in PASS/FAIL, gồm 3 scenario thầy yêu cầu. Bản đầu có khoảng 30 kiểm tra và khá dài, sau khi nhóm nói quá dài thì rút gọn còn vài kiểm tra chính cho mỗi scenario. Khi code đổi tên (`AddReservation` thành `MakeReservation`, `AddAddOn` thành `AddService`, `CheckOut()` trả về `Invoice`) thì viết lại cho khớp. Số liệu kỳ vọng được tính tay từ dữ liệu mẫu: hóa đơn phòng Standard 2 đêm với đủ dịch vụ là 1,750,000 VND, phòng VIP là 2,880,000 VND. AI nói rõ lúc viết chưa có trình biên dịch nên chưa chạy thử. AI cũng phát hiện `Cancel()` bị ngược điều kiện (`Status == Booked` thì ném lỗi), hai test về hủy đơn sẽ bắt đúng lỗi này.

**Nhóm làm:** Dùng bản rút gọn, thêm lệnh gọi `TestRunner.Run()` vào `Program.cs`, sửa `Cancel()` thành `if (Status != ReservationStatus.Booked) throw ...`, tự chạy `dotnet run -- test` để lấy kết quả làm minh chứng.

### 4.3. Hỏi: Vẽ State Machine Diagram cho `Reservation`

**AI trả lời:** Vẽ sơ đồ (file `state-machine-reservation.png`) gồm 4 trạng thái `Booked`, `CheckedIn`, `CheckedOut`, `Cancelled`; trạng thái đầu là `new Reservation()`; chuyển trạng thái qua `CheckIn()`, `CheckOut()`, `Cancel()` kèm điều kiện; `AddService()` là vòng tự chuyển ở `Booked` và `CheckedIn`; mọi chuyển khác ném `InvalidOperationException`. AI lưu ý sơ đồ chỉ đúng nếu `Cancel()` đã sửa như ở mục 4.2, nên cần đối chiếu với code trước khi nộp.

**Nhóm làm:** Đưa sơ đồ vào báo cáo tuần 4 và vào repo, ghi vài dòng giải thích dưới hình.

### 4.4. Hỏi: Kiểm tra code đã hoàn chỉnh chưa và có khớp sơ đồ không

**AI trả lời:** Lần này AI cài được .NET 10 và build thật bằng đúng các file nhóm gửi: 0 lỗi, 5 cảnh báo nullable, `dotnet run -- test` ra 15/15 PASS. AI cũng chạy thử cả 4 trạng thái với `CheckIn`, `CheckOut`, `Cancel`, `AddService` và kết quả khớp từng mũi tên của sơ đồ. Phần còn thiếu không nằm ở code: `README.md` vẫn ghi "Đồ án DSA", AI log chưa có tuần 4, phần Self-Explanation chưa viết, form khai báo tool chỉ ghi Gemini trong khi log ghi cả Claude và Gemini. Một chi tiết nhỏ: `Guest` báo "10 or 11 digits" nhưng chỉ kiểm tra độ dài chuỗi.

**Nhóm làm:** Cập nhật `README.md`, bổ sung AI log tuần 4, viết Self-Explanation và sửa form khai báo tool cho đủ cả hai công cụ.
----------------------------------------------------------------------------
**Công cụ AI:** Claude (Sonnet 5.5 Medium) | **Thành viên:** Nguyễn Lê Hảo (25110019)
### 4.1. Hỏi: Rà toàn bộ code chính, đã hoàn chỉnh chưa, cần sửa ở file và dòng nào?

**AI trả lời:** Chưa hoàn chỉnh và chưa build được. Thiết kế OOP khá tốt (đa hình ở `Room`, đóng gói bằng `private set` và `IReadOnlyList`, tách `BillingService` khỏi `Reservation`) nhưng các file chưa khớp nhau. Lỗi biên dịch: `Program.cs` dùng `room.Number`, `room.Type.Name`, `room.Type.GetNightlyRate()` không tồn tại; `Billing.cs` dùng `IAddOnService` và `r.AddOns` chưa được định nghĩa. Lỗi logic: `CheckOut()` chỉ tính tiền phòng, bỏ qua dịch vụ; `Console.WriteLine` nằm trong class nghiệp vụ; `Cancel()` chưa chặn đúng trạng thái; `GetAvailableRooms` chưa kiểm tra ngày; `AddReservation` cho phép đặt trùng phòng. Còn thiếu chức năng đặt phòng, check-in, check-out trong menu.

**Nhóm làm:** Ghi lại danh sách việc cần sửa theo từng file (mục A đến G) để sửa dần.

### 4.2. Hỏi: Viết `IAddOnService` và điều chỉnh file `AddOnServices.cs` của nhóm

**AI trả lời:** Đưa mẫu interface kèm các dịch vụ cụ thể. Khi xem file thật của nhóm, AI nhận xét `FreeForVip` có giá trị mặc định `false` trong interface (default interface member) là thiết kế tốt hơn bản mẫu, và đề nghị sửa: thêm `using System;` (thiếu thì `ArgumentException` không biên dịch được), kiểm tra đầu vào trong constructor (`persons > 0`, giá không âm), đưa số người vào `Name` để hóa đơn dễ đối chiếu, ghi rõ "(free)" cho `EcoParkService`. Gợi ý tùy chọn: tách lớp trừu tượng `PerPersonService` cho `BreakfastService` và `SpaService` để bỏ code lặp. AI nhắc xóa file mẫu cũ để không bị lỗi định nghĩa trùng.

**Nhóm làm:** Áp dụng phần validate, đổi `Name` và thêm "(free)". Chưa tách `PerPersonService`.

### 4.3. Hỏi: `AddOns` là phương thức hay thuộc tính?

**AI trả lời:** Là thuộc tính chỉ đọc (`IReadOnlyList<IAddOnService> AddOns => _addOns;`), không có `()` và chỉ có `get`. Field `_addOns` là `List` private, bên ngoài chỉ duyệt được, muốn thêm dịch vụ phải gọi phương thức `AddService()` có kiểm tra hợp lệ. Đây là tính đóng gói, cùng mô hình với `Rooms` và `Reservations` trong `HotelManager`.

**Nhóm làm:** Đọc hiểu để chuẩn bị trả lời khi bảo vệ.

### 4.4. Hỏi: Gỡ rối từng phần B đến G, có code thì cho xem

**AI trả lời:** Đưa bản sửa theo từng file. `Reservation.cs`: kiểm tra đầu vào ở constructor, lưu `.Date` cho ngày vào ra, `CheckOut()` gọi `BillingService` và trả về `Invoice`, `Cancel()` chỉ cho hủy khi `Booked`, thêm `AddOns` và `AddService`, bỏ `Console.WriteLine`. `HotelManager.cs`: `readonly`, `AddRoom` chặn `null` và trùng số phòng, `GetAvailableRooms` ném `ArgumentException` khi ngày sai, thay `AddReservation` bằng `MakeReservation` (kiểm tra phòng tồn tại, ID trùng, phòng còn trống), thêm `FindRoom`, `FindReservation`, `CheckIn`, `CheckOut`, `Cancel`. `Billing.cs`: bỏ `if (Type == Vip)` bằng `HasFreeServices` của `Room`, kiểm tra số đêm, đóng gói `Invoice`. `Program.cs`: thêm menu 2 đến 7, hàm `Run(Action)` bắt lỗi ở một chỗ, các hàm `ReadText`, `ReadInt`, `ReadDate`, dữ liệu mẫu `R001`.

**Nhóm làm:** Áp dụng cho `Reservation.cs`, `HotelManager.cs`, `Program.cs`. Với `Billing.cs` nhóm chọn giữ bản gốc (xem 4.5).

### 4.5. Hỏi: Có thể giữ nguyên `Room-information.cs` và `Billing.cs` không?

**AI trả lời:** Được. Giữ `Room-information.cs` thì `Guest` nằm chung file, chỉ cần sửa `private set` cho `BasePrice`, chống `null` cho `amenities` và thêm `HasFreeServices`. Giữ `Billing.cs` gốc vẫn biên dịch và chạy đúng với các file đã sửa, đổi lại sẽ còn dùng `if (Type == Vip)` (ít đa hình hơn), `Invoice.Lines` còn là `List` công khai, `CalculateRoomCharge` không ai gọi và cột 40 ký tự có thể lệch khi tên dịch vụ dài. AI nói trước điểm này có thể bị nhắc khi chấm.

**Nhóm làm:** Giữ nguyên tên file `Room-information.cs` và `Billing.cs`, không thêm `HasFreeServices`.

### 4.6. Hỏi: Làm sao để chạy file, và lỗi `CS0246` về `BiliingService`

**AI trả lời:** Chạy bằng `dotnet build` rồi `dotnet run` ở thư mục có file `.csproj` (hoặc Visual Studio bằng Ctrl + F5), cần .NET 6 trở lên vì project dùng file-scoped namespace. Lỗi `CS0246` do gõ sai chính tả `BiliingService`, tên đúng là `BillingService`. AI cũng liệt kê các lỗi thường gặp như hai hàm `Main`, định nghĩa trùng do file cũ chưa xóa.

**Nhóm làm:** Sửa lại tên class trong `Reservation.cs` và build lại.

### 4.7. Hỏi: Giải thích nhóm các hàm `FindRoom`, `FindReservation`, `CheckIn`, `CheckOut`, `Cancel` trong `HotelManager` (mục D5)

**AI trả lời:** Vì `_reservations` là private nên nếu thiếu các hàm này thì `Program.cs` phải tự duyệt danh sách và tự xử lý `null` ở nhiều chỗ. `FindReservation` trả `null` khi không thấy, còn `GetReservationOrThrow` ném lỗi. `HotelManager` chỉ tìm đúng đối tượng rồi giao việc cho `Reservation`, luật đổi trạng thái chỉ nằm ở một nơi (che giấu cách lưu trữ, tránh lặp code, mỗi class một trách nhiệm).

**Nhóm làm:** Ghi chú lý do thiết kế để chuẩn bị bảo vệ.

### 4.8. Hỏi: Liên kết `TestRunner` vào `Program` và vì sao `dotnet run` vẫn hiện menu

**AI trả lời:** Đổi `Main` từ `void` sang `int` và thêm nhánh `if (args.Length > 0 && args[0] == "test") return TestRunner.Run();`. File test cũ gọi `AddAddOn` và `AddReservation` không còn tồn tại, cần đổi thành `AddService` và `MakeReservation`. Nếu vẫn ra menu thì do chạy `dotnet run` không có `-- test`, chưa thêm nhánh `if`, chạy bằng nút Run của IDE (không truyền tham số), hoặc đang chạy bản build cũ.

**Nhóm làm:** Sửa `Main` và `Testrunner.cs`, chạy `dotnet run -- test`.

### 4.9. Hỏi: Giải thích `Testrunner.cs` và viết thêm test đặt phòng, tính tiền

**AI trả lời:** `Check` kiểm tra một điều kiện đúng sai, `Throws<T>` kiểm tra đoạn code phải ném đúng loại lỗi (dùng generic và delegate `Action`). File chia ba nhóm: happy path, validation, đa hình. Có một điểm chưa tốt: các test dùng chung một `HotelManager` nên phụ thuộc thứ tự chạy. AI viết thêm hàm `RunExtra()` gồm bốn nhóm (đặt phòng, hóa đơn Standard, hóa đơn VIP, so sánh và trạng thái), mỗi nhóm một `HotelManager` riêng, kèm bảng tính tay các con số (ví dụ Standard 3 đêm = 1,500,000, VIP 2 đêm = 2,880,000). Trong lúc giải thích, AI viết nhầm giá VIP một lần rồi tự đính chính là 1,200,000 × 1.2 = 1,440,000.

**Nhóm làm:** Tự đối chiếu các con số bằng cách tính tay trước khi tin kết quả test.

### 4.10. Hỏi: Cho vài kịch bản để dùng thử từ đầu đến cuối với tư cách khách hàng, và kịch bản trùng phòng

**AI trả lời:** Đưa các kịch bản chạy bằng menu: khách Standard có dịch vụ (tổng 2,300,000), khách VIP được miễn phí dịch vụ (tổng 2,880,000), dùng đặt phòng mẫu `R001`, các ca nhập sai (ngày sai, ID không tồn tại, check-out khi chưa check-in...), đặt rồi hủy. Với trùng phòng: trùng hoàn toàn, nằm lọt bên trong, chồng đầu, chồng đuôi, bao trùm thì phải bị chặn; ngày nối tiếp (trả 13, nhận 13) và đơn đã hủy thì được đặt, do `IsOverlapping` dùng `<` và `>` không có dấu `=`.

**Nhóm làm:** Chạy thử theo kịch bản trên máy để kiểm tra luồng và các thông báo lỗi.

### 4.11. Hỏi: Kiểm tra lại 7 file đã sửa, đã ổn chưa

**AI trả lời:** Chưa ổn. `HotelManager.cs` thiếu `using System.Linq;` nên không biên dịch. `Reservation.cs` có lỗi logic nghiêm trọng: `Cancel()` kiểm tra `Status == Booked` nên bị ngược (đơn đang đặt thì không hủy được, đơn đã check-in lại hủy được). Ngoài ra còn `Console.WriteLine` trong `Reservation` khiến thông báo bị in hai lần. AI viết kịch bản tái hiện lỗi `Cancel()` bằng menu và bằng test, rồi dự đoán kết quả sau khi sửa là toàn bộ test PASS. Khi đếm, AI nói 16 dòng rồi tự đếm lại còn 15, và đính chính rằng trước khi sửa có 3 test FAIL chứ không phải 2.

**Nhóm làm:** Thêm `using System.Linq;`, đổi điều kiện thành `Status != Booked`, bỏ các `Console.WriteLine` trong `Reservation`, chạy `dotnet run -- test`.

### 4.12. Hỏi: Ràng buộc số điện thoại từ 10 đến 11 số, và chỉ hỏi lại số điện thoại khi nhập sai

**AI trả lời:** Đặt luật kiểm tra trong constructor `Guest` (nơi bảo vệ dữ liệu): độ dài từ 10 đến 11 và mọi ký tự phải là chữ số (`phone.All(char.IsDigit)`), nếu không thì ném `ArgumentException`. Để chỉ hỏi lại riêng số điện thoại, thêm hàm `ReadPhone()` trong `Program.cs` lặp cho đến khi nhập đúng, tạo thử một `Guest` để tái sử dụng luật trong `Guest`, nhờ vậy đổi luật sau này chỉ sửa một nơi. AI cũng đưa thêm 5 test cho ranh giới 9, 10, 11, 12 số và số có chữ.

**Nhóm làm:** Sửa `Guest` và thêm `ReadPhone()`, nhập thử các trường hợp ranh giới bằng menu.
----------------------------------------------------------------------------
**Công cụ AI:** Gemini (3.1 Pro) | **Thành viên:** Nguyễn Lê Hảo (25110019)
### 4.1. Hỏi: Cấu trúc báo cáo tuần 4 cần những gì để đáp ứng đủ góp ý của Giảng viên?
**AI trả lời:** Phân tích yêu cầu và gợi ý chia báo cáo thành 5 phần chính để bù đắp các mục còn thiếu: Cập nhật Kế hoạch, viết Self-Explanation chỉ đích danh các class/hàm cho 4 nguyên lý OOP, thiết kế State Machine Diagram cho Reservation, mô tả 3 kịch bản test cụ thể và lưu ý phải đồng nhất ghi tên công cụ AI là Gemini.
**Nhóm làm:** Áp dụng khung báo cáo của AI, tự bổ sung giải thích logic hệ thống và đưa hình ảnh biểu đồ trạng thái vào báo cáo.

### 4.2. Hỏi: Logic kết nối toàn bộ luồng Console Menu ở Program.cs, bổ sung Edge cases và TestRunner
**AI trả lời:** AI rà soát mã nguồn `Program.cs` hiện tại, hướng dẫn nối các hàm `MakeReservation`, `CheckIn`, `CheckOut`. Hỗ trợ sinh mã cho file kiểm thử `TestRunner.cs`, bổ sung các điều kiện kiểm tra (validation) như số điện thoại, chặn số âm trong dịch vụ, chặn null trong phòng. Hướng dẫn đổi kiểu trả về của hàm `Main` thành `int` để chạy test.
**Nhóm làm:** Chạy kiểm thử hệ thống, xác nhận luồng console menu đã kết nối trọn vẹn. Thêm các logic validate dữ liệu, sửa lỗi logic trạng thái khi Cancel/Check-out, gộp nhánh xử lý xung đột và push code hoàn thiện lên nhánh `main`.