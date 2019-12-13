using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Ma.EntityLibrary;
using System.IO;
using Ma.EntityLibrary.Data;
using Ma.ClassLibrary.Utility;


namespace Ma.EntityLibrary.Data
{
    public class Dropdowndata :BaseReference
    {
        protected static MAEntities _Entities = new MAEntities();

        public static List<SelectListItem> GetCourseDrop()
        {
            MAEntities Entities = new MAEntities();
            var input = Entities.tb_Course.Where(z => z.IsActive).OrderByDescending(z => z.CourseId).ToList();
            return input.Select(x => new SelectListItem { Text = x.CourseName, Value = x.CourseId.ToString() }).ToList();
        }
        public static List<SelectListItem> GetPackageDrop(long courseid)
        {
            var input = _Entities.tb_Package.Where(z => z.Isactive && z.CourseID == courseid).OrderByDescending(z => z.PackageID).ToList();
            return input.Select(x => new SelectListItem { Text = x.Name, Value = x.PackageID.ToString() }).ToList();
        }
        public static List<SelectListItem> GetPackageDrop()
        {
            MAEntities Entities = new MAEntities();
            var input = Entities.tb_Package.Where(z => z.Isactive).OrderByDescending(z => z.PackageID).ToList();
            return input.Select(x => new SelectListItem { Text = x.Name, Value = x.PackageID.ToString() }).ToList();
        }
        public static List<SelectListItem> GetGroupDrop(long packageid)
        {
            MAEntities Entities = new MAEntities();
            var input = Entities.tb_Group.Where(z => z.Isactive && z.PackageID == packageid).OrderByDescending(z => z.Group_ID).ToList();
            return input.Select(x => new SelectListItem { Text = x.Groupname, Value = x.Group_ID.ToString() }).ToList();
        }
        public static List<SelectListItem> GetSubDrop()
        {
            MAEntities Entities = new MAEntities();
            var input = Entities.tb_Subjects.Where(z => z.Isactive).OrderByDescending(z => z.SubjectID).ToList();
            return input.Select(x => new SelectListItem { Text = x.SubjectName, Value = x.SubjectID.ToString() }).ToList();
        }

        public static List<SelectListItem> GetTopicDrop()
        {
            MAEntities Entities = new MAEntities();
            var input = Entities.tb_Topic.Where(z => z.IsActive).OrderByDescending(z => z.TopicID).ToList();
            return input.Select(x => new SelectListItem { Text = x.TopicName, Value = x.TopicID.ToString() }).ToList();
        }

        public static List<SelectListItem> GetSubtopics()
        {
            MAEntities Entities = new MAEntities();
            var input = Entities.tb_Subtopic.Where(z => z.Isactive).OrderByDescending(z => z.SubTopicID).ToList();
            return input.Select(x => new SelectListItem { Text = x.SubTopicName, Value = x.SubTopicID.ToString() }).ToList();
        }
        public static List<SelectListItem> GetSubDrop(long packageid)
        {
            MAEntities Entities = new MAEntities();
            var input = Entities.tb_Subjects.Where(z => z.Isactive && z.PackageID == packageid).OrderByDescending(z => z.SubjectID).ToList();
            return input.Select(x => new SelectListItem { Text = x.SubjectName, Value = x.SubjectID.ToString() }).ToList();
        }
        public static List<SelectListItem> GetTopicDrop(long subjectid)
        {
            MAEntities Entities = new MAEntities();
            var input = Entities.tb_Topic.Where(z => z.IsActive && z.SubjectID == subjectid).OrderBy(z => z.TopicID).ToList();
            return input.Select(x => new SelectListItem { Text = x.TopicName, Value = x.TopicID.ToString() }).ToList();
        }
        public static List<SelectListItem> GetSubtopics(long topicid)
        {
            MAEntities Entities = new MAEntities();
            var input = Entities.tb_Subtopic.Where(z => z.Isactive && z.TopicID == topicid).OrderBy(z => z.SubTopicID).ToList();
            return input.Select(x => new SelectListItem { Text = x.SubTopicName, Value = x.SubTopicID.ToString() }).ToList();
        }

        public static List<SelectListItem> GetLevels(long subtopicid)
        {
            MAEntities Entities = new MAEntities();
            var input = Entities.tb_Level.Where(z => z.IsActive && z.SubTopicID == subtopicid).OrderBy(z => z.SubTopicID).ToList();
            return input.Select(x => new SelectListItem { Text = x.LevelName, Value = x.LevelID.ToString() }).ToList();
        }

        public static List<SelectListItem> GetQuestionTypes()
        {
            MAEntities Entities = new MAEntities();
            var input = Entities.tb_QuestionType.Where(z => z.IsActive).OrderBy(q => q.QuestionTypeId).ToList();
            return input.Select(x => new SelectListItem { Text = x.QuestionType, Value = x.QuestionTypeId.ToString() }).ToList();
        }

        public static List<SelectListItem> GetAnswerTypes()
        {
            MAEntities Entities = new MAEntities();
            var input = Entities.tb_AnswerType.Where(z => z.IsActive).OrderBy(q => q.AnswerTypeId).ToList();
            return input.Select(x => new SelectListItem { Text = x.AnswerType, Value = x.AnswerTypeId.ToString() }).ToList();
        }
        public static List<SelectListItem> GetGroup()
        {
            var input = _Entities.tb_Group.Where(z => z.Isactive).OrderBy(z => z.Group_ID).ToList();
           // var xx = input.Select(x => new SelectListItem { Text = x.Groupname, Value = x.Group_ID.ToString() }).ToList();
            

            List<DropGroup> list = new List<DropGroup>();
            foreach (var item in input)
            {
                DropGroup one = new DropGroup();
                one.GroupName = item.Groupname;
                one.GroupID = Convert.ToInt32( item.Group_ID);
                list.Add(one);
            }

           
            DropGroup dropgrp = new DropGroup();
            dropgrp.GroupID = 0;
            dropgrp.GroupName = "All";
            list.Insert(0, dropgrp);
            return list.Select(x => new SelectListItem { Text = x.GroupName, Value = x.GroupID.ToString() }).ToList();
        }
        public static List<SelectListItem> GetGroupaddstudent()
        {
            var input = _Entities.tb_Group.Where(z => z.Isactive).OrderBy(z => z.Group_ID).ToList();
            // var xx = input.Select(x => new SelectListItem { Text = x.Groupname, Value = x.Group_ID.ToString() }).ToList();


            List<DropGroup> list = new List<DropGroup>();
            foreach (var item in input)
            {
                DropGroup one = new DropGroup();
                one.GroupName = item.Groupname;
                one.GroupID = Convert.ToInt32(item.Group_ID);
                list.Add(one);
            }


            //DropGroup dropgrp = new DropGroup();
            //dropgrp.GroupID = 0;
            //dropgrp.GroupName = "All";
            //list.Insert(0, dropgrp);
            return list.Select(x => new SelectListItem { Text = x.GroupName, Value = x.GroupID.ToString() }).ToList();
        }
        //sibi

        public class Genders
        {
            public string Gender { get; set; }
            public int Id { get; set; }
        }

        public static List<Genders> Gender = new List<Genders>()
        {
           new Genders { Gender  = "Male" , Id = 1},
           new Genders { Gender  = "Female" , Id = 2}
        };

        public static List<SelectListItem> GetGender()
        {
            return Gender.Select(x => new SelectListItem { Text = x.Gender, Value = x.Gender }).ToList();
        }

        public static List<SelectListItem> GetGroupforstudentedit()
        {
            var input = _Entities.tb_Group.Where(z => z.Isactive).OrderBy(z => z.Group_ID).ToList();
            return input.Select(x => new SelectListItem { Text = x.Groupname, Value = x.Group_ID.ToString() }).ToList();
        }
        public class Levels
        {
            public string Level { get; set; }
            public int Id { get; set; }
        }

        public static List<Levels> Level = new List<Levels>()
        {
           new Levels { Level  = "Level 1" , Id = 1},
           new Levels { Level  = "Level 2" , Id = 2},
           new Levels { Level  = "Level 3" , Id = 3}
        };

        public static List<SelectListItem> GetLevels()
        {
            return Level.Select(x => new SelectListItem { Text = x.Level, Value = x.Level }).ToList();
        }


    }
}
