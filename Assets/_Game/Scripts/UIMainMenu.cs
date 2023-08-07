using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainMenu : UICanvas
{
    public void PlayButton()
    {
        UIManager.Instance.gamePlay.Open();
        GameManager.Instance.StartGame();
        Close();
    }

    public void ShopButton()
    {
        UIManager.Instance.shop.Open();
        Close();
    }
}
