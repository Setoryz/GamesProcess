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
            //context.Database.EnsureCreated();

            if (context.GamesClass.Any())
            {
                return;
            }

            var gamesClass = new List<GamesClass>
            {
                new GamesClass { id = 1, Name = "Premiere Games"},
                new GamesClass { id = 2, Name = "Ghana Games"},
                new GamesClass { id =3, Name =  "Other Games"}
            };

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
                new Game { ID = 6, Name = "National Saturday" },
                new Game { ID = 7, Name = "Mark II"},
                new Game { ID = 8, Name = "06 Premier"},
                new Game { ID = 9, Name = "Premier Club Master"}
            };
            foreach (Game game in games)
            {
                context.Games.Add(game);
            }
            context.SaveChanges();

            // CODE TO ADD THE EXCEL FILE TO DATABASE
            string workbookFilePath1 = "C:\\Users\\Home\\Documents\\NF\\NR\\Load\\mondaySpecialDB.xlsx";
            string workbookFilePath2 = "C:\\Users\\Home\\Documents\\NF\\NR\\Load\\luckyTuesdayDB.xlsx";
            string workbookFilePath3 = "C:\\Users\\Home\\Documents\\NF\\NR\\Load\\midweekWednesdayDB.xlsx";
            string workbookFilePath4 = "C:\\Users\\Home\\Documents\\NF\\NR\\Load\\fortuneThursdayDB.xlsx";
            string workbookFilePath5 = "C:\\Users\\Home\\Documents\\NF\\NR\\Load\\bonanzaFridayDB.xlsx"; // FILE LOCATION
            string workbookFilePath6 = "C:\\Users\\Home\\Documents\\NF\\NR\\Load\\nationalDB.xlsx";
            string workbookFilePath7 = "C:\\Users\\Home\\Documents\\NF\\NR\\Load\\markIIDB.xlsx";
            string workbookFilePath8 = "C:\\Users\\Home\\Documents\\NF\\NR\\Load\\06premierDB.xlsx";
            string workbookFilePath9 = "C:\\Users\\Home\\Documents\\NF\\NR\\Load\\premierClubMasterDB.xlsx";
            var workbookFileInfos = new List<FileInfo> {
                new FileInfo(workbookFilePath1),
                new FileInfo(workbookFilePath2),
                new FileInfo(workbookFilePath3),
                new FileInfo(workbookFilePath4),
                new FileInfo(workbookFilePath5),
                new FileInfo(workbookFilePath6),
                new FileInfo(workbookFilePath7),
                new FileInfo(workbookFilePath8),
                new FileInfo(workbookFilePath9)
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
