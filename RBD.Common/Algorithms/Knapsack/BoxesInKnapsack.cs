using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RBD.Common.Algorithms.Knapsack
{
    public class BoxesInKnapsack
    {
        private readonly List<IVolumeObj> _boxes;
        public IVolumeObj Knapsack { get; set; }
        public IEnumerable<IVolumeObj> Boxes
        {
            get { return _boxes; }
        }

        public BoxesInKnapsack(IVolumeObj knapsack)
        {
            Knapsack = knapsack;
            _boxes = new List<IVolumeObj>();
        }

        public int BoxesCount
        {
            get { return _boxes.Count; }
        }

        public bool IsEmpty
        {
            get { return BoxesCount == 0; }
        }

        public int BoxesVolume
        {
            get { return Boxes.Sum(x => x.Capacity); }
        }

        public int FreeVolume
        {
            get { return Knapsack.Capacity - BoxesVolume; }
        }

        public void Add(IVolumeObj box)
        {
            if (FreeVolume < box.Capacity)
            {
                throw new OverflowException("Нет места в рюкзаке");
            }
            _boxes.Add(box);
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("Рюкзак с вместимостью {0}", Knapsack.Capacity);
            stringBuilder.AppendLine();
            foreach (IVolumeObj box in Boxes)
            {
                stringBuilder.AppendFormat(" - {0}", box);
                stringBuilder.AppendLine();
            }
            return stringBuilder.ToString();
        }
    }
}