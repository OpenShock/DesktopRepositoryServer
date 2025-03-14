﻿// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace OpenShock.Desktop.RepositoryServer.ExceptionHandler;

public sealed class RequestInfo
{
    public required string? Path { get; set; }
    public required IDictionary<string, string> Query { get; set; }
    public required string Body { get; set; }
    public required string Method { get; set; }
    public required string TraceId { get; set; }
    public required IDictionary<string, string> Headers { get; set; }
}