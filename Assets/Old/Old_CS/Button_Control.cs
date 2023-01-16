using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Button_Control : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    UnityEngine.UI.Button button;

    [SerializeField] bool Jump_Button;
    [SerializeField] bool Slide_Button;

    void Awake() { button = GetComponent<UnityEngine.UI.Button>(); }
    void Start()
    {
        if (Jump_Button) button.onClick.AddListener(Player.PL.Jump);
    }
    public void OnPointerDown(PointerEventData eventData) { if (Slide_Button) Player.PL.Slide_DAWN(); }

    public void OnPointerUp(PointerEventData eventData) { if (Slide_Button) Player.PL.Slide_UP(); }

    // Update is called once per frame
    void Update()
    {

    }
}
