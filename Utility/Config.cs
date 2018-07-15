using System;
using System.IO;

namespace EmuController.Utility
{
    public class Config
    {
        private static string _pathPrefix = "M:\\data\\emu";

        private Config()
        {
        }

        public static void Initialize()
        {
            _pathPrefix = Directory.GetCurrentDirectory();
        }

        public static string MameDirectory
        {
            get { return _pathPrefix + @"\mame\"; }
        }

        public static string PlaystationDirectory
        {
            get { return _pathPrefix + @"\psx\"; }
        }

        public static string SnesDirectory
        {
            get { return _pathPrefix + @"\snes\"; }
        }

        public static string GenesisDirectory
        {
            get { return _pathPrefix + @"\genesis\"; }
        }

        public static string NesDirectory
        {
            get { return _pathPrefix + @"\nes\"; }
        }

        public static string GameboyDirectory
        {
            get { return _pathPrefix + @"\gameboy\"; }
        }

        public static string Atari2600Directory
        {
            get { return _pathPrefix + @"\atari2600\"; }
        }
    }
}
