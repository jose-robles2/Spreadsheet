// <copyright file="GetCellTests.cs" company="Jose Robles">
// Copyright (c) Jose Robles. All Rights Reserved.
// </copyright>

using HomeworkFour;
using SpreadsheetEngine;

namespace HomeworkFourTests
{
    /// <summary>
    /// Temp class tests.
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
        private SpreadsheetTest spreadsheet;

        /// <summary>
        /// Setup function used to setup different objects needed for testing.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.content = this.CreateContent();
            this.spreadsheet = this.CreateSpreadsheet(this.content);
        }

        /// <summary>
        /// Setup function to create a 2D array of content strings.
        /// </summary>
        /// <returns> 2D String array. </returns>
        public string[,] CreateContent()
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
        /// <param name="content"> 2D array of string content to set the cells with. </param>
        /// <returns> 2D Concrete cell array. </returns>
        public SpreadsheetTest CreateSpreadsheet(string[,] content)
        {
            int rows = content.Length, cols = content.Length;
            SpreadsheetTest s = new SpreadsheetTest(rows, cols);
            return s;
        }

        /// <summary>
        /// Test for Spreadsheet.GetClass() under normal conditions.
        /// </summary>
        [Test]
        public void GetCellTestNormal()
        {
            this.spreadsheet.SetCell(0, 0, this.content[0, 0]);
            Cell cell = this.spreadsheet.GetCell(0, 0);
            Assert.That(cell.Text, Is.EqualTo("hello"));
        }

        /// <summary>
        /// Test for Spreadsheet.GetClass() under edge conditions.
        /// </summary>
        [Test]
        public void GetCellTestEdge()
        {
            this.spreadsheet.SetCell(1, 1, this.content[1, 1]);
            Cell cell = this.spreadsheet.GetCell(1, 1);
            Assert.That(cell?.Text, Is.EqualTo("world"));
        }

        /// <summary>
        /// Test for Spreadsheet.GetClass() under exception conditions.
        /// </summary>
        [Test]
        public void GetCellTestException()
        {
            Assert.Throws<ArgumentException>(() => this.spreadsheet.GetCell(this.content.Length, this.content.Length));
        }
    }
}