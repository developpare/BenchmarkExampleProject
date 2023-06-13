using BenchmarkDotNet.Attributes;

namespace BenchmarkExampleProject
{
    [MemoryDiagnoser]
    public class BenchmarkExample3
    {
        [Params(200, 1000)]
        public int N;

        public string[] ArrayOfElements;
        private SortedList<string, string> sortedList;
        private int SearchableElements = 4;

        [GlobalSetup]
        public void Setup()
        {
            int customIndexCount = 1;

            var offset = N / SearchableElements;
            N = N + SearchableElements;

            ArrayOfElements = new string[N];
            sortedList = new SortedList<string, string>();

            for (int i = 0; i < N; i++)
            {
                if (i == offset * customIndexCount)
                {
                    var value = customIndexCount + "TEST";
                    ArrayOfElements[i] = value;
                    sortedList.Add(value, value);
                    customIndexCount++;
                    continue;
                }

                ArrayOfElements[i] = Guid.NewGuid().ToString("N");
                sortedList.Add(ArrayOfElements[i], ArrayOfElements[i]);
            }
        }

        [Benchmark(Baseline = true)]
        [Arguments("1TEST")]
        [Arguments("2TEST")]
        [Arguments("3TEST")]
        [Arguments("4TEST")]
        public void NoSortedArray(string value)
        {
            Array.Find(ArrayOfElements, x => x.Equals(value));
        }
        [Benchmark]
        [Arguments("1TEST")]
        [Arguments("2TEST")]
        [Arguments("3TEST")]
        [Arguments("4TEST")]
        public void UsingSortedList(string value)
        {
            sortedList.TryGetValue(value, out var result);
        }




    }

}
