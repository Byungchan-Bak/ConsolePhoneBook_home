using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePhoneBook
{
    [Serializable]
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

        HashSet<PhoneInfo> infoStorage = new HashSet<PhoneInfo>();
        readonly string fileName = "PhoneBookList.dat"; //저장될 파일

        public bool[] error = new bool[4]; //오류 번호

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
                            baseList();
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
                            ErrorList();
                            break;
                    }
                }
                catch (Exception err)
                {
                    Console.WriteLine(err.Message);
                }
            }
        }   //전화번호부 보기 메서드

        public void SearchData()
        {
            Console.Write("찾을 번호 : ");
            PhoneInfo searchInfo = SearchNumber();
            if (searchInfo == null) { Console.WriteLine("해당되는 데이터가 없습니다."); }
            else { searchInfo.ToString(); }
        }   //전화번호 찾기 메서드

        public void DeleteData()
        {
            this.error[0] = true;

            Console.WriteLine("\n※삭제할 정보를 입력하시오.※");

            Console.Write("번호 : ");
            try
            {
                PhoneInfo delete = SearchNumber();
                if (delete == null)
                { Console.WriteLine("삭제할 데이터가 없습니다."); }
                else
                { infoStorage.Remove(delete); }
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
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
            string number = "";
            this.error[0] = true; this.error[1] = true;

            while (this.error[0] || this.error[1])
            {
                Console.Write("번호 : ");
                try
                {
                    number = Console.ReadLine().Trim(); //if(name == " " / if (name.Length < 1 / name.Equals(""))   //Trim() -->> 문자열 공백제거 : 문자열 사이의 공백제거(x)
                    //null또는 공백만 입력했을 경우
                    if (string.IsNullOrEmpty(number)) { ErrorList(); }
                    else { this.error[0] = false; }

                    //중복일 경우
                    if (SearchNumber(number)) { ErrorList(); }
                    else { this.error[1] = false; }
                }
                catch (Exception err)
                {
                    Console.WriteLine(err.Message);
                }
            }
            return number;
        }   //번호입력 메서드

        private string InputName()
        {
            string name = "";
            this.error[0] = true;

            while (this.error[0])
            {
                Console.Write("이름 : ");
                try
                {
                    //Trim() -->> 문자열 공백제거 : 문자열 사이의 공백제거(x)
                    //Replace -->> 문자열 변경 : " ", ""
                    //if(name == " " / if (name.Length < 1 / name.Equals(""))
                    name = Console.ReadLine().Trim();
                    //null또는 공백만 입력했을 경우
                    if (string.IsNullOrEmpty(name)) { ErrorList(); }
                    else { this.error[0] = false; }
                }
                catch (Exception err)
                {
                    Console.WriteLine(err.Message);
                }
            }
            return name;
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
                    infoStorage.Add(new PhoneInfo(input_name, input_number, input_birth));
                    break;

                case 2:
                    Console.Write("대학 : ");
                    input_univer = Console.ReadLine();
                    Console.Write("학년 : ");
                    int.TryParse(Console.ReadLine(), out input_year);
                    infoStorage.Add(new PhoneUnivInfo(input_name, input_number, input_birth, input_univer, input_year));
                    break;

                case 3:
                    Console.Write("회사 : ");
                    input_company = Console.ReadLine();
                    infoStorage.Add(new PhoneCompanyInfo(input_name, input_number, input_birth, input_company));
                    break;
            }   //데이터 저장
        }   //데이터 저장 메서드

        private PhoneInfo SearchNumber()  //번호 찾기
        {
            string number = Console.ReadLine().Trim().Replace(" ", "");

            foreach (PhoneInfo seach in infoStorage)
            {
                if (number.CompareTo(seach.RangeNumber) == 0)
                { return seach; }
            }
            return null;
        }

        private bool SearchNumber(string searchNumber)  //입력 시 번호가 이미 입력되어 있을 경우 찾기
        {
            foreach (PhoneInfo seach in infoStorage)
            {
                if (seach.RangeNumber.Equals(searchNumber))
                { return true; }
            }
            return false;
        }

        private void baseList()
        {
            foreach (PhoneInfo print in infoStorage)
            { Console.WriteLine(print.ToString()); }
        }

        private void UpChartName()
        {
            List<PhoneInfo> info = new List<PhoneInfo>(infoStorage);
            info.Sort(new NameRange());
            foreach (PhoneInfo print in info)
            { Console.WriteLine(print.ToString()); }
        }   //이름 오름차순정렬

        private void UpChartNumber()
        {
            List<PhoneInfo> info = new List<PhoneInfo>(infoStorage);
            info.Sort(new NumberRange());
            foreach (PhoneInfo print in info)
            { Console.WriteLine(print.ToString()); }
        }   //번호 오름차순정렬

        public void ErrorList()
        {
            if (this.error[0] == true) { throw new Exception("필수 입력사항입니다.!!"); }
            if (this.error[1] == true) { throw new Exception("이미 저장된 번호입니다.!!"); }
            if (this.error[2] == true) { throw new Exception("잘못 입력되었습니다. 다시 입력하시오.!!"); }
            if (this.error[3] == true) { throw new Exception("이미 저장된 데이터입니다.!!"); }
        }   //입력오류 리스트

        public void SaveText()
        {
            try
            {
                using (Stream pbFile = new FileStream(fileName, FileMode.Create))   //파일생성
                {
                    BinaryFormatter listType = new BinaryFormatter();   //파일 타입 -->> 바이너리 타입
                    listType.Serialize(pbFile, infoStorage);    //객체 직렬화
                }
            }catch(Exception err)
            { Console.WriteLine(err.Message); }
        }

        public void PrintText()
        {
            try
            {
                if (File.Exists(fileName))   //파일이 있을 경우 실행
                {
                    using (Stream pbFile = new FileStream(fileName, FileMode.Open)) //오픈할 파일
                    {
                        BinaryFormatter listType = new BinaryFormatter();   //파일 타입
                        infoStorage = (HashSet<PhoneInfo>)listType.Deserialize(pbFile); //파일 내용을 배열로 형변환
                    }
                }
            }catch(Exception err)
            { Console.WriteLine(err.Message); }
        }
    }
}