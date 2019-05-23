using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Audio;

namespace ElementsAwoken.Items.Weapons.Magic
{
    public class Warhorn : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 38;
            item.height = 18;

            item.damage = 35;
            item.mana = 20;

            item.useTime = 50;
            item.useAnimation = 50;
            item.useStyle = 5;
            item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Warhorn");

            item.magic = true;
            item.autoReuse = false;
            item.noMelee = true;
            item.knockBack = 4;

            item.value = Item.buyPrice(0, 1, 0, 0);
            item.rare = 4;

            item.shoot = mod.ProjectileType("WarhornP");
            item.shootSpeed = 24f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nordic War Horn");
            Tooltip.SetDefault("Pushes enemies away");
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && npc.damage > 0 && !npc.boss && Vector2.Distance(npc.Center, player.Center) < 100)
                {
                    Vector2 toTarget = new Vector2(player.Center.X - npc.Center.X, player.Center.Y - npc.Center.Y);
                    toTarget.Normalize();
                    npc.velocity -= toTarget * 10f;
                }
            }
            Projectile.NewProjectile(position.X, position.Y, 0f, 0f, mod.ProjectileType("WarhornP"), damage, knockBack, player.whoAmI, 0.0f, 0.0f);
            return false;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0, -10);
        }
    }
}
