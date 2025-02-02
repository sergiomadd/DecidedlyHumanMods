﻿using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using SmartBuilding.UI;
using SmartBuilding.Utilities;
using StardewModdingAPI;
using StardewValley;
using xTile.Dimensions;

namespace SmartBuilding
{
    public class ConsoleCommand
    {
        private Logger logger;
        private bool commandRunOnce = false;
        private Texture2D texture;
        private ModEntry mod;

        public ConsoleCommand(Logger logger, Texture2D texture, ModEntry mod)
        {
            this.logger = logger;
            this.texture = texture;
            this.mod = mod; // This is a terrible way to access the item identification, but it'll do for now.
        }

        /// <summary>
        /// Both arguments are ignored.
        /// </summary>
        /// <param name="command">Ignored.</param>
        /// <param name="args">Ignored.</param>
        public void BindingUI(string command, string[] args)
        {
            Rectangle viewport = Game1.uiViewport;
            BindingUi binding = new BindingUi(viewport.X, viewport.Y, viewport.Width, viewport.Height, texture, true);

            Game1.activeClickableMenu = binding;
        }

        public void IdentifyItemsCommand(string command, string[] args)
        {
            if (Game1.player.Items != null && Game1.player.Items.Count > 0)
            {
                IList<Item> items = Game1.player.Items;

                foreach (Item item in items)
                {
                    if (item != null)
                    {
                        ItemType type = mod.IdentifyItemType(item as StardewValley.Object);
                        logger.Log($"ItemType of {item.Name}: {type}.", LogLevel.Info);
                    }
                }
            }
        }

        public void TestCommand(string command, string[] args)
        {
            if (!commandRunOnce)
            {
                logger.Log("THIS IS YOUR ONE AND ONLY WARNING. This command is purely for testing, may wipe your player inventory, the farm, " +
                            "spawn buildings on the farm, and perform other acts of chaos. After this warning, you will be able to use " +
                            "the command.", LogLevel.Alert);
                commandRunOnce = true;

                return;
            }

            string subCommand = "";

            if (args.Length <= 0)
            {
                logger.Log("You forgot an argument.");

                return;
            }

            Farmer player = Game1.player;

            // We only care about the first argument, and can ignore the rest.
            subCommand = args[0];

            // Wipe the player's inventory with the vanilla "debug clear" command.
            Game1.game1.parseDebugInput("clear");

            // Max out the player's inventory size. It's over 9000.
            Game1.game1.parseDebugInput("backpack 9001");

            // Our item list
            Item[] items;

            // And figure out which subcommand we want.
            switch (subCommand)
            {
                case "notreallyplaceable":
                    items = new[]
                    {
                        Utility.fuzzyItemSearch("Stone", 100),
                        Utility.fuzzyItemSearch("Wood", 100),
                        Utility.fuzzyItemSearch("Spaghetti", 100),
                        Utility.fuzzyItemSearch("Legend", 100),
                    };

                    foreach (Item item in items)
                    {
                        if (item != null)
                            player.addItemToInventory(item);
                    }

                    break;
                case "torches":
                    items = new[]
                    {
                        Utility.fuzzyItemSearch("Wood Fence", 100),
                        Utility.fuzzyItemSearch("Stone Fence", 100),
                        Utility.fuzzyItemSearch("Iron Fence", 100),
                        Utility.fuzzyItemSearch("Hardwood Fence", 100),
                        Utility.fuzzyItemSearch("Torch", 100)
                    };

                    foreach (Item item in items)
                    {
                        if (item != null)
                            player.addItemToInventory(item);
                    }

                    break;
                case "rectangle":
                    items = new[]
                    {
                        Utility.fuzzyItemSearch("Wood Fence", 100),
                        Utility.fuzzyItemSearch("Stone Fence", 100),
                        Utility.fuzzyItemSearch("Iron Fence", 100),
                        Utility.fuzzyItemSearch("Hardwood Fence", 100),
                        Utility.fuzzyItemSearch("Weathered Floor", 1),
                        Utility.fuzzyItemSearch("Straw Floor", 4),
                        Utility.fuzzyItemSearch("Brick Floor", 9),
                        Utility.fuzzyItemSearch("Stone Floor", 16),
                        Utility.fuzzyItemSearch("Crystal Floor", 25),
                        Utility.fuzzyItemSearch("Stone Walkway Floor", 36),
                        Utility.fuzzyItemSearch("Gravel Path", 49),
                        Utility.fuzzyItemSearch("Wood Path", 64),
                        
                        Utility.fuzzyItemSearch("Torch", 100)
                    };

                    foreach (Item item in items)
                    {
                        if (item != null)
                            player.addItemToInventory(item);
                    }

                    break;
                case "morefertilizers":
                case "morefertilisers":
                    items = new[]
                    {
                        Utility.fuzzyItemSearch("Joja Fertilizer", 100),
                        Utility.fuzzyItemSearch("Fish Food Fertilizer", 100),
                        Utility.fuzzyItemSearch("Fruit Tree Fertilizer", 100),
                        Utility.fuzzyItemSearch("Domesticated Fish Food Fertilizer", 100),
                        Utility.fuzzyItemSearch("Apple Sapling", 20),
                        Utility.fuzzyItemSearch("Banana Sapling", 20),
                        Utility.fuzzyItemSearch("Mango Sapling", 20),
                        Utility.fuzzyItemSearch("Peach Sapling", 20),
                        Utility.fuzzyItemSearch("Orange Sapling", 20)
                    };

                    foreach (Item item in items)
                    {
                        if (item != null)
                            player.addItemToInventory(item);
                    }

                    Game1.game1.parseDebugInput("build Fish9Pond");

                    break;
                case "regression":
                    items = new[]
                    {
                        Utility.fuzzyItemSearch("Auto-Grabber")
                    };

                    foreach (Item item in items)
                    {
                        if (item != null)
                            player.addItemToInventory(item);
                    }

                    break;
                case "identification":
                    items = new[]
                    {
                        Utility.fuzzyItemSearch("Small FishTank"),
                        Utility.fuzzyItemSearch("Dresser"),
                        Utility.fuzzyItemSearch("Bed"),
                        Utility.fuzzyItemSearch("TV"),
                        Utility.fuzzyItemSearch("Oak Table"),
                        Utility.fuzzyItemSearch("Weathered Floor", 10),
                        Utility.fuzzyItemSearch("Chest", 10),
                        Utility.fuzzyItemSearch("Stone Chest", 10),
                        Utility.fuzzyItemSearch("Junimo Chest", 10),
                        Utility.fuzzyItemSearch("Hardwood Fence", 10),
                        Utility.fuzzyItemSearch("Iron Fence", 10),
                        Utility.fuzzyItemSearch("Grass Starter", 10),
                        Utility.fuzzyItemSearch("Crab Pot", 100),
                        Utility.fuzzyItemSearch("Parsnip Seed", 100),
                        Utility.fuzzyItemSearch("Eerie Seed", 100),
                        Utility.fuzzyItemSearch("Apple Sapling", 100),
                        Utility.fuzzyItemSearch("Acorn", 100),
                        Utility.fuzzyItemSearch("Tree Fertilizer", 100),
                        Utility.fuzzyItemSearch("Stone", 100),
                        Utility.fuzzyItemSearch("Wood", 100)
                    };

                    foreach (Item item in items)
                    {
                        if (item != null)
                            player.addItemToInventory(item);
                    }

                    break;
                case "flooring":
                    items = new[]
                    {
                        Utility.fuzzyItemSearch("Weathered Floor", 100),
                        Utility.fuzzyItemSearch("Straw Floor", 100),
                        Utility.fuzzyItemSearch("Brick Floor", 100),
                        Utility.fuzzyItemSearch("Stone Floor", 100),
                    };

                    foreach (Item item in items)
                    {
                        if (item != null)
                            player.addItemToInventory(item);
                    }

                    break;
                case "furniture":
                    items = new[]
                    {
                        Utility.fuzzyItemSearch("Dresser"),
                        Utility.fuzzyItemSearch("Oak Table"),
                        Utility.fuzzyItemSearch("Chair")
                    };

                    foreach (Item item in items)
                    {
                        if (item != null)
                            player.addItemToInventory(item);
                    }

                    break;
                case "tv":
                    items = new[]
                    {
                        Utility.fuzzyItemSearch("Floor TV"),
                        Utility.fuzzyItemSearch("Budget TV"),
                        Utility.fuzzyItemSearch("Plasma TV"),
                        Utility.fuzzyItemSearch("Tropical TV"),
                    };

                    foreach (Item item in items)
                    {
                        if (item != null)
                            player.addItemToInventory(item);
                    }

                    break;
                case "storage":
                    items = new[]
                    {
                        Utility.fuzzyItemSearch("Birch Dresser"),
                        Utility.fuzzyItemSearch("Oak Dresser"),
                        Utility.fuzzyItemSearch("Walnut Dresser"),
                        Utility.fuzzyItemSearch("Mahogany Dresser"),
                    };

                    foreach (Item item in items)
                    {
                        if (item != null)
                            player.addItemToInventory(item);
                    }

                    break;
                case "bed":
                    items = new[]
                    {
                        Utility.fuzzyItemSearch("Single Bed"),
                        Utility.fuzzyItemSearch("Double Bed"),
                        Utility.fuzzyItemSearch("Fisher Double Bed"),
                        Utility.fuzzyItemSearch("Wild Double Bed"),
                    };

                    foreach (Item item in items)
                    {
                        if (item != null)
                            player.addItemToInventory(item);
                    }

                    break;
                case "chests":
                case "chest":
                    items = new[]
                    {
                        Utility.fuzzyItemSearch("Chest", 100),
                        Utility.fuzzyItemSearch("Stone Chest", 100),
                        Utility.fuzzyItemSearch("Junimo Chest", 100)
                    };

                    foreach (Item item in items)
                    {
                        if (item != null)
                            player.addItemToInventory(item);
                    }

                    break;
                case "tree":
                case "trees":
                    items = new[]
                    {
                        Utility.fuzzyItemSearch("Apple Sapling", 10),
                        Utility.fuzzyItemSearch("Apricot Sapling", 10),
                        Utility.fuzzyItemSearch("Banana Sapling", 10),
                        Utility.fuzzyItemSearch("Cherry Sapling", 10),
                        Utility.fuzzyItemSearch("Maple Seed", 10),
                        Utility.fuzzyItemSearch("Acorn", 10),
                        Utility.fuzzyItemSearch("Pine Cone", 10),
                        Utility.fuzzyItemSearch("Mahogany Seed", 10),
                        Utility.fuzzyItemSearch("Tapper", 10),
                        Utility.fuzzyItemSearch("Heavy Tapper", 10),
                    };

                    foreach (Item item in items)
                    {
                        if (item != null)
                            player.addItemToInventory(item);
                    }

                    break;
                case "crop":
                case "crops":
                    items = new[]
                    {
                        Utility.fuzzyItemSearch("Eerie Seed", 100),
                        Utility.fuzzyItemSearch("Parsnip Seed", 100),
                        Utility.fuzzyItemSearch("Bison Seed", 100),
                        Utility.fuzzyItemSearch("Cactus Flower Seed", 100),
                        Utility.fuzzyItemSearch("Cabbage Seed", 100),
                    };

                    foreach (Item item in items)
                    {
                        if (item != null)
                            player.addItemToInventory(item);
                    }

                    break;
                case "fence":
                case "fences":
                    items = new[]
                    {
                        Utility.fuzzyItemSearch("Wood Fence", 100),
                        Utility.fuzzyItemSearch("Stone Fence", 100),
                        Utility.fuzzyItemSearch("Iron Fence", 100),
                        Utility.fuzzyItemSearch("Hardwood Fence", 100)
                    };

                    foreach (Item item in items)
                    {
                        if (item != null)
                            player.addItemToInventory(item);
                    }

                    break;
                case "modded":
                    items = new[]
                    {
                        Utility.fuzzyItemSearch("Cream Soda Maker", 100),
                        Utility.fuzzyItemSearch("Artisanal Soda Maker", 100),
                        Utility.fuzzyItemSearch("Alembic", 100),
                    };

                    foreach (Item item in items)
                    {
                        if (item != null)
                            player.addItemToInventory(item);
                    }

                    break;
                case "insertion":
                    items = new[]
                    {
                        Utility.fuzzyItemSearch("Furnace", 100),
                        Utility.fuzzyItemSearch("Copper Ore", 100),
                        Utility.fuzzyItemSearch("Coal", 999),
                        
                        Utility.fuzzyItemSearch("Keg", 100),
                        Utility.fuzzyItemSearch("Hops", 100),

                        Utility.fuzzyItemSearch("Cream Soda Maker", 100),
                        Utility.fuzzyItemSearch("Artisanal Soda Maker", 100),
                        Utility.fuzzyItemSearch("Carbonator", 100),
                        
                        Utility.fuzzyItemSearch("Alembic", 100),
                        Utility.fuzzyItemSearch("Vanilla", 100),

                        Utility.fuzzyItemSearch("Strawberry", 100),
                        Utility.fuzzyItemSearch("Sugar", 100),
                        Utility.fuzzyItemSearch("Fresh Water", 100),
                        Utility.fuzzyItemSearch("Sparkling Water", 100),
                    };

                    foreach (Item item in items)
                    {
                        if (item != null)
                            player.addItemToInventory(item);
                    }

                    break;
                default:
                    break;

            }
        }
    }
}