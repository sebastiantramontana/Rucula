using Microsoft.Extensions.DependencyInjection;
using Rucula.DataAccess.Converters;
using Rucula.DataAccess.Dtos;
using Rucula.DataAccess.Fetching;
using Rucula.DataAccess.Fetching.Byma;
using Rucula.DataAccess.Fetching.Byma.Config;
using Rucula.DataAccess.Mappers;
using Rucula.Domain.Abstractions;
using Rucula.Domain.Entities;
using System.Text.Json.Serialization;

namespace Rucula.DataAccess.IoC
{
    public static class Registrar
    {
        public static Register(ServiceCollection serviceCollection)
        {
            /*
             * ILetrasBymaHttpConfig letrasBymaHttpConfig, IBymaHttpReader bymaHttpReader
             * IRequestFactory requestFactory, IHandlerFactory handlerFactory
             * */
            serviceCollection
                .AddSingleton<IFetching<TituloIsin>, TituloIsinFetching>()
                .AddSingleton<IBymaLetrasFetcher, BymaLetrasFetcher>()
                .AddSingleton<IBymaBonosFetcher, BymaBonosFetcher>()
                .AddSingleton<IBymaTituloDetailsFetcher, BymaTituloDetailsFetcher>()
                .AddSingleton<IJsonConverter<TitulosContentDto>, JsonToTitulosContentDtoConverter>()
                .AddSingleton<IJsonConverter<TituloDetailsContentDto>, JsonToTituloDetailsContentDtoConverter>()
                .AddSingleton<IMapper<TituloDto, Titulo>, TituloMapper>()
                .AddSingleton<ILetrasBymaHttpConfig, LetrasBymaHttpConfig>()
                .AddSingleton<IBonosBymaHttpConfig, BonosBymaHttpConfig>()
                .AddSingleton<IBymaHttpReader, BymaHttpReader>()
                .AddSingleton<IRequestFactory, RequestFactory>()
                .AddSingleton<IHandlerFactory, HandlerFactory>();

        }
    }
}
