
# Pitney Bowes Shipping API C# Client

This project has the code for a nuget package that provides a fluent C# interface to the Pitney Bowes Shipping API.
The package uses the [.NET Standard 1.3 framework](https://docs.microsoft.com/en-us/dotnet/standard/net-standard), meaning 
it is compatible with:
* .NET core 1.0 and 2.0
* .NET framework 4.6 and above
Mono and Xamarin are also supported.

Features:
* **full wrapping of the API**. Hides (encapsulates) all protocol details, including authentication and report pagination..
* **strong typing** � objects for all entities. Enums for all options � which really helps in visual studio intellisense.
* **Contract via interfaces** as well as DTOs to reduce the need to copy data
* **Linq provider** for reports
* **Fluent interface** - less typing and really good way to extend with extension methods
* **Support for the metadata** provided by the carrier rules method. Use this for local validation/UI options/rate shop.
* **Mocking and recording** of live messages to disk for capture or later playback in mock mode. Mocking for unit and regression testing.
* **Example** console app.
* **Plug in your own configuration and log providers**
* **Keeps the API secret out of cleartext**

## Getting Started
If you just want to use the solution without building it is hosted [on nuget.org](https://www.nuget.org/packages/shippingapi/).
To generate shipping labels you will need to sign up for a Pitney Bowes shipping API account. It does not cost anything. Once you sign up you will have access to a fully functional sandbox environmernt. Sign up for the shipping API account here [Shipping API Signup](https://signup.pitneybowes.com/signup/shipping)

To use the API you will need the following account information:
* Your API key
* API secret
* Developer ID
* Shipper ID

There are instructions on how to get hold of these on this page [Getting Started](https://shipping.pitneybowes.com/getting-started.html). 
If you are developing on Windows I'd recommend that you install [Telerik Fiddler](http://www.telerik.com/fiddler), which will let you see the messages with the Pitney Bowes servers.

After you have signed up, to build an example app:

In VSCode/dotnet core
```bash
$ mkdir myshippingprojdir
$ cd myshippingprojdir
$ dotnet new console
$ dotnet add package ShippingAPI
```

In Visual Studio create a new console app, and then in the package manager console
```
PM> install-package ShippingAPI
```
Get the example program from github and replace your Program.cs file with [this one](https://github.com/paddypawprints/shippingAPI/blob/master/example/Program.cs).

Add your own IDs. Replace the values in the code below or create a shippingapisettings.json file in %APPDATA%
```csharp
            var configs = new Dictionary<string, string>
            {
                { "ApiKey", "YOUR_API_KEY" },
                { "ApiSecret", "YOUR_API_SECRET" },
                { "RatePlan", "YOUR_RATE_PLAN" },
                { "ShipperID", "YOUR_SHIPPER_ID" },
                { "DeveloperID", "YOUR_DEVELOPER_ID" }
            };
```
shippingapisettings.json
```json
{ 
    "ApiKey": "!###",
    "ApiSecret": "###",
    "RatePlan": "PP_SRP_NEWBLUE",
    "ShipperID": "1234567890",
    "DeveloperID": "1234567890" 
}
```


To create a shipping label:
```csharp
            // Create shipment
            var shipment = ShipmentFluent<Shipment>.Create()
                .ToAddress((Address)AddressFluent<Address>.Create()
                    .Company("ABC Company")
                    .Person("Rufous Sirius Canid", "323 555-1212", "rs.canid@gmail.com")
                    .Residential(false)
                    .AddressLines("643 Greenway RD")
                    .CityTown("Boone")
                    .StateProvince("NC")
                    .PostalCode("28607")
                    .CountryCode("US")
                    .Verify() // calls the service for address validation
                    )
                .MinimalAddressValidation("true")
                .ShipperRatePlan(Globals.DefaultSession.GetConfigItem("RatePlan"))
                .FromAddress((Address)AddressFluent<Address>.Create()
                    .Company("Pitney Bowes Inc.")
                    .AddressLines("27 Waterview Drive")
                    .Residential(false)
                    .CityTown("Shelton")
                    .StateProvince("CT")
                    .PostalCode("06484")
                    .CountryCode("US")
                    .Person("Paul Wright", "203-555-1213", "john.publica@pb.com")
                    )
                .Parcel((Parcel)ParcelFluent<Parcel>.Create()
                    .Dimension(12, 12, 10)
                    .Weight(16m, UnitOfWeight.OZ)
                    )
                .Rates(RatesArrayFluent<Rates>.Create()
                    .USPSPriority<Rates, Parameter>()
                    .InductionPostalCode("06484")
                    )
                .Documents(DocumentsArrayFluent<Document>.Create()
                    .ShippingLabel()
                    )
                .ShipmentOptions(ShipmentOptionsArrayFluent<ShipmentOptions>.Create()
                    .ShipperId(sandbox.GetConfigItem("ShipperID"))
                    .AddToManifest()
                    )
                .TransactionId(Guid.NewGuid().ToString().Substring(15));

            var label = ShipmentsMethods.CreateShipment((Shipment)shipment).GetAwaiter().GetResult();

            if (label.Success)
            {
```
### Prerequisites

If you want to build the system out of the box you will need Visual Studio 2017 Community Edition is fine. At one point it 
did build on MacOS but I moved to Windows due to the complexity of dependency management in VSCode.

### Building the system

In Visual studio developer command prompt. Git command line and dotnet core 2.0 have been previously installed.
 
 ```
C:\Development>mkdir apitest
 
C:\Development>cd apitest
 
C:\Development\apitest>git clone https://github.com/paddypawprints/shippingAPI.git
Cloning into 'shippingAPI'...
remote: Counting objects: 1558, done.
remote: Compressing objects: 100% (264/264), done.
Receiving objects: 100% (1558/1558), 651.92 KiB | 9.88 MiB/s, done.d 888
Resolving deltas: 100% (1028/1028), done.
 
C:\Development\apitest\shippingAPI> cd shippingAPI
 
C:\Development\apitest\shippingAPI > dotnet restore
 
C:\Development\apitest\shippingAPI> dotnet build
 
C:\Development\apitest\shippingAPI>  dotnet publish
 
C:\Development\apitest\shippingAPI> cd examples\example.core\bin\Debug\netcoreapp2.0\publish
 
C:\Development\apitest\examples\example.core\bin\Debug\netcoreapp2.0\publish > dotnet example.dll
9405509898642004103722
Document written to C:\Users\patrick\AppData\Local\Temp\2\USPS2200080642743578.PDF
Document written to C:\Users\patrick\AppData\Local\Temp\2\9475709899581000234042.PDF
```

## Running the tests

The tests use [xunit](https://xunit.github.io/). The API can run in either live or mocked mode. When it is in mocked mode the response to API requests are 
read from a file. The file is identified by certain attributes of the response - transaction ID in the case of a shipment, so different test
scenarios can be set up using the mocked interface. 

There are not many tests, as yet. I had trouble running the playback in the debugger, where Visual Studio would occaisionally hang when reading from the response file. Not sure why yet. Consequently, most of the testing has been done with the example progam.


## Built With

* [NewtonSoft.JSON](http://www.dropwizard.io/1.0.2/docs/) - Awesome serialization

## Contributing

Please read [CONTRIBUTING.md](https://gist.github.com/PurpleBooth/b24679402957c63ec426) for details on our code of conduct, and the process for submitting pull requests to us.

## Versioning

We use [SemVer](http://semver.org/) for versioning. For the versions available, see the [tags on this repository](https://github.com/your/project/tags). 

## Authors

* **Patrick Farry** - *Initial work* - [Pitney Bowes]

See also the list of [contributors](https://github.com/your/project/contributors) who participated in this project.

## License

Copyright 2016 Pitney Bowes Inc.
Licensed under the MIT License (the "License"); you may not use this file except in compliance with the License.  You may obtain a copy of the License in the README file or at
    https://opensource.org/licenses/MIT 
Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.  See the License for the specific language governing permissions and limitations under the License.
 - see the [LICENSE.md](LICENSE.md) file for details

## Acknowledgments

* NewtonSoft.JSON