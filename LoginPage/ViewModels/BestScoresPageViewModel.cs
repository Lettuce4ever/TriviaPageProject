using LoginPage.Models;
using LoginPage.Service;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LoginPage.ViewModels
{
    public class BestScoresPageViewModel:ViewModel
    {
        private Player selectedPlayer;
        private string darga;
        private int id;
        private bool ReOrder = false;
        private int selectedIndex;
        public int SelectedIndex { get { return selectedIndex; } set { if (selectedIndex != value) { selectedIndex = value; OnPropertyChanged(); } } }

        private Darga selectedDarga;
        public Darga SelectedDarga { get => selectedDarga; set { selectedDarga = value; OnPropertyChanged(); } }
        RankService dargaService;

        private List<Player> filteredList;
        

        private List<Player> fullPlayers;
        UserService userService;
        public ObservableCollection<Player> Players { get; set; }

        public ObservableCollection<Darga> DargasL { get; set; }
        //public ICommand FilterCommand { get; set; }
        public ICommand LoadPlayersCommand { get; private set; }
        public ICommand ResetCommand { get; set; }
        public ICommand ReorderCommand { get; set; }
        public string Darga { get => darga; set { darga = value; OnPropertyChanged(); } }
        public int ID { get => id; set { id = value; OnPropertyChanged(); } }
        public Player SelectedPlayer { get => selectedPlayer; set { selectedPlayer = value; OnPropertyChanged(); SpecificPlayer(); } }

        public BestScoresPageViewModel(UserService userService, RankService dargaService)
        {
            
            fullPlayers = new List<Player>();
            this.userService = userService;
            this.dargaService = dargaService;
            DargasL = new ObservableCollection<Darga>();

            foreach (Darga d in dargaService.Dargas)
            {
                DargasL.Add(d);
            }


            Players = new ObservableCollection<Player>();
            //FilterCommand = new Command(async () => await FilterByLevel());
            LoadPlayersCommand = new Command(async () => await LoadPlayers());
            ResetCommand = new Command(async () => await Reset());
            ReorderCommand = new Command(async () => await Reorder());
            selectedIndex = -1;
        }
        private async Task LoadPlayers()
        {
            if (SelectedIndex != -1)
            {
                fullPlayers = userService.ShowByEither(ReOrder);
                fullPlayers = fullPlayers.Where(x => x.Darga.DargaName == SelectedDarga.DargaName).ToList();
            }
            else { fullPlayers = userService.ShowByEither(ReOrder); }
            Players.Clear();
            foreach (var Player in fullPlayers)
                Players.Add(Player);
        }
        private async Task Reset()
        {
            SelectedIndex = -1;
            ReOrder = false;
            LoadPlayers();
            SelectedPlayer= null;
            SpecificPlayer();
        }
        private async Task Reorder()
        {
            if(!ReOrder)
            ReOrder = true;
            else
                ReOrder= false;
            LoadPlayers();
            
        }
        private void SpecificPlayer()
        {
            if (SelectedPlayer != null)
            {
                Darga = SelectedPlayer.Darga.DargaName;
                ID = SelectedPlayer.PlayerId;
            }
            else
            {
                Darga = string.Empty;
                ID = 0;
            }
            

        }

        //private async Task FilterByLevel()
        //{
        //    if (SelectedIndex != -1)
        //        filteredList = fullPlayers.Where(x => x.Darga.DargaName == SelectedDarga.DargaName).ToList();
        //    else
        //        filteredList = fullPlayers;
        //    DargasL.Clear();
        //    foreach (Player p in filteredList)
        //    {
        //        Players.Add(p);
        //    }
        //}


    }
}
