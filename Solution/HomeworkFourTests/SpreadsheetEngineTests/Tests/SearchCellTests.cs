// <copyright file="SearchCellTests.cs" company="Jose Robles">
// Copyright (c) Jose Robles. All Rights Reserved.
// </copyright>

using HomeworkFour;
using HomeworkFourTests.SpreadsheetEngineTests.TestClasses;

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
            return new SpreadsheetTest(2, 2);
        }

        /// <summary>
        /// Test for Spreadsheet.SearchCell() under normal conditions.
        /// </summary>
        [Test]
        public void SearchCellTestNormal()
        {
        }

        /// <summary>
        /// Test for Spreadsheet.SearchCell() under edge conditions.
        /// </summary>
        [Test]
        public void SearchCellTestEdge()
        {
        }

        /// <summary>
        /// Test for Spreadsheet.SearchCell() under exception conditions.
        /// </summary>
        [Test]
        public void SearchCellTestException()
        {
        }
    }
}