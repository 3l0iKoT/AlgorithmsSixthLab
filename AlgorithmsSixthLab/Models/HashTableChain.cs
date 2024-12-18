using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsSixthLab.Models
{
    public class HashTableChain
    {
        private LinkedList<KeyValuePair<int, string>>[] table;
        private Func<int, int, int> hashFunction;

        public HashTableChain(int size, Func<int, int, int> hashFunction)
        {
            table = new LinkedList<KeyValuePair<int, string>>[size];
            this.hashFunction = hashFunction;
            for (int i = 0; i < size; i++)
                table[i] = new LinkedList<KeyValuePair<int, string>>();
        }

        public void Insert(int key, string value)
        {
            int index = hashFunction(key, table.Length);
            foreach (var pair in table[index])
            {
                if (pair.Key == key)
                    throw new InvalidOperationException("Key already exists.");
            }
            table[index].AddLast(new KeyValuePair<int, string>(key, value));
        }

        public string Search(int key)
        {
            int index = hashFunction(key, table.Length);
            foreach (var pair in table[index])
            {
                if (pair.Key == key)
                    return pair.Value;
            }
            return null; // Element not found
        }

        public void Remove(int key)
        {
            int index = hashFunction(key, table.Length);
            var node = table[index].First;
            while (node != null)
            {
                if (node.Value.Key == key)
                {
                    table[index].Remove(node);
                    return;
                }
                node = node.Next;
            }
            throw new InvalidOperationException("Key not found.");
        }

        public void Analyze(out int minChain, out int maxChain, out double loadFactor)
        {
            minChain = int.MaxValue;
            maxChain = 0;
            int filledSlots = 0;

            foreach (var chain in table)
            {
                int chainLength = chain.Count;
                if (chainLength > 0) filledSlots++;
                minChain = Math.Min(minChain, chainLength);
                maxChain = Math.Max(maxChain, chainLength);
            }

            loadFactor = (double)filledSlots / table.Length;
        }
        public void ExportToFile(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                for (int i = 0; i < table.Length; i++)
                {
                    writer.WriteLine($"[{i}]: {table[i].Count}");
                    //writer.Write($"[{i}]: ");
                    //foreach (var pair in table[i])
                    //{
                    //    writer.Write($"({pair.Key}, {pair.Value}) -> ");
                    //}
                    //writer.WriteLine("null");
                }
            }
        }
    }
}
