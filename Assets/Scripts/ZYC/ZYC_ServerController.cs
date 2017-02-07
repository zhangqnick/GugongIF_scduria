using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ZYC_ServerController : MonoBehaviour
{


    private ZYC_Server zyc_server;

    public void Update()
    {
        int n = ZYCServerMsgEvents();
        string str = "";

        if(n > 0)
        {
            for(int i = 0; i < n;i++)
            {
                str = ZYCServerGetMsg(i);
            }
            ZYCServerReset();
        }

    }

    public void Start()
    {
        zyc_server = new ZYC_Server();
        zyc_server.starServer();
    }

    #region 获得内容三部曲
    public int ZYCServerMsgEvents()
    {
        return zyc_server.CheckMsgEvents();
    }

    public string ZYCServerGetMsg(int i)
    {
        return zyc_server.GetMsgEvents(i);
    }

    public void ZYCServerReset()
    {
        zyc_server.ResetListMsg();
    }
    #endregion



    ///// <summary>
    ///// 得到命令的数量
    ///// </summary>
    ///// <returns></returns>
    //public int TPSInfoCheckControlEvents()
    //{
    //    if (m_tpShardInfo == null || m_connection == "Disconnected")
    //        return 0;
    //    return m_tpShardInfo.CheckControlEvents();
    //}

    ///// <summary>
    ///// 得到的命令内容
    ///// </summary>
    ///// <param name="i"></param>
    ///// <returns></returns>
    //public string TPSInfoGetControlEvent(int i)
    //{
    //    if (m_tpShardInfo == null || m_connection == "Disconnected")
    //        return null;
    //    return m_tpShardInfo.GetControlEvent(i);
    //}


    ///// <summary>
    ///// 命令的销毁与清除
    ///// </summary>
    //public void TPSInfoResetControlEvent()
    //{
    //    if (m_tpShardInfo == null || m_connection == "Disconnected")
    //        return;
    //    m_tpShardInfo.ResetControlEvent();
    //}


}
