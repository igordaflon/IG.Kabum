using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KBM.Identidade.API.Data;

public class AplicacaoDbContext : IdentityDbContext
{
    public AplicacaoDbContext(DbContextOptions<AplicacaoDbContext> options) : base(options){}
}
