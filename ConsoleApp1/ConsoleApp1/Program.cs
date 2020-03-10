using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> bitNumberOne = new List<int>();
            List<int> bitNumberTwo = new List<int>();
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

                    bitNumberOne.Add(e);
                }
            }
            WriteResult(numberOne.ToString(), bitNumberOne.ToArray());
            foreach (var el in arrayByteMN)
            {
                foreach (var e in FillBits(el))
                {
                    bitNumberTwo.Add(e);
                }
            }
            WriteResult(numberTwo.ToString(), bitNumberTwo.ToArray());
            int[] q = bitNumberOne.ToArray();
            int[] d = bitNumberTwo.ToArray();
            int[] different;
            int[] remainder = new int[q.Length];
            WriteResult("number one: ", q);
            WriteResult("number two: ", d);
            //int[] q = { 1, 1, 1, 1, 1, 0, 1, 0, 0 }; // 400
            //int[] d = { 0, 0, 1, 1, 0, 0, 1, 0, 0 }; //3
            //int[] different;
            //int[] remainder = new int[q.Length];

            d = Dopov(d);
            for (int i = 0; i < q.Length; i++)
            {
                Right(ref q, ref remainder);
                WriteResult("quotient", q);
                different = SumBit(remainder, d);
                WriteResult("different", different);
                if (different[0] == 0)
                {
                    remainder = different;
                    q[q.Length - 1] = 1;
                }
                WriteResult("remainder", remainder);
               
            }
            foreach (var el in q)
            {
                Console.Write(el);
            }
            Console.WriteLine();
            foreach (var el in remainder)
            {
                Console.Write(el);
            }
            Console.ReadKey();

        }
        private static void WriteResult(string isOperrand,int [] writeArray)
        {
            Console.Write(isOperrand+" :  ");
            foreach(var el in writeArray)
            {
                Console.Write(el);
            }
            Console.WriteLine();
        }
        private static int[] Dopov(int[] d)
        {
            int[] result = new int[d.Length];
            for (var index = d.Length-1; index >= 0; index--)
            {
                d[index] = d[index] == 0 ? 1 : 0;
            }
            result[d.Length - 1] = 1;
            d = SumBit(d, result);
            return d;
        }
        private static void Right(ref int[] q, ref int[] r)
        {
            List<int> res1 = new List<int>();
            List<int> res2 = new List<int>();
            
            for(int i = 1; i <q.Length; i++)
            {
                res1.Add(q[i]);
                if (i == q.Length-1)
                {
                    res1.Add(0);
                    for (var j = 1; j <r.Length; j++)
                    {
                        res2.Add(r[j]);
                    }
                    res2.Add(q[0]);
                }
            }
            q = res1.ToArray();
            r = res2.ToArray();

        }
        private static int[] SumBit(int[] q, int[] d)
        {
            int temp = 0;
            int[] result = new int[q.Length];
            for (int i = q.Length - 1; i >= 0; i--)
            {

                result[i] = (temp + q[i] + d[i]) % 2;
                temp = (temp+q[i] + d[i]) / 2;
            }
            return result;
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
