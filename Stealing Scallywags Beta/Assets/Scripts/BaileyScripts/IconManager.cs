using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconManager : MonoBehaviour
{
    public List<IconAllocation> objectList = new List<IconAllocation>();
    public List<PlayerIcon> iconList = new List<PlayerIcon>();
    public List<Sprite> iconSprite = new List<Sprite>();



    public void AddIconList()
    {
        for(int i = 0; i < objectList.Count; i++)
        {
            iconList.Add(new PlayerIcon(objectList[i].playerIcon, objectList[i].gunIcon, objectList[i].swordIcon, objectList[i].bombIcon));
        }
    }
}
public class PlayerIcon
{
    public Image playerIcon;
    public Image gunIcon;
    public Image swordIcon;
    public Image bombIcon;
    public PlayerIcon(Image p, Image g, Image s, Image b)
    {
        playerIcon = p;
        gunIcon = g;
        swordIcon = s;
        bombIcon = b;
    }
}