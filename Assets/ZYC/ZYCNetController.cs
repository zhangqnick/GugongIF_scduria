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
}
