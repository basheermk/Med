using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ma.EntityLibrary.Data
{
    public class News : BaseReference
    {
        private tb_News news;
        public News() { }
        public News(tb_News crs) { news = crs; }
        public News(long newsid) { news = _Entities.tb_News.FirstOrDefault(x => x.NewsID == newsid); }

        public int NewsID { get { return news.NewsID; } }
        public string Head { get { return news.Head; } }
        public string SubHead { get { return news.SubHead; } }
        public string Newsdetils { get { return news.News; } }
        public System.DateTime NewsDate { get { return news.NewsDate; } }
        public bool IsActive { get { return news.IsActive; } }
        public System.DateTime TimeStamp { get { return news.TimeStamp; } }
        public string Image { get { return news.Image; } }

        public List<News> Getnews()
        {
            var xx = _Entities.tb_News.Where(x => x.IsActive == true).OrderByDescending(c => c.NewsID).ToList().Select(q => new News(q)).ToList();
            return xx;
        }
    }
}
