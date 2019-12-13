using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Medacademy.Models
{
    public class LevelsModel
    {
        public long LevelID { get; set; }

        [Required(ErrorMessage = "SubTopic Required")]
        public long SubTopicID { get; set; }

        [Required(ErrorMessage = "LevelName Required")]
        public string LevelName { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime Timestamp { get; set; }
        public TimeSpan timer { get; set; }

        public long CourseID { get; set; }
        public long PackageID { get; set; }
        public long SubjectID { get; set; }
        public long TopicID { get; set; }

    }
}