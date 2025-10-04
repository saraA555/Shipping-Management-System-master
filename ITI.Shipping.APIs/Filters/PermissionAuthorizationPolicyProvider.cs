﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace ITI.Shipping.APIs.Filters;
public class PermissionAuthorizationPolicyProvider:DefaultAuthorizationPolicyProvider
{
    private readonly AuthorizationOptions _options;

    public PermissionAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options)
        : base(options)
    {
        _options = options.Value;
    }

    public override async Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
    {
        var policy = await base.GetPolicyAsync(policyName);
        if(policy is not null)
            return policy;

        var permissions = policyName.Split(",",StringSplitOptions.RemoveEmptyEntries);

        var permissionPolicy = new AuthorizationPolicyBuilder()
            .AddRequirements(new PermissionRequirements(permissions))
            .Build();

        _options.AddPolicy(policyName,permissionPolicy);

        return permissionPolicy;
    }
}