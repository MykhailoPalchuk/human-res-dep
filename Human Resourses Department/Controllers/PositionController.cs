using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Services;

namespace Controllers
{
    public class PositionController
    {
        static PositionService positionService = new PositionService();

        public bool AddPosition(string name, int hours, double payment)
        {
            return positionService.AddPosition(name, hours, payment);
        }

        public bool DeletePosition(int id)
        {
            return positionService.DeletePosition(id);
        }

        public bool ChangeName(int positionId, string newName)
        {
            return positionService.ChangeName(positionId, newName);
        }

        public bool ChangeHours(int positionId, int newHours)
        {
            return positionService.ChangeHours(positionId, newHours);
        }

        public bool ChangePayment(int positionId, double newPayment)
        {
            return positionService.ChangePayment(positionId, newPayment);
        }

        public List<Dictionary<string, string>> GetAllPositions()
        {
            return positionService.GetAllPositionsInfo();
        }

        public List<Dictionary<string, string>> GetTopFivePositions()
        {
            var list = GetAllPositions();
            if (list.Count == 0)
                return null;
            else
            {
                list.Sort(delegate (Dictionary<string, string> d1, Dictionary<string, string> d2)
                {
                    string a1, a2, b1, b2;
                    d1.TryGetValue("hours", out a1);
                    d1.TryGetValue("payment", out b1);
                    d2.TryGetValue("hours", out a2);
                    d2.TryGetValue("payment", out b2);
                    double h1 = double.Parse(a1);
                    double p1 = double.Parse(b1);
                    double h2 = double.Parse(a2);
                    double p2 = double.Parse(b2);

                    double first = h1 / p1;
                    double second = h2 / p2;
                    return first.CompareTo(second);
                });
                if (list.Count > 0 && list.Count < 5)
                    return list;
                else
                    return (List<Dictionary<string, string>>)list.Take(5);
            }
        }

        public Dictionary<string, string> GetTopWorker(int positionId)
        {
            var workers = positionService.GetWorkers(positionId);
            workers.Sort(delegate (Dictionary<string, string> d1, Dictionary<string, string> d2)
            {
                string exp1, cost1, exp2, cost2;
                d1.TryGetValue("experience", out exp1);
                d1.TryGetValue("projectsCost", out cost1);
                d2.TryGetValue("eperience", out exp2);
                d2.TryGetValue("projectsCost", out cost2);
                int e1 = int.Parse(exp1);
                int c1 = int.Parse(cost1);
                int e2 = int.Parse(exp2);
                int c2 = int.Parse(cost2);

                int first = e1 / c1;
                int second = e2 / c2;
                return first.CompareTo(second);
            });
            return workers.First();
        }
    }
}
