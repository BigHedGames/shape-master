using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShapeMaster
{
    // declare enumeration that stores the player shape status
    public enum ShapeStatus { Circle, Square, Star, Triangle };

    // declare enumeration that stores the player movement status
    public enum MovementStatus
    {
        Stationary, 
        North, 
        NorthEast, 
        East, 
        SouthEast,
        South, 
        SouthWest, 
        West, 
        NorthWest
    };

    // declare enumeration that stores 3 types of sprites
    public enum SpriteType
    {
        CHARly,
        Mad,
        Saved,
        Crazy
    };
}
