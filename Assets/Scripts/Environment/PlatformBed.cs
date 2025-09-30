public class PlatformBed : Platform
{
    private bool winner;
    public override void AddCat(Cat cat)
    {
        base.AddCat(cat);
        winner = true;
    }

    public override void RemoveCat(Cat cat)
    {
        base.RemoveCat(cat);
        winner = false;
    }

    public bool GetHasCat(){
        return winner;
    }

    public void SetHasCat(bool value){
        winner = value;
    }
}