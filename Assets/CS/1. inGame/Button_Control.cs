using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Button_Control : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    Button button;
    [SerializeField] bool Jump_Button;
    [SerializeField] bool Slide_Button;

    void Start()
    {
        button = GetComponent<Button>();
        if (Jump_Button) button.onClick.AddListener(Player_CS.PL.Jump);
    }
    public void OnPointerDown(PointerEventData eventData) { if (Slide_Button) Player_CS.PL.Slide_DAWN(); }

    public void OnPointerUp(PointerEventData eventData) { if (Slide_Button) Player_CS.PL.Slide_UP(); }

    void Update()
    {

    }
}
