using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{

	[SerializeField] private Text _bigAmountText;
	[SerializeField] private GameObject _bigArrow;
	[SerializeField] private Text _luigiAmountText;
	[SerializeField] private GameObject _luigiArrow;
	[SerializeField] private Text _daisyAmountText;
	[SerializeField] private GameObject _daisyArrow;
	[SerializeField] private Text _powAmountText;
	[SerializeField] private GameObject _powArrow;
	
	public Dictionary<CollectableEnum, Sprite> _collectableIconsDictionary;    
	public Dictionary<CollectableEnum, GameObject> _collectablePrefabDictionary;
	public Dictionary<CollectableEnum, int> _collectableAmountDictionary;
	public Dictionary<CollectableEnum, bool> _collectableProjectileDictionary;
	private Dictionary<int, GameObject> _activeSpecialArrowDictionary;
	private Dictionary<int, CollectableEnum> _activeSpecialCollectableDictionary;

	[SerializeField] public int activeSpecial;

		
	public static InventoryManager Instance = null;

	private void Awake()
	{
		if (Instance == null)
			Instance = this;
		
		activeSpecial = 0;
		
		var types = Resources.LoadAll<CollectableObject>("CollectableTypes");
		
		_collectableIconsDictionary = types.ToDictionary(x => x.Type, y => y.Icon);
		_collectablePrefabDictionary = types.ToDictionary(x => x.Type, y => y.Prefab);
		_collectableAmountDictionary = types.ToDictionary(x => x.Type, y => y.Amount);
		_collectableProjectileDictionary = types.ToDictionary(x => x.Type, y => y.ProjectileType);

		_activeSpecialArrowDictionary = new Dictionary<int, GameObject>
		{
			{0, _bigArrow},
			{2, _luigiArrow},
			{1, _daisyArrow},
			{3, _powArrow}
		};
		
		_activeSpecialCollectableDictionary = new Dictionary<int, CollectableEnum>
		{
			{0, CollectableEnum.Big},
			{2, CollectableEnum.Luigi},
			{1, CollectableEnum.Daisy},
			{3, CollectableEnum.Pow}
		};
	}

	void Start()
	{
		
	}
	
	void Update()
	{
		_bigAmountText.text = _collectableAmountDictionary[CollectableEnum.Big].ToString();
		_luigiAmountText.text = _collectableAmountDictionary[CollectableEnum.Luigi].ToString();
		_daisyAmountText.text = _collectableAmountDictionary[CollectableEnum.Daisy].ToString();
		_powAmountText.text = _collectableAmountDictionary[CollectableEnum.Pow].ToString();

        if (StateController.IsPaused) return;

		if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
		{
			if (activeSpecial > 0)
			{
				_activeSpecialArrowDictionary[activeSpecial].SetActive(false);
				activeSpecial--;
				_activeSpecialArrowDictionary[activeSpecial].SetActive(true);
			}
			
		}
		
		if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
		{
			if (activeSpecial < 3)
			{
				_activeSpecialArrowDictionary[activeSpecial].SetActive(false);
				activeSpecial++;
				_activeSpecialArrowDictionary[activeSpecial].SetActive(true);
			}
		}
	}
	

	public void AddCollectable(CollectableEnum type)
	{
		_collectableAmountDictionary[type]++;
	}
	
	public void RemoveCollectableFromActive()
	{
		if(_collectableAmountDictionary[_activeSpecialCollectableDictionary[activeSpecial]]>0)
		_collectableAmountDictionary[_activeSpecialCollectableDictionary[activeSpecial]]--;
	}

	public CollectableEnum GetActiveSpecial()
	{
		return _activeSpecialCollectableDictionary[activeSpecial];
	}
	
	public int GetActiveSpecialAmount()
	{
		return _collectableAmountDictionary[_activeSpecialCollectableDictionary[activeSpecial]];
	}
}
