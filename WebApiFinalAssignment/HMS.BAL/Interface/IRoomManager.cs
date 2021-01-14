using HMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.BAL.Interface
{
    public interface IRoomManager
    {
        List<Room> GetRooms(string City, string Pincode, string Category, decimal? Price);
        string Create(Room model);
        bool CheckRoomAvailability(int RoomID, DateTime? BookDate);
    }
}
