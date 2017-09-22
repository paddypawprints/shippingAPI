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

    public class MerchantsReportRequest : ShippingApiRequest, IReportRequest
    {
        [ShippingApiResource("developers", true, PathSuffix = "/merchants")]
        public string DeveloperId { get; set; }
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
        public override string ContentType { get => "application/json"; }
    

        public MerchantsReportRequest()
        {
            AcceptLanguage = CultureInfo.CurrentCulture.Name;
        }

        public bool Validate() => true;

    }

    public class MerchantsPageResponse
    {
        [JsonProperty("content")]
        public IEnumerable<Merchant> Content { get; set; }
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

    public class MerchantsReport<T> : ReportBase<T>
    {

        public string DeveloperId { get; set; }
        private Session _session;

        public MerchantsReport( string developerId, Session session = null) : base()
        {
            Provider = new MerchantsReportProvider(developerId, session);
            DeveloperId = developerId;
            _session = session;
        }

        public MerchantsReport(  MerchantsReportProvider provider, Expression expression, string developerId, Session session = null) : base(provider,expression)
        {
            DeveloperId = developerId;
            _session = session;
        }

        public async static Task<ShippingApiResponse<MerchantsPageResponse>> MerchantsPage(MerchantsReportRequest request, Session session = null)
        {
            if (session == null) session = SessionDefaults.DefaultSession;
            request.Authorization = new StringBuilder(session.AuthToken.AccessToken);
            return await WebMethod.Get<MerchantsPageResponse, MerchantsReportRequest>("/shippingservices/v2/ledger", request, session);
        }

        public static IEnumerable<Merchant> Report(MerchantsReportRequest request, Func<Merchant, bool> filter = null, Session session = null)
        {
            if (session == null) session = SessionDefaults.DefaultSession;
            request.Page = 0;
            MerchantsPageResponse page;
            do
            {
                var response = MerchantsPage(request, session).GetAwaiter().GetResult();
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

    public class MerchantsReportProvider : ReportProviderBase, IQueryProvider
    {
        private string _developerId;
        internal Session _session;

        public MerchantsReportProvider( string developerId, Session session) :base()
        {
            _developerId = developerId;
            _session = session;
        }

        // Queryable's collection-returning standard query operators call this method. 
        public override IQueryable<TResult> CreateQuery<TResult>(Expression expression) 
        {
            return new MerchantsReport<TResult>(this, expression, _developerId);
        }

        public override object Execute<TResult>(Expression expression, bool isEnumerable)
        {
            return Execute<TResult, Merchant, MerchantsReportRequest, MerchantsReportRequestFinder>(
                expression,
                isEnumerable,
                x => MerchantsReport<Merchant>.Report( x, null, _session),
                x => x.DeveloperId = _developerId
            );
        }
    }

    internal class MerchantsReportRequestFinder : RequestFinderVisitor<MerchantsReportRequest, Merchant>
    {
        protected override Expression VisitBinary(BinaryExpression be)
        {
            return base.VisitBinary(be);
        }
    }
}
