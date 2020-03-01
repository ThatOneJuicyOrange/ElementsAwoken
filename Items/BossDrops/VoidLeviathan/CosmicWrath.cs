using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.VoidLeviathan
{
    public class CosmicWrath : ModItem
    {
        public int killallDelay = 0;

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;

            item.damage = 240;

            item.thrown = true;
            item.noMelee = true;
            item.useTurn = true;
            item.consumable = false;
            item.noUseGraphic = true;
            item.autoReuse = true;

            item.useTime = 6;
            item.useAnimation = 6;
            item.reuseDelay = 6;
            item.useStyle = 1;
            item.knockBack = 6;
            item.UseSound = SoundID.Item1;

            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 12;
            item.value = Item.sellPrice(0, 25, 0, 0);

            item.shoot = mod.ProjectileType("CosmicWrathP");
            item.shootSpeed = 24f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cosmic Wrath");
            Tooltip.SetDefault("Right Click to cause all spears to explode\nCritical strikes do 3x damage");
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (player.altFunctionUse == 2)
            {
                if (killallDelay > 0)
                {
                    return false;
                }
            }
            return true;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (player.altFunctionUse == 2)
            {
                for (int i = 0; i < Main.projectile.Length; i++)
                {
                    Projectile proj = Main.projectile[i];
                    if (proj.active && proj.type == mod.ProjectileType("CosmicWrathP"))
                    {
                        proj.Kill();
                    }
                }
                killallDelay = 20;
                return false;
            }
            else
            {
                return true;
            }
        }
        public override void HoldItem(Player player)
        {
            killallDelay--;
        }
    }
}
