Arduino System
==============

This is a .net opensource system, inspired on ThingSpeak's API, with some differences.

You could self-host it on your own server and take care of it yourself, or use my server on arduino.izzitech.com.ar, which I will try to keep at it finest.

You can sign up to create an account in order to get your API Key and post data.

## Characteristics

- Made with .net framework, ASP.net Core.
- Entity Framework as ORM, PostgreSQL as default database.
- Based on ThingSpeak API with some changes.
- Open Source.

## How to use it

Well, as starter, you could use ThingSpeak documentation as a more complete reference. Here you will find more precise information for using this system.

### How to send data

Send a HttpPost to the URL: https://arduino.izzitech.com.ar/api/entries/ with the following data:

``` 
    X-APIKEY=<your_api_key>
    field1=<field1_data>
    field2=<field2_data>
    ...
    field8=<field8_data>
    latitude=<latitude_data>
    longitude=<longitude_data>
    elevation=<elevation_data>
    location=<location_data>
```