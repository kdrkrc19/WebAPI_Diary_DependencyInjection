using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class Diaries
    {
        public int diaryId { get; set; }
        public string diary { get; set; }
        public DateTime date { get; set; }
        public DateTime updateDate { get; set; }
        public int userId { get; set; }

        public Diaries(int diaryId, string diary, DateTime date, DateTime updateDate, int userId)
        {
            this.diaryId = diaryId;
            this.diary = diary;
            this.date = date;
            this.updateDate = updateDate;
            this.userId = userId;
        }
    }
}
