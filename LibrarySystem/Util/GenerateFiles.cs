using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Reflection;

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
                        table.AddCell(value != null ? value.ToString() : "");
                    }
                }
                document.Add(new Paragraph($"List of {typeof(T).Name}s"));
                document.Add(table);
                document.Close();
                return memoryStream.ToArray();
            }
        }
    }
}
