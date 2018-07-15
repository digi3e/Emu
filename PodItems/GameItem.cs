using System;
using System.Xml;

using EmuController.Controls;
using EmuController.PodControllers;

namespace EmuController.PodItems
{
    public class GameItem : PodItem
    {
        private GameSystem _system;
        private XmlNode _game;
        private PodController _controller;

        public GameItem(GameSystem system, XmlNode game, string name)
            : this(system, game, name, null)
        {
        }

        public GameItem(GameSystem system, XmlNode game, string name, PodController controller)
            : base(name)
        {
            _system = system;
            _game = game;
            _controller = controller;
        }

        public override bool IsLeaf
        {
            get { return (_controller == null); }
        }

        public override PodController GetChildController()
        {
            return _controller;
        }

        public GameSystem GameSystem
        {
            get { return _system; }
        }

        public XmlNode Game
        {
            get { return _game; }
        }

        public static void RunGame(XmlNode game)
        {
            XmlNode system = game.ParentNode;

            if (system.LocalName == "clones")
                system = system.ParentNode.ParentNode;

            switch (system.LocalName)
            {
                case "mame":
                    MameController.RunGame(game);
                    break;

                case "playstation":
                    PlaystationController.RunGame(game);
                    break;

                case "snes":
                    SnesController.RunGame(game);
                    break;

                case "genesis":
                    GenesisController.RunGame(game);
                    break;

                case "nes":
                    NesController.RunGame(game);
                    break;

                case "gameboy":
                    GameboyController.RunGame(game);
                    break;

                case "atari2600":
                    Atari2600Controller.RunGame(game);
                    break;
            }
        }

        public override void RunAction()
        {
            RunGame(_game);
        }
    }
}
