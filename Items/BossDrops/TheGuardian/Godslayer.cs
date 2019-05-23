using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.TheGuardian
{
    public class Godslayer : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 70;
            item.melee = true;
            item.width = 70;
            item.height = 70;
            item.useTime = 16;
            item.useTurn = true;
            item.useAnimation = 16;
            item.useStyle = 1;
            item.knockBack = 5;
            item.value = Item.buyPrice(0, 50, 0, 0);
            item.rare = 10;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("GodslayerStrike");
            item.shootSpeed = 12f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Godslayer");
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 200);
        }
    }
}
