// <copyright file="SearchCellTests.cs" company="Jose Robles">
// Copyright (c) Jose Robles. All Rights Reserved.
// </copyright>

using HomeworkFour;
using HomeworkFourTests.SpreadsheetEngineTests.TestClasses;
using SpreadsheetEngine;

namespace HomeworkFourTests.SpreadsheetEngineTests.Tests
{
    /// <summary>
    /// Temp class tests.
    /// </summary>
    public class SearchCellTests
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
            this.spreadsheet.GetCell(0, 0).Text = this.content[0, 0];
            this.spreadsheet.GetCell(0, 1).Text = this.content[0, 1];
            this.spreadsheet.GetCell(1, 0).Text = this.content[1, 0];
            this.spreadsheet.GetCell(1, 1).Text = this.content[1, 1];
        }

        /// <summary>
        /// Test for Spreadsheet.SearchCell() under normal conditions.
        /// </summary>
        [Test]
        public void SearchCellTestNormal()
        {
            Cell cell = this.spreadsheet.GetCell("A1");
            Assert.That(cell?.Text, Is.EqualTo(this.content[0, 0]));
        }

        /// <summary>
        /// Test for Spreadsheet.SearchCell() under edge conditions.
        /// </summary>
        [Test]
        public void SearchCellTestEdge()
        {
            Cell cell = this.spreadsheet.GetCell("B2");
            Assert.That(cell?.Text, Is.EqualTo(this.content[1, 1]));
        }

        /// <summary>
        /// Test for Spreadsheet.SearchCell() under exception conditions.
        /// </summary>
        [Test]
        public void SearchCellTestException()
        {
            Assert.Throws<KeyNotFoundException>(() => this.spreadsheet.GetCell("nonExistent"));
        }
    }
}