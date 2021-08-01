using Microsoft.FeatureManagement;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleApp.FeatureFilters
{
    /// <summary>
    /// A filter that uses the feature management context to ensure that the current task has the notion of an account id, and that the account id is allowed.
    /// This filter will only be executed if an object implementing <see cref="IAccountContext"/> is passed in during feature evaluation.
    /// </summary>
    [FilterAlias("AccountId")]
    class AccountIdFilter : IContextualFeatureFilter<IAccountContext>
    {
        public Task<bool> EvaluateAsync(FeatureFilterEvaluationContext featureEvaluationContext, IAccountContext accountContext)
        {
            if (string.IsNullOrEmpty(accountContext?.AccountId))
            {
                throw new ArgumentNullException(nameof(accountContext));
            }

            var allowedAccounts = new List<string>();

            featureEvaluationContext.Parameters.Bind("AllowedAccounts", allowedAccounts);

            return Task.FromResult(allowedAccounts.Contains(accountContext.AccountId));
        }
    }
}
