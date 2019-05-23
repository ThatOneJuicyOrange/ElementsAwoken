using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.Infernace
{
    public class FlareSword : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 32;
            item.melee = true;
            item.width = 70;
            item.height = 70;
            item.useTime = 19;
            item.useTurn = true;
            item.useAnimation = 19;
            item.useStyle = 1;
            item.knockBack = 5;
            item.value = Item.buyPrice(0, 10, 0, 0);
            item.rare = 3;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("FireBlade");
            item.shootSpeed = 12f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flare Sword");
            Tooltip.SetDefault("");
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 200);
        }
    }
}
