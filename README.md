# IP_Address
Aim: "Write a function in C# that takes an IP address and returns a two-character country code depending on the geo-location of the IP".
<br>
This repository uses this API: https://ipgeolocation.io/documentation/ip-geolocation-api-c-sharp-dotnet-sdk.html
<br>
There are two ways that the code can be used:
<br>
- Method that does not use the API's in-built method (but does use the API)
    - This is achieved by using two (edited) classes that are often used for programming to do with countries of the World (CountryList and CountryInfo), and using methods within the System namespace.
- Method that uses the API's in-built method
  - Calls the method GetCountryCode2() that the API provides.
  
The program can also either be used by parsing a string as an argument programmatically or by using the user's IP address via an in-built method.
