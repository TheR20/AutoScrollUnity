using System.Collections.Generic;
using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tienda : MonoBehaviour
{
   

    private bool isPlayerInRange;
    public GameObject feedbackRect;                // GameObject del rectángulo con la letra "F"
    private StarterAssetsInputs starterAssetsInputs;
    public GameObject MenuTienda;
    [SerializeField] private ScrollViewBuy scrollViewSampleSave;


    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            starterAssetsInputs = player.GetComponent<StarterAssetsInputs>();
        }
    }

    void Update()
    {
        if (isPlayerInRange && starterAssetsInputs.circulo)
        {
            isPlayerInRange = false;
            feedbackRect.SetActive(false);
            MenuTienda.SetActive(true);
            starterAssetsInputs.circulo = false;
            
            Time.timeScale = 0f;

             scrollViewSampleSave.SelectFirstButton();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            feedbackRect.SetActive(true); // Mostrar el rectángulo con "F" cuando entra en colisión
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            feedbackRect.SetActive(false); // Ocultar el rectángulo con "F" cuando sale de la colisión
        }
    }
}
