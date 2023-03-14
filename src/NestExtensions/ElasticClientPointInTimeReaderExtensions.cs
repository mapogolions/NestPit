using Nest;

namespace NestExtensions;

public static class ElasticClientPointInTimeReaderExtensions
{
    public static async Task<IPointInTimeReader<TDocument>> OpenPointInTimeReader<TDocument>(this IElasticClient client, PointInTimeReaderOptions options,
        CancellationToken cancellation = default) where TDocument : class
    {
        ArgumentNullException.ThrowIfNull(client);
        Time keepAlive = options.KeepAlive;
        var response = await client.OpenPointInTimeAsync(options.IndexName, o => o.KeepAlive(keepAlive.ToString()));
        return new PointInTimeReader<TDocument>(client, options, pit: response.Id);
    }

    public static Task<IPointInTimeReader<TDocument>> OpenPointInTimeReader<TDocument>(this IElasticClient client, string indexName, int size = 10_000, int slices = 1,
        CancellationToken cancellation = default)
        where TDocument : class
    {
        return OpenPointInTimeReader<TDocument>(client, new() { IndexName = indexName, Size = size, Slices = slices }, cancellation);
    }

    public static Task<IPointInTimeReader<TDocument>> OpenPointInTimeReader<TDocument>(this IElasticClient client, string indexName, TimeSpan keepAlive, int size = 10_000, int slices = 1,
        CancellationToken cancellation = default)
        where TDocument : class
    {
        return OpenPointInTimeReader<TDocument>(client, new() { IndexName = indexName, Size = size, Slices = slices, KeepAlive = keepAlive }, cancellation);
    }
}
