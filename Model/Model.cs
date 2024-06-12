using Logika;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public abstract class ModelAbstract
    {
        public static ModelAbstract createApi(Logic logic)
        {
            return new Model(logic);
        }
        public abstract ObservableCollection<CircleModel> getCircleModels();        
        public abstract void start(int width, int height, int amount, int radius);
        private class Model: ModelAbstract 
        {
            private ObservableCollection<CircleModel> circleModels=new ObservableCollection<CircleModel>();
            private Logic logic;
            public Model(Logic logic)
            {
                this.logic = logic;
            }
            public override ObservableCollection<CircleModel> getCircleModels()
            {
                return circleModels;
            }
            public override void start(int width, int height, int amount, int radius)
            {
                circleModels.Clear();
                logic.createPlane(width, height, amount, radius);
                foreach(CircleLogic circle in logic.GetCircleLogics())
                {
                    circleModels.Add(new CircleModel(circle));
                }


            }
          

        }

    }
}
