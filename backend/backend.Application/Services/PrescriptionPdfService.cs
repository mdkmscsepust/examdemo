using backend.Application.Models;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace backend.Application.Services
{
    public class PrescriptionPdfService
    {
        public byte[] GeneratePrescriptionPdf(AppointmentOutDTO appointmentOutDTO)
        {
            using var stream = new MemoryStream();
            var writer = new PdfWriter(stream);
            var pdf = new PdfDocument(writer);
            var document = new Document(pdf);
            document.Add(new Paragraph("Prescription report")
            .SetTextAlignment(TextAlignment.CENTER)
            .SetFontSize(20)
            .SetBold());

            document.Add(new Paragraph($"Patient Name: {appointmentOutDTO.PatientName}").SetFontSize(12));
            document.Add(new Paragraph($"Doctor Name: {appointmentOutDTO.DoctorName}").SetFontSize(12));
            document.Add(new Paragraph($"Date: {appointmentOutDTO.AppointmentDate:dd-MMM-yyyy}").SetFontSize(12));
            document.Add(new Paragraph($"Visit Type: {appointmentOutDTO.VisitType}").SetFontSize(12));
            document.Add(new Paragraph($"Diagnosis: {appointmentOutDTO.Diagnosis}").SetFontSize(12));

            document.Add(new Paragraph(".").SetMarginBottom(10));


            var table = new Table(5);
            table.AddHeaderCell("Medicine");
            table.AddHeaderCell("Dosage");
            table.AddHeaderCell("Start Date");
            table.AddHeaderCell("End Date");
            table.AddHeaderCell("Notes");

            foreach (var item in appointmentOutDTO.PrescriptionDetails)
            {
                table.AddCell(item.MedicineName);
                table.AddCell(item.Dosage);
                table.AddCell(item.StartDate.ToString("dd-MMM-yyyy"));
                table.AddCell(item.EndDate.ToString("dd-MMM-yyyy"));
                table.AddCell(item.Notes);
            }

            document.Add(table);
            document.Close();
            return stream.ToArray();
        }
    }
}