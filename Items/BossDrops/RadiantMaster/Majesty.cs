using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using ElementsAwoken.Tiles.Crafting;
using ElementsAwoken.Projectiles.Whips;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.Items.BossDrops.RadiantMaster
{
    public class Majesty : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 11;

            item.damage = 360;
            item.knockBack = 4f;

            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 13;

            item.useStyle = 5;
            item.useAnimation = 24;
            item.useTime = 24;
            item.UseSound = SoundID.Item116;

            item.noMelee = true;
            item.noUseGraphic = true;
            item.melee = true;
            item.autoReuse = true;
            item.noMelee = true;

            item.shoot = ProjectileType<MajestyP>();
            item.shootSpeed = 15f;

        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Majesty");
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float ai3 = (Main.rand.NextFloat() - 0.75f) * 0.7853982f; //0.5
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 0.0f, ai3);
            return false;
        }
    }
}
