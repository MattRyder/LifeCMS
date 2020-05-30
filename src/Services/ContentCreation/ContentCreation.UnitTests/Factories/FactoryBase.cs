using System;
using System.Collections.Generic;
using LifeCMS.Services.ContentCreation.Domain.Common;

namespace LifeCMS.Services.ContentCreation.UnitTests.Factories
{
    public class FactoryBase<T> where T : BaseEntity
    {
        protected static List<T> MakeList(Func<T> createFunc, int count = 0)
        {
            if (count == 0)
            {
                count = new Random().Next(1, 20);
            }

            var list = new List<T>();

            for (var i = 0; i < count; i++)
            {
                list.Add(createFunc());
            }

            return list;
        }
    }
}