using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using karaoke;

class ReportRenderingTests
{
    [STAThread] static int Main()
    {
        try
        {
            var report = new LocalReport();
            using (var stream = typeof(FrmBaoCaoRdlc).Assembly.GetManifestResourceStream("karaoke.Reports.ReportLines.rdlc"))
            {
                if (stream == null) throw new Exception("Missing embedded RDLC definition");
                report.LoadReportDefinition(stream);
            }
            report.DataSources.Add(new ReportDataSource("Lines", Enumerable.Range(1, 150).Select(i => new ReportLine { Text = "Dòng dịch vụ số " + i }).ToList()));
            var bytes = report.Render("PDF");
            if (bytes.Length < 1000 || bytes[0] != '%' || bytes[1] != 'P') throw new Exception("Invalid PDF render");
            string output = Path.Combine(Path.GetTempPath(), "karaoke-rdlc-regression.pdf");
            File.WriteAllBytes(output, bytes);
            Console.WriteLine("PASS: RDLC rendered " + bytes.Length + " bytes to " + output);
            return 0;
        }
        catch (Exception ex) { Console.Error.WriteLine(ex); return 1; }
    }
}
