namespace BPM.Organization
{
    public class Office
    {
        public Office()
        {
        }

        public string Name { get; set; }

        public Company Company { get; set; }

        public Location Location { get; set; }
    }
}