# What this project does

This project is a demonstration that a .NET isolated process Azure Function runs out of connections causing a `Host thresholds exceeded: Connections`, even though the connections are being pooled.

<strong>Running this project locally probably won't cause any issues, because the local connection pool is way larger than the 600 max active connections from the Azure Functions sandbox (or 1200 total connections).</strong>

## Functions contained

These are the functions currently implemented:

- **CallerFunctionHttpFactory**: Runs 1250 requests on an API using the HttpClientFactory, registered via DI.
- **CallerFunctionStaticHttpClient**: Runs 1250 requests on an API using a static HttpClient.