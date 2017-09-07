using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Reflection;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Globalization;
using System.Collections;

namespace PitneyBowes.Developer.ShippingApi
{

    public class TransactionsReportRequest : IShippingApiRequest
    {
        [ShippingAPIResource("developers", true, "/transactions/reports")]
        public string DeveloperId { get; set; }
        [ShippingAPIQuery("fromDate")]
        public DateTimeOffset FromDate { get; set; }
        [ShippingAPIQuery("toDate")]
        public DateTimeOffset ToDate { get; set; }
        [ShippingAPIQuery("transactionId", true)]
        public string TransactionId { get; set; }
        [ShippingAPIQuery("parcelTrackingNumber", true)]
        public string ParcelTrackingNumber { get; set; }
        [ShippingAPIQuery("merchantId", true)]
        public string MerchantId { get; set; }
        [ShippingAPIQuery("transactionType", true)]
        public TransactionType? TransactionType { get; set; }
        [ShippingAPIQuery("size", true)]
        public int PageSize { get; set; }
        [ShippingAPIQuery("page", true)]
        public int Page { get; set; }
        [ShippingAPIQuery("sort", true)]
        public string Sort { get; set; }
        [ShippingAPIHeader("Bearer")]
        public StringBuilder Authorization { get; set; }
        [ShippingAPIHeader("Accept-Language")]
        public string AcceptLanguage { get; set; }
        public string ContentType { get => "application/json"; set => throw new NotImplementedException(); }

        public TransactionsReportRequest()
        {
            AcceptLanguage = CultureInfo.CurrentCulture.Name;
        }
    }

    public class TransactionPageResponse
    {
        [JsonProperty("content")]
        public IEnumerable<ITransaction> Content { get; set; }
        [JsonProperty("totalPages")]
        public int TotalPages { get; set; }
        [JsonProperty("totalElements")]
        public int TotalElements { get; set; }
        [JsonProperty("last")]
        public bool LastPage { get; set; }
        [JsonProperty("size")]
        public int Size { get; set; }
        [JsonProperty("number")]
        public int PageIndex { get; set; }
        [JsonProperty("sort")]
        public string Sort { get; set; }
        [JsonProperty("numberOfElements")]
        public int Count { get; set; }
        [JsonProperty("first")]
        public bool FirstPage { get; set; }
    }



    public class TransactionsReport<T> : IOrderedQueryable<T> 
    {

        public IQueryProvider Provider { get; private set; }
        public Expression Expression { get; private set; }

        public TransactionsReport()
        {
            Provider = new TransactionsReportProvider();
            Expression = Expression.Constant(this);
        }

        public TransactionsReport( TransactionsReportProvider provider, Expression expression)
        {
            Provider = provider;
            Expression = expression;
        }

        public Type ElementType
        {
            get { return typeof(T); }
        }

        public async static Task<ShippingAPIResponse<TransactionPageResponse>> TransactionPage(TransactionsReportRequest request, ShippingApi.Session session = null)
        {
            if (session == null) session = ShippingApi.DefaultSession;
            return await WebMethod.Get<TransactionPageResponse, TransactionsReportRequest >("/v2/ledger", request, session);
        }

        public static IEnumerable<ITransaction> Report(TransactionsReportRequest request, ShippingApi.Session session = null)
        {
            if (session == null) session = ShippingApi.DefaultSession;
            TransactionPageResponse page;
            do
            {
                var response = TransactionPage(request, session).GetAwaiter().GetResult();
                page = response.APIResponse;
                request.Page += 1;
                foreach (var t in page.Content) yield return t;
            } while (!page.LastPage);
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

    public class TransactionsReportProvider : IQueryProvider
    {
        public IQueryable CreateQuery(Expression expression)
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

        // Queryable's collection-returning standard query operators call this method. 
        public IQueryable<TResult> CreateQuery<TResult>(Expression expression)
        {
            return new TransactionsReport<TResult>(this, expression);
        }

        public object Execute(Expression expression)
        {
            return TransactionsReportContext.Execute(expression, false);
        }

        // Queryable's "single value" standard query operators call this method.
        // It is also called from QueryableTerraServerData.GetEnumerator(). 
        public TResult Execute<TResult>(Expression expression)
        {
            bool IsEnumerable = (typeof(TResult).Name == "IEnumerable`1");

            return (TResult)TransactionsReportContext.Execute(expression, IsEnumerable);
        }
    }
    class TransactionsReportContext
    {
        // Executes the expression tree that is passed to it. 
        internal static object Execute(Expression expression, bool IsEnumerable)
        {
            // The expression must represent a query over the data source. 
            if (!IsQueryOverDataSource(expression))
                throw new InvalidProgramException("No query over the data source was specified.");

            // Find the call to Where() and get the lambda expression predicate.
            InnermostWhereFinder whereFinder = new InnermostWhereFinder();
            MethodCallExpression whereExpression = whereFinder.GetInnermostWhere(expression);
            LambdaExpression lambdaExpression = (LambdaExpression)((UnaryExpression)(whereExpression.Arguments[1])).Operand;

            // Send the lambda expression through the partial evaluator.
            lambdaExpression = (LambdaExpression)Evaluator.PartialEval(lambdaExpression);

            // Get the place name(s) to query the Web service with.
            TransactionsReportRequestFinder rf = new TransactionsReportRequestFinder(lambdaExpression.Body);
            TransactionsReportRequest request = rf.Request;
            if (request.ToDate == null || request.FromDate == null)
                throw new Exception("You must specify FromDate and ToDate"); // TODO

            // Call the Web service and get the results.
            var transactions=TransactionsReport<ITransaction>.Report(request);

            // Copy the IEnumerable places to an IQueryable.
            IQueryable<ITransaction> queryableTransactions = transactions.AsQueryable<ITransaction>();

            // Copy the expression tree that was passed in, changing only the first 
            // argument of the innermost MethodCallExpression.
            ExpressionTreeModifier treeCopier = new ExpressionTreeModifier(queryableTransactions);
            Expression newExpressionTree = treeCopier.Visit(expression);

            // This step creates an IQueryable that executes by replacing Queryable methods with Enumerable methods. 
            if (IsEnumerable)
                return queryableTransactions.Provider.CreateQuery(newExpressionTree);
            else
                return queryableTransactions.Provider.Execute(newExpressionTree);
        }

        private static bool IsQueryOverDataSource(Expression expression)
        {
            // If expression represents an unqueried IQueryable data source instance, 
            // expression is of type ConstantExpression, not MethodCallExpression. 
            return (expression is MethodCallExpression);
        }
    }

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
    internal class TransactionsReportRequestFinder : ExpressionVisitor
    {
        private Expression expression;
        private TransactionsReportRequest request;

        public TransactionsReportRequestFinder(Expression exp)
        {
            this.expression = exp;
        }

        public TransactionsReportRequest Request
        {
            get
            {
                if (request == null)
                {
                    request = new TransactionsReportRequest();
                    this.Visit(this.expression);
                }
                return this.request;
            }
        }

        protected override Expression VisitBinary(BinaryExpression be)
        {
            if (be.NodeType == ExpressionType.Equal)
            {
                if (ExpressionTreeHelpers.IsMemberEqualsValueExpression(be, typeof(ITransaction), "MerchantId"))
                {
                    request.MerchantId = (ExpressionTreeHelpers.GetValueFromEqualsExpression<string>(be, typeof(ITransaction), "MerchantId"));
                    return be;
                }
                else if (ExpressionTreeHelpers.IsMemberEqualsValueExpression(be, typeof(ITransaction), "TransactionType"))
                {
                    request.TransactionType = (ExpressionTreeHelpers.GetValueFromEqualsExpression<TransactionType>(be, typeof(ITransaction), "TransactionType"));
                    return be;
                }
                else
                    return base.VisitBinary(be);
            }
            else if (be.NodeType == ExpressionType.GreaterThanOrEqual)
            {
                if (ExpressionTreeHelpers.IsMemberEqualsValueExpression(be, typeof(ITransaction), "TransactionDateTime"))
                {
                    request.FromDate = ExpressionTreeHelpers.GetValueFromEqualsExpression<DateTimeOffset>(be, typeof(ITransaction), "TransactionDateTime");
                    return be;
                }
                else
                    return base.VisitBinary(be);
            }
            else if (be.NodeType == ExpressionType.LessThanOrEqual)
            {
                if (ExpressionTreeHelpers.IsMemberEqualsValueExpression(be, typeof(ITransaction), "TransactionDateTime"))
                {
                    request.ToDate = ExpressionTreeHelpers.GetValueFromEqualsExpression<DateTimeOffset>(be, typeof(ITransaction), "TransactionDateTime");
                    return be;
                }
                else
                    return base.VisitBinary(be);
            }
            else
                return base.VisitBinary(be);
        }
    }
    internal class ExpressionTreeModifier : ExpressionVisitor
    {
        private IQueryable<ITransaction> queryablePlaces;

        internal ExpressionTreeModifier(IQueryable<ITransaction> places)
        {
            this.queryablePlaces = places;
        }

        protected override Expression VisitConstant(ConstantExpression c)
        {
            // Replace the constant QueryableTerraServerData arg with the queryable Place collection. 
            if (c.Type == typeof(TransactionsReport<ITransaction>))
                return Expression.Constant(this.queryablePlaces);
            else
                return c;
        }
    }

    public static class Evaluator
    {
        /// <summary> 
        /// Performs evaluation & replacement of independent sub-trees 
        /// </summary> 
        /// <param name="expression">The root of the expression tree.</param>
        /// <param name="fnCanBeEvaluated">A function that decides whether a given expression node can be part of the local function.</param>
        /// <returns>A new tree with sub-trees evaluated and replaced.</returns> 
        public static Expression PartialEval(Expression expression, Func<Expression, bool> fnCanBeEvaluated)
        {
            return new SubtreeEvaluator(new Nominator(fnCanBeEvaluated).Nominate(expression)).Eval(expression);
        }

        /// <summary> 
        /// Performs evaluation & replacement of independent sub-trees 
        /// </summary> 
        /// <param name="expression">The root of the expression tree.</param>
        /// <returns>A new tree with sub-trees evaluated and replaced.</returns> 
        public static Expression PartialEval(Expression expression)
        {
            return PartialEval(expression, Evaluator.CanBeEvaluatedLocally);
        }

        private static bool CanBeEvaluatedLocally(Expression expression)
        {
            return expression.NodeType != ExpressionType.Parameter;
        }

        /// <summary> 
        /// Evaluates & replaces sub-trees when first candidate is reached (top-down) 
        /// </summary> 
        class SubtreeEvaluator : ExpressionVisitor
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

        /// <summary> 
        /// Performs bottom-up analysis to determine which nodes can possibly 
        /// be part of an evaluated sub-tree. 
        /// </summary> 
        class Nominator : ExpressionVisitor
        {
            Func<Expression, bool> fnCanBeEvaluated;
            HashSet<Expression> candidates;
            bool cannotBeEvaluated;

            internal Nominator(Func<Expression, bool> fnCanBeEvaluated)
            {
                this.fnCanBeEvaluated = fnCanBeEvaluated;
            }

            internal HashSet<Expression> Nominate(Expression expression)
            {
                this.candidates = new HashSet<Expression>();
                this.Visit(expression);
                return this.candidates;
            }

            public override Expression Visit(Expression expression)
            {
                if (expression != null)
                {
                    bool saveCannotBeEvaluated = this.cannotBeEvaluated;
                    this.cannotBeEvaluated = false;
                    base.Visit(expression);
                    if (!this.cannotBeEvaluated)
                    {
                        if (this.fnCanBeEvaluated(expression))
                        {
                            this.candidates.Add(expression);
                        }
                        else
                        {
                            this.cannotBeEvaluated = true;
                        }
                    }
                    this.cannotBeEvaluated |= saveCannotBeEvaluated;
                }
                return expression;
            }
        }
    }
    internal static class TypeSystem
    {
        internal static Type GetElementType(Type seqType)
        {
            Type ienum = FindIEnumerable(seqType);
            if (ienum == null) return seqType;
            return ienum.GetGenericArguments()[0];
        }

        private static Type FindIEnumerable(Type seqType)
        {
            if (seqType == null || seqType == typeof(string))
                return null;

            if (seqType.IsArray)
                return typeof(IEnumerable<>).MakeGenericType(seqType.GetElementType());

            if (seqType.GetTypeInfo().IsGenericType)
            {
                foreach (Type arg in seqType.GetGenericArguments())
                {
                    Type ienum = typeof(IEnumerable<>).MakeGenericType(arg);
                    if (ienum.IsAssignableFrom(seqType))
                    {
                        return ienum;
                    }
                }
            }

            Type[] ifaces = seqType.GetInterfaces();
            if (ifaces != null && ifaces.Length > 0)
            {
                foreach (Type iface in ifaces)
                {
                    Type ienum = FindIEnumerable(iface);
                    if (ienum != null) return ienum;
                }
            }

            if (seqType.GetTypeInfo().BaseType != null && seqType.GetTypeInfo().BaseType != typeof(object))
            {
                return FindIEnumerable(seqType.GetTypeInfo().BaseType);
            }

            return null;
        }

    }
    internal class ExpressionTreeHelpers
    {
        internal static bool IsMemberEqualsValueExpression(Expression exp, Type declaringType, string memberName)
        {
            if (exp.NodeType != ExpressionType.Equal)
                return false;

            BinaryExpression be = (BinaryExpression)exp;

            // Assert. 
            if (ExpressionTreeHelpers.IsSpecificMemberExpression(be.Left, declaringType, memberName) &&
                ExpressionTreeHelpers.IsSpecificMemberExpression(be.Right, declaringType, memberName))
                throw new Exception("Cannot have 'member' == 'member' in an expression!");

            return (ExpressionTreeHelpers.IsSpecificMemberExpression(be.Left, declaringType, memberName) ||
                ExpressionTreeHelpers.IsSpecificMemberExpression(be.Right, declaringType, memberName));
        }

        internal static bool IsSpecificMemberExpression(Expression exp, Type declaringType, string memberName)
        {
            return ((exp is MemberExpression) &&
                (((MemberExpression)exp).Member.DeclaringType == declaringType) &&
                (((MemberExpression)exp).Member.Name == memberName));
        }

        internal static T GetValueFromEqualsExpression<T>(BinaryExpression be, Type memberDeclaringType, string memberName)
        {
            if (be.NodeType != ExpressionType.Equal)
                throw new Exception("There is a bug in this program.");

            if (be.Left.NodeType == ExpressionType.MemberAccess)
            {
                MemberExpression me = (MemberExpression)be.Left;

                if (me.Member.DeclaringType == memberDeclaringType && me.Member.Name == memberName)
                {
                    return GetValueFromExpression<T>(be.Right);
                }
            }
            else if (be.Right.NodeType == ExpressionType.MemberAccess)
            {
                MemberExpression me = (MemberExpression)be.Right;

                if (me.Member.DeclaringType == memberDeclaringType && me.Member.Name == memberName)
                {
                    return GetValueFromExpression<T>(be.Left);
                }
            }

            // We should have returned by now. 
            throw new Exception("There is a bug in this program.");
        }

        internal static T GetValueFromExpression<T>(Expression expression)
        {
            if (expression.NodeType == ExpressionType.Constant)
                return (T)(((ConstantExpression)expression).Value);
            else
                throw new Exception(
                    String.Format("The expression type {0} is not supported to obtain a value.", expression.NodeType)); //TODO
        }
    }

}
