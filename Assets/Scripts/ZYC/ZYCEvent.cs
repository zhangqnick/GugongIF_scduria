﻿using UnityEngine;
using System.Collections;

public class ZYCEvent : MonoBehaviour
{

    private ZYCNetController zycNetController;

    // Use this for initialization
    void Start()
    {
        zycNetController = GetComponent<ZYCNetController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (zycNetController.GetTPSharedInfo() == null)
            return;
        int n = zycNetController.TPSInfoCheckControlEvents();
        string str = "";

        if(n > 0)
        {
            for(int i = 0;i < n; i++)
            {
                str = zycNetController.TPSInfoGetControlEvent(i);
                //命令的接受出口
                exeCommand(str);
            }
            zycNetController.TPSInfoResetControlEvent();
        }
    }

    public void OnDisable()
    {
        if(zycNetController)
        {
            zycNetController.disconnectNetwork();
        }
        Application.Quit();
    }


    /// <summary>
    /// 命令出发的接口
    /// </summary>
    /// <param name="com"></param>
    public void exeCommand(string com)
    {

        string[] com_str;//命令的全体
        string[] com_name;//命令的报头
        string[] com_prm;//命令的尾部

        com_str = getSplitCommandStr(com);
        com_name = getCommandName(com_str[0].ToLower());
        com_prm = getParameterValues(com_str[1]);

        int isDebugMode = PlayerPrefs.GetInt("debug_mode");

        int num;
        int snum;

        switch (com_name[0])
        {
            case "send_message":
                zycNetController.sendMessage(com_str[1]);
                break;
            case "exit_app":
                if (isDebugMode == 1)
                    OnDisable();
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 将命令从：分离开来，indexof索引是从0开始;
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    string[] getSplitCommandStr(string str)
    {
        string[] split = new string[2];
        split[0] = str;
        split[1] = "";

        int n = str.IndexOf(":");
        if (0 < n)
        {
            split[0] = str.Substring(0, n);
            if (n == (str.Length - 1))
                split[1] = "";
            else
                split[1] = str.Substring(n + 1, str.Length - (n + 1));
        }
        return split;
    }

    string[] getCommandName(string str)
    {
        string[] split;
        split = str.Split(","[0]);
        return split;
    }

    string[] getParameterValues(string str)
    {
        string[] split;
        split = str.Split(","[0]);
        return split;
    }
}

