// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the Apache License, Version 2.0.
// See License.txt in the project root for license information.

namespace AzureFunctionsCustomBindingSample.Binding.Validation
{
  using System;
  using System.Collections.Generic;
  using System.Text;

  using Microsoft.AspNetCore.Http;
  using Microsoft.AspNetCore.Routing;
  using Microsoft.Extensions.DependencyInjection;

  /// <summary>Provides a simple API to get an instance of the <see cref="AzureFunctionsCustomBindingSample.Binding.Validation.IValidator"/> type that associated with a request.</summary>
  public sealed class ValidatorProvider : IValidatorProvider, IValidationConfig
  {
    private readonly IDictionary<string, Type> _rules
      ;

    /// <summary>Initializes a new instance of the <see cref="AzureFunctionsCustomBindingSample.Binding.Validation.ValidatorProvider"/> class.</summary>
    public ValidatorProvider() => _rules = new Dictionary<string, Type>();

    /// <summary>Gets an instance of the <see cref="AzureFunctionsCustomBindingSample.Binding.Validation.IValidator"/> type that associated with a request.</summary>
    /// <param name="httpRequest">An object that represents the incoming side of an individual HTTP request.</param>
    /// <returns>An instance of the <see cref="AzureFunctionsCustomBindingSample.Binding.Validation.IValidator"/> type that associated with a request.</returns>
    public IValidator GetValidator(HttpRequest httpRequest)
    {
      IValidator validator = null;

      var builder = new StringBuilder();
      var routes = httpRequest.HttpContext.GetRouteData().Values;

      foreach (var route in routes)
      {
        builder.Replace(route.Value.ToString(), route.Key);
      }

      var key = ValidatorProvider.GetKey(httpRequest.Method, builder.ToString());

      if (_rules.TryGetValue(key, out var type))
      {
        validator = httpRequest.HttpContext.RequestServices.GetRequiredService(type) as IValidator;
      }

      return validator;
    }

    /// <summary>Registers a validator.</summary>
    /// <typeparam name="TValidator">A type of a validator.</typeparam>
    /// <param name="uri">A value that represents a URI of an HTTP request.</param>
    /// <param name="method">A value that represents a type of an HTTP request.</param>
    /// <returns>An instance of the <see cref="AzureFunctionsCustomBindingSample.Binding.Validation.ValidatorProvider"/>.</returns>
    public IValidationConfig AddValidator<TValidator>(string uri, string method) where TValidator : IValidator
    {
      var key = ValidatorProvider.GetKey(uri, method);

      _rules.Add(key, typeof(TValidator));

      return this;
    }

    private static string GetKey(string uri, string method) => $"{method}_{uri}".ToLower();
  }
}
