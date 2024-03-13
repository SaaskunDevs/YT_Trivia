using UnityEngine;
using System.Net.Sockets;
using System.Text;
using System.IO;
using System;

public class TCPClient : MonoBehaviour
{
    TcpClient client;
    public string ip;

    private void Start()
    {
     //   SendImage();
        return;

        client = new TcpClient(ip, 8080);
        NetworkStream stream = client.GetStream();
        string request = "Hola servidor";
        byte[] data = Encoding.ASCII.GetBytes(request);
        stream.Write(data, 0, data.Length);
        Debug.Log("Datos enviados al servidor.");
        client.Close();
    }

    private void Update()
    {
    //  if(Input.GetKeyDown(KeyCode.Space))
    //     {
    //         SendImage3();
    //     }
    }

    void SendImage()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "Img.jpg");
        byte[] imageBytes = File.ReadAllBytes(path);
        Debug.Log("Lengh " + imageBytes.Length);

        string base64String = System.Convert.ToBase64String(imageBytes);

        client = new TcpClient(ip, 8080);
        NetworkStream stream = client.GetStream();

        byte[] data = Encoding.ASCII.GetBytes(base64String);


        byte[] dataSize = BitConverter.GetBytes(data.Length);
        stream.Write(dataSize, 0, dataSize.Length);

        stream.Write(data, 0, data.Length);
        Debug.Log("Datos enviados al servidor." + data.Length);
        client.Close();
    }

    void SendImage2()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "Img.jpg");
        byte[] imageBytes = File.ReadAllBytes(path); //bytes de imagen default

        client = new TcpClient(ip, 8080);
        NetworkStream stream = client.GetStream();

        byte[] dataSize = BitConverter.GetBytes(imageBytes.Length);
        stream.Write(dataSize, 0, dataSize.Length); //manda datos de que tan grande es la imagen

        stream.Write(imageBytes, 0, imageBytes.Length);
        Debug.Log("Datos enviados al servidor." + imageBytes.Length);
        client.Close();
    }

    public void RegisterNewUser()
    {
        
    }

    void SendImage3() //funciona
    {
        string path = Path.Combine(Application.streamingAssetsPath, "Img.jpg");
        byte[] imageBytes = File.ReadAllBytes(path); //bytes de imagen default

        client = new TcpClient(ip, 8080);
        NetworkStream stream = client.GetStream();

        string datos = "Register|23|juan|perez|55555|juanito@ed�.com|"+imageBytes.Length.ToString();
        byte[] dataBytes = Encoding.UTF8.GetBytes(datos);


        stream.Write(dataBytes, 0, dataBytes.Length); //manda datos de que tan grande es la imagen

        stream.Write(imageBytes, 0, imageBytes.Length);
        Debug.Log("Datos enviados al servidor." + imageBytes.Length);
        client.Close();
    }


    void SendData(byte[] dataBytes, byte[] imgDataBytes, bool hasImage = false)
    {
        client = new TcpClient(ip, 8080);
        NetworkStream stream = client.GetStream();
        /*
        if(hasImage)
        {
            
            byte[] ImgdataSize = BitConverter.GetBytes(dataBytes.Length + imgDataBytes.Length); //tama�o de la imagen en bytes[
            stream.Write(ImgdataSize, 0, ImgdataSize.Length); //manda datos de que tan grande es la imagen

            byte[] data = Encoding.ASCII.GetBytes(dataBase64 + imgDataBase64);
            stream.Write(data, 0, data.Length);
            Debug.Log("Datos enviados al servidor." + data.Length);
            client.Close();
        }
        else
        {
            byte[] data = Encoding.ASCII.GetBytes(dataBase64);
            stream.Write(data, 0, data.Length);
            Debug.Log("Datos enviados al servidor." + data.Length);
            client.Close();
        }

       */
    }
}
