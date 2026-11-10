## 2026-11-10T17:02:51Z
You are Explorer 3 (Cafe Business Logic, Invoicing & Printing, Security & Authentication, Vulnerability Audit).

Your working directory is: d:\DoAnCuoiKi_QuanLyTiemCaPhe
Your private state folder is: d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\explorer_survey_3

MANDATORY FIRST STEP:
Read the original request document at:
d:\DoAnCuoiKi_QuanLyTiemCaPhe\ORIGINAL_REQUEST.md

YOUR MISSION:
Perform a comprehensive survey of the codebase focusing on:
1. Cafe Management Business Logic:
   - Table management (bàn, trạng thái bàn Trống / Có người / Đặt trước, hiển thị trực quan).
   - Ordering workflow (gọi món, chọn số lượng, thêm món vào bàn, chuyển bàn, gộp bàn).
   - Billing & Invoice printing:
     * Bill calculation (tổng tiền, giảm giá/chiết khấu, VAT/phụ thu nếu có, tiền khách đưa, tiền thối).
     * Invoice printing / export functionality: How is invoice printing implemented? (PrintDocument, Crystal Reports, RDLC, or custom print/export). Does it print/export properly formatted invoices?
   - Menu & Category management (Thực đơn, Danh mục món, Đơn giá).
   - Warehouse / Inventory / Nguyên vật liệu if present.
   - Staff & Account management.
2. Security & Authentication:
   - Password Hashing:
     * How are passwords stored and compared? Is it plain text, MD5, SHA-256, or BCrypt?
     * Does password hashing exist on Login, Create User, Change Password?
     * What changes are needed to ensure robust password hashing?
   - Role-Based Access Control (RBAC):
     * Are roles clearly separated (e.g. Quản lý vs Nhân viên)?
     * Are form-level and button/action-level permissions enforced? Can an employee access manager functions (doanh thu, nhân viên, quản trị)?
3. Vulnerabilities & Code Quality:
   - SQL Injection risks.
   - Unhandled exceptions / crash vectors.
   - Data validation bugs (negative numbers, empty strings, null reference exceptions).
   - UI/UX issues (misaligned controls, unresponsive buttons, confusing workflows).

DELIVERABLE:
Write a detailed survey report to:
d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\explorer_survey_3\handoff.md
Follow the standard Handoff format: Observation, Logic Chain, Caveats, Conclusion, Verification Method.
When finished, notify the orchestrator via send_message.
