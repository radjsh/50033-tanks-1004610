using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int m_NumRoundsToWin = 2;            
    public float m_StartDelay = 3f;             
    public float m_EndDelay = 3f;               
    public CameraControl m_CameraControl;       
    public Text m_MessageText;                  
    public GameObject[] m_TankPrefabs;
    public TankManager[] m_Tanks;               
    public List<Transform> wayPointsForAI;

    private int m_RoundNumber;                  
    private WaitForSeconds m_StartWait;         
    private WaitForSeconds m_EndWait;           
    private TankManager m_RoundWinner;          
    private TankManager m_GameWinner;           
    private TankManager m_CurrentWinner;
    public Transform[] spawnPoints;

    // Bomb Related
    public GameObject bomb1;
    public GameObject bomb2;
    public GameObject bomb3;

    private void Start()
    {
        m_StartWait = new WaitForSeconds(m_StartDelay);
        m_EndWait = new WaitForSeconds(m_EndDelay); 

        SetRandomSP();
        SpawnAllTanks();
        SetCameraTargets();

        for (int i = 0; i < m_Tanks.Length; i++)
        {
            GameObject crown = m_Tanks[i].m_Instance.transform.GetChild(0).GetChild(0).gameObject;
            crown.SetActive(false);
        }        

        StartCoroutine(GameLoop());
    }


    private void SpawnAllTanks()
    {
        m_Tanks[0].m_Instance =
            Instantiate(m_TankPrefabs[0], m_Tanks[0].m_SpawnPoint.position, m_Tanks[0].m_SpawnPoint.rotation) as GameObject;
        m_Tanks[0].m_PlayerNumber = 1;
        m_Tanks[0].SetupPlayerTank();

        for (int i = 1; i < m_Tanks.Length; i++)
        {
            m_Tanks[i].m_Instance =
                Instantiate(m_TankPrefabs[i], m_Tanks[i].m_SpawnPoint.position, m_Tanks[i].m_SpawnPoint.rotation) as GameObject;
            m_Tanks[i].m_PlayerNumber = i + 1;
            m_Tanks[i].SetupAI(wayPointsForAI);
        }
    }


    private void SetCameraTargets()
    {
        Transform[] targets = new Transform[m_Tanks.Length];

        for (int i = 0; i < targets.Length; i++)
            targets[i] = m_Tanks[i].m_Instance.transform;

        m_CameraControl.m_Targets = targets;
    }

    private void AddCrown(){
        GameObject crown = m_CurrentWinner.m_Instance.transform.GetChild(0).GetChild(0).gameObject;
        crown.SetActive(true);

        for (int i = 1; i < m_Tanks.Length; i++)
        {
            if (m_CurrentWinner != m_Tanks[i]){
                GameObject loser_crown = m_Tanks[i].m_Instance.transform.GetChild(0).GetChild(0).gameObject;
                loser_crown.SetActive(false);
            }
        }

    }


    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(RoundStarting());
        yield return StartCoroutine(RoundPlaying());
        yield return StartCoroutine(RoundEnding());

        if (m_GameWinner != null) SceneManager.LoadScene(0);
        else StartCoroutine(GameLoop());
    }


    private IEnumerator RoundStarting()
    {
        ResetAllTanks();
        DisableTankControl();
        if (m_CurrentWinner != null){
            AddCrown();
        }

        SetBombs();

        m_CameraControl.SetStartPositionAndSize();

        m_RoundNumber++;
        m_MessageText.text = $"ROUND {m_RoundNumber}";

        yield return m_StartWait;
    }


    private IEnumerator RoundPlaying()
    {
        EnableTankControl();

        m_MessageText.text = string.Empty;

        while (!OneTankLeft()) yield return null;
    }


    private IEnumerator RoundEnding()
    {
        DisableTankControl();

        m_RoundWinner = null;

        m_RoundWinner = GetRoundWinner();
        if (m_RoundWinner != null) m_RoundWinner.m_Wins++;

        m_CurrentWinner = GetCurrentWinner();

        m_GameWinner = GetGameWinner();

        string message = EndMessage();
        m_MessageText.text = message;

        SetRandomSP();

        yield return m_EndWait;
    }


    private bool OneTankLeft()
    {
        int numTanksLeft = 0;

        for (int i = 0; i < m_Tanks.Length; i++)
        {
            if (m_Tanks[i].m_Instance.activeSelf) numTanksLeft++;
        }

        return numTanksLeft <= 1;
    }

    private TankManager GetRoundWinner()
    {
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            if (m_Tanks[i].m_Instance.activeSelf)
                return m_Tanks[i];
        }

        return null;
    }

    private TankManager GetCurrentWinner()
    {
        TankManager currentWinner = m_Tanks[0];
        bool isDraw = false;

        for (int i = 1; i < m_Tanks.Length; i++)
        {
            if (m_Tanks[i].m_Wins > currentWinner.m_Wins){
                currentWinner = m_Tanks[i];
                isDraw = false;
            }
            else if (m_Tanks[i].m_Wins == currentWinner.m_Wins){
                isDraw = true;
            }
        } 

        if (!isDraw){
            return currentWinner;
        }

        if (isDraw){
            GameObject crown = m_CurrentWinner.m_Instance.transform.GetChild(0).GetChild(0).gameObject;
            crown.SetActive(false);
        }

        return null;
    }

    private TankManager GetGameWinner()
    {
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            if (m_Tanks[i].m_Wins == m_NumRoundsToWin)
                return m_Tanks[i];
        }

        return null;
    }

    private void SetRandomSP(){
        int group = Random.Range(0, 4);
        switch (group){
            case 0:
                int[] SPList = {0, 1, 2};
                m_Tanks[0].m_SpawnPoint = spawnPoints[0];
                m_Tanks[1].m_SpawnPoint = spawnPoints[1];
                m_Tanks[2].m_SpawnPoint = spawnPoints[2];
                break;
            case 1:
                m_Tanks[0].m_SpawnPoint = spawnPoints[2];
                m_Tanks[1].m_SpawnPoint = spawnPoints[3];
                m_Tanks[2].m_SpawnPoint = spawnPoints[4];
                break;
            case 2:
                m_Tanks[0].m_SpawnPoint = spawnPoints[5];
                m_Tanks[1].m_SpawnPoint = spawnPoints[1];
                m_Tanks[2].m_SpawnPoint = spawnPoints[0];
                break;
            case 3:
                m_Tanks[0].m_SpawnPoint = spawnPoints[1];
                m_Tanks[1].m_SpawnPoint = spawnPoints[5];
                m_Tanks[2].m_SpawnPoint = spawnPoints[6];
                break;                                
        }
    }

    void SetBombs(){

        if (m_RoundNumber == 0) {
            bomb1.SetActive(false);
            bomb2.SetActive(false);
            bomb3.SetActive(false);
        } else if (m_RoundNumber == 1){
            bomb1.SetActive(true);
        } else if (m_RoundNumber == 2){
            bomb2.SetActive(true);
        } else if (m_RoundNumber == 3){
            bomb3.SetActive(true);
        }
    }


    private string EndMessage()
    {
        var sb = new StringBuilder();

        if (m_RoundWinner != null) sb.Append($"{m_RoundWinner.m_ColoredPlayerText} WINS THE ROUND!");
        else sb.Append("DRAW!");

        sb.Append("\n\n\n\n");

        for (int i = 0; i < m_Tanks.Length; i++)
        {
            sb.AppendLine($"{m_Tanks[i].m_ColoredPlayerText}: {m_Tanks[i].m_Wins} WINS");
        }

        if (m_GameWinner != null)
            sb.Append($"{m_GameWinner.m_ColoredPlayerText} WINS THE GAME!");

        return sb.ToString();
    }


    private void ResetAllTanks()
    {
        for (int i = 0; i < m_Tanks.Length; i++) m_Tanks[i].Reset();
    }


    private void EnableTankControl()
    {
        for (int i = 0; i < m_Tanks.Length; i++) m_Tanks[i].EnableControl();
    }


    private void DisableTankControl()
    {
        for (int i = 0; i < m_Tanks.Length; i++) m_Tanks[i].DisableControl();
    }
}