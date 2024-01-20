namespace API_asp_start_project.Domain.Pagings
{
    public class OwnerParameters : QueryStringParameters
    {
        public OwnerParameters()
        {
            OrderBy = "Name";
        }
        public uint MinYearOfBirth { get; set; }
        public uint MaxYearOfBirth { get; set; } = (uint)DateTime.Now.Year;

        public bool ValidYearRange => MaxYearOfBirth > MinYearOfBirth;

        public string Name { get; set; }
    }
}
