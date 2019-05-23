using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.GameContent;
using Terraria.IO;
using Terraria.ObjectData;
using Terraria.Utilities;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.BossDrops.Azana
{
    public class AzanaMinionStaff : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 62;
            item.height = 62;

            item.damage = 400;
            item.knockBack = 2f;
            item.mana = 10;

            item.useTime = 36;
            item.useAnimation = 36;
            item.useStyle = 1;

            item.summon = true;
            item.autoReuse = true;
            item.noMelee = true;

            item.rare = 11;
            item.GetGlobalItem<EARarity>().rare = 13;
            item.value = Item.sellPrice(0, 35, 0, 0);

            item.UseSound = SoundID.Item44;
            item.shoot = mod.ProjectileType("AzanaMinion");
            item.shootSpeed = 10f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vow of Chaos");
            Tooltip.SetDefault("Summons Azana herself to fight alongside you\nTakes 5 minion slots");
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("AzanaEyeMinion"), damage, knockBack, player.whoAmI, 0.0f, 0.0f);
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("AzanaMinion"), damage, knockBack, player.whoAmI, 0.0f, 0.0f);
            return false;
        }
    }
}
