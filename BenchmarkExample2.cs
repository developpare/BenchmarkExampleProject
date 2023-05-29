using BenchmarkDotNet.Attributes;

namespace BenchmarkExampleProject
{
    [MemoryDiagnoser]
    public class BenchmarkExample2
    {
        [Params(30, 200, 1000)]
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
        public void NoSortedArray()
        {
            for (int SearchableElementsIndex = 1; SearchableElementsIndex <= SearchableElements; SearchableElementsIndex++)
            {
                var value = SearchableElementsIndex + "TEST";

                Array.Find(ArrayOfElements, x => x.Equals(value));
            }
        }
        [Benchmark]
        public void UsingSortedList()
        {
            for (int SearchableElementsIndex = 1; SearchableElementsIndex <= SearchableElements; SearchableElementsIndex++)
            {
                var value = SearchableElementsIndex + "TEST";

                sortedList.TryGetValue(value, out var result);
            }
        }



    }

}
