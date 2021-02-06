using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;

namespace ExcelExtraction.excel
{
    class createFile
    {
        Excel.Application excelApp = null;
        Excel.Workbook wb = null;
        Excel.Worksheet ws1 = null;
        Excel.Worksheet ws2 = null;

        public void createNewFile()
        {
            excelApp = new Excel.Application();
            excelApp.DisplayAlerts = false;

            wb = excelApp.Workbooks.Add();
            ws1 = (Excel.Worksheet)wb.Worksheets.Add(Type.Missing, wb.Worksheets[1]);
            ws1.Name = "시트테스트1";

            ws2 = (Excel.Worksheet)wb.Worksheets.Add(Type.Missing, wb.Worksheets[2]);
            ws2.Name = "시트테스트2";
            wb.SaveAs("C:\\Users\\MYHOME\\Desktop\\jaehomersimpson\\ExcelExtraction\\test.xlsx");
        }

        public void deallocate()
        {
            wb.Close();
            excelApp.Quit();
            ReleaseExcelObject(ws1);
            ReleaseExcelObject(ws2);
            ReleaseExcelObject(wb);
            ReleaseExcelObject(excelApp);
        }

        private void ReleaseExcelObject(object obj)
        {
            try
            {
                if (obj != null)
                {
                    Marshal.ReleaseComObject(obj);
                    obj = null;
                }
            }
            catch (Exception ex)
            {
                obj = null;
                throw ex;
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}
