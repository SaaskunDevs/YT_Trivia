using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
public static class TCPEmiter
{
    private static TcpClient client;
    public static string ip = ""; 
    public static string localIP = ""; 
    private static int port = 8080;

    /// <summary>
    /// Registro de usuario nuevo
    /// </summary>
    /// <param name="ID"></param>
    /// <param name="name"></param>
    /// <param name="lastname"></param>
    /// <param name="phone"></param>
    /// <param name="mail"></param>
    /// <param name="imageLenght"></param>
    /// <param name="imageBytes"></param>
    public static void RegisterUser(string ID, string nickname, string imageLenght, byte[] imageBytes)
    {
        try
        {
            client = new TcpClient(ip, port);
            NetworkStream stream = client.GetStream();

            string datos = "Register|"+ ID + "|" + nickname + "|" + imageLenght + "|";
            datos = datos.PadRight(250, '*');
            Debug.Log("Mandando: " + datos);
            byte[] dataBytes = Encoding.UTF8.GetBytes(datos);

            stream.Write(dataBytes, 0, dataBytes.Length);
            stream.Write(imageBytes, 0, imageBytes.Length);
            client.Close();

            Debug.Log("Mensaje enviado");
        }
        catch (Exception e)
        {
            Debug.Log("Error: " + e.ToString());
        }
    }

    public static void SendHostEntered(string player, bool hostEntered)
    {
        try
        {
            client = new TcpClient(ip, port);
            NetworkStream stream = client.GetStream();

            string datos = "Entered|" + player + "|" + hostEntered + "|";
            byte[] dataBytes = Encoding.UTF8.GetBytes(datos);

            stream.Write(dataBytes, 0, dataBytes.Length);
            client.Close();

            Debug.Log("Mensaje enviado");
        }
        catch (Exception e)
        {
            Debug.Log("Error: " + e.ToString());
        }
    }

    public static void SendClientEntered(string player, bool clientEntered)
    {
        try
        {
            client = new TcpClient(ip, port);
            NetworkStream stream = client.GetStream();

            string datos = "Entered|" + player + "|" + clientEntered + "|";
            byte[] dataBytes = Encoding.UTF8.GetBytes(datos);

            stream.Write(dataBytes, 0, dataBytes.Length);
            client.Close();

            Debug.Log("Mensaje enviado");
        }
        catch (Exception e)
        {
            Debug.Log("Error: " + e.ToString());
        }
    }
}
