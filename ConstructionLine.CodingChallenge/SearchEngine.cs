using System.Collections.Generic;
using System.Linq;
using System;

namespace ConstructionLine.CodingChallenge
{

    public class SearchEngine
    {
        private readonly List<Shirt> _shirts;
        
        List<Tuple<Color, List<Guid>>> colorLists = new List<Tuple<Color, List<Guid>>>();
        List<Tuple<Size, List<Guid>>> sizeLists = new List<Tuple<Size, List<Guid>>>();

        public SearchEngine(List<Shirt> shirts)
        {

            // TODO: data preparation and initialisation of additional data structures to improve performance goes here.

            foreach (var x in Color.All)
            {
                colorLists.Add(new Tuple<Color, List<Guid>>(x, new List<Guid>()));
            }

            foreach (var x in Size.All) 
            {
                sizeLists.Add(new Tuple<Size, List<Guid>>(x, new List<Guid>()));
            }

            foreach (var x in shirts)
            {
                colorLists.Where(item=>item.Item1 == x.Color).Single().Item2.Add(x.Id);
                sizeLists.Where(item => item.Item1 == x.Size).Single().Item2.Add(x.Id);
            }

            _shirts = shirts;
        }


        public SearchResults Search(SearchOptions options)
        {
           var colorCounts = new List<ColorCount>();
            foreach (var x in colorLists) 
            {
                var colorCount = new ColorCount();
                colorCount.Color = x.Item1;
                colorCount.Count = options.Colors.Contains(x.Item1) ? x.Item2.Count() : 0;
                colorCounts.Add(colorCount);
            }

            var sizeCounts = new List<SizeCount>();
            foreach (var x in sizeLists) 
            {
                var sizeCount = new SizeCount();
                sizeCount.Size = x.Item1; 
                sizeCount.Count = options.Sizes.Contains(x.Item1) ? x.Item2.Count() : 0;
                sizeCounts.Add(sizeCount);
            }

            return new SearchResults
            {
                Shirts = _shirts,  
                ColorCounts = colorCounts,
                SizeCounts = sizeCounts
            };
        }
    }
}