using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesProcess.Libs
{
    public class LoadExcel
    {
        // wCellValue METHOD TO GET THE CELL VALUE FROM SPECIFIED EXCEL WORKSHEET USING THE ROW INDEX AND COLUM INDEX
        public static string wCellValue(ExcelWorksheet workSheet, int rowIndex, int columnIndex)
        {
            // TRY CATCH BLOCK TO CATCH NULLREFERENCE EXCEPTIONS FOR CELLS WITH NULL VALUE
            try
            {
                return workSheet.Cells[rowIndex, columnIndex].Value.ToString();
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }

        // wCellValue METHOD TO GET THE CELL VALUES FROM SPECIFIED EXCEL WORKSHEET USING THE ROW INDEX AND RANGE FOR COLUMN INDEXES: USED TO COLLECT ARRAY VALUES ON AN EXCEL ROW
        public static int[] wCellValue(ExcelWorksheet workSheet, int rowIndex, int columnIndexBegin, int columnIndexEnd)
        {
            int[] arr = new int[(columnIndexEnd - columnIndexBegin) + 1];
            for (int i = 0; i < arr.Length; i++)
            {
                // TO BREAK THE FOR LOOP FROM GETTING VALUES WHEN IT LOCATES A NULL LOCATION IN THE FIRST CELL
                if (wCellValue(workSheet, rowIndex, columnIndexBegin + 1) == null)
                {
                    break;
                }
                arr[i] = int.Parse(wCellValue(workSheet, rowIndex, columnIndexBegin + i));
            }
            return arr;
        }
    }
}
