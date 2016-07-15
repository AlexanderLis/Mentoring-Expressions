//Создайте класс-трансформатор на основе ExpressionVisitor, выполняющий следующие 2 вида преобразований дерева выражений:
//- Замену выражений вида <переменная> + 1 / <переменная> - 1 на операции инкремента и декремента
//- Замену параметров, входящих в lambda-выражение, на константы (в качестве параметров такого преобразования передавать:
//   - Исходное выражение
//   - Список пар <имя параметра: значение для замены>
//Для контроля полученное дерево выводить в консоль или смотреть результат под отладчиком, использую ExpressionTreeVisualizer, 
//а также компилировать его и вызывать полученный метод.

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ExpressionTrees
{
    class Program
    {
        static void Main(string[] args)
        {
            RunFirstTask();
            RunSecondTask();
        }

        private static void RunFirstTask()
        {
            Func<int, int> func = (a) => (a + 1) * (a + 11) + (1 + a) * (a - 1);
            Expression<Func<int, int>> sourceExpression = (a) => (a + 1) * (a + 11) + (1 + a) * (a - 1);
            var convertedExpression = new IncDecTransformator().VisitAndConvert(sourceExpression, "");

            Console.WriteLine("Source Expression:" + sourceExpression + "\n" + "Result: " + sourceExpression.Compile().Invoke(10));
            Console.WriteLine("Converted Expression: " + convertedExpression + "\n" + "Result: " + convertedExpression.Compile().Invoke(10));
        }

        private static void RunSecondTask()
        {
            var paramVocabulary = new Dictionary<string, int> {{"a", 8}, {"b", 88}, {"c", 888}};

            Expression<Func<int, int, int, int>> sourceExpression = (a, b, c) => (a + 1) * (c + 11) + (1 + c) * (b - 1);
            var convertedExpression = new ParameterTransformator().VisitLocal(sourceExpression, paramVocabulary);

            Console.WriteLine("Source Expression:" + sourceExpression + "\n" + "Result: " +
                              sourceExpression.Compile().Invoke(8, 88, 888));
            Console.WriteLine("Converted Expression: " + convertedExpression + "\n" + "Result: " +
                              convertedExpression.Compile().Invoke());

        }

    }
}
