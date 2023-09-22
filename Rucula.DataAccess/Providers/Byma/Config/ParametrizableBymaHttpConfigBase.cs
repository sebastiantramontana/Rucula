﻿namespace Rucula.DataAccess.Providers.Byma.Config
{
    internal abstract class ParametrizableBymaHttpConfigBase : IParametrizableBymaHttpConfig
    {
        private readonly string _url;
        private readonly IRequestFactory _requestFactory;

        protected ParametrizableBymaHttpConfigBase(string url, IRequestFactory requestFactory)
        {
            _url = url;
            _requestFactory = requestFactory;
        }

        public HttpRequestMessage CreateRequest(string parameters)
            => _requestFactory.CreateRequest(_url, parameters);
    }
}
