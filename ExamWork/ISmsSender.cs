using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamWork
{
    interface ISmsSender
    {

            void SendMessage(string numTo, string content);
    }
}
