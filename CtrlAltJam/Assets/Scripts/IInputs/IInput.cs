using UnityEngine;

public interface IInput
{
    Vector2 direction { get; }
    Vector2 look { get; }
    Vector2 sensitivity { get; }

    bool aButtonDown { get; }
    bool aButton { get; }
    bool aButtonUp { get; }

    bool batButtonDown { get; }
    bool batButton { get; }
    bool batButtonUp { get; }
}
