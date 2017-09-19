using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PitneyBowes.Developer.ShippingApi
{
    public class ReportBase<T> : IOrderedQueryable<T>
    {

        public IQueryProvider Provider { get; protected set; }
        public Expression Expression { get; private set; }

        public ReportBase()
        {
            Expression = Expression.Constant(this);
        }

        public ReportBase(IQueryProvider provider, Expression expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression is null");
            }

            if (!typeof(IQueryable<T>).IsAssignableFrom(expression.Type))
            {
                throw new ArgumentOutOfRangeException("expression type mismatch");
            }

            Provider = provider ?? throw new ArgumentNullException("provider is null");
            Expression = expression;
        }

        public Type ElementType
        {
            get => typeof(T);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return (Provider.Execute<IEnumerable<T>>(Expression)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (Provider.Execute<IEnumerable>(Expression)).GetEnumerator();
        }

    }


}

