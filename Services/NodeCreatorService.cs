using Blazor.Diagrams.Core.Models;
using Blazor.Diagrams.Core.Geometry;

public sealed class NodeCreatorService
{
    private readonly Workflow _workflow;
    private readonly Dictionary<int, NodeModel> _nodes = [];

    public IReadOnlyDictionary<int, NodeModel> CreatedNodes => _nodes;

    public NodeCreatorService(Workflow workflow)
    {
        _workflow = workflow;
    }

    public void CreateNodes()
    {
        int x = 100;
        int y = 100;
        int step = 0;

        foreach (var s in _workflow.Steps)
        {
            var point = new Point(x + (step * 250), y);
            var type = GetBlockType(s); // Determine node type
            var node = new NodeModel(point)
            {
                Data = new NodeInfo
                {
                    Title = s.ActivityName,
                    Type = type
                }
            };

            _nodes[s.StepNo] = node;
            step++;
        }
    }

    private string GetBlockType(Workflow.WorkflowStep step)
    {

        return step.ActivityName.Contains("For Each", StringComparison.OrdinalIgnoreCase)
            ? "Loop"
            : "Regular";
    }
}

public class NodeInfo
{
    public string Title { get; set; } = "";
    public string Type { get; set; } = "Regular"; // Regular, Loop, If, etc.
}