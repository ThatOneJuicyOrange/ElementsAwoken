using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementsAwoken.Items.Essence
{
    public class VoidEssence : ModItem
    {
        public int variant = 0;
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 999;
            item.value = Item.sellPrice(0, 0, 1, 0);
            item.rare = 10;
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Essence");
            Tooltip.SetDefault("Essence from the depths of Terraria");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(9, 9));
        }
        public override void UpdateInventory(Player player)
        {
            for (int i = 0; i < 50; i++)
            {
                Item other = Main.LocalPlayer.inventory[i];
                if (other.type == item.type)
                {
                    VoidEssence essence = (VoidEssence)other.modItem;
                    essence.variant = variant;
                }
            }
        }
        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            DrawAnimation itemAnim = Main.itemAnimations[item.type];
            int frameHeight = Main.itemTexture[item.type].Height / 9;
            if (variant == 0)
            {
                if (itemAnim.Frame >= 5)
                {
                    itemAnim.Frame = 0;
                    itemAnim.FrameCounter = 0;
                }
            }
            else
            {
                if (itemAnim.Frame < 6)
                {
                    itemAnim.Frame = 6;
                    itemAnim.FrameCounter = 0;
                }
                if (itemAnim.Frame >= 8)
                {
                    itemAnim.Frame = 6;
                    itemAnim.FrameCounter = 0;
                }
            }
            spriteBatch.Draw(Main.itemTexture[item.type], item.position - Main.screenPosition, new Rectangle(0, itemAnim.Frame * frameHeight, Main.itemTexture[item.type].Width, frameHeight), lightColor, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            return false;
        }
        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            DrawAnimation itemAnim = Main.itemAnimations[item.type];
            if (variant == 0)
            {
                if (itemAnim.Frame >= 5)
                {
                    itemAnim.Frame = 0;
                    itemAnim.FrameCounter = 0;
                }
            }
            else
            {
                if (itemAnim.Frame < 6)
                {
                    itemAnim.Frame = 6;
                    itemAnim.FrameCounter = 0;
                }
                if (itemAnim.Frame >= 8)
                {
                    itemAnim.Frame = 6;
                    itemAnim.FrameCounter = 0;
                }
            }         
            spriteBatch.Draw(Main.itemTexture[item.type], position, frame, drawColor, 0f, origin, scale, SpriteEffects.None, 0f);
            return false;
        }
        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
            float num = (float)Main.rand.Next(90, 111) * 0.01f;
            num *= Main.essScale;
            Lighting.AddLight((int)((item.position.X + (float)(item.width / 2)) / 16f), (int)((item.position.Y + (float)(item.height / 2)) / 16f), 0.5f * num, 0.3f * num, 0.05f * num);
        }
    }
}
