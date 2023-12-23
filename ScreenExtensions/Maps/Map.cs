
namespace ScreenLib;
public abstract class Map{
    public int intendedSizeX;
    public int intendedSizeY;

    public abstract void LoadMap(Screen screen, ObjectList objectList);
}