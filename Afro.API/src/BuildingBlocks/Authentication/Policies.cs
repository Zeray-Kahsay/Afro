using Afro.API.src.Modules.Identity.Constants;

namespace Afro.API.src.BuildingBlocks.Authentication;

public class Policies
{
    public const string AppPolicies = $"{RoleNames.Admin}, {RoleNames.Agent}";
}
