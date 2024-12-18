using AlgorithmsSixthLab.Models;
using AlgorithmsSixthLab.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsSixthLab
{
    class Program
    {
        static void Main(string[] args)
        {
            // Генерация уникальных ключей
            HashSet<int> keys1 = GenerateKeys(100000);

            // Анализ работы хэш-функций с цепочками
            Console.WriteLine("Analysis for Chaining:");
            Analyzer.AnalyzeHashFunctions(keys1, 1000);

            HashSet<int> keys2 = GenerateKeys(10000);

            // Анализ работы хэш-функций с открытой адресацией
            Console.WriteLine("Analysis for Open Addressing:");
            Analyzer.AnalyzeOpenAddressing(keys2, 10000);

            Console.ReadKey();
        }

        static HashSet<int> GenerateKeys(int count)
        {
            HashSet<int> keys = new HashSet<int>();
            Random random = new Random();
            while (keys.Count < count)
            {
                keys.Add(random.Next());
            }
            return keys;
        }
    }
}
