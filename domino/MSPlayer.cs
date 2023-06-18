using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominoC
{
    class MSPlayer
    {
        static public string PlayerName = "Бывалый";
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
        {
            lHand.Add(sb);
        }

        // дать сумму очков на руке
        static public int GetScore()
        {
            // 4. Подсчет очков происходит по их обычному номиналу за исключением ситуации,
            // когда осталась одна доминошка 0:0, цена которой в этом случае устанавливается
            // равной 25.

            if ((lHand.Count == 1) && ((lHand[0].First == 0) && (lHand[0].Second == 0)))
                return 25;

            return lHand.Sum(bone => bone.First + bone.Second);
        }

        // сделать ход
        static public bool MakeStep(out MTable.SBone sb, out bool End)
        {
            var board = MTable.GetGameCollection();

            if (!IsMovePossible(board, lHand))
            {
                sb = new MTable.SBone();
                End = false;
                return false;
            }
            
            if (lHand.Count == 0)
            {
                MTable.SBone bone;
                var boneyardEmpty = !MTable.GetFromShop(out bone);
                if (boneyardEmpty)
                {
                    
                }
            }
        }

        static private bool IsMovePossible(in List<MTable.SBone> board, in List<MTable.SBone> hand)
        {
            if ((hand.Count == 0) && (MTable.GetShopCount() == 0))
                return false;
            
            var boardHead = board.First();
            var boardTail = board.Last();  // `End` = true

            var haveCompatibleBone = false;
            foreach (var boneInHand in hand)
            {
                
            }

            return true;
        }
        
        private static bool AreBonesCompatible(MTable.SBone a, MTable.SBone b)
        {
            return (a.First == b.First) || (a.First == b.Second) || (a.Second == b.First) || (a.Second == b.Second);
        }

        private static List<MTable.SBone> GetCompatibleBones(in List<MTable.SBone> list, MTable.SBone bone)
        {
            return list.FindAll(boneFromList => AreBonesCompatible(boneFromList, bone));
        }

        private static MTable.SBone GetBestBoneToGetRidOf()
        {
            
        }

    }
}
