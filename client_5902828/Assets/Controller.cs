using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SocketIO;

public class Controller : MonoBehaviour
{

    static SocketIOComponent socket;

    string me;
    public Text Myinput, showPlayerName,thewin;
    public InputField input;
    // Start is called before the first frame update
    void Start()
    {
        socket = GetComponent<SocketIOComponent>();
        socket.On("open", OnConnected);
        socket.On("go", Go);
        socket.On("GetBack", Get);
        socket.On("over", Over);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown("enter"))
        {
            Sent();
        }
    }

    void Sent()
    {
        int num = int.Parse(Myinput.text);

        JSONObject js = new JSONObject(JSONObject.Type.NUMBER);
        js.AddField("num", num);
        js.AddField("name", me);
        socket.Emit("go", js);
        thewin.text = "";
        Myinput.text = "";
        input.text = "";
 
    }
    void OnConnected(SocketIOEvent e)
    {
        print("HasConnect");
        showPlayerName.text ="You Are Player "+ e.data.GetField("name").ToString();
        me = "Player " + e.data.GetField("name").ToString() + " Win";
    }
    void Over(SocketIOEvent e)
    {
          
       thewin.text =  e.data.GetField("data").ToString() ;

    }
    void Go(SocketIOEvent e)
    {
      
        thewin.text = "You Are Win";
    }
    void Get(SocketIOEvent e)
    {
 
       
        string a = e.data.GetField("data").ToString();
        thewin.text = a;

    }


}
