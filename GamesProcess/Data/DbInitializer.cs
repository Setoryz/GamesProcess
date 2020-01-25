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
                new GamesClass { ID = 01, Name = "Gab Games"},
                new GamesClass { ID = 02, Name = "Ghana Games"},
                new GamesClass { ID = 03, Name = "Grand Lotto Games"},
                new GamesClass { ID = 04, Name = "Lagtech Games"},
                new GamesClass { ID = 05, Name = "Premier Games"},
                new GamesClass { ID = 06, Name = "Ritppong Games"},
                new GamesClass { ID = 07, Name = "Sky Games"},
                new GamesClass { ID = 08, Name = "Wesco Games"},
                new GamesClass { ID = 09, Name = "Winlot Games"},
                new GamesClass { ID = 10, Name = "Other Games"},
            };
            foreach (GamesClass gamesclass in gamesClass)
            {
                context.GamesClass.Add(gamesclass);
            }
            context.SaveChanges();


            var games = new List<Game> {
                // Gab
                new Game { ID = 0001, GamesClassID = 01, Name = "Gab Gold Lotto" },
                new Game { ID = 0002, GamesClassID = 01, Name = "Gab Maga Star"},
                new Game { ID = 0003, GamesClassID = 01, Name = "Gab Super Star"},
                new Game { ID = 0004, GamesClassID = 01, Name = "Gab Sure Key Lotto"},
                // Ghana
                new Game { ID = 0005, GamesClassID = 02, Name = "Ghana Bonaza"},
                new Game { ID = 0006, GamesClassID = 02, Name = "Ghana Fortune"},
                new Game { ID = 0007, GamesClassID = 02, Name = "Ghana Lukky"},
                new Game { ID = 0008, GamesClassID = 02, Name = "Ghana Mid Week"},
                new Game { ID = 0009, GamesClassID = 02, Name = "Ghana Monday Special"},
                new Game { ID = 0010, GamesClassID = 02, Name = "Ghana National"},
                // Grand Lotto
                new Game { ID = 0012, GamesClassID = 03, Name = "Grand Lotto 77"},
                new Game { ID = 0013, GamesClassID = 03, Name = "Grand Lotto Akwa Ibom"},
                new Game { ID = 0014, GamesClassID = 03, Name = "Grand Lotto Arena"},
                new Game { ID = 0015, GamesClassID = 03, Name = "Grand Lotto Dansaki"},
                new Game { ID = 0016, GamesClassID = 03, Name = "Grand Lotto Embassy"},
                new Game { ID = 0017, GamesClassID = 03, Name = "Grand Lotto Gold"},
                new Game { ID = 0018, GamesClassID = 03, Name = "Grand Lotto Jara"},
                new Game { ID = 0019, GamesClassID = 03, Name = "Grand Lotto Master"},
                new Game { ID = 0020, GamesClassID = 03, Name = "Grand Lotto Metro"},
                new Game { ID = 0021, GamesClassID = 03, Name = "Grand Lotto Pay Day"},
                new Game { ID = 0022, GamesClassID = 03, Name = "Grand Lotto Rambo"},
                new Game { ID = 0023, GamesClassID = 03, Name = "Grand Lotto Royal Chance"},
                new Game { ID = 0024, GamesClassID = 03, Name = "Grand Lotto Success"},
                new Game { ID = 0025, GamesClassID = 03, Name = "Grand Lotto Sunday Bonus"},
                new Game { ID = 0026, GamesClassID = 03, Name = "Grand Lotto Sunday Cannival"},
                new Game { ID = 0027, GamesClassID = 03, Name = "Grand Lotto Visa"},
                // Lagtech
                new Game { ID = 0030, GamesClassID = 04, Name = "Lagtech Don King Lotto"},
                new Game { ID = 0031, GamesClassID = 04, Name = "Lagtech Don King Van"},
                new Game { ID = 0032, GamesClassID = 04, Name = "Lagtech Instanta Lotto"},
                new Game { ID = 0033, GamesClassID = 04, Name = "Lagtech Tota Lotto"},
                // Premier
                new Game { ID = 0036, GamesClassID = 05, Name = "Premier 06"},
                new Game { ID = 0037, GamesClassID = 05, Name = "Premier Bingo"},
                new Game { ID = 0038, GamesClassID = 05, Name = "Premier Club Master"},
                new Game { ID = 0039, GamesClassID = 05, Name = "Premier Diamond"},
                new Game { ID = 0040, GamesClassID = 05, Name = "Premier Enugu"},
                new Game { ID = 0041, GamesClassID = 05, Name = "Premier Fair Chance"},
                new Game { ID = 0042, GamesClassID = 05, Name = "Premier Gold"},
                new Game { ID = 0043, GamesClassID = 05, Name = "Premier International"},
                new Game { ID = 0044, GamesClassID = 05, Name = "Premier Jacpot"},
                new Game { ID = 0045, GamesClassID = 05, Name = "Premier King"},
                new Game { ID = 0046, GamesClassID = 05, Name = "Premier Lucky"},
                new Game { ID = 0047, GamesClassID = 05, Name = "Premier Mark II"},
                new Game { ID = 0048, GamesClassID = 05, Name = "Premier Metro"},
                new Game { ID = 0049, GamesClassID = 05, Name = "Premier People's"},
                new Game { ID = 0050, GamesClassID = 05, Name = "Premier Royal"},
                new Game { ID = 0051, GamesClassID = 05, Name = "Premier Super"},
                new Game { ID = 0052, GamesClassID = 05, Name = "Premier Tota"},
                new Game { ID = 0053, GamesClassID = 05, Name = "Premier Vag"},
                // Ritppong
                new Game { ID = 0054, GamesClassID = 06, Name = "Ritppong 2010 Lotto"},
                new Game { ID = 0055, GamesClassID = 06, Name = "Ritppong Akwa Ibom"},
                new Game { ID = 0056, GamesClassID = 06, Name = "Ritppong Gold"},
                new Game { ID = 0057, GamesClassID = 06, Name = "Ritppong Victory Lotto"},
                new Game { ID = 0058, GamesClassID = 06, Name = "Ritppong Weekly Lotto"},
                // Sky
                new Game { ID = 0062, GamesClassID = 07, Name = "Sky A.T.M"},
                new Game { ID = 0063, GamesClassID = 07, Name = "Sky Alart"},
                new Game { ID = 0064, GamesClassID = 07, Name = "Sky Bonus"},
                new Game { ID = 0065, GamesClassID = 07, Name = "Sky Champion"},
                new Game { ID = 0066, GamesClassID = 07, Name = "Sky Diamond Lotto"},
                new Game { ID = 0067, GamesClassID = 07, Name = "Sky Favour"},
                new Game { ID = 0068, GamesClassID = 07, Name = "Sky Fountain"},
                new Game { ID = 0069, GamesClassID = 07, Name = "Sky Gold Lotto"},
                new Game { ID = 0070, GamesClassID = 07, Name = "Sky Legend"},
                new Game { ID = 0071, GamesClassID = 07, Name = "Sky Silver Lotto"},
                new Game { ID = 0072, GamesClassID = 07, Name = "Sky Success"},
                new Game { ID = 0073, GamesClassID = 07, Name = "Sky Sun Shine"},
                new Game { ID = 0074, GamesClassID = 07, Name = "Sky Sure"},
                new Game { ID = 0075, GamesClassID = 07, Name = "Sky Ultimate"},
                new Game { ID = 0076, GamesClassID = 07, Name = "Sky Vag Lotto"},
                // Wesco
                new Game { ID = 0077, GamesClassID = 08, Name = "Wesco America"},
                new Game { ID = 0078, GamesClassID = 08, Name = "Wesco ATM Lotto"},
                new Game { ID = 0079, GamesClassID = 08, Name = "Wesco Bank Lotto"},
                new Game { ID = 0080, GamesClassID = 08, Name = "Wesco Billion Lotto"},
                new Game { ID = 0081, GamesClassID = 08, Name = "Wesco Bonanza"},
                new Game { ID = 0082, GamesClassID = 08, Name = "Wesco Bonus Lotto" },
                new Game { ID = 0083, GamesClassID = 08, Name = "Wesco Canada"},
                new Game { ID = 0084, GamesClassID = 08, Name = "Wesco Cash Lotto"},
                new Game { ID = 0085, GamesClassID = 08, Name = "Wesco Cheque Lotto"},
                new Game { ID = 0086, GamesClassID = 08, Name = "Wesco Diamond Lotto"},
                new Game { ID = 0087, GamesClassID = 08, Name = "Wesco Dollar Lotto"},
                new Game { ID = 0088, GamesClassID = 08, Name = "Wesco Dubai"},
                new Game { ID = 0089, GamesClassID = 08, Name = "Wesco Euro Lotto"},
                new Game { ID = 0090, GamesClassID = 08, Name = "Wesco Fortune"},
                new Game { ID = 0091, GamesClassID = 08, Name = "Wesco Gold Lotto"},
                new Game { ID = 0092, GamesClassID = 08, Name = "Wesco International Lotto"},
                new Game { ID = 0093, GamesClassID = 08, Name = "Wesco Jacpot Lotto"},
                new Game { ID = 0094, GamesClassID = 08, Name = "Wesco Key Lotto"},
                new Game { ID = 0095, GamesClassID = 08, Name = "Wesco London DB"},
                new Game { ID = 0096, GamesClassID = 08, Name = "Wesco Lucky Lotto"},
                new Game { ID = 0097, GamesClassID = 08, Name = "Wesco Maga Lotto"},
                new Game { ID = 0098, GamesClassID = 08, Name = "Wesco Millions Lotto"},
                new Game { ID = 0099, GamesClassID = 08, Name = "Wesco Silver Lotto"},
                new Game { ID = 0100, GamesClassID = 08, Name = "Wesco Soccer Lotto"},
                new Game { ID = 0101, GamesClassID = 08, Name = "Wesco Special"},
                new Game { ID = 0102, GamesClassID = 08, Name = "Wesco Success"},
                new Game { ID = 0103, GamesClassID = 08, Name = "Wesco Super Lotto"},
                new Game { ID = 0104, GamesClassID = 08, Name = "Wesco Trophy Lotto"},
                // Winlot
                new Game { ID = 0108, GamesClassID = 09, Name = "Winlot Continental"},
                new Game { ID = 0109, GamesClassID = 09, Name = "Winlot Fantasy5"},
                new Game { ID = 0110, GamesClassID = 09, Name = "Winlot Formular"},
                new Game { ID = 0111, GamesClassID = 09, Name = "Winlot Gold Coast"},
                new Game { ID = 0112, GamesClassID = 09, Name = "Winlot International"},
                new Game { ID = 0113, GamesClassID = 09, Name = "Winlot Jubilee"},
                new Game { ID = 0114, GamesClassID = 09, Name = "Winlot Megga Cash"},
                new Game { ID = 0115, GamesClassID = 09, Name = "Winlot Rain Bow"},
                new Game { ID = 0116, GamesClassID = 09, Name = "Winlot Royal Cash"},
                new Game { ID = 0117, GamesClassID = 09, Name = "Winlot Special"},
                new Game { ID = 0118, GamesClassID = 09, Name = "Winlot Triple6"},
                // Other
                new Game { ID = 0011, GamesClassID = 10, Name = "Grand Havava"},
                new Game { ID = 0028, GamesClassID = 10, Name = "Grand Platinum"},
                new Game { ID = 0029, GamesClassID = 10, Name = "Grand Ttrophy"},
                new Game { ID = 0034, GamesClassID = 10, Name = "Network Biggy Lotto"},
                new Game { ID = 0035, GamesClassID = 10, Name = "Network House Master Lotto"},
                new Game { ID = 0059, GamesClassID = 10, Name = "Royal 06 Lotto"},
                new Game { ID = 0060, GamesClassID = 10, Name = "Royal A1 Lotto"},
                new Game { ID = 0061, GamesClassID = 10, Name = "Royal Vag Lotto"},
                new Game { ID = 0105, GamesClassID = 10, Name = "Western Option"},
                new Game { ID = 0106, GamesClassID = 10, Name = "Western Power"},
                new Game { ID = 0107, GamesClassID = 10, Name = "Western Splash 5L90" }
            };
            foreach (Game game in games)
            {
                context.Games.Add(game);
            }
            context.SaveChanges();

            // CODE TO ADD THE EXCEL FILE TO DATABASE
            string[] workbookFilePaths = new String[118];
            string FolderLocation = "C:\\Users\\ODUKOYA JESUSEYITAN\\Documents\\NF\\NR\\Load\\ALL GAMES\\";
            for (int currentLoop = 1; currentLoop <= games.Count; currentLoop++)
            {
                workbookFilePaths[currentLoop - 1] = $"{FolderLocation}{Trim.trim(games.Where(e => e.ID == currentLoop).SingleOrDefault()?.Name)}DB.xlsx";
            }

            #region Former Input

            //string workbookFilePath1 = "C:\\Users\\Home\\Documents\\NF\\NR\\Load\\mondaySpecialDB.xlsx";
            //string workbookFilePath2 = "C:\\Users\\Home\\Documents\\NF\\NR\\Load\\luckyTuesdayDB.xlsx";
            //string workbookFilePath3 = "C:\\Users\\Home\\Documents\\NF\\NR\\Load\\midweekWednesdayDB.xlsx";
            //string workbookFilePath4 = "C:\\Users\\Home\\Documents\\NF\\NR\\Load\\fortuneThursdayDB.xlsx";
            //string workbookFilePath5 = "C:\\Users\\Home\\Documents\\NF\\NR\\Load\\bonanzaFridayDB.xlsx"; // FILE LOCATION
            //string workbookFilePath6 = "C:\\Users\\Home\\Documents\\NF\\NR\\Load\\nationalDB.xlsx";
            //string workbookFilePath7 = "C:\\Users\\Home\\Documents\\NF\\NR\\Load\\markIIDB.xlsx";
            //string workbookFilePath8 = "C:\\Users\\Home\\Documents\\NF\\NR\\Load\\06premierDB.xlsx";
            //string workbookFilePath9 = "C:\\Users\\Home\\Documents\\NF\\NR\\Load\\premierClubMasterDB.xlsx";

            //var workbookFileInfos = new List<FileInfo> {
            //    new FileInfo(workbookFilePath1),
            //    new FileInfo(workbookFilePath2),
            //    new FileInfo(workbookFilePath3),
            //    new FileInfo(workbookFilePath4),
            //    new FileInfo(workbookFilePath5),
            //    new FileInfo(workbookFilePath6),
            //    new FileInfo(workbookFilePath7),
            //    new FileInfo(workbookFilePath8),
            //    new FileInfo(workbookFilePath9)
            //}; // PASS FILE LOCATION INTO FILE INFORMATION


            //int i = 1;
            //int j = 1;
            //foreach (FileInfo workbookFileInfo in workbookFileInfos)
            //{
            //    //var Events = new List<Event>();
            //    using (ExcelPackage excelPackage = new ExcelPackage(workbookFileInfo))
            //    {
            //        for (int currentIndex = 1; currentIndex <= excelPackage.Workbook.Worksheets.Count; currentIndex++)
            //        {
            //            var workSheet = excelPackage.Workbook.Worksheets[currentIndex]; // set current worksheet in workbook to var to load data
            //            int rowCount = workSheet.Dimension.Rows; // Calculate sum of rows
            //            int columnCount = workSheet.Dimension.Columns; // Calculate sum of column
            //            for (int rowIndex = 3; rowIndex <= rowCount; rowIndex++)
            //            {
            //                if (LoadExcel.wCellValue(workSheet, rowIndex, 1) == null)
            //                {
            //                    continue;
            //                }

            //                //context.Events.Add(
            //                context.Events.Add(
            //                    new Event
            //                    {
            //                        EventID = j,
            //                        EventNumber = int.Parse(LoadExcel.wCellValue(workSheet, rowIndex, 1)),
            //                        GameID = i,
            //                        Date = ConvOADate.FromString(LoadExcel.wCellValue(workSheet, rowIndex, 2)),
            //                        Winning = LoadExcel.wCellValue(workSheet, rowIndex, 3, 7),
            //                        Machine = LoadExcel.wCellValue(workSheet, rowIndex, 9, 13)
            //                    });
            //                context.SaveChanges();
            //                j++;
            //            }

            //        }
            //    }
            //}
            #endregion



            int i = 1;
            int j = 1;

            foreach (string workbookFilePath in workbookFilePaths)
            {
                var workbookFileInfo = new FileInfo(workbookFilePath);

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

                context.SaveChanges();
                i++;
            }
        }

    }
}

