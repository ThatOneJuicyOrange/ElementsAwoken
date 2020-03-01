using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Audio;

namespace ElementsAwoken.Items.Weapons.Magic
{
    public class ImpishWarhorn : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 38;
            item.height = 18;

            item.damage = 20;
            item.mana = 20;

            item.useTime = 50;
            item.useAnimation = 50;
            item.useStyle = 5;
            //item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Warhorn");

            item.magic = true;
            item.autoReuse = false;
            item.noMelee = true;
            item.knockBack = 4;

            item.value = Item.buyPrice(0, 1, 0, 0);
            item.rare = 4;

            item.shoot = mod.ProjectileType("ImpishWave");
            item.shootSpeed = 24f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Impish Warhorn");
            Tooltip.SetDefault("Inflicts 'Impish Curse' on enemies, which increases damage taken");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Main.PlaySound(2, -1, -1, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/Warhorn"), 1, 0.5f);
            return true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HellstoneBar, 16);
            recipe.AddIngredient(ItemID.Fireblossom, 2);
            recipe.AddIngredient(null, "ImpEar", 8);
            recipe.AddIngredient(null, "MagmaCrystal", 8);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
