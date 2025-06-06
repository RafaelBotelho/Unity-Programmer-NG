public interface IInteractable
{
    bool isInteractable { get; }
    string GetDescription();
    void Interact();
}