
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ExamWork
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public bool IsConfirmed { get; set; }
    }
}