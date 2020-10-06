using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePhoneBook
{
    public class NameRange : IComparer<PhoneInfo>
    {
        public int Compare(PhoneInfo x, PhoneInfo y)
        {
            return x.RangeName.CompareTo(y.RangeName);
        }
    }   //이름 오름차순 정렬

    public class NumberRange : IComparer<PhoneInfo>
    {
        public int Compare(PhoneInfo x, PhoneInfo y)
        {
            return x.RangeNumber.CompareTo(y.RangeNumber);
        }
    }   //번호 오름차순 정렬
    class ChartRange{ }
}
