using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPGAmbientApp.Models
{
    public class AudioFile
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Nullable<int> FileLength { get; set; }
        public string FilePath { get; set; }
        public string ImgPath { get; set; }
        public string AdventureIdea { get; set; }
    }
}
