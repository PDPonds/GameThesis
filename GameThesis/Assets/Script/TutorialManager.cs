using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : Auto_Singleton<TutorialManager>
{
    //========================= Day 1 =========================
    //ขึ้นหน้าต่าง ตัวละครพูด กด Space หรือ Click ซ้าย เพื่อออก (ค่อยคิด)
    //จ้างเด็กเซิฟ 1 คน ขึ้น Waypoint ที่บอร์ดอันเดียว ปิด UI ทั้งหมด ขึ้น Objective ด้านซ้ายว่าไปที่บอร์ดเพื่อจ้างลูกน้อง
    //Waypoint เปิดร้าน บอกให้ไปกดเปิด ขึ้น Waypoint ที่หน้าประตูและขึ้น Objctive ด้านซ้าย
    //เมื่อร้านเปิด รอลูกค้าคนแรกเดินเข้ามา จะขึ้นให้ไปทำอาหาร เมื่อทำอาหารเสร็จ Waiter จะเดินไปเสริฟให้(ขึ้นอธิบาย)
    //รอลูกค้ากินเสร็จ จะเดินไปจ่ายตัง บอกให้ผู้เล่นเดินไปเก็บตัง
    //เก็บตังเสร็จลูกค้า เริ่มเข้าแบบปกติ
    //ขึ้นเป้าหมาย ให้เก็บตังในวันนี้ให้ได้ 255
    //เริ่มไปสักครึ่งวัน จะบังคับเกิดคนหนี 1 คน ถ้าผู้เล่นอยู่ในระยะที่มองเห็น จะขึ้นอธิบาย ถ้าอยู่หลังร้าน คนหนีจะยังไม่วิ่งรอให้มาเห็น

    //========================= Day 2 =========================
    //ขึ้นหน้าต่าง ตัวละครพูด กด Space หรือ Click ซ้าย เพื่อออก (เมื่อวานยุ่งชิบหาย เราน่าจะจ้างพ่อครัว 1)
    //จ้างพ่อครัว 1 คน ขึ้น Waypoint ที่บอร์ด
    //ขึ้น Waypoint ที่ โต๊ะ และ MenuBoard พาไปอัพเกรด
    //เปิดร้านตามปกติ ลูกค้าเข้าตามปกติ
    //ลูกค้าคนแรกที่เข้ามาสั่งเบียร์จะให้เกิด Event หลับ

    //========================= Day 3 =========================
    //ขึ้นหน้าต่าง ตัวละครพูด กด Space หรือ Click ซ้าย เพื่อออก (มีจดหมายมา ต้องไปดูสักหน่อย)
    //Waypoint เกิดที่จดหมาย พาไปอ่านจดหมาย
    //Objective ด้านซ้ายเปลี่ยนเป็นเก็บเงินให้ได้ 1000 และไปจ่ายเงิน ก่อนหมดวันที่ 5
    //ขึ้นหน้าต่าง กด Space หรือ Click ซ้าย เพื่อออก (เรารู้พื้นฐานหมดแล้ว แต่ถ้าร้านยังกระจอกแบบนี้หาเงินไม่ทันแน่ๆ อย่าลืมอัพเกรดร้านด้วยล่ะ)

    public int currentTutorialIndex;

    public GameObject dialog;
    public GameObject tutorialImage;

    [Header("===== Day 1 ======")]
    [Header("- T1")]
    public string startText;

    [Header("- T2")]
    public Transform empBoard;
    public float boardDis;

    [Header("-T3")]
    public Vector3 lookAtOffSet;
    public Sprite addWaiterSprite;

    private void Update()
    {
        if (dialog.activeSelf || tutorialImage.activeSelf)
        {
            PlayerManager.Instance.b_canMove = false;
        }

        switch (GameManager.Instance.i_currentDay)
        {
            case 1:

                GameState state = GameManager.Instance.s_gameState;
                if (state.s_currentState == state.s_beforeOpenState)
                {
                    switch (currentTutorialIndex)
                    {
                        case 1:

                            UIManager.Instance.UIBar.SetActive(false);
                            UIManager.Instance.g_open.SetActive(false);
                            UIManager.Instance.g_close.SetActive(false);
                            tutorialImage.SetActive(false);
                            Pause.isPause = true;

                            dialog.SetActive(true);
                            DialogBox dialogBox = dialog.GetComponent<DialogBox>();
                            dialogBox.SetupDailogText(startText);

                            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
                            {
                                currentTutorialIndex = 2;
                            }

                            UIManager.Instance.g_goToBoardForWaiter.SetActive(false);

                            break;
                        case 2:

                            Pause.isPause = false;
                            dialog.SetActive(false);
                            UIManager.Instance.SpawnManagementBoardWaypoint();
                            UIManager.Instance.g_goToBoardForWaiter.SetActive(true);
                            float currentDis = Vector3.Distance(PlayerManager.Instance.transform.position, empBoard.position);
                            if (currentDis <= boardDis)
                            {
                                currentTutorialIndex = 3;
                            }

                            break;
                        case 3:

                            tutorialImage.SetActive(true);
                            TutorialImage image = tutorialImage.GetComponent<TutorialImage>();
                            image.SetupImage(addWaiterSprite);

                            CameraController.Instance.s_playerCamera.PlayerLookAtTarget(empBoard, lookAtOffSet);
                            Pause.isPause = true;

                            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
                            {
                                Pause.isPause = false;
                                currentTutorialIndex = 4;
                            }

                            break;
                        case 4:

                            tutorialImage.SetActive(false);

                            break;
                        case 5:

                            UIManager.Instance.g_goToBoardForWaiter.SetActive(false);
                            UIManager.Instance.DestroyManagementBoardWaypoint();
                            UIManager.Instance.g_open.SetActive(true);
                            UIManager.Instance.SpawnOpenCloseWaypoint();
                            UIManager.Instance.g_goToBoardForWaiter.SetActive(false);

                            break;
                        default: break;
                    }
                }
                else if (state.s_currentState == state.s_openState)
                {
                    UIManager.Instance.UIBar.SetActive(true);
                    UIManager.Instance.g_open.SetActive(false);
                    UIManager.Instance.DestroyOpenClseWaypoint();

                }
                else
                {

                }

                break;
            case 2:

                break;
            case 3:

                break;
            default: break;
        }

    }

}
