using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePhoneBook
{
    [Serializable]
    public class PhoneInfo //클래스 앞 public
    {
        string name;    //이름(필수)
        string phoneNumber; //전화번호(필수)
        string birth;   //생일(선택)
        public string checkName, checkNum;

        public string RangeName { get { return name; } }
        public string RangeNumber { get { return phoneNumber; } }

        public PhoneInfo() { }

        public PhoneInfo(string num)
        { checkNum = num; }

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

        public override string ToString()
        {
            return $"\n이름 : {this.name} | 번호 : {this.phoneNumber} | 생일 : {this.birth}";
        }
    }

    [Serializable]
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

        public override string ToString()
        {
            return base.ToString().Replace("생일 : ", "") + $"대학/학년 : {this.major}/{this.year}";
        }


    }

    [Serializable]
    public class PhoneCompanyInfo : PhoneInfo
    {
        string company;

        public PhoneCompanyInfo(string name, string phonenumber, string birth, string company)
            : base(name, phonenumber, birth)
        {
            this.company = company;
        }

        public override string ToString()
        {
            return base.ToString().Replace("생일 : ", "") + $"회사 : {this.company}";
        }
    }
}