using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PokemonKotas.Data.Models;

namespace PokemonKotas.Data.Context
{
    public class MasterPokemonDbContext : DbContext
    {
        public DbSet<MasterPokemon> MasterPokemons { get; set; } = null!;
        public DbSet<CapturedPokemon> CapturedPokemons { get; set; } = null!;
        public DbSet<PokemonAbility> PokemonAbilities { get; set; } = null!;
        public DbSet<PokemonEvolution> PokemoEvolutions { get; set; } = null!;
        public DbSet<PokemonSprite> PokemonSprite { get; set; } = null!;
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=app.db");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<MasterPokemon>(x =>
            {
                x.HasKey(y => y.Id);
                x.Property(p => p.Id).ValueGeneratedOnAdd();
                x.HasMany<CapturedPokemon>().WithOne(y => y.MasterPokemon);
            });

            builder.Entity<CapturedPokemon>(x =>
            {
                x.HasKey(y => y.Id);
                x.HasMany<PokemonSprite>(y => y.Sprites).WithOne(y => y.CapturedPokemon);
                x.HasMany<PokemonAbility>(y => y.Abilities).WithOne(y => y.CapturedPokemon);
                x.HasMany<PokemonEvolution>(y => y.Evolutions).WithOne(y => y.CapturedPokemon);
                x.HasOne<MasterPokemon>(y => y.MasterPokemon).WithMany(y => y.CapturedPokemons);
            });
            builder.Entity<PokemonAbility>(x =>
            {
                x.HasKey(y => y.Id);
                x.HasOne<CapturedPokemon>(y => y.CapturedPokemon);
            });
            builder.Entity<PokemonEvolution>(x =>
            {
                x.HasKey(y => y.Id);
                x.HasMany<PokemonSprite>(y => y.Sprites).WithOne(y => y.PokemonEvolution);
            });

            builder.Entity<PokemonSprite>(x =>
            {
                x.HasKey(y => y.Id);
                x.HasOne<CapturedPokemon>(y => y.CapturedPokemon);
                x.HasOne<PokemonEvolution>(y => y.PokemonEvolution);
            });
            base.OnModelCreating(builder);
        }
    }
}
