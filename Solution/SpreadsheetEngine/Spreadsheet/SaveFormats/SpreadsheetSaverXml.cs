// <copyright file="SpreadsheetSaverXml.cs" company="Jose Robles">
// Copyright (c) Jose Robles. All Rights Reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using SpreadsheetEngine.Spreadsheet.Formats;

namespace SpreadsheetEngine.Spreadsheet
{
    /// <summary>
    /// Pure fabrication saving class that can save/load using xml.
    /// </summary>
    public class SpreadsheetSaverXml : ISpreadSheetSaver
    {
        /// <summary>
        /// Spreadsheet that we are saving/loading.
        /// </summary>
        private Spreadsheet spreadsheet;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpreadsheetSaverXml"/> class.
        /// </summary>
        /// <param name="spreadsheet"> spreadsheet. </param>
        public SpreadsheetSaverXml(Spreadsheet spreadsheet)
        {
            this.spreadsheet = spreadsheet;
        }

        /// <summary>
        /// Save/write to a file using xml. Xmlwriter is utilized to create an xml structure of.
        /// <spreadsheet>
        ///     <cell>
        ///         <cellAttrN> </cellAttrN>
        ///     </cell>
        /// </spreadsheet>
        /// </summary>
        /// <param name="stream"> stream. </param>
        public void Save(Stream stream)
        {
            XmlWriter xmlWriter = XmlWriter.Create(stream);

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("spreadsheet");

            foreach (Cell cell in this.spreadsheet.ChangedCells)
            {
                xmlWriter.WriteStartElement("cell");
                xmlWriter.WriteAttributeString("name", cell.Name);

                xmlWriter.WriteStartElement("text");
                xmlWriter.WriteString(cell.Text);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("bgcolor");
                xmlWriter.WriteString(cell.BGColor.ToString("X"));
                xmlWriter.WriteEndElement();

                xmlWriter.WriteEndElement();
            }

            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Flush();
            xmlWriter.Close();
        }

        /// <summary>
        /// Load to a file using xml.
        /// </summary>
        /// <param name="stream"> stream. </param>
        /// <returns> spreadsheet. </returns>
        public Spreadsheet Load(Stream stream)
        {
            this.ClearSpreadsheet();

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(stream);

            XmlNode? xmlRootNode = xmlDocument.SelectSingleNode("spreadsheet");

            if (xmlRootNode == null)
            {
                return null;
            }

            foreach (XmlNode childNode in xmlRootNode.ChildNodes)
            {
                XmlElement element = (XmlElement)childNode;

                if (element.Name != "cell")
                {
                    continue;
                }

                string cellName = element.GetAttribute("name");
                Cell? cell = this.spreadsheet.GetCell(cellName);

                if (cell == null)
                {
                    continue;
                }

                foreach (XmlNode nestedChildNode in childNode.ChildNodes)
                {
                    XmlElement childElement = (XmlElement)nestedChildNode;

                    if (childElement.Name == "text")
                    {
                        cell.Text = childElement.InnerText;
                    }
                    else if (childElement.Name == "bgcolor")
                    {
                        string color = childElement.InnerText;
                        cell.BGColor = uint.Parse(color, NumberStyles.HexNumber);
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            return this.spreadsheet;
        }

        /// <summary>
        /// Clear the spreadsheet since a load overwrites the current sheet. Don't create
        /// a new object because then we'll have to resub. the delegates to events. Other
        /// spreadsheet members will be cleared within the spreadsheet object.
        /// </summary>
        private void ClearSpreadsheet()
        {
            foreach (Cell changedCell in this.spreadsheet.ChangedCells)
            {
                changedCell.Text = string.Empty;
                changedCell.BGColor = Cell.DEFAULTCOLOR;
            }
        }
    }
}