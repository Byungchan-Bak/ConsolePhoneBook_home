using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePhoneBook
{
    public class PhoneBookManager
    {
        #region 싱글톤
        static PhoneBookManager instance;

        private PhoneBookManager() { }
        
        public static PhoneBookManager SingleTon()
        {
            if (instance == null)
                instance = new PhoneBookManager();

            return instance;
        }
        #endregion

        const int MAX_CNT = 100;
        PhoneInfo[] infoStorage = new PhoneInfo[MAX_CNT];   //전화번호 부 최대 100개
        int curCnt = 0; //현재 저장된 전화번호 수
        public bool[] error = new bool[3]; //오류 번호

        public void ShowMenu()
        {
            Console.WriteLine("\n*************************주소록*************************");
            Console.WriteLine("1. 입력  |  2. 목록  |  3. 검색  |  4. 삭제  |  5. 종료");
            Console.WriteLine("********************************************************");
            Console.Write("선택 -->> ");
        }   //전화번호부 시작화면 메서드

        public void InputData()
        {
            SaveData(InputMenu(), InputName(), InputNumber());
            this.curCnt++;
        }   //전화번호부 등록 메서드

        public void ListData()
        {
            int select;
            error[2] = true;

            Console.WriteLine("1. 기본  |  2. 오름차순(이름)  |  3. 오름차순(번호)");
            while (error[2])
            {
                Console.Write("선택 -->> ");
                try
                {
                    select = int.Parse(Console.ReadLine());
                    switch (select)
                    {
                        case 1:
                            for (int i = 0; i < curCnt; i++) { infoStorage[i].ShowInfo(); }
                            error[2] = false;
                            break;
                        case 2:
                            UpChartName();
                            error[2] = false;
                            break;
                        case 3:
                            UpChartNumber();
                            error[2] = false;
                            break;
                        default:
                            error[2] = true;
                            ErrorList();
                            break;
                    }
                }catch(Exception err)
                {
                    Console.WriteLine(err.Message);
                }
            }
        }   //전화번호부 보기 메서드

        public void SearchData()
        {
            Console.Write("찾을 번호 : ");
            string search_number = Console.ReadLine();
            int check_code = SearchNumber(search_number);
            //중복이 없을 경우
            if (check_code < 0) { Console.WriteLine("해당되는 데이터가 없습니다."); }
            else{ infoStorage[check_code].ShowInfo(); }
        }   //전화번호 찾기 메서드

        public void DeleteData()
        {
            string delete_number = "";
            this.error[0] = true;

            Console.WriteLine("\n※삭제할 정보를 입력하시오.※");
            while (this.error[0])
            {
                Console.Write("번호 : ");
                try
                {
                    delete_number = Console.ReadLine().Trim().Replace(" ", ""); //if(name == " " / if (name.Length < 1 / name.Equals(""))   //Trim() -->> 문자열 공백제거 : 문자열 사이의 공백제거(x)
                    //null또는 공백만 입력했을 경우
                    if (string.IsNullOrEmpty(delete_number))    { ErrorList(); }
                    else{ this.error[0] = false; }
                }
                catch (Exception err)
                {
                    Console.WriteLine(err.Message);
                }
            }

            int checkcode = SearchNumber(delete_number);
            if (checkcode < 0)   //중복이 없을 경우
            {
                Console.WriteLine("삭제할 데이터가 없습니다.");
            }
            else
            {
                for (int i = checkcode; i < curCnt; i++)
                {
                    infoStorage[i] = infoStorage[i + 1];    //데이터 삭제
                }
                curCnt--;   //저장된 전화번호 수 감소
                infoStorage[curCnt] = null; //마지막 데이터 삭제
            }
        }   //전화번호 삭제 메서드

        private int InputMenu()
        {
            int input_select = -1;
            this.error[2] = true;

            Console.WriteLine("1. 일반  |  2. 대학  |  3.회사");
            while (this.error[2])
            {
                Console.Write("선택 -->> ");
                try
                {
                    input_select = int.Parse(Console.ReadLine());
                    switch (input_select)
                    {
                        case 1:
                        case 2:
                        case 3:
                            this.error[2] = false;
                            break;
                        default:
                            ErrorList();
                            break;
                    }
                }
                catch (Exception err)
                {
                    Console.WriteLine(err.Message);
                }
            }
            return input_select;
        }   //입력 데이터 선택 메서드

        private string InputNumber()
        {
            string input_number = "";
            this.error[0] = true; this.error[1] = true;

            while (this.error[0] || this.error[1])
            {
                Console.Write("번호 : ");
                try
                {
                    input_number = Console.ReadLine().Trim(); //if(name == " " / if (name.Length < 1 / name.Equals(""))   //Trim() -->> 문자열 공백제거 : 문자열 사이의 공백제거(x)
                    //null또는 공백만 입력했을 경우
                    if (string.IsNullOrEmpty(input_number)) { ErrorList(); }
                    else{ this.error[0] = false; }

                    int checkcode = SearchNumber(input_number);
                    //중복일 경우
                    if (checkcode > -1) { ErrorList(); }
                    else{ this.error[1] = false; }
                }
                catch (Exception err)
                {
                    Console.WriteLine(err.Message);
                }
            }
            return input_number;
        }   //번호입력 메서드

        private string InputName()
        {
            string input_name = "";
            this.error[0] = true;

            while (this.error[0])
            {
                Console.Write("이름 : ");
                try
                {
                    //Trim() -->> 문자열 공백제거 : 문자열 사이의 공백제거(x)
                    //Replace -->> 문자열 변경 : " ", ""
                    //if(name == " " / if (name.Length < 1 / name.Equals(""))
                    input_name = Console.ReadLine().Trim();
                    //null또는 공백만 입력했을 경우
                    if (string.IsNullOrEmpty(input_name)){ ErrorList(); }
                    else{ this.error[0] = false; }
                }
                catch (Exception err)
                {
                    Console.WriteLine(err.Message);
                }
            }
            return input_name;
        }   //이름입력 메서드

        private void SaveData(int input_select, string input_name, string input_number)
        {
            string input_birth, input_univer, input_company;   //추가 데이터(생일, 대학, 회사)
            int input_year;

            input_birth = null;
            input_univer = null;
            input_company = null;

            switch (input_select)
            {
                case 1:
                    Console.Write("생일 : ");
                    input_birth = Console.ReadLine();
                    PhoneInfo telBook = new PhoneInfo(input_name, input_number, input_birth);
                    infoStorage[curCnt] = telBook;  //데이터 입력
                    break;

                case 2:
                    Console.Write("대학 : ");
                    input_univer = Console.ReadLine();
                    Console.Write("학년 : ");
                    int.TryParse(Console.ReadLine(), out input_year);
                    PhoneUnivInfo adduniver = new PhoneUnivInfo(input_name, input_number, input_birth, input_univer, input_year);
                    infoStorage[curCnt] = adduniver;
                    break;

                case 3:
                    Console.Write("회사 : ");
                    input_company = Console.ReadLine();
                    PhoneCompanyInfo addcom = new PhoneCompanyInfo(input_name, input_number, input_birth, input_company);
                    infoStorage[curCnt] = addcom;
                    break;
            }   //데이터 저장
        }   //데이터 저장 메서드

        private int SearchNumber(string search_number)  //파라미터 1 전화번호 찾기
        {
            for (int i = 0; i < curCnt; i++)
            {
                if (infoStorage[i].SearchNumber.Replace(" ", "").CompareTo(search_number) == 0) //기존데이터에 특정데이터가 있을 경우
                {
                    return i;
                }
            }
            return -1;
        }

        private void UpChartName()
        {
            PhoneInfo[] copyinfo = new PhoneInfo[this.curCnt];
            Array.Copy(infoStorage, copyinfo, curCnt);
            NameRange range = new NameRange();
            Array.Sort(copyinfo, range);
            for (int i = 0; i < curCnt; i++)
            {
                copyinfo[i].ShowInfo();
            }
        }   //이름 오름차순정렬

        private void UpChartNumber()
        {
            PhoneInfo[] copyinfo = new PhoneInfo[this.curCnt];
            Array.Copy(infoStorage, copyinfo, curCnt);
            NumberRange range = new NumberRange();
            Array.Sort(copyinfo, range);
            for (int i = 0; i < curCnt; i++)
            {
                copyinfo[i].ShowInfo();
            }
        }   //번호 오름차순정렬

        public void ErrorList()
        {
            if(this.error[0] == true){ throw new Exception("필수 입력사항입니다.!!");   }
            if(this.error[1] == true){ throw new Exception("이미 저장된 번호입니다.!!"); }
            if (this.error[2] == true) { throw new Exception("잘못 입력되었습니다. 다시 입력하시오.!!"); }
        }   //입력오류 리스트
    }
}
