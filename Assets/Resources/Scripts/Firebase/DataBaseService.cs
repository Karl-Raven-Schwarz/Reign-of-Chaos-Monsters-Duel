using Firebase.Database;

public class DataBaseService
{
    public static DatabaseReference databaseReference { get; private set; }
    
    public static void Initialize()
    {
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public static DatabaseReference Context()
    {
        if(databaseReference == null) Initialize();
        return databaseReference;
    }
}