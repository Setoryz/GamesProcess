using GamesProcess.Libs;
using GamesProcess.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GamesProcess.Data
{
    public class DbInitializer
    {
        public static void Initialize(GameContext context)
        {
            context.Database.EnsureCreated();

            if (context.Games.Any())
            {
                return; //DB has been seeded before
            }

            var games = new Game { ID = 1, Name = "National" };
            context.Games.Add(games);
            context.SaveChanges();
            // CODE TO ADD THE EXCEL FILE TO DATABASE
            string workbookFilePath = "C:\\Users\\ODUKOYA JESUSEYITAN\\Documents\\NF\\NR\\nationalDb.xlsx"; // FILE LOCATION
            var workbookFileInfo = new FileInfo(workbookFilePath); // PASS FILE LOCATION INTO FILE INFORMATION

            using (ExcelPackage excelPackage = new ExcelPackage(workbookFileInfo))
            {
                if (!context.Events.Any())
                {
                    for (int currentIndex = 1; currentIndex < excelPackage.Workbook.Worksheets.Count; currentIndex++)
                    {
                        var workSheet = excelPackage.Workbook.Worksheets[currentIndex]; // set current worksheet in workbook to var to load data
                        int rowCount = workSheet.Dimension.Rows; // Calculate sum of rows
                        int columnCount = workSheet.Dimension.Columns; // Calculate sum of column
                        for (int rowIndex = 3; rowIndex <= rowCount; rowIndex++)
                        {
                            if (LoadExcel.wCellValue(workSheet, rowIndex, 1) == null)
                            {
                                continue;
                            }
                            context.Events.Add(
                                new Event
                                {
                                    EventID = int.Parse(LoadExcel.wCellValue(workSheet, rowIndex, 1)),
                                    GameID = 1,
                                    Date = ConvOADate.FromString(LoadExcel.wCellValue(workSheet, rowIndex, 2)),
                                    Winning = LoadExcel.wCellValue(workSheet, rowIndex, 3, 7),
                                    Machine = LoadExcel.wCellValue(workSheet, rowIndex, 9, 13)
                                });
                        }

                    }
                }
            }

            context.SaveChanges();
        }
    }
}
