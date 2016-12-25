using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using UnityEngine.UI;
using System;

public class ZYC_Client : MonoBehaviour {

    Socket clientSocket = null;
    Thread thread = null;

    public InputField _usernameText;

    // Use this for initialization
    void Start () { 
        clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //这里的ip地址，端口号都是服务端绑定的相关数据。
        IPAddress ip = IPAddress.Parse("192.168.1.110");
        IPEndPoint endpoint = new IPEndPoint(ip,1025);
        try
        {
            clientSocket.Connect(endpoint);//链接有端口号与IP地址确定服务端.
        }
        catch (Exception e)
        {
            Debug.Log("clientConnect err:" +  e.Message);
        }
        //finally
        //{

        //}
 

        //客户端在接受服务端发送过来的数据是通过Socket 中的Receive方法，
        //该方法会阻断线程，所以我们自己为该方法创建了一个线程
        //thread = new Thread(ReceMsg);
        //thread.IsBackground = true;//设置后台线程
        //thread.Start();
    }


    /// <summary>
    /// 发送用户名
    /// </summary>
    public void SendMsg()
    {
        if(clientSocket.Connected)
        {
            string txtMsg = _usernameText.text;
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(txtMsg);
            byte[] newbuffer = new byte[buffer.Length + 1];//定义一个新数组
            newbuffer[0] = 0;//设置标识，表示发送的是字符串
            Buffer.BlockCopy(buffer, 0, newbuffer, 1, buffer.Length);//源数组中的数据拷贝到新数组中
            clientSocket.Send(newbuffer);//发送新数组中的数据
        }

     }
}
