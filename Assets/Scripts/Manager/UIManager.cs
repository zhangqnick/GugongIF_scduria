using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{

    public Text TextChooseA;
    public Text TextChooseB;

    int a = 0;
    int b = 0;

    public void Update()
    {
        if (a != GlobalManager.ChooseA)
        {
            a = GlobalManager.ChooseA;
            TextChooseA.text = a.ToString();

        }
        if (b != GlobalManager.ChooseB)
        {
            b = GlobalManager.ChooseB;
            TextChooseB.text = b.ToString();
        }
    }

    public void SetChooseA()
    {
         GlobalManager.ChooseA++;
    }
    public void SetChooseB()
    {
         GlobalManager.ChooseB++;
    }
}
