It should work something like this. Design is still in progress

```c#
await using (var reader = client.PointInTimeReader("index", size: 10_000, slices: 4))
{
    var slices = reader.Slices;
    await reader.OpenPit();
    await Task.WhenAll(slices.Select(HandleSlice));
}
```
