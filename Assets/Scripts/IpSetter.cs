using System.Collections;
using UnityEngine;

public class IpSetter : MonoBehaviour
{
    public string serverIP;

    void Start()
    {
        TCPEmiter.localIP = GetLocalIPAddress();
        TCPEmiter.ip = serverIP;

    }

    string GetLocalIPAddress()
    {
        var host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        throw new System.Exception("Local IP Address Not Found!");
    }
}
