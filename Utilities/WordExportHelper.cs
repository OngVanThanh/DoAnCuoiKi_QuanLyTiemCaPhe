using System;
using System.IO;
using Microsoft.Reporting.WinForms;

namespace DoAnCuoiKi_QuanLyTiemCaPhe.Utilities
{
    public static class WordExportHelper
    {
        /// <summary>
        /// Kết xuất nội dung LocalReport ra file Word (.docx)
        /// </summary>
        public static bool ExportReportToWord(LocalReport report, string filePath, out string errorMessage)
        {
            errorMessage = string.Empty;
            try
            {
                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string extension;

                byte[] bytes = report.Render(
                    "WORDOPENXML", // Chuẩn định dạng .docx
                    null,
                    out mimeType,
                    out encoding,
                    out extension,
                    out streamids,
                    out warnings
                );

                File.WriteAllBytes(filePath, bytes);
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }
    }
}