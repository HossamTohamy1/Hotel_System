using Hotel.Core.Entities;
using Hotel.Services;
using Hotel.Services.Services;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

public class CustomAuthorizeFilter : ActionFilterAttribute
{
    private readonly RoleFeatureService _roleFeatureService;
    private readonly string _featureName;

    public CustomAuthorizeFilter(RoleFeatureService roleFeatureService, string featureName)
    {
        _roleFeatureService = roleFeatureService;
        _featureName = featureName;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var roleIdClaim = context.HttpContext.User.FindFirst(ClaimTypes.Role);
        if (roleIdClaim == null || string.IsNullOrWhiteSpace(roleIdClaim.Value))
            throw new UnauthorizedAccessException("No role assigned.");

        if (!int.TryParse(roleIdClaim.Value, out int roleId))
            throw new UnauthorizedAccessException("Invalid role ID in token.");

        var featureServices = context.HttpContext.RequestServices.GetRequiredService<FeatureServices>();
        var feature = featureServices.GetFeatureByName(_featureName);

        if (!_roleFeatureService.CheckFeatureAccess(roleId, feature.Id))
            throw new UnauthorizedAccessException("Access denied to this feature.");
    }
}
