

namespace Forum.API.DataObjects.TopicObjects
{
    public class Attachment
    {
        public int Id { get; set; }
        public string File { get; set; }
        public int PostId { get; set; }
    }
}
