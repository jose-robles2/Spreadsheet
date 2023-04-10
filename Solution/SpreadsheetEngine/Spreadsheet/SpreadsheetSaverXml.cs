// <copyright file="SpreadsheetSaverXml.cs" company="Jose Robles">
// Copyright (c) Jose Robles. All Rights Reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// Save to a file using xml.
        /// </summary>
        /// <param name="stream"> stream. </param>
        public void Save(Stream stream)
        {
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
