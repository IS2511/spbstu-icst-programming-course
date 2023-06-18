using System;
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
        {
            lHand.Add(sb);
        }

        // дать сумму очков на руке
        static public int GetScore()
        {
            // 4. Подсчет очков происходит по их обычному номиналу за исключением ситуации,
            // когда осталась одна доминошка 0:0, цена которой в этом случае устанавливается
            // равной 25.

            if (lHand.Count == 1)
                if ((lHand[0].First == 0) && (lHand[0].Second == 0))
                    return 25;

            return lHand.Sum(bone => bone.First + bone.Second);
        }

        // сделать ход
        static public bool MakeStep(out MTable.SBone sb, out bool End)
        {
            // Если у нас в руке нет доминошек - мы победили
            if (lHand.Count == 0)
            {
                sb = new MTable.SBone();
                End = false;
                return false;
            }
            
            var board = MTable.GetGameCollection();
            var firstBone = board.First();
            var lastBone = board.Last();

            // Находим все подходящие доминошки в руке (и убираем повторы)
            var compatibleBones = GetCompatibleBones(lHand, firstBone.First)
                .Concat(GetCompatibleBones(lHand, lastBone.Second))
                .DistinctBy(bone => (1 << bone.First) | (1 << bone.Second))
                .ToList();
            
            // Выбираем оптимальную доминошку из подходящих чтобы положить на стол
            var bestBone = GetBestBoneToGetRidOf(board, lHand, compatibleBones, MTable.GetShopCount());

            if (!bestBone.HasValue)
            {
                // Если нет ни одной подходящей доминошки, берем из базара пока не найдем подходящую
                while (MTable.GetShopCount() > 0)
                {
                    MTable.GetFromShop(out var bone);
                    lHand.Add(bone);
                    if (IsBoneCompatible(bone, firstBone.First) || IsBoneCompatible(bone, lastBone.Second))
                    {
                        lHand.Remove(bone);
                        sb = bone;
                        // Being a bit lazy, just doing a duplicate comparison. It's cheap anyway
                        End = IsBoneCompatible(sb, lastBone.Second);
                        return true;
                    }
                }
                
                // Если базар пустой, а подходящей доминошки так и нет - сдаемся
                sb = new MTable.SBone();
                End = false;
                return false;
            }
            
            // Если сразу нашли подходящую доминошку - кладем ее на стол
            lHand.Remove(bestBone.Value);
            sb = bestBone.Value;
            End = IsBoneCompatible(bestBone.Value, lastBone.Second);
            return true;
        }
        

        private static bool IsBoneCompatible(MTable.SBone bone, ushort side)
        {
            return (bone.First == side) || (bone.Second == side);
        }

        private static IEnumerable<MTable.SBone> GetCompatibleBones(in List<MTable.SBone> list, ushort side)
        {
            return list.FindAll(boneFromList => IsBoneCompatible(boneFromList, side));
        }

        private static MTable.SBone? GetBestBoneToGetRidOf(
            in List<MTable.SBone> board,
            in List<MTable.SBone> hand,
            in List<MTable.SBone> compatibleBones,
            int boneyardCount)
        {
            // Если подходящих доминошек нет - возвращаем null
            // Если подходящая только одна - возвращаем ее
            // Если меньше трех доминошек в руке и есть (0, 0) - возвращаем ее
            switch (compatibleBones.Count)
            {
                case 0:
                    return null;
                case 1:
                    return compatibleBones.First();
                case <= 3 
                when (compatibleBones.Contains(new MTable.SBone())):
                    return new MTable.SBone(); // (0, 0)
            }
            
            // Считаем количиство уникальных (по кол-ву точек) лиц доминошек на столе
            var facesOnBoard = new Dictionary<ushort, int>(7);
            for (ushort i = 0; i < 7; i++)
                facesOnBoard.Add(i, 0);
            foreach (var bone in board)
            {
                facesOnBoard[bone.First]++;
                facesOnBoard[bone.Second]++;
            }
            
            // Считаем количиство уникальных (по кол-ву точек) лиц доминошек в совместимых
            var facesOnCompatible = new Dictionary<ushort, int>(7);
            for (ushort i = 0; i < 7; i++)
                facesOnCompatible.Add(i, 0);
            foreach (var bone in compatibleBones)
            {
                facesOnCompatible[bone.First]++;
                facesOnCompatible[bone.Second]++;
            }

            // Убираем счетчики лиц из счетчика стола, которых нет в подходящих домишках
            foreach (var face in facesOnCompatible.Where(face => face.Value == 0))
                facesOnBoard.Remove(face.Key);

            // Выбираем лицо которое встречается на столе чаще всего
            var maxCompatibleFace = facesOnBoard.Max().Key;

            // Возвращаем максимально ценную (по очкам) доминошку из подходящих,
            // имеющую выбранное (наиболее частое) лицо
            return compatibleBones
                .Where(bone => (bone.First == maxCompatibleFace) || (bone.Second == maxCompatibleFace))
                .MaxBy(bone => bone.First + bone.Second);
        }

    }
}
