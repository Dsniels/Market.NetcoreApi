using Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Data
{
    public class SeguridadDbContextData
    {

        public static async Task SeedUserAsync(UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager)
        {

            if (!userManager.Users.Any())
            {
                var usuario = new Usuario()
                {
                    Nombre = "Daniel",
                    Apellido = "Salazar",
                    UserName = "Dasa",
                    Email = "dsnielsalazarr@outlook.com",
                    Direccion = new Direccion()
                    {
                        Calle = "Foresta 131",
                        Ciudad = "Monterrey",
                        CodigoPostal = "282929",
                        Departamento = "Mty",
                    }
                };
                await userManager.CreateAsync(usuario, "DanielSalazar271278$");

            }

            if(!roleManager.Roles.Any())
            {
                var role = new IdentityRole
                {
                    Name = "ADMIN"
                };
                await roleManager.CreateAsync(role);
            }


        }



    }
}
