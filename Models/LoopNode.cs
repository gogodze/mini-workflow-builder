using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;

namespace MiniWorkflowBuilder.Models;

public class LoopNode : BaseNode
{
    public PortModel? LoopPort { get; private set; }
    public PortModel? DonePort { get; private set; }

    public LoopNode(Point position) : base(position)
    {
        CreatePorts();
    }

    private void CreatePorts()
    {
        LoopPort = AddPort(PortAlignment.Right);
        DonePort = AddPort(PortAlignment.Right);
    }
}