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
        public static void Register(ServiceCollection serviceCollection)
        {
            /*
             * ILetrasBymaHttpConfig letrasBymaHttpConfig, IBymaHttpReader bymaHttpReader
             * IRequestFactory requestFactory, IHandlerFactory handlerFactory
             * */
            serviceCollection
                .AddSingleton<IProvider<TituloIsin>, TituloIsinProvider>()
                .AddSingleton<IBymaLetrasFetcher, BymaLetrasFetcher>()
                .AddSingleton<IBymaBonosFetcher, BymaBonosFetcher>()
                .AddSingleton<IBymaTituloDetailsFetcher, BymaTituloDetailsFetcher>()
                .AddSingleton<IJsonDeserializer<TitulosContentDto>, JsonToTitulosContentDtoDeserializer>()
                .AddSingleton<IJsonDeserializer<TituloDetailsContentDto>, JsonToTituloDetailsContentDtoDeserializer>()
                .AddSingleton<IMapper<TituloDto, Titulo>, TituloMapper>()
                .AddSingleton<ILetrasBymaHttpConfig, LetrasBymaHttpConfig>()
                .AddSingleton<IBonosBymaHttpConfig, BonosBymaHttpConfig>()
                .AddSingleton<IBymaHttpReader, BymaHttpReader>()
                .AddSingleton<IRequestFactory, RequestFactory>()
                .AddSingleton<IHandlerFactory, HandlerFactory>();

        }
    }
}
