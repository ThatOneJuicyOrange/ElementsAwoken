using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.Wasteland
{
    public class ScorpionBlade : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 28;
            item.melee = true;
            item.width = 40;
            item.height = 40;
            item.useTime = 32;
            item.useTurn = true;
            item.useAnimation = 32;
            item.useStyle = 1;
            item.knockBack = 5;
            item.value = Item.buyPrice(0, 5, 0, 0);
            item.rare = 2;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Scorpion Blade");
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, 200);
        }
    }
}
