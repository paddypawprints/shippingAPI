using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PitneyBowes.Developer.ShippingApi
{
    public abstract class ReportProviderBase
    {
        public virtual IQueryable CreateQuery(Expression expression)
        {
            Type elementType = TypeSystem.GetElementType(expression.Type);
            try
            {
                return (IQueryable)Activator.CreateInstance(typeof(TransactionsReport<>).MakeGenericType(elementType), new object[] { this, expression });
            }
            catch (System.Reflection.TargetInvocationException tie)
            {
                throw tie.InnerException;
            }
        }

        public object Execute<TResult, ReportItem, Request, RequestFinder>(Expression expression, bool isEnumerable, Func<Request, IEnumerable<ReportItem>> reportService, Action<Request> initializeRequest)
            where Request : IReportRequest, new()
            where RequestFinder : RequestFinderVisitor<Request, ReportItem>, new()
        {
            // The expression must represent a query over the data source. 
            if (!(expression is MethodCallExpression))
                throw new InvalidProgramException("No query over the data source was specified.");

            var rf = new RequestFinder() { Expression = GetWhereExpression(expression).Body };
            var request = rf.Request;
            initializeRequest(request);
            if (!request.Validate())
                throw new System.InvalidOperationException("Execute: Request is not valid"); 

            // Call the Web service and get the results.
            var report = reportService(request);

            // Copy the IEnumerable places to an IQueryable.
            IQueryable<ReportItem> queryableTransaction = report.AsQueryable();

            var newExpressionTree = ReplaceWhereByTransaction<TResult, IQueryable<ReportItem>>(expression, queryableTransaction);
            // This step creates an IQueryable that executes by replacing Queryable methods with Enumerable methods.
            if (isEnumerable)
                return queryableTransaction.Provider.CreateQuery(newExpressionTree);
            else
                return queryableTransaction.Provider.Execute(newExpressionTree);

        }

        public Expression ReplaceWhereByTransaction<TResult, ReportItem>(Expression expression, ReportItem items ) 
        {
            // Copy the expression tree that was passed in, changing only the first 
            // argument of the innermost MethodCallExpression.

            var treeCopier = new ReplaceQueryWithResultSetModifier<TResult, ReportItem>(items);
            return treeCopier.Visit(expression);
         }


        public LambdaExpression GetWhereExpression(Expression expression)
        {
            // Find the call to Where() and get the lambda expression predicate.
            var whereFinder = new InnermostWhereFinder();
            var whereExpression = whereFinder.GetInnermostWhere(expression);
            var lambdaExpression = (LambdaExpression)((UnaryExpression)(whereExpression.Arguments[1])).Operand;

            // Send the lambda expression through the partial evaluator.
            lambdaExpression = (LambdaExpression)Evaluator.PartialEval(lambdaExpression);
            return lambdaExpression;
        }


        // Queryable's "single value" standard query operators call this method.
        // It is also called from QueryableTerraServerData.GetEnumerator(). 
        public virtual TResult Execute<TResult>(Expression expression)
        {
            bool IsEnumerable = (typeof(TResult).Name == "IEnumerable`1");

            return (TResult)Execute<TResult>(expression, IsEnumerable);
        }

        public virtual object Execute(Expression expression)
        {
            Type elementType = TypeSystem.GetElementType(expression.Type);
            try
            {
                throw new NotImplementedException(); //TODO: Figure out how to know the return type
                //call internal static object Execute<TResult>(Expression expression, bool IsEnumerable), where typeof(TResult) = elementType;
            }
            catch (System.Reflection.TargetInvocationException tie)
            {
                throw tie.InnerException;
            }
        }
        // Queryable's collection-returning standard query operators call this method. 
        public abstract IQueryable<TResult> CreateQuery<TResult>(Expression expression);
        public abstract object Execute<TResult>(Expression expression, bool IsEnumerable);
    }


}

