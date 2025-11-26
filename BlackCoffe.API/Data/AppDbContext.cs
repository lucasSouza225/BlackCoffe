 using BlackCoffe.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlackCoffe.API.Data;

public class AppDbContext : IdentityDbContext<Usuario>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        SeedUsuarioPadrao(builder);
        SeedCategoriaPadrao(builder);
        SeedProdutoPadrao(builder);
    }

    private static void SeedUsuarioPadrao(ModelBuilder builder)
    {
        #region Populate Roles - Perfis de Usuário
        List<IdentityRole> roles =
        [
            new IdentityRole() {
               Id = "0b44ca04-f6b0-4a8f-a953-1f2330d30894",
               Name = "Administrador",
               NormalizedName = "ADMINISTRADOR"
            },
            new IdentityRole() {
               Id = "ddf093a6-6cb5-4ff7-9a64-83da34aee005",
               Name = "Cliente",
               NormalizedName = "CLIENTE"
            },
        ];
        builder.Entity<IdentityRole>().HasData(roles);
        #endregion

        #region Populate Usuário
        List<Usuario> usuarios = [
            new Usuario(){
                Id = "ddf093a6-6cb5-4ff7-9a64-83da34aee005",
                Email = "lucas2469y@gmail.com",
                NormalizedEmail = "LUCAS2469Y@GMAIL.COM",
                UserName = "lucas2469y@gmail.com",
                NormalizedUserName = "LUCAS2469Y@GMAIL.COM",
                LockoutEnabled = true,
                EmailConfirmed = true,
                Nome = "Lucas de Souza Santos",
                DataNascimento = DateTime.Parse("1997-06-27"),
                Foto = "/img/usuarios/avatar.jpg"
            }
        ];
        foreach (var user in usuarios)
        {
            PasswordHasher<Usuario> pass = new();
            user.PasswordHash = pass.HashPassword(user, "123456");
        }
        builder.Entity<Usuario>().HasData(usuarios);
        #endregion

        #region Populate UserRole - Usuário com Perfil
        List<IdentityUserRole<string>> userRoles =
        [
            new IdentityUserRole<string>() {
                UserId = usuarios[0].Id,
                RoleId = roles[0].Id
            }
        ];
        builder.Entity<IdentityUserRole<string>>().HasData(userRoles);
        #endregion
    }

    private static void SeedCategoriaPadrao(ModelBuilder builder)
    {
        List<Categoria> categorias =
        [
         new Categoria { Id = 1, Nome = "Cafés Especiais" },
            new Categoria { Id = 2, Nome = "Cápsulas" },
            new Categoria { Id = 3, Nome = "Grãos" },
            new Categoria { Id = 4, Nome = "Acessórios" },
            new Categoria { Id = 5, Nome = "Doces e acompanhamentos" }   
        ];
        builder.Entity<Categoria>().HasData(categorias);
    }

    private static void SeedProdutoPadrao(ModelBuilder builder)
    {
        List<Produto> produtos =
        [
            // Categoria 1 - Cafés em Grãos
        new Produto { Id = 1, CategoriaId = 1, Nome = "Café em Grãos Arábica Premium", Descricao = "Grãos 100% arábica torrados artesanalmente, com aroma intenso e notas de chocolate.", Qtde = 50, ValorCusto = 30.00m, ValorVenda = 45.90m, Destaque = true, Foto = "/img/produtos/1.jpg" },
        new Produto { Id = 2, CategoriaId = 1, Nome = "Café em Grãos Gourmet 500g", Descricao = "Blend equilibrado com torra média e acidez suave.", Qtde = 40, ValorCusto = 25.00m, ValorVenda = 39.50m, Destaque = false, Foto = "/img/produtos/1.jpg" },
        new Produto { Id = 3, CategoriaId = 1, Nome = "Café em Grãos Espresso Forte", Descricao = "Café encorpado, ideal para máquinas de espresso.", Qtde = 60, ValorCusto = 27.00m, ValorVenda = 42.00m, Destaque = false, Foto = "/img/produtos/1.jpg" },

        // Categoria 2 - Cafés Moídos
        new Produto { Id = 4, CategoriaId = 2, Nome = "Café Moído Tradicional 500g", Descricao = "Café com moagem fina, pronto para coadores e cafeteiras elétricas.", Qtde = 100, ValorCusto = 20.00m, ValorVenda = 29.90m, Destaque = true, Foto = "moido-tradicional.jpg" },
        new Produto { Id = 5, CategoriaId = 2, Nome = "Café Moído Extra Forte", Descricao = "Blend intenso com sabor marcante e aroma persistente.", Qtde = 80, ValorCusto = 22.00m, ValorVenda = 32.00m, Destaque = false, Foto = "/img/produtos/1.jpg" }  
        ];
        builder.Entity<Produto>().HasData(produtos);
    }

}
