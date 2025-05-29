var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.FcmMessaging_Api>("fcmmessaging-api");

builder.Build().Run();
