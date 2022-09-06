using System;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static bool _stop = false;
        private static void MainThread()
        {
            Console.WriteLine("쓰레드 시작");
            while (_stop == false)
            {
                //누군가가 stop 신호를 해주기를 기다린다
            }
            Console.WriteLine("쓰레드 종료");
        }

        private static void Main(string[] args)
        {
            Task t = new Task(MainThread);
            t.Start();

            Thread.Sleep(1000);

            _stop = true;

            Console.WriteLine("stop 호출");
            Console.WriteLine("종료 대기중");
            t.Wait();
            Console.WriteLine("종료 완료");
        }
    }
}