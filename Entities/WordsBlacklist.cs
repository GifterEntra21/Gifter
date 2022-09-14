using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public static class WordsBlacklist
    {
        static WordsBlacklist()
        {
            // Blacklist of words to ignore when getting the tags from the images
            Blacklist = new List<string> 
            {"human face", "person", "portrait", "chin", "man", "forehead", "neck", "eyebrow", "human", "text", "wall",
             "clothing", "font", "smile", "facial hair", "wearing", "white", "electric blue", "bed sheet", "bedroom",
             "room", "fan", "screenshot", "standing", "ground", "sleeve", "people", "phone", "holding", "cellphone",
             "hood", "sitting", "wrist", "sky", "water", "woman", "women", "men", "outdoor", "indoor"};
        }
        public static List<string> Blacklist { get; set; }
    }
}
