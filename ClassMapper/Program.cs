//Используя возможность конструировать Expression Tree и выполнять его код, создайте собственный механизм маппинга (копирующего поля (свойства) одного класса в другой).
//Приблизительный интерфейс и пример использования приведен ниже (MapperGenerator – фабрика мапперов, Mapper – класс маппинга). 
//Обратите внимание, что в данном примере создается только новый экземпляр конечного класса, но сами данные не копируются.

//public class Mapper< TSource, TDestination >
//{
//    Func< TSource, TDestination > mapFunction;
//    internal Mapper(Func< TSource, TDestination > func)
//    {
//        mapFunction = func;
//    }
//    public TDestination Map(TSource source)
//    {
//        return mapFunction(source);
//    }
//}
//public class MappingGenerator
//{
//    public Mapper< TSource, TDestination > Generate< TSource, TDestination >()
//    {
//        var sourceParam = Expression.Parameter(typeof(TSource));
//        var mapFunction = 
//            Expression.Lambda< Func < TSource, TDestination > >(
//            Expression.New(typeof(TDestination)),
//            sourceParam
//            );
 
//        return new Mapper< TSource, TDestination >(mapFunction.Compile());
//    }
//}
//public class Foo { }
//public class Bar { }
 
//[TestMethod]
//public void TestMethod3()
//{
//    var mapGenerator = new MappingGenerator();
//    var mapper = mapGenerator.Generate< Foo, Bar >();
 
//    var res = mapper.Map(new Foo());
//}

using System;
using System.Linq.Expressions;

namespace ClassMapper
{
    class Program
    {
        static void Main(string[] args)
        {

            var mapGenerator = new MappingGenerator();
            var mapper = mapGenerator.Generate<Foo, Bar>();

            var res = mapper.Map(new Foo());
        }
    }

    public class Mapper< TSource, TDestination >
    {
        Func< TSource, TDestination > mapFunction;
        internal Mapper(Func< TSource, TDestination > func)
        {
            mapFunction = func;
        }

        public TDestination Map(TSource source)
        {
            return mapFunction(source);
        }
    }
    public class MappingGenerator
    {
        public Mapper< TSource, TDestination > Generate< TSource, TDestination >()
        {
            var sourceParam = Expression.Parameter(typeof(TSource));
            var mapFunction = 
                Expression.Lambda< Func < TSource, TDestination > >(
                Expression.New(typeof(TDestination)),
                sourceParam
                );

            return new Mapper< TSource, TDestination >(mapFunction.Compile());
        }
    }
    public class Foo { }
    public class Bar { }

}
