using RulesEngine.Models;
using VisionaryCoder.ifx;

namespace VisionaryCoder.Access.Rules.Contract;

public interface IRulesAccess
{

    Task<RuleDefinition> LoadRule(Identifier rulesId);
    Task<RuleDefinition> Store(RuleDefinition rule);

    Task<WorkspaceDefinition> LoadWorkflow(Identifier workflowId);
    Task<ICollection<WorkspaceDefinition>> LoadWorkflows(ICollection<Identifier> workflowIds);
    Task<WorkspaceDefinition> Store(WorkspaceDefinition workflow);

}

public class RuleDefinition
{

    public Identifier Id { get; set; }
    public Rule Rule { get; set; }

}

public class WorkspaceDefinition
{
    
    public Identifier Id { get; set; }
    public Workflow Workflow { get; set; }

}