using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Pets
{
    public class LilOrange : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.Penguin);
            projectile.height = 32;
            projectile.width = 28;
            aiType = ProjectileID.Penguin;
            Main.projFrames[projectile.type] = 3;
            Main.projPet[projectile.type] = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lil Orange");
        }
        public override bool PreAI()
        {
            Player player = Main.player[projectile.owner];
            player.penguin = false; // Relic from aiType
            return true;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (player.dead)
            {
                modPlayer.lilOrange = false;
            }
            if (modPlayer.lilOrange)
            {
                projectile.timeLeft = 2;
            }
        }
    }
}