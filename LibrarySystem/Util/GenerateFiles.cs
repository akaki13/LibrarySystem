using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Reflection;
using System.Text;
using System.Globalization;

namespace LibrarySystem.Util
{
    public static class GenerateFiles
    {
        public static byte[] GeneratePdfBytes<T>(List<T> models)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                Document document = new Document();
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                document.Open();
                PdfPTable table = new PdfPTable(typeof(T).GetProperties().Length);
                table.WidthPercentage = 100; 
                foreach (var property in typeof(T).GetProperties())
                {
                    table.AddCell(property.Name);
                }
                foreach (var model in models)
                {
                    foreach (var property in typeof(T).GetProperties())
                    {
                        var value = property.GetValue(model);
                        if (value is DateTime dateTimeValue)
                        {
                            table.AddCell(dateTimeValue.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
                        }
                        else
                        {
                            table.AddCell(value != null ? value.ToString() : "");
                        }
                    }
                }
                document.Add(new Paragraph($"List of {typeof(T).Name}s"));
                document.Add(table);

                document.Close();
                return memoryStream.ToArray();
            }
        }

        public static byte[] GenerateCsvBytes<T>(List<T> models)
        {
            var csvBuilder = new StringBuilder();
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                csvBuilder.Append(property.Name);
                csvBuilder.Append(",");
            }
            csvBuilder.AppendLine();
            foreach (var model in models)
            {
                foreach (var property in properties)
                {
                    var value = property.GetValue(model);
                    if (value is DateTime dateTimeValue)
                    {
                        csvBuilder.Append(dateTimeValue.ToString("yyyy-MM-dd"));
                    }
                    else
                    {
                        csvBuilder.Append(value != null ? value.ToString() : "");
                    }
                    csvBuilder.Append(",");
                }
                csvBuilder.AppendLine();
            }
            return Encoding.UTF8.GetBytes(csvBuilder.ToString());
        }
    }
}
