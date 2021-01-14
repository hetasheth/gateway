using HMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.DAL.Repository
{
    public interface IRoomRepository
    {
        List<Room> GetRooms(string City, string Pincode, string Category,decimal? Price);
        string Create(Room model);
        bool CheckRoomAvailability(int RoomID, DateTime? BookDate);
    }
}
