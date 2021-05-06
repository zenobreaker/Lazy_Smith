using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ComboHit { PERFECT, COOL, GOOD, MISS, FEVER };

public class NoteManager : MonoBehaviour
{
    /*   노트 매니저
     *   노트 랜덤하게 일정 개수만큼 배치 
     *   난이도 별로 개수가 다름 
     */
    public bool isStart = false;
    public bool isCreateGuideNote = false;          // 가이드 노트 생성 여부


    float timingValue;
    int fixedNoteCount = 7;         // 고정 수 
    //public int maxNoteCount;      // 최대로 생성될 노트 수 
    int currentNoteCount;           // 현재 나오는 노트 수 
    int correctCount;
    int initCount;                  // 게임 시작시 등장할 노트 수
    int inputCount;
    int prevInputCount; 

    ComboHit comboHit;
    List<int> guideNoteList = new List<int>();
    List<int> userNoteList = new List<int>();
    public List<GameObject> feverNotes = new List<GameObject>();


    int prevCount;      // 이전에 노트 수
    int curFeverIdx = 0;
    int feverCount;
    public int[] feverValue = new int[5];
    public static bool isFever = false;
    bool isFeverCheck = false;
    public bool isTimeAttack = false;
    bool isMiss = false;
    bool isInput = true;   // 입력 제한 
    //bool oneTime = false; 

    [Header("화살표 노트")]
    [SerializeField] GameObject go_UpArrow = null;      // 화살표 프리팹  
    [SerializeField] GameObject go_DownArrow = null;
    [SerializeField] GameObject go_RightArrow = null;
    [SerializeField] GameObject go_LeftArrow = null;

    [Header("생성 가이드 노트 박스")]
    [SerializeField] GameObject go_GuideBox = null;       // 게임 노트를 두는 박스  

    [Header("생성 유저 노트 박스")]
    [SerializeField] GameObject go_UserBox = null;       // 유저가 노트를 두는 박스  

    [Header("기타 매니저")]
    [SerializeField] ComboManager comboManager = null;
    [SerializeField] EffectManager effectManager = null;
    [SerializeField] TimingManager timingManager = null;
    [SerializeField] HammerController theHammer = null;
    [SerializeField] StageManager stageManager = null;


    private void Update()
    {
        if (isStart)
        {

            CreateNote();
            CheckTiming();
            
            if(isInput)
                GameManager.instance.GetInput();
            CheckCorrectNote();
            CheckFeverNote();
            GameManager.instance.GameEnd();
        }
    }


    // 일정시간 안에 노트를 다 누르지 못했나?
    void CheckTiming()
    {
        if (!timingManager.GetTiming() && !isMiss && !isFever)
        {
            isMiss = true;
            comboHit = ComboHit.MISS;
            effectManager.judgementEffect(3);
            comboManager.ResetCombo();
            ResetNoteCount();
            StopCoroutine(CheckNoteComplete());
            StartCoroutine(CheckNoteComplete());
        }
        else if (!timingManager.GetTiming() && !isMiss && isFever)
        {
            isMiss = true;
            isFever = false;
            Debug.Log("피버 종료");
            StartCoroutine(CheckNoteComplete());
        }
    }

    public void ClearGame()
    {
        isStart = false;
        isCreateGuideNote = false;
        ClearGuideNote();
        ClearUserNote();
        timingManager.StopTiming();
    }

    public bool CheckSingleNote()
    {
        int targetIdx = userNoteList.Count - 1;

        if (userNoteList.Count > guideNoteList.Count)
        {
            targetIdx = guideNoteList.Count - 1;
        }

        if (targetIdx >= 0 && userNoteList[targetIdx] == guideNoteList[targetIdx])
        {
            return true;
        }
        else
            return false;
    }

    // 노트 검사 
    public void CheckCorrectNote()
    {
        if (isFever)
            return;

        if (inputCount > 0 && prevInputCount != inputCount) {
            if (userNoteList[inputCount-1].Equals(guideNoteList[inputCount-1]))
            {
                prevInputCount = inputCount;
                correctCount++;
            }else
            {
                correctCount = 0;
                prevInputCount = inputCount;
                comboHit = ComboHit.MISS;
                effectManager.judgementEffect(3);
                comboManager.ResetCombo();
                ResetNoteCount();
                StartCoroutine(CheckNoteComplete());
            }
        }
        /*
        if (CheckSingleNote())
        {
            // prveIpCount = inputCount;
            correctCount++;
            Debug.Log("이거 몇번 검사?");

        }
        else
        {
            Debug.Log("여기옹?");
            comboHit = ComboHit.MISS;
            effectManager.judgementEffect(3);
            comboManager.ResetCombo();
            ResetNoteCount();
            StartCoroutine(CheckNoteComplete());

        }
        */
        if (correctCount == currentNoteCount)
        {
            correctCount = 0;

            switch (timingManager.GetTimeValue())
            {
                case 0:
                    comboHit = ComboHit.PERFECT;
                    effectManager.judgementEffect(0);
                    break;
                case 1:
                    comboHit = ComboHit.COOL;
                    effectManager.judgementEffect(1);
                    break;
                case 2:
                case 3:
                    comboHit = ComboHit.GOOD;
                    effectManager.judgementEffect(2);
                    break;
            }
            comboManager.IncreaseScore(comboHit);
            comboManager.IncreaseCombo();
            IncreaseNoteCount();
            theHammer.StartAction();


            // 노트 정리 
            StartCoroutine(CheckNoteComplete());
        }

    }


    // 피버 상태일 때 입력 검사
    public void CheckFeverNote()
    {
        if (isFever)
        {
            Debug.Log("피버 노트 등장 ");
            if (userNoteList.Count >= 3 && !isFeverCheck)
            {
                Debug.Log("멈춤?" + timingManager.isStop());
                isFeverCheck = true;
                comboHit = ComboHit.FEVER;
                effectManager.judgementEffect(4);
                comboManager.IncreaseScore(comboHit);
                comboManager.IncreaseCombo();
                theHammer.StartAction();
                StartCoroutine(CheckNoteComplete());
            }
        }
    }

    // 노트 등장 수 초기화 
    void ResetNoteCount()
    {
        currentNoteCount = initCount;
    }

    // 진행에 따른 노트 수 추가 
    void IncreaseNoteCount()
    {
        if (isTimeAttack)
        {
            if (currentNoteCount < fixedNoteCount)
                currentNoteCount++;
        }
        else
        {
            if (stageManager.GetStageProcesivity() >= stageManager.GetStageMaxProcess() * 0.6f ||
                stageManager.GetStageProcesivity() >= stageManager.GetStageMaxProcess() * 0.3f)
            {
                if (currentNoteCount < stageManager.GetCurStageMaxLevel())
                    currentNoteCount++;
            }
        }
    }

    // 노트 확인 코루틴 
    IEnumerator CheckNoteComplete()
    {
        isInput = false;
        effectManager.NoteClearEffect();
        
        yield return new WaitForSeconds(0.3f);

        ClearGuideNote();
        ClearUserNote();
        GameManager.instance.IncreaseLevel(comboHit);
        if (isTimeAttack)
            IncreaseFeverCount(comboHit);
        if (!isFever)
            timingManager.StopTiming();        // 타이밍값 초기화

        isCreateGuideNote = false;
        isMiss = false;

        if (isFever)
            isFeverCheck = false;

        isInput = true;
    }

    // 콤보에 따른 피버 수치 증가
    void IncreaseFeverCount(ComboHit p_Combo)
    {
        switch (p_Combo)
        {
            case ComboHit.PERFECT:
                feverCount += 10;
                break;
            case ComboHit.COOL:
                feverCount += 7;
                break;
            case ComboHit.GOOD:
                feverCount += 3;
                break;
            case ComboHit.MISS:
                feverCount -= 2;
                break;
        }
        Debug.Log("현재 피버 점수 : " + feverCount + "피버 인덱 " + curFeverIdx + "콤보 : " + p_Combo);
        if (feverCount >= feverValue[curFeverIdx] && !isFever)
        {
         //   Debug.Log("피버!!1");
            if (curFeverIdx < 4)
                curFeverIdx++;
            isFever = true;
            timingManager.StartFever();
            feverCount = 0;
        }
    }

    // 가이드 및 입력한 노트 및 정보 초기화 
    public void ClearGuideNote()
    {
        for (int i = 0; i < guideNoteList.Count; i++)
        {
            Destroy(go_GuideBox.transform.GetChild(i).gameObject);

        }
        guideNoteList.Clear();
    }

    public void ClearUserNote()
    {
        for (int i = 0; i < userNoteList.Count; i++)
        {
            Destroy(go_UserBox.transform.GetChild(i).gameObject);
        }
        userNoteList.Clear();
        inputCount = 0;
        prevInputCount = 0;
    }



    // 랜덤하게 화살표 뽑기
    GameObject CreateRandomArrow()
    {
        int rand = Random.Range(1, 5);

        switch (rand)
        {
            case 1:
                guideNoteList.Add(1);
                return go_UpArrow;
            case 2:
                guideNoteList.Add(2);
                return go_DownArrow;
            case 3:
                guideNoteList.Add(3);
                return go_RightArrow;
            case 4:
                guideNoteList.Add(4);
                return go_LeftArrow;
            default:    // 에러 방지 
                guideNoteList.Add(1);
                return go_UpArrow;
        }
    }

    // 게임 세팅  
    public void SettingGame(int p_NoteNum, float p_Time, bool p_TimeAttack = false)
    {
        // 노트 개수 설정 
        initCount = p_NoteNum;
        currentNoteCount = p_NoteNum;
        
        inputCount = 0;
        prevInputCount = 0;
        
        correctCount = 0;
        curFeverIdx = 0;    // 노트 피버 인덱스 초기화 

        comboManager.InitailCombo();
        comboManager.ResetCombo();

        effectManager.SettingAnim(true);
        effectManager.ResetEffect();

        curFeverIdx = 0;
        feverCount = 0;

        isStart = true;
        isTimeAttack = p_TimeAttack;
        // 타이밍 값 설정 
        timingValue = p_Time;
        //timingManager.StartTiming(p_Time);
    }

    public void ResetEffects()
    {
        effectManager.SettingAnim(false);
        comboManager.InitailCombo();
        comboManager.ResetCombo();
        effectManager.ResetEffect();
    }

    // 노트 생성 메소드
    public void CreateNote()
    {
        if (isCreateGuideNote)  // 중복 생성 방지 
            return;

        isCreateGuideNote = true;
        Debug.Log("노트 붙임 : " + currentNoteCount);
        if (!isFever)
        {
            timingManager.StartTiming(timingValue);
            for (int i = 0; i < currentNoteCount; i++)
            {

                // 노트 생성 후 가이드 박스에 붙이기 
                var clone = Instantiate(CreateRandomArrow(), go_GuideBox.transform);
                go_GuideBox.transform.SetParent(clone.transform);
            }
        }
        else
        {
            // 피버 타임 배치 
            prevCount = currentNoteCount;
            currentNoteCount = 3;
            for (int i = 0; i < feverNotes.Count; i++)
            {
                CreateRandomArrow();
                var clone = Instantiate(feverNotes[i], go_GuideBox.transform);
                go_GuideBox.transform.SetParent(clone.transform);
            }
        }
    }

    // 유저가 키를 누르면 작동 
    public void CreateNoteWithUser(KeyCode p_KeyCode)
    {
        // 유저가 입력한 노트 리스트에 추가 
        GameObject clone = null;

        if (userNoteList.Count >= currentNoteCount || !isInput)  // 추가 개수 입력 방지 
            return;

        switch (p_KeyCode)
        {
            case KeyCode.UpArrow:
                userNoteList.Add(1);
                clone = Instantiate(go_UpArrow, go_UserBox.transform);
                break;
            case KeyCode.DownArrow:
                userNoteList.Add(2);
                clone = Instantiate(go_DownArrow, go_UserBox.transform);
                break;
            case KeyCode.RightArrow:
                userNoteList.Add(3);
                clone = Instantiate(go_RightArrow, go_UserBox.transform);
                break;
            case KeyCode.LeftArrow:
                userNoteList.Add(4);
                clone = Instantiate(go_LeftArrow, go_UserBox.transform);
                break;
            default:
                userNoteList.Add(1);
                clone = Instantiate(go_UpArrow, go_UserBox.transform);
                break;
        }
        inputCount++;
        if (clone != null)
        {
            go_UserBox.transform.SetParent(clone.transform);
//            CheckCorrectNote();
        }
    }


}
