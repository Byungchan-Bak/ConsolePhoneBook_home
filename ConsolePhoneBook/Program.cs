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
    class Program
    {
        static void Main(string[] args)
        {
            PhoneBookManager manager = PhoneBookManager.SingleTon();
            manager.PrintText();    //파일에 저장된 데이터 호출(파일이 있을 경우)
            int select;
            while (true)
            {
                manager.error[2] = true;
                while (manager.error[2])
                {
                    manager.ShowMenu();
                    try
                    {
                        select = int.Parse(Console.ReadLine());
                        switch (select)
                        {
                            case 1: manager.InputData(); manager.error[2] = false; break;
                            case 2: manager.ListData(); manager.error[2] = false; break;
                            case 3: manager.SearchData(); manager.error[2] = false; break;
                            case 4: manager.DeleteData(); manager.error[2] = false; break;
                            case 5: Console.WriteLine("\n프로그램을 종료합니다."); manager.SaveText(); return;
                            default: manager.ErrorList(); break;
                        }
                    }
                    catch (Exception err)
                    {
                        Console.WriteLine(err.Message);
                    }
                }
            }
        }
    }
}