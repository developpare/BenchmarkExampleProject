using BenchmarkDotNet.Attributes;

namespace BenchmarkExampleProject
{
    [MemoryDiagnoser]
    [SimpleJob(BenchmarkDotNet.Jobs.RuntimeMoniker.Net70, baseline: true)]
    [SimpleJob(BenchmarkDotNet.Jobs.RuntimeMoniker.Net60)]

    public class BenchmarkExample4
    {
        [Params(200, 1000)]
        public int N;

        public string[] ArrayOfElements;
        private int SearchableElements = 4;

        [GlobalSetup]
        public void Setup()
        {
            int customIndexCount = 1;

            var offset = N / SearchableElements;
            N = N + SearchableElements;

            ArrayOfElements = new string[N];

            for (int i = 0; i < N; i++)
            {
                if (i == offset * customIndexCount)
                {
                    var value = customIndexCount + "TEST";
                    ArrayOfElements[i] = value;
                    customIndexCount++;
                    continue;
                }

                ArrayOfElements[i] = Guid.NewGuid().ToString("N");
            }
        }

        [Benchmark]
        [Arguments("1TEST")]
        [Arguments("2TEST")]
        [Arguments("3TEST")]
        [Arguments("4TEST")]
        public int IndexOfBase(string value)
        {
            return Array.IndexOf(ArrayOfElements, value);
        }



    }

}
