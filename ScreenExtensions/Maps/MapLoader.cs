
namespace ScreenLib;
public static class MapLoader{
    public static void LoadMap(Map map, Screen screen, ObjectList objectList){
        if(map.intendedSizeX != screen.xSize || map.intendedSizeY != screen.ySize) throw new Exception("Map not intended for this screen size");
        else{
            map.LoadMap(screen, objectList);
        }
    }
}