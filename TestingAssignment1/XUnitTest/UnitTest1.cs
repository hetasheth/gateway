using System;
using Xunit;
using Moq;
using Newtonsoft.Json;
using TestingWebAPI.Repository.Interface;
using TestingWebAPI.Controllers;
using TestingWebAPI.Models;
using System.Collections.Generic;
using System.Web.Http.Results;
using System.IO;
using System.Web.Http;
using System.Linq;

namespace XUnitTest
{
    public class UnitTest1
    {
        private readonly Mock<IPassengerRepository> mockDataRepo = new Mock<IPassengerRepository>();
        private readonly PassengerController _passengerController;
        public UnitTest1()
        {
            _passengerController = new PassengerController(mockDataRepo.Object);
        }

        [Fact]
        public void TestGetPassengers()
        {
            // Arrange
            var resultType = mockDataRepo.Setup(x => x.GetPassengers())
                                         .Returns(GetPassengerList().ToList().AsQueryable());
            // Act
            var response = _passengerController.GetPassengers();
            // Assert
            Assert.Equal(3, response.Count);
        }

        [Fact]
        public void TestGetPassengerById()
        {
            // Arrange
            var passenger = new Passenger();
            passenger.PassengerNumber = new Guid("3CDBC747-DB0A-4EF1-9353-6A1110DC05B9");

            var resultType = mockDataRepo.Setup(x => x.GetPassengerById(passenger.PassengerNumber)).Returns(passenger);
            // Act
            var result = _passengerController.GetPassengerById(passenger.PassengerNumber.ToString());
            // Assert
            var isNull = Assert.IsType<OkNegotiatedContentResult<Passenger>>(result);
            Assert.NotNull(isNull);
        }

        [Fact]
        public void TestRegisterPassenger()
        {
            
            var response = mockDataRepo.Setup(x => x.Register(AddPassenger())).Returns(true);

            // Act
            var result = _passengerController.RegisterPassenger(AddPassenger());
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void TestUpdatePassenger()
        {
            // Act
            var model = JsonConvert.DeserializeObject<Passenger>(File.ReadAllText("Data/UpdatePassenger.json"));

            // Arrange
            var result = mockDataRepo.Setup(x => x.Update(model)).Returns(true);
            var response = _passengerController.UpdatePassenger(model);

            // Assert
            Assert.IsType<OkResult>(response);
        }

        [Fact]
        public void TestDeletePassenger()
        {
            // Arrange
            var passenger = new Passenger();
            passenger.PassengerNumber = new Guid("71db59ab-f61c-4ff6-ba2b-2ff4026b05b9");

            var resultType = mockDataRepo.Setup(x => x.Delete(passenger.PassengerNumber.ToString())).Returns(true);
            // Act
            var response = _passengerController.DeletePassenger(passenger.PassengerNumber.ToString()) as OkNegotiatedContentResult<bool>;
            // Assert
            Assert.IsType<OkNegotiatedContentResult<bool>>(response);
        }

        private static List<Passenger> GetPassengerList()
        {
            List<Passenger> passengers = new List<Passenger>()
            {
                new Passenger()
                {
                    PassengerNumber=new Guid("3CDBC747-DB0A-4EF1-9353-6A1110DC05B9"),
                    FirstName="Sukeshi",
                    Lastname="Patel",
                    PhoneNumber="9988774411"
                },
                new Passenger()
                {
                    PassengerNumber=new Guid("71db59ab-f61c-4ff6-ba2b-2ff4026b05b9"),
                    FirstName="Heena",
                    Lastname="Khan",
                    PhoneNumber="7539510100"
                },
                new Passenger()
                {
                    PassengerNumber=new Guid("1f02f425-800a-4f8d-aa8b-c1450981ce1e"),
                    FirstName="Umang",
                    Lastname="Patel",
                    PhoneNumber="8877662200"
                }
            };
            return passengers;
        }

        private static Passenger AddPassenger()
        {
            var passenger = new Passenger()
            {
                PassengerNumber =
                new Guid("98D66512-B3F4-4107-BFB2-9E7BE0D82040"),
                FirstName = "YATIN",
                Lastname = "SHARMA",
                PhoneNumber = "123456789"
            };
            return passenger;
        }
    }
}
