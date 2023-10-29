using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public Name name;
    public AudioClip clip;

    [Range(0, 1)]
    public float volume;
    [Range(0, 3)]
    public float pitch;
    [Range(0, 1)]
    public float spatialBlend;
    public bool loop;

    [HideInInspector]
    public AudioSource audioSource;

    public enum Name
    {
        BulletCreate2001001,
        BulletCreate2001002,
        BulletCreate2001004,
        Hitted_Body2100001,
        Hitted_Bone2100005,
        BodyHit4100001,
        Thunder,
        LevelUp,
        SkillRotateEnd,
        SkillRotating,
        FireCol
    }
}