using Microsoft.EntityFrameworkCore;

namespace OpenGMT 
{
    public class OpenGMTContext :  DbContext 
    {
        public DbSet<Creature> Creatures { get; set; }
        public DbSet<Map> Maps { get; set; }
        public DbSet<Scene> Scenes { get; set; }
        public DbSet<Session> Session { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<MapMarker> MapMarker { get; set; }

        public OpenGMTContext(DbContextOptions<OpenGMTContext> options) : base(options)
        {   
        }
    }
}
