using Ma.EntityLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Medacademy.Repository
{
    public class UserRepository
    {
        MAEntities _Entities = new MAEntities();
        public object UploadFiles(HttpPostedFileBase fileupload,string dirFullPath)
        {
            string fileName = null;
            if (fileupload != null)
            {
                
                if (!Directory.Exists(dirFullPath))
                {
                    Directory.CreateDirectory(dirFullPath);
                }

                //var getSubtopic = _Entities.tb_Login.Max(x => x.UserId);
                //if (getSubtopic == null)
                //{
                //    getSubtopic = 0;
                //}
                //long subtopicNum = getSubtopic;
                //subtopicNum = subtopicNum + 1;
                //string subNum = subtopicNum.ToString();
                
                string Extention = Path.GetExtension(fileupload.FileName);
                //fileName = string.Format("{0:yyyy-MM-dd_hh-mm-ss-tt}" + Extention, DateTime.Now);

                 fileName = Guid.NewGuid().ToString() + Extention;


                string path = dirFullPath  + fileName;



               //int fileSize = fileupload.ContentLength;
               // int Size = fileSize / 100;
                fileupload.SaveAs(path);                

            }
            return fileName;
        }

        

    }
}