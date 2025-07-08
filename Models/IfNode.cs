using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;

namespace MiniWorkflowBuilder.Models;

public class IfNode : BaseNode
{
    public IfNode(Point? position = null) : base(position)
    {
        AddPort(PortAlignment.Right);
        AddPort(PortAlignment.Right);
        Title = "If Node";
    }
}