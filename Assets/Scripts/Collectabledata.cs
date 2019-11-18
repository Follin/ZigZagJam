using UnityEngine;

[CreateAssetMenu(fileName = "Collectable", menuName = "Collectable Data", order = 1)]
public class CollectableData : ScriptableObject
{
    public Color Color;
    public int Score;
    public ParticleSystem Particles;
    public AudioClip Clip;
}
