using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ma.EntityLibrary.Data
{
    public class Topic : BaseReference
    {

        private tb_Topic Topics;
        public Topic() { }
        public Topic(tb_Topic crs) { Topics = crs; }
        public Topic(long topicid) { Topics = _Entities.tb_Topic.FirstOrDefault(x => x.TopicID == topicid); }
        public long TopicID { get { return Topics.TopicID; } }

        public long SubjectID { get { return Topics.SubjectID; } }

        public string TopicName { get { return Topics.TopicName; } }
        public long CourseID { get { return Topics.CourseID; } }
        public long PackageID { get { return Topics.PackageID; } }
        public List<Topic> GetTopics()
        {
            return _Entities.tb_Topic.Where(x => x.IsActive == true).ToList().Select(q => new Topic(q)).OrderByDescending(c => c.TopicID).ToList();
        }

        public List<Topic> GetUserviewTopics(long subjectid)
        {
            return _Entities.tb_Topic.Where(x => x.IsActive == true && x.SubjectID== subjectid).ToList().Select(q => new Topic(q)).OrderBy(c => c.TopicID).ToList();
        }

        
    }
}
