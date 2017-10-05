using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Globalization;
using PitneyBowes.Developer.ShippingApi.Model;

namespace PitneyBowes.Developer.ShippingApi
{

    public class ReportRequest : ShippingApiRequest, IReportRequest
    {
        public override string RecordingSuffix => DeveloperId+"-Page"+Page;
        [ShippingApiResource("developers", true, PathSuffix ="/transactions/reports")]
        public string DeveloperId { get; set; }
        [ShippingApiQuery("fromDate",Format = "{0:yyyy-MM-ddTHH:mm:ssZ}")]
        public DateTimeOffset FromDate { get; set; }
        [ShippingApiQuery("toDate",Format = "{0:yyyy-MM-ddTHH:mm:ssZ}")]
        public DateTimeOffset ToDate { get; set; }
        [ShippingApiQuery("transactionId", true)]
        public string TransactionId { get; set; }
        [ShippingApiQuery("parcelTrackingNumber", true)]
        public string ParcelTrackingNumber { get; set; }
        [ShippingApiQuery("merchantId", true)]
        public string MerchantId { get; set; }
        [ShippingApiQuery("transactionType", true)]
        public TransactionType? TransactionType { get; set; }
        [ShippingApiQuery("size", true)]
        public int? PageSize { get; set; }
        [ShippingApiQuery("page", true)]
        public int Page { get; set; }
        [ShippingApiQuery("sort", true)]
        public string Sort { get; set; }
        [ShippingApiHeaderAttribute("Bearer")]
        public override StringBuilder Authorization { get; set; }
        [ShippingApiHeaderAttribute("Accept-Language")]
        public string AcceptLanguage { get; set; }
        public override string ContentType { get => "application/json";}

        public ReportRequest()
        {
            AcceptLanguage = CultureInfo.CurrentCulture.Name;
        }

        public bool Validate()
        {
            return ToDate != null && FromDate != null;
        }
    }

    public class TransactionPageResponse
    {
        [JsonProperty("content")]
        public IEnumerable<Transaction> Content { get; set; }
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
        public IEnumerable<ITransactionSort> Sort { get; set; }
        [JsonProperty("numberOfElements")]
        public int Count { get; set; }
        [JsonProperty("first")]
        public bool FirstPage { get; set; }
    }

    public class TransactionsReport<T> : ReportBase<T>
    {

        public string DeveloperId { get; set; }
        private ISession _session;

        public TransactionsReport( string developerId, ISession session = null) : base()
        {
            Provider = new TransactionsReportProvider(developerId, session);
            DeveloperId = developerId;
            _session = session;
        }

        public TransactionsReport( TransactionsReportProvider provider, Expression expression, string developerId, ISession session = null) : base(provider,expression)
        {
            DeveloperId = developerId;
            _session = session;
        }

        public async static Task<ShippingApiResponse<TransactionPageResponse>> TransactionPage(ReportRequest request, ISession session = null)
        {
            if (session == null) session = Globals.DefaultSession;
            request.Authorization = new StringBuilder(session.AuthToken.AccessToken);
            return await WebMethod.Get<TransactionPageResponse, ReportRequest>("/shippingservices/v2/ledger", request, session);
        }

        public static IEnumerable<Transaction> Report(ReportRequest request, Func<Transaction, bool> filter = null, ISession session = null)
        {
            if (session == null) session = Globals.DefaultSession;
            request.Page = 0;
            TransactionPageResponse page;
            do
            {
                var response = TransactionPage(request, session).GetAwaiter().GetResult();
                if (!response.Success) break;
                page = response.APIResponse;
                request.Page += 1;
                foreach (var t in page.Content)
                {
                    if ( filter == null || (filter != null && filter(t)) ) yield return t;
                }
            } while (!page.LastPage);
        }
    }

    public class TransactionsReportProvider : ReportProviderBase, IQueryProvider
    {
        private string _developerId;
        internal ISession _session;

        public TransactionsReportProvider( string developerId, ISession session) :base()
        {
            _developerId = developerId;
            _session = session;
        }

        // Queryable's collection-returning standard query operators call this method. 
        public override IQueryable<TResult> CreateQuery<TResult>(Expression expression) 
        {
            return new TransactionsReport<TResult>(this, expression, _developerId);
        }

        public override object Execute<TResult>(Expression expression, bool isEnumerable)
        {
            return Execute<TResult, Transaction, ReportRequest, ReportRequestFinder>(
                expression,
                isEnumerable,
                x => TransactionsReport<Transaction>.Report( x, null, _session),
                x => x.DeveloperId = _developerId
            );
        }
    }

    internal class ReportRequestFinder : RequestFinderVisitor<ReportRequest, Transaction>
    {
        protected override Expression VisitBinary(BinaryExpression be)
        {
            Expression exp;
            if(( exp = AssignExpressionValue<string>(be, ExpressionType.Equal, "MerchantId", (x, y) => x.MerchantId = y))!=null) return exp;
            if ((exp = AssignExpressionValue<TransactionType>(be, ExpressionType.Equal, "TransactionType", (x, y) => x.TransactionType = y))!=null)return exp;
            if ((exp = AssignExpressionValue<DateTimeOffset>(be, ExpressionType.GreaterThanOrEqual, "TransactionDateTime", (x, y) => x.FromDate = y))!=null) return exp;
            if ((exp = AssignExpressionValue<DateTimeOffset>(be, ExpressionType.LessThanOrEqual, "TransactionDateTime", (x, y) => x.ToDate = y))!=null) return exp;
            return base.VisitBinary(be);
        }
    }
}
