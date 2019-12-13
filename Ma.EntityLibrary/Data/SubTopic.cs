using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ma.ClassLibrary.Utility;

namespace Ma.EntityLibrary.Data
{
   public class SubTopic : BaseReference
    {
        private tb_Subtopic SubTopics;
        public SubTopic() { }
        public SubTopic(tb_Subtopic crs) { SubTopics = crs; }
        public SubTopic(long subtopicid) { SubTopics = _Entities.tb_Subtopic.FirstOrDefault(x => x.SubTopicID == subtopicid); }
        public long SubTopicID { get { return SubTopics.SubTopicID; } }

        public long TopicID { get { return SubTopics.TopicID; } }

        public string SubTopicName { get { return SubTopics.SubTopicName; } }

        public string Pdfpath { get { return SubTopics.Pdfpath; } }
        public string Videopath { get { return SubTopics.Videopath; } }

        public string YouTubeVideo { get { return SubTopics.YouTubeVideo; } }
        public List<SubTopic> GetTopics()
        {
            return _Entities.tb_Subtopic.Where(x => x.Isactive == true).ToList().Select(q => new SubTopic(q)).OrderByDescending(c => c.SubTopicID).ToList();
        }

        public List<SubTopic> GetSubtopic(long topicid)
        {
            return _Entities.tb_Subtopic.Where(x => x.Isactive == true && x.TopicID == topicid).ToList().Select(q => new SubTopic(q)).OrderBy(c => c.SubTopicID).ToList();
        }

        public List<SubTopic> GetSubtopicusingsubid(long subtopicid)
        {
            return _Entities.tb_Subtopic.Where(x => x.Isactive == true && x.SubTopicID == subtopicid).ToList().Select(q => new SubTopic(q)).OrderBy(c => c.SubTopicID).ToList();
        }




    }
}
