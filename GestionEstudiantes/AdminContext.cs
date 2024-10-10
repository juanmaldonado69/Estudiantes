using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GestionEstudiantes

{
    public class AdminContext : IdentityDbContext
    {
        public AdminContext(DbContextOptions options): base(options) 
        { }

    }
}
