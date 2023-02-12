// <copyright file="Form1.cs" company="Jose Robles">
// Copyright (c) Jose Robles. All Rights Reserved.
// </copyright>

namespace HomeworkFour
{
    /// <summary>
    /// MyForm class that points to the running instance of the form for this program.
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary
        public Form1()
        {
            this.InitializeComponent();
            this.InitializeDataGrid();
        }

        /// <summary>
        /// Initialize the data grid by creating columns A to Z. Create a list of 26 chars and convert
        /// to string so that they can be inserted as column name, and header using the Add() method.
        /// Then create 50 rows.
        /// </summary>
        private void InitializeDataGrid()
        {
            List<char> alphabet = Enumerable.Range('A', 26).Select(i => (char)i).ToList();

            foreach (char c in alphabet)
            {
                this.dataGridView1.Columns.Add(c.ToString(), c.ToString());
            }

            for (int i = 1; i <= 50; i++)
            {
                this.dataGridView1.Rows.Add(i.ToString());
            }
        }
    }
}