
using System.Dynamic;

namespace ScreenLib;
public class Screen{

    public IDictionary<int, char> characters{get;} = new Dictionary<int, char>();
    List<int> DisplayList = new List<int>(); 
    public List<int> LockedList = new List<int>();
    public List<string> TextDisplayBefore = new List<string>();
    public List<string> TextDisplayAfter = new List<string>();
    private int _xSize;
    private int _ySize;
    public int xSize{
        get => _xSize;
    }
    public int ySize{
        get => _ySize;
    }

    public Screen(int xScale, int yScale){
        if(xScale <= 50 && yScale <= 50){
        _xSize = xScale;
        _ySize = yScale;
        }
    }

        public void InitScreen(){
        Console.OutputEncoding = System.Text.Encoding.UTF8;    

        characters.Add(0,'□');
        characters.Add(1, '■');
        characters.Add(2, '*');
        characters.Add(3, '+');

        for(int i = 0; i < _xSize; i++){
            for(int j = 0; j < _ySize; j++){
                DisplayList.Add(0);
            }
        }
        RenderScreen();
    }
    public void ChangeCharacter(int x, int y, int StateId){
        if(x+y * ySize > xSize * ySize){
            throw new IndexOutOfRangeException();
        }else{
            DisplayList[GetCorrespondingINum(x,y)] = StateId;
        }
        RenderScreen();
    }

    public int GetCorrespondingINum(int x, int y){
         if(x+y * ySize > xSize * ySize){
            throw new IndexOutOfRangeException();
        }
        return x + y*ySize;
    }
    public int[] GetCorrespondingCoordinates(int index){
        if(index > (xSize * ySize) - 1){
            throw new IndexOutOfRangeException();
        }

        return new int[] {index / (ySize - 1), index / (xSize - 1)};
    }

    public void RenderScreen()
    {
        string Scene = "\n";
        Console.Write("");
        Console.Clear();
        int counter = 0;
        foreach(string text in TextDisplayBefore)Console.Write(text);
        for(int i = 0; i < _xSize*_ySize; i++){

                char State;

                characters.TryGetValue(DisplayList[i], out State);
                Scene += State;
           counter++;
            if(counter == xSize){
                Scene += "\n";
                counter = 0;
            }
        }
        Console.Write(Scene);
        foreach(string text in TextDisplayAfter)Console.Write(text);
    }
    public void ClearScreen(){
        for(int i = 0; i < xSize * ySize; i++)
        {
            if(!LockedList.Contains(i))
            DisplayList[i] = 0;
            else continue;
        }
    }
}