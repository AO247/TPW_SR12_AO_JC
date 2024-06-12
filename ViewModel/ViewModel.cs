using Logika;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
namespace ViewModel
{
    public class ViewModel : INotifyPropertyChanged
    {
        
        private ModelAbstract model;
        private int circleCount;
        private int circleRadius = 30;
        private ObservableCollection<CircleModel> circleModels = new ObservableCollection<CircleModel>();
        public ICommand Start { get; set; }

        public ViewModel()
        {
            Logic logicAPI = Logic.CreateAPI();
            model = ModelAbstract.createApi(logicAPI);
            Start = new RelayCommand(() => start());
        }
        public int CircleCount
        {
            get { return circleCount; }
            set
            {
                circleCount = value;
                OnPropertyChanged(nameof(circleCount));
            }
        }
        public int CircleRadius
        {
            get { return circleRadius; }
            set
            {
                circleRadius = value;
                OnPropertyChanged(nameof(circleRadius));
            }
        }
        public ObservableCollection<CircleModel> CircleModels
        {
            get {
                
                return circleModels;
                
            }
            set { circleModels = value; OnPropertyChanged(nameof(CircleModels)); }
        }

        public void start()
        {
            
            model.start(750, 350, CircleCount, CircleRadius);
           
            this.CircleModels=model.getCircleModels();

            
        }






        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
