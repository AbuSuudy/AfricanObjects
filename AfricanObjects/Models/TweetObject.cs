using System.Collections.Generic;

namespace AfricanObjects.Models
{
    public class MuseumObject
    {
        public string Title { get; set; }
        public string Location { get; set; }
        public string Culture { get; set; }
        public string objectDate { get; set; }
        public string objectURL { get; set; }
        public List<string> objectImage { get; set; }
        public string Country { get; set; }
        public string Source { get; set; }
    }

    public class Media
    {
        public List<string> media_ids { get; set; }
    }

    public class MediaTweet
    {
        public string text { get; set; }
        public Media media { get; set; }
    }
}
