using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.API.BL.Configuration.Interfaces
{
    public interface IFileValidationConfiguration
    {
        public int MaxSize { get; set; }
        public List<string> Extensions { get; set; }
    }
}
