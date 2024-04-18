

using Forum.API.DataObjects.Enums;

namespace Forum.API.DataObjects.Restrictions
{
    public class BanData
    {
        public int UserId { get; set; }
        public BanType BanType { get; set; }
        public TimeSpan? BanTime { get; set; }
    }
}
