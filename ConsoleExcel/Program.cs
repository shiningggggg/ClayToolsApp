using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleExcel
{
    class Program
    {
        static void Main(string[] args)
        {
            Microsoft.Office.Interop.Excel.Application myExcel;
            Microsoft.Office.Interop.Excel.Workbooks myWorkBooks;
            Microsoft.Office.Interop.Excel.Workbook myWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet workSheet;
            try
            {
                object miss = Missing.Value;
                myExcel = new Microsoft.Office.Interop.Excel.Application();
                //打开一个模板
                //excelApp.Workbooks.Open("文件名", miss, true, miss, miss, miss, miss, miss, miss, miss, miss, miss, miss, miss, miss);
                myWorkBooks = myExcel.Workbooks;
                myWorkBook = myWorkBooks.Add(miss);

                myExcel.Sheets.Add(miss, miss, miss, miss);

                workSheet = (Worksheet)myWorkBook.Worksheets[1];
                int rowCount = 20;
                int colCount = 5;
                object[,] dataArray = new object[rowCount, colCount];
                Random ran = new Random(DateTime.Now.Millisecond);
                for (int i = 0; i < rowCount; i++)
                {
                    for (int j = 0; j < colCount; j++)
                    {
                        dataArray[i, j] = i + j;
                    }
                }
                workSheet.get_Range(workSheet.Cells[1, 1], workSheet.Cells[rowCount, colCount]).Value2 = dataArray;

                Range range = workSheet.get_Range(workSheet.Cells[1, 1], workSheet.Cells[1, colCount]);
                range.Interior.Color = 255;//设置区域背景色
                range.Font.Bold = true; //设置字体粗体
                range.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThick, XlColorIndex.xlColorIndexAutomatic, 15);//设置区域边框

                //设置图标大小
                ChartObjects charts = (ChartObjects)workSheet.ChartObjects();
                ChartObject chartObj = charts.Add(0, 0, 400, 300);
                Chart chart = chartObj.Chart;
                //设置图标数据区域
                Range rangeChart = workSheet.get_Range("A1", "E10");
                chart.ChartWizard(rangeChart, XlChartType.xl3DColumn, miss, XlRowCol.xlColumns, 1, 1, true, "标题", "X轴标题", "Y轴标题", miss);
                //将图标移至数据区域之下
                chartObj.Left = Convert.ToDouble(range.Left);
                chartObj.Top = Convert.ToDouble(range.Top) + Convert.ToDouble(range.Height);

                //Workbook workbook = excelApp.Workbooks[1];
                myWorkBook.RefreshAll();

                //通过打开模板的方式，并且没有设置只读
                //workbook.Save();
                myWorkBook.SaveAs("123.xls", miss, miss, miss, miss, miss, XlSaveAsAccessMode.xlNoChange, miss, miss, miss, miss, miss);

                //关闭
                myWorkBook.Close(false, miss, miss);
                myWorkBook = null;

                //清空内存
                myExcel.Quit();
                myExcel = null;
                GC.Collect();

                Console.WriteLine("OK");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.Read();
        }
    }
}
