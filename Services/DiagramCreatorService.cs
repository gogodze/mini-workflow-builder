using Blazor.Diagrams;
using Blazor.Diagrams.Core.Geometry;
using Blazor.Diagrams.Core.Models;
using Blazor.Diagrams.Core.Models.Base;
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
            AllowMultiSelection = true,
        });

    public DiagramCreatorService()
    {
        Diagram.Options.AllowPanning = true;
        Diagram.Options.Zoom.Enabled = true;
    }


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

                var fromPort = fromNode.Ports.FirstOrDefault(p => p.Alignment == PortAlignment.Right)
                               ?? fromNode.AddPort(PortAlignment.Right);
                var toPort = toNode.Ports.FirstOrDefault(p => p.Alignment == PortAlignment.Left)
                             ?? toNode.AddPort(PortAlignment.Left);

                var jumpLink = new LinkModel(fromPort, toPort)
                {
                    Router = new NormalRouter(),
                    PathGenerator = new SmoothPathGenerator(),
                    Color = "gray",
                    TargetMarker = LinkMarker.Arrow,
                };

                Diagram.Links.Add(jumpLink);
            }
        }
    }

    public BaseNode DuplicateNode(BaseNode originalNode)
    {
        var newPosition = new Point(originalNode.Position.X + 100, originalNode.Position.Y + 50);

        BaseNode duplicatedNode = originalNode switch
        {
            RegularNode regularNode => new RegularNode(newPosition)
            {
                Title = $"{regularNode.Title} (Copy)",
                Filters = regularNode.Filters?.Select(f => new ActivityCondition
                {
                    FieldName = f.FieldName,
                    ConditionId = f.ConditionId,
                    FieldValue = f.FieldValue
                }).ToList()
            },
            LoopNode loopNode => new LoopNode(newPosition)
            {
                Title = $"{loopNode.Title} (Copy)",
                Filters = loopNode.Filters?.Select(f => new ActivityCondition
                {
                    FieldName = f.FieldName,
                    ConditionId = f.ConditionId,
                    FieldValue = f.FieldValue
                }).ToList()
            },
            IfNode ifNode => new IfNode(newPosition)
            {
                Title = $"{ifNode.Title} (Copy)",
                Filters = ifNode.Filters?.Select(f => new ActivityCondition
                {
                    FieldName = f.FieldName,
                    ConditionId = f.ConditionId,
                    FieldValue = f.FieldValue
                }).ToList()
            },
            _ => throw new ArgumentException($"Unknown node type: {originalNode.GetType()}")
        };

        Diagram.Nodes.Add(duplicatedNode);
        return duplicatedNode;
    }

    public void DeleteNode(BaseNode node)
    {
        var connectedLinks = new List<BaseLinkModel>();

        foreach (var port in node.Ports)
        {
            connectedLinks.AddRange(Diagram.Links.Where(l =>
                ReferenceEquals(l.Source, port) || ReferenceEquals(l.Target, port)));
        }

        foreach (var link in connectedLinks.Distinct())
        {
            Diagram.Links.Remove(link);
        }

        Diagram.Nodes.Remove(node);
    }

    public void CreateNodeFromType(string nodeType, Point position)
    {
        BaseNode newNode = nodeType.ToLower() switch
        {
            "regular" => new RegularNode(position) { Title = "New Node" },
            "loop" => new LoopNode(position) { Title = "For Each" },
            "if" => new IfNode(position) { Title = "If Condition" },
            _ => new RegularNode(position) { Title = "Unknown" }
        };

        Diagram.Nodes.Add(newNode);
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