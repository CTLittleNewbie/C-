using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CShaparTest
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Person> persons = new List<Person>() {

                new Person(){ name="xiaohua",age=18,height =160f,weight=80f,xueli="xiaoxue"},
                new Person(){ name="xiaowang",age=19,height =166f,weight=90f,xueli="chuzhong"},
                new Person(){ name="xiaoxue",age=20,height =169f,weight=99f,xueli="gaozhong"},
                new Person(){ name="xiaoming",age=18,height =170f,weight=102f,xueli ="daxue"},
                new Person(){ name="xiaohuang",age=119,height =159f,weight=75f,xueli="yanjiusheng"},
                new Person(){ name="xiaosha",age=17,height =180f,weight=120f,xueli="boshisheng"},
            };

            List<XueLi> xueli = new List<XueLi>()
            {

                new XueLi() {xueli="xiaoxue",year =4},
                new XueLi() { xueli = "chuzhong" ,year=9},
                new XueLi() { xueli = "gaozhong",year=13 },
                new XueLi() { xueli = "daxue" ,year=17},
                new XueLi() { xueli = "yanjiusheng",year=20 },
                new XueLi() { xueli = "boshisheng",year=23 },
                new XueLi() { xueli = "boshihou" ,year=26},
            };
            var res = from item in persons
                      where item.age > 18
                      select item.name;

            foreach (string item in res)
            {
                Console.WriteLine(item);
            }

            var res1=persons.Where<Person>(item => item.age > 18);

            foreach (Person item in res1)
            {
                Console.WriteLine(item.name);
            }

            //根据学历查询每个人都了多少年的书

            for (int i = 0; i < persons.Count; i++)
            {
                for (int j = 0; j < xueli.Count; j++)
                {
                    if (persons[i].xueli == xueli[j].xueli)
                    {
                        Console.WriteLine(persons [i].name+" "+xueli[j].year);
                    }
                }
            }

            //使用linq联合查询

            var res2 = from i in persons
                       from j in xueli
                       where i.xueli == j.xueli
                       select i.name + "" + j.year;

            foreach (string item in res2)
            {
                Console.WriteLine(item);
            }

            var res3 = persons.SelectMany(p=>xueli,(p,k)=>new {aaa=p,bbb=k}).Where(x=>x.aaa.xueli==x.bbb.xueli);

            foreach (var item in res3)
            {
                Console.WriteLine(item);
            }

            //排序

            var res4 = from i in persons
                       orderby i.age,i.height //多字段排序。先按照age排序，如果age相同，然后再按照height排序
                       select i;

            foreach (var item in res4)
            {
                Console.WriteLine(item);
            }

            var res5=persons.OrderBy(m => m.age).ThenBy(m=>m.height);  //多字段排序。先按照age排序，如果age相同，然后再按照height排序

            foreach (var item in res5)
            {
                Console.WriteLine(item);
            }
            //join on 集合联合

            var res6 = from j in persons
                       join k in xueli on j.xueli equals k.xueli
                       where j.age > 18
                       select new { aaa = j, bbb = k };
            foreach (var item in res6)
            {
                Console.WriteLine(item);
            }


            //输出各种学历的人都有多少人，并排序

            var res7 = from i in xueli
                       join j in persons on i.xueli equals j.xueli
                       into groups
                       orderby groups.Count()
                       select new { x=i.xueli ,count=groups.Count(),p=groups};

            foreach (var item in res7)
            {
                for (int i = 0; i < item.count; i++)
                {
                    Console.WriteLine(item.x + "," + item.count + item.p.ElementAt(i));
                }
            }

            //对自身属性进行分组

            var res8 = from i in persons
                       group i by i.age
                       into g
                       select new {age=g.Key,count=g.Count()};  //g.key表示按照那个属性分组
            foreach (var item in res8)
            {
                Console.WriteLine(item);
            }

            bool b=persons.Any(m=>m.xueli =="xiaoxue");//判断是否存在满足某个条件的

            bool b1=persons.All(m => m.xueli == "chuzhong"); //判断是否都满足某个条件
        }
    }

    class Person
    {
        public string name;
        public int age;
        public float height;
        public float weight;
        public string xueli;

        public override string ToString()
        {
            return name + "," + age + "," + height + "," + weight + "," + xueli;
        }
    }

    class XueLi
    {
        public string xueli;

        public int year;
        public override string ToString()
        {
            return xueli+","+year;
        }
    }
}
