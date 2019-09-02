using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace 反射
{
    class Program
    {
        static void Main(string[] args)
        {
            Peson peson = new Peson() { name="xiaowang",age=20,Heiget =160};

            Type type = peson.GetType();

            Console.WriteLine(type.Name);  //获取类型名

            Console.WriteLine(type.Namespace); //获取命名空间

            Console.WriteLine(type.Assembly);  //获取所在集合

            FieldInfo[] field=type.GetFields();   //获取所有的公共字段

            foreach (var item in field)
            {
                Console.WriteLine(item.Name);               
            }
            PropertyInfo[] proper = type.GetProperties();
            foreach (var item in proper)
            {
                Console.WriteLine(item.Name);
            }

            MethodInfo[] method = type.GetMethods();

            foreach (var item in method)
            {
                Console.WriteLine(item.Name);
            }

            Assembly assembly = peson.GetType().Assembly;

            Type[] types = assembly.GetTypes();  //获取集合中所有的类型

            FieldInfo name = type.GetField("name");

           

        }
    }

    class Peson
    {
        public string name;
        public int age;
        private string address;

        public float Heiget { set; get; }
        public void Test()
        {

        }
        public void Test1() { }        

        private void Test2() { }

    }
}
