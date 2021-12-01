namespace AfricanObjects.Models
{

    public class TwitterImage
    {
        public string image_type { get; set; }
        public int w { get; set; }
        public int h { get; set; }
    }

    public class TwitterResponse
    {
        public long media_id { get; set; }
        public string media_id_string { get; set; }
        public string media_key { get; set; }
        public int size { get; set; }
        public int expires_after_secs { get; set; }
        public TwitterImage image { get; set; }
    }


}
