using System;
using System.Linq;
using System.Linq.Expressions;

namespace ClassMapper
{
    public class MappingGenerator
    {
        public Mapper<TSource, TDestination> Generate<TSource, TDestination>()
        {
            var sourceParam = Expression.Parameter(typeof(TSource));

            var memberBindings = (from property in sourceParam.Type.GetProperties()
                                  where typeof(TDestination).GetProperty(property.Name) != null
                                  select
                                      Expression.Bind(typeof(TDestination).GetProperty(property.Name),
                                          Expression.Property(sourceParam, property))).Cast<MemberBinding>().ToList();

            memberBindings.AddRange(from field in sourceParam.Type.GetFields()
                                    where typeof(TDestination).GetField(field.Name) != null
                                    select Expression.Bind(typeof(TDestination).GetField(field.Name), Expression.Field(sourceParam, field)));

            var body = Expression.MemberInit(Expression.New(typeof(TDestination)), memberBindings);

            var mapFunction =
                Expression.Lambda<Func<TSource, TDestination>>(body, sourceParam);

            return new Mapper<TSource, TDestination>(mapFunction.Compile());
        }
    }

}
