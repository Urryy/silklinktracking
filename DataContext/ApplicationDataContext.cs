using cargosiklink.Extensions;
using cargosiklink.Models;
using cargosiklink.Models.Const;
using Microsoft.EntityFrameworkCore;

namespace cargosiklink.DataContext;

public class ApplicationDataContext : DbContext
{
	public DbSet<User> Users { get; set; }
    public DbSet<NumberTrack> NumberTracks { get; set; }
    public DbSet<State> States { get; set; }


	public ApplicationDataContext(DbContextOptions<ApplicationDataContext> opt) : base(opt)
	{
		Database.EnsureCreated();
	}
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
        modelBuilder.Entity<User>(builder =>
        {
            builder.HasData(new User("jobandprojects@gmail.com", HashPasswordHelper.HashPassword("zxKlasd1jvSn"), "jobandprojects@gmail.com", "+7827312513", "admin", "Moscow", Models.Enum.Roles.admin));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Password).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(1000).IsRequired();
            builder.Property(x => x.Password).HasMaxLength(1000).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(1000).IsRequired();
            builder.Property(x => x.Phone).HasMaxLength(1000).IsRequired();
            builder.Property(x => x.UserCode).HasMaxLength(1000).IsRequired();
            builder.Property(x => x.City).HasMaxLength(1000).IsRequired();
            builder.Property(x => x.Role).IsRequired();
        });

        modelBuilder.Entity<NumberTrack>(builder =>
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasOne(r => r.User);
            builder.HasOne(r => r.State);

            builder.Property(x => x.NumberTrackCode).IsRequired();
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.Weight).IsRequired();
            builder.Property(x => x.Volume).IsRequired();
            builder.Property(x => x.Link).IsRequired();
        });

        modelBuilder.Entity<State>(builder =>
        {
            builder.HasKey(x => x.Id);
            builder.HasData(new List<State>
            {
                new State(StateConst.StateAddByUser,"Добавлено пользователем", Models.Enum.StateType.AddByUser, "#fff"),
                new State(StateConst.StateDestinationCountry, "Отправлено в страну назначения", Models.Enum.StateType.DestinationCountry, "#e0ffff"),
                new State(StateConst.StateArrived ,"Прибыло в страну назначения", Models.Enum.StateType.Arrived, "#90ee90")
            });
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Type).IsRequired();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Color).IsRequired();
        });
    }
}

