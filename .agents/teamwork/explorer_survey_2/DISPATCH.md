## 2026-11-10T17:02:42Z
You are Explorer 2 (Data Access Layer, LINQ Lambda Compliance, App.config, Relative Paths).

Your working directory is: d:\DoAnCuoiKi_QuanLyTiemCaPhe
Your private state folder is: d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\explorer_survey_2

MANDATORY FIRST STEP:
Read the original request document at:
d:\DoAnCuoiKi_QuanLyTiemCaPhe\ORIGINAL_REQUEST.md

YOUR MISSION:
Perform a comprehensive survey of the codebase focusing on:
1. Database Architecture & ORM:
   - Identify how database access is implemented (LINQ to SQL .dbml, Entity Framework, ADO.NET, custom DAL/BLL/DTO/DAO).
   - Locate database files (.mdf, .ldf, .sql scripts) and schema definitions.
2. LINQ Lambda Compliance Audit:
   - The instructor's matrix strictly requires: "LINQ Lambda: 3 điểm (bắt buộc dùng toàn bộ là LINQ Lambda, kiểm tra toàn bộ dbo CRUD)".
   - Scan ALL C# files across the entire solution.
   - Enumerate all database operations (Create, Read, Update, Delete).
   - Detect any raw SQL queries (SqlCommand, SqlDataAdapter, ExecuteReader, ExecuteNonQuery, inline SQL strings, concatenated SQL).
   - Detect any LINQ Query syntax (`from x in db.Table where ... select x`).
   - List every file, method, and line number where non-Lambda queries exist, and document exactly what needs to be refactored to pure LINQ Lambda methods (`.Where()`, `.Select()`, `.FirstOrDefault()`, `.InsertOnSubmit()`, `.ToList()`, etc.).
3. App.config Audit:
   - Check App.config: Is the connection string stored here?
   - Check if any connection strings are hardcoded in C# code, DataContext constructors, or classes.
   - Verify if ConfigurationManager or ApplicationSettings is used to load connection strings dynamically.
4. Relative Paths Audit:
   - Check for hardcoded absolute paths anywhere in the code (e.g. `C:\...`, `D:\...`, `E:\...`), especially for:
     * Database connection string attachDbFilename or Data Source.
     * Image loading (food images, icons, logos).
     * Report files, invoice templates, export files.
   - Verify that all paths use relative paths (e.g. `Application.StartupPath`, `AppDomain.CurrentDomain.BaseDirectory`, `|DataDirectory|`).

DELIVERABLE:
Write a detailed survey report to:
d:\DoAnCuoiKi_QuanLyTiemCaPhe\.agents\teamwork\explorer_survey_2\handoff.md
Follow the standard Handoff format: Observation, Logic Chain, Caveats, Conclusion, Verification Method.
When finished, notify the orchestrator via send_message.
