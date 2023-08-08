using Async__Inn.Models.DTO;
using Async__Inn.Models.Services;

namespace AsyncInn_Test
{
    public class UnitTest1 : Mock
    {
        [Fact]
        public async Task Can_add_amenity_to_room()
        {
            // Arrange
            var roomService = new RoomServices(_db);
            var room = await CreateAndSaveTestRoom();

            // Act
            var roomDTO = await roomService.GetRoom(room.ID);


            Assert.NotNull(roomDTO);
            Assert.Equal(room.ID, roomDTO.ID);
            Assert.Equal(room.Name, roomDTO.Name);
        }

        

        [Fact]
        public async Task Can_get_room() 
        {
            // Arrange
            var room = await CreateAndSaveTestRoom();

            var service = new RoomServices(_db);

            // Act
            var actualRoom = await service.GetRoom(room.ID);

            // Assert
            Assert.NotNull(actualRoom);
            Assert.Equal(room.ID, actualRoom.ID);
            Assert.Equal(room.Name, actualRoom.Name);
            Assert.Equal(room.Layout, actualRoom.Layout);
        }

        [Fact]
        public async Task Can_update_room()
        {
            // Arrange
            var room = await CreateAndSaveTestRoom();
            var updatedRoomDTO = new RoomDTO { Name = "Updated Room", Layout = 3 };

            var service = new RoomServices(_db);

            // Act
            var updatedRoom = await service.UpdateRoom(room.ID, updatedRoomDTO);

            // Assert
            var actualRoom = await service.GetRoom(room.ID);
            Assert.NotNull(actualRoom);
            Assert.Equal(updatedRoomDTO.Name, actualRoom.Name);
            Assert.Equal(updatedRoomDTO.Layout, actualRoom.Layout);
        }

        

        [Fact]
        public async Task Can_get_amenity()
        {
            // Arrange
            var amenity = await CreateAndSaveTestAmenity();

            var service = new AmenityServices(_db);

            // Act
            var actualAmenity = await service.GetAmenity(amenity.ID);

            // Assert
            Assert.NotNull(actualAmenity);
            Assert.Equal(amenity.ID, actualAmenity.ID);
            Assert.Equal(amenity.Name, actualAmenity.Name);
        }

        [Fact]
        public async Task Can_update_amenity()
        {
            // Arrange
            var amenity = await CreateAndSaveTestAmenity();
            var updatedAmenityDTO = new AmenityDTO { Name = "Updated Amenity" };

            var service = new AmenityServices(_db);

            // Act
            var updatedAmenity = await service.UpdateAmenity(amenity.ID, updatedAmenityDTO);

            // Assert
            var actualAmenity = await service.GetAmenity(amenity.ID);
            Assert.NotNull(actualAmenity);
            Assert.Equal(updatedAmenityDTO.Name, actualAmenity.Name);
        }

        

    }
}