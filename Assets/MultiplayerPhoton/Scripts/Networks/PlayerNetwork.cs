using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerNetwork : MonoBehaviour {
    
    public List<Vector3> listInt = new List<Vector3>() {
        new Vector3(114.05f, 4.06f, 317.73f), new Vector3(111.71f, 4.06f, 316.17f),
        new Vector3(111.96f, 4.06f, 321.13f), new Vector3(109.68f, 4.06f, 319.74f),
        new Vector3(110.02f, 4.06f, 316.33f), new Vector3(108.68f, 4.06f, 318.46f),
        new Vector3(114.66f, 4.06f, 319.01f), new Vector3(112.84f, 4.06f, 321.33f) };
    public List<Quaternion> list = new List<Quaternion>() {
        Quaternion.Euler(0, 327.661f, 0), Quaternion.Euler(0, 327.661f, 0),
        Quaternion.Euler(0, 147.851f, 0), Quaternion.Euler(0, 147.851f, 0),
        Quaternion.Euler(0, 419.072f, 0), Quaternion.Euler(0, 419.072f, 0),
        Quaternion.Euler(0, 595.335f, 0), Quaternion.Euler(0, 595.335f, 0) };

    public List<Vector3> male04 = new List<Vector3>() {
        new Vector3(114.36f, 3.983f, 317.41f), new Vector3(111.71f, 3.983f, 315.84f),
        new Vector3(111.73f, 3.983f, 321.55f), new Vector3(109.38f, 3.983f, 320.11f),
        new Vector3(109.86f, 3.983f, 315.82f), new Vector3(108.35f, 3.983f, 318.27f),
        new Vector3(114.93f, 3.983f, 319.196f), new Vector3(113.27f, 3.983f, 321.49f) };
    public List<Vector3> female1_3 = new List<Vector3>() {
        new Vector3(114.69f, 4.02f, 317.03f), new Vector3(112.16f, 4.02f, 315.29f),
        new Vector3(111.426f, 4.02f, 321.93f), new Vector3(109.02f, 4.02f, 320.47f),
        new Vector3(109.335f, 4.02f, 315.601f), new Vector3(107.76f, 4.02f, 318.09f),
        new Vector3(115.43f, 4.02f, 319.58f), new Vector3(113.771f, 4.02f, 321.836f) };
    public List<Vector3> male_6_3 = new List<Vector3>() {
        new Vector3(114.53f, 4.02f, 317.13f), new Vector3(112.24f, 4.02f, 315.58f),
        new Vector3(111.57f, 4.02f, 321.79f), new Vector3(109.09f, 4.02f, 320.33f),
        new Vector3(109.45f, 4.02f, 315.79f), new Vector3(107.893f, 4.02f, 318.132f),
        new Vector3(115.16f, 4.02f, 319.58f), new Vector3(113.6f, 4.02f, 321.836f) };

    public List<Quaternion> rot = new List<Quaternion>() {
        Quaternion.Euler(0, -38.174f, 0), Quaternion.Euler(0, -38.174f, 0),
        Quaternion.Euler(0, 147.851f, 0), Quaternion.Euler(0, 147.851f, 0),
        Quaternion.Euler(0, 419.072f, 0), Quaternion.Euler(0, 419.072f, 0),
        Quaternion.Euler(0, 595.335f, 0), Quaternion.Euler(0, 595.335f, 0) };

    public Hashtable PlayerVotesMap = new Hashtable();
    public int countVotes = 0;
    public Sprite V;
    public Sprite L;
    public Sprite S;
    public Sprite VO;
    public Sprite SA;
    public bool gameended = false;
    float timer;
    public bool startturn=false;
    public bool mevotedJour = false;
    public static PlayerNetwork Instance;
    private PhotonView PhotonView;
    private int PlayersInGame = 0;
    public string PlayerName { get; set; }
    public string PlayerRole { get; set; }
    private ExitGames.Client.Photon.Hashtable m_playerCustomProperties = new ExitGames.Client.Photon.Hashtable();
    private Coroutine m_pingCoroutine;
    public bool exittest=false;
    GameObject startingsound;
    public static bool bvo= false,bso=false,bsa=false;
    Image myrole;
    private int number;
    public int Tokill=0;
    private bool gfff = false;
    public bool iskill=false;
    public bool votedenuit=false;
    public int ProtectedBySalvator=0;
    public bool sorcpower = true;
    public bool magic= false;
    private bool medtest = false;
    private GameObject spawnpoint;
    public bool mevoted = false;
    public bool re;
    public bool votedujour = false;
    Hashtable hashtable = new Hashtable();
    float timeLeft =5, timeLeftvo = 5, timeLeftsa=5, timeLeftso=5, timeLeftni=5, timeLeftmed=5, timeLefttodie=5,timeLefttodie1=5;
    private void Awake()
    {
        Instance = this;
        PhotonView = GetComponent<PhotonView>();
        PlayerName = "Player#" + Random.Range(1000, 9999);
        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }
    [PunRPC]
    public void ChooseRoles()
    {

       // Debug.Log("i'm" + PhotonNetwork.player.ID);
        PlayerRole = CurrentRoomCanvas.roles[PhotonNetwork.player.ID - 1];
      //  Debug.Log(PlayerRole);

    }
    public void Update()
    {
    
        if (startturn == true && gameended == false)
        {
          //  startturn = false;
            round();
        
    }

    }
    public void VoteForPlayer(string name)
    {
        mevoted = true;
        PhotonView.RPC("VoteForPlayer_RPC", PhotonTargets.AllBuffered, PhotonNetwork.player, name);
    }

    [PunRPC]
    private void VoteForPlayer_RPC(PhotonPlayer p, string name)
    {

        int nb = int.Parse(PlayerVotesMap[name].ToString());
        PlayerVotesMap[name] = nb + 1;
        countVotes += 1;
        int l=0;
        Debug.Log("num votes = " + countVotes);
        PhotonPlayer[] listpl = PhotonNetwork.playerList;
        foreach (var item in listpl)
        {
            string f = item.CustomProperties["Role"] as string;
            if (f.Equals("L"))
            {
                l++;
            }
        }
        Debug.Log("number of wolfes =" + l);
            if (countVotes == l)
        {

            KillPlayerMax();
            VoteManager.instance.VoteIsFinish();
            
        }
    }

    public void VoteForPlayerJour(string name)
    {
        mevotedJour = true;
        PhotonView.RPC("VoteForPlayerJour_RPC", PhotonTargets.AllBuffered, PhotonNetwork.player, name);
    }

    [PunRPC]
    private void VoteForPlayerJour_RPC(PhotonPlayer p, string name)
    {

        int nb = int.Parse(PlayerVotesMap[name].ToString());
        PlayerVotesMap[name] = nb + 1;
        countVotes += 1;
        int l = 0;
        Debug.Log("num votes = " + countVotes);
        PhotonPlayer[] listpl = PhotonNetwork.playerList;
        l = listpl.Length;
        Debug.Log("number of players =" + l);
        if (countVotes == l)
        {

          
            KillPlayerMaxJour();
            VoteManager.instance.VoteIsFinish();
            

        }
    }


    void KillPlayerMaxJour()
    {
        int max = 0;
        int IdPlayerToKill = 0;
        foreach (string key in PlayerVotesMap.Keys)
        {
            Debug.Log(string.Format("{0}: {1}", key, PlayerVotesMap[key]));
        }
        foreach (DictionaryEntry entry in PlayerVotesMap)
        {
            if (max < int.Parse(entry.Value.ToString()))
            {
                max = int.Parse(entry.Value.ToString());
                IdPlayerToKill = int.Parse(entry.Key.ToString());
            }
        }
        Debug.Log(" gonna kill" + IdPlayerToKill);
        if ((IdPlayerToKill != ProtectedBySalvator) || ((IdPlayerToKill != Tokill) && (iskill == false)))
        {
            string xxx = "";
            foreach (var item in PhotonNetwork.playerList)
            {
                if (item.ID == IdPlayerToKill )
                {
                    xxx = item.NickName;
                }
            }
            StartCoroutine(diemsg(xxx));
            foreach (var item in PhotonNetwork.playerList)
            {
                
                if (item.ID == IdPlayerToKill && (PhotonNetwork.isMasterClient))
                {
                    PhotonNetwork.DestroyPlayerObjects(item);
                    PhotonNetwork.CloseConnection(item);
                }
                if (item.ID == IdPlayerToKill && (item.ID == PhotonNetwork.player.ID)  )
                {

                    PlayerPrefs.SetInt("kill", 1);

                    PhotonNetwork.Destroy(PlayerNetwork.Instance.PhotonView);
                    Application.LoadLevel(2);

                }
            }
            resetallvals();
          

        }
        countVotes = 0;
        votedujour = true;

        // Kill Player With ID
    }


    
    void KillPlayerMax()
    {
        int max = 0;
        int IdPlayerToKill=0;
        foreach (string key in PlayerVotesMap.Keys)
        {
            Debug.Log(string.Format("{0}: {1}", key, PlayerVotesMap[key]));
        }
        foreach (DictionaryEntry entry in PlayerVotesMap)
        {
            if (max < int.Parse(entry.Value.ToString()))
            {
                max = int.Parse(entry.Value.ToString());
                IdPlayerToKill = int.Parse(entry.Key.ToString());
            }
        }
        Debug.Log(" gonna kill" + IdPlayerToKill);
        if(   (IdPlayerToKill != ProtectedBySalvator) ||   ((IdPlayerToKill != Tokill)  && (iskill == false)) )
        {
            string xxx = "";
            foreach (var item in PhotonNetwork.playerList)
            {
                if (item.ID == IdPlayerToKill)
                {
                    xxx = item.NickName;
                }
            }
            StartCoroutine(diemsg(xxx));
            foreach (var item in PhotonNetwork.playerList)
            {
                if (item.ID == IdPlayerToKill && PhotonNetwork.isMasterClient)
                {

                        
                        PhotonNetwork.DestroyPlayerObjects(item);
                        PhotonNetwork.CloseConnection(item);
                   

                }
                if (item.ID == IdPlayerToKill && (item.ID == PhotonNetwork.player.ID))
                {
                    PlayerPrefs.SetInt("kill", 1);
                    PhotonNetwork.Destroy(PlayerNetwork.Instance.PhotonView);
                    Application.LoadLevel(2);
                    


                }
            }
         

        }
        countVotes = 0;
        votedenuit = true;

    }

    public void delaymsg(string msg)
    {
        Camera.main.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = msg;
    }
    public void round()
    {
        int l = 0;
        int v = 0;
        bool vo = false;
        int idvo= 0;
        bool sa = false;
        int idsa=0;
        bool so = false;
        int idso=0;
        PhotonPlayer[] listpl = PhotonNetwork.playerList;
        Debug.Log("SIze List : " + listpl.Length);
        foreach (var item in listpl)
        {
            string f = item.CustomProperties["Role"] as string;
            if (f.Equals("L"))
            {
                l++;
            }
            else
            {
                v++;
            }
            if (f.Equals("VVOYANTE"))
            {
                vo = true;
                idvo = item.ID;
            }
            if (f.Equals("VSALVATEUR"))
            {
                sa = true;
                idsa = item.ID;
            }
            if (f.Equals("VSORCIERE"))
            {
                so = true;
                idso = item.ID;
            }

        }
        Debug.Log("nb Loup " + l);
        Debug.Log("nb Villageois " + v);
        if (l==0 || v == 0)
        {
            gameended = true;

            if (exittest == false)
            {
                exittest = true;
                RPC_victory();
                //PhotonView.RPC("RPC_victory", PhotonTargets.AllBuffered);
            }
            Debug.Log("wfe etor7");
        }


        if(vo == true && bvo == false && gameended == false)
        {
            delaymsg("The Seer wakes up, and designates a player whose true personality she wants to probe");

            timeLeftvo -= Time.deltaTime;

            if (timeLeftvo < 0)
            {
                delaymsg("");
                //choice voyante
                if (PhotonNetwork.player.ID == idvo)
                {
                    GameObject pp = GameObject.FindGameObjectWithTag("paper");
                    GameObject voCanvas = pp.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
                    GameObject voCanvas1 = pp.transform.GetChild(0).gameObject;
                    voCanvas1.SetActive(true);
                    voCanvas.transform.LookAt(new Vector3(listInt[idvo - 1].x, voCanvas.transform.position.y, listInt[idvo - 1].z));
                    voCanvas.transform.Rotate(new Vector3(0, 180, 0));
                }
            }
        }
        if (sa == true && (bvo == true || idvo ==0) && bsa == false && gameended == false)
        {
            delaymsg("The Salvator wakes up, and chooses someone to protect");
            timeLeftsa -= Time.deltaTime;

            if (timeLeftsa < 0)
            {
                delaymsg("");
                //vote salva
                Debug.Log("ok");
                if (PhotonNetwork.player.ID == idsa)
                {
                    GameObject pp = GameObject.FindGameObjectWithTag("papersa");
                    GameObject SaCanvas = pp.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
                    GameObject SaCanvas1 = pp.transform.GetChild(0).gameObject;
                    SaCanvas1.SetActive(true);
                    Debug.Log("ok another time");
                    SaCanvas.transform.LookAt(new Vector3(listInt[idsa - 1].x, SaCanvas.transform.position.y, listInt[idsa - 1].z));
                    SaCanvas.transform.Rotate(new Vector3(0, 180, 0));
                }
            }
        }
        if (so == true && (bvo == true || idvo == 0) && (bsa == true || idsa == 0) && bso == false && sorcpower == true && gameended == false)
        {


            delaymsg("The Witch wakes up, I show her the victim of the Werewolves.Will she use her healing potion, or poisoning ? ");
            timeLeftso -= Time.deltaTime;

            if (timeLeftso < 0)
            {
                delaymsg("");
                //choice soTrcT
                if (PhotonNetwork.player.ID == idso)
                {
                    GameObject pp = GameObject.FindGameObjectWithTag("paperso");
                    GameObject SaCanvas = pp.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
                    GameObject SaCanvas1 = pp.transform.GetChild(0).gameObject;
                    SaCanvas1.SetActive(true);
                    Debug.Log(" another time");
                    SaCanvas.transform.LookAt(new Vector3(listInt[idso - 1].x, SaCanvas.transform.position.y, listInt[idso - 1].z));
                    SaCanvas.transform.Rotate(new Vector3(0, 180, 0));
                }


            }
        }

        if (so == true && (bvo == true || idvo == 0) && (bsa == true || idsa == 0) && bso == true && magic == false && gameended == false)
        {
            //kill by sorc
            magic = true;
            if(iskill)
            {
             if(Tokill != ProtectedBySalvator) {
                    string xxx = "";
                    foreach (var item in PhotonNetwork.playerList)
                    {
                        if (item.ID == Tokill)
                        {
                            xxx = item.NickName;
                        }
                    }
                    StartCoroutine(diemsg(xxx));
                    foreach (var item in PhotonNetwork.playerList)
                    {

                        if (item.ID == Tokill && PhotonNetwork.isMasterClient)
                        {
                            PhotonNetwork.DestroyPlayerObjects(item);
                            PhotonNetwork.CloseConnection(item);
                        }

                            if (item.ID == Tokill && (item.ID == PhotonNetwork.player.ID))
                        {
                            PlayerPrefs.SetInt("kill", 1);
                            PhotonNetwork.Destroy(PlayerNetwork.Instance.PhotonView);
                            Application.LoadLevel(2);

                        }



                    }
                    
                }

            }
        }


        if ((bvo == true || idvo == 0) && (bsa == true || idsa == 0) && (  (bso == true && magic == true) || idso == 0) && gameended == false)
        {

            if (votedenuit == false)
            {

                delaymsg("Night vote is starting now");
                timeLeftni -= Time.deltaTime;

                if (timeLeftni < 0)
                {
                    delaymsg("");
                    Debug.Log("myrole is" + PlayerRole);
                if ((PlayerRole.Equals("V")) || (PlayerRole.Equals("VVOYANTE")) || (PlayerRole.Equals("VSORCIERE")) || (PlayerRole.Equals("VSALVATEUR")))
                {
                    Debug.Log("i'm muted and my id is" + PhotonNetwork.player.ID);
                    GameObject player = GameObject.Find("playerobj" + PhotonNetwork.player.ID);
                    player.GetComponent<PhotonVoiceRecorder>().Transmit = false;
                    AudioListener.pause = true;
                    GameObject eye = GameObject.FindGameObjectWithTag("eye");
                    eye.GetComponent<Eye>().enabled = true;
                }
                else
                {
                    if (mevoted == false)
                    {


                        GameObject pp = GameObject.FindGameObjectWithTag("paperloup");
                        GameObject SaCanvas = pp.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
                        GameObject SaCanvas1 = pp.transform.GetChild(0).gameObject;
                        SaCanvas1.SetActive(true);

                        SaCanvas.transform.LookAt(new Vector3(listInt[PhotonNetwork.player.ID - 1].x, SaCanvas.transform.position.y, listInt[PhotonNetwork.player.ID - 1].z));
                        SaCanvas.transform.Rotate(new Vector3(0, 180, 0));
                    }

                }
            }

                }

            if (votedenuit == true && votedujour == false)
            {

                /**
                 * animation for kill
                 * guide animation
                 * */
                
                //vote de jour

                if ((PlayerRole.Equals("V")) || (PlayerRole.Equals("VVOYANTE")) || (PlayerRole.Equals("VSORCIERE")) || (PlayerRole.Equals("VSALVATEUR")))
                {
                    GameObject eye = GameObject.FindGameObjectWithTag("eye");
                    eye.GetComponent<Eye>().OnEyeOpen();
                    Debug.Log("i'm not muted and my id is" + PhotonNetwork.player.ID);
                    GameObject player = GameObject.Find("playerobj" + PhotonNetwork.player.ID);
                    player.GetComponent<PhotonVoiceRecorder>().Transmit = true;
                    AudioListener.pause = false;
                   

                }

                if (mevotedJour == false)
                {
                    delaymsg("Day vote is starting now");
                    timeLeft -= Time.deltaTime;

                    if (timeLeft < 0)
                    {
                        delaymsg("");
                        GameObject pp = GameObject.FindGameObjectWithTag("paperjour");
                        GameObject SaCanvas = pp.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
                        GameObject SaCanvas1 = pp.transform.GetChild(0).gameObject;
                        SaCanvas1.SetActive(true);

                        SaCanvas.transform.LookAt(new Vector3(listInt[PhotonNetwork.player.ID - 1].x, SaCanvas.transform.position.y, listInt[PhotonNetwork.player.ID - 1].z));
                        SaCanvas.transform.Rotate(new Vector3(0, 180, 0));
                    }

                   


                    
                }

                

            }


        }



        }
    public void resetallvals()
    {
        PlayerVotesMap = new Hashtable();
        countVotes = 0;
        startturn = false;
        mevotedJour = false;
        bvo = false;
        bsa = false;
        Tokill = 0;
        iskill = false;
        votedenuit = false;
        ProtectedBySalvator = 0;
        mevoted = false;
        votedujour = false;
        timeLeft = 5;
        timeLeftsa = 5;
        timeLeftso = 5;
        timeLefttodie = 5;
        timeLefttodie1 = 5;
        gfff = false;
        timeLeftvo = 5;
        timeLeftni = 5;

    }
    /*
    [PunRPC]
    public void muteforvote()
    {
        
        GameObject[] players = GameObject.FindGameObjectsWithTag("myplayer");
        foreach (GameObject player in players)
        {
            int playerLoopId = player.GetComponent<PhotonView>().owner.ID;
            if (playerLoopId == PhotonNetwork.player.ID)
            {
                if  ( !(PlayerRole.Equals("L")   ))
                {
                   
                    player.GetComponent<PhotonVoiceRecorder>().Transmit = false;
                    AudioListener.pause = true;
                }
            }
        }
        

    }
    */

    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "gamescene")
        {
            if (PhotonNetwork.isMasterClient)
                MasterLoadedGame();
            else
                NonMasterLoadedGame();
        }
    }

    private void MasterLoadedGame()
    {
        PhotonView.RPC("ChooseRoles", PhotonTargets.All);
        PhotonView.RPC("RPC_LoadedGameScene", PhotonTargets.MasterClient, PhotonNetwork.player);
        PhotonView.RPC("RPC_LoadGameOthers", PhotonTargets.Others);
    }

    private void NonMasterLoadedGame()
    {
        PhotonView.RPC("RPC_LoadedGameScene", PhotonTargets.MasterClient, PhotonNetwork.player);
    }


    [PunRPC]
    private void RPC_LoadGameOthers()
    {
        PhotonNetwork.LoadLevel(3);
    }

   // [PunRPC]
    private void RPC_victory()
    {
        
            exittest = true;
            GameObject tf = GameObject.FindGameObjectWithTag("vict");
        GameSManager.instance.SubmitScore(20);
        GameObject ggg = new GameObject();
        ggg.transform.position = new Vector3(111.5862f, 7.09f, 318.6533f);
        Camera.main.GetComponent<CameraFollow>().setTarget(ggg.transform);
        Camera.main.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "YOU WIN !";
        GameObject[] gg = GameObject.FindGameObjectsWithTag("myplayer");


        foreach (var item in gg)
        {
            Animation anim = item.GetComponent<Animation>();
            foreach (AnimationState clip in anim)
            {
                if (clip.name.Equals("Victory"))
                {
                    item.GetComponent<Animation>().Play(clip.name);
                }
            }
        }
        medtest = true;
        StartCoroutine(exitvictory());
        
    }
    private IEnumerator diemsg(string x)
    {
        Camera.main.transform.GetChild(1).GetChild(2).GetComponent<Text>().text = x +" just died !";
        yield return new WaitForSeconds(4f);
        Camera.main.transform.GetChild(1).GetChild(2).GetComponent<Text>().text = "";
    }
    private IEnumerator exitvictory()
    {
        GameObject ggg = new GameObject();
        ggg.transform.position = new Vector3(111.5862f, 7.09f, 318.6533f);
        Camera.main.GetComponent<CameraFollow>().setTarget(ggg.transform);
        yield return new WaitForSeconds(5f);
        if (PhotonNetwork.isMasterClient)
        {
            foreach (var item in PhotonNetwork.playerList)
            {
                
                    PhotonNetwork.DestroyPlayerObjects(item);
                    PhotonNetwork.CloseConnection(item);
                PhotonNetwork.Destroy(PlayerNetwork.Instance.PhotonView);
                GameObject fff = GameObject.Find("Toggle");
                fff.GetComponent<ToggleVR>().DisableVR();
                

            }
            PhotonNetwork.LoadLevel(2);
        }
        

    }
    private IEnumerator showmycard()
    {
        float step = 0.1f * Time.deltaTime;
        startingsound = GameObject.FindGameObjectWithTag("startingsound");
        startingsound.GetComponent<AudioSource>().Play();
        m_playerCustomProperties["Role"] = PlayerRole;
        PhotonNetwork.player.SetCustomProperties(m_playerCustomProperties);
        yield return new WaitForSeconds(18f);
        GameObject v = GameObject.FindGameObjectWithTag("v");
        GameObject sa = GameObject.FindGameObjectWithTag("sa");
        GameObject l = GameObject.FindGameObjectWithTag("l");
        GameObject vo = GameObject.FindGameObjectWithTag("vo");
        GameObject so = GameObject.FindGameObjectWithTag("so");
        if (PlayerRole.Equals("V"))
        {
            v.GetComponent<cardanimation>().enabled = false;
            v.GetComponent<Transform>().position = new Vector3(111.596f, 6.36f, 318.56f);
            v.GetComponent<Transform>().LookAt(LobbyNetwork.instance.listInt[PhotonNetwork.player.ID-1]);
            v.GetComponent<Transform>().Rotate (new Vector3(0,v.GetComponent<Transform>().rotation.y+180, v.GetComponent<Transform>().rotation.z));
            Destroy(sa.gameObject);
            Destroy(l.gameObject);
            Destroy(vo.gameObject);
            Destroy(so.gameObject);
            yield return new WaitForSeconds(3f);
            timer = v.GetComponent<SpriteRenderer>().material.GetFloat("_Progress");
            yield return new WaitForSeconds(0.1f);
            v.GetComponent<SpriteRenderer>().material.SetFloat("_Progress",0.8f);
            yield return new WaitForSeconds(0.1f);
            v.GetComponent<SpriteRenderer>().material.SetFloat("_Progress", 0.7f);
            yield return new WaitForSeconds(0.1f);
            v.GetComponent<SpriteRenderer>().material.SetFloat("_Progress", 0.6f);
            yield return new WaitForSeconds(0.1f);
            v.GetComponent<SpriteRenderer>().material.SetFloat("_Progress", 0.5f);
            yield return new WaitForSeconds(0.1f);
            v.GetComponent<SpriteRenderer>().material.SetFloat("_Progress", 0.4f);
            yield return new WaitForSeconds(0.1f);
            v.GetComponent<SpriteRenderer>().material.SetFloat("_Progress", 0.3f);
            yield return new WaitForSeconds(0.1f);
            v.GetComponent<SpriteRenderer>().material.SetFloat("_Progress", 0.2f);
            yield return new WaitForSeconds(0.1f);
            v.GetComponent<SpriteRenderer>().material.SetFloat("_Progress", 0.1f);
            Destroy(v.gameObject);




        }
        else if (PlayerRole.Equals("L"))
        {
            l.GetComponent<cardanimation>().enabled = false;
            l.GetComponent<Transform>().position = new Vector3(111.596f, 6.36f, 318.56f);
            l.GetComponent<Transform>().LookAt(LobbyNetwork.instance.listInt[PhotonNetwork.player.ID - 1]);
            l.GetComponent<Transform>().Rotate(new Vector3(0, l.GetComponent<Transform>().rotation.y+180, l.GetComponent<Transform>().rotation.z));
            Destroy(sa.gameObject);
            Destroy(v.gameObject);
            Destroy(vo.gameObject);
            Destroy(so.gameObject);
            yield return new WaitForSeconds(3f);
            timer = l.GetComponent<SpriteRenderer>().material.GetFloat("_Progress");
            yield return new WaitForSeconds(0.1f);
            l.GetComponent<SpriteRenderer>().material.SetFloat("_Progress", 0.8f);
            yield return new WaitForSeconds(0.1f);
            l.GetComponent<SpriteRenderer>().material.SetFloat("_Progress", 0.7f);
            yield return new WaitForSeconds(0.1f);
            l.GetComponent<SpriteRenderer>().material.SetFloat("_Progress", 0.6f);
            yield return new WaitForSeconds(0.1f);
            l.GetComponent<SpriteRenderer>().material.SetFloat("_Progress", 0.5f);
            yield return new WaitForSeconds(0.1f);
            l.GetComponent<SpriteRenderer>().material.SetFloat("_Progress", 0.4f);
            yield return new WaitForSeconds(0.1f);
            l.GetComponent<SpriteRenderer>().material.SetFloat("_Progress", 0.3f);
            yield return new WaitForSeconds(0.1f);
            l.GetComponent<SpriteRenderer>().material.SetFloat("_Progress", 0.2f);
            yield return new WaitForSeconds(0.1f);
            l.GetComponent<SpriteRenderer>().material.SetFloat("_Progress", 0.1f);
            Destroy(l.gameObject);

        }
        else if (PlayerRole.Equals("VVOYANTE"))
        {
            vo.GetComponent<cardanimation>().enabled = false;
            vo.GetComponent<Transform>().position = new Vector3(111.596f, 6.36f, 318.56f);
            vo.GetComponent<Transform>().LookAt(LobbyNetwork.instance.listInt[PhotonNetwork.player.ID - 1]);
            vo.GetComponent<Transform>().Rotate(new Vector3(0, vo.GetComponent<Transform>().rotation.y+180, vo.GetComponent<Transform>().rotation.z));
            Destroy(sa.gameObject);
            Destroy(v.gameObject);
            Destroy(l.gameObject);
            Destroy(so.gameObject);
            yield return new WaitForSeconds(3f);
            timer = vo.GetComponent<SpriteRenderer>().material.GetFloat("_Progress");
            yield return new WaitForSeconds(0.1f);
            vo.GetComponent<SpriteRenderer>().material.SetFloat("_Progress", 0.8f);
            yield return new WaitForSeconds(0.1f);
            vo.GetComponent<SpriteRenderer>().material.SetFloat("_Progress", 0.7f);
            yield return new WaitForSeconds(0.1f);
            vo.GetComponent<SpriteRenderer>().material.SetFloat("_Progress", 0.6f);
            yield return new WaitForSeconds(0.1f);
            vo.GetComponent<SpriteRenderer>().material.SetFloat("_Progress", 0.5f);
            yield return new WaitForSeconds(0.1f);
            vo.GetComponent<SpriteRenderer>().material.SetFloat("_Progress", 0.4f);
            yield return new WaitForSeconds(0.1f);
            vo.GetComponent<SpriteRenderer>().material.SetFloat("_Progress", 0.3f);
            yield return new WaitForSeconds(0.1f);
            vo.GetComponent<SpriteRenderer>().material.SetFloat("_Progress", 0.2f);
            yield return new WaitForSeconds(0.1f);
            vo.GetComponent<SpriteRenderer>().material.SetFloat("_Progress", 0.1f);
            Destroy(vo.gameObject);

        }
        else if (PlayerRole.Equals("VSORCIERE"))
        {
            so.GetComponent<cardanimation>().enabled = false;
            so.GetComponent<Transform>().position = new Vector3(111.596f, 6.36f, 318.56f);
            so.GetComponent<Transform>().LookAt(LobbyNetwork.instance.listInt[PhotonNetwork.player.ID - 1]);
            so.GetComponent<Transform>().Rotate(new Vector3(0, so.GetComponent<Transform>().rotation.y+180, so.GetComponent<Transform>().rotation.z));
            Destroy(sa.gameObject);
            Destroy(v.gameObject);
            Destroy(l.gameObject);
            Destroy(vo.gameObject);
            yield return new WaitForSeconds(3f);
            timer = so.GetComponent<SpriteRenderer>().material.GetFloat("_Progress");
            yield return new WaitForSeconds(0.1f);
            so.GetComponent<SpriteRenderer>().material.SetFloat("_Progress", 0.8f);
            yield return new WaitForSeconds(0.1f);
            so.GetComponent<SpriteRenderer>().material.SetFloat("_Progress", 0.7f);
            yield return new WaitForSeconds(0.1f);
            so.GetComponent<SpriteRenderer>().material.SetFloat("_Progress", 0.6f);
            yield return new WaitForSeconds(0.1f);
            so.GetComponent<SpriteRenderer>().material.SetFloat("_Progress", 0.5f);
            yield return new WaitForSeconds(0.1f);
            so.GetComponent<SpriteRenderer>().material.SetFloat("_Progress", 0.4f);
            yield return new WaitForSeconds(0.1f);
            so.GetComponent<SpriteRenderer>().material.SetFloat("_Progress", 0.3f);
            yield return new WaitForSeconds(0.1f);
            so.GetComponent<SpriteRenderer>().material.SetFloat("_Progress", 0.2f);
            yield return new WaitForSeconds(0.1f);
            so.GetComponent<SpriteRenderer>().material.SetFloat("_Progress", 0.1f);
            Destroy(so.gameObject);

        }
        else if (PlayerRole.Equals("VSALVATEUR"))
        {
            sa.GetComponent<cardanimation>().enabled = false;
            sa.GetComponent<Transform>().position = new Vector3(111.596f, 6.36f, 318.56f);
            sa.GetComponent<Transform>().LookAt(LobbyNetwork.instance.listInt[PhotonNetwork.player.ID - 1]);
            sa.GetComponent<Transform>().Rotate(new Vector3(0, sa.GetComponent<Transform>().rotation.y+180, sa.GetComponent<Transform>().rotation.z));
            
            Destroy(so.gameObject);
            Destroy(v.gameObject);
            Destroy(l.gameObject);
            Destroy(vo.gameObject);
            yield return new WaitForSeconds(3f);
            timer = sa.GetComponent<SpriteRenderer>().material.GetFloat("_Progress");
            yield return new WaitForSeconds(0.1f);
            sa.GetComponent<SpriteRenderer>().material.SetFloat("_Progress", 0.8f);
            yield return new WaitForSeconds(0.1f);
            sa.GetComponent<SpriteRenderer>().material.SetFloat("_Progress", 0.7f);
            yield return new WaitForSeconds(0.1f);
            sa.GetComponent<SpriteRenderer>().material.SetFloat("_Progress", 0.6f);
            yield return new WaitForSeconds(0.1f);
            sa.GetComponent<SpriteRenderer>().material.SetFloat("_Progress", 0.5f);
            yield return new WaitForSeconds(0.1f);
            sa.GetComponent<SpriteRenderer>().material.SetFloat("_Progress", 0.4f);
            yield return new WaitForSeconds(0.1f);
            sa.GetComponent<SpriteRenderer>().material.SetFloat("_Progress", 0.3f);
            yield return new WaitForSeconds(0.1f);
            sa.GetComponent<SpriteRenderer>().material.SetFloat("_Progress", 0.2f);
            yield return new WaitForSeconds(0.1f);
            sa.GetComponent<SpriteRenderer>().material.SetFloat("_Progress", 0.1f);
            Destroy(sa.gameObject);

        }
        myrole =  GameObject.FindGameObjectWithTag("myrole").GetComponent<Image>();
        myrole.color = new Color32(255, 255, 225, 255);
        if (PlayerRole.Equals("V"))
        {
            myrole.sprite = V;
        }
        else if (PlayerRole.Equals("L"))
        {
            myrole.sprite = L;
        }
        else if (PlayerRole.Equals("VVOYANTE"))
        {
            myrole.sprite = VO;
        }
        else if (PlayerRole.Equals("VSORCIERE"))
        {
            myrole.sprite = S;
        }
        else
        {
            myrole.sprite = SA;
        }
        
        yield return new WaitForSeconds(3f);

        startturn = true;

    }

    [PunRPC]
    private void startingsoundd()
    {
        StartCoroutine(showmycard());
    }
    [PunRPC]
    private void RPC_LoadedGameScene(PhotonPlayer photonPlayer)
    {
        Debug.Log(PlayersInGame);
        PlayersInGame++;
        Debug.Log(photonPlayer.NickName + " is Ready");
        Debug.Log(PlayersInGame);
        if (PlayersInGame == PhotonNetwork.playerList.Length)
        {
            
            PhotonView.RPC("startingsoundd", PhotonTargets.AllBuffered);
            print("All players are in the game scene.");
            PhotonView.RPC("RPC_CreatePlayer", PhotonTargets.All);
        }
    }




    private IEnumerator C_SetPing()
    {
        while (PhotonNetwork.connected)
        {
            m_playerCustomProperties["Ping"] = PhotonNetwork.GetPing();
            PhotonNetwork.player.SetCustomProperties(m_playerCustomProperties);

            yield return new WaitForSeconds(5f);
        }

        yield break;
    }


    //When connected to the master server (photon).
    private void OnConnectedToMaster()
    {
        if (m_pingCoroutine != null)
            StopCoroutine(m_pingCoroutine);
        m_pingCoroutine = StartCoroutine(C_SetPing());
    }

    [PunRPC]
    private void RPC_CreatePlayer()
    {
        GameObject obj = null;
        switch (Select.mainchamp)
            {
            case 1:
                obj = PhotonNetwork.Instantiate(Path.Combine("Prefabs", "male_04Mix"), male04[PhotonNetwork.player.ID - 1], rot[PhotonNetwork.player.ID - 1], 0);
                break;
            case 0:
                obj = PhotonNetwork.Instantiate(Path.Combine("Prefabs", "male_03Mix"), male_6_3[PhotonNetwork.player.ID - 1], rot[PhotonNetwork.player.ID - 1], 0);
                break;
            case 2:
                obj = PhotonNetwork.Instantiate(Path.Combine("Prefabs", "female_03Mix"), female1_3[PhotonNetwork.player.ID - 1], rot[PhotonNetwork.player.ID - 1], 0);
                break;
            case 3:
                obj = PhotonNetwork.Instantiate(Path.Combine("Prefabs", "male_06Mix"), male_6_3[PhotonNetwork.player.ID - 1], rot[PhotonNetwork.player.ID - 1], 0);
                break;
            case 4:
                obj = PhotonNetwork.Instantiate(Path.Combine("Prefabs", "female_01Mix"), female1_3[PhotonNetwork.player.ID - 1], rot[PhotonNetwork.player.ID - 1], 0);
                break;
        }
        string yy=  "playerobj"+ PhotonNetwork.player.ID;
        obj.name = yy;
        PhotonNetwork.player.SetCustomProperties(m_playerCustomProperties);
        Camera.main.GetComponent<CameraFollow>().setTarget(obj.transform);  
    }

    public void OnMasterClientSwitched(PhotonPlayer newMasterClient)
    {
        Debug.Log(newMasterClient.NickName);
    }

    public void setVoyantTue()
    {
        PhotonView.RPC("setVoyTrue", PhotonTargets.AllBuffered);
    }

    [PunRPC]
    public void setVoyTrue()
    {
        bvo = true;
    }

    /*
    [PunRPC]
    public void addme(GameObject obj)
    {
        hashtable[PhotonNetwork.player.ID] = obj;
    }
    */
    public void setSetProtectedBySalavator(int p)
    {
        PhotonView.RPC("protectsa", PhotonTargets.AllBuffered,p);
    }

    [PunRPC]
    public void protectsa(int p)
    {
        Debug.Log("protectsa" + ProtectedBySalvator);
        ProtectedBySalvator = p;
        Debug.Log("protectsa" + ProtectedBySalvator);
        bsa = true;
    }



    public void setSorc(int p,bool x)
    {
        PhotonView.RPC("killsorc", PhotonTargets.AllBuffered, p,x);
    }

    [PunRPC]
    public void killsorc(int p,bool x)
    {
        sorcpower = false;
        Tokill = p;
        iskill = x;
        bso = true;
    }

}
