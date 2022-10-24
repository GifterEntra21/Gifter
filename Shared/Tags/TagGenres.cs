namespace Shared
{
    public static class TagGenres
    {
        static TagGenres()
        {
            sportTags = new GenreWithTags()
            {
                GenreName = "sport",
                Tags = new List<string>(){"soccer", "football", "gym", "player", "sports uniform", "sportswear",
            "field", "athletic game", "sport venue", "ball game", "stadium", "soccer player", "team sport", "championship", "baseball",
            "tournament", "artificial turf", "soccer-specific stadium", "football player", "ball", "sports equipment"}
            };


            animeTags = new GenreWithTags
            {
                GenreName = "anime",
                Tags = new List<string>(){"anime", "manga", "fiction", "cartoon", "drawing", "art", "animation",
            "animated cartoon", "illustration", "fictional character", "comic","comics", "toy", "collection", "book", "hair coloring",
            "novel", "bookcase", "figure", "animal figure", "baby toys", "fashion accessory", "costume", "cosplay", "asian", "poster" }
            };

            exotericTags = new GenreWithTags
            {
                GenreName = "exoteric",
                Tags = new List<string>(){"plant", "garden", "housplant", "flowerpot",
            "flower", "art", "candle", "animal", "grass", "lady", "christmas", "candle holder", "tree", "still life", "floral design", "sunflower", "forest", "nature", "meadow", "insect", "star", "drawing", "art"}

            };

            fashionTags = new GenreWithTags
            {
                GenreName = "fashion",
                Tags = new List<string>(){"fashion", "lip", "lady", "fashion accessory",
            "lipstick", "makeover", "photo shoot", "eyelash", "fashion model", "necklace", "model", "earrings",
            "lipgloss", "cosmetics", "mascara", "eye liner", "jewellery", "art model", "eye shadow", "fabric",
            "pattern", "textile", "vacation", "travel", "body piercing", "fashion design", "posing"}
            };

            gamerTags = new GenreWithTags
            {
                GenreName = "gamer",
                Tags = new List<string>(){"television", "television set", "computer monitor",
            "output device", "eletronics", "computer", "eletronic device", "computer keyboard", "desktop computer",
            "personal computer", "display device", "computer desk", "media", "computer hardware", "screen", "headphones",
            "audio equipment", "toy", "peripheral", "gadget", "earphone", "laptop", "netbook", "touchpad" }
            };

        }


        private static GenreWithTags sportTags;

        private static GenreWithTags animeTags;

        private static GenreWithTags exotericTags;

        private static GenreWithTags fashionTags;

        private static GenreWithTags gamerTags;

        public static List<GenreWithTags> GenresList { get { return new List<GenreWithTags> { sportTags, animeTags, exotericTags,fashionTags,gamerTags }; } }
    }
}
