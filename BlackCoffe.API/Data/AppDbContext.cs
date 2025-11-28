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
            new IdentityRole
            {
                Id = "0b44ca04-f6b0-4a8f-a953-1f2330d30894",
                Name = "Administrador",
                NormalizedName = "ADMINISTRADOR"
            },
            new IdentityRole
            {
                Id = "ddf093a6-6cb5-4ff7-9a64-83da34aee005",
                Name = "Cliente",
                NormalizedName = "CLIENTE"
            }
        ];

        builder.Entity<IdentityRole>().HasData(roles);
        #endregion

        #region Populate Usuário
        List<Usuario> usuarios =
        [
            new Usuario
            {
                Id = "ddf093a6-6cb5-4ff7-9a64-83da34aee005",
                Email = "lucas2469y@gmail.com",
                NormalizedEmail = "LUCAS2469Y@GMAIL.COM",
                UserName = "lucas2469y@gmail.com",
                NormalizedUserName = "LUCAS2469Y@GMAIL.COM",
                LockoutEnabled = true,
                EmailConfirmed = true,
                Nome = "Lucas de Souza Santos",
                DataNascimento = new DateTime(1997, 6, 27),
                Foto = "/img/usuarios/avatar.png"
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
            new IdentityUserRole<string>
            {
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
            new Categoria { Id = 1, Nome = "Cafés Especiais"},
            new Categoria { Id = 2, Nome = "Cápsulas"},
            new Categoria { Id = 3, Nome = "Grãos"},
            new Categoria { Id = 4, Nome = "Acessórios"},
            new Categoria { Id = 5, Nome = "Doces e acompanhamentos"}
        ];

        builder.Entity<Categoria>().HasData(categorias);
    }

    private static void SeedProdutoPadrao(ModelBuilder builder)
    {
        List<Produto> produtos =
        [

            // Categoria 1 - Cafés Especiais
            new Produto
            {
                Id = 1,
                CategoriaId = 1,
                Nome = "Café Cerrado Mineiro 500g",
                Descricao = "Café especial do Cerrado Mineiro, com aroma intenso e notas achocolatadas.",
                Qtde = 100,
                ValorCusto = 29.90m,
                ValorVenda = 20.90m,
                Destaque = true,
                Foto = "/img/produtos/especiais/6.jpg"
            },

            new Produto
            {
                Id = 2,
                CategoriaId = 1,
                Nome = "Café Moído Antigua",
                Descricao = "Café moído de sabor marcante, com torra média e aroma persistente.",
                Qtde = 80,
                ValorCusto = 32.00m,
                ValorVenda = 29.00m,
                Destaque = true,
                Foto = "/img/produtos/especiais/5.jpg"
            },

            new Produto
            {
                Id = 3,
                CategoriaId = 1,
                Nome = "Café Moído Kopi Luwak",
                Descricao = "Um dos cafés mais raros do mundo, com sabor extremamente suave e complexo.",
                Qtde = 80,
                ValorCusto = 32.00m,
                ValorVenda = 22.00m,
                Destaque = true,
                Foto = "/img/produtos/especiais/8.jpg"
            },

             // Categoria 1 - Cápsulas
            new Produto
            {
                Id = 4,
                CategoriaId = 2,
                Nome = "Cápsulas de Café Kopi Luwak",
                Descricao = "Café raro com sabor suave e complexo, notas de caramelo e chocolate, produzido a partir dos grãos ingeridos e excretados pela civeta.",
                Qtde = 80,
                ValorCusto = 30.00m,
                ValorVenda = 19.90m,
                Destaque = true,
                Foto = "/img/produtos/capsulas/1.jpg"
            },

            new Produto
            {
                Id = 5,
                CategoriaId = 2,
                Nome = "Cápsulas de Café Cerrado Mineiro",
                Descricao = "Café suave e equilibrado, com notas de nozes, chocolate e frutas secas. Cultivado na região do Cerrado Mineiro, é uma opção premium.",
                Qtde = 80,
                ValorCusto = 22.00m,
                ValorVenda = 19.90m,
                Destaque = true,
                Foto = "/img/produtos/capsulas/2.jpg"
            },           

             new Produto
                {
                    Id = 6,
                    CategoriaId = 2,
                    Nome = "Cápsulas de Café Especial Mineiro",
                    Descricao = "Café de sabor rico com notas de frutas vermelhas, cacau e caramelo, cultivado nas montanhas de Minas Gerais.",
                    Qtde = 80,
                    ValorCusto = 30.00m,
                    ValorVenda = 23.90m,
                    Destaque = true,
                    Foto = "/img/produtos/capsulas/3.jpg"
                },

             new Produto
                {
                    Id = 7,
                    CategoriaId = 2,
                    Nome = "Cápsulas de Café Bourbon",
                    Descricao = "Café refinado, com notas de frutas tropicais e chocolate amargo. Sabor suave e levemente adocicado.",
                    Qtde = 80,
                    ValorCusto = 32.00m,
                    ValorVenda = 24.90m,
                    Destaque = true,
                    Foto = "/img/produtos/capsulas/4.jpg"
            },

            // Categoria 3 - Grãos
            new Produto
            {
                Id = 8,
                CategoriaId = 3,
                Nome = "Café Premium em Grãos",
                Descricao = "Grãos selecionados com torra especial e sabor encorpado.",
                Qtde = 100,
                ValorCusto = 35.00m,
                ValorVenda = 29.90m,
                Destaque = true,
                Foto = "/img/produtos/graos/19.jpg"
            },

            new Produto
            {
                Id = 9,
                CategoriaId = 3,
                Nome = "Café Jamaica Blue Mountain",
                Descricao = "Café de origem jamaicana, extremamente aromático e equilibrado.",
                Qtde = 80,
                ValorCusto = 32.00m,
                ValorVenda = 25.00m,
                Destaque = true,
                Foto = "/img/produtos/graos/15.jpg"
            },

            new Produto
            {
                Id = 10,
                CategoriaId = 3,
                Nome = "Café Bourbon em Grãos",
                Descricao = "Café Bourbon de alta qualidade, com sabor doce e baixa acidez.",
                Qtde = 80,
                ValorCusto = 32.00m,
                ValorVenda = 22.00m,
                Destaque = false,
                Foto = "/img/produtos/graos/14.jpg"
            },

            // Categoria 4 - Acessórios
            new Produto
            {
                Id = 11,
                CategoriaId = 4,
                Nome = "Cafeteira Premium",
                Descricao = "Cafeteira premium com acabamento refinado e alta durabilidade.",
                Qtde = 50,
                ValorCusto = 600.00m,
                ValorVenda = 549.90m,
                Destaque = true,
                Foto = "/img/produtos/acessorios/1.jpg"
            },

            new Produto
            {
                Id = 12,
                CategoriaId = 4,
                Nome = "Cafeteira Elétrica",
                Descricao = "Cafeteira elétrica moderna, ideal para uso diário.",
                Qtde = 40,
                ValorCusto = 499.50m,
                ValorVenda = 399.00m,
                Destaque = false,
                Foto = "/img/produtos/acessorios/4.jpg"
            },

            new Produto
            {
                Id = 13,
                CategoriaId = 4,
                Nome = "Cafeteira para Expresso",
                Descricao = "Máquina especial para preparo de cafés espresso.",
                Qtde = 60,
                ValorCusto = 520.00m,
                ValorVenda = 499.00m,
                Destaque = false,
                Foto = "/img/produtos/acessorios/3.jpg"
            },

            new Produto
            {
                Id = 14,
                CategoriaId = 4,
                Nome = "Mini Cafeteira Espresso",
                Descricao = "Design moderno e compacto, perfeito para qualquer espaço.",
                Qtde = 50,
                ValorCusto = 699.90m,
                ValorVenda = 549.90m,
                Destaque = true,
                Foto = "/img/produtos/acessorios/2.jpg"
            }

        ];

        builder.Entity<Produto>().HasData(produtos);
    }
}
