﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominoC
{
    class MFPlayer
    {
        static public string PlayerName = "Балбес";
        static private List<MTable.SBone> lHand;


        //=== Готовые функции =================
        // инициализация игрока
        static public void Initialize()
        { 
            lHand = new List<MTable.SBone>();
        }

        // Вывод на экран
        static public void PrintAll()
        { MTable.PrintAll(lHand); }

        // дать количество доминушек
        static public int GetCount()
        { return lHand.Count; }

        //=== Функции для разработки =================
        // добавить доминушку в свою руку
        static public void AddItem(MTable.SBone sb)
        {  }

        // дать сумму очков на руке
        static public int GetScore()
        {


        }

        // сделать ход
        static public bool MakeStep(out MTable.SBone sb, out bool End)
        {


        }

    }
}
