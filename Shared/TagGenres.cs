namespace Shared
{
    public static class TagGenres
    {

        public static GenreWithTags SportTags = new GenreWithTags
        {
            GenreName = "sports",
            Tags = {"soccer", "football", "gym", "player", "sports uniform", "sportswear",
            "field", "athletic game", "sport venue", "ball game", "stadium", "soccer player", "team sport", "championship", "baseball",
            "tournament", "artificial turf", "soccer-specific stadium", "football player", "ball", "sports equipment"}
        };

        public static GenreWithTags AnimeTags = new GenreWithTags 
        {
            GenreName = "anime",
            Tags = {"anime", "manga", "fiction", "cartonn", "drawing", "art", "animation",
            "animated cartoon", "illustration", "fictional character", "comic","comics", "toy", "collection", "book", "hair coloring",
            "novel", "bookcase", "figure", "animal figure", "baby toys", "fashion accessory", "costume", "cosplay", "asian", "poster" }
        };

        public static GenreWithTags ExotericTags = new GenreWithTags
        {
            GenreName = "exoteric",
            Tags = {"plant", "garden", "housplant", "flowerpot",
            "flower", "art", "candle", "animal", "grass", "lady", "christmas", "candle holder", "tree", "still life",
            "floral design", "sunflower", "forest", "nature", "meadow", "insect", "star"}
        };

        public static GenreWithTags FashionTags = new GenreWithTags
        {
            GenreName = "fashion",
            Tags = {"fashion", "lip", "lady", "fashion accessory",
            "lipstick", "makeover", "photo shoot", "eyelash", "fashion model", "necklace", "model", "earrings",
            "lipgloss", "cosmetics", "mascara", "eye liner", "jewellery", "art model", "eye shadow", "fabric",
            "pattern", "textile", "vacation", "travel", "body piercing", "fashion design", "posing"}
        };

        public static GenreWithTags GamerTags = new GenreWithTags 
        {
            GenreName = "gamer",
            Tags = {"television", "television set", "computer monitor",
            "output device", "eletronics", "computer", "eletronic device", "computer keyboard", "desktop computer",
            "personal computer", "display device", "computer desk", "media", "computer hardware", "screen", "headphones",
            "audio equipment", "toy", "peripheral", "gadget", "earphone", "laptop", "netbook", "touchpad" }
        };

        public static GenreWithTags CarTags = new GenreWithTags
        {
            GenreName = "car",
            Tags = {"vehicle", "land vehicle", "car", "race car", "race", "wheel", "tire",
            "parking", "parked", "auto part", "transport", "automotive", "automotive lighting", "car seat", "drive", "driver", "driving", "automotive design",
            "car seat cover", "road", "sports sedan", "sedan", "coupe", "hatch", "sports coupe", "sports car", "automotive parking light",
            "parking lot", "garage", "automotive wheel system", "headlamp", "automobile repair shop", "engine", "motor", "automotive light bulb",
            "vehicle door", "windshield", "performance car", "supercar", "hypercar", "bmw", "city car", "motor vehicle", "mechanic", "automotive mechanic" }
        };

        public static List<GenreWithTags> GenresList { get { return new List<GenreWithTags> { SportTags, AnimeTags, ExotericTags, FashionTags,
                                                                                              GamerTags, CarTags }; } }
    }
}
