using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Sample03
{
	public class ExpressionToFTSRequestTranslator : ExpressionVisitor
	{
		StringBuilder resultString;
        List<string> resultStrings = new List<string>();

		public IEnumerable<string> Translate(Expression exp)
		{
			resultString = new StringBuilder();
			Visit(exp);

		    return resultStrings;
		}

	    protected override Expression VisitMethodCall(MethodCallExpression node)
	    {
	        if (node.Method.DeclaringType == typeof (Queryable)
	            && node.Method.Name == "Where")
	        {
	            var predicate = node.Arguments[1];
	            Visit(predicate);

	            return node;
	        }

	        resultString.Clear();
	        switch (node.Method.Name)
	        {
	            case "StartsWith":
	                Visit(node.Object);
	                resultString.Append("(");
	                Visit(node.Arguments);
	                resultString.Append("*)");
                    resultStrings.Add(resultString.ToString());
	                return node;
	                break;
	            case "EndsWith":
	                Visit(node.Object);
	                resultString.Append("(*");
	                Visit(node.Arguments);
	                resultString.Append(")");
                    resultStrings.Add(resultString.ToString());
	                return node;
	                break;
	            case "Contains":
	                Visit(node.Object);
	                resultString.Append("(*");
	                Visit(node.Arguments);
	                resultString.Append("*)");
                    resultStrings.Add(resultString.ToString());
	                return node;
	                break;
	        }

	        return base.VisitMethodCall(node);
	    }

	    protected override Expression VisitBinary(BinaryExpression node)
		{
		    switch (node.NodeType)
		    {
		        case ExpressionType.Equal:
		            if (node.Left.NodeType == ExpressionType.MemberAccess && node.Right.NodeType == ExpressionType.Constant)
		            {
		                resultString.Clear();
		                Visit(node.Left);
		                resultString.Append("(");
		                Visit(node.Right);
		                resultString.Append(")");
                        resultStrings.Add(resultString.ToString());
		            }
                    else if (node.Left.NodeType == ExpressionType.Constant && node.Right.NodeType == ExpressionType.MemberAccess)
                    {
                        resultString.Clear();
		                Visit(node.Right);
		                resultString.Append("(");
		                Visit(node.Left);
		                resultString.Append(")");
                        resultStrings.Add(resultString.ToString());
		            }
                    else
                    {
                        throw new NotSupportedException(string.Format("The operand should be either constant or property/field ", node.NodeType));
		            }
                    break;
                case ExpressionType.AndAlso:
		            Visit(node.Left);
		            Visit(node.Right);
                    
		            break;
		        default:
		            throw new NotSupportedException(string.Format("Operation {0} is not supported", node.NodeType));
		    }
		    return node;
		}

		protected override Expression VisitMember(MemberExpression node)
		{
			resultString.Append(node.Member.Name).Append(":");

			return base.VisitMember(node);
		}

		protected override Expression VisitConstant(ConstantExpression node)
		{
			resultString.Append(node.Value);

			return node;
		}


	}
}
