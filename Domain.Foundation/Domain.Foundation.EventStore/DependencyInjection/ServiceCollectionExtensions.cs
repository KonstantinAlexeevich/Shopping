using System;
using Domain.Foundation.Events;
using Domain.Foundation.EventSourcing;
using Domain.Foundation.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.Foundation.EventStore.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEventNames(this IServiceCollection serviceCollection, Action<IEventMetadataMapper> configurationAction)
        {
            var metadataProvider = new EventMetadataProvider();
            configurationAction?.Invoke(metadataProvider);
            serviceCollection.AddSingleton<IEventMetadataProvider>(metadataProvider);
            return serviceCollection;
        }

        public static IServiceCollection StoreAggregate<TAggregate, TIdentity, TEvent>(this IServiceCollection serviceCollection) 
            where TAggregate : IEventsAggregate<TIdentity, TEvent> where TEvent : IEvent
        {
            serviceCollection.AddScoped<IAggregateStore<TAggregate, TIdentity>, EventsAggregateStore<TAggregate, TIdentity, TEvent>>();
            return serviceCollection;
        }
    }
}