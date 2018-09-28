
using Trails4Health.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Trails4Health.Data
{
    public class UsersSeedData
    {
        // 5. (b.d.AUTENTICAÇÃO) 
        // este metodo vai ser chamado no controlador AcountController.cs
        public static async Task EnsurePopulatedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            const string adminName = "Admin";
            const string adminPass = "Secret123$";
            const string professorName = "professor";
            const string professorPass = adminPass;
            const string turistaName = "turista";
            const string turistaPass = adminPass;

            if (!await roleManager.RoleExistsAsync("Administrador"))
            {
                await roleManager.CreateAsync(new IdentityRole("Administrador"));
            }

            if (!await roleManager.RoleExistsAsync("Turista"))
            {
                await roleManager.CreateAsync(new IdentityRole("Turista"));
            }

            if (!await roleManager.RoleExistsAsync("Professor"))
            {
                await roleManager.CreateAsync(new IdentityRole("Professor"));
            }

            ApplicationUser admin = await userManager.FindByNameAsync(adminName);

            // se não encontra admin na tabela users que foi criada automaticamente
            if (admin == null)
            {
                admin = new ApplicationUser { UserName = adminName };
                await userManager.CreateAsync(admin, adminPass);
            }

            // adicionadas linhas:
            if (!await userManager.IsInRoleAsync(admin, "Administrador"))
            {
                await userManager.AddToRoleAsync(admin, "Administrador");
            }

            // role turista
            ApplicationUser turista = await userManager.FindByNameAsync(turistaName);
            if (turista == null)
            {
                turista = new ApplicationUser { UserName = turistaName };
                await userManager.CreateAsync(turista, turistaPass);
            }

            if (!await userManager.IsInRoleAsync(turista, "Turista"))
            {
                await userManager.AddToRoleAsync(turista, "Turista");
            }

            // role professor
            ApplicationUser professor = await userManager.FindByNameAsync(professorName);
            if (professor == null)
            {
                professor = new ApplicationUser { UserName = professorName };
                await userManager.CreateAsync(professor, professorPass);
            }

            if (!await userManager.IsInRoleAsync(professor, "Professor"))
            {
                await userManager.AddToRoleAsync(professor, "Professor");
            }
        }
    }
}
