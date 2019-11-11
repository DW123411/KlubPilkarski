using KlubPilkarski.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KlubPilkarski.Startup))]
namespace KlubPilkarski
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            utworzRoleIUzytkownikow();
        }

        public void utworzRoleIUzytkownikow()
        {
            ApplicationDbContext kontekst = new ApplicationDbContext();
            var menedzerRol = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(kontekst));
            var menedzerUzytkownikow = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(kontekst));

            if (!menedzerRol.RoleExists("Administrator"))
            {
                var rola = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                rola.Name = "Administrator";
                menedzerRol.Create(rola);

                var uzytkownik = new ApplicationUser();
                uzytkownik.UserName = "Administrator";
                uzytkownik.Email = "administrator@mail.com";

                string haslo = "zaq1@WSX";

                var sprUzytkownika = menedzerUzytkownikow.Create(uzytkownik, haslo);
                if (sprUzytkownika.Succeeded)
                {
                    var wynik = menedzerUzytkownikow.AddToRole(uzytkownik.Id, "Administrator");
                }
            }

            if (!menedzerRol.RoleExists("PracownikKlubu"))
            {
                var rola = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                rola.Name = "PracownikKlubu";
                menedzerRol.Create(rola);
            }

            if (!menedzerRol.RoleExists("Trener"))
            {
                var rola = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                rola.Name = "Trener";
                menedzerRol.Create(rola);
            }
        }
    }
}
