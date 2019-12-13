using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ma.EntityLibrary.Data
{
    public class PreparationResult : BaseReference
    {
        private tb_UserPreparationResult PreResult;
        public PreparationResult() { }
        public PreparationResult(tb_UserPreparationResult crs) { PreResult = crs; }
        public PreparationResult(long preresultid) { PreResult = _Entities.tb_UserPreparationResult.FirstOrDefault(x => x.Id == preresultid); }

        public int Id { get { return PreResult.Id; } }
        public long UserId { get { return PreResult.UserId; } }
        public long PreparationId { get { return PreResult.PreparationId; } }
        public double Mark { get { return PreResult.Mark; } }
        public long Time { get { return PreResult.Time; } }

        public List<PreparationResult> GetUserPreparationResult(long userid)
        {
            return _Entities.tb_UserPreparationResult.Where(x => x.UserId == userid).ToList().Select(q => new PreparationResult(q)).OrderByDescending(c => c.PreparationId).ToList();
        }

    }
}
