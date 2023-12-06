using System.Linq;
using System.Text.RegularExpressions;

namespace Jacco.AOC.Challenges_2023
{
    public class Day05 : IChallenge
    {
        public string Title { get; set; } = "";
        public int Day { get; set; } = 5;
        public int Year { get; set; } = 2023;

#if false
        private class Range
        {
            public long start { get; set; }
            public long length { get; set; }
        }

        private class MapRange
        {
            public long destinationStart { get; set; }
            public long sourceStart { get; set; }
            public long length { get; set; }

            public List<Range> GetOverlappedRange(Range range)
            {
                List<Range> overlappedRanges = new();

                long rangeStart = range.start;
                long rangeEnd = range.start + range.length;

                long sourceStart = this.sourceStart;
                long sourceEnd = this.sourceStart + this.length;

                long destinationStart = this.destinationStart;

                // Add before
                if (rangeStart < sourceStart)
                {
                    // start at rangeEnd or sourceStart, whichever is lower
                    long startEnd = rangeEnd < sourceStart ? rangeEnd : sourceStart;
                    Console.WriteLine($"Adding before: {rangeStart} - {startEnd - rangeStart}");
                    Console.WriteLine(sourceStart);
                    overlappedRanges.Add(new Range()
                    {
                        start = rangeStart,
                        length = startEnd - rangeStart
                    });
                }

                // add after
                if (rangeEnd > sourceEnd)
                {
                    // start at rangeStart or sourceEnd, whichever is higher
                    long endStart = rangeStart > sourceEnd ? rangeStart : sourceEnd;
                    Console.WriteLine($"Adding after: {endStart} - {rangeEnd - sourceEnd}");
                    overlappedRanges.Add(new Range()
                    {
                        start = endStart,
                        length = rangeEnd - sourceEnd
                    });
                }

                // Add mapped overlap
                bool hasOverlap = rangeStart < sourceEnd && rangeEnd > sourceStart;
                if (hasOverlap)
                {
                    long start = rangeStart > sourceStart ? rangeStart : sourceStart;
                    long end = rangeEnd < sourceEnd ? rangeEnd : sourceEnd;

                    long mappedStart = destinationStart + (start - sourceStart);
                    long mappedEnd = destinationStart + (end - sourceStart);

                    Console.WriteLine($"Adding overlap: {mappedStart} - {mappedEnd - mappedStart}");
                    overlappedRanges.Add(new Range()
                    {
                        start = mappedStart,
                        length = mappedEnd - mappedStart
                    });
                }

                return overlappedRanges;
            }
        }

        private class Map
        {
            public string from;
            public string to;

            public IReadOnlyList<MapRange> ranges { get; }

            public Map(string[] input)
            {
                // fertilizer-to-water map:
                string fromTo = input[0];
                from = fromTo.Split("-")[0].Trim();
                to = fromTo.Split("-")[2].Split(" ")[0].Trim();

                // // Cut list of strings up in ranges
                ranges = input[1..]
                    .Select(s => s.Split(" ", StringSplitOptions.RemoveEmptyEntries))
                    .Select(parts => new MapRange()
                    {
                        destinationStart = long.Parse(parts[0]),
                        sourceStart = long.Parse(parts[1]),
                        length = long.Parse(parts[2])
                    })
                    .ToList();
            }

            public List<Range> GetMappedRanges(List<Range> rangesToMap)
            {
                // Initialize list with range
                List<Range> mappedRanges = rangesToMap;

                foreach (MapRange mapRange in ranges)
                {
                    // mappedRanges.AddRange(mapRange.GetOverlappedRange(rangeToMap));
                    mappedRanges = mappedRanges.SelectMany(r => mapRange.GetOverlappedRange(r)).ToList();
                }

                return mappedRanges;
            }

            public long GetMappedValue(long num)
            {
                foreach (MapRange range in ranges)
                {
                    if (num >= range.sourceStart && num < range.sourceStart + range.length)
                    {
                        return range.destinationStart + (num - range.sourceStart);
                    }
                }

                return num;
            }
        }

        private class Mapper()
        {
            public List<Map> maps = new();

            public Mapper(string[] input) : this()
            {
                // Cut list of strings up in map 
                maps = string.Join("\n", input)
                    .Split(new[] { "\n\n" }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => s.Split("\n"))
                    .Select(m => new Map(m))
                    .ToList();
            }

            public long GetLocation(long seed)
            {
                long var = seed;

                foreach (Map map in maps)
                {
                    var = map.GetMappedValue(var);
                }

                return var;
            }

            public List<Range> GetLocationRanges(Range seedRange)
            {
                var ranges = new List<Range>() { seedRange };

                foreach (Map map in maps)
                {
                    ranges = map.GetMappedRanges(ranges);
                    if (ranges.Count > 0) Console.WriteLine($"{map.from} -> {map.to}: {seedRange.start} -> {ranges[0].start}");
                }

                return ranges;
            }
        }

        public int Part1(string[] input)
        {
            List<long> locations = new();

            // Cut list of strings up in seeds
            List<long> seeds = Regex.Matches(input[0], @"\d+")
                .Select(m => long.Parse(m.Value))
                .ToList();

            // Cut list of strings up in map 
            Mapper mapper = new(input[2..]);

            // Get location number for all seeds
            seeds.ForEach(seed =>
            {
                locations.Add(mapper.GetLocation(seed));
            });

            return int.Parse(locations.Min().ToString());
        }

#endif

        public int Part1(string[] input)
        {
            return 0;
        }

        // Range struct
        private struct Range
        {
            public long start;
            public long end;

            public override string ToString()
            {
                return $"[{start} - {end}]";
            }

            public bool Contains(long num)
            {
                return num >= start && num < end;
            }

            // Return the range where the two ranges overlap
            public Range Intersect(Range range)
            {
                return new Range()
                {
                    start = start > range.start ? start : range.start,
                    end = end < range.end ? end : range.end
                };
            }

            public Range Join(Range range)
            {
                return new Range()
                {
                    start = start < range.start ? start : range.start,
                    end = end > range.end ? end : range.end
                };
            }
        }

        private class RangeMapper
        {
            public Range from { get; set; }
            public Range to { get; set; }

            public RangeMapper(Range from, Range to)
            {
                this.from = from;
                this.to = to;
            }

            public List<Range> MapRange(Range range)
            {
                List<Range> mappedRanges = new();

                List<long> barriers = [range.start];
                if (range.Contains(from.start)) barriers.Add(from.start);
                if (range.Contains(from.end)) barriers.Add(from.end);
                barriers.Add(range.end);

                // Create Ranges from barriers
                for (int i = 0; i < barriers.Count - 1; i++)
                {
                    long start = barriers[i];
                    long end = barriers[i + 1];

                    // Map if it's within from
                    if (from.Contains(start) && from.Contains(end))
                    {
                        start += to.start - from.start;
                        end += to.start - from.start;
                    }

                    mappedRanges.Add(new Range()
                    {
                        start = start,
                        end = end
                    });
                }

                Console.WriteLine($"Mapped {range} to {mappedRanges[0]}");

                return mappedRanges;
            }
        }

        private class Mapper
        {
            public string from { get; }
            public string to { get; }
            public List<RangeMapper> rangeMappers { get; set; } = [];

            public Mapper(string from, string to)
            {
                this.from = from;
                this.to = to;
            }

            public override string ToString()
            {
                string s = $"[Mapper] {from}-to-{to}\n";

                // rangeMappers.ForEach(rm => s += $"  {rm.from.start} - {rm.from.end} -> {rm.to.start} - {rm.to.end}\n");

                return $"[Mapper] {from}-to-{to}";
            }

            public void AddRangeMapper(RangeMapper rangeMapper)
            {
                rangeMappers.Add(rangeMapper);
            }

            public List<Range> MapRanges(List<Range> ranges)
            {
                Console.WriteLine(this);

                List<Range> mappedRanges = ranges;

                rangeMappers.ForEach(rangeMapper =>
                {
                    ranges.ForEach(range =>
                    {
                        mappedRanges = rangeMapper.MapRange(range);
                    });
                });

                // Print all ranges
                mappedRanges.ForEach(range => Console.WriteLine(range));

                return mappedRanges;
            }
        }

        public int Part2(string[] input)
        {
            List<Range> seedRanges = Regex.Matches(input[0], @"\d+ \d+")
                .Select(match =>
                {
                    string[] parts = match.Value.Split(" ");
                    return new Range()
                    {
                        start = long.Parse(parts[0]),
                        end = long.Parse(parts[1])
                    };
                })
                .ToList();

            // Create mappers from input 
            List<Mapper> mappers = new();

            input[2..]
                .ToList()
                .ForEach(line =>
                {
                    if (line.Trim() == "") return;
                    if (line.Contains("map:"))
                    {
                        string fromName = line.Split("-")[0].Trim();
                        string toName = line.Split("-")[2].Split(" ")[0].Trim();
                        mappers.Add(new Mapper(fromName, toName));
                    }
                    else
                    {
                        string[] parts = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                        long destinationRangeStart = long.Parse(parts[0]);
                        long sourceRangeStart = long.Parse(parts[1]);
                        long length = long.Parse(parts[2]);

                        Range fromRange = new Range()
                        {
                            start = sourceRangeStart,
                            end = sourceRangeStart + length
                        };

                        Range toRange = new Range()
                        {
                            start = destinationRangeStart,
                            end = destinationRangeStart + length
                        };

                        mappers.Last().AddRangeMapper(new RangeMapper(fromRange, toRange));
                    }
                });

            List<Range> ranges = new();

            // Ensure MapRanges works
            ranges.Add(new Range(){start = 82, end = 83});

            mappers.ForEach(mapper =>
            {
                ranges = mapper.MapRanges(ranges);
            });

            return 0;

#if false
            // Cut list of strings up in pairs of seed: range
            List<Range> seedRanges = Regex.Matches(input[0], @"\d+ \d+")
                .Select(m => {
                    string[] parts = m.Value.Split(" ");
                    return new Range()
                    {
                        start = long.Parse(parts[0]),
                        length = long.Parse(parts[1])
                    };
                })
                .ToList();
            
            // Cut list of strings up in map 
            Mapper mapper = new(input[2..]);

            // Get location number for all seeds
            List<Range> ranges = new(); 
            // seedRanges.ForEach(seedRange =>
            // {
            //     ranges.AddRange(mapper.GetLocationRanges(seedRange));
            // });
            ranges.AddRange(mapper.GetLocationRanges(new Range(){start = 82,length = 1}));

            // Get lowest in ranges
            List<long> locations = ranges.Select(r => r.start).ToList();

            return int.Parse(locations.Min().ToString());
#endif
        }
    }
}