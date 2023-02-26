﻿// <copyright file="GlobalSuppressions.cs" company="Jose Robles">
// Copyright (c) Jose Robles. All Rights Reserved.
// </copyright>

// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.
using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1401:Field should be private", Justification = "<Fields of the Cell.cs class needed to be protected, and this stylecop warning needed to be suppressed in order to comply with the requirements for this assignment.>", Scope = "type", Target = "~T:SpreadsheetEngine.Cell")]
[assembly: SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1201:Elements should appear in the correct order", Justification = "<'Field should not follow a class' would arise from declaring a private class concretecell within class spreadsheet. This warning was suppressed so that our concretecell class could be the first thing declared within spreadsheet.cs so that we when opening spreadsheet.cs, we know right away that there is class declared inside of it, instead of finding out about it when scrolling hundreds of lines of code further.>", Scope = "member", Target = "~F:SpreadsheetEngine.Spreadsheet.rowCount")]
