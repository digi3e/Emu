using System;
using System.IO;

namespace EmuController.Utility
{
    public class SystemHelper
    {
        private static string[] _imageExtensions = new string[] { "png", "jpg", "gif", "bmp" };

        private SystemHelper()
        {
        }

        public static string GetDirectoryForGameSystem(GameSystem type)
        {
            switch (type)
            {
                case GameSystem.Mame:
                    return Config.MameDirectory;

                case GameSystem.Playstation:
                    return Config.PlaystationDirectory;

                case GameSystem.Snes:
                    return Config.SnesDirectory;
                
                case GameSystem.Genesis:
                    return Config.GenesisDirectory;
                
                case GameSystem.Nes:
                    return Config.NesDirectory;
                
                case GameSystem.Gameboy:
                    return Config.GameboyDirectory;
                
                case GameSystem.Atari2600:
                    return Config.Atari2600Directory;
            }

            return null;
        }

        public static string GetImageFilename(string directory, string name)
        {
            string baseName = directory + name;

            foreach (string extension in _imageExtensions)
            {
                string filename = baseName + "." + extension;

                if (File.Exists(filename))
                    return filename;
            }

            return null;
        }
    }
}
