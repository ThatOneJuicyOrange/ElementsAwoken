using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.Regaroth
{
    [AutoloadEquip(EquipType.Legs)]
    public class EnergyWeaversLeggings : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;

            item.value = Item.sellPrice(0, 7, 50, 0);
            item.rare = 6;

            item.defense = 12;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Energy Weaver's Leggings");
            Tooltip.SetDefault("Damage increased the closer it is to midday to a maximum of 5%\n20% increased movement speed\nYou leave a trail of energy particles");
        }
        public override void UpdateEquip(Player player)
        {
            player.moveSpeed *= 1.2f;

            float damageBonus = 1f;
            if (Main.dayTime)
            {
                damageBonus = MathHelper.Lerp(1.05f, 1f, MathHelper.Distance((float)Main.time, 27000) / 27000);
            }
            player.meleeDamage *= damageBonus;
            player.magicDamage *= damageBonus;
            player.rangedDamage *= damageBonus;
            player.minionDamage *= damageBonus;
            player.thrownDamage *= damageBonus;
        }
        public override void UpdateVanity(Player player, EquipType type)
        {
            if (player.velocity.Y == 0f && player.velocity.X != 0)
            {
                int dustType = Main.rand.Next(2) == 0 ? 135 : 242;
                int dust = Dust.NewDust(new Vector2(player.position.X, player.position.Y + (float)player.height - 4f), player.width, 8, dustType, 0f, 0f, 100, default(Color), 1.4f);
                Main.dust[dust].velocity *= 0.1f;
                Main.dust[dust].noGravity = true;
                Main.dust[dust].scale *= 1f + (float)Main.rand.Next(20) * 0.01f;
                Main.dust[dust].shader = GameShaders.Armor.GetSecondaryShader(player.cShoe, player);
            }
        }
    }
}
