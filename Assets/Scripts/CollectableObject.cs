using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "CollectableType")]
public class CollectableObject : ScriptableObject
{
   [SerializeField]public CollectableEnum Type;
   [SerializeField]public Sprite Icon;
   [SerializeField]public GameObject Prefab;
   [SerializeField]public int Amount;
   [SerializeField] public bool ProjectileType;
}