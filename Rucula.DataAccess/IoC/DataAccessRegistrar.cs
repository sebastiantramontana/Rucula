using Microsoft.Extensions.DependencyInjection;
using Rucula.DataAccess.Deserializers;
using Rucula.DataAccess.Dtos;
using Rucula.DataAccess.Mappers;
using Rucula.DataAccess.Providers;
using Rucula.DataAccess.Providers.Ambito;
using Rucula.DataAccess.Providers.Byma;
using Rucula.DataAccess.Providers.Byma.Config;
using Rucula.Domain.Abstractions;
using Rucula.Domain.Entities;

namespace Rucula.DataAccess.IoC
{
    public static class DataAccessRegistrar
    {
        public static void Register(IServiceCollection serviceCollection)
        {
            RegisterDeserializers(serviceCollection);
            RegisterProviders(serviceCollection);
            RegisterReaders(serviceCollection);
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
                .AddSingleton<IRequestFactory, RequestFactory>();
        }

        private static void RegisterReaders(IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddSingleton<IBymaHttpReader, BymaHttpReader>()
                .AddSingleton<IAmbitoHttpReader, AmbitoHttpReader>();
        }

        private static void RegisterProviders(IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddSingleton<IProvider<TituloIsin>, TituloIsinProvider>()
                .AddSingleton<ITitulosProvider, TitulosProvider>()
                .AddSingleton<IProvider<Titulo>, TitulosProvider>()
                .AddSingleton<IDolarBlueProvider, DolarBlueProvider>();
        }

        private static void RegisterFetchers(IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddSingleton<IBymaLetrasFetcher, BymaLetrasFetcher>()
                .AddSingleton<IBymaBonosFetcher, BymaBonosFetcher>()
                .AddSingleton<IBymaTituloDetailsFetcher, BymaTituloDetailsFetcher>()
                .AddSingleton<IAmbitoBlueFetcher, AmbitoBlueFetcher>();
        }

        private static void RegisterMappers(IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddSingleton<IMapper<TituloDto, Titulo>, TituloMapper>()
                .AddSingleton<IMapper<BlueDto, Blue>, BlueMapper>();
        }

        private static void RegisterDeserializers(IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddSingleton<IJsonDeserializer<PaginationDto>, JsonToPaginationDtoDeserializer>()
                .AddSingleton<IJsonDeserializer<TituloDetailsContentDto>, JsonToTituloDetailsContentDtoDeserializer>()
                .AddSingleton<IJsonDeserializer<TituloDto>, JsonToTituloDtoDeserializer>()
                .AddSingleton<IJsonDeserializer<TitulosContentDto>, JsonToTitulosContentDtoDeserializer>()
                .AddSingleton<IJsonDeserializer<TituloDetailsDto>, JsonToTituloDetailsDtoDeserializer>()
                .AddSingleton<IJsonDeserializer<BlueDto>, JsonToBlueDtoDeserializer>()
                .AddSingleton<IJsonValueReader, JsonValueReader>();
        }
    }
}
