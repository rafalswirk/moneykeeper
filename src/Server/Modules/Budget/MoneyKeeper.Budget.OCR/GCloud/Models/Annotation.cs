using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper.OCR.GCloud.Models
{
    public record Vertices(int X, int Y);
    public record BoundingPoly(Vertices[] Vertices);
    public record Annotation(string Description, BoundingPoly BoundingPoly);
}
