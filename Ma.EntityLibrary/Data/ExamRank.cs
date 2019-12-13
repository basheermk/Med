using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ma.EntityLibrary.Data
{
   public   class ExamRank :BaseReference
    {
        private tb_ExamRank Exams;
        public ExamRank() { }
        public ExamRank(tb_ExamRank crs) { Exams = crs; }
        public ExamRank(long exammarkid) { Exams = _Entities.tb_ExamRank.FirstOrDefault(x => x.ExamMarkId == exammarkid); }
        public long ExamMarkId { get { return Exams.ExamMarkId; } }
        public long ExamId { get { return Exams.ExamId; } }
        public long UserId { get { return Exams.UserId; } }
        public string UserName { get { return Exams.UserName; } }
        public long Examtime { get { return Exams.Examtime; } }
        public double Total { get { return Exams.Total; } }
        public long Rank { get { return Exams.Rank; } }
    }
}
