using Microsoft.Extensions.DependencyInjection;
using Rucula.DataAccess.Deserializers;
using Rucula.DataAccess.Dtos;
using Rucula.DataAccess.Fetching;
using Rucula.DataAccess.Fetching.Byma;
using Rucula.DataAccess.Fetching.Byma.Config;
using Rucula.DataAccess.Mappers;
using Rucula.Domain.Abstractions;
using Rucula.Domain.Entities;

namespace Rucula.DataAccess.IoC
{
    public static class Registrar
    {
        public static void Register(IServiceCollection serviceCollection)
        {
            RegisterDeserializers(serviceCollection);
            RegisterProviders(serviceCollection);
            RegisterFetchers(serviceCollection);
            RegisterConfigs(serviceCollection);
            RegisterMappers(serviceCollection);
        }

        private static void RegisterConfigs(IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddSingleton<ITituloDetailsBymaHttpConfig, TituloDetailsBymaHttpConfig>()
                .AddSingleton<ILetrasBymaHttpConfig, LetrasBymaHttpConfig>()
                .AddSingleton<IBonosBymaHttpConfig, BonosBymaHttpConfig>()
                .AddSingleton<IBymaHttpReader, BymaHttpReader>()
                .AddSingleton<IRequestFactory, RequestFactory>()
                .AddSingleton<IHandlerFactory, HandlerFactory>();
        }

        private static void RegisterProviders(IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddSingleton<IProvider<TituloIsin>, TituloIsinProvider>();
        }

        private static void RegisterFetchers(IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddSingleton<IBymaLetrasFetcher, BymaLetrasFetcher>()
                .AddSingleton<IBymaBonosFetcher, BymaBonosFetcher>()
                .AddSingleton<IBymaTituloDetailsFetcher, BymaTituloDetailsFetcher>()
                .AddSingleton<IJsonDeserializer<TitulosContentDto>, JsonToTitulosContentDtoDeserializer>()
                .AddSingleton<IJsonDeserializer<TituloDetailsContentDto>, JsonToTituloDetailsContentDtoDeserializer>();
        }

        private static void RegisterMappers(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IMapper<TituloDto, Titulo>, TituloMapper>();
        }

        private static void RegisterDeserializers(IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddSingleton<IJsonDeserializer<PaginationDto>, JsonToPaginationDtoDeserializer>()
                .AddSingleton<IJsonDeserializer<TituloDetailsContentDto>, JsonToTituloDetailsContentDtoDeserializer>()
                .AddSingleton<IJsonDeserializer<TituloDto>, JsonToTituloDtoDeserializer>()
                .AddSingleton<IJsonDeserializer<TitulosContentDto>, JsonToTitulosContentDtoDeserializer>()
                .AddSingleton<IJsonDeserializer<TituloDetailsDto>, JsonToTituloDetailsDtoDeserializer>()
                .AddSingleton<IJsonValueReader, JsonValueReader>();
        }
    }
}
