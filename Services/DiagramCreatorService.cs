using Blazor.Diagrams;
using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;
using Blazor.Diagrams.Core.PathGenerators;
using Blazor.Diagrams.Core.Routers;
using Blazor.Diagrams.Options;
using MiniWorkflowBuilder.Components;
using MiniWorkflowBuilder.Models;

namespace MiniWorkflowBuilder.Services;

public sealed class DiagramCreatorService
{
    public BlazorDiagram Diagram { get; } = new(
        new BlazorDiagramOptions
        {
            GridSnapToCenter = true,
            AllowMultiSelection = false,
        });

    public void CreateNodes(Workflow workflow, Application application)
    {
        Diagram.RegisterComponent<LoopNode, LoopNodeComponent>();
        Diagram.RegisterComponent<RegularNode, RegularNodeComponent>();
        Diagram.RegisterComponent<IfNode, IfNodeComponent>();

        int baseX = 100;
        int baseY = 100;
        int verticalGap = 150;
        int horizontalGap = 250;

        var stepNodeMap = new Dictionary<int, NodeModel>();

        foreach (var step in workflow.Steps)
        {
            var activity = application.Activities.FirstOrDefault(a => a.ActivityId == step.ActivityId);
            var featureType = activity?.ActivityFeatureType ?? ActivityFeatureType.RegularBlock;

            int x = baseX + (step.StepNo * horizontalGap);
            int y = baseY + (step.StepNo * verticalGap);

            NodeModel node;

            if (featureType == ActivityFeatureType.Foreach)
            {
                var loop = new LoopNode(new Point(x, y)) { Title = step.ActivityName };
                Diagram.Nodes.Add(loop);
                node = loop;
            }
            else if (featureType == ActivityFeatureType.ifblock)
            {
                node = new IfNode(new Point(x, y)) { Title = step.ActivityName };
                Diagram.Nodes.Add(node);
            }
            else
            {
                node = new RegularNode(new Point(x, y)) { Title = step.ActivityName };
                Diagram.Nodes.Add(node);
            }

            stepNodeMap[step.StepNo] = node;
        }

        foreach (var step in workflow.Steps)
        {
            if (!stepNodeMap.TryGetValue(step.StepNo, out var fromNode))
                continue;

            foreach (var jumpStepNo in step.Jumps ?? Enumerable.Empty<int>())
            {
                if (!stepNodeMap.TryGetValue(jumpStepNo, out var toNode))
                    continue;
                var fromPort = fromNode.AddPort(PortAlignment.Right);
                var toPort = toNode.Ports.FirstOrDefault(p => p.Alignment == PortAlignment.Left)
                             ?? toNode.AddPort(PortAlignment.Left);

                var jumpLink = new LinkModel(fromPort, toPort)
                {
                    Router = new OrthogonalRouter(),
                    PathGenerator = new SmoothPathGenerator()
                };

                Diagram.Links.Add(jumpLink);
            }
        }
    }
}

public class BoundingClientRect
{
    public double Left { get; set; }
    public double Top { get; set; }
    public double Right { get; set; }
    public double Bottom { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
}