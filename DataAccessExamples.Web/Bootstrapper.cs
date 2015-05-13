using AutoMapper;
using DataAccessExamples.Core.MappingProfiles;
using DataAccessExamples.Core.Services.Department;
using DataAccessExamples.Core.Services.Employee;
using Nancy.Bootstrapper;
using Nancy.Responses;
using Nancy.TinyIoc;
using System;
using System.Linq;

namespace DataAccessExamples.Web
{
    using Nancy;

    /// <summary>
    ///   Custom Nancy bootstrapper supporting implementation-switching and wiring up AutoMapper config
    /// </summary>
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context)
        {
            base.ConfigureRequestContainer(container, context);
            container.Register(ResolveImplementation<IDepartmentService>(container, context));
            container.Register(ResolveImplementation<IEmployeeService>(container, context));
        }
        
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);
            pipelines.BeforeRequest += context =>
            {
                var implementationName = (string) context.Request.Query["Impl"];
                if (!String.IsNullOrWhiteSpace(implementationName))
                {
                    return new RedirectResponse(context.Request.Path).WithCookie("Impl", implementationName);
                }
                return null;
            };
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new EmployeesProfile());
            });
        }

        private T ResolveImplementation<T>(TinyIoCContainer container, NancyContext context) where T : class
        {
            var implementations = container.ResolveAll<T>();
            if (context.Request.Cookies.ContainsKey("Impl"))
            {
                return implementations.FirstOrDefault(i => i.GetType().Name.StartsWith(context.Request.Cookies["Impl"]));
            }
            return implementations.FirstOrDefault();
        }
    }
}