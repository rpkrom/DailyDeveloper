namespace BlazorWebApp.Server.Data
{
    public class DataContext : DbContext
    {      
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Article> Articles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>().ToTable("Articles");
        }

    }
}
