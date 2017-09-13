﻿using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Globalization;

namespace PitneyBowes.Developer.ShippingApi
{

    public class Transaction : ITransaction
    {
        public string TransactionId { get; set; }
        public DateTimeOffset TransactionDateTime { get; set; }
        public TransactionType TransactionType { get; set; }
        public string DeveloperName { get; set; }
        public string DeveloperId { get; set; }
        public string DeveloperPostagePaymentMethod { get; set; }
        public string DeveloperRatePlan { get; set; }
        public Decimal? DeveloperRateAmount { get; set; }
        public Decimal? DeveloperPostagePaymentAccountBalance { get; set; }
        public string MerchantName { get; set; }
        public string MerchantId { get; set; }
        public string MerchantPostageAccountPaymentMethod { get; set; }
        public string MerchantRatePlan { get; set; }
        public Decimal? MerchantRate { get; set; }
        public Decimal? ShipperPostagePaymentAccountBalance { get; set; }
        public Decimal? LabelFee { get; set; }
        public string ParcelTrackingNumber { get; set; }
        public Decimal? WeightInOunces { get; set; }
        public int? Zone { get; set; }
        public Decimal? PackageLengthInInches { get; set; }
        public Decimal? PackageWidthInInches { get; set; }
        public Decimal? PackageHeightInInches { get; set; }
        public PackageTypeIndicator? PackageTypeIndicator { get; set; }
        public USPSParcelType? PackageType { get; set; }
        public string MailClass { get; set; }
        public string InternationalCountryPriceGroup { get; set; }
        public string OriginationAddress { get; set; }
        public string OriginZip { get; set; }
        public string DestinationAddress { get; set; }
        public string DestinationZip { get; set; }
        public string DestinationCountry { get; set; }
        public Decimal? PostageDepositAmount { get; set; }
        public Decimal? CreditCardFee { get; set; }
        public string RefundStatus { get; set; }
        public string RefundDenialReason { get; set; }
    }

    public class TransactionSort : ITransactionSort
    {

        public string Ascending { get; set; }
        public string Direction { get; set; }
        public string IgnoreCase { get; set; }
        public string NullHandling { get; set; }
        public string Property { get; set; }
    }

    public class ReportRequest : IShippingApiRequest, IReportRequest
    {
        [ShippingAPIResource("developers", true, PathSuffix ="/transactions/reports")]
        public string DeveloperId { get; set; }
        [ShippingAPIQuery("fromDate",Format = "{0:yyyy-MM-ddTHH:mm:ssZ}")]
        public DateTimeOffset FromDate { get; set; }
        [ShippingAPIQuery("toDate",Format = "{0:yyyy-MM-ddTHH:mm:ssZ}")]
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
        public int? PageSize { get; set; }
        [ShippingAPIQuery("page", true)]
        public int Page { get; set; }
        [ShippingAPIQuery("sort", true)]
        public string Sort { get; set; }
        [ShippingAPIHeader("Bearer")]
        public StringBuilder Authorization { get; set; }
        [ShippingAPIHeader("Accept-Language")]
        public string AcceptLanguage { get; set; }
        public string ContentType { get => "application/json"; set => throw new NotImplementedException(); }

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
        private ShippingApi.Session _session;

        public TransactionsReport( string developerId, ShippingApi.Session session = null) : base()
        {
            DeveloperId = developerId;
            _session = session;
        }

        public TransactionsReport( TransactionsReportProvider provider, Expression expression, string developerId, ShippingApi.Session session = null) : base(provider,expression)
        {
            DeveloperId = developerId;
            _session = session;
        }

        public async static Task<ShippingAPIResponse<TransactionPageResponse>> TransactionPage(ReportRequest request, ShippingApi.Session session = null)
        {
            if (session == null) session = ShippingApi.DefaultSession;
            request.Authorization = new StringBuilder(session.AuthToken.AccessToken);
            return await WebMethod.Get<TransactionPageResponse, ReportRequest>("/shippingservices/v2/ledger", request, session);
        }

        public static IEnumerable<Transaction> Report(ReportRequest request, Func<Transaction, bool> filter = null, ShippingApi.Session session = null)
        {
            if (session == null) session = ShippingApi.DefaultSession;
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
                    if ( filter != null && filter(t) ) yield return t;
                }
            } while (!page.LastPage);
        }
    }

    public class TransactionsReportProvider : ReportProviderBase, IQueryProvider
    {
        private string _developerId;
        internal ShippingApi.Session _session;

        public TransactionsReportProvider( string developerId, ShippingApi.Session session) :base()
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

    public class ReportRequestFinder : RequestFinderVisitor<ReportRequest, Transaction>
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
