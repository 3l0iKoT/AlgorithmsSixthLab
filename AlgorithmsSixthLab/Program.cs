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
            const int numberOfKeys = 100000;

            // Генерация уникальных ключей
            HashSet<int> keys = GenerateKeys(numberOfKeys);

            // Анализ работы хэш-функций с цепочками
            Console.WriteLine("Analysis for Chaining:");
            Analyzer.AnalyzeHashFunctions(keys, 1000);

            // Анализ работы хэш-функций с открытой адресацией
            Console.WriteLine("Analysis for Open Addressing:");
            Analyzer.AnalyzeOpenAddressing(keys, 10000);
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
