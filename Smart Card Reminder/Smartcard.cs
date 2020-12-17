using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Smart_Card_Reminder
{
    internal class Smartcard
    {
        /// <summary>
        /// debug mode
        /// </summary>
        private static readonly bool debug = true;

        /// <summary>
        /// last check for debug value
        /// </summary>
        private static DateTime lastCheck;

        /// <summary>
        /// Returns value if application is in debug mode
        /// </summary>
        /// <returns></returns>
        private static bool GetDebugValue()
        {
            if (lastCheck == new DateTime())
            {
                lastCheck = DateTime.Now;
                return true;
            }
            if ((DateTime.Now - lastCheck).TotalMinutes > 0.3d)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Scope for smart card
        /// </summary>
        public static class SmartCardScope
        {
            public static readonly int User = 0;
            public static readonly int Terminal = 1;
            public static readonly int System = 2;
        }

        /// <summary>
        /// Smart Card Reader States
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct SmartCardReaderState
        {
            public string cardReaderString;
            public IntPtr userDataPointer;
            public uint currentState;
            public uint eventState;
            public uint atrLength;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 36)]
            public byte[] ATR;
        }
        /// <summary>
        /// Smartcard Sate
        /// </summary>
        public static class SmartCardState
        {
            public static readonly uint Unaware = 0x00000000;
            public static readonly uint Ignore = 0x00000001;
            public static readonly uint Changed = 0x00000002;
            public static readonly uint Unknown = 0x00000004;
            public static readonly uint Unavailable = 0x00000008;
            public static readonly uint Empty = 0x00000010;
            public static readonly uint Present = 0x00000020;
            public static readonly uint Atrmatch = 0x00000040;
            public static readonly uint Exclusive = 0x00000080;
            public static readonly uint Inuse = 0x00000100;
            public static readonly uint Mute = 0x00000200;
            public static readonly uint Unpowered = 0x00000400;
        }

        //public const int SCARD_S_SUCCESS = 0;
        // Defindes the operation scope and returns a new handle for resource manager context
        [DllImport("winscard.dll")]
        internal static extern int SCardEstablishContext(int dwScope, IntPtr pReserved1, IntPtr pReserved2, out int hContext);
        // Gets all smart card readers
        [DllImport("winscard.dll", EntryPoint = "SCardListReadersA", CharSet = CharSet.Ansi)]
        internal static extern int SCardListReaders(int hContext, byte[] cardReaderGroups, byte[] readersBuffer, out uint readersBufferLength);
        // Gets status of a smartcard
        [DllImport("winscard.dll")]
        internal static extern int SCardGetStatusChange(int hContext, uint timeoutMilliseconds, [In, Out] SmartCardReaderState[] readerStates, int readerCount);

        /// <summary>
        /// Split all readers at \o
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        private static List<string> ParseReaderBuffer(byte[] buffer)
        {
            string str = Encoding.ASCII.GetString(buffer);
            if (string.IsNullOrEmpty(str))
            {
                return new List<string>();
            }

            return new List<string>(str.Split(new char[] { '\0' }, StringSplitOptions.RemoveEmptyEntries));
        }

        /// <summary>
        /// checks if flag is set
        /// </summary>
        /// <param name="mask"></param>
        /// <param name="flagList"></param>
        /// <returns></returns>
        private static bool CheckIfFlagsSet(uint mask, params uint[] flagList)
        {
            foreach (uint flag in flagList)
            {
                if (IsFlagSet(mask, flag))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// check for flag
        /// </summary>
        /// <param name="mask"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        private static bool IsFlagSet(uint mask, uint flag)
        {
            return ((flag & mask) > 0);
        }

        /// <summary>
        /// Checks if a card is present
        /// </summary>
        /// <returns></returns>
        public static bool CardIsPresent()
        {
            if (debug)
            {
                return GetDebugValue();
            }
            try
            {
                addLog("******** NEW CARD CHECK ********");
                //https://docs.microsoft.com/de-de/windows/win32/secauthn/authentication-return-values
                addLog("Creating smart card handle");
                int result = SCardEstablishContext(SmartCardScope.User, IntPtr.Zero, IntPtr.Zero, out int context);
                addLog("Result: 0x" + result.ToString("X"));

                addLog("Creating buffer for card readers");
                uint bufferLength = 10000;
                byte[] readerBuffer = new byte[bufferLength];

                addLog("Buffering card readers");
                result = SCardListReaders(context, null, readerBuffer, out bufferLength);
                addLog("Result: 0x" + result.ToString("X"));

                addLog("Parsing card readers");
                List<string> readers = ParseReaderBuffer(readerBuffer);
                addLog(readers.Count.ToString() + " Card Reader(s) found");

                if (readers.Any())
                {
                    addLog("Getting reader states");
                    SmartCardReaderState[] readerStates = readers.Select(cardReaderName => new SmartCardReaderState() { cardReaderString = cardReaderName }).ToArray();
                    result = SCardGetStatusChange(context, 1000, readerStates, readerStates.Length);
                    addLog("Result: 0x" + result.ToString("X"));

                    readerStates.ToList().ForEach(readerState => addLog(string.Format("Reader: {0}, State: {1}", readerState.cardReaderString,
                        CheckIfFlagsSet(readerState.eventState, SmartCardState.Present, SmartCardState.Atrmatch) ? "Card Present" : "Card Absent")));

                    // Programm only checks for first reader
                    if (CheckIfFlagsSet(readerStates[0].eventState, SmartCardState.Present, SmartCardState.Atrmatch))
                    {
                        addLog("First card found is present. returning true");
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                addLog("An error occured: " + ex.Message);
            }
            addLog("returning false");
            return false;
        }

        /// <summary>
        /// Logs
        /// </summary>
        private static string log;

        /// <summary>
        /// This method adds new event messages to the log
        /// </summary>
        /// <param name="message"></param>
        private static void addLog(string message)
        {
            string logMessage = string.Format("[{0}]: {1}", System.DateTime.Now.ToString("HH:mm:ss"), message);
            log += (logMessage + "\r\n");
            Console.WriteLine(logMessage);
        }

        /// <summary>
        /// This method returns the log
        /// </summary>
        /// <returns></returns>
        public static string getLog()
        {
            return log;
        }
    }
}
