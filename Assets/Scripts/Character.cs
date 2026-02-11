using System;
using UnityEngine;

public enum AnimType
{
    IDLE,
    WALK,
    HURT
}

[Serializable]
public struct CharacterAnims
{
    public AnimType type;
    // whatever the right class for animation is
}

public class Character : MonoBehaviour
{
    [SerializeField] CharacterAnims[] anims;

    public void PlayAnim(AnimType type)
    {
        for(int i = 0; i < anims.Length; i++)
        {
            if(type == anims[i].type)
            {
                // play animation
                break;
            }
        }
    }
}
