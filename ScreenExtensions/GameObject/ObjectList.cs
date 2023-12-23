
namespace ScreenLib;

public class ObjectList{
    Screen screen;
    public ObjectList(Screen currScreen){
        screen = currScreen;
    }

    Dictionary<GameObject, int> objectsAndOccupiedSpots = new Dictionary<GameObject, int>();

    public List<GameObject> GetGameObjects(){
        return objectsAndOccupiedSpots.Keys.ToList();
    }
    public void RemoveObject(GameObject gameObject){
        objectsAndOccupiedSpots.Remove(gameObject);
    }
    public void AddObject(GameObject gameObject,int occupiedSpot){
        objectsAndOccupiedSpots.Add(gameObject, occupiedSpot);
    }
public GameObject isSpotOccupied(int x, int y)
{
    int correspondingINum = screen.GetCorrespondingINum(x, y);

    if (objectsAndOccupiedSpots.ContainsValue(correspondingINum))
    {
        GameObject occupyingObject = objectsAndOccupiedSpots
            .FirstOrDefault(z => z.Value == correspondingINum).Key;

        return occupyingObject;
    }
    else
    {
        return null;
    }
}
    public void ChangeObject(GameObject gameObject, int newSpot){
        if(objectsAndOccupiedSpots.ContainsKey(gameObject)){
            objectsAndOccupiedSpots[gameObject] = newSpot;
        }
    }
}