//
// Script name: ISubject
//
//
// Programmer: Kentaurus
//

public interface ISubject
{
	void RegisterObserver(IObserver observer);
	void UnregisterObserver(IObserver observer);
	void NotifyObservers(params object[] args);
}
