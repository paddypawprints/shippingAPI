﻿using System;
using System.Linq;
using System.Linq.Expressions;

namespace PitneyBowes.Developer.ShippingApi
{
    public class ReplaceQueryWithResultSetModifier<TResult, TReport> : ExpressionVisitor
    {
        private TReport _queryableTransactions;

        public ReplaceQueryWithResultSetModifier(TReport transactions)
        {
            _queryableTransactions = transactions;
        }

        protected override Expression VisitConstant(ConstantExpression c)
        {
            // Replace the constant QueryableTerraServerData arg with the queryable Place collection. 
            if (IsAssignableGeneric(typeof(TReport), c.Type))
                return Expression.Constant(_queryableTransactions);
            else
                return c;
        }

        protected bool IsAssignableGeneric(Type type, Type typeFrom)
        {
            return type.IsAssignableFrom(typeFrom);
        }

    }


}

