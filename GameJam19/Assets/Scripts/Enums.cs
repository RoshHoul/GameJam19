using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemName
{
    //NEVER ADD TO THIS LIST IN THE MIDDLE, ONLY ADD TO THE END OF THE LIST - IT WILL FUCK UP EVERYTHING!!!
    Unset,
    BlueCube,
    GreenCube,
    YellowCube,
    WhiteCube
}

public enum ItemType
{
    Collectible,
    Animated
}

public enum ItemStatus
{
    Inactive, //Nothing happened to it yet
    Activated, //It has been picked up / triggered the animation
    Placed, //Placed from inventory
    PlacedConfirmed, //Confirmed position
    CombiningLeft,
    CombiningRight
}
