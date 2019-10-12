﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AutoWrapper
{
    internal class AutoWrapperMiddleware : WrapperBase
    {
        private AutoWrapperMembers _awm;
        public AutoWrapperMiddleware(RequestDelegate next, AutoWrapperOptions options, ILogger<AutoWrapperMiddleware> logger) : base(next, options, logger)
        {
            var jsonSettings = Helpers.JSONHelper.GetJSONSettings(options.IgnoreNullValue, options.UseCamelCaseNamingStrategy);
            _awm = new AutoWrapperMembers(options, logger, jsonSettings);
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await InvokeAsyncBase(context, _awm);
        }
    }

    internal class AutoWrapperMiddleware<T> : WrapperBase
    {
        private AutoWrapperMembers _awm;
        public AutoWrapperMiddleware(RequestDelegate next, AutoWrapperOptions options, ILogger<AutoWrapperMiddleware> logger) : base(next, options, logger)
        {
            var jsonSettings = Helpers.JSONHelper.GetJSONSettings<T>(options.IgnoreNullValue, options.UseCamelCaseNamingStrategy);
            _awm = new AutoWrapperMembers(options, logger, jsonSettings, true);
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await InvokeAsyncBase(context, _awm);
        }

    }
}
