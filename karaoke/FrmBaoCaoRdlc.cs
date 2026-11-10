using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace karaoke
{
    public sealed class ReportLine
    {
        public string Text { get; set; }
    }
    public sealed partial class FrmBaoCaoRdlc : Form
    {
        public FrmBaoCaoRdlc()
        {
            InitializeComponent();
        }
        public FrmBaoCaoRdlc(string title, IEnumerable<string> lines) : this()
        {
            Text = title;
            using (var definition = typeof(FrmBaoCaoRdlc).Assembly.GetManifestResourceStream("karaoke.Reports.ReportLines.rdlc"))
            {
                if (definition == null) throw new InvalidOperationException("Không tìm thấy mẫu báo cáo RDLC.");
                viewer.LocalReport.LoadReportDefinition(definition);
            }
            viewer.LocalReport.DataSources.Add(new ReportDataSource("Lines", lines.Select(x => new ReportLine { Text = x ?? "" }).ToList()));
            Load += (s, e) => viewer.RefreshReport();
        }
    }
}
