﻿namespace Rucula.DataAccess.Providers.Byma.Config
{
    internal interface IParametrizableBymaHttpConfig
    {
        HttpRequestMessage CreateRequest(string parameters);
    }
}
