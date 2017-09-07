
namespace PitneyBowes.Developer.ShippingApi
{
    public enum AddressStatus
    {
        VALIDATED_CHANGED,
        VALIDATED_AND_NOT_CHANGED,
        NOT_CHANGED
    }

    public enum Carrier
    {
        USPS,
        PBPS
    }
    public enum CreditCardType
    {
        Amex,
        MC,
        Visa,
        Disc
    }
    public enum ContentType
    {
        URL,
        BASE64
    }
    public enum DocumentType
    {
        SHIPPING_LABEL,
        MANIFEST
    }
    public enum FileFormat
    {
        PDF,
        PNG,
        ZPL2
    }

    public enum MerchantStatus
    {
        ACTIVE,
        INACTIVE,
        DELETED
    }
    public enum NonDeliveryOption
    {
        @return,
        abandon,
        redirect
    }
    public enum PackageLocation
    {
        FrontDoor,
        BackDoor,
        SideDoor,
        KnockonDoorRingBell,
        MailRoom,
        Office,
        Reception,
        InAtMailbox,
        Other
    }

    public enum PackageTypeIndicator
    {
        Cubic,
        NonCubic
    }

    public enum USPSParcelType
    {
        FRE,
        LGENV,
        LGLFRENV,
        PFRENV,
        SFRB,
        FRB,
        LFRB,
        DVDBOX,
        VIDEOBOX,
        MLFRB,
        RBA,
        RBB,
        PKG

    }
    public enum PaymentMethod
    {
        CC,
        PAYPAL,
        PurchasePower
    }
    public enum PaymentType
    {
        POSTAGE,
        POSTAGE_AND_SUBSCRIPTION,
        SUBSCRIPTION
    }
    public enum PrintDialogOption 
    {
        NO_PRINT_DIALOG,
        EMBED_PRINT_DIALOG
    }

    public enum RefundStatus
    {
        INITIATED
    }
    public enum ReasonForExport
    {
        GIFT,
        COMMERCIAL_SAMPLE,
        MERCHANDISE,
        DOCUMENTS,
        RETURNED_GOODS,
        OTHER
    }

    public enum SBRPrintStatus
    {
        SBRPrinted,
        SBRCharged,
        NULL
    }
    public enum Size
    {
        DOC_4X6,
        DOC_8X11
    }
    public enum USPSServices
    {
        FCM, //	First Class Mail
        PM,	// Priority Mail
        EM,	// Priority Mail Express
        STDPOST, // Standard Post
        PRCLSEL, // Parcel Select
        MEDIA, // Media Mail
        LIB, // Library Mail
        FCMI, // First Class International
        FCPIS, // First Class Package International Service
        EMI, // Priority Mail Express International
        PMI // Priority Mail International
    }
    public enum ShipmentOption
    {
        HIDE_TOTAL_CARRIER_CHARGE,
        MINIMAL_ADDRESS_VALIDATION,
        SHIPPER_ID,
        NON_DELIVERY_OPTION,
        PRINT_CUSTOM_MESSAGE_1,
        PRINT_CUSTOM_MESSAGE_2,
        ADD_TO_MANIFEST,
        FUTURE_SHIPMENT_DATE,
        SHIPPING_LABEL_SENDER_SIGNATURE,
        SHIPPING_LABEL_RECEIPT
    }

    public enum Trackable
    {
        TRACKABLE,
        NON_TRACKABLE,
        REQUIRES_TRACKABLE_SPECIAL_SERVICE
    }

    public enum USPSSpecialServiceCodes
    {
        Ins,
        RR,
        Sig,
        ADSIG,
        Cert,
        DelCon,
        ERR,
        RRM,
        Reg,
        RegIns,
        SH,
        CertRD,
        COD,
        CODRD,
        InsRD,
        RegRD,
        RegCOD,
        SigRD,
        ADSIGRD,
        RegInsRD,
        CERTAD,
        CERTADRD,
        hazmat,
        liveanimal,
        liveanimal_poultry, //TODO: liveanimal-poultry
        holiday,
        sunday,
        sunday_holiday //TODO: sunday-holiday

    }
    public enum TransactionType
    {
        POSTAGE_PRINT,
        POSTAGE_FUND,
        POSTAQGE_REFUND
    }

    public enum UnitOfDimension
    {
        CM,
        IN
    }
    public enum UnitOfWeight
    {
        GM,
        OZ
    }
    public enum TrackingStatusCode
    {
        InTransit,
        Delivered,
        Manifest
    }
}