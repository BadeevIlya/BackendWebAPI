using Newtonsoft.Json;

namespace Badeev_L01
{
    [Serializable]
    public class Producer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        [JsonIgnore]
        public List<Film> Films { get; set; }

    }
}
