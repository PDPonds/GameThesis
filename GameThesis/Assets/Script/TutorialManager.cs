﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
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

}
