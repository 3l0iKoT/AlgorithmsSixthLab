using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsSixthLab.Models
{
    public static class HashFunctions
    {
        // Task 1
        public static int DivisionHash(int key, int size)
        {
            return key % size;
        }

        public static int MultiplicationHash(int key, int size)
        {
            double A = 0.6180339887; // Константа
            return (int)(size * ((key * A) % 1));
        }

        public static int CustomHash1(int key, int size)
        {
            key = (int)((key ^ (key >> 4)) * 2654435761); // XOR + побитовая перестановка
            return Math.Abs(key % size);
        }

        // Task 2
        public static int LinearProbing(int hash, int step, int size)
        {
            return (hash + step) % size;
        }

        public static int QuadraticProbing(int hash, int step, int size)
        {
            return (hash + step * step) % size;
        }

        public static int DoubleHashing(int hash, int step, int size)
        {
            int hash2 = 1 + (hash % (size - 1));
            return (hash + step * hash2) % size;
        }

        public static int CustomProbing1(int hash, int step, int size)
        {
            return (hash + step * (step + 3)) % size; // Модифицированное квадратичное
        }

        public static int CustomProbing2(int hash, int step, int size)
        {
            return (int)((hash ^ (step * 2654435761)) % size); // Побитовое смешивание
        }
    }
}
