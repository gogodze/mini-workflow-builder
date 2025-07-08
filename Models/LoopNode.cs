using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;

namespace MiniWorkflowBuilder.Models;

public class LoopNode : BaseNode
{
    public PortModel OutputPort { get; }
    public PortModel InputPort { get; }
    public LinkModel SelfLink { get; }
    public LoopNode(Point? position = null) : base(position)
    {
        OutputPort = AddPort(PortAlignment.Right);
        InputPort = AddPort(PortAlignment.Left);
        Title = "Loop Node";
        SelfLink = new LinkModel(OutputPort, InputPort);
    }

}