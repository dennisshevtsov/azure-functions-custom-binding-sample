// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Validation
{
  using System;
  using System.Collections.Generic;

  using Microsoft.AspNetCore.Http;
  using Microsoft.Extensions.DependencyInjection;

  /// <summary>Provides a simple API to get an instance of the <see cref="AzureFunctionsCustomBindingSample.Validation.IValidator"/> type that associated with a request.</summary>
  public sealed class ValidatorProvider : IValidatorProvider, IValidationConfig
  {
    private readonly Stack<Func<HttpRequest, IValidator>> _rules;

    /// <summary>Initializes a new instance of the <see cref="AzureFunctionsCustomBindingSample.Validation.ValidatorProvider"/> class.</summary>
    public ValidatorProvider() => _rules = new Stack<Func<HttpRequest, IValidator>>();

    /// <summary>Gets an instance of the <see cref="AzureFunctionsCustomBindingSample.Validation.IValidator"/> type that associated with a request.</summary>
    /// <param name="httpRequest">An object that represents the incoming side of an individual HTTP request.</param>
    /// <returns>An instance of the <see cref="AzureFunctionsCustomBindingSample.Validation.IValidator"/> type that associated with a request.</returns>
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

    /// <summary>Registers a validator.</summary>
    /// <typeparam name="TValidator">A type of a validator.</typeparam>
    /// <param name="uri">A value that represents a URI of an HTTP request.</param>
    /// <param name="method">A value that represents a type of an HTTP request.</param>
    /// <returns>An instance of the <see cref="AzureFunctionsCustomBindingSample.Validation.ValidatorProvider"/>.</returns>
    public IValidationConfig AddValidator<TValidator>(string uri, string method) where TValidator : IValidator
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
