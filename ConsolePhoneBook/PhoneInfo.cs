using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePhoneBook
{
    
    public class NameRange : IComparer
    {
        public int Compare(object x, object y)
        {
            // 이름이 크면 1, 이름이 작으면 -1, 이름이 같으면 0
            PhoneInfo first = x as PhoneInfo;
            PhoneInfo second = y as PhoneInfo;

            if (first.RangeName.CompareTo(second.RangeName) == 1)
                return 1;
            else if (first.RangeName.CompareTo(second.RangeName) == -1)
                return -1;
            else
                return 0;
        }
    }   //이름 오름차순 정렬

    public class NumberRange : IComparer
    {
        public int Compare(object x, object y)
        {
            // 번호가 크면 1, 작으면 -1
            PhoneInfo first = x as PhoneInfo;
            PhoneInfo second = y as PhoneInfo;

            if (first.SearchNumber.CompareTo(second.SearchNumber) == 1)
                return 1;
            else if (first.SearchNumber.CompareTo(second.SearchNumber) == -1)
                return -1;
            else
                return 0;
        }
    }   //번호 오름차순 정렬

    public class PhoneInfo //클래스 앞 public
    {
        string name;    //이름(필수)
        string phoneNumber; //전화번호(필수)
        string birth;   //생일(선택)

        public string RangeName { get { return name; } }    
        public string SearchNumber { get { return phoneNumber; } }

        public PhoneInfo() { }

        public PhoneInfo(string name, string num)
        {
            this.name = name;
            phoneNumber = num;
        }

        public PhoneInfo(string name, string num, string birth)
        {
            this.name = name;
            phoneNumber = num;
            this.birth = birth;
        }

        public virtual void ShowInfo()
        {
            Console.Write($"\n이름 : {this.name} | 번호 : {this.phoneNumber} | 생일 : {this.birth}");
        }
    }

    public class PhoneUnivInfo : PhoneInfo
    {
        string major;
        int year;

        public PhoneUnivInfo(string name, string phonenumber, string birth, string major, int year)
            : base(name, phonenumber, birth)
        {
            this.major = major;
            this.year = year;
        }
        
        public override void ShowInfo()
        {
            base.ShowInfo();
            Console.Write($" | 대학/학년 : {this.major}/{this.year}");
        }
    }
        
    public class PhoneCompanyInfo : PhoneInfo
    {
        string company;
    
        public PhoneCompanyInfo(string name, string phonenumber, string birth, string company)
            : base(name, phonenumber, birth)
        {
            this.company = company;
        }
    
        public override void ShowInfo()
        {
            base.ShowInfo();
            Console.Write($" | 회사 : {this.company}");
        }
    }
}
