
# Pitney Bowes Shipping API C# Client

This project has the code for a nuget package that provides a fluent C# interface to the Pitney Bowes Shipping API.

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

Add your own IDs.
```csharp
//*****************************************
// Replace these with your own values
//
AddConfigItem("ApiKey", "Ci4vEAgBP8Aww7TXyGOKhr43uKTPNyfO");
DefaultSession.GetAPISecret = () => "wgXYtZkNbP0iV8h0".ToCharArray();
AddConfigItem("RatePlan", "PP_SRP_NEWBLUE");
AddConfigItem("ShipperID", "9014888499");
AddConfigItem("DeveloperID", "46841999");
//******************************************
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


### Installing

There are solutions for dotnet core and .net 4.5. (shippingapi.core.sln, and shippingapi.net45.sln). Open the solution in Visual Studio or VSCode and build. 
The solution has a number of projects:
* **shippingapi.core** - shipping API nuget package code
* **example** - a console app that gives an example of using the api

Open the Program.cs file in the example project and substitute you API key, API secret, developer ID, and shipper ID for the values in the code (init method). 

```
Give the example
```

And repeat

```
until finished
```

End with an example of getting some data out of the system or using it for a little demo

## Running the tests

Explain how to run the automated tests for this system

### Break down into end to end tests

Explain what these tests test and why

```
Give an example
```

### And coding style tests

Explain what these tests test and why

```
Give an example
```

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