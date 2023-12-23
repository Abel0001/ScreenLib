using ScreenLib;

class TemplateMap : Map
{
    public TemplateMap(){
        intendedSizeX = 20;
        intendedSizeY = 20;
    }

    public override void LoadMap(Screen screen, ObjectList objectList)
    {
        GameObject gameObject = new GameObject("asd", screen, 2, objectList, true, true, true, 6,6);
        gameObject.Summon();
    }
}