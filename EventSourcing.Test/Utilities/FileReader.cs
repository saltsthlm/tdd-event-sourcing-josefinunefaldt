using EventSourcing.Events;
using System.Reflection;
using System.Text.Json;

namespace EventSourcing.Test.Utilities;

public static class FileReader
{
  private static readonly Assembly ASSEMBLY = Assembly.GetExecutingAssembly();
  private static readonly string ASSEMBLY_NAME = ASSEMBLY.GetName().Name!;

  public static async Task<Event[]> GetStream(int num)
  {
    var streamName = $"{ASSEMBLY_NAME}.test_streams.stream-{num:D3}.json";
    using var stream = ASSEMBLY.GetManifestResourceStream(streamName)
      ?? throw new FileNotFoundException($"The test stream with the name 'stream-{num:D3}.json' was not found!");

    return (await JsonSerializer.DeserializeAsync<Event[]>(stream))!;
  }
}
