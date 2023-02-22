// <copyright file="GetCellTests.cs" company="Jose Robles">
// Copyright (c) Jose Robles. All Rights Reserved.
// </copyright>

using HomeworkFour;
using HomeworkFourTests.SpreadsheetEngineTests;
using SpreadsheetEngine;
using System.Runtime.CompilerServices;

namespace HomeworkFourTests.SpreadsheetEngineTests.Tests
{
    /// <summary>
    /// Class for tests for the SpreadsheetEngine.GetCell() method
    /// </summary>
    public class GetCellTests
    {
        /// <summary>
        /// 2x2 string array containing text values.
        /// </summary>
        private string[,] content;

        /// <summary>
        /// Spreadsheet test object.
        /// </summary>
        private Spreadsheet spreadsheet;

        /// <summary>
        /// Setup function used to setup different objects needed for testing.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.content = CreateContent();
            this.spreadsheet = CreateSpreadsheet();
            this.InitializeSpreadsheet();
        }

        /// <summary>
        /// Setup function to create a 2D array of content strings.
        /// </summary>
        /// <returns> 2D String array. </returns>
        private static string[,] CreateContent()
        {
            string[,] content = new string[2, 2];
            content[0, 0] = "hello";
            content[0, 1] = "C";
            content[1, 0] = "sharp";
            content[1, 1] = "world";
            return content;
        }

        /// <summary>
        /// Setup function to create a Spreadsheet containing a 2D array of concrete cells.
        /// </summary>
        /// <returns> Spreadsheet object. </returns>
        private static Spreadsheet CreateSpreadsheet()
        {
            return new Spreadsheet(2, 2);
        }

        /// <summary>
        /// Setup function to initialize the Spreadsheet cells.
        /// </summary>
        private void InitializeSpreadsheet()
        {
            if (this.spreadsheet != null)
            {
                this.SetText(0, 0);
                this.SetText(0, 1);
                this.SetText(1, 0);
                this.SetText(1, 1);
            }
        }

        /// <summary>
        /// Helper function to set text of cell's at certain coordinates.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        private void SetText(int row, int col)
        {
            Cell? cell = this.spreadsheet.GetCell(row, col);
            if (cell != null)
            {
                cell.Text = this.content[row, col];
            }
        }
        
        /// <summary>
        /// Test for Spreadsheet.GetCell(int row, int col) under normal conditions.
        /// </summary>
        [Test]
        public void GetCellTestNormal()
        {
            Cell? cell = this.spreadsheet.GetCell(0, 0);
            Assert.That(cell?.Text, Is.EqualTo(this.content[0,0]));
        }

        /// <summary>
        /// Test for Spreadsheet.GetCell(string name) under normal conditions.
        /// </summary>
        [Test]
        public void GetCellTestOverloadedNormal()
        {
            Cell cell = this.spreadsheet.GetCell("A1");
            Assert.That(cell?.Text, Is.EqualTo(this.content[0, 0]));
        }

        /// <summary>
        /// Test for Spreadsheet.GetCell(int row, int col) under edge conditions.
        /// </summary>
        [Test]
        public void GetCellTestEdge()
        {
            Cell? cell = this.spreadsheet.GetCell(1, 1);
            Assert.That(cell?.Text, Is.EqualTo(this.content[1,1]));
        }

        /// <summary>
        /// Test for Spreadsheet.GetCell(string name) under edge conditions.
        /// </summary>
        [Test]
        public void GetCellTestOverloadedEdge()
        {
            Cell cell = this.spreadsheet.GetCell("B2");
            Assert.That(cell?.Text, Is.EqualTo(this.content[1, 1]));
        }

        /// <summary>
        /// Test for Spreadsheet.GetCell(int row, int col) under exception conditions.
        /// </summary>
        [Test]
        public void GetCellTestException()
        {
            Assert.Throws<ArgumentException>(() => this.spreadsheet.GetCell(int.MaxValue, int.MaxValue));
        }

        /// <summary>
        /// Test for Spreadsheet.GetCell(string name) under exception conditions.
        /// </summary>
        [Test]
        public void GetCellTestOverloadedException()
        {
            Assert.Throws<KeyNotFoundException>(() => this.spreadsheet.GetCell("nonExistent"));
        }
    }
}