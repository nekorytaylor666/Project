using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamWork
{
    class Program
    {
        static void Main(string[] args)
        {


            ISmsSender smsSender = new MessageSender();
            smsSender.SendMessage("+77783973990", "Hello world!");
        }
    }
}
