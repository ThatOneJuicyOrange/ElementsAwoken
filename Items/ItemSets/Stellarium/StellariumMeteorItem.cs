using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.ItemSets.Stellarium
{
    public class StellariumMeteorItem : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 34;
            item.height = 32;
            item.maxStack = 999;
            item.value = Item.buyPrice(0, 5, 0, 0);
            item.rare = 0;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stellarium Meteor");
            Tooltip.SetDefault("");
        }
        public override bool OnPickup(Player player)
        {
            player.QuickSpawnItem(ModContent.ItemType<Stellorite>(), 20);
            return false;
        }
        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Lighting.AddLight(item.Center, 0.5f, 0.5f, 0.5f);

            float maxDist = item.width / 2;
            int amount = 3;
            for (int i = 0; i < amount; i++)
            {
                double angle = Main.rand.NextDouble() * 2d * Math.PI;
                Vector2 offset = new Vector2((float)Math.Sin(angle) * maxDist, (float)Math.Cos(angle) * maxDist);
                Dust dust = Main.dust[Dust.NewDust(item.Center + offset - new Vector2(4, 4), 0, 0, ModContent.DustType<Dusts.StellariumMeteorDust>(), 0, 0, 100)];
                dust.noGravity = true;
                dust.velocity *= 0.3f;
                dust.velocity += item.velocity;
                dust.scale *= 0.6f;
            }
            if (Main.rand.NextBool(5))
            {
                Dust dust2 = Main.dust[Dust.NewDust(item.position, item.width, item.height, ModContent.DustType<Dusts.StellariumMeteorDust>())];
                dust2.noGravity = true;
                dust2.scale *= 0.8f;
                dust2.velocity.Y = -Main.rand.NextFloat(5, 10);
            }

            return true;
        }
    }
}
