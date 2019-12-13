using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ma.ClassLibrary.Utility;

namespace Ma.EntityLibrary.Data
{
    public class Level : BaseReference
    {
        private tb_Level Levels;
        public Level() { }
        public Level(tb_Level crs) { Levels = crs; }
        public Level(long levelid) { Levels = _Entities.tb_Level.FirstOrDefault(x => x.LevelID == levelid); }


        public long LevelID { get { return Levels.LevelID; } }
        public long SubTopicID { get { return Levels.SubTopicID; } }
        public string LevelName { get { return Levels.LevelName; } }
        public System.DateTime Timestamp { get { return Levels.Timestamp; } }

        public int? OrderValue { get { return Levels.orderValue; } }
        public TimeSpan Duration { get { return Levels.Duration; } }

        public long CourseID { get { return Levels.CourseID; } }
        public long PackageID { get { return Levels.PackageID; } }
        public long SubjectID { get { return Levels.SubjectID; } }
        public long TopicID { get { return Levels.TopicID; } }

        public List<Level> GetLevels()
        {
            return _Entities.tb_Level.Where(x => x.IsActive == true).ToList().Select(q => new Level(q)).OrderByDescending(c => c.LevelID).ToList();
        }
        public List<Level> GetLevels(long levelid)
        {
            return _Entities.tb_Level.Where(x => x.IsActive == true && x.LevelID== levelid).ToList().Select(q => new Level(q)).OrderByDescending(c => c.LevelID).ToList();
        }
        public List<Question> GetQuestions()
        {
            var xxx = _Entities.tb_Question.Where(z => z.LevelID == Levels.LevelID && z.IsActive).ToList().Select(q => new Question(q)).ToList();
            return xxx;
        }
        public List<LevelWithLockStatus> GetUserviewLevel(long subtopicid, long userid, int lockid, int userpaidlockid)//to get level with unlocked status for all user
        {
            var levels = _Entities.tb_Level.Where(x => x.IsActive == true && x.SubTopicID == subtopicid).ToList().Select(q => new Level(q)).OrderBy(c => c.OrderValue).ToList();

            List<LevelWithLockStatus> LevelList = new List<LevelWithLockStatus>();
            foreach (var lvls in levels)
            {
                LevelWithLockStatus Leveltest = new LevelWithLockStatus();
                Leveltest.LevelName = lvls.LevelName;
                Leveltest.LevelID = lvls.LevelID;
                if (lockid == 1)
                {
                    if (userpaidlockid == 1)
                    {
                        Leveltest.firstlockid = lockid;
                        Leveltest.LockStatus = 0;
                        Leveltest.paidfirstlockid = userpaidlockid;
                    }
                    else
                    {
                        Leveltest.firstlockid = lockid;
                        Leveltest.LockStatus = 0;
                        Leveltest.paidfirstlockid = 0;
                    }
                }
                else
                {
                    Leveltest.paidfirstlockid = 0;
                    Leveltest.firstlockid = 0;
                    Leveltest.LockStatus = _Entities.tb_Level.SelectMany(z => z.tb_UnlockedLevel).Any(z => z.LevelId == lvls.LevelID && z.IsActive && z.UserId == userid) ? 0 : 1; //return 1 if it is unlocked level
                    
                }

              
                LevelList.Add(Leveltest);
                lockid++;
            }
            return LevelList;
        }
        public List<LevelWithLockStatus> GetUserviewLevelcommon(long subtopicid, int lockid) //to get level with unlocked status for all visitors
        {
            var levels = _Entities.tb_Level.Where(x => x.IsActive == true && x.SubTopicID == subtopicid).ToList().Select(q => new Level(q)).OrderBy(c => c.OrderValue).ToList();

            List<LevelWithLockStatus> LevelList = new List<LevelWithLockStatus>();
            foreach (var lvls in levels)
            {
                LevelWithLockStatus Leveltest = new LevelWithLockStatus();
                Leveltest.LevelName = lvls.LevelName;
                Leveltest.LevelID = lvls.LevelID;
                Leveltest.Subtopic  = lvls.SubTopicID;
                Leveltest.OrderValue = lvls.OrderValue;
                if (lockid == 1)
                {
                    Leveltest.firstlockid = lockid;
                }
                else
                {
                    Leveltest.firstlockid = 0;
                }       
                LevelList.Add(Leveltest);
                lockid++;
            }
            return LevelList;
        }

        public List<PracticeAttempt> GetPracticeAtempt(long userId)
        {
            return _Entities.Database.SqlQuery<PracticeAttempt>(string.Format("SELECT * FROM [tb_PracticeAttempt] WHERE [Levelid] = {0} AND [UserId] = {1}  ORDER BY [Id] DESC ", Levels.LevelID, userId)).ToList();
        }
        public List<Question> GetPracticeTestQuestions()
        {
            return _Entities.tb_Question.Where(z => z.LevelID == Levels.LevelID && z.IsActive).ToList().Select(q => new Question(q)).ToList();
        }
        public List<Question> GetunpaidPracticeTestQuestions(long levelid)
        {
            return _Entities.tb_Question.Where(z => z.LevelID == levelid && z.IsActive).ToList().Select(q => new Question(q)).ToList();
        }


    }
}
