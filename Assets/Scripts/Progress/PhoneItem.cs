using UnityEngine;

public class PhoneItem : MonoBehaviour {
	private void Start ()
        => gameObject.SetActive(!PlayerProgress.Instance.hasPhone);
}
