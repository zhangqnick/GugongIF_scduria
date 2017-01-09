using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{

    public Text TextChooseA;
    public Text TextChooseB;

    int a;
    int b;

    public void Update()
    {
        TextChooseA.text = a.ToString();
        TextChooseB.text = b.ToString();
    }

    public void SetChooseA()
    {
         a += 1;
    }
    public void SetChooseB()
    {
         b += GlobalManager.ChooseB;
    }
}
