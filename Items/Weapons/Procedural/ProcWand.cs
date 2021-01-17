using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ElementsAwoken.Items.Weapons.Procedural
{
    public class ProcWand : ModItem
    {
        public override string Texture { get { return "ElementsAwoken/Items/Weapons/Procedural/Fire1"; } }// always load the biggest sprite

        public int variant = 0;
        public string element = "Fire";

        public int spell = 0; 
        public override bool CloneNewInstances
        {
            get { return true; }
        }
        public override void NetSend(BinaryWriter writer)
        {
            writer.Write(variant);
        }

        public override void NetRecieve(BinaryReader reader)
        {
            variant = reader.ReadInt32();
        }

        public override TagCompound Save()
        {
            return new TagCompound
            {
                {
                    "variant", variant
                }
            };
        }

        public override void Load(TagCompound tag)
        {
            variant = tag.GetInt("variant");
        }
        public override void SetDefaults()
        {
            item.width = 50;
            item.height = 50;

            item.damage = 28;
            item.mana = 9;

            item.useTime = 30;
            item.useAnimation = 30;

            Item.staff[item.type] = true;
            item.magic = true;
            item.noMelee = true;
            item.autoReuse = true;
            item.noUseGraphic = true;

            item.useStyle = 5;
            item.knockBack = 2;

            item.value = Item.sellPrice(0, 0, 50, 0);
            item.rare = 4;

            item.UseSound = SoundID.Item42;
            item.shoot = mod.ProjectileType("CursedBall");
            item.shootSpeed = 13f;
        }
        public override bool OnPickup(Player player)
        {
            if (ModContent.GetInstance<Config>().debugMode) variant = Main.rand.Next(2);
            return base.OnPickup(player);
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            MyPlayer modPlayer = Main.LocalPlayer.GetModPlayer<MyPlayer>();

            string name = element;

            foreach (TooltipLine line2 in tooltips)
            {
                /*if (line2.mod == "Terraria" && line2.Name.StartsWith("Tooltip"))
                {
                    line2.text = desc;
                    if (ModContent.GetInstance<Config>().debugMode)
                    {
                        line2.text += "\npotionNum:" + potionNum + "\ncolor:" + EAWorldGen.mysteriousPotionColours[potionNum];
                    }
                }*/
                if (line2.mod == "Terraria" && line2.Name.Contains("ItemName"))
                {
                    line2.text = name + " Wand";
                    if (ModContent.GetInstance<Config>().debugMode) line2.text += " " + variant;
                }
            }
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int input = 0;
            if (element == "Desert") input = 1;
            if (element == "Fire") input = 2;
            if (element == "Sky") input = 3;
            if (element == "Frost") input = 4;
            if (element == "Water") input = 5;
            if (element == "Void") input = 6;
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("WandHeld"), 0, 0, player.whoAmI, input, variant);
            return false;
        }
        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D tex = mod.GetTexture("Items/Weapons/Procedural/" + element + variant);
            Texture2D startTex = mod.GetTexture("Items/Weapons/Procedural/Fire1");
            float scaleExtra = (float)startTex.Width / (float)tex.Width;
            scaleExtra *= 0.99f;
            spriteBatch.Draw(tex, position, frame, drawColor, 0f, origin, scale * scaleExtra, SpriteEffects.None, 0f);
        }
        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D tex = mod.GetTexture("Items/Weapons/Procedural/" + element + variant);
            spriteBatch.Draw(tex, item.position, null, lightColor, 0f, Vector2.Zero, scale , SpriteEffects.None, 0f);
        }
        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            return false;
        }
        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            return false;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wand");
            Tooltip.SetDefault("");
        }
    }

}
