using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddDockerComposePublisher();

var apiService = builder.AddProject<Projects.aspiredockercompose_ApiService>("apiservice")
    .WithHttpsHealthCheck("/health");

builder.AddProject<Projects.aspiredockercompose_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithHttpsHealthCheck("/health")
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
    