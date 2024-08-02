using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Exporter;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

namespace pest.logging;
using Microsoft.Extensions.Hosting;

public static class PestLogging
{

    public static void AddLogging(this IHostApplicationBuilder builder)
    {
        builder.Services.AddOpenTelemetry()
            .WithTracing(tracing => tracing
                // The rest of your setup code goes here
                .AddOtlpExporter(options =>
                {
                    options.Endpoint = new Uri("http://seq/ingest/v1/traces");
                    options.Protocol = OtlpExportProtocol.HttpProtobuf;
                }))
            .WithMetrics(metrics => metrics
                // The rest of your setup code goes here
                .AddOtlpExporter(options =>
                {
                    options.Endpoint = new Uri("http://seq/ingest/v1/metrics");
                    options.Protocol = OtlpExportProtocol.HttpProtobuf;
                }));

        builder.Logging.AddOpenTelemetry(logging =>
        {
            // The rest of your setup code goes here
            logging.AddOtlpExporter(options =>
            {
                options.Endpoint = new Uri("http://seq/ingest/v1/logs");
                options.Protocol = OtlpExportProtocol.HttpProtobuf;
            });
        });
        
        // builder.Logging
        //     .ClearProviders()
        //     .AddOpenTelemetry(x =>
        //     {
        //         x.AddConsoleExporter();
        //         x.AddOtlpExporter(c =>
        //         {
        //             c.Endpoint = new Uri("http://seq/ingest/otlp/v1/logs");
        //             c.Protocol = OtlpExportProtocol.HttpProtobuf;
        //         });
        //     });    
    }
}