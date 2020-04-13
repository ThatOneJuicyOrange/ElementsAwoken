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
            item.width = 40;
            item.height = 40;
            
            item.damage = 28;

            item.useTime = 32;

            item.useTurn = true;
            item.melee = true;
            item.autoReuse = true;

            item.useAnimation = 32;
            item.useStyle = 1;
            item.knockBack = 5;
            item.value = Item.buyPrice(0, 5, 0, 0);
            item.rare = 2;
            item.UseSound = SoundID.Item1;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Protector's Edge");
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, 200);
        }
    }
}
