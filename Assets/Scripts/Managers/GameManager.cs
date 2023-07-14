using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameStateController stateController;
    public GraphicRaycaster graphicRaycaster;
    public static GameManager Instance;

    private void Start()
    {
        Instance = this;
    }

    private void Update()
    {
        stateController.UpdateActive();
    }
    private void FixedUpdate()
    {
        stateController.FixedUpdateActive();
    }




}
