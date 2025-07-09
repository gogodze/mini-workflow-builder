using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;

namespace MiniWorkflowBuilder.Models;

public class IfNode : BaseNode
{
    public IfNode(Point? position = null) : base(position)
    {
        AddPort(new PortModel(this, true, PortAlignment.Left));
        AddPort(new PortModel(this, false, PortAlignment.Right));
        AddPort(new PortModel(this, false, PortAlignment.Right));
        Title = "If Node";
    }
}