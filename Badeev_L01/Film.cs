using Newtonsoft.Json;

namespace Badeev_L01
{
    [Serializable]
    public class Film
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; } = string.Empty;
        public double Rating { get; set; }
        public string Description { get; set; } = string.Empty;
        public int ProducerId { get; set; }
        [JsonIgnore]
        public Producer Producer { get; set; }
        

    }
}
