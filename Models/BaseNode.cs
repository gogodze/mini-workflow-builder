using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;

namespace MiniWorkflowBuilder.Models;

public abstract class BaseNode : NodeModel
{
    public List<ActivityCondition>? Filters { get; set; }

    public BaseNode(Point? position = null) : base(position)
    {

    }

}