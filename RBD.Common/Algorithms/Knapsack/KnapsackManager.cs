using System;
using System.Collections.Generic;
using System.Linq;
using RBD.Common.Extensions;

namespace RBD.Common.Algorithms.Knapsack
{
    public class BoxesManager
    {
        public List<BoxesInKnapsack> Boxes { get; private set; }

        public BoxesManager(IEnumerable<IVolumeObj> boxes)
        {
            var boxesInKnapsackQuery = boxes.OrderByDescending(x => x.Capacity).Select(x => new BoxesInKnapsack(x));
            Boxes = new List<BoxesInKnapsack>(boxesInKnapsackQuery);
        }

        public int MaxFreeVolume
        {
            get { return Boxes.Max(x => x.FreeVolume); }
        }

        public int VolumeSum
        {
            get { return Boxes.Sum(x => x.BoxesVolume); }
        }

        public bool IsFull
        {
            get { return VolumeSum == 0; }
        }

        public void Add(IVolumeObj thing)
        {
            if (MaxFreeVolume < thing.Capacity)
            {
                throw new ApplicationException("Нет места для этого объекта");
            }
            var box = Boxes.First(x => x.FreeVolume >= thing.Capacity);
            box.Add(thing); 
        }
    }

    public class KnapsackManager
    {
        public KnapsackManager(IEnumerable<IVolumeObj> knapsacks)
        {
            Knapsacks = new List<IVolumeObj>(knapsacks);
        }

        public List<IVolumeObj> Knapsacks { get; set; }

        public IEnumerable<BoxesInKnapsack> Push(IEnumerable<IVolumeObj> boxes)
        {
            var boxesArr = boxes.ToArray();
            var optimalVariant = Push(Knapsacks, boxesArr).ToArray();
            var knapsackCount = optimalVariant.Count();
            var boxesVolume = boxesArr.Sum(x => x.Capacity);

            var variants = Knapsacks.GetAllVariants(knapsackCount);

            foreach (IVolumeObj[] knapsacksVariant in variants)
            {
                var variantCapacity = knapsacksVariant.Sum(x => x.Capacity);

                // Не хватает вместимости
                if (variantCapacity < boxesVolume) continue;

                // Вместимость временно оптимального варианта
                var optimalCapacity = optimalVariant.Sum(x => x.Knapsack.Capacity);

                // Этот вариант по вместимости не лучше - пропускаем
                if (optimalCapacity <= variantCapacity) continue;

                try
                {
                    optimalVariant = Push(knapsacksVariant, boxesArr).ToArray();
                }
                catch
                {
                }
            }

            return optimalVariant;
        }

        public IEnumerable<BoxesInKnapsack> FastPush(IEnumerable<IVolumeObj> boxes)
        {
            return Push(Knapsacks, boxes).ToArray();
        }

        private IEnumerable<BoxesInKnapsack> Push(IEnumerable<IVolumeObj> knapsacks, IEnumerable<IVolumeObj> boxes)
        {
            var boxesInKnapsackQuery = knapsacks.OrderByDescending(x => x.Capacity).Select(x => new BoxesInKnapsack(x));
            var result = new List<BoxesInKnapsack>(boxesInKnapsackQuery);

            foreach (IVolumeObj box in boxes.OrderByDescending(x => x.Capacity))
            {
                var placeInKnapsack = result.FirstOrDefault(x => x.FreeVolume >= box.Capacity);
                if (placeInKnapsack == null)
                {
                    throw new OverflowException("Коробки не влезают в рюкзак");
                }
                placeInKnapsack.Add(box);
            }

            return result.Where(x => !x.IsEmpty);
        }

        public IEnumerable<BoxesInKnapsack> SortedPush(IEnumerable<IVolumeObj> boxes)
        {
            var boxesInKnapsackQuery = Knapsacks.OrderByDescending(x => x.Capacity).Select(x => new BoxesInKnapsack(x));
            var result = new List<BoxesInKnapsack>(boxesInKnapsackQuery);

            foreach (IVolumeObj box in boxes)
            {
                var placeInKnapsack = result.FirstOrDefault(x => x.FreeVolume >= box.Capacity);
                if (placeInKnapsack == null)
                {
                    throw new OverflowException("Коробки не влезают в рюкзак");
                }
                placeInKnapsack.Add(box);
            }

            return result.Where(x => !x.IsEmpty);
        }
    }
}