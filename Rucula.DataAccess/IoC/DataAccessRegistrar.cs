using Microsoft.Extensions.DependencyInjection;
using Rucula.DataAccess.Deserializers;
using Rucula.DataAccess.Dtos;
using Rucula.DataAccess.Globalization;
using Rucula.DataAccess.Mappers;
using Rucula.DataAccess.Providers;
using Rucula.DataAccess.Providers.Ambito;
using Rucula.DataAccess.Providers.Byma;
using Rucula.DataAccess.Providers.Byma.RequestFactories;
using Rucula.DataAccess.Providers.WesternUnion;
using Rucula.Domain.Abstractions;
using Rucula.Domain.Entities;

namespace Rucula.DataAccess.IoC;

public static class DataAccessRegistrar
{
    public static IServiceCollection AddDataAccess(this IServiceCollection serviceCollection)
    {
        RegisterDeserializers(serviceCollection);
        RegisterProviders(serviceCollection);
        RegisterReaders(serviceCollection);
        RegisterFetchers(serviceCollection);
        RegisterRequestFactories(serviceCollection);
        RegisterMappers(serviceCollection);
        RegisterGlobalization(serviceCollection);

        return serviceCollection;
    }

    private static void RegisterRequestFactories(IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<IBymaRequestFactory, BymaRequestFactory>()
            .AddSingleton<ITituloDetailsRequestFactory, TituloDetailsRequestFactory>()
            .AddSingleton<ILetrasRequestFactory, LetrasRequestFactory>()
            .AddSingleton<IBonosRequestFactory, BonosRequestFactory>()
            .AddSingleton<IBymaIsOpenMarketRequestFactory, BymaIsOpenMarketRequestFactory>();
    }

    private static void RegisterReaders(IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<IHttpReader, HttpReader>();
    }

    private static void RegisterGlobalization(IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<ISpanishNumberConverter, SpanishNumberConverter>();
    }

    private static void RegisterProviders(IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<ITitulosProvider, TitulosProvider>()
            .AddSingleton<IProvider<Titulo>, TitulosProvider>()
            .AddSingleton<ITituloDetailsProvider, TituloDetailsProvider>()
            .AddSingleton<IDolarBlueProvider, DolarBlueProvider>()
            .AddSingleton<IDolarCryptoProvider, DolarCryptoProvider>()
            .AddSingleton<IWesternUnionProvider, WesternUnionProvider>();
    }

    private static void RegisterFetchers(IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<IBymaIsMarketOpenFetcher, BymaIsOpenMarketFetcher>()
            .AddSingleton<IBymaLetrasFetcher, BymaLetrasFetcher>()
            .AddSingleton<IBymaBonosFetcher, BymaBonosFetcher>()
            .AddSingleton<IBymaTituloDetailsFetcher, BymaTituloDetailsFetcher>()
            .AddSingleton<IAmbitoBlueFetcher, AmbitoBlueFetcher>()
            .AddSingleton<IAmbitoDolarCryptoFetcher, AmbitoDolarCryptoFetcher>()
            .AddSingleton<IWesternUnionFetcher, WesternUnionFetcher>();
    }

    private static void RegisterMappers(IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<IMapper<TituloDto, Titulo>, TituloMapper>()
            .AddSingleton<IMapper<TituloDetailsDto, TituloDetails>, TituloDetailsMapper>()
            .AddSingleton<IMapper<BlueDto, Blue>, BlueMapper>()
            .AddSingleton<IMapper<DolarCryptoDto, DolarCrypto>, DolarCryptoMapper>()
            .AddSingleton<IMapper<DolarWesternUnionDto, DolarWesternUnion>, DolarWesternUnionMapper>();
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
            .AddSingleton<IJsonDeserializer<DolarCryptoDto>, JsonToDolarCryptoDtoDeserializer>()
            .AddSingleton<IJsonDeserializer<DolarWesternUnionDto>, JsonToDolarWesternUnionDtoDeserializer>()
            .AddSingleton<IJsonValueReader, JsonValueReader>();
    }
}
