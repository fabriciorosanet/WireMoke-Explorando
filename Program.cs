using WireMock.Server;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Iniciar o servidor WireMock
var server = WireMockServer.Start();
Console.WriteLine($"WireMock rodando na porta: {server.Ports[0]}");

    // Configurar rotas
    server.Given(Request.Create().WithPath("/api/explorando").UsingGet())
      .RespondWith(Response.Create()
      .WithStatusCode(200)
      .WithHeader("Content-Type", "application/json")
      .WithBody("{ \"message\": \"Hello, WireMock!\" }"));

    server.Given(Request.Create().WithPath("/api/error").UsingGet())
          .RespondWith(Response.Create()
          .WithStatusCode(500)
          .WithHeader("Content-Type", "application/json")
          .WithBody("{ \"error\": \"Erro interno do servidor.\" }"));


    server.Given(Request.Create().WithPath("/api/itens").UsingGet())
          .RespondWith(Response.Create()
          .WithStatusCode(200)
          .WithHeader("Content-Type", "application/json")
          .WithBody("[{ \"id\": 1, \"nome\": \"Item 1\" }, { \"id\": 2, \"nome\": \"Item 2\" }, {\"id\": 3, \"nome\": \"Item 3\"}]"));

    server.Given(Request.Create().WithPath("/api/itens").UsingPost())
          .RespondWith(Response.Create()
          .WithStatusCode(201)
          .WithHeader("Content-Type", "application/json")
          .WithBody("{ \"id\": 3, \"nome\": \"Item 3\" }"));

    server.Given(Request.Create().WithPath("/api/itens/1").UsingGet())
          .RespondWith(Response.Create()
          .WithStatusCode(200)
          .WithHeader("Content-Type", "application/json")
          .WithBody("{ \"id\": 1, \"nome\": \"Item 1\" }"));

    server.Given(Request.Create().WithPath("/api/itens/1").UsingPut())
          .RespondWith(Response.Create()
          .WithStatusCode(200)
          .WithHeader("Content-Type", "application/json")
          .WithBody("{ \"id\": 1, \"nome\": \"Item 1 atualizado\" }"));

    server.Given(Request.Create().WithPath("/api/itens/1").UsingDelete())
          .RespondWith(Response.Create()
          .WithStatusCode(204));



app.Run();
