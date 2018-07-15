using System;

using EmuController.Controls;
using EmuController.PodItems;

namespace EmuController.PodControllers
{
    public class SystemController : PodController
    {
        //private MameController _mame = new MameController();
        //private PlaystationController _playstation = new PlaystationController();
        //private SnesController _snes = new SnesController();
        //private GenesisController _genesis = new GenesisController();
        private NesController _nes = new NesController();
        //private GameboyController _gameboy = new GameboyController();
        //private Atari2600Controller _atari2600 = new Atari2600Controller();

        public SystemController()
        {
            //Items.Add(new SimpleItem("MAME", "mame", _mame));
            //Items.Add(new SimpleItem("Playstation", "psx", _playstation));
            //Items.Add(new SimpleItem("Super Nintendo", "snes", _snes));
            //Items.Add(new SimpleItem("Genesis", "genesis", _genesis));
            Items.Add(new SimpleItem("Nintendo", "nes", _nes));
            //Items.Add(new SimpleItem("Game Boy", "gameboy", _gameboy));
            //Items.Add(new SimpleItem("Atari 2600", "atari2600", _atari2600));
        }

        public void Reload()
        {
            //_mame.Reload();
            //_playstation.Reload();
            //_snes.Reload();
            //_genesis.Reload();
            _nes.Reload();
            //_gameboy.Reload();
            //_atari2600.Reload();
        }
    }
}
