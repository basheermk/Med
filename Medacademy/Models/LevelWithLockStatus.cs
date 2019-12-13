using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Medacademy.Models
{
    public class LevelWithLockStatus
    {
        
            public string Level { get; set; }
            public string LevelName { get; set; }
            public long LevelId { get; set; }
            public long Subtopic { get; set; }
            public int LockStatus { get; set; }
           
       
    }
}