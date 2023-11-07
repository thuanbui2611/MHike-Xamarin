using MHike.Model;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MHike
{
    public class SQLiteHelper
    {
        private readonly SQLiteAsyncConnection db;

        public SQLiteHelper(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<HikeModel>();
            db.CreateTableAsync<ObservationModel>();
        }

        public Task<int> CreateHike(HikeModel hike)
        {
            return db.InsertAsync(hike);
        }

        public Task<List<HikeModel>> GetAllHike()
        {
            return db.Table<HikeModel>().ToListAsync();
        }
        public Task<int> UpdateHike(HikeModel hike)
        {
            return db.UpdateAsync(hike);
        }
        public Task<int> DeleteHike(HikeModel hike)
        {
            return db.DeleteAsync(hike);
        }

        public Task<List<HikeModel>> Search(string search)
        {
            return db.Table<HikeModel>()
                .Where(hike => hike.Name.Contains(search) ||
                               hike.Level.Contains(search) ||
                               hike.Location.Contains(search) ||
                               hike.Date.Contains(search))
                .ToListAsync();
        }

        public Task<List<ObservationModel>> GetObservationByHikeId(int hikeId)
        {
            var result = db.Table<ObservationModel>()
                .Where(observation => observation.HikeID == hikeId)
                .ToListAsync();
            return result;
        }

        public Task<int> CreateObservation(ObservationModel observation)
        {
            return db.InsertAsync(observation);
        }

        public Task<int> UpdateObservation(ObservationModel observation)
        {
            return db.UpdateAsync(observation);
        }

        public Task<int> DeleteObservation(ObservationModel observation)
        {

            return db.DeleteAsync(observation);
        }
    }
}
