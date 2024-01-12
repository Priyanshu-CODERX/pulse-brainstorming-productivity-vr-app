using UnityEngine;

public class Switcher : MonoBehaviour
{
    [SerializeField] private GameObject _object;
    private bool _switched = false;

    public void Switch()
    {
        _object.SetActive(!_switched);
        _switched = !_switched;
    }
}
