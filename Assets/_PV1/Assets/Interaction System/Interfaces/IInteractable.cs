namespace PV.Interaction
{

    public interface IInteractable
    {
        public bool OnInteract();

        public InteractableSettings GetSettings();
    }
}