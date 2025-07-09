using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;
using Blazor.Diagrams.Core.Models.Base;

namespace MiniWorkflowBuilder.Models;

public class PortModel : Blazor.Diagrams.Core.Models.PortModel
{
    public bool In { get; set; }

    public PortModel(BaseNode parent, bool @in, PortAlignment alignment = PortAlignment.Bottom, Point? position = null, Size? size = null) : base(parent, alignment, position, size)
    {
        In = @in;
    }

    public PortModel(string id, BaseNode parent, bool @in, PortAlignment alignment = PortAlignment.Bottom, Point? position = null, Size? size = null) : base(id, parent, alignment, position, size)
    {
        In = @in;
    }

    public override bool CanAttachTo(ILinkable other)
    {
        if (!base.CanAttachTo(other))
            return false;

        if (other is not PortModel otherPort)
            return false;

        return In != otherPort.In;
    }
}