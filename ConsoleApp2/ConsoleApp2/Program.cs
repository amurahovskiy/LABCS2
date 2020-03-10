using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> multiPlicandList = new List<int>();
            List<int> multiplierList = new List<int>();
            int numberOne = int.Parse(Console.ReadLine());
            int numberTwo = int.Parse(Console.ReadLine());
            byte[] arrayByteM = BitConverter.GetBytes(numberOne);
            arrayByteM = arrayByteM.Reverse().ToArray();
            byte[] arrayByteMN = BitConverter.GetBytes(numberTwo);
            arrayByteMN = arrayByteMN.Reverse().ToArray();
            foreach (var el in arrayByteM)
            {
                foreach (var e in FillBits(el))
                {

                    multiPlicandList.Add(e);
                }
            }
            Write(multiPlicandList,numberOne);
            foreach (var el in arrayByteMN)
            {
                foreach (var e in FillBits(el))
                {
                    multiplierList.Add(e);
                }
            }
            Write(multiplierList, numberTwo);
            //int[] multiPlicand = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0 };
            //int[] multiplier =   { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 };
            //List<int> multiPlicandList = multiPlicand.ToList();
            //List<int> multiplierList = multiplier.ToList();
            List<List<int>> partialProducts = new List<List<int>>();
            var tempResult = new List<int>();
            for(var index= multiplierList.Count-1;index>=0;index--)
            {
                partialProducts.Add(multiplierList[index] == 1 ? multiPlicandList.ToList() : new int[multiPlicandList.Count].ToList());
                multiPlicandList.Add(0);
            }
            foreach (var el in partialProducts)
            {
                foreach (var e in el)
                {
                    Console.Write(e);
                }
                Console.WriteLine();
            }
            for (var index = partialProducts.Count - 1; index > 0; index--)
            {

                partialProducts[index - 1].InsertRange(0, new int[partialProducts[partialProducts.Count - 1].Count - partialProducts[index - 1].Count].ToList());
                if (index == partialProducts.Count - 1)
                {
                    tempResult = SumBit(partialProducts[index], partialProducts[index - 1]);
                }
                else
                {
                    tempResult = SumBit(tempResult, partialProducts[index - 1]);
                }
            }
            foreach (var el in tempResult)
            {
                Console.Write(el);
            }

            Console.ReadKey();
        }
        private static void Write(List<int> bitNumber, int number)
        {
            Console.Write(number + " число в бінарному вигляді: ");
            foreach (var el in bitNumber)
            {

                Console.Write(el);

            }
            Console.WriteLine();
        }
        private static List<int> SumBit(List<int>q, List<int>d)
        {
            int temp = 0;
            int[] result = new int[q.Count];
            for (int i = q.Count- 1; i >= 0; i--)
            {

                result[i] = (temp + q[i] + d[i]) % 2;
                temp = (temp + q[i] + d[i]) / 2;
            }
            return result.ToList();
        }
        private static List<int> FillBits(byte byt)
        {
            var result = new List<int>();
            var bits = new Stack<int>();
            for (var index = 0; index < 8; index++)
            {
                bits.Push(byt % 2);
                byt /= 2;
            }

            return bits.ToList();
        }
    }
}
