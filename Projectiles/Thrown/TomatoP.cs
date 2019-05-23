using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Thrown
{
    public class TomatoP : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            projectile.width = 22;
            projectile.height = 22;
            projectile.friendly = true;
            projectile.thrown = true;
            projectile.penetrate = 1;
            projectile.aiStyle = 2;
            projectile.timeLeft = 600;
            aiType = 48;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tomato");
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (target.type == mod.NPCType("Azana"))
            {
                Item.NewItem((int)target.position.X, (int)target.position.Y, target.width, target.height, mod.ItemType("AzanaChibi"));
                Main.NewText("Hey! I just polished this armour!", new Color(93, 25, 43, 200));
            }
            else
            {
                Item.NewItem((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height, mod.ItemType("Tomato"));
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Item.NewItem((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height, mod.ItemType("Tomato"));
            return true;
        }
    }
}