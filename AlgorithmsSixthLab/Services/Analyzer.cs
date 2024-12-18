using AlgorithmsSixthLab.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsSixthLab.Services
{
    public static class Analyzer
    {
        public static void AnalyzeHashFunctions(IEnumerable<int> keys, int tableSize)
        {
            var hashFunctions = new List<Func<int, int, int>>
            {
                HashFunctions.DivisionHash,
                HashFunctions.MultiplicationHash,
                HashFunctions.CustomHash,
                //HashFunctions.BadHash1,
                //HashFunctions.BadHash2
            };

            foreach (var hashFunction in hashFunctions)
            {
                var table = new HashTableChain(tableSize, hashFunction);
                foreach (var key in keys)
                {
                    table.Insert(key, $"Value{key}");
                }

                table.Analyze(out int minChain, out int maxChain, out double loadFactor);
                Console.WriteLine($"Hash Function: {hashFunction.Method.Name}");
                Console.WriteLine($"Min Chain: {minChain}, Max Chain: {maxChain}, Load Factor: {loadFactor}");
                Console.WriteLine();
                table.ExportToFile($"HashTableChain{hashFunction.Method.Name}.txt");
            }
        }
        public static void AnalyzeOpenAddressing(IEnumerable<int> keys, int tableSize)
        {
            var probingMethods = new List<Func<int, int, int, int>>
            {
                HashFunctions.LinearProbing,
                HashFunctions.QuadraticProbing,
                HashFunctions.DoubleHashing,
                HashFunctions.CustomProbing1,
                HashFunctions.CustomProbing2
            };

            var hashFunction = HashFunctions.DivisionHash; // Можно использовать любую функцию
            foreach (var probingMethod in probingMethods)
            {
                var table = new OpenAddressingHashTable(tableSize, hashFunction, probingMethod);
                foreach (var key in keys)
                {
                    try
                    {
                        table.Insert(key, $"Value{key}");
                    }
                    catch (InvalidOperationException)
                    {
                        Console.WriteLine("Table overflow.");
                        break;
                    }
                }

                int longestCluster = table.GetLongestCluster();
                Console.WriteLine($"Probing Method: {probingMethod.Method.Name}");
                Console.WriteLine($"Longest Cluster: {longestCluster}");
                Console.WriteLine();
                table.ExportToFile($"OpenAddressingHashTable{probingMethod.Method.Name}.txt");
            }
        }
    }
}
