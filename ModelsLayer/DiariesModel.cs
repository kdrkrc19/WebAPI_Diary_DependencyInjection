﻿namespace ModelsLayer
{
    public class DiariesModel
    {
        public int diaryId { get; set; }
        public string diary { get; set; }
        public DateTime date { get; set; }
        public DateTime updateDate { get; set; }
        public int userId { get; set; }
    }
}
