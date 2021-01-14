using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.Models;
using AutoMapper;

namespace HMS.DAL.Repository
{
    public class RoomRepository : IRoomRepository
    {
        private readonly Database.HotelManageSystemDBEntities _dbContext;

        public RoomRepository()
        {
            _dbContext = new Database.HotelManageSystemDBEntities();
        }

        public List<Room> GetRooms(string City, string Pincode, string Category, decimal? Price)
        {
            var rList = _dbContext.Rooms.OrderBy(x=>x.Price).ToList();
            if (City != "")
            {
                rList = rList.Where(x => x.Hotel.City.Equals(City)).ToList();
            }
            if (Pincode != "")
            {
                rList = rList.Where(x => x.Hotel.Pincode.Equals(Pincode)).ToList();
            }
            if (Category != "")
            {
                rList = rList.Where(x => x.Category.Equals(Category)).ToList();
            }
            if (Price != null)
            {
                rList = rList.Where(x => x.Price.Equals(Price)).ToList();
            }

            List<Room> roomList = new List<Room>();
            if (rList != null)
            {
                foreach (var items in rList)
                {
                    //// TODO: automapper mapping

                    var config = new MapperConfiguration(cfg => cfg.CreateMap<Database.Room, Room>());
                    var mapper = new Mapper(config);
                    Room room = mapper.Map<Room>(items);

                    //// TODO: manually mapping
                    
                    //Room room = new Room();
                    //room.ID = items.ID;
                    //room.Name = items.Name;
                    //room.Category = items.Category;
                    //room.Price = items.Price;
                    //room.IsActive = items.IsActive;
                    //room.CreatedBy = items.CreatedBy;
                    //room.CreatedDate = items.CreatedDate;
                    //room.UpdatedBy = items.UpdatedBy;
                    //room.UpdatedDate = items.UpdatedDate;
                    //room.HotelID = items.HotelID;

                    roomList.Add(room);
                }
            }
            return roomList;
        }

        public string Create(Room model)
        {
            try
            {
                
                if (model != null)
                {
                    //// TODO: automapper mapping

                    var config = new MapperConfiguration(cfg => cfg.CreateMap<Room, Database.Room>());
                    var mapper = new Mapper(config);
                    Database.Room room = mapper.Map<Database.Room>(model);
                    room.CreatedDate = DateTime.Now;
                    room.UpdatedDate = DateTime.Now;

                    //// TODO: manually mapping

                    //Database.Room room = new Database.Room();
                    //room.Name = model.Name;
                    //room.Category = model.Category;
                    //room.Price = model.Price;
                    //room.IsActive = model.IsActive;
                    //room.CreatedBy = model.CreatedBy;
                    //room.CreatedDate = DateTime.Now;
                    //room.UpdatedBy = model.UpdatedBy;
                    //room.UpdatedDate = DateTime.Now;
                    //room.HotelID = model.HotelID;

                    _dbContext.Rooms.Add(room);
                    _dbContext.SaveChanges();
                    return "Success";
                }
                return "Model is null";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public bool CheckRoomAvailability(int RoomID, DateTime? BookDate)
        {
            try
            {
                var bookedRoom = _dbContext.Bookings.Where(x => x.RoomID==RoomID && DbFunctions.TruncateTime(x.BookingDate)==BookDate).FirstOrDefault();
                if (bookedRoom == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
