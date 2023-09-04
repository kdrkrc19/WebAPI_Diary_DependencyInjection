using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface IDiary
    {
        public List<Diaries> GetAllDiaries(int userId);
        public List<Diaries> GetDiaryData(int userId);
        public bool AddDiary(Diaries diary);
        public bool UpdateDiary(int diaryId, Diaries diary);
        public bool DeleteDiary(int diaryId, int userId);
        public bool DeleteAllDiaries(int userId);
        
    }
}
