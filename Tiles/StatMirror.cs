using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ElementsAwoken.Tiles
{
    public class StatMirror : ModTile
	{
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
            TileObjectData.newTile.Height = 4;
            TileObjectData.newTile.Width = 2;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16, 16 };

            TileObjectData.newTile.Direction = TileObjectDirection.PlaceLeft;
            TileObjectData.newTile.StyleWrapLimit = 2; //not really necessary but allows me to add more subtypes of chairs below the example chair texture
            TileObjectData.newTile.StyleMultiplier = 2; //same as above
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
            TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceRight; //allows me to place example chairs facing the same way as the player
            TileObjectData.addAlternate(1); //facing right will use the second texture style

            Main.tileLighted[Type] = true;
            TileObjectData.addTile(Type);
            AddMapEntry(new Color(66, 241, 244));
            disableSmartCursor = true;
        }
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 16, 16, mod.ItemType("StatMirror"));
        }
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.0f;
            g = 0.0f;
            b = 0.3f;
        }
        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;
            Tile tile = Main.tile[i, j];

            player.showItemIconText = "Melee boosts: Damage: +" + (int)((player.meleeDamage * 100) - 100) + "%" + " Crit chance: +" + (player.meleeCrit - 4) + "%" +
                 "\nMagic boosts: Damage: +" + (int)((player.magicDamage * 100) - 100) + "%" + " Crit chance: +" + (player.magicCrit - 4) + "%" +
                 "\nRanged boosts: Damage: +" + (int)((player.rangedDamage * 100) - 100) + "%" + " Crit chance: +" + (player.rangedCrit - 4) + "%" +
                 "\nThrown boosts: Damage: +" + (int)((player.thrownDamage * 100) - 100) + "%" + " Crit chance: +" + (player.thrownCrit - 4) + "%" +
                 "\nSummoner boosts: Damage: +" + (int)((player.minionDamage * 100) - 100) + "%" +
                 "\nMax amount of: Minions: +" + (player.maxMinions - 1) + " Sentries: +" + (player.maxTurrets - 1) +
                 "\nSwing speed: +" + (int)(100 - (player.meleeSpeed * 100)) + "%" + 
                 "\nMovement speed: +" + (int)((player.moveSpeed * 100) - 100) + "%" +
                 "\nDamage reduction: +" + (int)(player.endurance * 100) + "%" +
                 "\nMax life: +" + +(player.statLifeMax2 - player.statLifeMax - player.GetModPlayer<MyPlayer>().voidHeartsUsed * 10 - player.GetModPlayer<MyPlayer>().chaosHeartsUsed * 10) +
                 "\nMax mana: +" + +(player.statManaMax2 - player.statManaMax - player.GetModPlayer<MyPlayer>().lunarStarsUsed * 100) +
                 "\nCurrent life regen: " + (player.lifeRegen);

            player.noThrow = 2;
            player.showItemIcon = true;
        }
    }
}