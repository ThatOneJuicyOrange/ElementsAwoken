using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Projectiles.Thrown
{
    public class TomatoP : ModProjectile
    {
    	
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
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
                int item = Item.NewItem((int)target.position.X, (int)target.position.Y, target.width, target.height, mod.ItemType("AzanaChibi"));
                NetMessage.SendData(MessageID.SyncItem, -1, -1, null, item, 1f);
                Main.NewText("Glegrep?", new Color(235, 70, 106));
            }
            else
            {
                int item = Item.NewItem((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height, mod.ItemType("Tomato"));
                NetMessage.SendData(MessageID.SyncItem, -1, -1, null, item, 1f);
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            int item = Item.NewItem((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height, mod.ItemType("Tomato"));
            NetMessage.SendData(MessageID.SyncItem, -1, -1, null, item, 1f);
            return true;
        }
    }
}