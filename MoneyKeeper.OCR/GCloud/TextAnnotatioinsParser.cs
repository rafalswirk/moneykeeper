using System.Drawing;
using System.Text.Json;

namespace MoneyKeeper.OCR.GCloud
{
    public record TextAnnotation(string Description, Rectangle BoundingPoly);

    public class TextAnnotationParser
    {
        public IReadOnlyCollection<TextAnnotation> Parse(JsonDocument document)
        {
            throw new NotImplementedException();
        }
    }
}