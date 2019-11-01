using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    //dont touch this (unless you're trap loving lolicon)
    public string[] CharacterListNames = new string[]
    {
       /*0 */ "Cannoli",
       /*1 */ "Cherry",
       /*2 */ "Cone",
       /*3 */ "Donut",
       /*4 */ "Fries",
       /*5 */ "Burger",
       /*6 */ "Hotdog",
       /*7 */ "Napolean",
       /*8 */ "Onigiri",
       /*9 */ "Sashimi",
       /*10*/ "Takoyaki",
       /*11*/ "Tofu",
    };

    //dont touch this one (unless you're trap loving lolicon)
    string[] ItemsListNames = new string[] 
    {
       /*0 */ "Thyme",
       /*1 */ "Drain Trap",
       /*2 */ "Soul Sake",
       /*3 */ "Donut Holes",
       /*4 */ "Couple's Milkshake",
       /*5 */ "",
       /*6 */ "King of Cola",
       /*7 */ "Caviar",
       /*8 */ "",
       /*9 */ "Whole Milk",
       /*10*/ "Rabbit Stew",
       /*11*/ "",
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
       /*36*/ "Spicy Jalapeno"
    };

    //dont touch this one either (unless you're trap loving lolicon)
    string[] EquipmentListNames = new string[]
    {
       /*0 */ "Hot Sauce",
       /*1 */ " ",
       /*2 */ "Valentine's Day Chocolate",
       /*3 */ "Maggots",
       /*4 */ " ",
       /*5 */ "Kiss The Cook Apron",
       /*6 */ " ",
       /*7 */ "Scored Cutting Board",
       /*8 */ "Bludgeoned Cutting Board",
       /*9 */ " ",
       /*10*/ " ",
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
       /*36*/ "Oven Mitt",
       /*37*/ "Knife Sharpening Steel",
       /*38*/ "Crazy Straw",
       /*39*/ "Bendy Straw",
       /*40*/ "Food Stamp",
       /*41*/ "Coupon",
       /*42*/ "Non-Stick Spray",
       /*43*/ "Salt",
       /*44*/ "Pepper Grinder",
       /*45*/ "Mold",
       /*46*/ "Left Overs",
       /*47*/ "Vacuumed-Sealed Bag"
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
         /* 2  */ false,  /* [Cone] */
         /* 3  */ false,  /* [Donut] */
         /* 4  */ false,  /* [Fries] */
         /* 5  */ false,  /* [Burger] */
         /* 6  */ false,  /* [Hotdog] */
         /* 7  */ false,  /* [Napolean] */
         /* 8  */ false,  /* [Onigiri] */
         /* 9  */ false,  /* [Sashimi] */
         /* 10 */ false,  /* [Takoyaki] */
         /* 11 */ false,  /* [Tofu] */
    };

    public bool[] ItemsList { set; get; }
    //Items list [Bool] -> 1 = collected item in past, 0 = never collected item
    /*
     * Array Pos - Item Name
     * 0           [Thyme] 
     * 1           [Drain Trap] 
     * 2           [Soul Sake] 
     * 3           [Donut Holes] 
     * 4           [Couple's Milkshake] 
     * 5           [          ] 
     * 6           [King of Cola] 
     * 7           [Caviar] 
     * 8           [          ] 
     * 9           [Whole Milk] 
     * 10          [Rabbit Stew] 
     * 11          [          ] 
     * 12          [Weird Dough] 
     * 13          [Molasses] 
     * 14          [Preserves] 
     * 15          [Paper Towel]
     * 16          [Sponge]
     * 17          [Rag]
     * 18          [Granola]
     * 19          [Ceramic Plate]
     * 20          [Whipped Cream]
     * 21          [Pizza Cutter]
     * 22          [Tenderizer]
     * 23          [Whisk]
     * 24          [Canned Good]
     * 25          [Smoothie]
     * 26          [Detergent Pod]
     * 27          [Culinary Torch]
     * 28          [Paprika]
     * 29          [Rosemary]
     * 30          [Garnish]
     * 31          [Easter Egg]
     * 32          [Banana Peel]
     * 33          [Drink Coaster]
     * 34          [Spinach]
     * 35          [Spicy Jalapeno]
     */
     
    public bool[] EquipmentList { set; get; }
    //Equipment list [Bool] -> 1 = collected equipment in past, 0 = never collected equipment
    /*
     * Array Pos - Equipment
     * 0           [Hot Sauce] 
     * 1           [         ] 
     * 2           [Valentine's Day Chocolate] 
     * 3           [Maggots] 
     * 4           [         ] 
     * 5           [Kiss The Cook Apron] 
     * 6           [         ] 
     * 7           [Scored Cutting Board] 
     * 8           [Bludgeoned Cutting Board] 
     * 9           [         ] 
     * 10          [         ] 
     * 11          [Freeze Pops] 
     * 12          [XXL Soda] 
     * 13          [Marshmallow Cereal] 
     * 14          [Protein Shake] 
     * 15          [Truffle]
     * 16          [Bag Of Ice]
     * 17          [Rocket Popsicle]
     * 18          [Turtle Soup]
     * 19          [Starfruit]
     * 20          [Sugar]
     * 21          [Caffeine]
     * 22          [Frog Legs]
     * 23          [Garlic Powder]
     * 24          [Italian Seasoning]
     * 25          [Aluminum Foil]
     * 26          [Plastic Tupperware]
     * 27          [Plastic Wrap]
     * 28          [Plastic Straw]
     * 29          [Chopsticks]
     * 30          [Spatula]
     * 31          [Spork]
     * 32          [Swiss Army Cheese]
     * 33          [Butcher's Knife]
     * 34          [Rat Poison]
     * 35          [Oven Mitt]
     * 36          [Knife Sharpening Steel]
     * 37          [Crazy Straw] 
     * 38          [Bendy Straw]
     * 39          [Food Stamp]
     * 40          [Coupon]
     * 41          [Non-Stick Spray]
     * 42          [Salt]
     * 43          [Pepper Grinder]
     * 44          [Mold] 
     * 45          [Left Overs]
     * 46          [Vacuumed-Sealed Bag]
     */

}
