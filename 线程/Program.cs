using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 线程
{
    class Program
    {
        static void Main(string[] args)
        {
            //通过委托开启一个线程
            Func<string,int,string> a = Test;
            IAsyncResult ar=a.BeginInvoke("xiaohua",20,null,null); //开启一个新的线程执行a所引用的方法

            //IAsyncResult 可以获取当前线程的状态
            Console.WriteLine("main");

            //while (ar.IsCompleted==false)   //等待线程结束
            //{
            //    Console.WriteLine(".");
            //    Thread.Sleep(10);        
            //}
            //string res = a.EndInvoke(ar);
            //Console.WriteLine(res);

            //bool isEnd=ar.AsyncWaitHandle.WaitOne(1000); //1000毫秒表示超时时间，如果过了1000毫秒,返回false,不在进行等待，接着执行下面的代码如果子1000毫秒内执行完毕会立即返回true并接着执行下面的代码;

            //if (isEnd)
            //{
            //    string res = a.EndInvoke(ar);
            //    Console.WriteLine(res);
            //}


            Func<string, int, string> a1 = Test1;

            //倒数第二个参数是一个委托类型的参数，表示回掉函数，就是当线程结束的时候会调用这个委托
            //倒数第一个参数用来给回掉函数传递数据,这里的a,代表要将ar1传递到回到函数中
            /*
             * 相当于(item)=>{
                Console.WriteLine("线程结束");

                Func<string, int, string> a1 = ar.AsyncState as Func<string, int, string>;

                string res = a1.EndInvoke(ar);
                Console.WriteLine(res);

            }
            */
            IAsyncResult ar1 = a.BeginInvoke("xiaohua", 20, OnCallBack, a); //开启一个新的线程执行a所引用的方法

            //等价于
            IAsyncResult ar2 = a.BeginInvoke("xiaohua", 20, arr=> 
            {
                string res = a.EndInvoke(arr);
                Console.WriteLine(res);
            },null);


            Thread thread = new Thread(Test3);

            thread.Start("c://jjfk");

            MyThread th = new MyThread("xihua",20);

            new Thread(th.DownLoad).Start();


            Task task = new Task(ThreadTask);
            task.Start();

            TaskFactory tf = new TaskFactory();
            Task t=tf.StartNew(ThreadTask);


           


            Console.ReadKey();

        }

        static void DoFirst()
        {
            Console.WriteLine("开始第一个任务");
            Thread.Sleep(2000);

        }
        static void DoSecond()
        {
            Console.WriteLine("开始第一个任务");
            Thread.Sleep(2000);

        }
        static void ThreadTask()
        {
            Console.WriteLine("任务开始");
            Thread.Sleep(2000);
            Console.WriteLine("任务结束");

        }
        static void Test3(object path)
        {
            Console.WriteLine("线程开始"+Thread.CurrentThread.ManagedThreadId);

            Thread.Sleep(2000);
            Console.WriteLine("线程结束"+path );

        }
        static void OnCallBack(IAsyncResult ar)
        {
            Console.WriteLine("线程结束");

            Func<string, int, string> a1 = ar.AsyncState as Func<string, int, string>;

            string res= a1.EndInvoke(ar);
            Console.WriteLine(res);
        }
        static string Test(string name,int age)
        {
            Console.WriteLine("test");
            Thread.Sleep(900);
            return name + "," + age;
        }

        static string Test1(string name, int age)
        {
            Console.WriteLine("test");
            Thread.Sleep(900);
            return name + "," + age;
        }
    }
    public class MyThread
    {
        public string name;
        public int age;
        public MyThread(string name,int age)
        {
            this.name = name;
            this.age = age;          
        }

        public void DownLoad()
        {
            Console.WriteLine(name+","+age);
        }
    }
}
