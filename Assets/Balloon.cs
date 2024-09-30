using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class Balloon : MonoBehaviour
{
    [SerializeField]
    int _id;
    [SerializeField]
    Transform _main;
    [SerializeField]
    Transform _string;

    Vector3 _originScale;
    Vector3 _balloonScale;
    bool    _isActive;

    public int ID { get { return _id; } }
    public Transform MainTransForm { get { return _main; } }
    public bool IsActive{get { return _isActive; }}
    void Awake()
    {
        _originScale  = transform.lossyScale;

        if (_main != null)
            _balloonScale = _main.lossyScale;

        _isActive = gameObject.activeSelf;
    }
    public void SetActive(bool isActive)
    {
        _main.gameObject.SetActive(isActive);
        _string.gameObject.SetActive(isActive);

        _isActive = isActive;
    }
    public void SetScale(float scale,bool isBallonOnly = true)
    {
        if(isBallonOnly)
            _main.localScale = _balloonScale * scale;
        else
            transform.localScale =  _originScale * scale;
    }
    public void SetColor(string hexColor)
    {
        Renderer render = _main.GetComponent<Renderer>();
        if (render != null)
        {
            Color color;
            if (ColorUtility.TryParseHtmlString(hexColor, out color))
                render.material.color = color;
        }
    }
    public void Setting()
    {
        Component[] components = GetComponentsInChildren<Component>();
        for (int i = 0; i < components.Length; i++)
        {
            if (components[i].name.Contains("main"))
            {
                _main = components[i].GetComponent<Transform>();
            }
            if (components[i].name.Contains("string"))
            {
                _string = components[i].GetComponent<Transform>();
            }
        }
    }
}
