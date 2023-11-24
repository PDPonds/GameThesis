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

    [Header("- T3")]
    public Vector3 lookAtOffSet;
    public Sprite addWaiterSprite;

    [Header("- T6")]
    //public Vector3 lookAtFirstCusOffset;
    public Sprite firstCusSprite;
    [HideInInspector] public CustomerStateManager firstCus;

    [Header("- T7")]
    public Sprite firstCookSprite;

    [Header("- T8")]
    public float counterDis;
    public Transform counter;
    public GameObject counterWaypoint;
    WaypointIndicator currentCounterWaypoint;

    [Header("- 11")]
    public Sprite firstPay;

    [Header("- 13")]
    public float queueCoin;

    [Header("- 14")]
    public string startDay2Text;

    //[Header("- 16")]
    //public Sprite addCookerSprite;

    [Header("- 18")]
    public string newMenuText;

    [Header("- 19")]
    public float beerDis;

    [Header("- 20")]
    public Sprite unlockBeerSprite;
    public Vector3 menuBoardOffset;

    [Header("- 22")]
    public GameObject upgrateTableWaypoint;
    public float tableDis;
    WaypointIndicator currentupgrateTableWaypoint;
    [HideInInspector] public UpgradTable currentUpgradeTable;

    //[Header("- 23")]
    //public Vector3 unlickTableOffset;
    //public Sprite unlockTableSprite;

    [Header("- 26")]
    [HideInInspector] public CustomerStateManager drunkCus;
    [HideInInspector] public EmployeeStateManager slackOffEmp;
    [HideInInspector] public CustomerStateManager dashCus;

    [Header("- 29")]
    public Sprite drunkSprite;

    [Header("- 32")]
    public Sprite slackOffSprite;

    [Header("- 36")]
    public Sprite dashCusSprite;

    [Header("- 38")]
    public string day3Text;

    [Header("- 40")]
    public Transform letterMesh;
    public GameObject letterWaypoint;
    public Sprite letterSprite;
    WaypointIndicator currentLetterWaypoint;

    private void Update()
    {
        if (dialog.activeSelf || tutorialImage.activeSelf)
        {
            PlayerManager.Instance.b_canMove = false;
        }

        GameState state = GameManager.Instance.s_gameState;
        switch (GameManager.Instance.i_currentDay)
        {
            case 1:

                if (state.s_currentState == state.s_beforeOpenState)
                {
                    switch (currentTutorialIndex)
                    {
                        case 1:

                            UIManager.Instance.UIBar.SetActive(false);
                            UIManager.Instance.g_open.SetActive(false);
                            UIManager.Instance.g_close.SetActive(false);
                            UIManager.Instance.g_goToCounter.SetActive(false);
                            UIManager.Instance.g_firstQ.SetActive(false);
                            UIManager.Instance.g_unlockBeer.SetActive(false);
                            UIManager.Instance.g_unlockTable.SetActive(false);
                            UIManager.Instance.g_letter.SetActive(false);
                            UIManager.Instance.g_finalQueue.SetActive(false);

                            UIManager.Instance.g_goToBoardForCooker.SetActive(false);

                            tutorialImage.SetActive(false);
                            Pause.isPause = true;

                            dialog.SetActive(true);
                            DialogBox dialogBox = dialog.GetComponent<DialogBox>();
                            dialogBox.SetupDailogText(startText);
                            UIManager.Instance.g_goToBoardForWaiter.SetActive(false);

                            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
                            {
                                currentTutorialIndex = 2;
                            }


                            break;
                        case 2:

                            Pause.isPause = false;
                            dialog.SetActive(false);
                            UIManager.Instance.SpawnManagementBoardWaypoint();
                            UIManager.Instance.g_goToBoardForWaiter.SetActive(true);
                            float currentDis = Vector3.Distance(PlayerManager.Instance.transform.position, UIManager.Instance.t_managementBoardMesh.position);
                            if (currentDis <= boardDis)
                            {
                                currentTutorialIndex = 3;
                            }

                            break;
                        case 3:

                            tutorialImage.SetActive(true);
                            TutorialImage image = tutorialImage.GetComponent<TutorialImage>();
                            image.SetupImage(addWaiterSprite);

                            CameraController.Instance.s_playerCamera.PlayerLookAtTarget(UIManager.Instance.t_managementBoardMesh, lookAtOffSet);
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

                            //เปิดร้าน

                            UIManager.Instance.g_goToBoardForWaiter.SetActive(false);
                            UIManager.Instance.DestroyManagementBoardWaypoint();
                            UIManager.Instance.g_open.SetActive(true);
                            UIManager.Instance.SpawnOpenCloseWaypoint();

                            break;
                        default: break;
                    }
                }
                else if (state.s_currentState == state.s_openState)
                {
                    UIManager.Instance.UIBar.SetActive(true);
                    UIManager.Instance.g_open.SetActive(false);
                    UIManager.Instance.DestroyOpenClseWaypoint();

                    switch (currentTutorialIndex)
                    {
                        case 6:

                            //ลูกค้าคนแรกเข้า

                            if (RestaurantManager.Instance.GetWaitFirstChair(out int chairIndex, out int cusIndex))
                            {
                                CustomerStateManager cus = RestaurantManager.Instance.allCustomers[cusIndex];
                                ChairObj chair = RestaurantManager.Instance.allChairs[chairIndex];
                                CameraController.Instance.s_playerCamera.PlayerLookAtTarget(cus.transform, Vector3.zero);

                                tutorialImage.SetActive(true);
                                TutorialImage image = tutorialImage.GetComponent<TutorialImage>();
                                image.SetupImage(firstCusSprite);

                                Pause.isPause = true;
                                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
                                {
                                    currentTutorialIndex = 7;
                                }
                            }

                            break;
                        case 7:

                            //เปิด Interactive Pot
                            Pause.isPause = false;
                            tutorialImage.SetActive(false);

                            if (PlayerManager.Instance.g_interactiveObj != null)
                            {
                                PotAndPan pot = PlayerManager.Instance.g_interactiveObj.GetComponent<PotAndPan>();
                                if (pot != null)
                                {
                                    if (pot.b_canUse)
                                    {
                                        tutorialImage.SetActive(true);
                                        TutorialImage image2 = tutorialImage.GetComponent<TutorialImage>();
                                        image2.SetupImage(firstCookSprite);
                                        Pause.isPause = true;

                                        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
                                        {
                                            currentTutorialIndex = 8;
                                        }

                                    }
                                }
                            }

                            break;
                        case 8:

                            //ทำอาหาร
                            Pause.isPause = false;
                            tutorialImage.SetActive(false);
                            if (firstCus.s_currentState == firstCus.s_eatFoodState)
                            {
                                currentTutorialIndex = 9;
                            }

                            break;
                        case 9:

                            //พาไป Counter

                            UIManager.Instance.g_goToCounter.SetActive(true);
                            if (currentCounterWaypoint == null)
                            {
                                currentCounterWaypoint = UIManager.Instance.SpawnWayPoint(counterWaypoint, counter);
                            }

                            float counterAndPlayerDis = Vector3.Distance(PlayerManager.Instance.transform.position, counter.position);
                            if (counterAndPlayerDis <= counterDis)
                            {
                                currentTutorialIndex = 10;
                            }

                            break;
                        case 10:

                            //เปลี่ยน cus ไปจ่ายตัง
                            if (firstCus.s_currentState != firstCus.s_goToCounterState &&
                                firstCus.s_currentState != firstCus.s_frontCounter)
                            {
                                firstCus.SwitchState(firstCus.s_goToCounterState);
                            }

                            if (firstCus.s_currentState == firstCus.s_frontCounter)
                            {
                                currentTutorialIndex = 11;
                            }

                            break;
                        case 11:

                            //cus ทำท่าจ่ายตัง
                            tutorialImage.SetActive(true);
                            TutorialImage image3 = tutorialImage.GetComponent<TutorialImage>();
                            image3.SetupImage(firstPay);

                            CameraController.Instance.s_playerCamera.PlayerLookAtTarget(firstCus.transform, Vector3.zero);
                            Pause.isPause = true;
                            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
                            {
                                currentTutorialIndex = 12;
                            }

                            break;
                        case 12:
                            //กดเก็บตัง
                            Pause.isPause = false;
                            tutorialImage.SetActive(false);
                            if (firstCus.s_currentState != firstCus.s_frontCounter)
                            {
                                currentTutorialIndex = 13;
                            }

                            break;
                        case 13:

                            firstCus = null;
                            //ปิด couter waypoint เปิด 255Q
                            if (currentCounterWaypoint != null)
                            {
                                Destroy(currentCounterWaypoint.gameObject);
                                currentCounterWaypoint = null;
                            }
                            UIManager.Instance.g_goToCounter.SetActive(false);

                            UIManager.Instance.g_firstQ.SetActive(true);
                            if (GameManager.Instance.f_coin >= queueCoin)
                            {
                                if (state.s_currentState != state.s_afterOpenState)
                                {
                                    state.SwitchState(state.s_afterOpenState);
                                }
                            }

                            break;
                        default: break;
                    }

                }

                break;
            case 2:

                if (state.s_currentState == state.s_beforeOpenState)
                {
                    switch (currentTutorialIndex)
                    {
                        case 14:

                            UIManager.Instance.g_firstQ.SetActive(false);
                            UIManager.Instance.g_close.SetActive(false);
                            UIManager.Instance.DestroyOpenClseWaypoint();
                            Pause.isPause = true;

                            dialog.SetActive(true);
                            DialogBox dialogBox = dialog.GetComponent<DialogBox>();
                            dialogBox.SetupDailogText(startDay2Text);

                            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
                            {
                                currentTutorialIndex = 15;
                            }

                            break;
                        case 15:

                            Pause.isPause = false;
                            dialog.SetActive(false);
                            UIManager.Instance.SpawnManagementBoardWaypoint();
                            UIManager.Instance.g_goToBoardForCooker.SetActive(true);
                            float currentDis = Vector3.Distance(PlayerManager.Instance.transform.position, empBoard.position);
                            if (currentDis <= boardDis)
                            {
                                currentTutorialIndex = 16;
                            }

                            break;
                        case 16:

                            //tutorialImage.SetActive(true);
                            //TutorialImage image = tutorialImage.GetComponent<TutorialImage>();
                            //image.SetupImage(addCookerSprite);

                            //CameraController.Instance.s_playerCamera.PlayerLookAtTarget(UIManager.Instance.t_managementBoardMesh, lookAtOffSet);
                            //Pause.isPause = true;

                            //if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
                            //{
                            //    currentTutorialIndex = 17;
                            //}

                            float cookerBoardDis = Vector3.Distance(PlayerManager.Instance.transform.position, UIManager.Instance.t_managementBoardMesh.position);
                            if (cookerBoardDis <= 5)
                            {
                                currentTutorialIndex = 17;
                            }

                            break;
                        case 17:

                            tutorialImage.SetActive(false);
                            Pause.isPause = false;

                            break;
                        case 18:

                            UIManager.Instance.DestroyManagementBoardWaypoint();
                            UIManager.Instance.g_goToBoardForCooker.SetActive(false);
                            Pause.isPause = true;

                            dialog.SetActive(true);
                            DialogBox dialogBox2 = dialog.GetComponent<DialogBox>();
                            dialogBox2.SetupDailogText(newMenuText);

                            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
                            {
                                currentTutorialIndex = 19;
                            }

                            break;
                        case 19:

                            dialog.SetActive(false);
                            UIManager.Instance.SpawnMenuBoardWaypoint();
                            UIManager.Instance.g_unlockBeer.SetActive(true);
                            Pause.isPause = false;
                            float menuBoardDis = Vector3.Distance(PlayerManager.Instance.transform.position, UIManager.Instance.t_menuBoardMesh.position);

                            if (menuBoardDis <= beerDis)
                            {
                                currentTutorialIndex = 20;
                            }

                            break;
                        case 20:

                            tutorialImage.SetActive(true);
                            TutorialImage image2 = tutorialImage.GetComponent<TutorialImage>();
                            image2.SetupImage(unlockBeerSprite);

                            CameraController.Instance.s_playerCamera.PlayerLookAtTarget(UIManager.Instance.t_menuBoardMesh, menuBoardOffset);
                            Pause.isPause = true;

                            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
                            {
                                currentTutorialIndex = 21;
                            }

                            break;
                        case 21:

                            Pause.isPause = false;
                            tutorialImage.SetActive(false);

                            break;
                        case 22:

                            UIManager.Instance.DestroyMenuBoardWaypoint();
                            UIManager.Instance.g_unlockBeer.SetActive(false);

                            if (currentUpgradeTable == null)
                            {
                                if (RestaurantManager.Instance.GetTableToUpgrade(out int tableIndex))
                                {
                                    currentUpgradeTable = RestaurantManager.Instance.allTables[tableIndex].GetComponent<UpgradTable>();
                                }
                            }
                            else
                            {
                                Transform tableMesh = currentUpgradeTable.tableObj.g_table.transform;
                                UIManager.Instance.g_unlockTable.SetActive(true);

                                if (currentupgrateTableWaypoint == null)
                                {
                                    currentupgrateTableWaypoint = UIManager.Instance.SpawnWayPoint(upgrateTableWaypoint, tableMesh);
                                }

                                float playerAndtableDis = Vector3.Distance(PlayerManager.Instance.transform.position, tableMesh.position);
                                if (playerAndtableDis <= tableDis)
                                {
                                    currentTutorialIndex = 23;
                                }
                            }

                            break;
                        case 23:

                            //tutorialImage.SetActive(true);
                            //TutorialImage image3 = tutorialImage.GetComponent<TutorialImage>();
                            //image3.SetupImage(unlockTableSprite);

                            Transform tableMesh2 = currentUpgradeTable.tableObj.g_table.transform;

                            //CameraController.Instance.s_playerCamera.PlayerLookAtTarget(tableMesh2, unlickTableOffset);
                            //Pause.isPause = true;

                            //if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
                            //{
                            //    currentTutorialIndex = 24;
                            //}

                            float upTable = Vector3.Distance(PlayerManager.Instance.transform.position, tableMesh2.position);
                            if(upTable <= 5)
                            {
                                currentTutorialIndex = 24;
                            }

                            break;
                        case 24:

                            Pause.isPause = false;
                            tutorialImage.SetActive(false);

                            break;
                        case 25:

                            UIManager.Instance.g_open.SetActive(true);
                            UIManager.Instance.SpawnOpenCloseWaypoint();
                            if (currentupgrateTableWaypoint != null)
                            {
                                Destroy(currentupgrateTableWaypoint.gameObject);
                                currentupgrateTableWaypoint = null;
                            }
                            UIManager.Instance.g_unlockTable.SetActive(false);

                            break;
                        default: break;
                    }
                }
                else if (state.s_currentState == state.s_openState)
                {
                    switch (currentTutorialIndex)
                    {
                        case 26:

                            UIManager.Instance.DestroyOpenClseWaypoint();
                            UIManager.Instance.g_open.SetActive(false);

                            if (drunkCus != null)
                            {
                                drunkCus.i_drink = 1;
                                if (drunkCus.s_currentState == drunkCus.s_drunkState)
                                {
                                    currentTutorialIndex = 27;
                                }
                            }

                            break;
                        case 27:

                            if (drunkCus != null)
                            {
                                float drunkDis = Vector3.Distance(PlayerManager.Instance.transform.position, drunkCus.t_mesh.position);
                                if (drunkCus.t_mesh.GetComponent<Renderer>().isVisible && drunkDis < 3)
                                {
                                    currentTutorialIndex = 28;

                                }

                            }

                            break;
                        case 28:

                            CameraController.Instance.s_playerCamera.PlayerLookAtTarget(drunkCus.transform, Vector3.zero);

                            tutorialImage.SetActive(true);
                            TutorialImage image = tutorialImage.GetComponent<TutorialImage>();
                            image.SetupImage(drunkSprite);

                            Pause.isPause = true;
                            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
                            {
                                currentTutorialIndex = 29;
                            }

                            break;
                        case 29:

                            Pause.isPause = false;
                            tutorialImage.SetActive(false);

                            break;
                        case 30:

                            if (slackOffEmp == null)
                            {
                                for (int i = 0; i < RestaurantManager.Instance.allEmployees.Length; i++)
                                {
                                    if (RestaurantManager.Instance.allEmployees[i].s_currentState == RestaurantManager.Instance.allEmployees[i].s_activityState)
                                    {
                                        RestaurantManager.Instance.allEmployees[i].SwitchState(RestaurantManager.Instance.allEmployees[i].s_slackOffState);
                                        slackOffEmp = RestaurantManager.Instance.allEmployees[i];
                                    }
                                }
                            }

                            if (slackOffEmp != null)
                            {
                                currentTutorialIndex = 31;
                            }

                            break;
                        case 31:
                            float slackoffDis = Vector3.Distance(PlayerManager.Instance.transform.position, slackOffEmp.transform.position);
                            if (slackOffEmp.t_mesh.GetComponent<Renderer>().isVisible && slackoffDis < 3)
                            {
                                currentTutorialIndex = 32;
                            }

                            break;
                        case 32:

                            CameraController.Instance.s_playerCamera.PlayerLookAtTarget(slackOffEmp.transform, Vector3.zero);

                            tutorialImage.SetActive(true);
                            TutorialImage image2 = tutorialImage.GetComponent<TutorialImage>();
                            image2.SetupImage(slackOffSprite);

                            Pause.isPause = true;
                            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
                            {
                                currentTutorialIndex = 33;
                            }


                            break;
                        case 33:

                            Pause.isPause = false;
                            tutorialImage.SetActive(false);
                            if (slackOffEmp.s_currentState != slackOffEmp.s_slackOffState)
                            {
                                currentTutorialIndex = 34;
                                slackOffEmp = null;
                            }

                            break;
                        case 34:

                            if (dashCus != null)
                            {
                                currentTutorialIndex = 35;
                            }

                            break;
                        case 35:

                            if (dashCus != null)
                            {
                                if (dashCus.b_escape)
                                {
                                    currentTutorialIndex = 36;
                                }
                            }

                            break;
                        case 36:

                            if (dashCus.currentAreaStay == AreaType.OutRestaurant)
                            {
                                CameraController.Instance.s_playerCamera.PlayerLookAtTarget(dashCus.transform, Vector3.zero);

                                tutorialImage.SetActive(true);
                                TutorialImage image3 = tutorialImage.GetComponent<TutorialImage>();
                                image3.SetupImage(dashCusSprite);

                                Pause.isPause = true;
                                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
                                {
                                    currentTutorialIndex = 37;
                                }
                            }

                            break;
                        case 37:

                            Pause.isPause = false;
                            tutorialImage.SetActive(false);

                            break;
                        default: break;
                    }
                }

                break;
            case 3:

                if (state.s_currentState == state.s_beforeOpenState)
                {
                    switch (currentTutorialIndex)
                    {
                        case 38:

                            UIManager.Instance.g_close.SetActive(false);
                            UIManager.Instance.DestroyOpenClseWaypoint();
                            Pause.isPause = true;

                            dialog.SetActive(true);
                            DialogBox dialogBox = dialog.GetComponent<DialogBox>();
                            dialogBox.SetupDailogText(day3Text);

                            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
                            {
                                currentTutorialIndex = 39;
                            }

                            break;
                        case 39:

                            Pause.isPause = false;
                            dialog.SetActive(false);

                            if (currentLetterWaypoint == null)
                            {
                                currentLetterWaypoint = UIManager.Instance.SpawnWayPoint(letterWaypoint, letterMesh);
                            }
                            UIManager.Instance.g_letter.SetActive(true);


                            break;
                        case 40:

                            if (currentLetterWaypoint != null)
                            {
                                Destroy(currentLetterWaypoint.gameObject);
                                currentLetterWaypoint = null;
                            }
                            UIManager.Instance.g_letter.SetActive(false);
                            UIManager.Instance.g_finalQueue.SetActive(true);

                            break;

                        default: break;
                    }
                }

                break;
            default: break;
        }

    }

}
