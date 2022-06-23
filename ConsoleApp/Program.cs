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

        // take high memory
        var result = (from keyValuePair in solutionDetail
                      let matchedPair = answerDetail.FirstOrDefault(c => c.Key == keyValuePair.Key)
                      where string.Equals(keyValuePair.Value, matchedPair.Value, StringComparison.CurrentCultureIgnoreCase)
                      select keyValuePair).Count();
        // take low memory
        var anotherCalculation = CalculateResult(solutionDetail, answerDetail);
        

        Console.WriteLine("Match value between 2 JSON Array \nResult: ");
        Console.WriteLine(result);
        Console.WriteLine(anotherCalculation);
    }
    public static int CalculateResult(IDictionary<byte, string> value, IDictionary<byte, string> compare)
    {
        var result = 0;
        foreach (var item in value)
        {
            if (compare.ContainsKey(item.Key))
            {
                if (item.Value.Equals(compare[item.Key]))
                {
                    result++;
                }
            }
        }
        return result;
    }
}