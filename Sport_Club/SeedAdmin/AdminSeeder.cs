namespace Sport_Club.SeedAdmin
{
    public class AdminSeeder
    {
        public static async Task SeedAdmin(UserManager<ApplicationUser> userManager)
        {
            string email = "Wael999@sgmail.com";
            string password = "AdminWael999!";

            if (await userManager.FindByEmailAsync(email) == null)
            {
                var admin = new ApplicationUser
                {
                    UserName = "Wael",
                    Email = email,
                    FullName = "Wael Gamiel"
                };

                var result = await userManager.CreateAsync(admin, password);

                if (result.Succeeded)
                    await userManager.AddToRoleAsync(admin, "Admin");
            }
        }
    }

}
