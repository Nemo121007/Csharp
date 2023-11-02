using System;
using System.Collections.Generic;
using System.Linq;
using Greedy.Architecture;
using System.Drawing;
using Point = Greedy.Architecture.Point;

namespace Greedy
{
    public class GreedyPathFinder : IPathFinder
    {
        public List<Point> FindPathToCompleteGoal(State state)
        {
            // Если цель равна 0 или не хватает сундуков
            if (state.Goal == 0 || state.Chests.Count < state.Goal)
                return new List<Point>();

            // Множество для хранения координат сундуков
            // Можно использовать практически любую enumerable коллекцию
            // В случае list и подобных будет пробежка по всем элементам
            // В случае хэшированных коллекций будет обращение по хэшу, что происходит на порядок быстрее
            // Dictionary занимает больше места, поэтому использовать его нерационально
            // HashSet работает с такой же скоростью, занимает меньший объём
            // И главное - не может иметь одинаковые значения внутри, что очень удобно для уникальных объектов 
            HashSet<Point> chests = new HashSet<Point>(state.Chests);

            // Для рассчёта расстояний используется экземпляр класса DijkstraPathFinder
            // Конкретно метод GetPathsByDijkstra
            // Самая быстрая реализация будет, если класс DijkstraPathFinder будет статическим
            // Но в данной ситуации остаётся два варианта:
            // Постоянно при сборе нового сундука создавать новый экземпляр класса и тратить ресурсы
            // Либо создать один экземпляр и многократно его использовать, экономя ресурсы
            DijkstraPathFinder pathDijkstra = new DijkstraPathFinder();

            List<Point> resultPath = new List<Point>();

            // Item1 - местонахождение игрока
            // Item2 - затраты энергии
            // Item3 - количество собранных сундуков
            (Point, int, int) result = (new Point(), 0, 0);
            // Записываем стартовую позицию
            result.Item1 = state.Position;

            while (result.Item3 < state.Goal)
            {
                // Найти путь к ближайшему сундуку
                PathWithCost pathNextChest = pathDijkstra.GetPathsByDijkstra(state, result.Item1, chests).FirstOrDefault();

                // Если путь не найден, возвратить пустой список
                if (pathNextChest == null)
                    return new List<Point>();

                result.Item1 = pathNextChest.End;  // Сохранение последнего местоположения
                result.Item2 += pathNextChest.Cost; // Увеличиваем стоимость

                chests.Remove(pathNextChest.End); // Удалить сундук из множества
                result.Item3++;

                if (result.Item2 > state.Energy)
                    return new List<Point>();

                pathNextChest.Path.RemoveAt(0);
                resultPath.AddRange(pathNextChest.Path);
            }

            return resultPath; // Возвратить найденный путь к цели
        }
    }
}
