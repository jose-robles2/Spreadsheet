// <copyright file="SpreadsheetSaverXml.cs" company="Jose Robles">
// Copyright (c) Jose Robles. All Rights Reserved.
// </copyright>

using System;
using System.Collections.Generic;
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
        ///         <cellAttrN>
        ///
        ///         </cellAttrN>
        ///     </cell>
        /// </spreadsheet>
        /// </summary>
        /// <param name="stream"> stream. </param>
        public void Save(Stream stream)
        {
            XmlWriter xmlWriter = XmlWriter.Create(stream);

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("spreadsheet");

            foreach (Cell cell in this.spreadsheet.Matrix)
            {
                // Only save cells that have been edited
                if (cell.Text != string.Empty || cell.BGColor != Cell.DEFAULTCOLOR)
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
        public void Load(Stream stream)
        {
        }
    }
}
