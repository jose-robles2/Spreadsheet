// <copyright file="ExpressionTree.cs" company="Jose Robles">
// Copyright (c) Jose Robles. All Rights Reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SpreadsheetEngine.Spreadsheet;

namespace Tests.SpreadsheetEngineTests.SpreadsheetTests.SaveFormatTests.SpreadsheetSaverXML
{
    internal class SaveLoadTests
    {
        [Test]
        public void SaveLoadTest1()
        {
            // Create a new Spreadsheet with data
            Spreadsheet spreadsheet = new Spreadsheet(1, 2);
            spreadsheet.GetCell(0, 0).Text = "hi";
            spreadsheet.GetCell(0, 1).Text = "there";

            // Create a MemoryStream to hold the XML data
            using (MemoryStream stream = new MemoryStream())
            {
                SpreadsheetSaverXml spreadsheetSaver = new SpreadsheetSaverXml(spreadsheet);
                spreadsheetSaver.Save(stream);

                stream.Position = 0;

                Spreadsheet result = spreadsheetSaver.Load(stream);

                Assert.AreEqual(spreadsheet.GetCell(0, 0).Text, result.GetCell(0, 0).Text);
                Assert.AreEqual(spreadsheet.GetCell(0, 1).Text, result.GetCell(0, 1).Text);
            }
        }

        [Test]
        public void SaveAndLoadEmptySpreadsheet()
        {
            // Create an empty Spreadsheet
            Spreadsheet spreadsheet = new Spreadsheet(0, 0);

            // Create a MemoryStream to hold the XML data
            using (MemoryStream stream = new MemoryStream())
            {
                SpreadsheetSaverXml spreadsheetSaver = new SpreadsheetSaverXml(spreadsheet);
                spreadsheetSaver.Save(stream);

                stream.Position = 0;

                Spreadsheet loadedSpreadsheet = spreadsheetSaver.Load(stream);

                Assert.AreEqual(spreadsheet.RowCount, loadedSpreadsheet.RowCount);
                Assert.AreEqual(spreadsheet.ColumnCount, loadedSpreadsheet.ColumnCount);
            }
        }

        [Test]
        public void SaveAndLoadSpreadsheetWithMultipleRowsAndColumns()
        {
            // Create a new Spreadsheet with data
            Spreadsheet spreadsheet = new Spreadsheet(3, 4);
            spreadsheet.GetCell(0, 0).Text = "A1";
            spreadsheet.GetCell(0, 1).Text = "B1";
            spreadsheet.GetCell(0, 2).Text = "C1";
            spreadsheet.GetCell(0, 3).Text = "D1";
            spreadsheet.GetCell(1, 0).Text = "A2";
            spreadsheet.GetCell(1, 1).Text = "B2";
            spreadsheet.GetCell(1, 2).Text = "C2";
            spreadsheet.GetCell(1, 3).Text = "D2";
            spreadsheet.GetCell(2, 0).Text = "A3";
            spreadsheet.GetCell(2, 1).Text = "B3";
            spreadsheet.GetCell(2, 2).Text = "C3";
            spreadsheet.GetCell(2, 3).Text = "D3";

            // Create a MemoryStream to hold the XML data
            using (MemoryStream stream = new MemoryStream())
            {
                SpreadsheetSaverXml spreadsheetSaver = new SpreadsheetSaverXml(spreadsheet);
                spreadsheetSaver.Save(stream);

                stream.Position = 0;

                Spreadsheet loadedSpreadsheet = spreadsheetSaver.Load(stream);

                Assert.AreEqual(spreadsheet.RowCount, loadedSpreadsheet.RowCount);
                Assert.AreEqual(spreadsheet.ColumnCount, loadedSpreadsheet.ColumnCount);
                for (int row = 0; row < spreadsheet.RowCount; row++)
                {
                    for (int col = 0; col < spreadsheet.ColumnCount; col++)
                    {
                        Assert.AreEqual(spreadsheet.GetCell(row, col).Text, loadedSpreadsheet.GetCell(row, col).Text);
                    }
                }
            }
        }

        [Test]
        public void SaveAndLoadSpreadsheetWithCellColors()
        {
            // Create a new Spreadsheet with data and cell colors
            Spreadsheet spreadsheet = new Spreadsheet(2, 2);
            spreadsheet.GetCell(0, 0).Text = "A1";
            spreadsheet.GetCell(0, 0).BGColor = (uint)Color.Red.ToArgb();
            spreadsheet.GetCell(0, 1).Text = "B1";
            spreadsheet.GetCell(0, 1).BGColor = (uint)Color.Green.ToArgb();
            spreadsheet.GetCell(1, 0).Text = "A2";
            spreadsheet.GetCell(1, 0).BGColor = (uint)Color.Blue.ToArgb();
            spreadsheet.GetCell(1, 1).Text = "B2";
            spreadsheet.GetCell(1, 1).BGColor = (uint)Color.Yellow.ToArgb();

            // Create a MemoryStream to hold the XML data
            using (MemoryStream stream = new MemoryStream())
            {
                SpreadsheetSaverXml spreadsheetSaver = new SpreadsheetSaverXml(spreadsheet);
                spreadsheetSaver.Save(stream);

                stream.Position = 0;

                Spreadsheet loadedSpreadsheet = spreadsheetSaver.Load(stream);

                Assert.AreEqual(spreadsheet.RowCount, loadedSpreadsheet.RowCount);
                Assert.AreEqual(spreadsheet.ColumnCount, loadedSpreadsheet.ColumnCount);
                for (int row = 0; row < spreadsheet.RowCount; row++)
                {
                    for (int col = 0; col < spreadsheet.ColumnCount; col++)
                    {
                        Assert.AreEqual(spreadsheet.GetCell(row, col).Text, loadedSpreadsheet.GetCell(row, col).Text);
                        Assert.AreEqual(spreadsheet.GetCell(row, col).BGColor, loadedSpreadsheet.GetCell(row, col).BGColor);
                    }
                }
            }
        }
    }
}