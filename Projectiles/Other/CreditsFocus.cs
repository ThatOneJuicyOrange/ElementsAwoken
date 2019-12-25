using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Other
{
    public class CreditsFocus : ModProjectile
    {
        public override string Texture { get { return "ElementsAwoken/Projectiles/Blank"; } }
        public override void SetDefaults()
        {
            projectile.width = 2;
            projectile.height = 2;
            projectile.alpha = 255;
            projectile.timeLeft = 60000;
            projectile.light = 100;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("");
        }
        public override void AI()
        {
            if (!MyWorld.credits) projectile.Kill();
            projectile.Center = Main.screenPosition + new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);
        }      
    }
}