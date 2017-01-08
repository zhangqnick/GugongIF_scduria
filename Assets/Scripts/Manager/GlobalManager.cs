using UnityEngine;
using System.Collections;

public class  GlobalManager : Singleton<GlobalManager> {

    private int _chooseA;

    public int ChooseA
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

    private int _chooseB;
    public int ChooseB
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
