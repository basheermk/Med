using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ma.EntityLibrary.Data
{
    public class Sp_GetUserResults : BaseReference
    {
        private sp_GetUserResults_Result status;
        public Sp_GetUserResults(sp_GetUserResults_Result obj) { status = obj; }

        public long UserId { get { return status.UserId; } }
        public string Name { get { return status.Name; } }
        public long ExamId { get { return status.ExamId; } }
        public Nullable<long> Examtime { get { return status.Examtime; } }
        public Nullable<double> Total { get { return status.Total; } }
    }
}
