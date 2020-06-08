using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models
{
    public class LockServiceDatabase : DbContext
    {
        public LockServiceDatabase(DbContextOptions<LockServiceDatabase> options)
            : base(options)
        {
            
        }
        
        public DbSet<HistoryData> HistoryItems { get; set; }
        public DbSet<Locks> LocksData { get; set; }
        public DbSet<Buildings> BuildingsData { get; set; }
        public DbSet<Groups> GroupsData { get; set; }
        public DbSet<Media> MediaData { get; set; }

        public void WriteData(SecurityData data)
        {
            foreach(Buildings bd in data.buildings){
                BuildingsData.Add(bd);
            }
            foreach(Locks lks in data.locks)
            {
                LocksData.Add(lks);
            }
            foreach(Groups gr in data.groups)
            {
                GroupsData.Add(gr);
            }
            foreach(Media md in data.media)
            {
                MediaData.Add(md);
            }
        }
    }
}