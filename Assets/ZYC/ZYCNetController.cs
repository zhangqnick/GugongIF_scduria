using UnityEngine;
using System.Collections;

public class ZYCNetController : MonoBehaviour
{

    /// <summary>
    /// 是否自动连接
    /// </summary>
    public bool autoConnect = false;

    /// <summary>
    ///  客户端连接
    /// </summary>
    private TPNetworkClient m_tpNetworkClient = null;

    private TPSharedInfo m_tpShardInfo = null;

    private string m_connection = "";

    private bool autoConnectBak;
    // Use this for initialization
    void Awake()
    {
        autoConnectBak = autoConnect;
        PlayerPrefs.SetString("ip_address", "192.167.1.199");
        PlayerPrefs.SetString("port_number", "28000");
        PlayerPrefs.SetInt("debug_mode", 1);
    }

    public void Start()
    {
        if (PlayerPrefs.GetInt("auto_connect") == 1)
            autoConnect = true;
        else
            autoConnect = false;
    }



    /// <summary>
    /// 自动连接的出口
    /// </summary>
    void Update()
    {
        if (autoConnect)
        {
            if (isConnectNetwork() == false)
            {
                connectNetwork();
            }
        }
    }

    /// <summary>
    /// 判断是否连接
    /// </summary>
    /// <returns></returns>
    bool isConnectNetwork()
    {
        if (m_tpNetworkClient == null)
        {
            m_connection = "Disconnected";
            return false;
        }
        if (m_tpShardInfo == null)
        {
            m_connection = "Disconnected";
            return false;
        }

        m_connection = m_tpShardInfo.IsConnected() ? "Connected" : "Disconnected";
        if (m_connection == "Connected")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void connectNetwork()
    {
        if (m_connection == "Connected")
            return;

        m_tpNetworkClient = new TPNetworkClient();
        if (m_tpNetworkClient.Initialize())
        {
            m_tpShardInfo = new TPSharedInfo();
            bool connected = false;
            //注意此端口加一
            connected = m_tpShardInfo.Initialize(m_tpNetworkClient.GetIPAddress(), m_tpNetworkClient.GetPort() + 1);

            m_connection = connected ? "Connected" : "Disconnected";
        }
        else
        {
            m_connection = "Disconnected";
        }
    }

    /// <summary>
    /// 向服务器发送消息
    /// </summary>
    /// <param name="mstr"></param>
    public void sendMessage(string mstr)
    {
        if (isConnectNetwork() == false)
            return;
        if(mstr != "")
        {
            if(m_tpNetworkClient != null)
            {
                m_tpNetworkClient.Send(mstr);
            }
        }
    }

    public void disconnectNetwork()
    {
        if(m_tpNetworkClient != null)
        {
            m_tpNetworkClient.Terminate();
            m_tpNetworkClient = null;
        }
        if(m_tpShardInfo != null)
        {
            m_tpShardInfo.Terminate();
            m_tpShardInfo = null;
        }

        m_connection = "Disconnected";
    }

    /// <summary>
    /// 得到TPSharedInfo是否有信息
    /// </summary>
    /// <returns></returns>
    public TPSharedInfo GetTPSharedInfo()
    {
        return m_tpShardInfo;
    }

    /// <summary>
    /// 得到命令的数量
    /// </summary>
    /// <returns></returns>
    public int TPSInfoCheckControlEvents()
    {
        if (m_tpShardInfo == null || m_connection == "Disconnected")
            return 0;
        return m_tpShardInfo.CheckControlEvents();
    }

    /// <summary>
    /// 得到的命令内容
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    public string TPSInfoGetControlEvent(int i)
    {
        if (m_tpShardInfo == null || m_connection == "Disconnected")
            return null;
        return m_tpShardInfo.GetControlEvent(i);
    }


    /// <summary>
    /// 命令的销毁与清除
    /// </summary>
    public void TPSInfoResetControlEvent()
    {
        if (m_tpShardInfo == null || m_connection == "Disconnected")
            return;
        m_tpShardInfo.ResetControlEvent();
    }
}
