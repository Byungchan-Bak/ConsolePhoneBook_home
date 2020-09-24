using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePhoneBook
{
    class Program
    {
        static void Main(string[] args)
        {
            PhoneBookManager manager = new PhoneBookManager();
            int select;
            while (true)
            {
                manager.error[2] = true;
                while (manager.error[2])
                {
                    try
                    {
                        manager.ShowMenu();
                        select = int.Parse(Console.ReadLine());
                        switch (select)
                        {
                            case 1: manager.InputData(); manager.error[2] = false; break;
                            case 2: manager.ListData(); manager.error[2] = false; break;
                            case 3: manager.SearchData(); manager.error[2] = false; break;
                            case 4: manager.DeleteData(); manager.error[0] = false; break;
                            case 5: Console.WriteLine("\n프로그램을 종료합니다."); return;
                            default: manager.error[2] = true; manager.ErrorList(); break;
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
