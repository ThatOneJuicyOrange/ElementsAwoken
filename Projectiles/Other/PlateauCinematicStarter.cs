using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Other
{
    public class PlateauCinematicStarter : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            projectile.width = 2;
            projectile.height = 2;
            projectile.alpha = 255;
            projectile.timeLeft = 600;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("plateau");
        }
        public override void AI()
        {
            projectile.ai[0]++;
            if (projectile.ai[0] > 120)
            {
                MyPlayer modPlayer = Main.LocalPlayer.GetModPlayer<MyPlayer>();

                modPlayer.plateauCinematic = true;
                projectile.Kill();
            }
        }      
    }
}