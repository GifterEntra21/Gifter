using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public static class BanishedTags
    {
        static BanishedTags()
        {
            // Banished tags to ignore when getting the tags from the images
            BanishedTagsList = new List<string> 
            {"human face", "person", "portrait", "chin", "man", "forehead", "neck", "eyebrow", "human", "text", "wall",
             "clothing", "font", "smile", "facial hair", "wearing", "white", "electric blue", "bed sheet", "bedroom",
             "room", "fan", "screenshot", "standing", "ground", "sleeve", "people", "phone", "holding", "cellphone",
             "hood", "sitting", "wrist", "sky", "water", "woman", "women", "men", "outdoor", "indoor", "girl", "boy",
             "kid", "child", "hair"};
        }
        public static List<string> BanishedTagsList { get; set; }
    }
}
