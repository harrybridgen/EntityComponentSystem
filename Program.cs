public class Program {
    static void Main(String[] args) {
        Game game = new Game();
        game.SpawnEntity();
        Console.WriteLine("Position: " + game.ComponentManager.GetEntityPosition(0).x + ", " + game.ComponentManager.GetEntityPosition(0).y);
        game.MoveEntities();
        Console.WriteLine("Position: " + game.ComponentManager.GetEntityPosition(0).x + ", " + game.ComponentManager.GetEntityPosition(0).y);
        Console.WriteLine("Health: " + game.ComponentManager.GetEntityHealth(0).health);
        game.DamageEntity(0);
        Console.WriteLine("Health: " + game.ComponentManager.GetEntityHealth(0).health);

    }
}

public struct Entity {
    public int EntityID { get; set; }
}

public struct Position {
    public int x { get; set; }
    public int y { get; set; }
}

public struct Health {
    public int health { get; set; }
}

public class EntityManager {
    public List<Entity> FreeList { get; set; }
    public List<Entity> Entities { get; set; }
    public EntityManager(int MaxEntities) {
        FreeList = new List<Entity>();
        Entities = new List<Entity>();
        for (int i = 0; i < MaxEntities; i++) {
            Entity entity = new Entity();
            entity.EntityID = i;
            FreeList.Add(entity);
        }
    }

    public void AddEntity(int EntityID) {
        Entity Entity = new Entity();
        Entity.EntityID = EntityID;
        Entities.Add(Entity);
    }

    public int GetFreeEntityID() {
        if (FreeList.Count() == 0) {
            Console.WriteLine("[SpawnEntity] No free entites!");
            return -1;
        }
        Entity entity = FreeList.First();
        int ID = entity.EntityID;
        FreeList.Remove(entity);
        return ID;
    }
}

public class ComponentManager {
    public Position[] PositionArray { get; set; }
    public Health[] HealthArray { get; set; }
    public ComponentManager(int MaxEntities) {
        PositionArray = new Position[MaxEntities];
        HealthArray = new Health[MaxEntities];
    }

    public void CreateEntityComponents(int EntityID, int x, int y, int health) {
        PositionArray[EntityID].x = x;
        PositionArray[EntityID].y = y; 
        HealthArray[EntityID].health = health;
    }

    public Position GetEntityPosition(int EntityID) {
        return PositionArray[EntityID];
    }

    public Health GetEntityHealth(int EntityID) {
        return HealthArray[EntityID];
    }

}

public class Game {
    const int MaxEntities = 100;
    public EntityManager EntityManager;
    public ComponentManager ComponentManager;
    public Game() {
        EntityManager = new EntityManager(MaxEntities);
        ComponentManager = new ComponentManager(MaxEntities);
    }

    public void SpawnEntity() {
        int FreeID = EntityManager.GetFreeEntityID();
        if (FreeID == -1) {
            return;
        }
        EntityManager.AddEntity(FreeID);
        ComponentManager.CreateEntityComponents(FreeID, 1, 1, 10);
    }

    public void MoveEntities() {
        foreach (Entity entity in EntityManager.Entities) {
            int id = entity.EntityID;
            ComponentManager.PositionArray[id].x += 1;
            ComponentManager.PositionArray[id].y += 1;
        }

    }

    public void DamageEntity(int ID) {
            ComponentManager.HealthArray[ID].health -= 1;
    }
}