using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PitneyBowes.Developer.ShippingApi
{
    /// <summary> 
    /// Evaluates & replaces sub-trees when first candidate is reached (top-down) 
    /// </summary> 
    internal class SubtreeEvaluator : ExpressionVisitor
    {
        HashSet<Expression> candidates;

        internal SubtreeEvaluator(HashSet<Expression> candidates)
        {
            this.candidates = candidates;
        }

        internal Expression Eval(Expression exp)
        {
            return this.Visit(exp);
        }

        public override Expression Visit(Expression exp)
        {
            if (exp == null)
            {
                return null;
            }
            if (this.candidates.Contains(exp))
            {
                return this.Evaluate(exp);
            }
            return base.Visit(exp);
        }

        private Expression Evaluate(Expression e)
        {
            if (e.NodeType == ExpressionType.Constant)
            {
                return e;
            }
            LambdaExpression lambda = Expression.Lambda(e);
            Delegate fn = lambda.Compile();
            return Expression.Constant(fn.DynamicInvoke(null), e.Type);
        }
    }


}

