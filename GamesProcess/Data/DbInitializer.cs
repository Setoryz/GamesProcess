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
        /// <summary>
        /// Method to initialize Database with Datas from excel workbooks. 
        /// </summary>
        /// <param name="context"></param>
        public static void Initialize(GameContext context)
        {
            //context.Database.EnsureCreated();

            if (context.Games.Any())
            {
                return; //DB has been seeded before
            }

            var games = new List<Game> {
                new Game { ID = 1, Name = "Monday Special" },
                new Game { ID = 2, Name = "Lucky Tuesday" },
                new Game { ID = 3, Name = "Midweek Wednesday"},
                new Game { ID = 4, Name = "Fortune Thursday" },
                new Game { ID = 5, Name = "Friday Bonanza"},
                new Game { ID = 6, Name = "National Saturday" }
            };
            foreach (Game game in games)
            {
                context.Games.Add(game);
            }
            context.SaveChanges();

            // CODE TO ADD THE EXCEL FILE TO DATABASE
            string workbookFilePath1 = "C:\\Users\\ODUKOYA JESUSEYITAN\\Documents\\NF\\NR\\Load\\mondaySpecialDB.xlsx";
            string workbookFilePath2 = "C:\\Users\\ODUKOYA JESUSEYITAN\\Documents\\NF\\NR\\Load\\luckyTuesdayDB.xlsx";
            string workbookFilePath3 = "C:\\Users\\ODUKOYA JESUSEYITAN\\Documents\\NF\\NR\\Load\\midweekWednesdayDB.xlsx";
            string workbookFilePath4 = "C:\\Users\\ODUKOYA JESUSEYITAN\\Documents\\NF\\NR\\Load\\fortuneThursdayDB.xlsx";
            string workbookFilePath5 = "C:\\Users\\ODUKOYA JESUSEYITAN\\Documents\\NF\\NR\\Load\\bonanzaFridayDB.xlsx"; // FILE LOCATION
            string workbookFilePath6 = "C:\\Users\\ODUKOYA JESUSEYITAN\\Documents\\NF\\NR\\Load\\nationalDB.xlsx";
            var workbookFileInfos = new List<FileInfo> {
                new FileInfo(workbookFilePath1),
                new FileInfo(workbookFilePath2),
                new FileInfo(workbookFilePath3),
                new FileInfo(workbookFilePath4),
                new FileInfo(workbookFilePath5),
                new FileInfo(workbookFilePath6)
            }; // PASS FILE LOCATION INTO FILE INFORMATION

            int i = 1;
            int j = 1;
            foreach (FileInfo workbookFileInfo in workbookFileInfos)
            {
                //var Events = new List<Event>();
                using (ExcelPackage excelPackage = new ExcelPackage(workbookFileInfo))
                {
                    for (int currentIndex = 1; currentIndex <= excelPackage.Workbook.Worksheets.Count; currentIndex++)
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

                            //context.Events.Add(
                            context.Events.Add(
                                new Event
                                {
                                    EventID = j,
                                    EventNumber = int.Parse(LoadExcel.wCellValue(workSheet, rowIndex, 1)),
                                    GameID = i,
                                    Date = ConvOADate.FromString(LoadExcel.wCellValue(workSheet, rowIndex, 2)),
                                    Winning = LoadExcel.wCellValue(workSheet, rowIndex, 3, 7),
                                    Machine = LoadExcel.wCellValue(workSheet, rowIndex, 9, 13)
                                });
                            context.SaveChanges();
                            j++;
                        }

                    }
                }
                //foreach (Event eventData in Events)
                //{
                //    context.Events.Add(eventData);
                //}
                context.SaveChanges();
                i++;
            }

        }
    }
}
