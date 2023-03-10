namespace Badeev_L01
{
    public class CreateFilmDto
    {
        public string Name { get; set; } = "Name";
        public string Genre { get; set; } = string.Empty;
        public double Rating { get; set; } = 0.0;
        public string Description { get; set; } = string.Empty;
    }
}
