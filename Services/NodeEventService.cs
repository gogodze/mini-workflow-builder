using MiniWorkflowBuilder.Models;

namespace MiniWorkflowBuilder.Services;

public class NodeEventService
{
    public event Action<BaseNode>? NodeClicked;

    public void RaiseNodeClicked(BaseNode node)
    {
        NodeClicked?.Invoke(node);
    }
}