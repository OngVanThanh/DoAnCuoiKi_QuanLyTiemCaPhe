# Original User Request

## Initial Request — 2026-11-10T17:01:02Z

Rà soát toàn diện dự án Quản lý tiệm Cafe, tìm và trực tiếp sửa tất cả các lỗi chức năng, lỗ hổng, lỗi nghiệp vụ và lỗi UI/UX trong mã nguồn, đồng thời đánh giá hệ thống nghiêm ngặt theo từng tiêu chí chấm điểm và lưu ý của giảng viên (bỏ qua phần viết báo cáo Word).

Working directory: d:\DoAnCuoiKi_QuanLyTiemCaPhe
Integrity mode: development

Reference material:
- Ma trận chấm điểm cuối kỳ:
  + LINQ Lambda: 3 điểm (bắt buộc dùng toàn bộ là LINQ Lambda, kiểm tra toàn bộ dbo CRUD)
  + 15 form: 2 điểm (bắt buộc có Design form, không dùng AI generate từ behind code)
  + Mã hóa, phân quyền: 3 điểm
  + Nghiệp vụ + In ấn: 2 điểm (đúng yêu cầu nghiệp vụ quản lý quán cafe)
  + App.config: 1 điểm
  + UI 3rd party: 1 điểm
  + Chart: 1 điểm
  + Đóng gói: 1 điểm
  + Khác: 1 điểm
- Các lưu ý bắt buộc từ giảng viên:
  + Bắt buộc dùng toàn bộ là LINQ Lambda
  + Bắt buộc có Design form (không dùng AI generate từ behind code)
  + Sử dụng địa chỉ tương đối (không dùng địa chỉ tuyệt đối)
  + Đúng yêu cầu nghiệp vụ chủ đề Quản lý tiệm Cafe

## Requirements

### R1. Rà soát, phát hiện và sửa toàn bộ lỗi hệ thống
Tìm kiếm và khắc phục trực tiếp trong mã nguồn tất cả các lỗi chức năng, lỗ hổng bảo mật (SQL injection, phân quyền, rò rỉ dữ liệu), lỗi vi phạm quy tắc nghiệp vụ quản lý quán cafe và lỗi giao diện người dùng (UI/UX) trên toàn bộ hệ thống.

### R2. Đảm bảo tuân thủ 100% các tiêu chí và lưu ý bắt buộc của giảng viên
Trực tiếp kiểm tra và hoàn thiện mã nguồn đáp ứng nghiêm ngặt ma trận chấm điểm: chuyển đổi toàn bộ các truy vấn sang LINQ Lambda (không sót truy vấn ADO.NET thô hoặc query syntax); đảm bảo hệ thống có tối thiểu 15 form với file Designer chuẩn; triển khai đầy đủ cơ chế mã hóa mật khẩu và phân quyền người dùng; kiểm tra tính năng nghiệp vụ quán cafe kết hợp in ấn; cấu hình connection string qua App.config; tích hợp UI bên thứ ba; có biểu đồ Chart thống kê; đảm bảo 100% đường dẫn là địa chỉ tương đối; chuẩn bị cấu hình đóng gói phần mềm.

### R3. Lập tài liệu đánh giá ma trận tiêu chí và nhật ký sửa lỗi
Ghi lại chi tiết kết quả rà soát, bảng tự chấm điểm theo từng tiêu chí trong ma trận của giảng viên, danh sách các lỗi đã phát hiện cùng giải pháp khắc phục tương ứng vào tài liệu kiểm định trong thư mục dự án (bỏ qua yêu cầu làm báo cáo Word).

## Acceptance Criteria

### Tuân thủ tiêu chí giảng viên
- [ ] 100% các thao tác truy vấn dữ liệu CRUD trên cơ sở dữ liệu sử dụng cú pháp LINQ Lambda, không còn câu lệnh SQL thô hoặc LINQ Query syntax trong mã nguồn.
- [ ] Dự án có tối thiểu 15 form có giao diện trực quan được định nghĩa qua file Designer, không tạo form hoàn toàn bằng code-behind runtime.
- [ ] Mật khẩu tài khoản được mã hóa trong cơ sở dữ liệu; hệ thống phân quyền chặt chẽ giữa các vai trò (quản lý, nhân viên) khi thao tác các chức năng và form.
- [ ] Quy trình nghiệp vụ quán cafe (gọi món, chuyển bàn, thanh toán, quản lý thực đơn/kho) hoạt động chính xác và có chức năng xuất/in hóa đơn/phiếu thanh toán.
- [ ] Chuỗi kết nối cơ sở dữ liệu và các cấu hình hệ thống được lưu trong file App.config.
- [ ] Giao diện sử dụng ít nhất một thư viện UI bên thứ ba và có hiển thị biểu đồ thống kê (Chart).
- [ ] Toàn bộ các đường dẫn tài nguyên (hình ảnh, cơ sở dữ liệu, file cấu hình) đều là đường dẫn tương đối, không tồn tại đường dẫn tuyệt đối dạng ổ đĩa cục bộ cứng.
- [ ] Ứng dụng build thành công không lỗi, sẵn sàng cho việc đóng gói triển khai.

### Khắc phục lỗi & Tài liệu kiểm định
- [ ] Toàn bộ các lỗi chức năng, lỗ hổng bảo mật, lỗi nghiệp vụ và bất cập UI/UX được sửa dứt điểm trong mã nguồn, giải pháp build và chạy ổn định.
- [ ] Có tài liệu tổng kết đánh giá (dạng Markdown) trong dự án chi tiết hóa từng mục điểm theo ma trận, đối chiếu trước và sau khi sửa lỗi.
