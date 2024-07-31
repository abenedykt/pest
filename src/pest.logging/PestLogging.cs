using Microsoft.Extensions.Logging;
using OpenTelemetry.Exporter;
using OpenTelemetry.Logs;

namespace pest.logging;
using Microsoft.Extensions.Hosting;

public static class PestLogging
{

    public static void AddLogging(this IHostApplicationBuilder builder)
    {
        builder.Logging
            .ClearProviders()
            .AddOpenTelemetry(x =>
            {
                x.AddConsoleExporter();
                x.AddOtlpExporter(c =>
                {
                    c.Endpoint = new Uri("http://seq/ingest/otlp/v1/logs");
                    c.Protocol = OtlpExportProtocol.HttpProtobuf;
                });
            });    
    }
}