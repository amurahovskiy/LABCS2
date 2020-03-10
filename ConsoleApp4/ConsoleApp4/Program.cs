using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    class Program
    {
        static bool isFlag = false;
        static int counterOverFlow = 0;
        static void Main(string[] args)
        {
            float numb1, numb2;
            string write = null;
            numb1 = float.Parse(Console.ReadLine());
            numb2 = float.Parse(Console.ReadLine());

            // Two operand



            string numb1InBin = FillBits(numb1);
            string numb2InBin = FillBits(numb2);

            FloatBit first = new FloatBit(numb1InBin);
            FloatBit second = new FloatBit(numb2InBin);



            Console.WriteLine($"{numb1}\t{Convert.ToInt32(first.IsNegative)} {GetListAsStr(first.Exponent)} {GetListAsStr(first.Mantissa)} ");
            Console.WriteLine($"{numb2}\t{Convert.ToInt32(second.IsNegative)} {GetListAsStr(second.Exponent)} {GetListAsStr(second.Mantissa)}");


            first.Exponent = Subtract(first.Exponent, new List<int>() { 0, 1, 1, 1, 1, 1, 1, 1 });
            second.Exponent = Subtract(second.Exponent, new List<int>() { 0, 1, 1, 1, 1, 1, 1, 1 });


            Result result = new Result();

            result.IsNegative = ((!(bool)first.IsNegative && !(bool)second.IsNegative) || ((bool)first.IsNegative && (bool)second.IsNegative)) ? false : true;
            result.Exponent = ADD(first.Exponent, second.Exponent, 0);
            write = WriteResult("step 1 :", result.Exponent);
            Console.WriteLine(write);
            result.Mantissa = multiplicate(first.Mantissa, second.Mantissa);
            write = WriteResult("step 2 :", result.Mantissa);
            Console.WriteLine(write);
            if (counterOverFlow >= 1 && (!(bool)first.IsNegative && !(bool)second.IsNegative))
                result.Exponent = ADD(result.Exponent, new List<int>() { 0, 0, 0, 0, 0, 0, 0, 1 }, 0);

            result.Exponent = ADD(result.Exponent, new List<int>() { 0, 1, 1, 1, 1, 1, 1, 1 }, 0);

            Console.WriteLine();
            Console.WriteLine("result: " + Convert.ToInt32(result.IsNegative) + "  " + WriteResult(string.Empty, result.Exponent) + "  " + WriteResult(string.Empty, result.Mantissa));

            string resultStr = Convert.ToInt32(result.IsNegative) + WriteResult(string.Empty, result.Exponent) + WriteResult(string.Empty, result.Mantissa);
            long num = Convert.ToInt64(resultStr, 2);
            byte[] bits = BitConverter.GetBytes(num);
            float res = BitConverter.ToSingle(bits, 0);

            Console.WriteLine(res);
            Console.ReadKey();

        }
        static string WriteResult(string num,List<int> listNumber)
        {
            Console.Write(num+ "  ");
            string str = string.Empty;
            foreach(var el in listNumber)
            {
                
                str += el;
            }
            return str;

        }
        static List<int> Subtract(List<int> exponent, List<int> numb)
        {
            List<int> result = new List<int>();

            for (int i = exponent.Count - 1; i >= 0; i--)
            {
                if (exponent[i] == 0 && numb[i] == 0)
                {
                    result.Add(0);
                }
                else if (exponent[i] == 1 && numb[i] == 1)
                {
                    result.Add(0);
                }
                else if (exponent[i] == 1 && numb[i] == 0)
                {
                    result.Add(1);
                }
                else
                {
                    for (int j = i - 1; j >= 0; j--)
                    {
                        if (exponent[j] == 1)
                        {
                            for (int k = j + 1; k <= i; k++)
                            {
                                if (exponent[k] == 0)
                                    exponent[k] = 1;
                            }
                            exponent[j] = 0;
                            result.Add(1);
                            break;
                        }
                    }
                }
            }

            result.Reverse();
            return result;
        }
        static string GetListAsStr(List<int> list)
        {
            string result = string.Join("", list.ToArray());
            return result;
        }
     
        static List<int> multiplicate(List<int> first, List<int> second)
        {
            List<int> result = new List<int>();
            bool isisFlag = false;

            List<int> help = new List<int>() { 1 };
            help.AddRange(first);
            first.Clear();
            first.AddRange(help);
            help.Clear();
            help.Add(1);
            help.AddRange(second);
            second.Clear();
            second.AddRange(help);

            List<int> helper1 = new List<int>();
            List<int> helper2 = new List<int>();

            if (second[second.Count - 1] == 0)
            {
                helper1.AddRange(new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });
            }
            else
            {
                helper1.AddRange(first);
            }

            List<int> helper = new List<int>();
            int shift = 1;
            for (int i = second.Count - 2; i >= 0; i--)
            {
                helper2.Clear();
                if (second[i] == 0)
                {
                    helper2.AddRange(new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });
                }
                else
                {
                    helper2.AddRange(first);
                }
                helper.AddRange(helper1);
                helper1.Clear();
                shift = helper.Count - helper2.Count + 1;
                helper1.AddRange(ADD(helper, helper2, shift));
                
                helper.Clear();
            }

            for (int i = 0; i < helper1.Count; i++)
            {
                while (helper1[0] == 0)
                {
                    helper1.RemoveAt(0);
                }
                helper1.RemoveAt(0);
                break;
            }
            List<int> helperForHelp = new List<int>();
            helperForHelp.AddRange(helper1);
            helper1.Clear();
            for (int i = 0; i < 23; i++)
            {
                helper1.Add(helperForHelp[i]);
            }
          

            return helper1;
        }
        static List<int> ADD(List<int> first, List<int> second, int shift)
        {
            List<int> result = new List<int>();

            if (shift > 0 && !Program.isFlag)
            {
                List<int> help = new List<int>() { 0 };
                help.AddRange(first);
                first.Clear();
                first.AddRange(help);
            }
            Program.isFlag = false;

            while (shift > 0)
            {
                second.Add(0);
                shift--;
            }

            int mind = 0;
            for (int i = first.Count - 1; i >= 0; i--)
            {
                if (first[i] == 0 && second[i] == 0 && mind == 0)
                {
                    result.Add(0);
                }
                else if ((first[i] == 0 && second[i] == 0 && mind == 1) ||
                        (first[i] == 0 && second[i] == 1 && mind == 0) ||
                        (first[i] == 1 && second[i] == 0 && mind == 0))
                {
                    result.Add(1);
                    mind = 0;
                }
                else if ((first[i] == 0 && second[i] == 1 && mind == 1) ||
                        (first[i] == 1 && second[i] == 0 && mind == 1) ||
                        (first[i] == 1 && second[i] == 1 && mind == 0))
                {
                    result.Add(0);
                    mind = 1;
                }
                else
                {
                    result.Add(1);
                    mind = 1;
                }
            }
            if (mind == 1)
            {
                result.Add(1);
                Program.isFlag = true;
                Program.counterOverFlow++;
            }

            result.Reverse();
            return result;
        }
        static string FillBits(float f)
        {
            StringBuilder sb = new StringBuilder();
            Byte[] ba = BitConverter.GetBytes(f);
            foreach (Byte b in ba)
                for (int i = 0; i < 8; i++)
                {
                    sb.Insert(0, ((b >> i) & 1) == 1 ? "1" : "0");
                }
            string s = sb.ToString();
            string r = s.Substring(0, 1) + " " + s.Substring(1, 8) + " " + s.Substring(9);
            return r;
        }
    }
   
}
