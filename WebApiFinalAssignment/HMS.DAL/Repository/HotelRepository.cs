using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMS.Models;
using AutoMapper;

namespace HMS.DAL.Repository
{
    public class HotelRepository : IHotelRepository
    {
        private readonly Database.HotelManageSystemDBEntities _dbContext;

        public HotelRepository()
        {
            _dbContext = new Database.HotelManageSystemDBEntities();

        }

        public List<Hotel> GetAllHotels()
        {
            var hList = _dbContext.Hotels.OrderBy(x => x.Name).ToList();
            List<Hotel> hotelList = new List<Hotel>();
            if (hList != null)
            {
                foreach (var items in hList)
                {
                    //// TODO: automapper mapping

                    var config = new MapperConfiguration(cfg =>cfg.CreateMap<Database.Hotel, Hotel>());
                    var mapper = new Mapper(config);
                    Hotel hotel = mapper.Map<Hotel>(items);

                    //// TODO: manually mapping

                    //Hotel hotel = new Hotel();
                    //hotel.ID = items.ID;
                    //hotel.Name = items.Name;
                    //hotel.Address = items.Address;
                    //hotel.City = items.City;
                    //hotel.Pincode = items.Pincode;
                    //hotel.ContactNumber = items.ContactNumber;
                    //hotel.ContactPerson = items.ContactPerson;
                    //hotel.Website = items.Website;
                    //hotel.Facebook = items.Facebook;
                    //hotel.Twitter = items.Twitter;
                    //hotel.IsActive = items.IsActive;
                    //hotel.CreatedBy = items.CreatedBy;
                    //hotel.CreatedDate = items.CreatedDate;
                    //hotel.UpdatedBy = items.UpdatedBy;
                    //hotel.UpdatedDate = items.UpdatedDate;

                    hotelList.Add(hotel);
                }
            }
            return hotelList;
        }

        public string Create(Hotel model)
        {
            try
            {                
                if (model != null)
                {
                    //// TODO: automapper mapping

                    var config = new MapperConfiguration(cfg => cfg.CreateMap<Hotel, Database.Hotel>());
                    var mapper = new Mapper(config);
                    Database.Hotel hotel = mapper.Map<Database.Hotel>(model);

                    //// TODO: manually mapping

                    //Database.Hotel hotel = new Database.Hotel();
                    //hotel.Name = model.Name;
                    //hotel.Address = model.Address;
                    //hotel.City = model.City;
                    //hotel.Pincode = model.Pincode;
                    //hotel.ContactNumber = model.ContactNumber;
                    //hotel.ContactPerson = model.ContactPerson;
                    //hotel.Website = model.Website;
                    //hotel.Facebook = model.Facebook;
                    //hotel.Twitter = model.Twitter;
                    //hotel.IsActive = model.IsActive;
                    //hotel.CreatedBy = model.CreatedBy;
                    //hotel.CreatedDate = DateTime.Now;
                    //hotel.UpdatedBy = model.UpdatedBy;
                    //hotel.UpdatedDate = DateTime.Now;

                    _dbContext.Hotels.Add(hotel);
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


    }
}
