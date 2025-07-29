namespace Forgotten_OOP.Serialization;

#region Using Directives

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;

using Forgotten_OOP.Entities;
using Forgotten_OOP.GameManagers;
using Forgotten_OOP.Items;
using Forgotten_OOP.Items.Interfaces;
using Forgotten_OOP.Mapping;
using Forgotten_OOP.Serialization.DTOs;

#endregion

/// <summary>
/// Provides methods to serialize and deserialize game state data
/// </summary>
public static class GameStateConverter
{
    #region Public Methods

    /// <summary>
    /// Serializes the current state of the game managed by the specified <see cref="GameManager"/> into a JSON string
    /// </summary>
    /// <param name="gameManager">The <see cref="GameManager"/> instance containing the game state to serialize</param>
    /// <param name="options">The <see cref="JsonSerializerOptions"/> to customize the JSON serialization process</param>
    /// <returns>A JSON string representing the serialized game state</returns>
    public static string SerializeGameState(GameManager gameManager, JsonSerializerOptions options)
    {
        var gameStateDto = ConvertToDto(gameManager);
        return JsonSerializer.Serialize(gameStateDto, options);
    }

    /// <summary>
    /// Deserializes a JSON string into a <see cref="GameStateDto"/> object
    /// </summary>
    /// <param name="json">The JSON string representing the game state</param>
    /// <param name="options">The options to configure the JSON deserialization process</param>
    /// <returns>A <see cref="GameStateDto"/> object deserialized from the JSON string. Returns a new <see cref="GameStateDto"/>
    /// if deserialization fails</returns>
    public static GameStateDto DeserializeGameState(string json, JsonSerializerOptions options)
    {
        return JsonSerializer.Deserialize<GameStateDto>(json, options) ?? new GameStateDto();
    }

    /// <summary>
    /// Converts a <see cref="GameStateDto"/> back into a <see cref="GameManager"/> instance
    /// </summary>
    /// <param name="gameStateDto">The game state DTO to convert</param>
    /// <returns>A new <see cref="GameManager"/> instance with the state from the DTO</returns>
    public static GameManager ConvertFromDto(GameStateDto gameStateDto)
    {
        // Create new GameManager instance
        var gameManager = RestoreGameState(gameStateDto);

        return gameManager;
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Converts a GameManager instance to a GameStateDto
    /// </summary>
    /// <param name="gameManager">The game manager to convert</param>
    /// <returns>A GameStateDto containing all game state data</returns>
    private static GameStateDto ConvertToDto(GameManager gameManager)
    {
        var isFirstPlayerMarloEncounterField = typeof(GameManager).GetField("isFirstPlayerMarloEncounter", BindingFlags.NonPublic | BindingFlags.Instance);
        var isFirstPlayerMarloEncounter = (bool)(isFirstPlayerMarloEncounterField?.GetValue(gameManager) ?? false);

        return new GameStateDto
        {
            GameConfigs = gameManager.GameConfigs,
            PlayerName = gameManager.Player.Name,
            Entities = [.. gameManager.Entities.Select(ConvertEntityToDto)],
            GameMap = ConvertMapToDto(gameManager.GameMap),
            ActionsCount = gameManager.ActionsCount,
            IsFirstPlayerMarloEncounter = isFirstPlayerMarloEncounter
        };
    }

    /// <summary>
    /// Converts an Entity to a DTO, handling type-specific properties
    /// </summary>
    /// <param name="entity">The entity to convert</param>
    /// <returns>An EntityDto containing entity state data</returns>
    private static EntityDto ConvertEntityToDto(Entity entity)
    {
        var dto = new EntityDto
        {
            Name = entity.Name,
            CurrentRoomId = entity.CurrentRoom.Id,
            EntityType = entity.GetType().Name
        };

        switch (entity)
        {
            // Handle Entity-specific properties
            case Player player:
                dto.EntityType = nameof(Player);
                dto.Lives = player.Lives;
                dto.Inventory = [.. player.Backpack.Select(item => item.GetType().Name)];
                dto.FollowingEntities = [.. player.FollowingEntities.Select(e => e.Name)];
                break;

            case Enemy enemy:
                dto.ActionDelay = enemy.ActionDelay;
                dto.PreviousRoomId = enemy.PreviousRoom?.Id;

                break;

            case Marlo marlo:
                var initialRoomField = typeof(Marlo).GetField("initialRoom", BindingFlags.NonPublic | BindingFlags.Instance);
                dto.InitialRoomId = initialRoomField != null ? (initialRoomField.GetValue(marlo) as Room)?.Id
                    : null;

                break;
        }

        return dto;
    }

    /// <summary>
    /// Converts a Map to a DTO using reflection to access private fields
    /// </summary>
    /// <param name="map">The map to convert</param>
    /// <returns>A MapDto containing map layout and room data</returns>
    private static MapDto<RoomDto> ConvertMapToDto(Map<Room> map)
    {
        // Get map dimension
        var dimensionField = typeof(Map<Room>).GetField("mapDimension", BindingFlags.NonPublic | BindingFlags.Instance);
        var dimension = (int)(dimensionField?.GetValue(map) ?? 0);

        // Get rooms list directly instead of iterating through layout
        var rooms = new List<RoomDto>();

        foreach (var room in map.Rooms)
        {
            {
                // Get coordinates from room
                Tuple<int, int>? coordinates = room.GetCoordinates();
                if (coordinates != null)
                {
                    rooms.Add(ConvertRoomToDto(room, coordinates.Item1, coordinates.Item2));
                }
            }
        }

        return new MapDto<RoomDto>
        {
            MapDimension = dimension,
            Rooms = rooms,
            StartingRoomId = map.StartingRoom?.Id ?? 0
        };
    }

    /// <summary>
    /// Converts a Room to a DTO using reflection to access private fields
    /// </summary>
    /// <param name="room">The room to convert</param>
    /// <param name="x">The X coordinate of the room</param>
    /// <param name="y">The Y coordinate of the room</param>
    /// <returns>A RoomDto containing room state data</returns>
    private static RoomDto ConvertRoomToDto(Room room, int x, int y) => new()
    {
        Id = room.Id,
        X = x,
        Y = y,
        IsPinkRoom = room.IsPinkRoom,
        IsStartingRoom = room.IsStartingRoom,
        IsEnemySpawningRoom = room.IsEnemySpawningRoom,
        IsClosed = room.IsClosed,
        ItemsOnGround = [.. room.ItemsOnGround.Select(item => item.GetType().Name)]
    };

    /// <summary>
    /// Restores the game state from a DTO to an existing GameManager instance using reflection
    /// </summary>
    /// <param name="gameStateDto">The DTO containing the state to restore</param>
    private static GameManager RestoreGameState(GameStateDto gameStateDto)
    {

        // Restore map state
        Map<Room> map = RestoreMapState(gameStateDto.GameMap);

        // Restore other entities
        List<Entity> entities = RestoreEntitiesState(gameStateDto.Entities, map);

        var game = new GameManager(
            gameStateDto.GameConfigs,
            entities.Find(e =>
                string.Equals(e.Name, gameStateDto.PlayerName, StringComparison.InvariantCultureIgnoreCase)) as Player
                ?? throw new InvalidOperationException("Player not found"),
            entities,
            map,
            gameStateDto.ActionsCount,
            gameStateDto.IsFirstPlayerMarloEncounter);

        return game;
    }

    /// <summary>
    /// Restores a map from the DTO
    /// </summary>
    /// <param name="mapDto">The map DTO containing map data</param>
    /// <returns>A restored Map object</returns>
    private static Map<Room> RestoreMapState(MapDto<RoomDto> mapDto)
    {
        // Create the map with the correct dimension
        var map = new Map<Room>(mapDto.MapDimension);

        // Create rooms and add them to the map at the correct coordinates
        foreach (var roomDto in mapDto.Rooms)
        {
            var room = new Room(
                roomDto.Id,
                map,
                roomDto.IsStartingRoom,
                roomDto.IsEnemySpawningRoom,
                roomDto.IsPinkRoom,
                roomDto.IsClosed
            );

            // Add items to the room
            foreach (var item in roomDto.ItemsOnGround.Select(CreateItemFromTypeName).OfType<Item>())
            {
                room.ItemsOnGround.Add(item);
            }

            // Place the room in the layout
            if (map.Layout != null && roomDto.X < mapDto.MapDimension && roomDto.Y < mapDto.MapDimension)
            {
                map.Layout[roomDto.X, roomDto.Y] = room;
            }
        }

        return map;
    }

    /// <summary>
    /// Restores entities including the player from DTOs
    /// </summary>
    /// <param name="entitiesDto">The list of entity DTOs</param>
    /// <param name="map">The restored map</param>
    /// <returns>A list of restored entities</returns>
    private static List<Entity> RestoreEntitiesState(List<EntityDto> entitiesDto, Map<Room> map)
    {
        var entities = new List<Entity>();
        var entityByName = new Dictionary<string, Entity>();
        Player? player = null;

        // First pass: create all entities
        foreach (var entityDto in entitiesDto)
        {
            // Get the room for this entity
            var room = map.Rooms.FirstOrDefault(r => r.Id == entityDto.CurrentRoomId);
            if (room == null)
            {
                // Use starting room as fallback
                room = map.Rooms.FirstOrDefault(r => r.IsStartingRoom) ?? map.Rooms.FirstOrDefault();
                if (room == null)
                {
                    // Skip this entity if no valid room found
                    continue;
                }
            }

            Entity? entity = null;

            // Create entity based on type
            switch (entityDto.EntityType)
            {
                case nameof(Player):
                    player = new Player(entityDto.Name, room, map, entityDto.Lives);

                    // Add inventory
                    foreach (var item in entityDto.Inventory.Select(CreateItemFromTypeName))
                    {
                        if (item is IStorable<Room> storableItem)
                        {
                            player.Backpack.Push(storableItem);
                        }
                    }

                    entity = player;
                    break;

                case nameof(Enemy):
                    if (entityDto.ActionDelay.HasValue)
                    {
                        entity = new Enemy(entityDto.Name, room, map, entityDto.ActionDelay.Value);
                    }
                    break;

                case nameof(Marlo):
                    entity = new Marlo(room, map);

                    // Set initial room if available
                    if (entityDto.InitialRoomId.HasValue)
                    {
                        var initialRoom = map.Rooms.FirstOrDefault(r => r.Id == entityDto.InitialRoomId.Value);
                        if (initialRoom != null)
                        {
                            var initialRoomField = typeof(Marlo).GetField("initialRoom", BindingFlags.NonPublic | BindingFlags.Instance);
                            initialRoomField?.SetValue(entity, initialRoom);
                        }
                    }
                    break;

                default:
                    entity = new Entity(entityDto.Name, room, map);
                    break;
            }

            if (entity != null)
            {
                entities.Add(entity);
                entityByName[entity.Name] = entity;
            }
        }

        // Second pass: restore relationships between entities
        var playerDto = entitiesDto.FirstOrDefault(e => e.EntityType == nameof(Player));

        if (player == null || playerDto?.FollowingEntities == null)
        {
            return entities;
        }

        foreach (var entityName in playerDto.FollowingEntities)
        {
            if (entityByName.TryGetValue(entityName, out var followingEntity))
            {
                player.FollowingEntities.Add(followingEntity);
            }
        }

        return entities;
    }

    /// <summary>
    /// Creates an item instance from its type name
    /// </summary>
    /// <param name="typeName">The type name of the item</param>
    /// <returns>A new item instance or null if creation fails</returns>
    private static Item? CreateItemFromTypeName(string typeName)
    {
        var itemType = Assembly.GetExecutingAssembly().GetTypes()
            .FirstOrDefault(t => t.Name == typeName && typeof(Item).IsAssignableFrom(t));

        if (itemType != null)
        {
            return Activator.CreateInstance(itemType) as Item;
        }

        return null;
    }

    #endregion
}
