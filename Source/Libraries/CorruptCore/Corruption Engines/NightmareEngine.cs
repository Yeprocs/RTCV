using System;
using System.Linq;

namespace RTCV.CorruptCore
{
    using System.Windows.Forms;
    using RTCV.Common.CustomExtensions;
    using RTCV.NetCore;
    using RTCV.CorruptCore.Extensions;

    public static class NightmareEngine
    {
        public static NightmareAlgo Algo
        {
            get => (NightmareAlgo)AllSpec.CorruptCoreSpec[RTCSPEC.NIGHTMARE_ALGO];
            set => AllSpec.CorruptCoreSpec.Update(RTCSPEC.NIGHTMARE_ALGO, value);
        }

        public static bool ShouldUseRanges
        {
            get => (bool)AllSpec.CorruptCoreSpec[RTCSPEC.NIGHTMARE_SHOULDUSERANGES];
            set => AllSpec.CorruptCoreSpec.Update(RTCSPEC.NIGHTMARE_SHOULDUSERANGES, value);
        }
        
        public static (ulong Min, ulong Max)[] Ranges
        {
            get =>
                RtcCore.CurrentPrecision switch
                {
                    1 => Ranges8Bit,
                    2 => Ranges16Bit,
                    4 => Ranges32Bit,
                    8 => Ranges64Bit,
                    _ => Array.Empty<(ulong, ulong)>()
                };
            set
            {
                _ = RtcCore.CurrentPrecision switch
                {
                    1 => Ranges8Bit = value,
                    2 => Ranges16Bit = value,
                    4 => Ranges32Bit = value,
                    8 => Ranges64Bit = value,
                    _ => Array.Empty<(ulong, ulong)>()
                };
            }
        }

        public static (ulong Min, ulong Max)[] Ranges8Bit
        {
            get => ((ulong, ulong)[])AllSpec.CorruptCoreSpec[RTCSPEC.NIGHTMARE_RANGES8BIT];
            set => AllSpec.CorruptCoreSpec.Update(RTCSPEC.NIGHTMARE_RANGES8BIT, value);
        }

        public static (ulong Min, ulong Max)[] Ranges16Bit
        {
            get => ((ulong, ulong)[])AllSpec.CorruptCoreSpec[RTCSPEC.NIGHTMARE_RANGES16BIT];
            set => AllSpec.CorruptCoreSpec.Update(RTCSPEC.NIGHTMARE_RANGES16BIT, value);
        }

        public static (ulong Min, ulong Max)[] Ranges32Bit
        {
            get => ((ulong, ulong)[])AllSpec.CorruptCoreSpec[RTCSPEC.NIGHTMARE_RANGES32BIT];
            set => AllSpec.CorruptCoreSpec.Update(RTCSPEC.NIGHTMARE_RANGES32BIT, value);
        }

        public static (ulong Min, ulong Max)[] Ranges64Bit
        {
            get => ((ulong, ulong)[])AllSpec.CorruptCoreSpec[RTCSPEC.NIGHTMARE_RANGES64BIT];
            set => AllSpec.CorruptCoreSpec.Update(RTCSPEC.NIGHTMARE_RANGES64BIT, value);
        }
        
        public static ulong MinValue
        {
            get =>
                RtcCore.CurrentPrecision switch
                {
                    1 => MinValue8Bit,
                    2 => MinValue16Bit,
                    4 => MinValue32Bit,
                    8 => MinValue64Bit,
                    _ => 0
                };
            set
            {
                _ = RtcCore.CurrentPrecision switch
                {
                    1 => MinValue8Bit = value,
                    2 => MinValue16Bit = value,
                    4 => MinValue32Bit = value,
                    8 => MinValue64Bit = value,
                    _ => 0ul
                };
            }
        }

        public static ulong MaxValue
        {
            get =>
                RtcCore.CurrentPrecision switch
                {
                    1 => MaxValue8Bit,
                    2 => MaxValue16Bit,
                    4 => MaxValue32Bit,
                    8 => MaxValue64Bit,
                    _ => 0
                };
            set
            {
                _ = RtcCore.CurrentPrecision switch
                {
                    1 => MaxValue8Bit = value,
                    2 => MaxValue16Bit = value,
                    4 => MaxValue32Bit = value,
                    8 => MaxValue64Bit = value,
                    _ => 0ul
                };
            }
        }

        public static ulong MinValue8Bit
        {
            get => (ulong)AllSpec.CorruptCoreSpec[RTCSPEC.NIGHTMARE_MINVALUE8BIT];
            set => AllSpec.CorruptCoreSpec.Update(RTCSPEC.NIGHTMARE_MINVALUE8BIT, value);
        }

        public static ulong MaxValue8Bit
        {
            get => (ulong)AllSpec.CorruptCoreSpec[RTCSPEC.NIGHTMARE_MAXVALUE8BIT];
            set => AllSpec.CorruptCoreSpec.Update(RTCSPEC.NIGHTMARE_MAXVALUE8BIT, value);
        }

        public static ulong MinValue16Bit
        {
            get => (ulong)AllSpec.CorruptCoreSpec[RTCSPEC.NIGHTMARE_MINVALUE16BIT];
            set => AllSpec.CorruptCoreSpec.Update(RTCSPEC.NIGHTMARE_MINVALUE16BIT, value);
        }

        public static ulong MaxValue16Bit
        {
            get => (ulong)AllSpec.CorruptCoreSpec[RTCSPEC.NIGHTMARE_MAXVALUE16BIT];
            set => AllSpec.CorruptCoreSpec.Update(RTCSPEC.NIGHTMARE_MAXVALUE16BIT, value);
        }

        public static ulong MinValue32Bit
        {
            get => (ulong)AllSpec.CorruptCoreSpec[RTCSPEC.NIGHTMARE_MINVALUE32BIT];
            set => AllSpec.CorruptCoreSpec.Update(RTCSPEC.NIGHTMARE_MINVALUE32BIT, value);
        }

        public static ulong MaxValue32Bit
        {
            get => (ulong)AllSpec.CorruptCoreSpec[RTCSPEC.NIGHTMARE_MAXVALUE32BIT];
            set => AllSpec.CorruptCoreSpec.Update(RTCSPEC.NIGHTMARE_MAXVALUE32BIT, value);
        }

        public static ulong MinValue64Bit
        {
            get => (ulong)AllSpec.CorruptCoreSpec[RTCSPEC.NIGHTMARE_MINVALUE64BIT];
            set => AllSpec.CorruptCoreSpec.Update(RTCSPEC.NIGHTMARE_MINVALUE64BIT, value);
        }

        public static ulong MaxValue64Bit
        {
            get => (ulong)AllSpec.CorruptCoreSpec[RTCSPEC.NIGHTMARE_MAXVALUE64BIT];
            set => AllSpec.CorruptCoreSpec.Update(RTCSPEC.NIGHTMARE_MAXVALUE64BIT, value);
        }


        public static PartialSpec getDefaultPartial()
        {
            var partial = new PartialSpec("RTCSpec")
            {
                [RTCSPEC.NIGHTMARE_MINVALUE8BIT] = 0UL,
                [RTCSPEC.NIGHTMARE_MAXVALUE8BIT] = 0xFFUL,
                [RTCSPEC.NIGHTMARE_MINVALUE16BIT] = 0UL,
                [RTCSPEC.NIGHTMARE_MAXVALUE16BIT] = 0xFFFFUL,
                [RTCSPEC.NIGHTMARE_MINVALUE32BIT] = 0UL,
                [RTCSPEC.NIGHTMARE_MAXVALUE32BIT] = 0xFFFFFFFFUL,
                [RTCSPEC.NIGHTMARE_MINVALUE64BIT] = 0UL,
                [RTCSPEC.NIGHTMARE_MAXVALUE64BIT] = 0xFFFFFFFFFFFFFFFFUL,
                [RTCSPEC.NIGHTMARE_ALGO] = NightmareAlgo.RANDOM,
                [RTCSPEC.NIGHTMARE_SHOULDUSERANGES] = false,
                [RTCSPEC.NIGHTMARE_RANGES8BIT] = Array.Empty<(ulong, ulong)>(),
                [RTCSPEC.NIGHTMARE_RANGES16BIT] = Array.Empty<(ulong, ulong)>(),
                [RTCSPEC.NIGHTMARE_RANGES32BIT] = Array.Empty<(ulong, ulong)>(),
                [RTCSPEC.NIGHTMARE_RANGES64BIT] = Array.Empty<(ulong, ulong)>(),
            };

            return partial;
        }

        private static NightmareType _type = NightmareType.SET;

        public static BlastUnit GenerateUnit(string domain, long address, int precision, int alignment, bool useAlignment, byte[] replacementValue = null)
        {
            // Randomly selects a memory operation according to the selected algorithm

            switch (Algo)
            {
                case NightmareAlgo.RANDOM: //RANDOM always sets a random value
                    _type = NightmareType.SET;
                    break;

                case NightmareAlgo.RANDOMTILT: //RANDOMTILT may add 1,substract 1 or set a random value
                    int result = RtcCore.RND.Next(1, 4);
                    switch (result)
                    {
                        case 1:
                            _type = NightmareType.ADD;
                            break;
                        case 2:
                            _type = NightmareType.SUBTRACT;
                            break;
                        case 3:
                            _type = NightmareType.SET;
                            break;
                        default:
                            MessageBox.Show("Random returned an unexpected value (NightmareEngine switch(Algo) RANDOMTILT)");
                            return null;
                    }

                    break;

                case NightmareAlgo.TILT: //TILT can either add 1 or substract 1
                    result = RtcCore.RND.Next(1, 3);
                    switch (result)
                    {
                        case 1:
                            _type = NightmareType.ADD;
                            break;

                        case 2:
                            _type = NightmareType.SUBTRACT;
                            break;

                        default:
                            MessageBox.Show("Random returned an unexpected value (NightmareEngine switch(Algo) TILT)");
                            return null;
                    }
                    break;
            }


            if (domain == null)
            {
                return null;
            }

            MemoryInterface mi = MemoryDomains.GetInterface(domain);

            byte[] value = new byte[precision];

            long safeAddress = address;
            if (useAlignment)
                safeAddress = safeAddress - (address % precision) + alignment;
            if (safeAddress > mi.Size - precision && mi.Size > precision)
            {
                safeAddress = mi.Size - (2 * precision) + alignment; //If we're out of range, hit the last aligned address
            }

            if (_type == NightmareType.SET)
            {
                ulong randomValue = 0;

                bool def = false;
                switch (precision)
                {
                    case 1:
                        randomValue = ShouldUseRanges
                            ? GetRandomFromRanges(Ranges8Bit)
                            : RtcCore.RND.NextULong(MinValue8Bit, MaxValue8Bit, true);
                        break;
                    case 2:
                        randomValue = ShouldUseRanges
                            ? GetRandomFromRanges(Ranges16Bit)
                            : RtcCore.RND.NextULong(MinValue16Bit, MaxValue16Bit, true);
                        break;
                    case 4:
                        randomValue = ShouldUseRanges
                            ? GetRandomFromRanges(Ranges32Bit)
                            : RtcCore.RND.NextULong(MinValue32Bit, MaxValue32Bit, true);
                        break;
                    case 8:
                        randomValue = ShouldUseRanges
                            ? GetRandomFromRanges(Ranges64Bit)
                            : RtcCore.RND.NextULong(MinValue64Bit, MaxValue64Bit, true);
                        break;
                    default:
                        def = true;
                        break;
                }

                if (replacementValue == null)
                {
                    if (def)
                    {
                        for (int i = 0; i < precision; i++)
                        {
                            value[i] = (byte)RtcCore.RND.Next();
                        }
                    }
                    else
                    {
                        value = ByteArrayExtensions.GetByteArrayValue(precision, randomValue, true);
                    }
                }
                else
                {
                    value = replacementValue;
                }

                return new BlastUnit(value, domain, safeAddress, precision, mi.BigEndian, 0, RtcCore.CreateInfiniteUnits ? 0 : 1);
            }
            BlastUnit bu = new BlastUnit(StoreType.ONCE, StoreTime.PREEXECUTE, domain, safeAddress, domain, safeAddress, precision, mi.BigEndian, 0, RtcCore.CreateInfiniteUnits ? 0 : 1);
            switch (_type)
            {
                case NightmareType.ADD:
                    bu.TiltValue = 1;
                    break;
                case NightmareType.SUBTRACT:
                    bu.TiltValue = -1;
                    break;
                default:
                    bu.TiltValue = 0;
                    break;
            }
            return bu;
        }
        
        private static ulong GetRandomFromRanges((ulong Min, ulong Max)[] ranges)
        {
            for (int i = 0; i < ranges.Length; i++)
            {
                ref var range = ref ranges[i];
                if (range.Max < range.Min)
                {
                    (range.Max, range.Min) = (range.Min, range.Max);
                }
            }

            decimal cumulativeLength = ranges.Sum(x => (decimal)(x.Max - x.Min + 1));
            decimal diceRoll = (decimal)RtcCore.RND.NextDouble() * cumulativeLength;
            decimal runningLength = 0;
            foreach (var range in ranges)
            {
                runningLength += range.Max - range.Min + 1;
                if (diceRoll <= runningLength)
                {
                    return RtcCore.RND.NextULong(range.Min, range.Max, true);
                }
            }

            return 0; // Should never happen
        }
    }
}
