namespace MadDroid.Helpers
{
    /// <summary>
    /// Class to convert bytes to many prefixes
    /// </summary>
    public sealed class ByteConversion
    {
        #region Fields

        private static readonly ulong EiB = 0x1000000000000000;
        private static readonly ulong PiB = 0x4000000000000;
        private static readonly ulong TiB = 0x10000000000;
        private static readonly ulong GiB = 0x40000000;
        private static readonly ulong MiB = 0x100000;
        private static readonly ulong KiB = 0x400;

        private static readonly ulong EB = 0xDE0B6B3A7640000;
        private static readonly ulong PB = 0x38D7EA4C68000;
        private static readonly ulong TB = 0xE8D4A51000;
        private static readonly ulong GB = 0x3B9ACA00;
        private static readonly ulong MB = 0xF4240;
        private static readonly ulong KB = 0x3E8;

        #endregion

        #region Properties

        /// <summary>
        /// The bytes length
        /// </summary>
        public ulong BytesLength { get; }

        /// <summary>
        /// The converted bytes
        /// </summary>
        public double ConvertedBytes { get; private set; }

        /// <summary>
        /// The <see cref="PrefixKind"/> of the converted bytes
        /// </summary>
        public PrefixKind Kind { get; private set; }

        /// <summary>
        /// The <see cref="Helpers.Prefix"/> of the converted bytes
        /// </summary>
        public Prefix Prefix { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of the <see cref="ByteConversion"/> class
        /// </summary>
        /// <param name="bytes">The bytes to be converted</param>
        public ByteConversion(ulong bytes) : this(bytes, PrefixKind.Binary) { }

        /// <summary>
        /// Creates a new instance of the <see cref="ByteConversion"/> class
        /// </summary>
        /// <param name="bytes">The bytes to be converted</param>
        /// <param name="kind">The kind of the bytes to be converted</param>
        public ByteConversion(ulong bytes, PrefixKind kind)
        {
            BytesLength = bytes;
            Convert(kind);
        }
        /// <summary>
        /// Creates a new instance of the <see cref="ByteConversion"/> class
        /// </summary>
        /// <param name="bytes">The bytes to be converted</param>
        /// <param name="prefix">The prefix of the bytes to be converted</param>
        public ByteConversion(ulong bytes, Prefix prefix)
        {
            BytesLength = bytes;
            Convert(prefix);
        }

        #endregion

        #region Private Methods

        private double Convert(Prefix prefix)
        {
            Prefix = prefix;

            if (BytesLength == 0)
                return BytesLength;

            switch (prefix)
            {
                case Prefix.B:
                    ConvertedBytes = BytesLength;
                    break;
                case Prefix.KB:
                    ConvertedBytes = (double)BytesLength / KB;
                    break;
                case Prefix.KiB:
                    ConvertedBytes = (double)BytesLength / KiB;
                    break;
                case Prefix.MB:
                    ConvertedBytes = (double)BytesLength / MB;
                    break;
                case Prefix.MiB:
                    ConvertedBytes = (double)BytesLength / MiB;
                    break;
                case Prefix.GB:
                    ConvertedBytes = (double)BytesLength / GB;
                    break;
                case Prefix.GiB:
                    ConvertedBytes = (double)BytesLength / GiB;
                    break;
                case Prefix.TB:
                    ConvertedBytes = (double)BytesLength / TB;
                    break;
                case Prefix.TiB:
                    ConvertedBytes = (double)BytesLength / TiB;
                    break;
                case Prefix.PB:
                    ConvertedBytes = (double)BytesLength / PB;
                    break;
                case Prefix.PiB:
                    ConvertedBytes = (double)BytesLength / PiB;
                    break;
                case Prefix.EB:
                    ConvertedBytes = (double)BytesLength / EB;
                    break;
                case Prefix.EiB:
                    ConvertedBytes = (double)BytesLength / EiB;
                    break;
                default:
                    ConvertedBytes = BytesLength;
                    break;
            }

            return ConvertedBytes;
        }

        private double Convert(PrefixKind kind)
        {
            Kind = kind;

            if (BytesLength == 0)
                return BytesLength;

            switch (kind)
            {
                case PrefixKind.Binary:
                    ConvertBinary();
                    break;
                case PrefixKind.Decimal:
                    ConvertDecimal();
                    break;
            }

            return ConvertedBytes;
        }

        private void ConvertBinary()
        {
            if (BytesLength >= EiB)
            {
                Prefix = Prefix.EiB;
                ConvertedBytes = (double)BytesLength / EiB;
            }
            else if (BytesLength >= PiB)
            {
                Prefix = Prefix.PiB;
                ConvertedBytes = (double)BytesLength / PiB;
            }
            else if (BytesLength >= TiB)
            {
                Prefix = Prefix.TiB;
                ConvertedBytes = (double)BytesLength / TiB;
            }
            else if (BytesLength >= GiB)
            {
                Prefix = Prefix.GiB;
                ConvertedBytes = (double)BytesLength / GiB;
            }
            else if (BytesLength >= MiB)
            {
                Prefix = Prefix.MiB;
                ConvertedBytes = (double)BytesLength / MiB;
            }
            else if (BytesLength >= KiB)
            {
                Prefix = Prefix.KiB;
                ConvertedBytes = (double)BytesLength / KiB;
            }
            else
            {
                Prefix = Prefix.B;
                ConvertedBytes = BytesLength;
            }
        }

        private void ConvertDecimal()
        {
            if (BytesLength >= EB)
            {
                Prefix = Prefix.EB;
                ConvertedBytes = (double)BytesLength / EB;
            }
            else if (BytesLength >= PB)
            {
                Prefix = Prefix.PB;
                ConvertedBytes = (double)BytesLength / PB;
            }
            else if (BytesLength >= TB)
            {
                Prefix = Prefix.TB;
                ConvertedBytes = (double)BytesLength / TB;
            }
            else if (BytesLength >= GB)
            {
                Prefix = Prefix.GB;
                ConvertedBytes = (double)BytesLength / GB;
            }
            else if (BytesLength >= MB)
            {
                Prefix = Prefix.MB;
                ConvertedBytes = (double)BytesLength / MB;
            }
            else if (BytesLength >= KB)
            {
                Prefix = Prefix.KB;
                ConvertedBytes = (double)BytesLength / KB;
            }
            else
            {
                Prefix = Prefix.B;
                ConvertedBytes = BytesLength;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Converts the <see cref="ConvertedBytes"/> to string with the <see cref="Prefix"/>
        /// </summary>
        /// <returns></returns>
        public override string ToString() => ConvertedBytes.ToString($"#,##0.######### {Prefix}");

        /// <summary>
        /// Converts the <see cref="ConvertedBytes"/> to string with the <see cref="Prefix"/>
        /// </summary>
        /// <param name="format">The formatting to be used</param>
        /// <returns></returns>
        public string ToString(string format) => ConvertedBytes.ToString($"{format} {Prefix}");

        /// <summary>
        /// Converts the current bytes to the give prefix
        /// </summary>
        /// <param name="prefix">The prefix of the bytes to be converted</param>
        /// <returns>The converted bytes</returns>
        public double ConvertTo(Prefix prefix) => Convert(prefix);

        /// <summary>
        /// Converts the current bytes to the give kind
        /// </summary>
        /// <param name="kind">The kind of the bytes to be converted</param>
        /// <returns>The converted bytes</returns>
        public double ConvertTo(PrefixKind kind) => Convert(kind);

        #endregion
    }

    /// <summary>
    /// Represents the prefix of converted bytes
    /// </summary>
    public enum Prefix
    {
        /// <summary>
        /// Byte
        /// </summary>
        B,
        /// <summary>
        /// Kilobyte
        /// </summary>
        KB,
        /// <summary>
        /// kibibyte
        /// </summary>
        KiB,
        /// <summary>
        /// Megabyte
        /// </summary>
        MB,
        /// <summary>
        /// Mebibyte
        /// </summary>
        MiB,
        /// <summary>
        /// Gigabyte
        /// </summary>
        GB,
        /// <summary>
        /// Gibibyte
        /// </summary>
        GiB,
        /// <summary>
        /// Terabyte
        /// </summary>
        TB,
        /// <summary>
        /// Tebibyte
        /// </summary>
        TiB,
        /// <summary>
        /// Petabyte
        /// </summary>
        PB,
        /// <summary>
        /// Pebibyte
        /// </summary>
        PiB,
        /// <summary>
        /// Exabyte
        /// </summary>
        EB,
        /// <summary>
        /// Exbibyte
        /// </summary>
        EiB
    }

    /// <summary>
    /// Represents the kind of converted bytes
    /// </summary>
    public enum PrefixKind
    {
        /// <summary>
        /// Binary kind
        /// </summary>
        Binary,
        /// <summary>
        /// Decimal kind
        /// </summary>
        Decimal
    }
}
