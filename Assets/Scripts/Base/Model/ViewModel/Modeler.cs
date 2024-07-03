using System;
using Saem;
using UniRx;
namespace Saem
{
    public class Modeler<T> : ViewModel where T : Model
    {
        ReactiveProperty<T> modelProperty = new ReactiveProperty<T>();

        public IObservable<T> getStream()
        {
            return modelProperty.Where(model => model != null);
        }

        public Modeler(T model)
        {
            setModel(model);
        }

        public Modeler()
        {

        }

        public virtual void initModel(T model)
        {
            setModel(model);
        }

        void setModel(T model)
        {
            modelProperty.Value = model;
        }

        public T getModel()
        {
            return modelProperty.Value;
        }
    }
}