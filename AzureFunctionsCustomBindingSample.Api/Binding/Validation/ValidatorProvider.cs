// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Api.Binding.Validation
{
  using System;
  using System.Collections.Generic;

  using Microsoft.AspNetCore.Http;
  using Microsoft.Extensions.DependencyInjection;

  public sealed class ValidatorProvider : IValidatorProvider
  {
    private readonly Stack<Func<HttpRequest, IValidator>> _rules;

    public ValidatorProvider() => _rules = new Stack<Func<HttpRequest, IValidator>>();

    public IValidator GetValidator(HttpRequest httpRequest)
    {
      IValidator validator = null;

      foreach (var rule in _rules)
      {
        validator = rule(httpRequest);

        if (validator != null)
        {
          break;
        }
      }

      return validator;
    }

    public ValidatorProvider AddValidator(Func<HttpRequest, IValidator> rule)
    {
      _rules.Push(rule);

      return this;
    }

    public ValidatorProvider AddValidator<TValidator>(string uri, string method) where TValidator : IValidator
    {
      _rules.Push(httRequest =>
      {
        if (string.Equals(httRequest.Path.Value, uri) &&
            string.Equals(httRequest.Method, method))
        {
          return httRequest.HttpContext.RequestServices.GetRequiredService<TValidator>();
        }

        return null;
      });

      return this;
    }
  }
}
