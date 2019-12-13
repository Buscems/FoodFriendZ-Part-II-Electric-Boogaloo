using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    //dont touch this (unless you're trap loving lolicon)
    public string[] CharacterListNames = new string[]
    {
       /*0 */ "Tofu",
       /*1 */ "Onigiri",
       /*2 */ "Takoyaki",
       /*3 */ "Cherry",
       /*4 */ "Cannoli",
       /*5 */ "Burger",
       /*6 */ "Sashimi",
       /*7 */ "Fries",
       /*8 */ "Taco",
       /*9 */ "Donut",
       /*10*/ "Hotdog",
       /*11*/ "Napoleon",
       /*12*/ "Muffin",
       /*13*/ "Cone",
       /*14*/ "LobsterTail",
       /*15*/ "Samosa",
       /*16*/ "TunaCan",
       /*17*/ "Pancakes",
       /*18*/ "Orb",
       /*19*/ "Pancakes",
    };



    //dont touch this one (unless you're trap loving lolicon)
    public string[] ItemsListNames = new string[] 
    {
       /*0 */ "Thyme",
       /*1 */ "Drain Trap",
       /*2 */ "Soul Sake",
       /*3 */ "Donut Holes",
       /*4 */ "Couple's Milkshake",
       /*5 */ "Starfruit",
       /*6 */ "King of Cola",
       /*7 */ "Caviar",
       /*8 */ "Stopwatch",
       /*9 */ "Whole Milk",
       /*10*/ "Rabbit Stew",
       /*11*/ "Utensil Box",
       /*12*/ "Weird Dough",
       /*13*/ "Molasses",
       /*14*/ "Preserves",
       /*15*/ "Paper Towel",
       /*16*/ "Sponge",
       /*17*/ "Rag",
       /*18*/ "Granola",
       /*19*/ "Ceramic Plate",
       /*20*/ "Whipped Cream",
       /*21*/ "Pizza Cutter",
       /*22*/ "Tenderizer",
       /*23*/ "Whisk",
       /*24*/ "Canned Good",
       /*25*/ "Smoothie",
       /*26*/ "Detergent Pod",
       /*27*/ "Culinary Torch",
       /*28*/ "Paprika",
       /*29*/ "Rosemary",
       /*30*/ "Garnish",
       /*31*/ "Easter Egg",
       /*32*/ "Banana Peel",
       /*33*/ "Drink Coaster",
       /*34*/ "Spinach",
       /*35*/ "Spicy Jalapeno",
       /*36*/ "Aluminum Foil",
       /*37*/ "Egg Timer"
    };

    public bool[] ItemsList = new bool[]
    {
        /*
        items list [Bool] -> true = picked up item in past, false = never picked up item 
        
         * Array Pos - Item
         */
         /* 0  */ false,  /* [Thyme] */
         /* 1  */ false,  /* [Drain Trap] */
         /* 2  */ false,  /* [Soul Sake] */
         /* 3  */ false,  /* [Donut Holes] */
         /* 4  */ false,  /* [Couple's Milkshake] */
         /* 5  */ false,  /* [Starfruit] */
         /* 6  */ false,  /* [King of Cola] */
         /* 7  */ false,  /* [Caviar] */
         /* 8  */ false,  /* [Stopwatch] */
         /* 9  */ false,  /* [Whole Milk] */
         /* 10 */ false,  /* [Rabbit Stew] */
         /* 11 */ false,  /* [Utensil Box] */
         /* 12 */ false,  /* [Weird Dough] */
         /* 13 */ false,  /* [Molasses] */
         /* 14 */ false,  /* [Preserves] */
         /* 15 */ false,  /* [Paper Towel] */
         /* 16 */ false,  /* [Sponge] */
         /* 17 */ false,  /* [Rag] */
         /* 18 */ false,  /* [Granola] */
         /* 19 */ false,  /* [Ceramic Plate] */
         /* 20 */ false,  /* [Whipped Cream] */
         /* 21 */ false,  /* [Pizza Cutter] */
         /* 22 */ false,  /* [Tenderizer] */
         /* 23 */ false,  /* [Whisk] */
         /* 24 */ false,  /* [Canned Good] */
         /* 25 */ false,  /* [Smoothie] */
         /* 26 */ false,  /* [Detergent Pod] */
         /* 27 */ false,  /* [Culinary Torch] */
         /* 28 */ false,  /* [Paprika] */
         /* 29 */ false,  /* [Rosemary] */
         /* 30 */ false,  /* [Garnish] */
         /* 31 */ false,  /* [Easter Egg] */
         /* 32 */ false,  /* [Banana Peel] */
         /* 33 */ false,  /* [Drink Coaster] */
         /* 34 */ false,  /* [Spinach] */
         /* 35 */ false,  /* [Spicy Jalapeno] */
         /* 36 */ false,   /* [Aluminum Foil] */
         /* 37 */ false    /* [Egg Timer] */
    };




    //dont touch this one either (unless you're trap loving lolicon)
    public string[] EquipmentListNames = new string[]
    {
       /*0 */ "Hot Sauce",
       /*1 */ " ",
       /*2 */ "Valentine's Day Chocolate",
       /*3 */ "Maggots",
       /*4 */ " ",
       /*5 */ "Kiss The Cook Apron",
       /*6 */ "Sweet Carolina",
       /*7 */ "Scored Cutting Board",
       /*8 */ "Bludgeoned Cutting Board",
       /*9 */ "Cooking Spray",
       /*10*/ "Kitchen Knife",
       /*11*/ "Freeze Pops",
       /*12*/ "XXL Soda",
       /*13*/ "Marshmallow Cereal",
       /*14*/ "Protein Shake",
       /*15*/ "Truffles",
       /*16*/ "Bag Of Ice",
       /*17*/ "Rocket Popsicle",
       /*18*/ "Turtle Soup",
       /*19*/ "Starfruit",
       /*20*/ "Sugar",
       /*21*/ "Caffeine",
       /*22*/ "Frog Legs",
       /*23*/ "Garlic Powder",
       /*24*/ "Italian Seasoning",
       /*25*/ "Aluminum Foil",
       /*26*/ "Plastic Tupperware",
       /*27*/ "Plastic Wrap",
       /*28*/ "Plastic Straw",
       /*29*/ "Chopsticks",
       /*30*/ "Spatula",
       /*31*/ "Spork",
       /*32*/ "Swiss Army Cheese",
       /*33*/ "Butcher's Knife",
       /*34*/ "Rat Poison",
       /*35*/ "Oven Mitt",
       /*36*/ "Knife Sharpening Steel",
       /*37*/ "Crazy Straw",
       /*38*/ "Bendy Straw",
       /*39*/ "Food Stamp",
       /*40*/ "Coupon",
       /*41*/ "Non-Stick Spray",
       /*42*/ "Salt",
       /*43*/ "Pepper Grinder",
       /*44*/ "Mold",
       /*45*/ "Left Overs",
       /*46*/ "Vacuumed-Sealed Bag",
       /*47*/ "Coconut"
    };

    public bool[] EquipmentList = new bool[]
   {
        /*
        items list [Bool] -> true = picked up item in past, false = never picked up item 
        
         * Array Pos - Item
         */
         /* 0  */ false,  /* [Hot Sauce] */
         /* 1  */ false,  /* [] */
         /* 2  */ false,  /* [Valentine's Day Chocolate] */
         /* 3  */ false,  /* [Maggots] */
         /* 4  */ false,  /* [] */
         /* 5  */ false,  /* [Kiss The Cook Apron] */
         /* 6  */ false,  /* [Sweet Carolina] */
         /* 7  */ false,  /* [Scored Cutting Board] */
         /* 8  */ false,  /* [Bludgeoned Cutting Board] */
         /* 9  */ false,  /* [Cooking Spray] */
         /* 10 */ false,  /* [Kitchen Knife] */
         /* 11 */ false,  /* [Freeze Pops] */
         /* 12 */ false,  /* [XXL Soda] */
         /* 13 */ false,  /* [Marshmallow Cereal] */
         /* 14 */ false,  /* [Protein Shake] */
         /* 15 */ false,  /* [Truffles] */
         /* 16 */ false,  /* [Bag Of Ice] */
         /* 17 */ false,  /* [Rocket Popsicle] */
         /* 18 */ false,  /* [Turtle Soup] */
         /* 19 */ false,  /* [Starfruit] */
         /* 20 */ false,  /* [Sugar] */
         /* 21 */ false,  /* [Caffeine] */
         /* 22 */ false,  /* [Frog Legs] */
         /* 23 */ false,  /* [Garlic Powder] */
         /* 24 */ false,  /* [Italian Seasoning] */
         /* 25 */ false,  /* [Aluminum Foil] */
         /* 26 */ false,  /* [Plastic Tupperware] */
         /* 27 */ false,  /* [Plastic Wrap] */
         /* 28 */ false,  /* [Plastic Straw] */
         /* 29 */ false,  /* [Chopsticks] */
         /* 30 */ false,  /* [Spatula] */
         /* 31 */ false,  /* [Spork] */
         /* 32 */ false,  /* [Swiss Army Cheese] */
         /* 33 */ false,  /* [Butcher's Knife] */
         /* 34 */ false,  /* [Rat Poison] */
         /* 35 */ false,  /* [Oven Mitt] */
         /* 36 */ false,  /* [Knife Sharpening Steel] */
         /* 37 */ false,  /* [Crazy Straw] */
         /* 38 */ false,  /* [Bendy Straw] */
         /* 39 */ false,  /* [Food Stamp] */
         /* 40 */ false,  /* [Coupon] */
         /* 41 */ false,  /* [Non-Stick Spray] */
         /* 42 */ false,  /* [Salt] */
         /* 43 */ false,  /* [Pepper Grinder] */
         /* 44 */ false,  /* [Mold] */
         /* 45 */ false,  /* [Left Overs] */
         /* 46 */ false,  /* [Vacuumed-Sealed Bag] */
         /* 47 */ false   /* [Coconut] */

   };


    //dont touch this one either either (unless you're trap loving lolicon)
    string[] EquipmentListDescriptions = new string[]
    {
       /*0 */ " ",
       /*1 */ " ",
       /*2 */ " ",
       /*3 */ " ",
       /*4 */ " ",
       /*5 */ " ",
       /*6 */ " ",
       /*7 */ " ",
       /*8 */ " ",
       /*9 */ " ",
       /*10*/ " ",
       /*11*/ " ",
       /*12*/ " ",
       /*13*/ " ",
       /*14*/ " ",
       /*15*/ " ",
       /*16*/ " ",
       /*17*/ " ",
       /*18*/ " ",
       /*19*/ " ",
       /*20*/ "The life force of kids",
       /*21*/ "The life force of college students",
       /*22*/ "Amphibious upgrades",
       /*23*/ " ",
       /*24*/ "These hands are out of control!!",
       /*25*/ "Armor worthy of a true meal",
       /*26*/ "Versatile garbs for your average leftover",
       /*27*/ "Lightweight and snack-tight",
       /*28*/ "Seafoods’ least favorite food",
       /*29*/ "The only utensil you dual-wield with one hand",
       /*30*/ " ",
       /*31*/ "Jack of all trades, master of none.",
       /*32*/ " ",
       /*33*/ " ",
       /*34*/ "The seasoning that eats YOUR insides",
       /*36*/ "Incase you need a hand",
       /*37*/ "Razor sharp",
       /*38*/ "A straw that poses a threat to everyone’s safety",
       /*39*/ "Extend n’ Bend",
       /*40*/ "A deal you can’t (afford to) refuse!",
       /*41*/ "A deal you can’t refuse!",
       /*42*/ "Can’t catch me slick!",
       /*43*/ "The salt is real",
       /*44*/ "The gatling gun of a restaurant",
       /*45*/ "Micro biotic armor!",
       /*46*/ "Still good leftovers are the best!",
       /*47*/ "Pushing the limits of science and edibility!"
    };




    public int[] GameStats { set; get; }
    //Various stats the game tracks [Int]
    /* 
     * Array Pos -  Game Stat  
     * 0            [Total money earned] 
     * 1            [Total money spent] 
     * 2            [Player deaths] 
     * 3            [Total enemies killed]
     * 4            [Bosses killed] 
     * 5            [Damage dealt] 
     * 6            [Total bullets fired] 
     * 7            [Traps set] 
     * 8            [Longest play session (in seconds)
     */

    public bool[] CharacterList = new bool[]
    {
        /*
        character list [Bool] -> true = has character, false = doesn't have character  
        
         * Array Pos - Character
         */
         /* 0  */ true,   /* [Cannoli] */
         /* 1  */ true,   /* [Cherry] */
         /* 2  */ true,  /* [Cone] */
         /* 3  */ true,  /* [Donut] */
         /* 4  */ true,  /* [Fries] */
         /* 5  */ true,  /* [Burger] */
         /* 6  */ true,  /* [Hotdog] */
         /* 7  */ true,  /* [Napolean] */
         /* 8  */ true,  /* [Onigiri] */
         /* 9  */ true,  /* [Sashimi] */
         /* 10 */ true,  /* [Takoyaki] */
         /* 11 */ true,  /* [Tofu] */
         /* 12 */ true,  /* [5RoundBurst] */
         /* 13 */ true,  /* [Blueberry Muffin] */
         /* 14 */ true,  /* [Lobster Tail] */
         /* 15 */ true,  /* [Samosa] */
         /* 16 */ true,  /* [Taco] */
         /* 17 */ true,  /* [Tuna Can] */
         /* 18 */ true,  /* [Orb] */
         /* 19 */ true,  /* [Pancakes] */
    };
    

}
