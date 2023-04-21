// <copyright file="ExpressionTree.cs" company="Jose Robles">
// Copyright (c) Jose Robles. All Rights Reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpreadsheetEngine.Spreadsheet;

namespace Tests.SpreadsheetEngineTests.SpreadsheetTests
{
    internal class BadReferenceTests
    {
        [Test]
        public void SelfReferenceTest()
        {
            Spreadsheet spreadsheet = new Spreadsheet(1, 1);
            Cell cell = spreadsheet.GetCell(0, 0);
            cell.Text = "=A1";
            Assert.That(cell.Value, Is.EqualTo(Cell.SELFREFERENCESTR));
        }

        [Test]
        public void UnknownCellTest()
        {
            Spreadsheet spreadsheet = new Spreadsheet(1, 1);
            Cell cell = spreadsheet.GetCell(0, 0);
            cell.Text = "=A55264355241";
            Assert.That(cell.Value, Is.EqualTo(Cell.BADREFERENCESTR));
        }

        [Test]
        public void UnknownCellTest2()
        {
            Spreadsheet spreadsheet = new Spreadsheet(1, 1);
            Cell cell = spreadsheet.GetCell(0, 0);
            cell.Text = "=A15";
            Assert.That(cell.Value, Is.EqualTo(Cell.BADREFERENCESTR));
        }

        [Test]
        public void UnknownCellTest3()
        {
            Spreadsheet spreadsheet = new Spreadsheet(1, 1);
            Cell cell = spreadsheet.GetCell(0, 0);
            cell.Text = "=A4";
            Assert.That(cell.Value, Is.EqualTo(Cell.BADREFERENCESTR));
        }

        [Test]
        public void UnknownOperatorTest()
        {
            Spreadsheet spreadsheet = new Spreadsheet(1, 3);
            Cell cell = spreadsheet.GetCell(0, 0);
            cell.Text = "=A2&A3";
            Assert.That(cell.Value, Is.EqualTo(Cell.UNKNOWNOPERATORSTR));
        }

        [Test]
        public void UnknownOperatorTest2()
        {
            Spreadsheet spreadsheet = new Spreadsheet(1, 3);
            Cell cell = spreadsheet.GetCell(0, 0);
            cell.Text = "=A2#A3";
            Assert.That(cell.Value, Is.EqualTo(Cell.UNKNOWNOPERATORSTR));
        }

        [Test]
        public void CircularReferenceTest()
        {
            Spreadsheet spreadsheet = new Spreadsheet(2, 2);
            spreadsheet.GetCell("B2").Text = "=A2";
            spreadsheet.GetCell("A2").Text = "=A1";
            spreadsheet.GetCell("A1").Text = "=B1";
            spreadsheet.GetCell("B1").Text = "=B2";

            Assert.That(spreadsheet.GetCell("B1").Value, Is.EqualTo(Cell.CIRCULARREFERENCESTR));
        }

        [Test]
        public void CircularReferenceTest2()
        {
            Spreadsheet spreadsheet = new Spreadsheet(3, 3);
            spreadsheet.GetCell("A1").Text = "=B1";
            spreadsheet.GetCell("B1").Text = "=A3";
            spreadsheet.GetCell("A3").Text = "=B2";
            spreadsheet.GetCell("B2").Text = "=C1";
            spreadsheet.GetCell("C1").Text = "=B1";

            Assert.That(spreadsheet.GetCell("C1").Value, Is.EqualTo(Cell.CIRCULARREFERENCESTR));
        }
    }
}
