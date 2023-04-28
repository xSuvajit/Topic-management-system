namespace TMS.DTO
{
    public class Topic
    {
        public int Id { get; set; }
        public string MyTopics { get; set; }
        public string Status { get; set; }
        public int topicCode { get; set; }
        public string createdBy { get; set; }
        public DateTime created { get; set; }
        public string modifiedBy { get; set; }
        public DateTime modified { get; set; }
    }
}
