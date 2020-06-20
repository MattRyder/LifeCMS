using System;
using System.Data;
using Dapper;

namespace LifeCMS.Services.ContentCreation.API.Infrastructure.Dapper
{
    public class UriTypeHandler : SqlMapper.TypeHandler<Uri>
    {
        public override Uri Parse(object value)
        {
            var uri = value as string;

            return new Uri(uri);
        }

        public override void SetValue(IDbDataParameter parameter, Uri value)
        {
            parameter.Value = value.ToString();
        }
    }
}