using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;

namespace MiniWorkflowBuilder.Models;

public class RegularNode : BaseNode
{
    public RegularNode(Point? position = null) : base(position)
    {
        AddPort(PortAlignment.Right);
        AddPort(PortAlignment.Left);
        Title = "Regular Node";
    }
}