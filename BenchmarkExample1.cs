using BenchmarkDotNet.Attributes;
using System.Text;

namespace BenchmarkExampleProject
{
    [MemoryDiagnoser]
    public class BenchmarkExample1
    {
        public int numberOfElements = 1000;
        private string[] testArray;
        public BenchmarkExample1()
        {
            testArray = new string[numberOfElements];

            for (int i = 0; i < numberOfElements; i++)
                testArray[i] = Guid.NewGuid().ToString("N");
        }

        [Benchmark(Baseline = true)]
        public string GenerateData()
        {
            string result = string.Empty;

            for (int i = 0; i < testArray.Length; i++)
                result = result + testArray[i] + "-";

            return result;
        }
        [Benchmark]
        public string GenerateDataWithConcat()
        {
            string result = string.Empty;

            for (int i = 0; i < testArray.Length; i++)
                result = string.Concat(result, testArray[i], "-");

            return result;
        }
        [Benchmark]
        public string GenerateDataWithWithStringbuilder()
        {
            var stringBuilder = new StringBuilder();

            for (int i = 0; i < testArray.Length; i++)
            {
                stringBuilder.Append(testArray[i]);
                stringBuilder.Append("-");
            }

            return stringBuilder.ToString();
        }
        [Benchmark]
        public string GenerateDataWithJoin()
        {
            return string.Join("-", testArray);
        }


    }
}
