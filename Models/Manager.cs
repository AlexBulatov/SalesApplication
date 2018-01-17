namespace NEB.Models
{
    public class Manager
    {
        public Manager(int? ID)
        {
            this.ID = ID;
        }

        public int? ID { get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }

        public static bool operator !=(Manager l, Manager r)
        {
            if (l is null || r is null) return true;
            return (l.FirstName!=r.FirstName || l.LastName!=r.LastName);
        }

        public static bool operator ==(Manager l, Manager r)
        {
            return !(l == r);
        }
    }
}