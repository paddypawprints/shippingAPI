/*
Copyright 2016 Pitney Bowes Inc.

Licensed under the MIT License(the "License"); you may not use this file except in compliance with the License.  
You may obtain a copy of the License in the README file or at
   https://opensource.org/licenses/MIT 
Unless required by applicable law or agreed to in writing, software distributed under the License is distributed 
on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.  See the License 
for the specific language governing permissions and limitations under the License.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO 
THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS 
OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, 
TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

*/

namespace PitneyBowes.Developer.ShippingApi
{
    /// <summary>
    /// Status of an address after a call to the Shipping API address validation method.
    /// </summary>
    public enum AddressStatus
    {
        /// <summary>
        /// The address was corrected by the system.
        /// </summary>
        VALIDATED_CHANGED,
        /// <summary>
        /// The address was validates the system.
        /// </summary>
        VALIDATED_AND_NOT_CHANGED,
        /// <summary>
        /// The address has not been validated, or could not be validated.
        /// </summary>
        NOT_CHANGED
    }

    /// <summary>
    /// Shipping carriers.
    /// </summary>
    public enum Carrier
    {
        /// <summary>
        /// US Postal Service
        /// </summary>
        USPS,
        /// <summary>
        /// Pitney Powes Presort Services
        /// </summary>
        PBPRESORT
    }

    /// <summary>
    /// Party cancelling a shipment.
    /// </summary>
    public enum CancelInitiator
    {
        SHIPPER
    }

    /// <summary>
    /// Credit card type.
    /// </summary>
    public enum CreditCardType
    {
        Amex,
        MC,
        Visa,
        Disc
    }

    /// <summary>
    /// Content type of a  document - URL or base64.
    /// </summary>
    public enum ContentType
    {
        /// <summary>
        /// A URL will be returned and the label can be retrieved later via an http GET call.
        /// </summary>
        URL,
        /// <summary>
        /// The label is embedded in a field in the returned json document. It is base64 encoded.
        /// </summary>
        BASE64
    }

    /// <summary>
    /// Document type - shipping label or manifest.
    /// </summary>
    public enum DocumentType
    {
        SHIPPING_LABEL,
        MANIFEST
    }
    /// <summary>
    /// Document file format - PDF, bitmap, Zebra etc.
    /// </summary>
    public enum FileFormat
    {
        PDF,
        PNG,
        /// <summary>
        /// Zebra thermal printer language.
        /// </summary>
        ZPL2
    }

    /// <summary>
    /// Manifest parameter.
    /// </summary>
    public enum ManifestParameter
    {
        SHIPPER_ID,
        MANIFEST_TYPE
    }

    /// <summary>
    /// Merchant status.
    /// </summary>
    public enum MerchantStatus
    {
        ACTIVE,
        INACTIVE,
        DELETED
    }

    /// <summary>
    /// Non delivery option for tracking.
    /// </summary>
    public enum NonDeliveryOption
    {
        @return,
        abandon,
        redirect
    }

    /// <summary>
    /// Package identifier type.
    /// </summary>
    public enum PackageIdentifierType
    {
        TrackingNumber
    }

    /// <summary>
    /// Package location for pickup.
    /// </summary>
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

    /// <summary>
    /// Package type indicator - cubic etc.
    /// </summary>
    public enum PackageTypeIndicator
    {
        Cubic,
        NonCubic
    }

    /// <summary>
    /// Payment type.
    /// </summary>
    public enum PaymentType
    {
        POSTAGE_AND_SUBSCRIPTION,
        SUBSCRIPTION,
        POSTAGE
    }

    /// <summary>
    /// Parcel type - letter, package, flat rate box etc.
    /// </summary>
    public enum ParcelType
    {
        LETTER,
        /// <summary>
        /// Flat rate envelope.
        /// </summary>
        FRE,
        /// <summary>
        /// Large envelope.
        /// </summary>
        LGENV,
        /// <summary>
        /// Legal flat rate envelope.
        /// </summary>
        LGLFRENV,
        /// <summary>
        /// Padded flat rate envelope.
        /// </summary>
        PFRENV,
        /// <summary>
        /// Small flat rate box
        /// </summary>
        SFRB,
        /// <summary>
        /// Medium flat rate box.
        /// </summary>
        FRB,
        /// <summary>
        /// Large Flat rate box.
        /// </summary>
        LFRB,
        /// <summary>
        /// DVD box.
        /// </summary>
        DVDBOX,
        /// <summary>
        /// Video box.
        /// </summary>
        VIDEOBOX,
        /// <summary>
        /// Military flat raqte box.
        /// </summary>
        MLFRB,
        /// <summary>
        /// Regional rate box, type A
        /// </summary>
        RBA,
        /// <summary>
        /// Regional rate box, type B.
        /// </summary>
        RBB,
        /// <summary>
        /// Package (not eligible for special package rate).
        /// </summary>
        PKG,
        LP,
        FLAT,
        EMMTB,
        FTB,
        HTB,
        SACK,
        FTTB,
        SOFTPACK,
        /// <summary>
        /// PMOD Enclosed Package Type
        /// </summary>
        MIX
    }

    /// <summary>
    /// Payment method.
    /// </summary>
    public enum PaymentMethod
    {
        CC,
        PAYPAL,
        PurchasePower
    }

    /// <summary>
    /// Services for parcels for pickup
    /// </summary>
    public enum PickupService
    {
        /// <summary>
        /// First-Class Mail
        /// </summary>
        FCM,
        /// <summary>
        /// Priority Mail
        /// </summary>
        PM,
        /// <summary>
        /// Priority Mail Express
        /// </summary>
        EM,
        /// <summary>
        /// Parcel Select
        /// </summary>
        PRCLSEL,
        /// <summary>
        /// Use INT for all international services:
        ///    First-Class Mail International,
        ///    First-Class Package International Service,
        ///    Priority Mail Express International, and
        ///    Priority Mail International
        /// </summary>
        INT,
        /// <summary>
        /// Other Packages
        /// </summary>
        OTH
    }

    /// <summary>
    /// PDF documents only.This defines an option to embed script that can render an interactive print dialog box for the end user within the shipment document/label.
    /// </summary>
    public enum PrintDialogOption 
    {

        NO_PRINT_DIALOG,
        EMBED_PRINT_DIALOG
    }

    /// <summary>
    /// Refund status.
    /// </summary>
    public enum RefundStatus
    {
        INITIATED
    }

    /// <summary>
    /// Reason for export.
    /// </summary>
    public enum ReasonForExport
    {
        GIFT,
        COMMERCIAL_SAMPLE,
        MERCHANDISE,
        DOCUMENTS,
        RETURNED_GOODS,
        OTHER
    }

    /// <summary>
    /// Scan based return package status.
    /// </summary>
    public enum SBRPrintStatus
    {
        SBRPrinted,
        SBRCharged,
        NULL
    }

    /// <summary>
    /// Document Size.
    /// </summary>
    public enum Size
    {
        DOC_4X6,
        DOC_8X11
    }

    /// <summary>
    /// Carrier Services.
    /// </summary>
    public enum Services
    {
        /// <summary>
        /// USPS First Class Mail
        /// </summary>
        FCM,
        /// <summary>
        /// Priority Mail
        /// </summary>
        PM,
        /// <summary>
        /// Priority Mail Express 
        /// </summary>
        EM,
        /// <summary>
        /// Standard Post
        /// </summary>
        STDPOST,
        /// <summary>
        /// Parcel Select
        /// </summary>
        PRCLSEL,
        /// <summary>
        /// Media Mail
        /// </summary>
        MEDIA,
        /// <summary>
        /// Library Mail
        /// </summary>
        LIB,
        /// <summary>
        /// First Class International
        /// </summary>
        FCMI,
        /// <summary>
        /// First Class Package International Service
        /// </summary>
        FCPIS,
        /// <summary>
        /// Priority Mail Express International
        /// </summary>
        EMI,
        /// <summary>
        /// Priority Mail International
        /// </summary>
        PMI,
        /// <summary>
        /// PMOD: Marketing Mail
        /// </summary>
        MKTMAIL,
        /// <summary>
        /// PMOD: Periodicals
        /// </summary>
        PER,
        /// <summary>
        /// PMOD: Only very specific combinations are allowed. The shipper is responsible for following USPS rules. PB does not provide validation.
        /// </summary>
        MIX,
        /// <summary>
        /// Priority Mail Open and Distribute
        /// </summary>
        PMOD

    }

    /// <summary>
    /// Shipment options.
    /// </summary>
    public enum ShipmentOption
    {
        /// <summary>
        /// Set the value to true in order to hide the carrier shipping charge on the label.
        /// </summary>
        HIDE_TOTAL_CARRIER_CHARGE,
        /// <summary>
        /// When set to true, only City, State, and PostalCode (zip) are validated for fromAddress and toAddress.
        /// Note: Applicable only to domestic address.
        /// </summary>
        MINIMAL_ADDRESS_VALIDATION,
        /// <summary>
        /// The Shipper ID of the merchant on whose behalf the label is being printed. 
        /// The merchantís Shipper ID is found in the postalReportingNumber field in the merchant object.
        /// </summary>
        SHIPPER_ID,
        /// <summary>
        /// Use this field for instructions in case the package is not delivered. Valid values are return, abandon, and redirect.
        /// Note: This applies to international labels ONLY.
        /// </summary>
        NON_DELIVERY_OPTION,
        /// <summary>
        /// This is a user-specified message that gets printed on the face of the label. 
        /// A string of up to 50 characters can be printed on the label.
        /// Note: This applies to domestic labels ONLY
        /// </summary>
        PRINT_CUSTOM_MESSAGE_1,
        /// <summary>
        /// This is a user-specified message that gets printed on the bottom of the label. 
        /// A string of up to 50 characters can be printed on the label.
        /// Note: This applies to domestic labels ONLY
        /// </summary>
        PRINT_CUSTOM_MESSAGE_2,
        /// <summary>
        /// Set this value to true in order to make this shipment eligible to be included in the end-of-day manifest.
        /// </summary>
        ADD_TO_MANIFEST,
        /// <summary>
        /// This field can be used if the shipment label is to be tendered at a future date. 
        /// The format of the field is YYYY-MM-DD or YYYY-MM-DD HH:MM:SS where the time format is in UTC.
        /// Note: Shipment date can be advanced only up to 7 days of the shipment print date.
        /// </summary>
        FUTURE_SHIPMENT_DATE,
        /// <summary>
        /// Adds the senderís signature and the date on CN22 and CP72 shipping labels.
        /// Enter the signature as a string. The senderís signature date is automatically populated.
        /// </summary>
        SHIPPING_LABEL_SENDER_SIGNATURE,
        SHIPPING_LABEL_RECEIPT,
        SHIPPER_BASE_CHARGE,
        SHIPPER_TOTAL_CHARGE,
        ORIGIN_ENTRY_FACILITY,
        DESTINATION_ENTRY_FACILITY,
        ENCLOSED_MAIL_CLASS,
        ENCLOSED_PARCEL_TYPE,
        ENCLOSED_PAYMENT_METHOD
    }

    /// <summary>
    /// Shipment type - outbound or return.
    /// </summary>
    public enum ShipmentType
    {
        OUTBOUND,
        RETURN
    }

    /// <summary>
    /// Service trackability.
    /// </summary>
    public enum Trackable
    {
        TRACKABLE,
        NON_TRACKABLE,
        REQUIRES_TRACKABLE_SPECIAL_SERVICE
    }

    /// <summary>
    /// Carrier special servicves - signature etc.
    /// </summary>
    public enum SpecialServiceCodes
    {
        /// <summary>
        /// Insured Mail.
        /// </summary>
        Ins,
        /// <summary>
        /// Return receipt.
        /// </summary>
        RR,
        /// <summary>
        /// Signature confirmation.
        /// </summary>
        Sig,
        /// <summary>
        /// Adult signature required.
        /// </summary>
        ADSIG,
        /// <summary>
        /// Certified mail.
        /// </summary>
        Cert,
        /// <summary>
        /// Delivery confirmation.
        /// </summary>
        DelCon,
        /// <summary>
        /// Electronic return receipt.
        /// </summary>
        ERR,
        /// <summary>
        /// Return receipt for merchandise
        /// </summary>
        RRM,
        /// <summary>
        /// Registered mail.
        /// </summary>
        Reg,
        /// <summary>
        /// Registered mail with insurance.
        /// </summary>
        RegIns,
        /// <summary>
        /// Special handling - fragile.
        /// </summary>
        SH,
        /// <summary>
        /// Certified mail with restricted delivery.
        /// </summary>
        CertRD,
        /// <summary>
        /// Collect on delivery.
        /// </summary>
        COD,
        /// <summary>
        /// Collect on delivery with restricted delivery.
        /// </summary>
        CODRD,
        /// <summary>
        /// Insurance with restricted delivery.
        /// </summary>
        InsRD,
        /// <summary>
        /// Registered mail with restricted delivery.
        /// </summary>
        RegRD,
        /// <summary>
        /// Registered mail with COD.
        /// </summary>
        RegCOD,
        /// <summary>
        /// Signature required with restricted delivery.
        /// </summary>
        SigRD,
        /// <summary>
        /// Adult signature required with restricted delivery.
        /// </summary>
        ADSIGRD,
        /// <summary>
        /// Registered mail with insurance and restricted delivery.
        /// </summary>
        RegInsRD,
        /// <summary>
        /// Certified mail with adult signature.
        /// </summary>
        CERTAD,
        /// <summary>
        /// Certified mail with adult signature and restricted delivery.
        /// </summary>
        CERTADRD,
        /// <summary>
        /// Hazardous materials.
        /// </summary>
        hazmat,
        /// <summary>
        /// Live animal surcharge.
        /// </summary>
        liveanimal,
        /// <summary>
        /// Live Animal-Day Old Poultry Surcharge
        /// </summary>
        liveanimal_poultry,
        /// <summary>
        /// Holiday Delivery- For Priority Mail Express Service Only
        /// </summary>
        holiday,
        /// <summary>
        /// Sunday delivery.
        /// </summary>
        sunday,
        /// <summary>
        /// Sunday and holidaqy delivery.
        /// </summary>
        sunday_holiday,
        PO_to_Addressee,
        
        noWeekend,

        /// <summary>
        /// Delivery by 10:30 AM
        /// </summary>
        TenThirty,

        PMOD_OPTIONS
    }
    /// <summary>
    /// Transaction type in the tracking report.
    /// </summary>
    public enum TransactionType
    {
        POSTAGE_PRINT,
        POSTAGE_FUND,
        POSTAQGE_REFUND
    }

    /// <summary>
    /// Unit of dimension.
    /// </summary>
    public enum UnitOfDimension
    {
        CM,
        IN
    }
    /// <summary>
    /// Unit of weight.
    /// </summary>
    public enum UnitOfWeight
    {
        GM,
        OZ
    }

    /// <summary>
    /// Tracking status code - in transit. delivered etc.
    /// </summary>
    public enum TrackingStatusCode
    {
        InTransit,
        Delivered,
        Manifest
    }
}