using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine.UI;
using System;

public class TCPReceiver : MonoBehaviour
{
    [SerializeField] HostGameplay _hostGameplay;
    [SerializeField] ClientManager _clientManager;
    string ip = "192.168.0.240";
    TcpListener server;
    TcpClient client;
    private bool isRuning = true;
    Thread t;

    #region delegados
    public delegate void RegisterDelegate(string player, bool clientEntered);
    public static event RegisterDelegate OnRegister;

    #endregion

    private void Start()
    {
        _hostGameplay = GetComponent<HostGameplay>();
        server = new TcpListener(IPAddress.Parse(ip), 8080);
        server.Start();
        Debug.Log("Servidor iniciado en el puerto 8080." + IPAddress.Parse(ip));
        t = new Thread(new ThreadStart(ServerThread));
        t.Start();
    }

    void ServerThread()
    {
        while (isRuning)
        {
            // Acepta una nueva conexión
            client = server.AcceptTcpClient();
            NetworkStream stream = client.GetStream();

            Debug.Log("Data received");

            byte[] dataSize = new byte[250];  // Tama�o de un int en bytes.
            stream.Read(dataSize, 0, dataSize.Length);

            int dataLength = BitConverter.ToInt32(dataSize, 0);

            string dataString = Encoding.UTF8.GetString(dataSize);
           // Debug.Log("Tama�o de datos mandado desde el cliente: " + dataLength );
            Debug.Log("Texto de server " + dataString);

            string[] split = dataString.Split("|");

            if (split[0] == "Entered")
            {
                CheckMessageData(split[1], split[2]);
            }
            if (split[0] == "QuestionIndex")
            {
                CheckMessageData(split[1], split[2]);
            }
        }
    }

    void CheckMessageData(string player, string client)
    {

        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
            Debug.Log("LLegada mensaje al receiver " + player + " mensaje: " + client);
            switch (player)
            {
                case "HostEntered":
                    Debug.Log("HostEntered server");
                    // OnRegister?.Invoke(player, Convert.ToBoolean(client));
                    break;
                case "ClientEntered":
                    _hostGameplay.ClientEntered();
                    Debug.Log("ClientEntered server");
                    break;
                case "ChangeToInstructions":
                    _clientManager.InstructionUI();
                    Debug.Log("ChangeToInstructions server");
                    break;
                case "ReadyToPlay":
                    Debug.Log("ReadyToPlay server");
                    break;
                case "Questions":
                    _hostGameplay.GetQuestions(Convert.ToInt32(client));
                    Debug.Log("QuestionIndex server");
                    break;
                default:
                    break;
            }
        });

    }

    private void OnDisable()
    {
        t.Abort();
        isRuning = false;
        server.Stop();
    }
}
