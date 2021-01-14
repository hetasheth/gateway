using HMS.BAL.Interface;
using HMS.DAL.Repository;
using HMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.BAL
{
    public class RoomManager : IRoomManager
    {
        private readonly IRoomRepository _roomRepository;

        public RoomManager(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }        

        public List<Room> GetRooms(string City, string Pincode, string Category, decimal? Price)
        {
            return _roomRepository.GetRooms(City, Pincode, Category, Price);
        }

        public string Create(Room model)
        {
            return _roomRepository.Create(model);
        }

        public bool CheckRoomAvailability(int RoomID, DateTime? BookDate)
        {
            return _roomRepository.CheckRoomAvailability(RoomID, BookDate);
        }
    }
}
