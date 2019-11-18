using UnityEngine;

[CreateAssetMenu(fileName = "Collectable", menuName = "Collectable Data", order = 1)]
public class Collectabledata : ScriptableObject
{
    [SerializeField] Color _color;
    [SerializeField] int _score;
}
