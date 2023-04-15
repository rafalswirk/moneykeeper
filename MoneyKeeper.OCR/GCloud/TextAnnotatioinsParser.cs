using MoneyKeeper.OCR.GCloud.Models;
using System.Drawing;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MoneyKeeper.OCR.GCloud
{
    public record TextAnnotation(string Description);

    public class TextAnnotationParser
    {
        public IReadOnlyCollection<TextAnnotation> Parse(JsonDocument document, string textPattern)
        {
            var textAnnotations = document
                .RootElement
                .GetProperty("responses")
                .EnumerateArray()
                .First()
                .GetProperty("textAnnotations");
                //.EnumerateArray();

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var annotations = JsonSerializer.Deserialize<IReadOnlyCollection<Annotation>>(textAnnotations, options);
            if (annotations == null)
            {
                return new List<TextAnnotation>(0);
            }
            var searchResults = annotations.Skip(1)?.Where(a => a.Description.ToLower().Contains(textPattern)) ?? new List<Annotation>(0);
            
            var results = new List<TextAnnotation>();
            foreach (var tex in searchResults)
            {
                var lineText = GetLineText(annotations,
                    tex.BoundingPoly.Vertices[0].Y,
                    tex.BoundingPoly.Vertices[3].Y, 10);
                results.Add(new TextAnnotation(lineText));
            }

            return results;
        }

        private string GetLineText(IReadOnlyCollection<Annotation> annotations, int y1, int y2, int margin)
        {
            var resultText = new StringBuilder();
            foreach (var annotation in annotations)
            {
                if (annotation.BoundingPoly.Vertices[0].Y + margin >= y1
                    && annotation.BoundingPoly.Vertices[1].Y + margin >= y1
                    && annotation.BoundingPoly.Vertices[2].Y - margin <= y2
                    && annotation.BoundingPoly.Vertices[3].Y - margin <= y2)
                {
                    resultText.Append(annotation.Description + " ");
                }
            }

            return resultText.ToString().Trim();
        }
    }
}