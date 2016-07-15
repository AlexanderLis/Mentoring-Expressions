using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;

namespace ExpressionTrees
{
    public class ParameterTransformator : ExpressionVisitor
    {
        private Dictionary<string, int> _dictionary;

        protected override Expression VisitParameter(ParameterExpression root)
        {
                if (_dictionary.ContainsKey(root.Name))
                {
                    var param = _dictionary[root.Name];

                    return Expression.Constant(param);
                }

            return base.VisitParameter(root);
        }

        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            return Expression.Lambda<Func<int>>(Visit(node.Body), null);
        }


        public Expression<Func<int>> VisitLocal(Expression root, Dictionary<string, int> dictionary)
        {
            _dictionary = dictionary;
            return VisitAndConvert(root, "") as Expression<Func<int>>;
        }
    }
}
