namespace DogProject
{
	public class BaseManager 
	{
        protected Facade facade;

        public BaseManager(Facade facade)
        {
            this.facade = facade;
        }

        public virtual void OnInit() { }
        public virtual void OnUpdate() { }
        public virtual void OnDestroy() { }
	}
}
