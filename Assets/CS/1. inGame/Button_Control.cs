using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Button_Control : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    Button button;
    Player_CS player_CS;
    [SerializeField] string Button_Name;


    void Start()
    {
        player_CS = GameObject.FindWithTag("Player").GetComponent<Player_CS>();
        button = GetComponent<Button>();
        switch (Button_Name)
        {
            case "Attack_Button": button.onClick.AddListener(player_CS.Attack); break;
            case "Jump_Button": button.onClick.AddListener(player_CS.Jump); break;
            //case "Slide_Button":  break;
        }
    }
    public void OnPointerDown(PointerEventData eventData) { if (Button_Name == "Slide_Button") player_CS.Slide_DAWN(); }

    public void OnPointerUp(PointerEventData eventData) { if (Button_Name == "Slide_Button") player_CS.Slide_UP(); }

    // Update is called once per frame
    void Update()
    {

    }
}
