using ElementsAwoken.NPCs.Town;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.UI.Chat;
using static Terraria.ModLoader.ModContent;

namespace ElementsAwoken.UI
{
	internal class AlchemistUI : UIState
	{
		private VanillaItemSlotWrapper itemSlot;

		public override void OnInitialize() {
            itemSlot = new VanillaItemSlotWrapper(ItemSlot.Context.BankItem, 0.85f) {
                Left = { Pixels = 50 },
                Top = { Pixels = 270 },
                ValidItemFunc = item => item.IsAir || !item.IsAir && GetItem().Contains(item.type)
			};
			Append(itemSlot);
		}

        // spawns the item on the player if they didnt take it out
		public override void OnDeactivate() {
			if (!itemSlot.Item.IsAir) {
				Main.LocalPlayer.QuickSpawnClonedItem(itemSlot.Item, itemSlot.Item.stack);
				itemSlot.Item.TurnToAir();
			}
		}

		public override void Update(GameTime gameTime) {
			base.Update(gameTime);

            // if not talking to alchemist
			if (Main.LocalPlayer.talkNPC == -1 || Main.npc[Main.LocalPlayer.talkNPC].type != NPCType<Alchemist>()) {
				GetInstance<ElementsAwoken>().AlchemistUserInterface.SetState(null);
			}
		}
        private List<int> GetItem()
        {
            List<int> idList = new List<int>()
            {
                // ore
                ItemID.CopperOre,
                ItemID.TinOre,
                ItemID.IronOre,
                ItemID.LeadOre,
                ItemID.SilverOre,
                ItemID.TungstenOre,
                ItemID.GoldOre,
                ItemID.PlatinumOre,
                ItemID.DemoniteOre,
                ItemID.CrimtaneOre,
                ItemID.CobaltOre,
                ItemID.PalladiumOre,
                ItemID.MythrilOre,
                ItemID.OrichalcumOre,
                ItemID.AdamantiteOre,
                ItemID.TitaniumOre,
                // bars
                ItemID.CopperBar,
                ItemID.TinBar,
                ItemID.IronBar,
                ItemID.LeadBar,
                ItemID.SilverBar,
                ItemID.TungstenBar,
                ItemID.GoldBar,
                ItemID.PlatinumBar,
                ItemID.DemoniteBar,
                ItemID.CrimtaneBar,
                ItemID.CobaltBar,
                ItemID.PalladiumBar,
                ItemID.MythrilBar,
                ItemID.OrichalcumBar,
                ItemID.AdamantiteBar,
                ItemID.TitaniumBar,
                // blocks
                ItemID.EbonstoneBlock,
                ItemID.CrimstoneBlock,
                ItemID.EbonsandBlock,
                ItemID.CrimsandBlock,
                ItemID.PurpleIceBlock,
                ItemID.RedIceBlock,
                // biome weapons
                ItemID.ScourgeoftheCorruptor,
                ItemID.VampireKnives,
                // mimic weapons
                ItemID.DartRifle,
                ItemID.DartPistol,
                ItemID.ClingerStaff,
                ItemID.SoulDrain,
                ItemID.WormHook,
                ItemID.TendonHook,
                ItemID.PutridScent,
                ItemID.FleshKnuckles,
                ItemID.ChainGuillotines,
                ItemID.FetidBaghnakhs,
                // o r b
                ItemID.Musket,
                ItemID.TheUndertaker,
                ItemID.ShadowOrb,
                ItemID.CrimsonHeart,
                ItemID.Vilethorn,
                ItemID.CrimsonRod,
                ItemID.BallOHurt,
                ItemID.TheRottedFork,
                ItemID.BandofStarpower,
                ItemID.PanicNecklace,
                // solutions and seeds
                ItemID.CorruptSeeds,
                ItemID.CrimsonSeeds,
                ItemID.PurpleSolution,
                ItemID.RedSolution,
                // boss drops               
                ItemID.WormScarf,
                ItemID.BrainOfConfusion,
                // random
                ItemID.RottenChunk,
                ItemID.Vertebrae,
                ItemID.CursedFlame,
                ItemID.Ichor,
                ItemID.ShadowScale,
                ItemID.TissueSample,
            };
            return idList;
        }
        private bool tickPlayed;
		protected override void DrawSelf(SpriteBatch spriteBatch) {
			base.DrawSelf(spriteBatch);
			Main.HidePlayerCraftingMenu = true;

			const int slotX = 50;
			const int slotY = 270;
			if (!itemSlot.Item.IsAir) 
            {
                #region get and draw price
                int convertPrice = (itemSlot.Item.value / 10) * itemSlot.Item.stack;
                if (convertPrice == 0) convertPrice = Item.buyPrice(0, 0, 1, 0) * itemSlot.Item.stack;
				string costText = Language.GetTextValue("LegacyInterface.46") + ": ";
				string coinsText = "";
				int[] coins = Utils.CoinsSplit(convertPrice);
				if (coins[3] > 0) {
					coinsText = coinsText + "[c/" + Colors.AlphaDarken(Colors.CoinPlatinum).Hex3() + ":" + coins[3] + " " + Language.GetTextValue("LegacyInterface.15") + "] ";
				}
				if (coins[2] > 0) {
					coinsText = coinsText + "[c/" + Colors.AlphaDarken(Colors.CoinGold).Hex3() + ":" + coins[2] + " " + Language.GetTextValue("LegacyInterface.16") + "] ";
				}
				if (coins[1] > 0) {
					coinsText = coinsText + "[c/" + Colors.AlphaDarken(Colors.CoinSilver).Hex3() + ":" + coins[1] + " " + Language.GetTextValue("LegacyInterface.17") + "] ";
				}
				if (coins[0] > 0) {
					coinsText = coinsText + "[c/" + Colors.AlphaDarken(Colors.CoinCopper).Hex3() + ":" + coins[0] + " " + Language.GetTextValue("LegacyInterface.18") + "] ";
				}
				ItemSlot.DrawSavings(Main.spriteBatch, slotX + 130, Main.instance.invBottom, true);
				ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontMouseText, costText, new Vector2(slotX + 50, slotY), new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor), 0f, Vector2.Zero, Vector2.One, -1f, 2f);
				ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontMouseText, coinsText, new Vector2(slotX + 50 + Main.fontMouseText.MeasureString(costText).X, (float)slotY), Color.White, 0f, Vector2.Zero, Vector2.One, -1f, 2f);
                #endregion
                int reforgeX = slotX + 70;
				int reforgeY = slotY + 40;
				bool hoveringOverReforgeButton = Main.mouseX > reforgeX - 15 && Main.mouseX < reforgeX + 15 && Main.mouseY > reforgeY - 15 && Main.mouseY < reforgeY + 15 && !PlayerInput.IgnoreMouseInterface;
				Texture2D reforgeTexture = Main.reforgeTexture[hoveringOverReforgeButton ? 1 : 0];
				Main.spriteBatch.Draw(reforgeTexture, new Vector2(reforgeX, reforgeY), null, Color.White, 0f, reforgeTexture.Size() / 2f, 0.8f, SpriteEffects.None, 0f);
				if (hoveringOverReforgeButton) {
					Main.hoverItemName = Language.GetTextValue("LegacyInterface.19");
					if (!tickPlayed) {
						Main.PlaySound(12, -1, -1, 1, 1f, 0f);
					}
					tickPlayed = true;
					Main.LocalPlayer.mouseInterface = true;
					if (Main.mouseLeftRelease && Main.mouseLeft && Main.LocalPlayer.CanBuyItem(convertPrice, -1) && ItemLoader.PreReforge(itemSlot.Item)) {
						Main.LocalPlayer.BuyItem(convertPrice, -1);
						bool favorited = itemSlot.Item.favorited;
						int stack = itemSlot.Item.stack;
                        int itemType = GetItem().IndexOf(itemSlot.Item.type);
                        int counterPart = GetItem()[itemType + ((GetItem().IndexOf(itemSlot.Item.type) + 1) % 2 == 0 ? -1 : 1)];

                        Item reforgeItem = new Item();
						reforgeItem.SetDefaults(counterPart);
						reforgeItem = reforgeItem.CloneWithModdedDataFrom(itemSlot.Item);
						itemSlot.Item = reforgeItem.Clone();
						itemSlot.Item.position.X = Main.LocalPlayer.position.X + (float)(Main.LocalPlayer.width / 2) - (float)(itemSlot.Item.width / 2);
						itemSlot.Item.position.Y = Main.LocalPlayer.position.Y + (float)(Main.LocalPlayer.height / 2) - (float)(itemSlot.Item.height / 2);
						itemSlot.Item.favorited = favorited;
						itemSlot.Item.stack = stack;
						ItemText.NewText(itemSlot.Item, itemSlot.Item.stack, true, false);
						Main.PlaySound(SoundID.Item37, -1, -1);
					}
				}
				else {
					tickPlayed = false;
				}
			}
			else {
				string message = "Place an item here to convert it into its\ncounterpart";
				ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontMouseText, message, new Vector2(slotX + 50, slotY), new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor), 0f, Vector2.Zero, Vector2.One, -1f, 2f);
			}
		}
	}
}
