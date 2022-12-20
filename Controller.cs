using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    //Debug
    [Header("DEBUGGGING")]
    public Text RoxbDebug;
    public Text HolodomorDebug;
    public Text AlpugatoryDebug;
    public Text AlpugatoryColDebug;

    [Header("Mechanism Player")]
    //InSource
    public Animator MainCam;
    public Animator DoorLefts;
    public Animator DoorRights;
    public GameObject Light;

    //bool
    public bool ThisIsLeft = false;
    public bool ThisIsRight = false;
    public bool CheckDoor = false;
    public bool PosisiCheckKanan = false;
    public bool PosisiCheckKiri = false;
    public bool PosisiCheckNetral = true;
    public bool IsDefendingKanan = false;
    public bool IsDefendingKiri = false;
    public bool IsTahanEksekusi = false;
    public bool SedangDiKanan = false;
    public bool SedangDiKiri = false;
    public bool IsSoundTersedia = true;
    public bool IsFlashTersedia = true;

    // bool Special
    public bool HolomonorExtraTime = false;
    public bool RoxbExtraTime = false;
    public bool AlpugatoryExtraTime = false;

    //float
    public float speed = 100f;
    public float TahanEksekusiWaktu = 0f;
    public float BerapaLamaMenahanWaktu = 0f;
    public float RandomSounding = 0f;
    public float AturanDelaySoundFX = 0f;

    //Mechanic AI
    [Header("Holomonor Monster Mechanics")]

    // 1. Holomonor
    [Header("Mechanics AI")] //Kebalik Di Inspectornya yah, Ini Pokoknya "Holomonor Monster Mechanics"
    public float HolomonorTime;
    public GameObject HolomonorJumpscare;
    public float FastProtectionKiri;
    public float maxProtectionKiri = 5f;
    public float maxTimeHolomonor = 10f;

    // 2. Roxb
    [Header("Roxb Monster Mechanics")]
    public float RoxbTime;
    public GameObject RoxbJumpscare;
    public float FastProtectionKanan;
    public float maxProtectionKanan = 5f;
    public float maxTimeRoxb = 10f;

    // 3. Alpugatory
    [Header("Alpugatory Monster Mechanics")]
    public float AlpugatoryTime;
    public GameObject AlpugatoryJumpscare;
    public float FastProtectionNetral;
    public float maxProtectionNetral = 5f;
    public float CollectionClick = 0f;
    public bool MainClickCollection = false;

    // Audio Music
    [Header("Audio Music FX")]
    public AudioSource BreathFX;
    public AudioSource NoiseDoorFX;
    public AudioSource OpenFlashLightFX;
    public AudioSource CloseFlashLightFX;
    public AudioSource HelloBBFX;
    public AudioSource HiBBFX;
    public AudioSource ByeBBFX;
    public AudioSource OpenDoorFX;
    public AudioSource CloseDoorFX;
    public AudioSource CrawlingPlayerToDoorFX;
    public AudioSource CrawlingPlayerFromDoorFX;


    //Button Flash
    [Header("Mobile")]
    public GameObject TombolLampuKanan;
    public GameObject TombolLampuKiri;
    public GameObject TombolLampuNetral;
    public GameObject TombolSounding;

    // Button Pintu Kanan
    public GameObject TombolPintuKanan;

    // Button Pintu Kiri
    public GameObject TombolPintuKiri;
    public GameObject CoverA;
    public GameObject CoverD;
    public GameObject CoverPintu;

    // Start is called before the first frame update
    void Start()
    {
        //
        // GameObject
        //
        Light.SetActive(false);
        TombolLampuKiri.SetActive(false);
        TombolLampuKanan.SetActive(false);
        TombolPintuKiri.SetActive(false);
        TombolPintuKanan.SetActive(false);
        TombolLampuNetral.SetActive(true);
        TombolSounding.SetActive(true);
        CoverA.SetActive(false);
        CoverD.SetActive(false);
        CoverPintu.SetActive(false);

        //
        // Bool
        //

        IsSoundTersedia = true;
        IsFlashTersedia = true;

        //
        // AI
        //

        PosisiCheckNetral = true;
        HolomonorJumpscare.SetActive(false);
        RoxbJumpscare.SetActive(false);
        AlpugatoryJumpscare.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //
        // Posisi Player (Keyboard)
        //
        if (ThisIsLeft == false)
        {
            if(SedangDiKiri == false)
            {
                if(PosisiCheckNetral == true)
                {
                    if (Input.GetKeyDown(KeyCode.A))
                    {
                        MainCam.Play("ToLeft");
                        ThisIsLeft = true;
                        ThisIsRight = false;
                        Light.SetActive(false);
                        PosisiCheckKiri = true;
                        FastProtectionKiri = maxProtectionKiri;
                        PosisiCheckNetral = false;
                        IsTahanEksekusi = true;
                        SedangDiKanan = false;
                        SedangDiKiri = true;
                        CrawlingPlayerToDoorFX.Play();
                    }
                }
                
            } else if (SedangDiKanan == true)
            {
                Debug.Log("Salah");
                SedangDiKanan = false;
            }
            
            
        }
        else if (ThisIsLeft == true)
        {
            if (SedangDiKanan == false)
            {
                if(SedangDiKiri == true)
                {
                    if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        MainCam.Play("DragFaceLeftPlayer");
                        DoorLefts.Play("LeftDoorAnim");
                        CheckDoor = true;
                        Light.SetActive(false);
                        IsDefendingKiri = true;
                        CloseDoorFX.Play();
                    }
                    else if (Input.GetKey(KeyCode.RightArrow))
                    {
                        Light.SetActive(false);
                    }
                    else if (Input.GetKeyUp(KeyCode.RightArrow))
                    {
                        MainCam.Play("PullFaceLeftPlayer");
                        DoorLefts.Play("OpenDoorLeft");
                        Light.SetActive(false);
                        CheckDoor = false;
                        IsDefendingKiri = false;
                        OpenDoorFX.Play();
                    }
                    else if (Input.GetKeyDown(KeyCode.F))
                    {
                        if(IsFlashTersedia == true)
                        {
                            Light.SetActive(true);
                            OpenFlashLightFX.Play();
                            HolomonorExtraTime = true;
                            StartCoroutine(FalseInExtraTime());
                        }
                    }
                    else if (Input.GetKeyUp(KeyCode.F))
                    {
                        Light.SetActive(false);
                    }
                    if (Input.GetKeyUp(KeyCode.A))
                    {
                        if (TahanEksekusiWaktu <= BerapaLamaMenahanWaktu)
                        {
                            StartCoroutine(EksekusiManualKiri());
                        }
                        else if (TahanEksekusiWaktu >= BerapaLamaMenahanWaktu)
                        {
                            MainCam.Play("FromLeft");
                            if (CheckDoor == true)
                            {
                                DoorLefts.Play("OpenDoorLeft");
                            }
                            ThisIsLeft = false;
                            ThisIsRight = false;
                            Light.SetActive(false);
                            PosisiCheckKiri = false;
                            CollectionClick = 0f;
                            PosisiCheckNetral = true;
                            IsTahanEksekusi = false;
                            TahanEksekusiWaktu = 0f;
                            StartCoroutine(KeyBoardLockA());
                            IsDefendingKiri = false;
                            CheckDoor = false;
                            CrawlingPlayerFromDoorFX.Play();
                        }
                    }
                }
            }
        }
        if (ThisIsRight == false)
        {
            if (SedangDiKanan == false)
            {
                if (PosisiCheckNetral == true)
                {
                    if (Input.GetKeyDown(KeyCode.D))
                    {
                        MainCam.Play("ToRight");
                        ThisIsRight = true;
                        ThisIsLeft = false;
                        PosisiCheckKanan = true;
                        FastProtectionKanan = maxProtectionKanan;
                        PosisiCheckNetral = false;
                        IsTahanEksekusi = true;
                        SedangDiKiri = false;
                        SedangDiKanan = true;
                        CrawlingPlayerToDoorFX.Play();
                    }
                }
            } else if (SedangDiKiri == true)
            {
                Debug.Log("Salah");
                SedangDiKiri = false;
            }
            
        }
        else if (ThisIsRight == true)
        {
            if (SedangDiKiri == false)
            {
                if(SedangDiKanan == true)
                {
                    if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        MainCam.Play("DragFaceRightPlayer");
                        DoorRights.Play("RightDoorAnim");
                        CheckDoor = true;
                        Light.SetActive(false);
                        IsDefendingKanan = true;
                        CloseDoorFX.Play();

                    }
                    else if (Input.GetKey(KeyCode.LeftArrow))
                    {
                        Light.SetActive(false);
                    }
                    else if (Input.GetKeyUp(KeyCode.LeftArrow))
                    {
                        MainCam.Play("PullFaceRightPlayer");
                        DoorRights.Play("OpenDoorRight");
                        CheckDoor = false;
                        IsDefendingKanan = false;
                        OpenDoorFX.Play();

                    }
                    else if (Input.GetKeyDown(KeyCode.F))
                    {
                        if(IsFlashTersedia == true)
                        {
                            Light.SetActive(true);
                            OpenFlashLightFX.Play();
                            RoxbExtraTime = true;
                            StartCoroutine(FalseInExtraTime());
                        }
                    }
                    else if (Input.GetKeyUp(KeyCode.F))
                    {
                        Light.SetActive(false);
                    }
                    if (Input.GetKeyUp(KeyCode.D))
                    {
                        if (TahanEksekusiWaktu <= BerapaLamaMenahanWaktu)
                        {
                            StartCoroutine(EksekusiManualKanan());
                        }
                        else if (TahanEksekusiWaktu >= BerapaLamaMenahanWaktu)
                        {
                            MainCam.Play("FromRight");
                            if (CheckDoor == true)
                            {
                                DoorRights.Play("OpenDoorRight");
                            }
                            ThisIsRight = false;
                            ThisIsLeft = false;
                            Light.SetActive(false);
                            PosisiCheckKanan = false;
                            CollectionClick = 0f;
                            PosisiCheckNetral = true;
                            IsTahanEksekusi = false;
                            TahanEksekusiWaktu = 0f;
                            IsDefendingKanan = false;
                            StartCoroutine(KeyBoardLockD());
                            CheckDoor = false;
                            CrawlingPlayerFromDoorFX.Play();
                        }
                    }
                }            
            }
        }
        if (PosisiCheckKiri == false && PosisiCheckKanan == false)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Light.SetActive(true);
                OpenFlashLightFX.Play();
            }
            
            else if (Input.GetKey(KeyCode.F))
            {
                CollectionClick += 1f * Time.deltaTime;
                
            }
            else if (Input.GetKeyUp(KeyCode.F))
            {
                Light.SetActive(false);
                CloseFlashLightFX.Play();
            }
            else if (CollectionClick >= 2f)
            {
                CollectionClick += 1f * Time.deltaTime;
            } 
        }
        if(MainClickCollection == true)
        {
            CollectionClick += 1f * Time.deltaTime;
        }

        //
        // Mechanism Sounding FX 
        //
        //

        if(SedangDiKiri == false)
        {
            if(SedangDiKanan == false)
            {
                if(IsSoundTersedia == true)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        SoundBB();
                    }
                }
            }
        }
        // Mechanism ("Holomonor")
        //
        if (PosisiCheckKiri == true)
        {
            if(IsDefendingKiri == false)
            {
                if(HolomonorExtraTime == true)
                {
                    HolomonorTime += 1f * Time.deltaTime;
                } else if (HolomonorExtraTime == false)
                {
                    FastProtectionKiri -= 1f * Time.deltaTime;
                    HolomonorTime -= 1f * Time.deltaTime;
                    if (HolomonorTime <= 1f)
                    {
                        if (FastProtectionKiri <= 1f)
                        {
                            HolomonorJumpscare.SetActive(true);
                            HolomonorTime = maxTimeHolomonor;
                        }
                    }
                }
                
            }
            else if (IsDefendingKiri == true)
            {
                if (HolomonorTime <= 5f)
                {
                    HolomonorTime -= 1f * Time.deltaTime;
                }
            }

        }
        //
        // Mechanism ("Roxb")
        //
        if (PosisiCheckKanan == true)
        {
            if (IsDefendingKanan == false)
            {
                if (RoxbExtraTime == true)
                {
                    RoxbTime += 1f * Time.deltaTime;
                } else if (RoxbExtraTime == false)
                {
                    FastProtectionKanan -= 1f * Time.deltaTime;
                    RoxbTime -= 1f * Time.deltaTime;
                    if (RoxbTime <= 1f)
                    {
                        if (FastProtectionKanan <= 1f)
                        {
                            RoxbJumpscare.SetActive(true);
                            RoxbTime = maxTimeRoxb;
                        }
                    }
                }
            }
            else if (IsDefendingKanan == true)
            {
                if (RoxbTime <= 5f)
                {
                    RoxbTime -= 1f * Time.deltaTime;
                }
            }

        }

        //
        // Mechanism ("Alpugatory")
        //
        if(PosisiCheckNetral == true)
        {
            if (CollectionClick <= 2f || CollectionClick >= 4f)
            {
                if(AlpugatoryExtraTime == true)
                {
                    AlpugatoryTime += 10f * Time.deltaTime;
                } else if (AlpugatoryExtraTime == false)
                {
                    FastProtectionNetral -= 1f * Time.deltaTime;
                    AlpugatoryTime -= 1f * Time.deltaTime;
                    if (AlpugatoryTime <= 1f)
                    {
                        if (FastProtectionNetral <= 1f)
                        {
                            AlpugatoryJumpscare.SetActive(true);
                            AlpugatoryTime = 10f;
                        }    
                    }
                }
            }
        } else if (PosisiCheckNetral == false)
        {
            FastProtectionNetral = maxProtectionNetral;
        }
        RoxbDebug.text = "Roxb : " + "\n" + RoxbTime.ToString();
        HolodomorDebug.text = "Holomonor : " + "\n" + HolomonorTime.ToString();
        AlpugatoryDebug.text = "Alpugatory : " + "\n" + AlpugatoryTime.ToString();
        AlpugatoryColDebug.text = "Collection : " + "\n" + CollectionClick.ToString();

        //
        // Mekanisme Tahan Eksekusi Kegiatan Berikutnya
        //
        if(IsTahanEksekusi == true)
        {
            TahanEksekusiWaktu += 1f * Time.deltaTime;
        }
    }

    void SoundBB() //Mekanisme Suara Netral
    {
        if (IsSoundTersedia == true)
        {
            RandomSounding = Random.Range(1, 4);
        }
        if (RandomSounding == 1)
        {
            HiBBFX.Play();
        }
        else if (RandomSounding == 2)
        {
            HelloBBFX.Play();
        }
        else if (RandomSounding == 3)
        {
            ByeBBFX.Play();
        }
        AlpugatoryExtraTime = true;
        TombolSounding.SetActive(false);
        StartCoroutine(FalseInExtraTime());
        StartCoroutine(AturanSounding());
    }

    // 
    // Controller (Mobile)
    // 

    //
    // "Button Kanan"
    //

    public void KeKanan()
    {
        MainCam.Play("ToRight");
        ThisIsRight = true;
        ThisIsLeft = false;
        Light.SetActive(false);
        TombolLampuKiri.SetActive(false);
        TombolLampuNetral.SetActive(false);
        TombolLampuKanan.SetActive(true);
        TombolPintuKanan.SetActive(true);
        PosisiCheckKanan = true;
        PosisiCheckNetral = false;
        CoverA.SetActive(true);
        TombolSounding.SetActive(false);
        CrawlingPlayerToDoorFX.Play();
        IsTahanEksekusi = true;
        TombolPintuKanan.SetActive(true);
        StartCoroutine(PenutupTombolPintu());
    }
    public void TarikPintuKanan()
    {
        if(ThisIsRight == true)
        {
            MainCam.Play("DragFaceRightPlayer");
            DoorRights.Play("RightDoorAnim");
            CheckDoor = true;
            Light.SetActive(false);
            IsDefendingKanan = true;
            CloseDoorFX.Play();
        }
    }
    public void TahanPintuKanan()
    {
        Light.SetActive(false);
    }
    public void ExitPintuKanan()
    {
        if (ThisIsRight == true)
        {
            MainCam.Play("PullFaceRightPlayer");
            DoorRights.Play("OpenDoorRight");
            Light.SetActive(false);
            CheckDoor = false;
            IsDefendingKanan = false;
            OpenDoorFX.Play();
        }
    }
    public void OpenFlashKanan()
    {
        if(ThisIsRight == true)
        {
            if(IsFlashTersedia == true)
            {
                if (IsDefendingKanan == false)
                {
                    Light.SetActive(true);
                    OpenFlashLightFX.Play();
                    RoxbExtraTime = true;
                    StartCoroutine(FalseInExtraTime());
                }
            }
        }
    }
    public void ExitFlashKanan()
    {
        if(ThisIsRight == true)
        {
            Light.SetActive(false);
        }
    }
        
    public void ExitKanan()
    {
        if(TahanEksekusiWaktu <= BerapaLamaMenahanWaktu)
        {
            StartCoroutine(EksekusiManualKanan());
        } else if (TahanEksekusiWaktu >= BerapaLamaMenahanWaktu)
        {
            MainCam.Play("FromRight");
            if (CheckDoor == true)
            {
                DoorRights.Play("OpenDoorRight");
            }
            ThisIsRight = false;
            ThisIsLeft = false;
            Light.SetActive(false);
            TombolLampuKiri.SetActive(false);
            TombolLampuNetral.SetActive(true);
            TombolLampuKanan.SetActive(false);
            TombolPintuKanan.SetActive(false);
            PosisiCheckKanan = false;
            CollectionClick = 0f;
            TahanEksekusiWaktu = 0f;
            PosisiCheckNetral = true;
            CrawlingPlayerFromDoorFX.Play();
            StartCoroutine(DontInputKananAgainUntil());
        }
        
        if(PosisiCheckKanan == false && PosisiCheckNetral == true)
        {
            if(IsSoundTersedia == true)
            {
                TombolSounding.SetActive(true);
            }
        }
    }

    //
    // "Button Kiri"
    //
    public void KeKiri()
    {
        MainCam.Play("ToLeft");
        ThisIsLeft = true;
        ThisIsRight = false;
        Light.SetActive(false);
        TombolLampuKiri.SetActive(true);
        TombolLampuNetral.SetActive(false);
        TombolPintuKiri.SetActive(true);
        PosisiCheckKiri = true;
        PosisiCheckNetral = false;
        CoverD.SetActive(true);
        TombolSounding.SetActive(false);
        CrawlingPlayerToDoorFX.Play();
        IsTahanEksekusi = true;
        StartCoroutine(PenutupTombolPintu());
    }
    public void TarikPintuKiri()
    {
        if(ThisIsLeft == true)
        {
            MainCam.Play("DragFaceLeftPlayer");
            DoorLefts.Play("LeftDoorAnim");
            CheckDoor = true;
            Light.SetActive(false);
            IsDefendingKiri = true;
            CloseDoorFX.Play();
        }
    }
    public void TahanPintuKiri()
    {
        Light.SetActive(false);
    }
    public void ExitPintuKiri()
    {
        if(ThisIsLeft == true)
        {
            MainCam.Play("PullFaceLeftPlayer");
            DoorLefts.Play("OpenDoorLeft");
            Light.SetActive(false);
            CheckDoor = false;
            IsDefendingKiri = false;
            OpenDoorFX.Play();
        }
    }
    public void OpenFlashKiri()
    {
        if(ThisIsLeft == true)
        {
            if(IsDefendingKiri == false)
            {
                if(IsFlashTersedia == true)
                {
                    Light.SetActive(true);
                    OpenFlashLightFX.Play();
                    HolomonorExtraTime = true;
                    StartCoroutine(FalseInExtraTime());
                }
            }
        }
    }
    public void ExitFlashKiri()
    {
        if(ThisIsLeft == true)
        {
            Light.SetActive(false);
        }
    }
    public void ExitKiri()
    {
        if(TahanEksekusiWaktu <= BerapaLamaMenahanWaktu)
        {
            StartCoroutine(EksekusiManualKiri());
        } else if(TahanEksekusiWaktu >= BerapaLamaMenahanWaktu)
        {
            MainCam.Play("FromLeft");
            if (CheckDoor == true)
            {
                DoorLefts.Play("OpenDoorLeft");
            }
            ThisIsLeft = false;
            ThisIsRight = false;
            Light.SetActive(false);
            Light.SetActive(false);
            TombolLampuKiri.SetActive(false);
            TombolLampuNetral.SetActive(true);
            TombolLampuKanan.SetActive(false);
            TombolPintuKanan.SetActive(false);
            TombolPintuKiri.SetActive(false);
            CoverD.SetActive(false);
            PosisiCheckKiri = false;
            CollectionClick = 0f;
            PosisiCheckNetral = true;
            IsTahanEksekusi = false;
            TahanEksekusiWaktu = 0f;
            SedangDiKiri = false;
            SedangDiKanan = false;
            IsDefendingKiri = false;
            CheckDoor = false;
            CrawlingPlayerFromDoorFX.Play();
            StartCoroutine(DontInputKiriAgainUntil());
        }
        if (CheckDoor == true)
        {
            DoorLefts.Play("OpenDoorLeft");
        }
        if(PosisiCheckKiri == false && PosisiCheckNetral == true)
        {
            if(IsSoundTersedia == true)
            {
                TombolSounding.SetActive(true);
            }
        }
        CrawlingPlayerFromDoorFX.Play();
    }
    IEnumerator PenutupTombolPintu() //Penutup
    {
        CoverPintu.SetActive(true);
        yield return new WaitForSeconds(1.9f);
        CoverPintu.SetActive(false);
    }
    IEnumerator DariKiri() //Penutup
    {
        yield return new WaitForSeconds(1.7f);
        CoverD.SetActive(false);
        CoverA.SetActive(false);
    }
    IEnumerator DariKanan() //Penutup
    {
        yield return new WaitForSeconds(1.7f);
        CoverA.SetActive(false);
        CoverD.SetActive(false);
    }
    IEnumerator DontInputKiriAgainUntil() //Penutup
    {
        CoverD.SetActive(true);
        CoverA.SetActive(true);
        yield return new WaitForSeconds(1.7f);
        CoverD.SetActive(false);
        CoverA.SetActive(false);
    }
    IEnumerator DontInputKananAgainUntil() //Penutup
    {
        CoverD.SetActive(true);
        CoverD.SetActive(true);
        yield return new WaitForSeconds(1.7f);
        CoverD.SetActive(false);
        CoverA.SetActive(false);
    }
    IEnumerator EksekusiManualKanan()
    {
        DontInputKananAgainUntil();
        yield return new WaitForSeconds(1.5f);
        Debug.Log("Kanan COKKKK");
        MainCam.Play("FromRight");
        if (CheckDoor == true)
        {
            DoorRights.Play("OpenDoorRight");
        }
        ThisIsRight = false;
        ThisIsLeft = false;
        Light.SetActive(false);
        PosisiCheckKanan = false;
        CollectionClick = 0f;
        PosisiCheckNetral = true;
        IsTahanEksekusi = false;
        TahanEksekusiWaktu = 0f;
        SedangDiKiri = false;
        SedangDiKanan = false;
        IsDefendingKanan = false;
        CheckDoor = false;
        TombolLampuKiri.SetActive(false);
        TombolLampuNetral.SetActive(true);
        TombolLampuKanan.SetActive(false);
        CrawlingPlayerFromDoorFX.Play();
    }
    IEnumerator EksekusiManualKiri()
    {
        DontInputKiriAgainUntil();
        yield return new WaitForSeconds(1.5f);
        
        MainCam.Play("FromLeft");
        if (CheckDoor == true)
        {
            DoorLefts.Play("OpenDoorLeft");
        }
        ThisIsLeft = false;
        ThisIsRight = false;
        Light.SetActive(false);
        PosisiCheckKiri = false;
        CollectionClick = 0f;
        PosisiCheckNetral = true;
        IsTahanEksekusi = false;
        TahanEksekusiWaktu = 0f;
        SedangDiKiri = false;
        SedangDiKanan = false;
        IsDefendingKiri = false;
        CheckDoor = false;
        TombolLampuKiri.SetActive(false);
        TombolLampuNetral.SetActive(true);
        TombolLampuKanan.SetActive(false);
        TombolPintuKiri.SetActive(false);
        TombolPintuKanan.SetActive(false);
        CrawlingPlayerFromDoorFX.Play();
    }
    IEnumerator FalseInExtraTime()
    {
        IsFlashTersedia = false;
        yield return new WaitForSeconds(1f);
        RoxbExtraTime = false;
        HolomonorExtraTime = false;
        AlpugatoryExtraTime = false;
        IsFlashTersedia = true;
        Light.SetActive(false);
        CloseFlashLightFX.Play();
    }
    IEnumerator AturanSounding()
    {
        IsSoundTersedia = false;
        yield return new WaitForSeconds(AturanDelaySoundFX);
        IsSoundTersedia = true;
        if (PosisiCheckKanan == false)
        {
            if(PosisiCheckKiri == false)
            {
                if(PosisiCheckNetral == true)
                {
                    TombolSounding.SetActive(true);
                } 
            }
        }
    }
    IEnumerator KeyBoardLockA()
    {
        yield return new WaitForSeconds(0.1f);
        SedangDiKiri = false;
        SedangDiKanan = false;
    }
    IEnumerator KeyBoardLockD()
    {
        yield return new WaitForSeconds(0.1f);
        SedangDiKiri = false;
        SedangDiKanan = false;
    }
    

    //
    // Button Tengah
    //

    public void MainFlash()
    {
        Light.SetActive(true);
        MainClickCollection = true;
        OpenFlashLightFX.Play();
    }
    public void ExitMainFlash()
    {
        Light.SetActive(false);
        MainClickCollection = false;
        CloseFlashLightFX.Play();
    }
    public void Sounding()
    {
        SoundBB();
    }
}
