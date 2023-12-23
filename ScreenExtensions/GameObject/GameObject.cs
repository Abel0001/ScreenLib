
namespace ScreenLib;

using System.Dynamic;

public class GameObject{

    protected ObjectList objectList;

    public bool willCollide = false;
    public bool isPushable = false;
    public bool canPush{get;}
    public int[] position = new int[2];
    protected Screen screen;
    public string NameId{get; set;}
    protected int lockedListPosition;
    public int State{
        get; set;
    }
    protected bool exists = false;
    public GameObject(string name, Screen occupiedScreen, int stateId, ObjectList list, bool canCollide, bool ableToPush, bool IsPushable, int summonX, int summonY){
        NameId = name;
        screen = occupiedScreen;
        State = stateId;
        exists = false;
        objectList = list;
        willCollide = canCollide;
        canPush = ableToPush;
        isPushable = IsPushable;
        position[0] = summonX;
        position[1] = summonY;
    }

    public void Summon(){
        if(!exists){
        if(!isValidPosition(position[0],position[1])) return;

        screen.LockedList.Add(screen.GetCorrespondingINum(position[0],position[1]));
        lockedListPosition = screen.LockedList.Count - 1;
        screen.ChangeCharacter(position[0],position[1],State);
        objectList.AddObject(this, screen.GetCorrespondingINum(position[0],position[1]));
        exists = true;
        }
    }

    public bool isValidPosition(int x, int y){
        return (y < screen.ySize) && (x >= 0 && y >= 0) && (x % screen.xSize != 0 || x == 0);

    }

    public void Remove(){
        if(exists){
            screen.LockedList.Remove(screen.GetCorrespondingINum(position[0],position[1]));
            lockedListPosition = -1;
            screen.ChangeCharacter(position[0],position[1], 0);
            objectList.RemoveObject(this);
            exists = false;
        }
    }

    void ChangeSpot(){
        if(exists)
        objectList.ChangeObject(this, screen.GetCorrespondingINum(position[0], position[1]));
    }

    public void Move(int x, int y){
       
    if(exists){
        GameObject occupiedSpot = objectList.isSpotOccupied(position[0] + x, position[1] + y);

        if (occupiedSpot != null && occupiedSpot.willCollide && willCollide)
        {
            if (occupiedSpot.isPushable && canPush)
           {
                bool pushStatus = occupiedSpot.Push(this);
                if(pushStatus == false) return;
                Console.WriteLine("Object pushed");
           }
           else
           {
                Console.WriteLine("Spot occupied");
               return;
           }
        }  
    
        if(!isValidPosition(position[0] + x, position[1] + y)) return;
        position[0] += x;
        position[1] += y;
        screen.LockedList[lockedListPosition] = screen.GetCorrespondingINum(position[0],position[1]);
        ChangeSpot();
        screen.ChangeCharacter(position[0],position[1], State);
        screen.ClearScreen();
        screen.RenderScreen();
            Console.WriteLine("Object moved");
    }
    }
    /*
    public void Move(int[] coords){
        if(willCollide && objectList.isSpotOccupied(coords[0] + position[0], coords[1] + position[1])) {
            if(objectList.isSpotOccupied(position[0] + coords[0],position[1] + coords[1]).Item2.isPushable) objectList.isSpotOccupied(position[0] + coords[0],position[1] + coords[1]).Item2.Push();
       }else return; 
        if(!isValidPosition(coords[0] + position[0], coords[1] + position[1])) return;
        position[0] += coords[0];
        position[1] += coords[1];
        screen.LockedList[lockedListPosition] = screen.GetCorrespondingINum( position[0],position[1]);
        ChangeSpot();
        screen.ChangeCharacter(position[0],position[1], State);
        screen.ClearScreen();
        screen.RenderScreen();
    }
    */



public bool Push(GameObject gameObject){
    if(exists){
                int[] direction = new int[] {gameObject.position[0] - this.position[0], gameObject.position[1] - this.position[1]};

        if( objectList.isSpotOccupied(position[0] - direction[0], position[1] - direction[1]) == null || (objectList.isSpotOccupied(position[0] - direction[0], position[1] - direction[1]).isPushable && this.canPush) && objectList.isSpotOccupied(position[0] - direction[0] * 2, position[1] - direction[1] * 2) == null){
            if(this.canPush && objectList.isSpotOccupied(position[0] - direction[0], position[1] - direction[1]) != null && objectList.isSpotOccupied(position[0] - direction[0], position[1] - direction[1]).isPushable){
                Push(objectList.isSpotOccupied(position[0] - direction[0], position[1] - direction[1]));
            }
            switch(direction){
                case [1,0]:
                Move(-1,0);
                break;
                case [-1,0]:
                Move(1,0);
                break;
                case [0, 1]:
                Move(0,-1);
                break;
                case [0, -1]:
                Move(0, 1);
                break;
            }
            return true;
        } else {
            return false;
        }
    }else return false;
}
}