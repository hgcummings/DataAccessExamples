using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccessExamples.Core.Services;
using DataAccessExamples.Core.ViewModels;
using Nancy.TinyIoc;

namespace DataAccessExamples.Web
{
    using Nancy;

    public class Bootstrapper : DefaultNancyBootstrapper
    {
        // The bootstrapper enables you to reconfigure the composition of the framework,
        // by overriding the various methods and properties.
        // For more information https://github.com/NancyFx/Nancy/wiki/Bootstrapper
        protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context)
        {
            base.ConfigureRequestContainer(container, context);
            
            container.Register(ResolveImplementation<IDepartmentService>(container, context));
        }

        private T ResolveImplementation<T>(TinyIoCContainer container, NancyContext context) where T : class
        {
            var implementations = container.ResolveAll<T>();
            var implementationName = (string) context.Request.Query["Impl"];
            if (!String.IsNullOrWhiteSpace(implementationName))
            {
                return implementations.Distinct().FirstOrDefault(i => i.GetType().Name.StartsWith(implementationName));
            }
            return implementations.FirstOrDefault();
        }
    }
}