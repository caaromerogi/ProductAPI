# Product API Challenge

### Description
The following project was develop in C# using ASP.Net Core 6. Implements some patterns like builder, mediator, CQRS, Clean Architecture.

### How to run it?
To run it you need to have the SDK of ASP.Net Core 6. Also, you need to change "ProductConnection" string in ProductAPI.Api\appsetings.json to your connection string. Once you have it, go to the file /ProductAPI.Infrastructure
```
cd .\ProductAPI.Infrastructure\
```
and excute the following command:

```
dotnet ef -p . --startup-project ..\ProductAPI.Api\ database update
```

https://documenter.getpostman.com/view/22399476/2s8Z72WCHo