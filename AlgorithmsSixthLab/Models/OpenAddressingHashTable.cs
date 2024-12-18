using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsSixthLab.Models
{
    public class OpenAddressingHashTable
    {
        private int[] keys;    // Массив ключей
        private string[] values; // Массив значений
        private int size;
        private Func<int, int, int> hashFunction; // Основная хэш-функция
        private Func<int, int, int, int> probingFunction; // Метод исследования

        public OpenAddressingHashTable(int size, Func<int, int, int> hashFunction, Func<int, int, int, int> probingFunction)
        {
            this.size = size;
            this.hashFunction = hashFunction;
            this.probingFunction = probingFunction;
            keys = new int[size];
            values = new string[size];
            Array.Fill(keys, -1); // Используем -1 для пустых ячеек
        }

        public void Insert(int key, string value)
        {
            for (int i = 0; i < size; i++)
            {
                int index = probingFunction(hashFunction(key, size), i, size);
                if (keys[index] == -1 || keys[index] == -2) // Свободная ячейка или удаленная
                {
                    keys[index] = key;
                    values[index] = value;
                    return;
                }
            }
            throw new InvalidOperationException("Hash table is full.");
        }

        public string Search(int key)
        {
            for (int i = 0; i < size; i++)
            {
                int index = probingFunction(hashFunction(key, size), i, size);
                if (keys[index] == -1) return null; // Не найдено
                if (keys[index] == key) return values[index]; // Найдено
            }
            return null;
        }

        public void Remove(int key)
        {
            for (int i = 0; i < size; i++)
            {
                int index = probingFunction(hashFunction(key, size), i, size);
                if (keys[index] == -1) break; // Ключ не найден
                if (keys[index] == key)
                {
                    keys[index] = -2; // Удаляем ключ
                    values[index] = null;
                    return;
                }
            }
            throw new InvalidOperationException("Key not found.");
        }

        public int GetLongestCluster()
        {
            int maxCluster = 0, currentCluster = 0;

            for (int i = 0; i < size; i++)
            {
                if (keys[i] != -1 && keys[i] != -2) currentCluster++;
                else
                {
                    maxCluster = Math.Max(maxCluster, currentCluster);
                    currentCluster = 0;
                }
            }

            return Math.Max(maxCluster, currentCluster);
        }
        public void ExportToFile(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                for (int i = 0; i < keys.Length; i++)
                {
                    if (keys[i] == -1)
                    {
                        writer.WriteLine($"[{i}]: Empty");
                    }
                    else if (keys[i] == -2)
                    {
                        writer.WriteLine($"[{i}]: Deleted");
                    }
                    else
                    {
                        writer.WriteLine($"[{i}]: ({keys[i]}, {values[i]})");
                    }
                }
            }
        }
    }
}
