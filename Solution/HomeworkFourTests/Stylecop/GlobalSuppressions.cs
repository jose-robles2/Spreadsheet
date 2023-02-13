// <copyright file="GlobalSuppressions.cs" company="Jose Robles">
// Copyright (c) Jose Robles. All Rights Reserved.
// </copyright>

// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.
using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1401:Field should be private", Justification = "<Fields of the CellTest.cs class needed to be protected, and this stylecop warning needed to be suppressed in order to comply with the requirements for this assignment.>", Scope = "type", Target = "~T:SpreadsheetEngine.TestClasses.CellTest")]
