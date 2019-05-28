 namespace HeMaNe.Web.Database.Models
{
    public class Team
    {
        public int Id { get; set; }
        public virtual Club Club { get; set; }
        public virtual League League { get; set; }
    }
}
