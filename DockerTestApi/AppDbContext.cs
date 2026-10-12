using DockerTestApi.Entity;
using Microsoft.EntityFrameworkCore;

namespace DockerTestApi;

public class AppDbContext(DbContextOptions<AppDbContext>  options): DbContext(options)
{
    public DbSet<User>  Users { get; set; }
}