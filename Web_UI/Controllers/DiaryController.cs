using BusinessLayer;
using DataAccessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Web_UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiaryController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IDiary _diary;
       
        public DiaryController(IConfiguration configuration, IDiary diary)
        {
            _configuration = configuration;
            _diary = diary;
        }

        [HttpGet("get-diary/")]
        public List<Diaries> GetDiary(int inputUserId)
        {                
            List<Diaries> diaries = new List<Diaries>();
            diaries = _diary.GetDiaryData(inputUserId);
        
            return diaries;
        }


        [HttpGet("get-all-diaries/{inputUserId}")]
        public List<Diaries> GetAllDiaries(int inputUserId)
        {
            List<Diaries> diaryDataList = new List<Diaries>();
            diaryDataList = _diary.GetAllDiaries(inputUserId);

            return diaryDataList;
        }

        [HttpPost("add-diary")]
        public IActionResult AddDiary([FromBody] Diaries diary)
        {
            if (diary == null) return BadRequest("Invalid Data");

            _diary.AddDiary(diary);

            return Ok("Diary Added Successfully");
        }


        [HttpPut("update-diary/{diaryId}")]
        public IActionResult UpdateDiary(int diaryId, [FromBody] Diaries diary)
        {
            if (diary == null) return BadRequest("Invalid Data");

            bool isUpdated = _diary.UpdateDiary(diaryId, diary);

            if (isUpdated) return Ok("Diary Updated Successfully");
            else return NotFound("Diary not found");
        }


        [HttpDelete("delete-diary/{diaryId}")]
        public IActionResult DeleteDiary(int diaryId, int userId)
        {
            bool isDeleted = _diary.DeleteDiary(diaryId, userId);

            if (isDeleted) return Ok("Diary Deleted Successfully");
            else return NotFound("Diary not found");

        }

        [HttpDelete("delete-all-diarys{userId}")]
        public IActionResult DeleteAllDiary(int userId)
        {
            bool isDeletedAll = _diary.DeleteAllDiaries(userId);

            if (isDeletedAll) return Ok("All Diarys Deleted Succesfully");
            else return NotFound("Diary Not Found.");
        }

    }
}
