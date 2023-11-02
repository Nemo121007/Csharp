using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Digger
{
    //Напишите здесь классы Player, Terrain и другие.
    public class Terrain : ICreature
    {
        public string GetImageFileName()
        {
            return "Terrain.png";
        }

        public int GetDrawingPriority()
        {
            return 0;
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return true;
        }

        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand { DeltaX = 0, DeltaY = 0 };
        }
    }

    public class Player: ICreature
    {
        public string GetImageFileName()
        {
            return "Terrain.png";
        }

        public int GetDrawingPriority()
        {
            return 1;
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return false;
        }

        public CreatureCommand Act(int x, int y)
        {
            var button = Game.KeyPressed;

            switch(button)
            {
                case Keys.Down:
                    if (y < Game.MapHeight - 1)
                        return new CreatureCommand { DeltaX = 0, DeltaY = 1 };
                    break;

                case Keys.Up:
                    if (y > 0)
                        return new CreatureCommand { DeltaX = 0, DeltaY = -1 };
                    break;

                case Keys.Right:
                    if (x < Game.MapWidth - 1)
                        return new CreatureCommand { DeltaX = 1, DeltaY = 0 };
                    break;

                case Keys.Left:
                    if (x > 0)
                        return new CreatureCommand { DeltaX = -1, DeltaY = 0 };
                    break;
            }
            return new CreatureCommand { DeltaX = 0, DeltaY = 0 };
        }
    }
}
