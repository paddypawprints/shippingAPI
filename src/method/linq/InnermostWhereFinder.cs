using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using System.Linq.Expressions;
using System.Globalization;
using System.Collections;

namespace PitneyBowes.Developer.ShippingApi
{

    internal class InnermostWhereFinder : ExpressionVisitor
    {
        private MethodCallExpression innermostWhereExpression;

        public MethodCallExpression GetInnermostWhere(Expression expression)
        {
            Visit(expression);
            return innermostWhereExpression;
        }

        protected override Expression VisitMethodCall(MethodCallExpression expression)
        {
            if (expression.Method.Name == "Where")
                innermostWhereExpression = expression;

            Visit(expression.Arguments[0]);

            return expression;
        }
    }


}

