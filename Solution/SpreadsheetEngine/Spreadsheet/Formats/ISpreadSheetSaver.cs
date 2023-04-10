// <copyright file="ISpreadSheetSaver.cs" company="Jose Robles">
// Copyright (c) Jose Robles. All Rights Reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine.Spreadsheet.Formats
{
    /// <summary>
    /// Interface for saving and loading spreadsheet objects.
    /// </summary>
    public interface ISpreadSheetSaver
    {
        /// <summary>
        /// Save to a file.
        /// </summary>
        /// <param name="stream"> stream. </param>
        void Save(Stream stream);

        /// <summary>
        /// Load from a file.
        /// </summary>
        /// <param name="stream"> stream. </param>
        void Load(Stream stream);
    }
}
