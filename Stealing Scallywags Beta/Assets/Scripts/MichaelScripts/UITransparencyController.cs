using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITransparencyController : MonoBehaviour
{

    private Image playerOneLocation, playerTwoLocation, playerThreeLocation, playerFourLocation;

    void Start()
    {

        foreach (Image image in Resources.FindObjectsOfTypeAll(typeof(Image)) as Image[])
        {
            //Debug.Log(image);
            if (image.gameObject.name == "Player1Area")
                playerOneLocation = image;
            if (image.gameObject.name == "Player2Area")
                playerTwoLocation = image;
            if (image.gameObject.name == "Player3Area")
                playerThreeLocation = image;
            if (image.gameObject.name == "Player4Area")
                playerFourLocation = image;

        }

        Debug.Log (playerOneLocation.name + " " + playerTwoLocation.name + " " + playerThreeLocation.name + " " + playerFourLocation.name);
    }

    // Update is called once per frame
    void Update()
    {
        var screenPoint = Camera.main.ViewportToScreenPoint(this.transform.position);
        Debug.Log("Screen Point: " + screenPoint);

        if (RectTransformUtility.RectangleContainsScreenPoint(playerOneLocation.rectTransform, screenPoint))
            Debug.Log ("Michael is a smart man sometimes because I am currently working :D");

        if (RectTransformUtility.RectangleContainsScreenPoint(playerTwoLocation.rectTransform, screenPoint))
            Debug.Log("Michael is a smart man sometimes because I am currently working :D");

        if (RectTransformUtility.RectangleContainsScreenPoint(playerThreeLocation.rectTransform, screenPoint))
            Debug.Log("Michael is a smart man sometimes because I am currently working :D");

        if (RectTransformUtility.RectangleContainsScreenPoint(playerFourLocation.rectTransform, screenPoint))
            Debug.Log("Michael is a smart man sometimes because I am currently working :D");

    }
}
