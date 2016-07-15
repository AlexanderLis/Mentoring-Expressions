using System.Linq.Expressions;

namespace ExpressionTrees
{
    public class IncDecTransformator : ExpressionVisitor
    {
        protected override Expression VisitBinary(BinaryExpression node)
        {

            ParameterExpression param = null;
            ConstantExpression constant = null;

            switch (node.NodeType)
            {
                case ExpressionType.Add:

                    if (node.Left.NodeType == ExpressionType.Parameter)
                    {
                        param = (ParameterExpression) node.Left;
                    }

                    if (node.Right.NodeType == ExpressionType.Constant)
                    {
                        constant = (ConstantExpression) node.Right;
                    }

                    if (param != null && constant != null && constant.Type == typeof (int) && (int) constant.Value == 1)
                    {
                        return Expression.Increment(param);
                    }
                    break;
                case ExpressionType.Subtract:

                    if (node.Left.NodeType == ExpressionType.Parameter)
                    {
                        param = (ParameterExpression) node.Left;
                    }

                    if (node.Right.NodeType == ExpressionType.Constant)
                    {
                        constant = (ConstantExpression) node.Right;
                    }

                    if (param != null && constant != null && constant.Type == typeof (int) && (int) constant.Value == 1)
                    {
                        return Expression.Decrement(param);
                    }
                    break;
            }
            return base.VisitBinary(node);
        }
    }
}
