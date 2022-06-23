using Newtonsoft.Json;
namespace ConsoleApp;

internal static class Program
{
    public static IDictionary<byte, string> Deserialize(this string serializedJsonString)
    {
        var deserializeObj = JsonConvert.DeserializeObject<List<Dictionary<byte, string>>>(serializedJsonString);
        var dictionary = new Dictionary<byte, string>(); deserializeObj?.ForEach(d => dictionary.Add((byte)d.FirstOrDefault().Key, d.FirstOrDefault().Value.ToString()));
        return dictionary;
    }

    private static void Main(string[] args)
    {
        var solutionDetail = @"[{'1':'A'},{'2':'B'},{'3':'C'},{'4':'D'}]".Deserialize();
        var answerDetail = @"[{'1':'A'},{'2':'Null'},{'3':'C'},{'4':'multiple'}]".Deserialize();

        var result = (from keyValuePair in solutionDetail
                      let matchedPair = answerDetail.FirstOrDefault(c => c.Key == keyValuePair.Key)
                      where string.Equals(keyValuePair.Value, matchedPair.Value, StringComparison.CurrentCultureIgnoreCase)
                      select keyValuePair).Count();

        Console.WriteLine("Match value between 2 JSON Array \nResult: ");
        Console.WriteLine(result);
    }
}