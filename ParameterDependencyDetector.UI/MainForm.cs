using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ParameterDependencyDetector.Core;
using ParameterDependencyDetector.UI.Database;

namespace ParameterDependencyDetector.UI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            PopulateProcedures();
        }

        private async void PopulateProcedures()
        {
            var foundStoredProcedures = await DatabaseSingleton.Instance.ListSystemStoredProcedures();

            this.uxStoredProcedures.DataSource = foundStoredProcedures;
            this.uxDetect.Enabled = foundStoredProcedures.Any();
        }

        private void DetectClick(object sender, EventArgs e)
        {
            if (this.uxStoredProcedures.SelectedItem == null)
                return;

            this.uxStoredProcedures.Enabled = false;
            this.uxDetect.Enabled = false;
            this.FindUsages((string)this.uxStoredProcedures.SelectedItem);
        }

        private async void FindUsages(string procedureName)
        {
            this.uxGrid.Visible = false;

            this.uxProgress.Value = 0;
            this.uxProgress.Visible = true;

            var parameterDetector = new ParameterDetector(DatabaseSingleton.Instance);
            parameterDetector.DetectParametersForProcedure(procedureName);

            this.SetupGrid(parameterDetector);

            var foundUsages = await DatabaseSingleton.Instance.ListUsages(procedureName);

            this.uxProgress.Maximum = foundUsages.Count();

            foreach (var parser in foundUsages.Select(usage => new StoredProcedureParser(DatabaseSingleton.Instance, usage, parameterDetector)))
            {
                foreach (var row in await parser.DetectUsages(procedureName))
                {
                    this.AddRowToGrid(row);
                }

                this.uxProgress.Value += 1;
            }

            this.uxDetect.Enabled = true;
            this.uxStoredProcedures.Enabled = true;
            this.uxProgress.Visible = false;
            this.uxGrid.Visible = true;
        }

        private void AddRowToGrid(Dictionary<string, string> row)
        {
            var newRow = new DataGridViewRow();

            foreach (var val in row)
            {
                newRow.Cells.Add(new DataGridViewTextBoxCell { Value = val.Value });
            }

            this.uxGrid.Rows.Add(newRow);
        }

        private void SetupGrid(ParameterDetector parameterDetector)
        {
            this.uxGrid.Columns.Clear();

            foreach (var parameter in parameterDetector.Parameters)
            {
                this.uxGrid.Columns.Add(parameter, parameter);
            }
        }
    }
}
