using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.Threading;
using UnityEngine.UI;
using System.Net;
using System;

public class ZYC_Server : MonoBehaviour
{
    /// <summary>
    /// 主机ip
    /// </summary>
    private IPAddress _homeIp;
    Socket sck = null;
    public UIManager uiMananger;

    ArrayList msgList;

    public void Start()
    {
        string name = Dns.GetHostName();
        IPAddress[] ipadrlist = Dns.GetHostAddresses(name);
        foreach (IPAddress ipa in ipadrlist)
        {
            if (ipa.AddressFamily == AddressFamily.InterNetwork)
                _homeIp = ipa;
        }

        msgList = new ArrayList();
    }


    /// <summary>
    /// 开启服务器
    /// </summary>
    public void starServer()
    {
        sck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        try
        {
            //IPAddress ip = IPAddress.Parse("192.168.1.110");
            IPEndPoint endpoint = new IPEndPoint(_homeIp, 1025);

            sck.Bind(endpoint);
            sck.Listen(54);

            Debug.Log("开始监听");

            Thread thread = new Thread(ConnectServer);
            //设置为后台线程
            thread.IsBackground = true;
            thread.Start();
        }
        catch (Exception e)
        {
            Console.WriteLine("Winsock error: " + e.ToString());
        }

    }

    //Socket服务监听函数
    void ConnectServer()
    {
        while (true)
        {
            //创建一个接收客户端通信的Socket
            Socket accSck = sck.Accept();
            //如果监听到客户端有链接，则运行到下一部，提示，链接成功！
            Debug.Log("链接成功！" + accSck.RemoteEndPoint.ToString());

            Thread thread = new Thread(new ParameterizedThreadStart(RecevieMsg));
            thread.IsBackground = true;
            thread.Start(accSck);
        }

    }


    /// <summary>
    /// 接收信息
    /// </summary>
    /// <param name="socket"></param>
    void RecevieMsg(object socket)
    {
        Socket newSocket = socket as Socket;//转成对应的Socket类型
        while (true)
        {
            byte[] buffer = new byte[1024 * 1024 * 2];
            int receiveLength = -1;
            try  //由于Socket中的Receive方法容易抛出异常，所以我们在这里要捕获异常。
            {
                receiveLength = newSocket.Receive(buffer);//接收从客户端发送过来的数据
            }
            catch (SocketException ex)//注意：在捕获异常时，先确定具体的异常类型。
            {
                Debug.Log("出现了异常:" + ex.Message);
                //socketDir.Remove(newSocket.RemoteEndPoint.ToString());//如果出现了异常，将该Socket实例从集合中移除
                //lsb_Ips.Items.Remove(newSocket.RemoteEndPoint.ToString());
                break;//出现异常以后，终止整个循环的执行
            }
            catch (Exception ex)
            {
                Debug.Log("出现了异常:" + ex.Message);
                break;
            }

            if (buffer[0] == 0)//表示是否是用户名
            {
                string str = System.Text.Encoding.UTF8.GetString(buffer, 1, receiveLength - 1);//注意，是从下标为1的开始转成字符串，为0的是标识。
                Debug.Log("用户名是：" + str);

                ZYC_SocketMaster.socketDir.Add(str, newSocket);
            }
            if (buffer[0] == 1)//表示投票方式
            {
                string str = System.Text.Encoding.UTF8.GetString(buffer, 1, receiveLength - 1);//注意，是从下标为1的开始转成字符串，为0的是标识。

                ////
                ////这个是可以改动的
                switch (str)
                {
                    case "right":
                       uiMananger.SetChooseB();
                        Debug.Log("right");
                        break;
                    case "left":
                        uiMananger.SetChooseA();
                        Debug.Log("left");
                        break;
                    case "end":
                        if (GlobalManager.ChooseA > GlobalManager.ChooseB)
                            Debug.Log("left win");
                        else
                            Debug.Log("right win");
                        break;
                }
            }
        }
    }


    public void startcot()
    {
        StartCoroutine(Corout());
    }

    IEnumerator Corout()
    {
        yield return new WaitForSeconds(5);
        Debug.Log("end" + GlobalManager.ChooseA);
    }
}
