using DAL.Models;

namespace StreamServer.Database.Models
{
    public class StreamInformation : Entity
    {
        public string AbbrName { get; set; } = string.Empty;

        public Guid Identifier { get; set; }

        public bool IsLive { get; set; }

        public int SubjectId { get; set; }
        public virtual Subject Subject { get; set; }

        public string StreamPath { get; set; } = string.Empty;

        public string Password { get; set; }
    }
}
