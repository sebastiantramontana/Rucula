using Microsoft.Extensions.DependencyInjection;
using Rucula.DataAccess.Deserializers;
using Rucula.DataAccess.Dtos;
using Rucula.DataAccess.Globalization;
using Rucula.DataAccess.Mappers;
using Rucula.DataAccess.Providers;
using Rucula.DataAccess.Providers.Ambito;
using Rucula.DataAccess.Providers.Byma;
using Rucula.DataAccess.Providers.Byma.RequestFactories;
using Rucula.DataAccess.Providers.CryptoYa;
using Rucula.DataAccess.Providers.WesternUnion;
using Rucula.Domain.Abstractions;
using Rucula.Domain.Entities;

namespace Rucula.DataAccess.IoC;

public static class DataAccessRegistrar
{
    public static IServiceCollection AddDataAccess(this IServiceCollection serviceCollection)
        => serviceCollection
            .RegisterDeserializers()
            .RegisterProviders()
            .RegisterReaders()
            .RegisterFetchers()
            .RegisterRequestFactories()
            .RegisterMappers()
            .RegisterGlobalization();

    private static IServiceCollection RegisterRequestFactories(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddSingleton<IBymaRequestPostFactory, BymaRequestPostFactory>()
            .AddSingleton<ITituloDetailsRequestFactory, TituloDetailsRequestFactory>()
            .AddSingleton<ILetrasRequestFactory, LetrasRequestFactory>()
            .AddSingleton<IBonosRequestFactory, BonosRequestFactory>()
            .AddSingleton<IBymaIsOpenMarketRequestFactory, BymaIsOpenMarketRequestFactory>();

    private static IServiceCollection RegisterReaders(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddSingleton<IHttpReader, HttpReader>();

    private static IServiceCollection RegisterGlobalization(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddSingleton<ISpanishNumberConverter, SpanishNumberConverter>();

    private static IServiceCollection RegisterProviders(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddSingleton<ITitulosProvider, TitulosProvider>()
            .AddSingleton<IProvider<Titulo>, TitulosProvider>()
            .AddSingleton<ITituloDetailsProvider, TituloDetailsProvider>()
            .AddSingleton<IDolarBlueProvider, DolarBlueProvider>()
            .AddSingleton<IWesternUnionProvider, WesternUnionProvider>()
            .AddSingleton<IDolarCryptoFeesProvider, DolarCryptoFeesProvider>()
            .AddSingleton<IDolarCryptoGrossPricesProvider, DolarCryptoGrossPricesProvider>();

    private static IServiceCollection RegisterFetchers(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddSingleton<IBymaIsMarketOpenFetcher, BymaIsOpenMarketFetcher>()
            .AddSingleton<IBymaLetrasFetcher, BymaLetrasFetcher>()
            .AddSingleton<IBymaBonosFetcher, BymaBonosFetcher>()
            .AddSingleton<IBymaTituloDetailsFetcher, BymaTituloDetailsFetcher>()
            .AddSingleton<IAmbitoBlueFetcher, AmbitoBlueFetcher>()
            .AddSingleton<IWesternUnionFetcher, WesternUnionFetcher>()
            .AddSingleton<ICryptoYaFeesFetcher, CryptoYaFeesFetcher>()
            .AddSingleton<ICryptoYaGrossPricesFetcher, CryptoYaGrossPricesFetcher>();

    private static IServiceCollection RegisterMappers(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddSingleton<IMapper<TituloDto, Titulo>, TituloMapper>()
            .AddSingleton<IMapper<TituloDetailsDto, TituloDetails>, TituloDetailsMapper>()
            .AddSingleton<IMapper<BlueDto, Blue>, BlueMapper>()
            .AddSingleton<IMapper<DolarWesternUnionDto, DolarWesternUnionInfo>, DolarWesternUnionMapper>()
            .AddSingleton<IMapper<DolarCryptoFeesDto, DolarCryptoFees>, DolarCrpyoFeesMapper>();

    private static IServiceCollection RegisterDeserializers(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddSingleton<IJsonDeserializer<PaginationDto>, JsonToPaginationDtoDeserializer>()
            .AddSingleton<IJsonDeserializer<TituloDetailsContentDto>, JsonToTituloDetailsContentDtoDeserializer>()
            .AddSingleton<IJsonDeserializer<TituloDto>, JsonToTituloDtoDeserializer>()
            .AddSingleton<IJsonDeserializer<TitulosContentDto>, JsonToTitulosContentDtoDeserializer>()
            .AddSingleton<IJsonDeserializer<TituloDetailsDto>, JsonToTituloDetailsDtoDeserializer>()
            .AddSingleton<IJsonDeserializer<BlueDto>, JsonToBlueDtoDeserializer>()
            .AddSingleton<IJsonDeserializer<DolarWesternUnionDto>, JsonToDolarWesternUnionDtoDeserializer>()
            .AddSingleton<IJsonDeserializer<IEnumerable<DolarCryptoFeesDto>>, JsonToDolarCryptoFeesDtoDeserializer>()
            .AddSingleton<IJsonDeserializer<IEnumerable<DolarCryptoCurrencyGrossPriceDto>>, JsonToDolarCryptoCurrencyGrossPriceDto>()
            .AddSingleton<IJsonValueReader, JsonValueReader>();
}
