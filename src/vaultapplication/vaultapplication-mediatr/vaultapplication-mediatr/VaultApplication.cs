using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MediatR.Pipeline;
using MFiles.VAF;
using MFiles.VAF.Common;
using MFiles.VAF.Configuration;
using MFiles.VAF.Core;
using MFilesAPI;
using SimpleInjector;
using VaultApplicationMediatr.Application;
using VaultApplicationMediatr.Application.Interfaces;
using VaultApplicationMediatr.Infrastructure;

namespace VaultApplicationMediatr
{
    /// <summary>
    /// The entry point for this Vault Application Framework application.
    /// </summary>
    /// <remarks>Examples and further information available on the developer portal: http://developer.m-files.com/. </remarks>
    public class VaultApplication : ConfigurableVaultApplicationBase<Configuration>
    {
        private IMediator _mediator;
        private Container container;


        protected override void StartApplication()
        {
            base.StartApplication();

            _mediator = BuildMediator();
        }


        [EventHandler(MFEventHandlerType.MFEventHandlerBeforeCheckInChangesFinalize, ObjectType = (int)MFBuiltInObjectType.MFBuiltInObjectTypeDocument)]
        [EventHandler(MFEventHandlerType.MFEventHandlerAfterCreateNewObjectFinalize, ObjectType = (int)MFBuiltInObjectType.MFBuiltInObjectTypeDocument)]
        public void EventHandler_BeforeCheckInChangesFinalizeObject(EventHandlerEnvironment env)
        {
            var request = new AddModiticationDateToTitle
            {
                ObjVer = env.ObjVer,
                UserID = env.CurrentUserID
            };

            string updatedTitle = _mediator.Send(request).Result;
        }



        // https://github.com/jbogard/MediatR/blob/master/samples/MediatR.Examples.SimpleInjector/Program.cs
        private IMediator BuildMediator()
        {
            var container   = new Container();
            var assemblies  = GetAssemblies().ToArray();


            container.RegisterSingleton<IMediator, Mediator>();
            container.Register(typeof(IRequestHandler<,>), assemblies);
            RegisterHandlers(container, typeof(INotificationHandler<>), assemblies);
            RegisterHandlers(container, typeof(IRequestExceptionAction<,>), assemblies);
            RegisterHandlers(container, typeof(IRequestExceptionHandler<,,>), assemblies);


            // Register our injectable infrastructure
            container.Register<IVault>(() => this.PermanentVault );  // TODO: figure something out to get the Scoped Transactional Vault from the event.
            container.Register<IObjectRepository, MFObjectRepository>();

            container.Collection.Register(typeof(IPipelineBehavior<,>), new []
            {
                typeof(RequestExceptionProcessorBehavior<,>),
                typeof(RequestExceptionActionProcessorBehavior<,>),
                typeof(RequestPreProcessorBehavior<,>),
                typeof(RequestPostProcessorBehavior<,>)
            });
            //container.Collection.Register(typeof(IRequestPreProcessor<>), new [] { typeof(GenericRequestPreProcessor<>) });
            //container.Collection.Register(typeof(IRequestPostProcessor<,>), new[] { typeof(GenericRequestPostProcessor<,>), typeof(ConstrainedRequestPostProcessor<,>) });

            //RegisterHandlers(container, typeof(ValidationBehavior<,>), assemblies);

            //container.Register(typeof(ValidationBehavior<,>), assemblies);
            //container.Collection.Register(typeof(IValidator<>), assemblies, Lifestyle.Singleton);

            //container.AddFluentValidation(typeof(UpdateMultiSelectFromString));


            // Pipeline
            container.Collection.Register(typeof(IPipelineBehavior<,>), new[]
            {
                typeof(RequestPreProcessorBehavior<,>),
                typeof(RequestPostProcessorBehavior<,>)
                //typeof(ValidationBehavior<,>)
            });

            //container.Collection.Register(typeof(IRequestPreProcessor<>), new [] { typeof(GenericRequestPreProcessor<>) });
            //container.Collection.Register(typeof(IRequestPostProcessor<,>), new[] { typeof(GenericRequestPostProcessor<,>) });

            container.Register(() => new ServiceFactory(container.GetInstance), Lifestyle.Singleton);

            container.Verify();

            var mediator = container.GetInstance<IMediator>();

            return mediator;
        }

        private static void RegisterHandlers(Container container, Type collectionType, Assembly[] assemblies)
        {
            // we have to do this because by default, generic type definitions (such as the Constrained Notification Handler) won't be registered
            var handlerTypes = container.GetTypesToRegister(collectionType, assemblies, new TypesToRegisterOptions
            {
                IncludeGenericTypeDefinitions = true,
                IncludeComposites = false,
            });

            container.Collection.Register(collectionType, handlerTypes);
        }

        private static IEnumerable<Assembly> GetAssemblies()
        {
            yield return typeof(IMediator).GetTypeInfo().Assembly;
            yield return typeof(VaultApplication).GetTypeInfo().Assembly;
            //yield return typeof(UpdateMultiSelectFromString).GetTypeInfo().Assembly;
        }
    }


    //public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    //{
    //    private readonly IEnumerable<IValidator<TRequest>> _validators;

    //    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    //    {
    //        _validators = validators;
    //    }

    //    public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    //    {
    //        if (_validators.Any())
    //        {
    //            var context = new ValidationContext(request);

    //            var failures = _validators
    //                .Select(v => v.Validate(context))
    //                .SelectMany(result => result.Errors)
    //                .Where(f => f != null)
    //                .ToList();

    //            if (failures.Any())
    //            {
    //                throw new ValidationException(failures);
    //            }
    //        }

    //        return next();
    //    }
    //}


    //public class GenericRequestPreProcessor<TRequest> : IRequestPreProcessor<TRequest>
    //{
    //    public GenericRequestPreProcessor()
    //    {
    //    }

    //    public Task Process(TRequest request, CancellationToken cancellationToken)
    //    {
    //        return Unit.Task;
    //    }
    //}

    //public class GenericRequestPostProcessor<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse>
    //{
    //    public GenericRequestPostProcessor()
    //    {
    //    }

    //    public Task Process(TRequest request, TResponse response)
    //    {
    //        return Unit.Task;
    //    }

    //    public Task Process(TRequest request, TResponse response, CancellationToken cancellationToken)
    //    {
    //        return Unit.Task;
    //    }
    //}
}