using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    public class Result
    {
        public List<int> Exponent { get; set; }
        public List<int> Mantissa { get; set; }
        public bool IsNegative { get; set; }
    }
   public class FloatBit
    {
        public FloatBit(string code)
        {
            this.BinaryCode = code;
            this.Exponent = null;
        }
      
        private string BinaryCode { get; set; }
        private List<int> exponent;
        public List<int> Exponent 
        {
            get
            {
                return exponent;
            }
            set
            {
                if(value==null)
                {
                    exponent=this.BinaryCode.Split(' ')[1].Select(c => Int32.Parse(c.ToString())).ToList();
                }
                else
                {
                    exponent = value;
                }
            }
        }
        public List<int> Mantissa
        {
            get
            {
                return this.BinaryCode.Split(' ')[2].Select(c => Int32.Parse(c.ToString())).ToList();
            }
        }
        
        public bool IsNegative
        {
            get
            {
                return Convert.ToInt32(this.BinaryCode.Split(' ')[0]) == 1 ? true : false;

            }
        }
    }
}
