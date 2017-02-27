using System.Collections.Generic;
using System.Linq;

namespace Models.Services
{
    public class PositionService
    {
        static WorkerService workerService = new WorkerService();

        public bool AddPosition(string name, int hours, double payment)
        {
            if (FindPosition(name) == null)
            {
                using (var db = new DataContext())
                {
                    db.Positions.Add(new Position { Name = name, Hours = hours, Payment = payment });
                    db.SaveChanges();
                    return true;
                }
            }
            else
                return false;
        }

        public bool DeletePosition(int id)
        {
            using (var db = new DataContext())
            {
                if (db.Positions.Find(id) != null)
                {
                    db.Positions.Remove(db.Positions.Find(id));
                    db.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
        }

        public Position FindPosition(string name)
        {
            using (var db = new DataContext())
            {
                foreach(var pos in db.Positions.ToList())
                {
                    if (pos.Name.Equals(name))
                        return pos;
                }
                return null;
            }
        }

        public Position FindPosition(int id)
        {
            using (var db = new DataContext())
            {
                return db.Positions.Find(id);
                
            }
        }

        public Dictionary<string, string> GetPositionInfo(int id)
        {
            var p = FindPosition(id);
            if (p != null)
            {
                var position = new Dictionary<string, string>();
                position.Add("id", p.Id.ToString());
                position.Add("name", p.Name);
                position.Add("hours", p.Hours.ToString());
                position.Add("payment", p.Payment.ToString());
                position.Add("totalPayment", p.TotalPayment.ToString());
                return position;
            }
            else
                return null;
        }

        public List<Dictionary<string, string>> GetAllPositionsInfo()
        {
            using (var db = new DataContext())
            {
                var list = new List<Dictionary<string, string>>();
                foreach(var p in db.Positions.ToList())
                {
                    list.Add(GetPositionInfo(p.Id));
                }
                return list;
            }
        }

        public List<Dictionary<string,string>> GetWorkers(int positionId)
        {
            using (var db = new DataContext())
            {
                if (db.Positions.Find(positionId) != null)
                {
                    var list = new List<Dictionary<string, string>>();
                    foreach (var w in db.Positions.Find(positionId).Workers)
                    {
                        list.Add(workerService.GetWorkerInfo(w.Id));
                    }
                    return list;
                }
                else
                    return null;
            }
        }

        public bool ChangeName(int positionId, string newName)
        {
            using (var db = new DataContext())
            {
                if (db.Positions.Find(positionId) != null)
                {
                    db.Positions.Find(positionId).Name = newName;
                    db.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
        }

        public bool ChangeHours(int positionId, int newHours)
        {
            using (var db = new DataContext())
            {
                if (db.Positions.Find(positionId) != null)
                {
                    db.Positions.Find(positionId).Hours = newHours;
                    db.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
        }

        public bool ChangePayment(int positionId, double newPayment)
        {
            using (var db = new DataContext())
            {
                if (db.Positions.Find(positionId) != null)
                {
                    db.Positions.Find(positionId).Payment = newPayment;
                    db.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
        }
    }
}
