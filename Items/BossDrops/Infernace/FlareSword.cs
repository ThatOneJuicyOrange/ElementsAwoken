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
            item.width = 70;
            item.height = 70;
            
            item.damage = 26;
            item.knockBack = 5;

            item.UseSound = SoundID.Item1;
            item.useTime = 24;
            item.useAnimation = 24;
            item.useStyle = 1;

            item.useTurn = true;
            item.melee = true;
            item.autoReuse = true;

            item.value = Item.buyPrice(0, 10, 0, 0);
            item.rare = 3;

            item.shoot = mod.ProjectileType("FireBlade");
            item.shootSpeed = 10f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blade of Ashes");
            Tooltip.SetDefault("");
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 200);
        }
    }
}
