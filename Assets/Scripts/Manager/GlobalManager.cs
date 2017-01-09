using UnityEngine;
using System.Collections;

public class  GlobalManager : Singleton<GlobalManager> {

    private static int _chooseA;

    public static int ChooseA
    {
        set
        {
            _chooseA = value;
        }
        get
        {
            return _chooseA;
        }
    }

    private static int _chooseB;
    public static int ChooseB
    {
        get
        {
            return _chooseB;
        }
        set
        {
            _chooseB = value;
        }
    }



}
