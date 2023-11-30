using Forum.API.BL.Configuration.Interfaces;

namespace Forum.API.BL.Configuration
{
    public class FileValidationConfiguration : IFileValidationConfiguration
    {
        public int MaxSize {  get; set; }
        public List<string> Extensions { get; set; }
    }
}
