using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;
using System.Text.Json.Serialization;

namespace MiniWorkflowBuilder.Models;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$nodeType")]
[JsonDerivedType(typeof(RegularNode), "regular")]
[JsonDerivedType(typeof(IfNode), "if")]
[JsonDerivedType(typeof(LoopNode), "loop")]
public abstract class BaseNode : NodeModel
{
    public List<ActivityCondition>? Filters { get; set; }

    public BaseNode(Point? position = null) : base(position)
    {

    }

}