using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Smart_Card_Reminder
{
    class Smartcard
    {
        private static string log;
        public static class SmartCardScope
        {
            public static readonly Int32 User = 0;
            public static readonly Int32 Terminal = 1;
            public static readonly Int32 System = 2;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct SmartCardReaderState
        {
            public string cardReaderString;
            public IntPtr userDataPointer;
            public UInt32 currentState;
            public UInt32 eventState;
            public UInt32 atrLength;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 36)]
            public byte[] ATR;
        }

        public static class SmartCardState
        {
            public static readonly UInt32 Unaware = 0x00000000;
            public static readonly UInt32 Ignore = 0x00000001;
            public static readonly UInt32 Changed = 0x00000002;
            public static readonly UInt32 Unknown = 0x00000004;
            public static readonly UInt32 Unavailable = 0x00000008;
            public static readonly UInt32 Empty = 0x00000010;
            public static readonly UInt32 Present = 0x00000020;
            public static readonly UInt32 Atrmatch = 0x00000040;
            public static readonly UInt32 Exclusive = 0x00000080;
            public static readonly UInt32 Inuse = 0x00000100;
            public static readonly UInt32 Mute = 0x00000200;
            public static readonly UInt32 Unpowered = 0x00000400;
        }

        public const int SCARD_S_SUCCESS = 0;

        // Defindes the operation scope and returns a new handle for resource manager context
        [DllImport("winscard.dll")]
        internal static extern int SCardEstablishContext(Int32 dwScope, IntPtr pReserved1, IntPtr pReserved2, out Int32 hContext);

        // Gets all smart card readers
        [DllImport("winscard.dll", EntryPoint = "SCardListReadersA", CharSet = CharSet.Ansi)]
        internal static extern int SCardListReaders(Int32 hContext, byte[] cardReaderGroups, byte[] readersBuffer, out UInt32 readersBufferLength);

        // Gets status of a smartcard
        [DllImport("winscard.dll")]
        internal static extern int SCardGetStatusChange(Int32 hContext, UInt32 timeoutMilliseconds, [In, Out] SmartCardReaderState[] readerStates, Int32 readerCount);

        private static List<string> ParseReaderBuffer(byte[] buffer)
        {
            var str = Encoding.ASCII.GetString(buffer);
            if (string.IsNullOrEmpty(str)) return new List<string>();
            return new List<string>(str.Split(new char[] { '\0' }, StringSplitOptions.RemoveEmptyEntries));
        }

        private static bool CheckIfFlagsSet(UInt32 mask, params UInt32[] flagList)
        {
            foreach (UInt32 flag in flagList)
            {
                if (IsFlagSet(mask, flag)) return true;
            }

            return false;
        }

        private static bool IsFlagSet(UInt32 mask, UInt32 flag)
        {
            return ((flag & mask) > 0);
        }

        public static bool CardIsPresent()
        {
            try
            {
                addLog("******** NEW CARD CHECK ********");
                int context = 0;
                //addLog("Return Values: https://docs.microsoft.com/de-de/windows/win32/secauthn/authentication-return-values");
                addLog("Creating smart card handle");
                var result = SCardEstablishContext(SmartCardScope.User, IntPtr.Zero, IntPtr.Zero, out context);
                addLog("Result: 0x" + result.ToString("X"));

                addLog("Creating buffer for card readers");
                uint bufferLength = 10000;
                byte[] readerBuffer = new byte[bufferLength];

                addLog("Buffering card readers");
                result = SCardListReaders(context, null, readerBuffer, out bufferLength);
                addLog("Result: 0x" + result.ToString("X"));

                addLog("Parsing card readers");
                var readers = ParseReaderBuffer(readerBuffer);
                addLog(readers.Count.ToString() + " Card Reader(s) found");

                if (readers.Any())
                {
                    addLog("Getting reader states");
                    var readerStates = readers.Select(cardReaderName => new SmartCardReaderState() { cardReaderString = cardReaderName }).ToArray();
                    result = SCardGetStatusChange(context, 1000, readerStates, readerStates.Length);
                    addLog("Result: 0x" + result.ToString("X"));

                    readerStates.ToList().ForEach(readerState => addLog(String.Format("Reader: {0}, State: {1}", readerState.cardReaderString,
                        CheckIfFlagsSet(readerState.eventState, SmartCardState.Present, SmartCardState.Atrmatch) ? "Card Present" : "Card Absent")));

                    // Programm only checks for first reader
                    if (CheckIfFlagsSet(readerStates[0].eventState, SmartCardState.Present, SmartCardState.Atrmatch))
                    {
                        addLog("First card found is present. returning true");
                        return true;
                    }                  
                }
            }
            catch(Exception ex)
            {
                addLog("An error occured: " + ex.Message);
            }
            addLog("returning false");
            return false;
        }
        /// <summary>
        /// This method adds new event messages to the log
        /// </summary>
        /// <param name="message"></param>
        private static void addLog(string message)
        {
            string logMessage = String.Format("[{0}]: {1}", System.DateTime.Now.ToString("HH:mm:ss"), message);
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
