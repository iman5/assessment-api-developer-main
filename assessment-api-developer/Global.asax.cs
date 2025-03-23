using assessment_platform_developer.Models;
using assessment_platform_developer.Repositories;
using assessment_platform_developer.Services;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using SimpleInjector;
using SimpleInjector.Diagnostics;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.WebApi;
using System;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Compilation;
using System.Web.Http;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.UI;

namespace assessment_platform_developer
{
	public sealed class PageInitializerModule : IHttpModule
	{
		public static void Initialize()
		{
			DynamicModuleUtility.RegisterModule(typeof(PageInitializerModule));
		}

		void IHttpModule.Init(HttpApplication app)
		{
			app.PreRequestHandlerExecute += (sender, e) =>
			{
				var handler = app.Context.CurrentHandler;
				if (handler != null)
				{
					string name = handler.GetType().Assembly.FullName;
					if (!name.StartsWith("System.Web") &&
						!name.StartsWith("Microsoft"))
					{
						Global.InitializeHandler(handler);
					}
				}
			};
		}

		void IHttpModule.Dispose() { }
	}

	public class Global : HttpApplication
	{
		private static Container container;

		public static void InitializeHandler(IHttpHandler handler)
		{
			var handlerType = handler is Page
				? handler.GetType().BaseType
				: handler.GetType();
			container.GetRegistration(handlerType, true).Registration
				.InitializeInstance(handler);
		}

        void Application_Start(object sender, EventArgs e)
        {
            // Register Web API routes.
            GlobalConfiguration.Configure(WebApiConfig.Register);

            // Register Web Forms routes and bundles.
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Set the database initializer.
            Database.SetInitializer(new DropCreateDatabaseAlways<CustomerDBContext>());

            // Force immediate initialization and seed the database.
            using (var context = new CustomerDBContext())
            {
                context.Database.Initialize(force: true);
                CustomerSeeder.Seed(context);
            }

            Bootstrap();
        }

        private static void Bootstrap()
        {
            // 1. Create a new Simple Injector container.
            var container = new Container();

            // Use WebRequestLifestyle as the default scoped lifestyle.
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            // 2. Register Web API controllers.
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            // Register the DbContext as scoped.
            container.Register<CustomerDBContext>(Lifestyle.Scoped);

            // 3. Register application components.
            container.Register<ICustomerRepository, CustomerRepository>(Lifestyle.Scoped);
            container.Register<ICustomerService, CustomerService>(Lifestyle.Scoped);

            // Register your Web Forms pages.
            RegisterWebPages(container);

            // Resolve unregistered concrete types.
            container.Options.ResolveUnregisteredConcreteTypes = true;

            // Set the dependency resolver for Web API.
            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);

            Global.container = container;

            // 4. Verify the container's configuration.
            container.Verify();

            HttpContext.Current.Application["DIContainer"] = container;
        }

        private static void RegisterWebPages(Container container)
		{
			var pageTypes =
				from assembly in BuildManager.GetReferencedAssemblies().Cast<Assembly>()
				where !assembly.IsDynamic
				where !assembly.GlobalAssemblyCache
				from type in assembly.GetExportedTypes()
				where type.IsSubclassOf(typeof(Page))
				where !type.IsAbstract && !type.IsGenericType
				select type;

			foreach (var type in pageTypes)
			{
				var reg = Lifestyle.Transient.CreateRegistration(type, container);
				reg.SuppressDiagnosticWarning(
					DiagnosticType.DisposableTransientComponent,
					"ASP.NET creates and disposes page classes for us.");
				container.AddRegistration(type, reg);
			}
		}

	}
}